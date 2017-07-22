using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using Excel = Microsoft.Office.Interop.Excel;
namespace Inventory_Management_System
{
    public partial class frmProductsRecord : Form
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


        public frmProductsRecord()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {

                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Select("SELECT productName as \"Product Name\",CategoryName,SupplierName from Product order by Productname");
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
                dataGridView1.DataSource = Select("SELECT (productName) as \"Product Name\",CategoryName,SupplierName from Product where Productname like '" + txtProductname.Text + "%' order by Productname");
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
                dataGridView1.DataSource = Select("Product Name\",CategoryName,SupplierName from Product where CategoryName like '" + txtCategory.Text + "%' order by Productname");
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
                dataGridView1.DataSource = Select("SELECT (productName) as \"Product Name\",CategoryName,SupplierName from Product where SupplierName like '" + txtCompany.Text + "%' order by Productname");
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

        private void Button3_Click(object sender, EventArgs e)
        {
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
    }
}
