using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyilvantarto_v2
{
    public partial class FormErettsegi : Form
    {
        public FormErettsegi()
        {
            InitializeComponent();
        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            (new FormMain()).Show(); this.Hide();
        }

        private void FormErettsegi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonTorzslap_Click(object sender, EventArgs e)
        {
            (new FormErettsegiTorzslap()).Show(); this.Hide();
        }

        private void buttonTanusitvany_Click(object sender, EventArgs e)
        {

        }
    }
}
