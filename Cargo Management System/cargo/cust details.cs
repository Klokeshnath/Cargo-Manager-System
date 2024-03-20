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
    public partial class cust_details : Form
    {
        // MySqlConnection for MySQL, SqlConnection for SQL Server
        MySqlConnection con;
        // MySqlCommand for MySQL, SqlCommand for SQL Server
        MySqlCommand cmd;
        // For SQL Server, you'd use SqlCommand

        public cust_details()
        {
            InitializeComponent();
            // MySQL connection string
            con = new MySqlConnection("Server=localhost;Database=cargo_mgmt;Uid=root;Pwd=your_password;");
            // SQL Server connection string
            // con = new SqlConnection("data source=CLIENT-07\\SQLEXPRESS;integrated security=true;initial catalog=cargo_mgmt;");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Open connection
                con.Open();
                // MySQL command
                cmd = new MySqlCommand("insert into cust_details(c_name,c_id,c_address,c_city,c_pincode,c_ph_no,r_name,r_id,r_address,r_city,r_pincode,r_ph_no) values(@c_name, @c_id, @c_address, @c_city, @c_pincode, @c_ph_no, @r_name, @r_id, @r_address, @r_city, @r_pincode, @r_ph_no)", con);
                // SQL Server command
                // cmd = new SqlCommand("insert into cust_details(c_name,c_id,c_address,c_city,c_pincode,c_ph_no,r_name,r_id,r_address,r_city,r_pincode,r_ph_no) values(@c_name, @c_id, @c_address, @c_city, @c_pincode, @c_ph_no, @r_name, @r_id, @r_address, @r_city, @r_pincode, @r_ph_no)", con);
                cmd.Parameters.AddWithValue("@c_name", textBox2.Text);
                cmd.Parameters.AddWithValue("@c_id", textBox12.Text);
                cmd.Parameters.AddWithValue("@c_address", textBox3.Text);
                cmd.Parameters.AddWithValue("@c_city", textBox4.Text);
                cmd.Parameters.AddWithValue("@c_pincode", textBox5.Text);
                cmd.Parameters.AddWithValue("@c_ph_no", textBox6.Text);
                cmd.Parameters.AddWithValue("@r_name", textBox11.Text);
                cmd.Parameters.AddWithValue("@r_id", textBox13.Text);
                cmd.Parameters.AddWithValue("@r_address", textBox10.Text);
                cmd.Parameters.AddWithValue("@r_city", textBox9.Text);
                cmd.Parameters.AddWithValue("@r_pincode", textBox8.Text);
                cmd.Parameters.AddWithValue("@r_ph_no", textBox7.Text);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                con.Close();
            }
        }

        private void cust_details_Load(object sender, EventArgs e)
        {
            try
            {
                // Open connection
                con.Open();
                // MySQL command
                MySqlCommand com = new MySqlCommand("select count(*) from cust_details", con);
                // SQL Server command
                // SqlCommand com = new SqlCommand("select count(*) from cust_details", con);
                int count = Convert.ToInt16(com.ExecuteScalar()) + 1;
                textBox1.Text = ("0" + count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
