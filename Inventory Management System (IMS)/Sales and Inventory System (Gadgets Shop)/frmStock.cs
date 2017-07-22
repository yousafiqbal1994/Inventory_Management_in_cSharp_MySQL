using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace Inventory_Management_System
{
    public partial class frmStock : Form
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



        public frmStock()
        {
            InitializeComponent();
        }

        private void frmStock_Load(object sender, EventArgs e)
        {
            GetData();
        }
        //public static string GetUniqueKey(int maxSize)
        //{
        //    char[] chars = new char[62];
        //    chars =
        //    "123456789".ToCharArray();
        //    byte[] data = new byte[1];
        //    RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        //    crypto.GetNonZeroBytes(data);
        //    data = new byte[maxSize];
        //    crypto.GetNonZeroBytes(data);
        //    StringBuilder result = new StringBuilder(maxSize);
        //    foreach (byte b in data)
        //    {
        //        result.Append(chars[b % (chars.Length)]);
        //    }
        //    return result.ToString();
        //}
        //private void auto()
        //{
        //    txtInventoryID.Text = "S-" + GetUniqueKey(6);
        //}

        public void GetData()
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand("SELECT InventoryID as \"Inventory ID\", ProductName as \"Product Name\",Thickness,Colour,Size,sum(Quantity) as \"Quantity\",Price,sum(TotalPrice) as \"Total Price\" from Configuration,Inventory where Configuration.ConfigID=Inventory.ConfigID group by InventoryID, ProductName,Thickness,Colour,Size,Price having sum(Quantity > 0)  order by ProductName",con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Inventory");
                myDA.Fill(myDataSet, "Configuration");
                dataGridView1.DataSource = myDataSet.Tables["inventory"].DefaultView;
                dataGridView1.DataSource = myDataSet.Tables["configuration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmConfigRecord frm = new frmConfigRecord();
            frm.label1.Text = label8.Text;
            frm.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductname.Text == "")
            {
                MessageBox.Show("Please retrieve product name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProductname.Focus();
                return;
            }
            if (txtQty.Text == "")
            {
                MessageBox.Show("Please enter quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQty.Focus();
                return;
            }

            try
            {
            con = new MySqlConnection(connString());
            con.Open();
            String ct = "select ConfigID  from inventory where ConfigID=" + txtConfigID.Text + "" ;
            cmd = new MySqlCommand(ct);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();

            if (rdr.Read()==true) 
                {
                MessageBox.Show("Record already exists" + "\n" + "please update the stock of product", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
                if ((rdr != null))
                {
                    rdr.Close();
                }
                return;
                }
//                auto();
                con = new MySqlConnection(connString());
                con.Open();
                string cb = "insert into Inventory(ConfigID,Quantity,Totalprice,InventoryDate) VALUES ('" + txtConfigID.Text + "','" + txtQty.Text + "','" + txtTotalPrice.Text +"','" + dtpInventoryDate.Value + "')";
                cmd = new MySqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                btnSave.Enabled = false;
                GetData();
                frmMainMenu frm = new frmMainMenu();
                frm.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            double val1 = 0;
            int val2 = 0;
            double.TryParse(txtPrice.Text,out val1);
            int.TryParse(txtQty.Text, out val2);
            double I = (val1 * val2);
            txtTotalPrice.Text = I.ToString();
        }
        private void Reset()
        {
            txtConfigID.Text = "";
            txtThickness.Text = "";
            txtColour.Text = "";
            txtSize.Text = "";
            txtPrice.Text = "";
            txtProductname.Text = "";
            txtQty.Text = "";
            txtTotalPrice.Text = "";
            txtInventoryID.Text = "";
            dtpInventoryDate.Text = DateTime.Today.ToString();
            txtProduct.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
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
                string cq = "delete from Stock where StockID='" + txtInventoryID.Text + "'";
                cmd = new MySqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
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

        private void frmStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.lblUser.Text = label8.Text;
            frm.Show();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStockRecord1 frm = new frmStockRecord1();
            frm.label1.Text = label8.Text;
            frm.Show();
            frm.GetData();
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand("SELECT InventoryID as \"Inventory ID], ProductName as \"Product Name\",Thickness,Colour,Size,sum(Quantity) as \"Quantity\",Price,sum(TotalPrice) as \"Total Price\" from configuration,inventory where configuration.ConfigID=inventory.ConfigID and ProductName like '" + txtProduct.Text + "%' group by InventryID, ProductName,Thickness, Colour,Size,Price having sum(quantity > 0)  order by ProductName",con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Inventory");
                myDA.Fill(myDataSet, "Configuration");
                dataGridView1.DataSource = myDataSet.Tables["Inventory"].DefaultView;
                dataGridView1.DataSource = myDataSet.Tables["Configuration"].DefaultView;
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
                string cb = "Update inventory set ConfigID=" + txtConfigID.Text + ",Quantity=" + txtQty.Text + ",Totalprice=" + txtTotalPrice.Text + " where InventoryID='" + txtInventoryID.Text + "'";
                cmd = new MySqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                btnUpdate.Enabled = false;
                GetData();
                frmMainMenu frm = new frmMainMenu();
                frm.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStockRecord1 frm = new frmStockRecord1();
            frm.label1.Text = label8.Text;
            frm.Show();
            frm.GetData();
        }

       
    }
}
