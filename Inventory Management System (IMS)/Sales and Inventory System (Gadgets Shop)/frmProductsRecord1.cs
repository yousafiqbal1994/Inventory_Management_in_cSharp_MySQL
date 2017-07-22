using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Inventory_Management_System
{
    public partial class frmProductsRecord1 : Form
    {
        MySqlConnection con = null;
        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;

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


        public frmProductsRecord1()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {

                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Select("SELECT ProductName as \"Product Name\",CategoryName as \"Category Name\",SupplierName as \"Supplier Name\" from Product order by ProductName");
                con = new MySqlConnection(connString());
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }
        private void frmProductsRecord_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Select("SELECT ProductName as \"Product Name\",CategoryName as \"Category Name\",SupplierName as \"Supplier Name\" from Product where ProductName like '" + txtProductname.Text + "%' order by ProductName");
                con = new MySqlConnection(connString());
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Select("SELECT ProductName as \"Product Name\",CategoryName as \"Category Name\",SupplierName as \"Supplier Name\" from Product where CategoryName like '" + txtCategory.Text + "%' order by ProductName");
                con = new MySqlConnection(connString());
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }

        private void txtCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Select("SELECT ProductName as \"Product Name\",CategoryName as \"Category Name\",SupplierName as \"Supplier Name\" from Product where SupplierName like '" + txtCompany.Text + "%' order by ProductName");
                con = new MySqlConnection(connString());
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }

      
        private void frmProductsRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmProduct frm = new frmProduct();
            frm.Show();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
     
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Hide();
                frmProduct frm = new frmProduct();
                frm.Show();
                frm.txtProductName.Text = dr.Cells[0].Value.ToString();
                frm.textBox1.Text = dr.Cells[0].Value.ToString();
                frm.cmbCategory.Text = dr.Cells[1].Value.ToString();
                frm.cmbCompany.Text = dr.Cells[2].Value.ToString();
                frm.btnUpdate.Enabled = true;
                frm.btnDelete.Enabled = true;
                frm.btnSave.Enabled = false;
                frm.txtProductName.Focus();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
