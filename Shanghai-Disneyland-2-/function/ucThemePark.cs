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
using System.IO;

namespace Shanghai_Disneyland_2_.function
{
    public partial class ucThemePark : UserControl
    {
        public ucThemePark()
        {
            InitializeComponent();
        }

        private void res()
        {
            using (SqlConnection cn = clscon.cn)
            {
                SqlCommand cmd = new SqlCommand();
                cn.Open();
                cmd.Connection = cn;
                SqlDataAdapter da = new SqlDataAdapter("Select ThemeParkID, ThemeParkName,Introduction From ThemePark", cn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = dataGridView1.Columns[2].Visible = false;
            }
        }

        private void ucThemePark_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = clscon.cn)
            {
                SqlCommand cmd = new SqlCommand();
                cn.Open();
                cmd.Connection = cn;
                SqlDataAdapter da = new SqlDataAdapter("Select ThemeParkID, ThemeParkName,Introduction,NewPic From ThemePark", cn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = dataGridView1.Columns[2].Visible = dataGridView1.Columns[3].Visible = false;
            }
        }
        string themeid;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = btnsave.Enabled = true;
            themeid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtth.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtin.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string str = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                picbox.Image = Image.FromFile(str);
            }
            else
                picbox.Image = default;
        }
        string picstr;
        private void button2_Click(object sender, EventArgs e)
        {
            using(SqlConnection cn = clscon.cn)
            {
                SqlCommand cmd = new SqlCommand();
                cn.Open();
                cmd.Connection = cn;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "匯入圖片";
                ofd.DefaultExt = "jpg(*.jpg)|*.jpg";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    picstr = Path.GetFullPath(ofd.FileName);
                    Guid guid = Guid.NewGuid();
                    string file = "./Image/" + guid + ".jpg";
                    File.Copy(picstr, file);
                    picbox.Image = Image.FromFile(file);
                    if (!string.IsNullOrEmpty(themeid))
                    {
                        cmd.CommandText =
                            $"Update ThemePark Set NewPic = '{file}' Where ThemeParkID = '{themeid}'";
                        cmd.ExecuteNonQuery();
                    }
                    else
                        picstr = file;
                    MessageBox.Show("匯入成功");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = clscon.cn)
            {
                SqlCommand cmd = new SqlCommand();
                cn.Open();
                cmd.Connection = cn;
                if(string.IsNullOrEmpty(themeid))
                {
                    cmd.CommandText =
                        $"Update ThemePark Set NewPic = NULL Where ThemeParkID = '{themeid}'";
                    cmd.ExecuteNonQuery();
                    picbox.Image = default;
                    picstr = string.Empty;
                }
                else
                {
                    picbox.Image = default;
                    picstr = string.Empty;
                }
                MessageBox.Show("刪除完成");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnsave.Enabled = btndel.Enabled = true;
            using (SqlConnection cn = clscon.cn)
            {
                SqlCommand cmd = new SqlCommand();
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText =
                    $"Select ThemeParkName From ThemePark Where ThemeParkName = N'{txtth.Text.Replace("'","''")}'";
                object ob = cmd.ExecuteScalar();
                if(ob != null)
                {
                    MessageBox.Show("項目已存在，請選擇保存");
                    return;
                }
                cmd.CommandText =
                    $"Insert Into ThemePark(ThemeParkName,IntroDuction) Values(N'{txtth.Text.Replace("'", "''")}',N'{txtin.Text.Replace("'", "''")}')";
                cmd.ExecuteNonQuery();
                cmd.CommandText =
                    $"Select ThemeParkID From ThemePark Where ThemeParkName = N'{txtth.Text.Replace("'","''")}'";
                themeid = cmd.ExecuteScalar().ToString();
                MessageBox.Show("新增成功");
                res();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = clscon.cn)
            {
                SqlCommand cmd = new SqlCommand();
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText =
                    $"Update ThemePark Set ThemeParkName = N'{txtth.Text.Replace("'","''")}',Introduction = N'{txtin.Text.Replace("'","''")}' Where ThemeParkID = '{themeid}'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("保存成功");
                res();
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = clscon.cn)
            {
                SqlCommand cmd = new SqlCommand();
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText =
                    $"Delete From ThemePark Where ThemeParkID = '{themeid}'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("刪除成功");
                res();
            }
        }
    }
}
