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
    public partial class frmSalesRecord : Form
    {

        MySqlConnection con = null;
        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;

        MySqlDataReader rdr = null;
        MySqlCommand cmd = null;

        DataTable dTable = new DataTable();
        DataSet ds = new DataSet();
        MySqlDataAdapter adp;
        MySqlTransaction tran;



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
            dTable = new DataTable();
            try
            {
                cmd = new MySqlCommand(query,new MySqlConnection(connString()));
                adp = new MySqlDataAdapter(cmd); //c.con is the connection string
                adp.Fill(dTable);
                return dTable;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while loading Grid View\n" + ex.Message);
                return dTable;
            }
        }


        public frmSalesRecord()
        {
            InitializeComponent();
        }

        private void frmSalesRecord_Load(object sender, EventArgs e)
        {
            FillCombo();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
        //    if (DataGridView1.DataSource == null)
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

        //        rowsTotal = DataGridView1.RowCount - 1;
        //        colsTotal = DataGridView1.Columns.Count - 1;
        //        var _with1 = excelWorksheet;
        //        _with1.Cells.Select();
        //        _with1.Cells.Delete();
        //        for (iC = 0; iC <= colsTotal; iC++)
        //        {
        //            _with1.Cells[1, iC + 1].Value = DataGridView1.Columns[iC].HeaderText;
        //        }
        //        for (I = 0; I <= rowsTotal - 1; I++)
        //        {
        //            for (j = 0; j <= colsTotal; j++)
        //            {
        //                _with1.Cells[I + 2, j + 1].value = DataGridView1.Rows[I].Cells[j].Value;
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

        private void Button4_Click(object sender, EventArgs e)
        {
        //    if (DataGridView2.DataSource == null)
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

        //        rowsTotal = DataGridView2.RowCount - 1;
        //        colsTotal = DataGridView2.Columns.Count - 1;
        //        var _with1 = excelWorksheet;
        //        _with1.Cells.Select();
        //        _with1.Cells.Delete();
        //        for (iC = 0; iC <= colsTotal; iC++)
        //        {
        //            _with1.Cells[1, iC + 1].Value = DataGridView2.Columns[iC].HeaderText;
        //        }
        //        for (I = 0; I <= rowsTotal - 1; I++)
        //        {
        //            for (j = 0; j <= colsTotal; j++)
        //            {
        //                _with1.Cells[I + 2, j + 1].value = DataGridView2.Rows[I].Cells[j].Value;
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

        private void Button7_Click(object sender, EventArgs e)
        {
        //    if (DataGridView3.DataSource == null)
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

        //        rowsTotal = DataGridView3.RowCount - 1;
        //        colsTotal = DataGridView3.Columns.Count - 1;
        //        var _with1 = excelWorksheet;
        //        _with1.Cells.Select();
        //        _with1.Cells.Delete();
        //        for (iC = 0; iC <= colsTotal; iC++)
        //        {
        //            _with1.Cells[1, iC + 1].Value = DataGridView3.Columns[iC].HeaderText;
        //        }
        //        for (I = 0; I <= rowsTotal - 1; I++)
        //        {
        //            for (j = 0; j <= colsTotal; j++)
        //            {
        //                _with1.Cells[I + 2, j + 1].value = DataGridView3.Rows[I].Cells[j].Value;
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

        private void Button9_Click(object sender, EventArgs e)
        {
        DataGridView3.DataSource = null;
        cmbCustomerName.Text = "";
        GroupBox4.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
        DataGridView1.DataSource = null;
        dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
        dtpInvoiceDateTo.Text = DateTime.Today.ToString();
        GroupBox3.Visible = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
        DateTimePicker1.Text = DateTime.Today.ToString();
        DateTimePicker2.Text = DateTime.Today.ToString();
        DataGridView2.DataSource = null;
        GroupBox10.Visible = false;
        }
        public void FillCombo()
        {

            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                adp = new MySqlDataAdapter();
                adp.SelectCommand = new MySqlCommand("SELECT distinct CusName FROM invoice,Customer where invoice.CusID=Customer.CusID",con);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dTable = ds.Tables[0];
                cmbCustomerName.Items.Clear();
                foreach (DataRow drow in dTable.Rows)
                {
                    cmbCustomerName.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
            GroupBox3.Visible = true;
            con = new MySqlConnection(connString());
            con.Open();
            cmd = new MySqlCommand("SELECT (invoiceNo) as \"Invoice No\",invoiceDate,(invoice.CusID) as \"Customer ID\",(CusName) as \"Customer Name\",(GrandTotal) as \"Grand Total\",(TotalPayment) as \"Total Payment\",(PaymentDue) as \"Payment Due\" from invoice,Customer where invoice.CusID=Customer.CusID and InvoiceDate between #'" + dtpInvoiceDateFrom.Text + "'# And #'" + dtpInvoiceDateTo.Text + "'# order by InvoiceDate desc",con);
            MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
            DataSet myDataSet = new DataSet();
            myDA.Fill(myDataSet, "Invoice");
            myDA.Fill(myDataSet, "Customer");
            DataGridView1.DataSource = myDataSet.Tables["Customer"].DefaultView;
            DataGridView1.DataSource = myDataSet.Tables["invoice"].DefaultView;
            Int64 sum = 0;
            Int64 sum1 = 0;
            Int64 sum2 = 0;

            foreach (DataGridViewRow r in this.DataGridView1.Rows)
            {
                Int64 i = Convert.ToInt64(r.Cells[4].Value);
                Int64 j = Convert.ToInt64(r.Cells[5].Value);
                Int64 k = Convert.ToInt64(r.Cells[6].Value);
                sum = sum + i;
                sum1 = sum1 + j;
                sum2 = sum2 + k;
              
            }
            TextBox1.Text = sum.ToString();
            TextBox2.Text = sum1.ToString();
            TextBox3.Text = sum2.ToString();

            con.Close();
            }
        catch (Exception ex)
            {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            GroupBox4.Visible = true;
            con = new MySqlConnection(connString());
            con.Open();
            cmd = new MySqlCommand("SELECT (invoiceNo) as \"Invoice No\",(invoice.CusID) as \"Customer ID\",(CusName) as \"Customer Name\",(GrandTotal) as \"Grand Total\",(TotalPayment) as \"Total Payment\",(PaymentDue) as \"Payment Due\" from invoice,Customer where invoice.CusID=Customer.CusID and Cusname='" + cmbCustomerName.Text + "' order by CusName",con);
            MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
            DataSet myDataSet = new DataSet();
            myDA.Fill(myDataSet, "Invoice");
            myDA.Fill(myDataSet, "Customer");
            DataGridView3.DataSource = myDataSet.Tables["Customer"].DefaultView;
            DataGridView3.DataSource = myDataSet.Tables["Invoice"].DefaultView;
            Int64 sum = 0;
            Int64 sum1 = 0;
            Int64 sum2 = 0;

            foreach (DataGridViewRow r in this.DataGridView3.Rows)
            {
                Int64 i = Convert.ToInt64(r.Cells[3].Value);
                Int64 j = Convert.ToInt64(r.Cells[4].Value);
                Int64 k = Convert.ToInt64(r.Cells[5].Value);
                sum = sum + i;
                sum1 = sum1 + j;
                sum2 = sum2 + k;
            }
            TextBox6.Text = sum.ToString();
            TextBox5.Text = sum1.ToString();
            TextBox4.Text = sum2.ToString();

            con.Close();
            }
        catch (Exception ex)
            {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                GroupBox10.Visible = true;
                con = new MySqlConnection(connString());
                con.Open();
                cmd = new MySqlCommand("SELECT (invoiceNo) as \"Invoice No\",(InvoiceDate) as \"Invoice Date\",(invoice.CusID) as \"Customer ID\",(CusName) as \"Customer Name\",(GrandTotal) as \"Grand Total\",(TotalPayment) as \"Total Payment\",(PaymentDue) as \"Payment Due\" from invoice,Customer where invoice.CusID=Customer.CusID and InvoiceDate between #" + DateTimePicker2.Text + "# And #" + DateTimePicker1.Text + "# and PaymentDue > 0 order by InvoiceDate desc",con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice");
                myDA.Fill(myDataSet, "Customer");
                DataGridView2.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView2.DataSource = myDataSet.Tables["Invoice"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView2.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[4].Value);
                    Int64 j = Convert.ToInt64(r.Cells[5].Value);
                    Int64 k = Convert.ToInt64(r.Cells[6].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                TextBox12.Text = sum.ToString();
                TextBox11.Text = sum1.ToString();
                TextBox10.Text = sum2.ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
     
        }

     
        private void DataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView3.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView3.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
     
        }

        private void DataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView2.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView2.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
     
        }

       
        private void TabControl1_Click(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            GroupBox3.Visible = false;
            DataGridView3.DataSource = null;
            cmbCustomerName.Text = "";
            GroupBox4.Visible = false;
            DateTimePicker1.Text = DateTime.Today.ToString();
            DateTimePicker2.Text = DateTime.Today.ToString();
            DataGridView2.DataSource = null;
            GroupBox10.Visible = false;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptSales rpt = new rptSales();
                //The report you created.
                cmd = new MySqlCommand();
                MySqlDataAdapter myDA = new MySqlDataAdapter();
                //SIS_DBDataSet myDS = new SIS_DBDataSet();
                DataSet myDS = new DataSet();
                //The DataSet you created.
                con = new MySqlConnection(connString());
                cmd.Connection = con;
                cmd.CommandText = "SELECT invoice.InvoiceNo, invoice.InvoiceDate, invoice.CusID, invoice.SubTotal, invoice.TaxPercentage, invoice.TaxAmount,invoice.Discount ,invoice.GrandTotal, invoice.TotalPayment,invoice.PaymentDue,Customer.CusID AS Expr1, Customer.CusName,Customer.CusCity,Customer.CusMob FROM (invoice INNER JOIN Customer ON invoice.CusID = Customer.CusID) where InvoiceDate between #'" + dtpInvoiceDateFrom.Text + "'# And #'" + dtpInvoiceDateTo.Text + "'# order by InvoiceDate desc";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS,"invoice");
                myDA.Fill(myDS, "Customer");
                rpt.SetDataSource(myDS);
                frmSalesReport frm = new frmSalesReport();
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

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCustomerName.Text == "")
                {
                    MessageBox.Show("Please select Customer name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCustomerName.Focus();
                    return;
                }
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;

                rptSales rpt = new rptSales();
                //The report you created.
                cmd = new MySqlCommand();
                MySqlDataAdapter myDA = new MySqlDataAdapter();
                DataSet myDS = new DataSet();
                //The DataSet you created.
                con = new MySqlConnection(connString());
                cmd.Connection = con;
                cmd.CommandText = "SELECT Invoice.InvoiceNo, Invoice.CusID, Invoice.SubTotal, Invoice.TaxPercentage, Invoice.TaxAmount,Invoice.Discount, Invoice.GrandTotal, Invoice.TotalPayment,Invoice.PaymentDue, Customer.CusID AS Expr1, Customer.CusName, Customer.CusCity, Customer.CusMob FROM (Invoice INNER JOIN Customer ON Invoice.CusID = Customer.CusID) where Cusname='" + cmbCustomerName.Text + "' order by CusName,InvoiceDate";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "invoice");
                myDA.Fill(myDS, "Customer");
                rpt.SetDataSource(myDS);
                frmSalesReport frm = new frmSalesReport();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;

                rptSales rpt = new rptSales();
                //The report you created.
                cmd = new MySqlCommand();
                MySqlDataAdapter myDA = new MySqlDataAdapter();
                //SIS_DBDataSet myDS = new SIS_DBDataSet();

                DataSet myDS = new DataSet();
                //The DataSet you created.
                con = new MySqlConnection(connString());
                cmd.Connection = con;
                cmd.CommandText = "SELECT Invoice.InvoiceNo, Invoice.InvoiceDate, Invoice.CusID, Invoice.SubTotal, Invoice.TaxPercentage, Invoice.TaxAmount,Invoice.Discount, Invoice.GrandTotal, Invoice.TotalPayment,Invoice.PaymentDue, Customer.CusID AS \"Expr1\", Customer.CusName,  Customer.CusCity, Customer.CusMob FROM (Sales INNER JOIN Customer ON Sales.CusID = Customer.CusID) Where InvoiceDate between #" + DateTimePicker2.Text + "# And #" + DateTimePicker1.Text + "# and PaymentDue > 0 order by InvoiceDate desc";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "invoice");
                myDA.Fill(myDS, "Customer");
                rpt.SetDataSource(myDS);
                frmSalesReport frm = new frmSalesReport();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
