using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Inventory_Management_System
{
    public partial class frmCustomers : Form
    {
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        
        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        


        public string connString()
        {
            server = "localhost";
            database = "test";
            uid = "root";
            password = "seecs@123";
            return connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }


        public frmCustomers()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtCity.Text = "";
            txtCustomerName.Text = "";
            txtMobileNo.Text = "";
            txtCustomerID.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtCustomerName.Focus();

        }
        private void frmCustomers_Load(object sender, EventArgs e)
        {

        }

        
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (txtMobileNo.TextLength > 11)
            {
                MessageBox.Show("Only 11 digits are allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobileNo.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "")
            {
                MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Focus();
                return;
            }

            if (txtCity.Text == "")
            {
                MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCity.Focus();
                return;
            }


            if (txtMobileNo.Text == "")
            {
                MessageBox.Show("Please enter mobile no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobileNo.Focus();
                return;
            }

            try
            {
                con = new MySqlConnection(connString());
                con.Open();

                string cb = "insert into customer(CusName,CusCity,CusMob) values (@CusName,@CusCity,@CusMob)";

                cmd = new MySqlCommand(cb);

                cmd.Connection = con;

                cmd.Parameters.Add(new MySqlParameter("@CusName",MySql.Data.MySqlClient.MySqlDbType.VarChar,25,"CusName"));
                cmd.Parameters.Add(new MySqlParameter("@CusCity",MySql.Data.MySqlClient.MySqlDbType.VarChar,25,"CusCity"));
                cmd.Parameters.Add(new MySqlParameter("@CusMob",MySql.Data.MySqlClient.MySqlDbType.VarChar,12,"CusMob"));


                cmd.Parameters["@CusName"].Value = txtCustomerName.Text;
                cmd.Parameters["@CusCity"].Value = txtCity.Text;
                cmd.Parameters["@CusMob"].Value = txtMobileNo.Text;

                cmd.ExecuteReader();
                MessageBox.Show("Successfully saved","Customer Details",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnSave.Enabled = false;
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Close();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void delete_records()
        {

            try
            {

              int RowsAffected = 0;
              con = new MySqlConnection(connString());
              con.Open();
              string cq = "delete from Customer where CusID=@DELETE1;";
              cmd = new MySqlCommand(cq);
              cmd.Connection = con;
              cmd.Parameters.Add(new MySqlParameter("@DELETE1", MySql.Data.MySqlClient.MySqlDbType.VarChar, 8, "CusID"));
              cmd.Parameters["@DELETE1"].Value = txtCustomerID.Text;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                    con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Do you really want to delete the record?", "Customer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    delete_records();
                }

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

                string cb = "update customer set CusName = '" + txtCustomerName.Text + "',CusCity= '" + txtCity.Text + "',CusMob= '" + txtMobileNo.Text + "' where CusID= '" + txtCustomerID.Text + "'";
                cmd = new MySqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                MessageBox.Show("Successfully updated", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate.Enabled = false;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomersRecord2 frm = new frmCustomersRecord2();
            frm.Show();
            frm.GetData();
        }
    }
}
