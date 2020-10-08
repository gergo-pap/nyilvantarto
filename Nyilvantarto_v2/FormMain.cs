using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Nyilvantarto_v2
{
    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();
            if (Global.checkDB_Conn())
            {
                this.Hide();
                FormProgressbar f = new FormProgressbar();
                f.setProgressbarMax(70);
                f.Show();
                f.incrementProgress(10, 20);
                MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;charset = utf8");
                f.incrementProgress(10, 20);
                Global.createTablesettings();
                f.incrementProgress(10, 20);
                Global.createTableKozepiskolaAnyakonyv();
                f.incrementProgress(10, 20);
                Global.createTableszakmaiviszgaanyakonyv();
                f.incrementProgress(10, 20);
                Global.createTableszakmaivizsgaTorzslap();
                f.incrementProgress(10, 20);
                Global.checkDirs(groupBoxEleresi, groupBoxButtons, labelMentesiHely);
                f.incrementProgress(10, 20);
                labelPath.Text = Global.fullPath;
                f.Hide();
            }
        }

        private void buttonErettsegi_Click(object sender, EventArgs e)
        {
            (new FormErettsegi()).Show(); this.Hide();
        }

        private void buttonSzkmaiVizsga_Click(object sender, EventArgs e)
        {
            (new FormSzakmaiVizsga()).Show(); this.Hide();
        }

        private void buttonKozepiskola_Click(object sender, EventArgs e)
        {
            (new FormKozepiskolaAnyakonyv()).Show(); this.Hide();
        }

        private void buttonTallozas_Click(object sender, EventArgs e)
        {
            Global.tallozas(labelMentesiHely);
            Global.setPathInDB(labelMentesiHely, groupBoxEleresi, groupBoxButtons);
            Global.createDirectiories(labelMentesiHely);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
