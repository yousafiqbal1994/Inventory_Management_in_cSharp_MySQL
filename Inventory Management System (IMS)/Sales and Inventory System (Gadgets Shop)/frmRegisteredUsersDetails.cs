using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Inventory_Management_System
{
    public partial class frmRegisteredUsersDetails : Form
    {
        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;

        public string connString()
        {
            server = "localhost";
            database = "test";
            uid = "root";
            password = "seecs@123";
            return connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        public frmRegisteredUsersDetails()
        {
            InitializeComponent();
        }
      
        private MySqlConnection Connection
        {
            get
            {
                MySqlConnection ConnectionToFetch = new MySqlConnection(connString());
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        public DataView GetData()
        {
            dynamic SelectQry = "SELECT RTRIM(Username) as \"User Name\",RTRIM(Password) as \"Password\",RTRIM(Name) as \"Name\",RTRIM(Email) as \"Email\",RTRIM(join_date) as \"Date Of Joining\" FROM registration";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
                MySqlCommand SampleCommand = new MySqlCommand();
                dynamic SampleDataAdapter = new MySqlDataAdapter();
                SampleCommand.CommandText = SelectQry;
                SampleCommand.Connection = Connection;
                SampleDataAdapter.SelectCommand = SampleCommand;
                SampleDataAdapter.Fill(SampleSource);
                TableView = SampleSource.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return TableView;
        }
        private void frmRegisteredUsersDetails_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetData();
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
