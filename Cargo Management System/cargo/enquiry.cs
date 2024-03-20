using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // MySQL library
// For SQL Server, you'd use System.Data.SqlClient instead

namespace cargo
{
    public partial class enquiry : Form
    {
        // MySqlConnection for MySQL, SqlConnection for SQL Server
        MySqlConnection con;
        // MySqlCommand for MySQL, SqlCommand for SQL Server
        MySqlCommand cmd1;
        // MySqlDataReader for MySQL, SqlDataReader for SQL Server
        MySqlDataReader rdr1;
        // For SQL Server, you'd use SqlDataReader

        public enquiry()
        {
            InitializeComponent();
            // MySQL connection string
            con = new MySqlConnection("Server=localhost;Database=cargo_mgmt;Uid=root;Pwd=;");
            // SQL Server connection string
            // con = new SqlConnection("data source=CLIENT-07\\SQLEXPRESS;integrated security=true;initial catalog=cargo_mgmt;");
        }

        private void enquiry_Load(object sender, EventArgs e)
        {
            try
            {
                // Open connection
                con.Open();
                AutoCompleteStringCollection nc = new AutoCompleteStringCollection();
                // MySQL command
                MySqlCommand cmd = new MySqlCommand("select * from cust_details", con);
                // SQL Server command
                // SqlCommand cmd = new SqlCommand("select * from cust_details", con);
                rdr1 = cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    nc.Add(rdr1[1].ToString());
                }
                textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox2.AutoCompleteCustomSource = nc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (rdr1 != null)
                {
                    rdr1.Close();
                    rdr1.Dispose();
                }
                // Close connection
                con.Close();
            }
            label17.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Open connection
                con.Open();
                // MySQL command
                cmd1 = new MySqlCommand("select * from trans_details where c_name=@c_name", con);
                // SQL Server command
                // cmd1 = new SqlCommand("select * from trans_details where c_name=@c_name", con);
                cmd1.Parameters.AddWithValue("@c_name", textBox2.Text);
                rdr1 = cmd1.ExecuteReader();
                if (rdr1.Read())
                {
                    label3.Text = rdr1["c_id"].ToString();
                    textBox1.Text = rdr1["bill_no"].ToString();
                    textBox3.Text = rdr1["type_of_goods"].ToString();
                    textBox4.Text = rdr1["goods_code"].ToString();
                    textBox5.Text = rdr1["goods_qty"].ToString();
                    textBox12.Text = rdr1["truck_no"].ToString();
                    textBox13.Text = rdr1["truck_status"].ToString();
                    textBox6.Text = rdr1["goods_cost"].ToString();
                    textBox7.Text = rdr1["date_of_sending"].ToString();
                    textBox8.Text = rdr1["date_of_delivery"].ToString();
                    textBox9.Text = rdr1["service_charge"].ToString();
                    textBox10.Text = rdr1["advance"].ToString();
                    textBox11.Text = rdr1["bal"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (rdr1 != null)
                {
                    rdr1.Close();
                    rdr1.Dispose();
                }
                // Close connection
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
