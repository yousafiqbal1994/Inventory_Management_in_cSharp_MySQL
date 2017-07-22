using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Inventory_Management_System
{
    public partial class frmSalesRecord1 : Form
    {

        MySqlConnection con = null;
        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;

        MySqlCommand cmd = null;

        DataTable dTable;
        DataSet ds = new DataSet();
        MySqlDataAdapter adp;



        public string connString()
        {
            server = "localhost";
            database = "test";
            uid = "root";
            password = "seecs@123";
            return connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }


        public frmSalesRecord1()
        {
            InitializeComponent();
        }

        private void frmSalesRecord_Load(object sender, EventArgs e)
        {
            FillCombo();
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
                adp.SelectCommand = new MySqlCommand("SELECT distinct CusName FROM invoice,customer where invoice.CusID=customer.CusID",con);
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
            cmd = new MySqlCommand("SELECT InvoiceNo as \"Invoice No\",invoice.CusID as \"Customer ID\",CusName as \"Customer Name\",SubTotal as \"Sub Total\",TaxPercentage as \"Tax %\",TaxAmount as \"Tax Amount\",GrandTotal as \"Grand Total\",TotalPayment as \"Total Payment\",PaymentDue as \"Payment Due\" from invoice,Customer where invoice.CusID=Customer.CusID and InvoiceDate between #'" + dtpInvoiceDateFrom.Text + "'# And #'" + dtpInvoiceDateTo.Text + "'# order by InvoiceDate desc",con);
            MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
            DataSet myDataSet = new DataSet();
            myDA.Fill(myDataSet, "invoice");
            myDA.Fill(myDataSet, "Customer");
            DataGridView1.DataSource = myDataSet.Tables["Customer"].DefaultView;
            DataGridView1.DataSource = myDataSet.Tables["invoice"].DefaultView;
            Int64 sum = 0;
            Int64 sum1 = 0;
            Int64 sum2 = 0;

            foreach (DataGridViewRow r in this.DataGridView1.Rows)
            {
                Int64 i = Convert.ToInt64(r.Cells[3].Value);
                Int64 j = Convert.ToInt64(r.Cells[4].Value);
                Int64 k = Convert.ToInt64(r.Cells[5].Value);
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
            cmd = new MySqlCommand("SELECT InvoiceNo as \"Invoice No\",invoice.CusID as \"Customer ID\",CusName as \"Customer Name\",SubTotal as \"SubTotal\",TaxPercentage as \"Tax %\",TaxAmount as \"Tax Amount\",GrandTotal as \"Grand Total\",TotalPayment as \"Total Payment\",PaymentDue as \"Payment Due\" from invoice,customer where invoice.CusID=Customer.CusID and CusName='" + cmbCustomerName.Text + "' order by CusName",con);
            MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
            DataSet myDataSet = new DataSet();
            myDA.Fill(myDataSet, "invoice");
            myDA.Fill(myDataSet, "Customer");
            DataGridView3.DataSource = myDataSet.Tables["Customer"].DefaultView;
            DataGridView3.DataSource = myDataSet.Tables["invoice"].DefaultView;
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
                cmd = new MySqlCommand("SELECT InvoiceNo as \"Invoice No\",,invoice.CusID as \"Customer ID\",CusName as \"Customer Name\",SubTotal as \"SubTotal\",TaxPercentage as \"Tax %\",TaxAmount as \"Tax Amount\",GrandTotal as \"Grand Total\",TotalPayment as \"Total Payment\",PaymentDue as \"Payment Due\" from inventory,Customer where inventory.CusID=Customer.CusID and InvoiceDate between #'" + DateTimePicker2.Text + "'# And #'" + DateTimePicker1.Text + "'# and PaymentDue > 0 ",con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "invoice");
                myDA.Fill(myDataSet, "Customer");
                DataGridView2.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView2.DataSource = myDataSet.Tables["invoice"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView2.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[3].Value);
                    Int64 j = Convert.ToInt64(r.Cells[4].Value);
                    Int64 k = Convert.ToInt64(r.Cells[5].Value);
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

        private void frmSalesRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmInvoice frm = new frmInvoice();
            frm.label6.Text = label9.Text;
            frm.Show();
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = DataGridView1.SelectedRows[0];
                this.Hide();
                frmInvoice frmSales = new frmInvoice();
                frmSales.Show();
                frmSales.txtInvoiceNo.Text = dr.Cells[0].Value.ToString();
              //  frmSales.dtpInvoiceDate.Text = dr.Cells[1].Value.ToString();
                frmSales.txtCustomerID.Text = dr.Cells[1].Value.ToString();
                frmSales.txtCustomerName.Text = dr.Cells[2].Value.ToString();
                frmSales.txtSubTotal.Text = dr.Cells[3].Value.ToString();
                frmSales.txtTaxPer.Text = dr.Cells[4].Value.ToString();
                frmSales.txtTaxAmt.Text = dr.Cells[5].Value.ToString();
                frmSales.txtTotal.Text = dr.Cells[6].Value.ToString();
                frmSales.txtTotalPayment.Text = dr.Cells[7].Value.ToString();
                frmSales.txtPaymentDue.Text = dr.Cells[8].Value.ToString();
                frmSales.btnUpdate.Enabled = true;
                frmSales.Delete.Enabled = true;
                frmSales.btnPrint.Enabled = true;
                frmSales.Save.Enabled = false;
                frmSales.label6.Text = label9.Text;
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

        private void DataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = DataGridView3.SelectedRows[0];
                this.Hide();
                frmInvoice frmSales = new frmInvoice();
                frmSales.Show();
                frmSales.txtInvoiceNo.Text = dr.Cells[0].Value.ToString();
                frmSales.dtpInvoiceDate.Text = dr.Cells[1].Value.ToString();
                frmSales.txtCustomerID.Text = dr.Cells[2].Value.ToString();
                frmSales.txtCustomerName.Text = dr.Cells[3].Value.ToString();
                frmSales.txtSubTotal.Text = dr.Cells[4].Value.ToString();
                frmSales.txtTaxPer.Text = dr.Cells[5].Value.ToString();
                frmSales.txtTaxAmt.Text = dr.Cells[6].Value.ToString();
                frmSales.txtTotal.Text = dr.Cells[7].Value.ToString();
                frmSales.txtTotalPayment.Text = dr.Cells[8].Value.ToString();
                frmSales.txtPaymentDue.Text = dr.Cells[9].Value.ToString();
                frmSales.btnUpdate.Enabled = true;
                frmSales.Delete.Enabled = true;
                frmSales.btnPrint.Enabled = true;
                frmSales.Save.Enabled = false;
                frmSales.label6.Text = label9.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void DataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = DataGridView2.SelectedRows[0];
                this.Hide();
                frmInvoice frmSales = new frmInvoice();
                frmSales.Show();
                frmSales.txtInvoiceNo.Text = dr.Cells[0].Value.ToString();
                frmSales.dtpInvoiceDate.Text = dr.Cells[1].Value.ToString();
                frmSales.txtCustomerID.Text = dr.Cells[2].Value.ToString();
                frmSales.txtCustomerName.Text = dr.Cells[3].Value.ToString();
                frmSales.txtSubTotal.Text = dr.Cells[4].Value.ToString();
                frmSales.txtTaxPer.Text = dr.Cells[5].Value.ToString();
                frmSales.txtTaxAmt.Text = dr.Cells[6].Value.ToString();
                frmSales.txtTotal.Text = dr.Cells[7].Value.ToString();
                frmSales.txtTotalPayment.Text = dr.Cells[8].Value.ToString();
                frmSales.txtPaymentDue.Text = dr.Cells[9].Value.ToString();
                frmSales.btnUpdate.Enabled = true;
                frmSales.Delete.Enabled = true;
                frmSales.btnPrint.Enabled = true;
                frmSales.Save.Enabled = false;
                frmSales.label6.Text = label9.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }

}
