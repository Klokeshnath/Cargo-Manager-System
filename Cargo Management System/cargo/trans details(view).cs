using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace cargo
{
    public partial class trans_details_view_ : Form
    {
        public trans_details_view_()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Server=localhost;Database=cargo_mgmt;Uid=root;Pwd=;");
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("select * from trans_details where c_name=@c_name", con);
            cmd1.Parameters.AddWithValue("@c_name", textBox2.Text);
            MySqlDataReader rdr1 = cmd1.ExecuteReader();
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
            rdr1.Close();
            con.Close();
        }

        private void trans_details_view__Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Server=localhost;Database=cargo_mgmt;Uid=root;Pwd=your_password;");
            con.Open();
            AutoCompleteStringCollection nc = new AutoCompleteStringCollection();
            MySqlCommand cmd = new MySqlCommand("select * from cust_details", con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                nc.Add(rdr[1].ToString());
            }
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = nc;
            rdr.Close();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
