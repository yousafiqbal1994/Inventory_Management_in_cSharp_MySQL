﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Inventory_Management_System
{
    public partial class frmLoginDetails : Form
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
        public frmLoginDetails()
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
        public void GetData()
        {
            try
            {

                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Select("SELECT Username as \"User Name\",Password FROM registration");
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
        private void frmLoginDetails_Load(object sender, EventArgs e)
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
      

     
    }
}
