using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
//using Excel = Microsoft.Office.Interop.Excel;
namespace Inventory_Management_System
{
    public partial class frmCustomersRecord : Form
    {

        MySqlDataReader rdr = null;
        MySqlConnection con = null;
        MySqlCommand cmd = null;


        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;

        private static MySqlConnection connection;
        MySqlCommand myCommand;

        DataTable dt = new DataTable();
        //       MySqlConnection myConnection = new MySqlConnection(connection.connString)
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
                myCommand = new MySqlCommand(query,new MySqlConnection(connString()));
                dataAdapter = new MySqlDataAdapter(myCommand); //c.con is the connection string
                dataAdapter.Fill(dt);
                return dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while loading Grid View\n" + ex.Message);
                return dt;
            }
        }

        public frmCustomersRecord()
        {
            InitializeComponent();
        }
        public void GetData()
        {
                try{
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand("SELECT CusID as \"Customer ID\",CusName as \"Customer Name\",CusCity as \"City\",CusMob as \"Mobile No\" from Customer order by CusName",con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "customer");
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

      

        private void txtCustomers_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand("SELECT CusID as \"Customer ID\",CusName as \"Customer Name\",CusCity as \"City\",CusMob as \"Mobile No\" from customer where CusName like '" + txtCustomers.Text + "%' order by CusName",con);
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

        private void Button3_Click(object sender, EventArgs e)
        {
        //    if (dataGridView1.DataSource == null)
        //    {
        //        MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    int rowsTotal = 0;
        //    int colsTotal = 0;
        //    int I = 0;
        //    int j = 0;
        //    int iC = 0;
        //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        //    Excel.Application xlApp = new Excel.Application();

        //    try
        //    {
        //        Excel.Workbook excelBook = xlApp.Workbooks.Add();
        //        Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
        //        xlApp.Visible = true;

        //        rowsTotal = dataGridView1.RowCount - 1;
        //        colsTotal = dataGridView1.Columns.Count - 1;
        //        var _with1 = excelWorksheet;
        //        _with1.Cells.Select();
        //        _with1.Cells.Delete();
        //        for (iC = 0; iC <= colsTotal; iC++)
        //        {
        //            _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
        //        }
        //        for (I = 0; I <= rowsTotal - 1; I++)
        //        {
        //            for (j = 0; j <= colsTotal; j++)
        //            {
        //                _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
        //            }
        //        }
        //        _with1.Rows["1:1"].Font.FontStyle = "Bold";
        //        _with1.Rows["1:1"].Font.Size = 12;

        //        _with1.Cells.Columns.AutoFit();
        //        _with1.Cells.Select();
        //        _with1.Cells.EntireColumn.AutoFit();
        //        _with1.Cells[1, 1].Select();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        //RELEASE ALLOACTED RESOURCES
        //        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        //        xlApp = null;
        //    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptCustomers rpt = new rptCustomers();
                //The report you created.
                cmd = new MySqlCommand();
                MySqlDataAdapter myDA = new MySqlDataAdapter();
                //SIS_DBDataSet myDS = new SIS_DBDataSet();
                DataSet myDS = new DataSet();
                //The DataSet you created.
                con = new MySqlConnection(connString());
                cmd.Connection = con;
                cmd.CommandText = "SELECT * from customer order by CusName";
                //dt = Select("SELECT * from customer");
                //cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS);
                rpt.SetDataSource(myDS);
                frmCustomersReport frm = new frmCustomersReport();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
    }
}
