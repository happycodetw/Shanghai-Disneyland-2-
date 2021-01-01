using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shanghai_Disneyland_2_.group
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            var log = new login();
            log.FormClosed += (s, args) => this.Close();
            log.Show();
        }
    }
}
