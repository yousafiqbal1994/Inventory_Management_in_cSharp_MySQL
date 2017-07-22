using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Inventory_Management_System
{
    public partial class frmConfigRecord : Form
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



        public frmConfigRecord()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand("SELECT ProductName as \"Product Name\",ConfigID as \"Configuration ID\",Thickness,Colour,Size,Price as \"Price\" from configuration order by ProductName",con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet,"product");
                dataGridView1.DataSource = myDataSet.Tables["Product"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmConfigRecord_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand("SELECT * from product where ProductName like '" + txtProductname.Text + "%' order by ProductName",con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet,"product");
                dataGridView1.DataSource = myDataSet.Tables["Product"].DefaultView;
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

     
    
        private void frmConfigRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmStock frm = new frmStock();
            frm.label8.Text = label1.Text;
            frm.Show();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Hide();
               
                frmStock frm = new frmStock();
                frm.Show();
                frm.txtProductname.Text = dr.Cells[0].Value.ToString();
                frm.txtConfigID.Text = dr.Cells[1].Value.ToString();
                frm.txtThickness.Text = dr.Cells[2].Value.ToString();
                frm.txtColour.Text = dr.Cells[3].Value.ToString();
                frm.txtSize.Text = dr.Cells[4].Value.ToString();
                frm.txtPrice.Text = dr.Cells[5].Value.ToString();
                frm.label8.Text = label1.Text;
                frm.txtQty.Focus();
               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
