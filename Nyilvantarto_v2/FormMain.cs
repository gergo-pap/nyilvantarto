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
using System.Threading;

namespace Nyilvantarto_v2
{
    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();
            //MessageBox.Show(Application.OpenForms.OfType<FormMain>().Count().ToString());
            if (Application.OpenForms.OfType<FormMain>().Count() == 1)
                Application.OpenForms.OfType<FormMain>().First().Close();
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
            Global.setPathInDB(labelMentesiHely, groupBoxEleresi, groupBoxButtons, "eleresiUtSzakmaiVizsgaTörzslap");
            Global.createDirectiories(labelMentesiHely);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Global.checkDB_Conn(true))
            {
                this.Hide();
                FormProgressbar f = new FormProgressbar();
                f.setProgressbarMax(70);
                f.Show();
                f.incrementProgress(10, 5);
                MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;charset = utf8");
                f.incrementProgress(10, 5);
                Global.createTablesettings();
                f.incrementProgress(10, 5);
                Global.createTableKozepiskolaAnyakonyv();
                f.incrementProgress(10, 5);
                Global.createTableszakmaiviszgaanyakonyv();
                f.incrementProgress(10, 5);
                Global.createTableszakmaivizsgaTorzslap();
                f.incrementProgress(10, 5);
                Global.checkDirs(groupBoxEleresi, groupBoxButtons, labelMentesiHely);
                f.incrementProgress(10, 5);
                labelPath.Text = Global.fullPath;
                f.Hide();
                this.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 2500;
            if (Global.checkDB_Conn(false))
            {
                //if (labelKapcsolatAdatbazissal.Text == "offline")
                //{
                //    FormMain_Load(sender, e);
                //}
                labelKapcsolatAdatbazissal.Text = "aktív";
                labelKapcsolatAdatbazissal.BackColor = Color.LightGreen;
                groupBoxButtons.Visible = true;
            }
            else
            {
                labelKapcsolatAdatbazissal.Text = "offline";
                labelKapcsolatAdatbazissal.BackColor = Color.Red;
                groupBoxButtons.Visible = false;
            }
        }
    }
}
