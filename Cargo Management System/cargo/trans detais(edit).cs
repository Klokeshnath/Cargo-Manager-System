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
    public partial class trans_detais_edit_ : Form
    {
        // MySqlConnection for MySQL, SqlConnection for SQL Server
        MySqlConnection con;
        // MySqlCommand for MySQL, SqlCommand for SQL Server
        MySqlCommand cmd;
        // MySqlDataReader for MySQL, SqlDataReader for SQL Server
        MySqlDataReader rdr;
        // For SQL Server, you'd use SqlDataReader

        public trans_detais_edit_()
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
                cmd = new MySqlCommand("update trans_details set c_id=@c_id, c_name=@c_name, type_of_goods=@type_of_goods, goods_code=@goods_code, goods_qty=@goods_qty, truck_no=@truck_no, truck_status=@truck_status, goods_cost=@goods_cost, date_of_sending=@date_of_sending, date_of_delivery=@date_of_delivery, service_charge=@service_charge, advance=@advance, bal=@bal where c_name=@c_name", con);
                // SQL Server command
                // cmd = new SqlCommand("update trans_details set c_id=@c_id, c_name=@c_name, type_of_goods=@type_of_goods, goods_code=@goods_code, goods_qty=@goods_qty, truck_no=@truck_no, truck_status=@truck_status, goods_cost=@goods_cost, date_of_sending=@date_of_sending, date_of_delivery=@date_of_delivery, service_charge=@service_charge, advance=@advance, bal=@bal where c_name=@c_name", con);
                cmd.Parameters.AddWithValue("@c_id", label13.Text);
                cmd.Parameters.AddWithValue("@c_name", textBox2.Text);
                cmd.Parameters.AddWithValue("@type_of_goods", textBox3.Text);
                cmd.Parameters.AddWithValue("@goods_code", textBox4.Text);
                cmd.Parameters.AddWithValue("@goods_qty", textBox5.Text);
                cmd.Parameters.AddWithValue("@truck_no", comboBox1.Text);
                cmd.Parameters.AddWithValue("@truck_status", comboBox2.Text);
                cmd.Parameters.AddWithValue("@goods_cost", textBox6.Text);
                cmd.Parameters.AddWithValue("@date_of_sending", textBox7.Text);
                cmd.Parameters.AddWithValue("@date_of_delivery", textBox8.Text);
                cmd.Parameters.AddWithValue("@service_charge", textBox9.Text);
                cmd.Parameters.AddWithValue("@advance", textBox10.Text);
                cmd.Parameters.AddWithValue("@bal", textBox11.Text);
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                // MySQL command
                cmd = new MySqlCommand("select * from trans_details where c_name=@c_name", con);
                // SQL Server command
                // cmd = new SqlCommand("select * from trans_details where c_name=@c_name", con);
                cmd.Parameters.AddWithValue("@c_name", textBox2.Text);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label3.Text = rdr["c_id"].ToString();
                    textBox1.Text = rdr["bill_no"].ToString();
                    textBox3.Text = rdr["type_of_goods"].ToString();
                    textBox4.Text = rdr["goods_code"].ToString();
                    textBox5.Text = rdr["goods_qty"].ToString();
                    comboBox1.Text = rdr["truck_no"].ToString();
                    comboBox2.Text = rdr["truck_status"].ToString();
                    textBox6.Text = rdr["goods_cost"].ToString();
                    textBox7.Text = rdr["date_of_sending"].ToString();
                    textBox8.Text = rdr["date_of_delivery"].ToString();
                    textBox9.Text = rdr["service_charge"].ToString();
                    textBox10.Text = rdr["advance"].ToString();
                    textBox11.Text = rdr["bal"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                rdr.Close();
                con.Close();
            }
        }

        private void trans_detais_edit__Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                AutoCompleteStringCollection nc = new AutoCompleteStringCollection();
                // MySQL command
                cmd = new MySqlCommand("select * from cust_details", con);
                // SQL Server command
                // cmd = new SqlCommand("select * from cust_details", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    nc.Add(rdr[1].ToString());
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
                rdr.Close();
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
