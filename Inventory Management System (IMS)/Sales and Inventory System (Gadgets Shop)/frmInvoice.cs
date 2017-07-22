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
using System.Security.Cryptography;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
namespace Inventory_Management_System
{
    public partial class frmInvoice : Form
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


        public frmInvoice()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtCustomerID.Text == "")
                {
                    MessageBox.Show("Please retrieve Customer ID","Input Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtCustomerID.Focus();
                    return;
                }

                if(txtTaxPer.Text == "")
                {
                    MessageBox.Show("Please enter tax percentage","Input Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtTaxPer.Focus();
                    return;
                }

                if(txtDiscount.Text == "")
                {
                    MessageBox.Show("Please enter discounted amount","Input Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtTaxPer.Focus();
                    return;
                }

                if(txtTotalPayment.Text == "")
                {
                    MessageBox.Show("Please enter total payment","Input Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtTotalPayment.Focus();
                    return;
                }
                if(ListView1.Items.Count == 0)
                {
                    MessageBox.Show("sorry no product added","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                {
                    con = new MySqlConnection(connString());
                    con.Open();

                    string cb = "insert Into invoice(InvoiceDate,CusID,SubTotal,TaxPercentage,TaxAmount,Discount,GrandTotal,TotalPayment,PaymentDue) VALUES (@InvoiceDate,@CusID,@SubTotal,@TaxPercentage,@TaxAmount,@Discount,@GrandTotal,@TotalPayment,@PaymentDue)";
                    cmd = new MySqlCommand(cb);
                    cmd.Connection = con;

                    cmd.Parameters.Add(new MySqlParameter("@InvoiceDate",MySql.Data.MySqlClient.MySqlDbType.Date,25,"InvoiceDate"));
                    cmd.Parameters.Add(new MySqlParameter("@CusID",MySql.Data.MySqlClient.MySqlDbType.Int32,11,"CusID"));
                    cmd.Parameters.Add(new MySqlParameter("@SubTotal",MySql.Data.MySqlClient.MySqlDbType.Int32,11,"SubTotal"));
                    cmd.Parameters.Add(new MySqlParameter("@TaxPercentage",MySql.Data.MySqlClient.MySqlDbType.Float,11,"TaxPercentage"));
                    cmd.Parameters.Add(new MySqlParameter("@TaxAmount",MySql.Data.MySqlClient.MySqlDbType.Double,16,"TaxAmount"));
                    cmd.Parameters.Add(new MySqlParameter("@Discount",MySql.Data.MySqlClient.MySqlDbType.Double,16,"Discount"));
                    cmd.Parameters.Add(new MySqlParameter("@GrandTotal",MySql.Data.MySqlClient.MySqlDbType.Double,16,"GrandTotal"));
                    cmd.Parameters.Add(new MySqlParameter("@TotalPayment",MySql.Data.MySqlClient.MySqlDbType.Int32,11,"TotalPayment"));
                    cmd.Parameters.Add(new MySqlParameter("@PaymentDue",MySql.Data.MySqlClient.MySqlDbType.Int32,11,"PaymentDue"));

                    cmd.Parameters["@InvoiceDate"].Value = dtpInvoiceDate.Text;
                    cmd.Parameters["@CusID"].Value = txtCustomerID.Text;
                    cmd.Parameters["@SubTotal"].Value = txtSubTotal.Text;
                    cmd.Parameters["@TaxPercentage"].Value = txtTaxPer.Text;
                    cmd.Parameters["@TaxAmount"].Value = txtTaxAmt.Text;
                    cmd.Parameters["@Discount"].Value = txtDiscount.Text;
                    cmd.Parameters["@GrandTotal"].Value = txtTotal.Text;
                    cmd.Parameters["@TotalPayment"].Value = txtTotalPayment.Text;
                    cmd.Parameters["@PaymentDue"].Value = txtPaymentDue.Text;

                    cmd.ExecuteReader();
                    if(con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Close();

                    {
                        con = new MySqlConnection(connString());
                        con.Open();
                        string inv = "SELECT max(InvoiceNo) FROM invoice";
                        cmd = new MySqlCommand(inv);
                        cmd.Connection = con;
                        txtInvoiceNo.Text = Convert.ToString(cmd.ExecuteScalar());
                        con.Close();
                    }

                    for(int i = 0; i <= ListView1.Items.Count - 1; i++)
                    {
                        con = new MySqlConnection(connString());

                        string cd = "insert Into sales(InvoiceNo,ConfigID,Quantity,Price,TotalAmount) VALUES (@InvoiceNo,@ConfigID,@Quantity,@Price,@Totalamount)";
                        cmd = new MySqlCommand(cd);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("InvoiceNo",txtInvoiceNo.Text);
                        cmd.Parameters.AddWithValue("ConfigID",ListView1.Items[i].SubItems[1].Text);
                        cmd.Parameters.AddWithValue("Quantity",ListView1.Items[i].SubItems[4].Text);
                        cmd.Parameters.AddWithValue("Price",ListView1.Items[i].SubItems[3].Text);
                        cmd.Parameters.AddWithValue("TotalAmount",ListView1.Items[i].SubItems[5].Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    for(int i = 0; i <= ListView1.Items.Count - 1; i++)
                    {
                        con = new MySqlConnection(connString());
                        con.Open();
                        string cb1 = "update inventory set Quantity = Quantity - " + ListView1.Items[i].SubItems[4].Text + " where ConfigID= " + ListView1.Items[i].SubItems[1].Text + "";
                        cmd = new MySqlCommand(cb1);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    for(int i = 0; i <= ListView1.Items.Count - 1; i++)
                    {
                        con = new MySqlConnection(connString());
                        con.Open();

                        string cb2 = "update inventory set TotalPrice = Totalprice - '" + ListView1.Items[i].SubItems[5].Text + "' where ConfigID= " + ListView1.Items[i].SubItems[1].Text + "";
                        cmd = new MySqlCommand(cb2);
                        cmd.Connection = con;
                        cmd.ExecuteReader();
                        con.Close();
                    }

                    Save.Enabled = false;
                    btnPrint.Enabled = true;
                    GetData();
                    MessageBox.Show("Successfully saved","Record",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomersRecord1 frm = new frmCustomersRecord1();
            frm.label1.Text = label6.Text;
            frm.Visible=true;
        }

      
        private void txtSaleQty_TextChanged(object sender, EventArgs e)
        {
            double val1 = 0;
            int val2 = 0;
            double.TryParse(txtPrice.Text, out val1);
            int.TryParse(txtSaleQty.Text, out val2);
            double I = (val1 * val2);
            txtTotalAmount.Text = I.ToString();
        }

        public double subtot()
        {

            int i = 0;
            int j = 0;
            int k = 0;
            i = 0;
            j = 0;
            k = 0;

            try
            {
                j = ListView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    k = k + Convert.ToInt32(ListView1.Items[i].SubItems[5].Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return k;

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductName.Text=="")
                {
                    MessageBox.Show("Please retrieve product name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtSaleQty.Text=="")
                {
                    MessageBox.Show("Please enter no. of sale quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSaleQty.Focus();
                    return;
                }
                int SaleQty = Convert.ToInt32(txtSaleQty.Text);
                if (SaleQty == 0)
                {
                    MessageBox.Show("no. of sale quantity can not be zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSaleQty.Focus();
                    return;
                }
              
                if (ListView1.Items.Count==0)
                {
                   
                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(txtConfigID.Text);
                    lst.SubItems.Add(txtProductName.Text);
                    lst.SubItems.Add(txtPrice.Text);
                    lst.SubItems.Add(txtSaleQty.Text);
                    lst.SubItems.Add(txtTotalAmount.Text);
                    ListView1.Items.Add(lst);
                    txtSubTotal.Text = subtot().ToString();
                    txtProductName.Text = "";
                    txtConfigID.Text = "";
                    txtPrice.Text = "";
                    txtAvailableQty.Text = "";
                    txtSaleQty.Text = "";
                    txtTotalAmount.Text = "";
                    textBox1.Text = "";
                    return;
                }

                for (int j = 0; j <= ListView1.Items.Count - 1; j++)
                {
                    if (ListView1.Items[j].SubItems[1].Text == txtConfigID.Text)
                    {
                        ListView1.Items[j].SubItems[1].Text = txtConfigID.Text;
                        ListView1.Items[j].SubItems[2].Text = txtProductName.Text;
                        ListView1.Items[j].SubItems[3].Text = txtPrice.Text;
                        ListView1.Items[j].SubItems[4].Text = (Convert.ToInt32(ListView1.Items[j].SubItems[4].Text) + Convert.ToInt32(txtSaleQty.Text)).ToString();
                        ListView1.Items[j].SubItems[5].Text = (Convert.ToInt32(ListView1.Items[j].SubItems[5].Text) + Convert.ToInt32(txtTotalAmount.Text)).ToString();
                        txtSubTotal.Text = subtot().ToString();
                        txtProductName.Text = "";
                        txtConfigID.Text = "";
                        txtPrice.Text = "";
                        txtAvailableQty.Text = "";
                        txtSaleQty.Text = "";
                        txtTotalAmount.Text = "";
                        return;

                    }
                }
                   
                    ListViewItem lst1 = new ListViewItem();

                    lst1.SubItems.Add(txtConfigID.Text);
                    lst1.SubItems.Add(txtProductName.Text);
                    lst1.SubItems.Add(txtPrice.Text);
                    lst1.SubItems.Add(txtSaleQty.Text);
                    lst1.SubItems.Add(txtTotalAmount.Text);
                    ListView1.Items.Add(lst1);
                    txtSubTotal.Text = subtot().ToString();
                    txtProductName.Text = "";
                    txtConfigID.Text = "";
                    txtPrice.Text = "";
                    txtAvailableQty.Text = "";
                    txtSaleQty.Text = "";
                    txtTotalAmount.Text = "";
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("No items to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int itmCnt = 0;
                    int i = 0;
                    int t = 0;

                    ListView1.FocusedItem.Remove();
                    itmCnt = ListView1.Items.Count;
                    t = 1;

                    for (i = 1; i <= itmCnt + 1; i++)
                    {
                        t = t + 1;
                    }
                    txtSubTotal.Text = subtot().ToString();
                }

                btnRemove.Enabled = false;
                if (ListView1.Items.Count == 0)
                {
                    txtSubTotal.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTaxPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaxPer.Text))
                {
                    txtTaxAmt.Text = "";
                    txtTotal.Text = "";
                    return;
                }
                txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString() ;
                temp.Text=txtTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDiscount_TextChanged(object sender,EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtDiscount.Text))
                {
                    txtDiscount.Text = "";
                    return;
                }
                txtTotal.Text = (Convert.ToInt32(temp.Text) - Convert.ToInt32(txtDiscount.Text)).ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                String sql = "SELECT ProductName as \"Product Name\",Thickness,Colour,Size,Price,sum(Quantity) as \"Total Quantity\" from inventory,configuration where inventory.ConfigID=configuration.ConfigID and ProductName like '" + textBox1.Text + "%' group by InventoryID,ProductName,Price,Thickness,Color,Size,configuration.ConfigID having sum(quantity > 0) order by ProductName";
                cmd = new MySqlCommand(sql, con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet,"Inventory");
                myDA.Fill(myDataSet,"Configuration");
                dataGridView1.DataSource = myDataSet.Tables["inventory"].DefaultView;
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
                txtConfigID.Text = dr.Cells[1].Value.ToString();
                txtProductName.Text = dr.Cells[2].Value.ToString();
                txtPrice.Text = dr.Cells[6].Value.ToString();
                txtAvailableQty.Text = dr.Cells[7].Value.ToString();
                txtSaleQty.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetData()
        {
            try
            {
                con = new MySqlConnection(connString());
                con.Open();
                String sql = "SELECT InventoryID,configuration.ConfigID, ProductName as \"Product Name\",Thickness,Colour,Size,Price,sum(Quantity) as \"Total Quantity\" from Inventory,configuration where Inventory.ConfigID=configuration.ConfigID group by InventoryID,ProductName,Price,Thickness,Colour,Size,configuration.ConfigID having sum(quantity > 0) order by ProductName";
                cmd = new MySqlCommand(sql,con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet,"Inventory");
                myDA.Fill(myDataSet,"Configuration");
                dataGridView1.DataSource = myDataSet.Tables["inventory"].DefaultView;
                dataGridView1.DataSource = myDataSet.Tables["configuration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reset()
        {
            txtInvoiceNo.Text = "";
            dtpInvoiceDate.Text = DateTime.Today.ToString();
            txtCustomerID.Text = "";
            txtCustomerName.Text = "";
            txtProductName.Text = "";
            txtConfigID.Text = "";
            txtPrice.Text = "";
            txtAvailableQty.Text = "";
            txtSaleQty.Text = "";
            txtTotalAmount.Text = "";
            ListView1.Items.Clear();
            txtSubTotal.Text = "";
            txtTaxPer.Text = "";
            txtTaxAmt.Text = "";
            txtDiscount.Text = "";
            txtTotal.Text = "";
            txtTotalPayment.Text = "";
            txtPaymentDue.Text = "";
            textBox1.Text = "";
            Save.Enabled = true;
            Delete.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            btnPrint.Enabled = false;


        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Delete_Click(object sender, EventArgs e)
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
                string cq1 = "delete from sales where InvoiceNo='" + txtInvoiceNo.Text + "'";
                cmd = new MySqlCommand(cq1);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                con = new MySqlConnection(connString());
                con.Open();
                string cq = "delete from Invoice where InvoiceNo='" + txtInvoiceNo.Text + "'";
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

        private void frmInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.lblUser.Text = label6.Text;
            frm.Show();
        }

        private void txtTotalPayment_TextChanged(object sender, EventArgs e)
        {
            double val1 = 0;
            double val2 = 0;
            double.TryParse(txtTotal.Text, out val1);
            double.TryParse(txtTotalPayment.Text, out val2);
            double I = (val1 - val2);
            txtPaymentDue.Text = I.ToString();
        }

        private void txtTotalPayment_Validating(object sender, CancelEventArgs e)
        {
            double val1 = 0;
            double val2 = 0;
            double.TryParse(txtTotal.Text,out val1);
            double.TryParse(txtTotalPayment.Text,out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Total Payment can't be more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPayment.Text = "";
                txtPaymentDue.Text = "";
                txtTotalPayment.Focus();
                return;
            }
        }

        private void txtSaleQty_Validating(object sender, CancelEventArgs e)
        {

            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtAvailableQty.Text, out val1);
            int.TryParse(txtSaleQty.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Selling quantities are more than available quantities", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSaleQty.Text = "";
                txtTotalAmount.Text = "";
                txtSaleQty.Focus();
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                
                rptInvoice rpt = new rptInvoice();
                //The report you created.
                cmd = new MySqlCommand();
                MySqlDataAdapter myDA = new MySqlDataAdapter();
                DataSet myDS = new DataSet();
                //The DataSet you created.
                con = new MySqlConnection(connString());
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM invoicereport where InvoiceNo= '" + txtInvoiceNo.Text + "'";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS,"invoicereport");
                //myDA.Fill(myDS, "configuration");
                //myDA.Fill(myDS,"invoice");
                //myDA.Fill(myDS, "sales");
                //myDA.Fill(myDS, "Customer");
                rpt.SetDataSource(myDS);
                frmInvoiceReport frm = new frmInvoiceReport();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible=true;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
            con = new MySqlConnection(connString());
            con.Open();
            String cb = "update invoice set GrandTotal= " + txtTotal.Text + ",TotalPayment= " + txtTotalPayment.Text + ",PaymentDue= " + txtPaymentDue.Text + " where InvoiceNo= '" + txtInvoiceNo.Text + "'";
            cmd = new MySqlCommand(cb);
            cmd.Connection = con;
            cmd.ExecuteReader();
            con.Close();
            MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnUpdate.Enabled = false;
            }
        catch (Exception ex)
            {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSalesRecord1 frm = new frmSalesRecord1();
            frm.label9.Text = label6.Text;
            frm.Show();
        }


    }
}