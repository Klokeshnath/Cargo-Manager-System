using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace cargo
{
    public partial class trans_details : Form
    {
        public trans_details()
        {
            InitializeComponent();
        }

        private void trans_details_Load(object sender, EventArgs e)
        {
            // Load customer names for auto-complete
            using (SqlConnection con = new SqlConnection("data source=CLIENT-07\\SQLEXPRESS;integrated security=true;initial catalog=cargo_mgmt;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT c_name FROM cust_details", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox2.AutoCompleteCustomSource.Add(rdr["c_name"].ToString());
                }
                rdr.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Show customer ID when customer name is typed
            using (SqlConnection con = new SqlConnection("data source=CLIENT-07\\SQLEXPRESS;integrated security=true;initial catalog=cargo_mgmt;"))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT c_id FROM cust_details WHERE c_name=@c_name", con);
                cmd1.Parameters.AddWithValue("@c_name", textBox2.Text);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                if (rdr1.Read())
                {
                    label3.Text = rdr1["c_id"].ToString();
                }
                rdr1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Insert transaction details into the database
            using (SqlConnection con = new SqlConnection("data source=CLIENT-07\\SQLEXPRESS;integrated security=true;initial catalog=cargo_mgmt;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO trans_details(c_id, bill_no, c_name, type_of_goods, goods_code, goods_qty, truck_no, truck_status, goods_cost, date_of_sending, date_of_delivery, service_charge, advance, bal) VALUES (@c_id, @bill_no, @c_name, @type_of_goods, @goods_code, @goods_qty, @truck_no, @truck_status, @goods_cost, @date_of_sending, @date_of_delivery, @service_charge, @advance, @bal)", con);
                cmd.Parameters.AddWithValue("@c_id", label13.Text);
                cmd.Parameters.AddWithValue("@bill_no", textBox1.Text);
                cmd.Parameters.AddWithValue("@c_name", textBox2.Text);
                cmd.Parameters.AddWithValue("@type_of_goods", textBox3.Text);
                cmd.Parameters.AddWithValue("@goods_code", textBox4.Text);
                cmd.Parameters.AddWithValue("@goods_qty", comboBox1.Text); // Assuming comboBox1 contains goods quantity
                cmd.Parameters.AddWithValue("@truck_no", comboBox2.Text);
                cmd.Parameters.AddWithValue("@truck_status", textBox5.Text);
                cmd.Parameters.AddWithValue("@goods_cost", textBox6.Text);
                cmd.Parameters.AddWithValue("@date_of_sending", textBox7.Text);
                cmd.Parameters.AddWithValue("@date_of_delivery", textBox8.Text);
                cmd.Parameters.AddWithValue("@service_charge", textBox9.Text);
                cmd.Parameters.AddWithValue("@advance", textBox10.Text);
                cmd.Parameters.AddWithValue("@bal", textBox11.Text);
                cmd.ExecuteNonQuery();
            }

            // Clear input fields after insertion
            textBox1.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            label13.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox11.Text = (double.Parse(textBox6.Text)).ToString();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            textBox11.Text = (double.Parse(textBox6.Text) + double.Parse(textBox9.Text)).ToString();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            textBox11.Text = (double.Parse(textBox6.Text) + double.Parse(textBox9.Text) - double.Parse(textBox10.Text)).ToString();
        }
    }
}
