using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shanghai_Disneyland_2_.methon;
using System.Data.SqlClient;
using Shanghai_Disneyland_2_.group;
using Shanghai_Disneyland_2_.function;

namespace Shanghai_Disneyland_2_
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            
            {
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = clscon.cn)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    $"Select Password From Account Where Email = N'{txtacc.Text.Replace("'", "''")}'";
                object ob = cmd.ExecuteScalar();
                if (ob == null)
                {
                    MessageBox.Show("賬號錯誤");
                    return;
                }
                if (txtpass.Text != ob.ToString())
                {
                    MessageBox.Show("密碼錯誤");
                    return;
                }
                cmd.CommandText =
                    $"Select Name From Account Where Email = N'{txtacc.Text.Replace("'", "''")}'";
                string name = cmd.ExecuteScalar().ToString();
                MessageBox.Show($"歡迎-{name}");
                cmd.CommandText =
                    $"Select GroupID From Account Where Email = N'{txtacc.Text.Replace("'", "''")}'";
                string id = cmd.ExecuteScalar().ToString();
                if (id == "3" || id == "4")
                {
                    this.Hide();
                    var admin = new admin();
                    admin.FormClosed += (s, args) => this.Close();
                    admin.Show();
                }
                if (id == "2" || id == "1")
                {
                    this.Hide();
                    var admin = new user();
                    admin.FormClosed += (s, args) => this.Close();
                    admin.Show();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            other.cls cl = new other.cls();
            cl.num = 0;
            this.Hide();
            var admin = new signup();
            admin.FormClosed += (s, args) => this.Close();
            admin.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            var admin = new guest();
            admin.FormClosed += (s, args) => this.Close();
            admin.Show();
        }
    }
}
