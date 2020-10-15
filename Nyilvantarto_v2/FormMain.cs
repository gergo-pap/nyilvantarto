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
            Global.setPathInDB(labelMentesiHely, groupBoxEleresi, groupBoxButtons, "eleresiUtSzakmaiVizsgaTorzslap");
            Global.createDirectiories(labelMentesiHely);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Global.checkDB_Conn(true))
            {
                this.Hide();
                FormProgressbar f = new FormProgressbar();
                f.setProgressbarMax(90);
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
                Global.createTableSzakmaivizsgaTorzslap();
                f.incrementProgress(10, 5);
                Global.createTableErettsegiTanusitvany();
                f.incrementProgress(10, 5);
                Global.createTableErettsegiTorzslap();
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
            timer1.Interval = 2000;
            if (Global.checkDB_Conn(false))
            {
                labelKapcsolatAdatbazissal.Text = "aktív";
                labelKapcsolatAdatbazissal.BackColor = Color.LightGreen;
                groupBoxButtons.Visible = true;
                Global.dataGridViewBasicSettings(dataGridView1, panelKeres, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve);

            }
            else
            {
                labelKapcsolatAdatbazissal.Text = "offline";
                labelKapcsolatAdatbazissal.BackColor = Color.Red;
                groupBoxButtons.Visible = false;
                Global.dataGridViewOffline(dataGridView1, panelKeres);
            }
        }



        

        private void buttonErettsegiTorzslap_Click(object sender, EventArgs e)
        {
            Global.setAndResetButtonColors(buttonErettsegiTorzslap, buttonErettsegiTanusitvany, buttonSzakmaiVizsgaTorzslap, buttonKozepiskolaAnyakonyv, buttonSzakmaiViszgaAnyakonyv);
            Global.buttonClickClear(dataGridView1, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve, checkBoxVizsgaEve);
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridView1.Columns[4].Name = "Vizsga időszaka";
            dataGridView1.Columns[0].Width = 35;

            Global.dataGridViewBasicSettings(dataGridView1, panelKeres, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve);
        }

        private void buttonErettsegiTanusitvany_Click(object sender, EventArgs e)
        {
            Global.setAndResetButtonColors(buttonErettsegiTanusitvany, buttonKozepiskolaAnyakonyv, buttonErettsegiTorzslap, buttonSzakmaiVizsgaTorzslap, buttonSzakmaiViszgaAnyakonyv);
            Global.buttonClickClear(dataGridView1, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve, checkBoxVizsgaEve);
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridView1.Columns[4].Name = "Tanulói azonosító";
            dataGridView1.Columns[0].Width = 35;
            Global.dataGridViewBasicSettings(dataGridView1, panelKeres, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve);
        }

        private void buttonSzakmaiVizsgaTorzslap_Click(object sender, EventArgs e)
        {
            Global.setAndResetButtonColors(buttonSzakmaiVizsgaTorzslap, buttonErettsegiTanusitvany, buttonErettsegiTorzslap, buttonKozepiskolaAnyakonyv, buttonSzakmaiViszgaAnyakonyv);

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridView1.Columns[4].Name = "Vizsga időszaka";
            dataGridView1.Columns[0].Width = 35;
            Global.dataGridViewBasicSettings(dataGridView1, panelKeres, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve);
            Global.buttonClickClear(dataGridView1, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve, checkBoxVizsgaEve);


        }

        private void buttonSzakmaiViszgaAnyakonyv_Click(object sender, EventArgs e)
        {
            Global.setAndResetButtonColors(buttonSzakmaiViszgaAnyakonyv, buttonErettsegiTanusitvany, buttonErettsegiTorzslap, buttonKozepiskolaAnyakonyv, buttonSzakmaiVizsgaTorzslap);
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Középiskola kezdete";
            dataGridView1.Columns[4].Name = "Érettségi éve";
            dataGridView1.Columns[0].Width = 35;
            Global.dataGridViewBasicSettings(dataGridView1, panelKeres, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve);
            Global.buttonClickClear(dataGridView1, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve, checkBoxVizsgaEve);
        }

        private void buttonKozepiskolaAnyakonyv_Click(object sender, EventArgs e)
        {
            Global.setAndResetButtonColors(buttonKozepiskolaAnyakonyv, buttonErettsegiTanusitvany, buttonErettsegiTorzslap, buttonSzakmaiViszgaAnyakonyv, buttonSzakmaiVizsgaTorzslap);
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Középiskola kezdete";
            dataGridView1.Columns[4].Name = "Érettségi éve";
            dataGridView1.Columns[0].Width = 35;
            Global.dataGridViewBasicSettings(dataGridView1, panelKeres, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve);
            Global.buttonClickClear(dataGridView1, textBoxTanuloNeve, textBoxanyjaNeve, numericUpDownVizsgaÉve, checkBoxVizsgaEve);
        }

        private void textBoxTanuloNeve_TextChanged(object sender, EventArgs e)
        {
            string from = "";
            string row1ErreKeres = "";
            string row2EztIsKiir = "";
            if (buttonErettsegiTanusitvany.BackColor == Color.Black)
            {
                from = "erettsegitanusitvany";
                row1ErreKeres = "vizsgaEvVeg";
                row2EztIsKiir = "tanuloiazonosito";
            }
            else if (buttonErettsegiTorzslap.BackColor == Color.Black)
            {
                from = "erettsegitorzslap";
                row1ErreKeres = "vizsgaEvVeg";
                row2EztIsKiir = "vizsgaTavasz1Osz0";
            }
            else if (buttonSzakmaiVizsgaTorzslap.BackColor == Color.Black)
            {
                from = "szakmaivizsgatorzslap";
                row1ErreKeres = "vizsgaEvVeg";
                row2EztIsKiir = "vizsgaTavasz1Osz0";
            }
            else if (buttonSzakmaiViszgaAnyakonyv.BackColor == Color.Black)
            {
                from = "szakmaivizsgaanyakonyv";
                row1ErreKeres = "vizsgaEvKezdet";
                row2EztIsKiir = "vizsgaEvVeg";
            }
            else if (buttonKozepiskolaAnyakonyv.BackColor == Color.Black)
            {
                from = "kozepiskolaanyakonyv";
                row1ErreKeres = "vizsgaEvKezdet";
                row2EztIsKiir = "vizsgaEvVeg";
            }
            Global.dataGridViewClear(dataGridView1);
            Global.osszetettKeresDataGridview("tanuloNeve", row1ErreKeres, row2EztIsKiir, "anyjaNeve",
                                        from,
                                        textBoxTanuloNeve.Text,
                                        textBoxanyjaNeve.Text,
                                        numericUpDownVizsgaÉve.Value.ToString(),
                                        dataGridView1,
                                        checkBoxVizsgaEve.Checked,
                                        int.Parse(numericUpDownTalalatokSzama.Value.ToString()));
        }

        private void textBoxanyjaNeve_TextChanged(object sender, EventArgs e)
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
