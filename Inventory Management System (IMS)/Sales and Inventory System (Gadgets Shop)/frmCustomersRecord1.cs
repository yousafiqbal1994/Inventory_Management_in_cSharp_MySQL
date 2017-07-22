using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Inventory_Management_System
{
    public partial class frmCustomersRecord1 : Form
    {

        MySqlConnection con = null;
        MySqlCommand cmd = null;


        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;

        MySqlCommand myCommand;

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


        public frmCustomersRecord1()
        {
            InitializeComponent();
        }
        public void GetData()
        {
                try{
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand( "SELECT CusID as \"Customer ID\",CusName as \"Customer Name\",CusCity as \"City\",CusMob as \"Mobile No\" from Customer order by CusName", con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Customer");
                dataGridView1.DataSource = myDataSet.Tables["Customer"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       }

        private void frmCustomersRecord_Load(object sender, EventArgs e)
        {
            GetData();
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
             try{
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmInvoice frm= new frmInvoice();
           frm.Show();
           frm.txtCustomerID.Text = dr.Cells[0].Value.ToString();
           frm.txtCustomerName.Text = dr.Cells[1].Value.ToString();
           frm.label6.Text = label1.Text;
       
             }
        
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCustomers_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand("SELECT CusID as \"Customer ID\",CusName as \"Customer Name\",CusCity as \"City\",CusMob as \"Mobile No\" from Customer where CusName like '" + txtCustomers.Text + "%' order by CusName", con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Customer");
                dataGridView1.DataSource = myDataSet.Tables["Customer"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCustomersRecord1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmInvoice frm = new frmInvoice();
            frm.label6.Text = label1.Text;
            frm.Show();
        }
    }
}
