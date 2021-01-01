using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Shanghai_Disneyland_2_.methon;

namespace Shanghai_Disneyland_2_.function
{
    public partial class signup : Form
    {
        public signup()
        {
            InitializeComponent();
        }

        private bool mail(string mail)
        {
            bool bin;
            try
            {
                System.Net.Mail.MailAddress maill = new System.Net.Mail.MailAddress(mail);
                bin = true;
            }
            catch
            {
                bin = false;
            }
            return bin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Control ctl in this.Controls)
            {
                for(int i = 1;i<=6;i++)
                {
                    if(ctl.Name == $"txt{i}")
                    {
                        if(string.IsNullOrEmpty(ctl.Text))
                        {
                            MessageBox.Show("項目不能為空");
                            return;
                        }
                    }
                }
            }
            using(SqlConnection cn = clscon.cn)
            {
                txt6.Value = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText =
                    $"Select Email From Account Where Email = N'{txt2.Text.Replace("'", "''")}'";
                object ob = cmd.ExecuteScalar();
                if (ob != null)
                {
                    MessageBox.Show("此賬號已註冊");
                    return;
                }
                if(mail(txt2.Text) == false)
                {
                    MessageBox.Show("Email請符合格式");
                    return;
                }
                cmd.CommandText =
                    $"Select Top 1 AccountID From Account Order By AccountID Desc";
                string id = (Convert.ToInt32(cmd.ExecuteScalar()) + 1).ToString();
                id = id.PadLeft(10, '0');
                cmd.CommandText =
                    $"Insert Into Account(AccountID,Password,Name,Birthday,Gender,Citizenship,Email,GroupID)" +
                    $"Values('{id}',N'{txt3.Text.Replace("'","''")}',N'{txt1.Text.Replace("'", "''")}','{txt6.Value.ToString("yyyy-MM-dd")}','{txt4.Text}','{txt5.Text}',N'{txt2.Text.Replace("'", "''")}','1')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("註冊成功，將返回登錄界面");
                this.Hide();
                var lg = new login();
                lg.FormClosed += (s, args) => this.Close();
                lg.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            other.cls cl = new other.cls();
            if(cl.num == 0)
            {
                this.Hide();
                var lg = new login();
                lg.FormClosed += (s, args) => this.Close();
                lg.Show();
            }
            if(cl.num == 99)
            {
                this.Hide();
                var lg = new group.guest();
                lg.FormClosed += (s, args) => this.Close();
                lg.Show();
            }
        }
    }
}
