using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Change to MySQL library

namespace cargo
{
    public partial class bill_report : Form
    {
        MySqlConnection con; // Change SqlConnection to MySqlConnection

        public bill_report()
        {
            InitializeComponent();
            con = new MySqlConnection("Server=localhost;Database=cargo_mgmt;Uid=root;Pwd='';"); // Update connection string
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox11.Text = (double.Parse(textBox10.Text) + double.Parse(textBox9.Text)).ToString();
        }

        private void bill_report_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                AutoCompleteStringCollection nc = new AutoCompleteStringCollection();
                MySqlCommand cmd = new MySqlCommand("select * from cust_details", con); // Change SqlCommand to MySqlCommand
                MySqlDataReader rdr = cmd.ExecuteReader(); // Change SqlDataReader to MySqlDataReader
                while (rdr.Read())
                {
                    nc.Add(rdr[1].ToString());
                }
                textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox2.AutoCompleteCustomSource = nc;
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            textBox7.Text = DateTime.Now.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from cust_details where c_name='" + textBox2.Text + "'", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox3.Text = rdr["c_address"].ToString();
                }
                rdr.Close();

                MySqlCommand cmd1 = new MySqlCommand("select * from trans_details where c_name='" + textBox2.Text + "'", con);
                MySqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    textBox1.Text = rdr1["bill_no"].ToString();
                    textBox4.Text = rdr1["type_of_goods"].ToString();
                    textBox5.Text = rdr1["goods_code"].ToString();
                    textBox6.Text = rdr1["date_of_sending"].ToString();
                    textBox8.Text = rdr1["goods_qty"].ToString();
                    textBox9.Text = rdr1["advance"].ToString();
                    textBox10.Text = rdr1["bal"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bill b = new bill();
            b.textBox1.Text = textBox1.Text;
            b.textBox2.Text = textBox2.Text;
            b.textBox3.Text = textBox3.Text;
            b.textBox4.Text = textBox4.Text;
            b.textBox5.Text = textBox5.Text;
            b.textBox6.Text = textBox6.Text;
            b.textBox7.Text = textBox7.Text;
            b.textBox8.Text = textBox8.Text;
            b.textBox9.Text = textBox9.Text;
            b.textBox10.Text = textBox10.Text;
            b.textBox11.Text = textBox11.Text;
            this.Hide();
            b.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
