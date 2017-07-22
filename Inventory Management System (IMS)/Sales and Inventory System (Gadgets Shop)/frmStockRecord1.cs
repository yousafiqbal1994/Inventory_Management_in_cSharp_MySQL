using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Inventory_Management_System
{
    public partial class frmStockRecord1 : Form
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
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password +";";
        }



        public frmStockRecord1()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                String sql = "SELECT InventoryID,configuration.ConfigID,ProductName as \"Product Name\",Thickness,Colour,Size,Price,Quantity,TotalPrice as \"Total Price\" from inventory,configuration where inventory.ConfigID=configuration.ConfigID order by ProductName";
                cmd = new MySqlCommand(sql,con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet,"configuration");
                dataGridView1.DataSource = myDataSet.Tables["configuration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmStockRecord_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                String sql = "SELECT InvnentoryID,configuration.ConfigID,ProductName as \"Product Name\",Thickness,Colour,Size,Price,Quantity,Totalprice \"Total Price\" from inventory,configuration where inventory.ConfigID=configuration.ConfigID and ProductName like '" + txtProductname.Text + "%' order by ProductName";
                cmd = new MySqlCommand(sql,con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet,"configuration");
                dataGridView1.DataSource = myDataSet.Tables["configuration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                frmStock frm = new frmStock();
                frm.Show();
                frm.txtInventoryID.Text = dr.Cells[0].Value.ToString();
                frm.txtConfigID.Text = dr.Cells[1].Value.ToString();
                frm.txtProductname.Text = dr.Cells[2].Value.ToString();
                frm.txtThickness.Text = dr.Cells[3].Value.ToString();
                frm.txtColour.Text = dr.Cells[4].Value.ToString();
                frm.txtSize.Text = dr.Cells[5].Value.ToString();
                frm.txtPrice.Text = dr.Cells[6].Value.ToString();
                frm.txtQty.Text = dr.Cells[7].Value.ToString();
                frm.txtTotalPrice.Text = dr.Cells[8].Value.ToString();
               // frm.dtpInventoryDate.Text =  DateTime.Parse(dr.Cells[9].Value.ToString()).ToString();
                frm.btnUpdate.Enabled = true;
                frm.btnDelete.Enabled = true;
                frm.btnSave.Enabled = false;
                frm.label8.Text = label1.Text;
           }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                String sql = "SELECT InventoryID,configuration.ConfigID,ProductName as \"Product Name\",Thickness,Colour,Size,Price,Quantity,Totalprice as \"Total Price\",InventoryDate from inventory,configuration where inventory.ConfigID=configuration.ConfigID and Quantity < 0 order by ProductName";
                cmd = new MySqlCommand(sql,con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet,"configuration");
                dataGridView1.DataSource = myDataSet.Tables["configuration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmStockRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmStock frm = new frmStock();
            frm.label8.Text = label1.Text;
            frm.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            txtProductname.Text = "";
            GetData();
        }
    }
}
