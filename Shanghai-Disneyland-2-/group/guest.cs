using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shanghai_Disneyland_2_.function;

namespace Shanghai_Disneyland_2_.group
{
    public partial class guest : Form
    {
        public guest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            other.cls cl = new other.cls();
            cl.num = 99;
            this.Hide();
            var admin = new signup();
            admin.FormClosed += (s, args) => this.Close();
            admin.Show();
        }
    }
}
