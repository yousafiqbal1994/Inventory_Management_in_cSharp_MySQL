using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Inventory_Management_System
{
    public partial class frmProduct : Form
    {
        MySqlConnection con = null;
        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;

        MySqlDataReader rdr = null;
        MySqlCommand cmd = null;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        MySqlDataAdapter dataAdapter;



        public string connString()
        {
            server = "localhost";
            database = "test";
            uid = "root";
            password = "seecs@123";
            return connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }


        public DataTable Select(String query)
        {
            dt = new DataTable();
            try
            {
                cmd = new MySqlCommand(query,new MySqlConnection(connString()));
                dataAdapter = new MySqlDataAdapter(cmd); //c.con is the connection string
                dataAdapter.Fill(dt);
                return dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while loading Grid View\n" + ex.Message);
                return dt;
            }
        }



        public 
            frmProduct()
        {
            InitializeComponent();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            FillCombo();
            Autocomplete();
        }
        public void FillCombo()
        {
            try
            {

                con = new MySqlConnection(connString());
                con.Open();
                string ct = "select RTRIM(CategoryName) from Category order by CategoryName";
                cmd = new MySqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbCategory.Items.Add(rdr[0]);
                }
                con.Close();

                con = new MySqlConnection(connString());
                con.Open();
                string ct1 = "select RTRIM(SupplierName) from Supplier order by SupplierName";
                cmd = new MySqlCommand(ct1);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbCompany.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reset()
        {
            txtProductName.Text = "";
            cmbCompany.Text = "";
            cmbCategory.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            txtProductName.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text == "")
            {
                MessageBox.Show("Please enter product name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProductName.Focus();
                return;
            }
            if (cmbCategory.Text == "")
            {
                MessageBox.Show("Please select category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCategory.Focus();
                return;
            }
            if (cmbCompany.Text == "")
            {
                MessageBox.Show("Please select supplier", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCompany.Focus();
                return;
            }

            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                string ct = "select ProductName from Product where ProductName='" + txtProductName.Text + "'";

                cmd = new MySqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Product Name Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductName.Text = "";
                    txtProductName.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }

                con = new MySqlConnection(connString());
                con.Open();

                string cb = "insert into Product(ProductName,CategoryName,SupplierName) VALUES ('" + txtProductName.Text + "','" + cmbCategory.Text+ "','" + cmbCompany.Text+ "')";
                cmd = new MySqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                txtProductName.Text = "";
                cmbCategory.Text = "";
                cmbCompany.Text = "";
                txtProductName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        private void delete_records()
        {

            try
            {

                int RowsAffected = 0;
                con = new MySqlConnection(connString());
                con.Open();
                string cq = "delete from product where ProductName='" + txtProductName.Text + "'";
                cmd = new MySqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    Autocomplete();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    Autocomplete();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Autocomplete()
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT distinct ProductName FROM product", con);
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Product");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["productname"].ToString());

                }
                txtProductName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtProductName.AutoCompleteCustomSource = col;
                txtProductName.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();

                string cb = "update Product set productName='" + txtProductName.Text + "',CategoryName='" + cmbCategory.Text + "', CompanyName='" + cmbCompany.Text + "' where Productname='" + textBox1.Text + "'";
                cmd = new MySqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnUpdate.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProductsRecord1 frm = new frmProductsRecord1();
            frm.Show();
            frm.GetData();
        }
    }
}
