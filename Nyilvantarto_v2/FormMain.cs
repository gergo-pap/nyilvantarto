using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Nyilvantarto_v2
{
    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();
            //MessageBox.Show(Application.OpenForms.OfType<FormMain>().Count().ToString());
            //if (Application.OpenForms.OfType<FormMain>().Count() == 1)
            //    Application.OpenForms.OfType<FormMain>().First().Close();
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

        private void dataGridViewBasicSettings()
        {
            Global.dataGridViewKeresesEredmenyeiClear(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Visible = true;
            panelKeres.Visible = true;
        }

        private void buttonErettsegiTorzslap_Click(object sender, EventArgs e)
        {
            Global.dataGridViewKeresesEredmenyeiClear(dataGridView1);

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridView1.Columns[4].Name = "Vizsga időszaka";
            dataGridViewBasicSettings();
        }

        private void buttonErettsegiTanusitvany_Click(object sender, EventArgs e)
        {
            
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridView1.Columns[4].Name = "Tanulói azonosító";
            dataGridViewBasicSettings();

        }

        private void buttonSzakmaiVizsgaTorzslap_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridView1.Columns[4].Name = "Vizsga időszaka";
            dataGridViewBasicSettings();

        }

        private void buttonSzakmaiViszgaAnyakonyv_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Középiskola kezdete";
            dataGridView1.Columns[4].Name = "Érettségi éve";
            dataGridViewBasicSettings();

        }

        private void buttonKozepiskolaAnyakonyv_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridViewBasicSettings();
        }

        private void textBoxTanuloNeve_TextChanged(object sender, EventArgs e)
        {
            Global.dataGridViewKeresesEredmenyeiClear(dataGridView1);

            Global.osszetettKeresDataGridview("szvt_tanuloNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve",
                                        "szakmaivizsgaTorzslap",
                                        textBoxTanuloNeve.Text,
                                        textBoxAnyjaNeve.Text,
                                        numericUpDownViszgaÉve.Value.ToString(),
                                        dataGridView1,
                                        checkBoxViszgaEve.Checked,
                                        int.Parse(numericUpDownTalalatokSzama.Value.ToString()));
        }

        private void textBoxAnyjaNeve_TextChanged(object sender, EventArgs e)
        {
            textBoxTanuloNeve_TextChanged(sender, e);
        }

        private void numericUpDownViszgaÉve_ValueChanged(object sender, EventArgs e)
        {
            textBoxTanuloNeve_TextChanged(sender, e);
        }

        private void checkBoxViszgaEve_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTanuloNeve_TextChanged(sender, e);
        }

        private void numericUpDownTalalatokSzama_ValueChanged(object sender, EventArgs e)
        {
            textBoxTanuloNeve_TextChanged(sender, e);
        }
    }
}
