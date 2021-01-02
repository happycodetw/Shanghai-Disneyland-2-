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

        private void admin_Load(object sender, EventArgs e)
        {
            ucThemePark uc = new ucThemePark();
            pl.Controls.Add(uc);
            uc.BringToFront();
        }
    }
}
