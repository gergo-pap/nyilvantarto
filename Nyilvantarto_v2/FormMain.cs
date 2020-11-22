using System;
using System.Drawing;
using System.IO;
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
            Global.setPathInDB(labelMentesiHely, groupBoxEleresi, groupBoxButtons, "eleresiUt");
            Global.createDirectiories(labelMentesiHely);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            panelMenu.Width = 0;
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
                Global.checkDirs(groupBoxEleresi, groupBoxButtons, labelMentesiHely, panelKeres, "eleresiUt");
                f.incrementProgress(10, 5);
                labelPath.Text = Global.fixPath;
                f.Hide();
                this.Show();
                Global.setFeltoltPanelPosition(panelErettsegiTanusitvanyFeltolt, panelErettsegiTorzslapFeltolt, panelKozepiskolaAnyakonyvFeltolt,
                    panelSzakmaiViszgaTorzslapFeltolt, panelSzakmaiVizsgaAnyakonyvFeltolt);
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
                Global.dataGridViewBasicSettings(dataGridView1, panelKeres, textBoxTanuloNeveKeres, textBoxanyjaNeveKeres, numericUpDownVizsgaÉveKeres);
            }
            else
            {
                labelKapcsolatAdatbazissal.Text = "offline";
                labelKapcsolatAdatbazissal.BackColor = Color.Red;
                groupBoxButtons.Visible = false;
                Global.dataGridViewOffline(dataGridView1, panelKeres, panelFeltModTorl);
            }
        }



        

        private void buttonErettsegiTorzslap_Click(object sender, EventArgs e)
        {
            Global.setAndResetButtonColors(buttonErettsegiTorzslap, buttonErettsegiTanusitvany, buttonSzakmaiVizsgaTorzslap, buttonKozepiskolaAnyakonyv, buttonSzakmaiViszgaAnyakonyv);
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridView1.Columns[4].Name = "Vizsga időszaka";
            dataGridView1.Columns[0].Width = 35;

            buttonKeresClick();
            textBoxTanuloNeve_TextChanged(sender, e);

        }

        private void buttonErettsegiTanusitvany_Click(object sender, EventArgs e)
        {
            Global.setAndResetButtonColors(buttonErettsegiTanusitvany, buttonKozepiskolaAnyakonyv, buttonErettsegiTorzslap, buttonSzakmaiVizsgaTorzslap, buttonSzakmaiViszgaAnyakonyv);
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Tanuló neve";
            dataGridView1.Columns[2].Name = "Anyja neve";
            dataGridView1.Columns[3].Name = "Vizsga éve";
            dataGridView1.Columns[4].Name = "Tanulói azonosító";
            dataGridView1.Columns[0].Width = 35;
            buttonKeresClick();
            textBoxTanuloNeve_TextChanged(sender, e);

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
            buttonKeresClick();
            textBoxTanuloNeve_TextChanged(sender, e);



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
            buttonKeresClick();
            textBoxTanuloNeve_TextChanged(sender, e);

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
            buttonKeresClick();
            textBoxTanuloNeve_TextChanged(sender, e);

        }

        private void buttonKeresClick()
        {
            Global.dataGridViewBasicSettings(dataGridView1, panelKeres, textBoxTanuloNeveKeres, textBoxanyjaNeveKeres, numericUpDownVizsgaÉveKeres);
            Global.dataGridViewClear(dataGridView1);
            Global.firstClickShow(panelFeltModTorl, panelKeres, dataGridView1);
            Global.buttonClickClear(dataGridView1, textBoxTanuloNeveKeres, textBoxanyjaNeveKeres, numericUpDownVizsgaÉveKeres, checkBoxVizsgaEve);
            labelMenuKat.Text = Global.globSelectedButton;
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Global.showAndHidePAnels(panelSzakmaiViszgaTorzslapFeltolt, panelErettsegiTanusitvanyFeltolt, panelErettsegiTorzslapFeltolt, panelSzakmaiVizsgaAnyakonyvFeltolt, panelKozepiskolaAnyakonyvFeltolt);
                    Global.getDestPathFromDatabase(@"\Adatok\Szakmai Vizsga\Törzslap\", "eleresiUt");
                    break;
                case "Érettségi - törzslap":
                    Global.showAndHidePAnels(panelErettsegiTorzslapFeltolt, panelSzakmaiViszgaTorzslapFeltolt, panelErettsegiTanusitvanyFeltolt, panelSzakmaiVizsgaAnyakonyvFeltolt, panelKozepiskolaAnyakonyvFeltolt);
                    Global.getDestPathFromDatabase(@"\Adatok\Érettségi\Törzslap\", "eleresiUt");
                    break;
                case "Érettségi - tanusítvány":
                    Global.showAndHidePAnels(panelErettsegiTanusitvanyFeltolt, panelErettsegiTorzslapFeltolt, panelSzakmaiViszgaTorzslapFeltolt, panelSzakmaiVizsgaAnyakonyvFeltolt, panelKozepiskolaAnyakonyvFeltolt);
                    Global.getDestPathFromDatabase(@"\Adatok\Érettségi\Tanusítvány\", "eleresiUt");
                    break;
                case "Szakmai vizsga - anyakönyv":
                    Global.showAndHidePAnels(panelSzakmaiVizsgaAnyakonyvFeltolt, panelErettsegiTanusitvanyFeltolt, panelErettsegiTorzslapFeltolt, panelSzakmaiViszgaTorzslapFeltolt, panelKozepiskolaAnyakonyvFeltolt);
                    Global.getDestPathFromDatabase(@"\Adatok\Szakmai Vizsga\Anyakönyv\", "eleresiUt");
                    break;
                case "Középiskola - anyakönyv":
                    Global.showAndHidePAnels(panelKozepiskolaAnyakonyvFeltolt, panelSzakmaiViszgaTorzslapFeltolt, panelErettsegiTanusitvanyFeltolt, panelErettsegiTorzslapFeltolt, panelSzakmaiVizsgaAnyakonyvFeltolt);
                    Global.getDestPathFromDatabase(@"\Adatok\Középiskola\Anyakönyv\", "eleresiUt");
                    break;
            }
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
                                        textBoxTanuloNeveKeres.Text,
                                        textBoxanyjaNeveKeres.Text,
                                        numericUpDownVizsgaÉveKeres.Value.ToString(),
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

        private void buttonFeltoltes_Click(object sender, EventArgs e)
        {
            //dataGridView1.SelectedCells[0].Value.ToString() --> id
            if (panelMenu.Visible)
            {
                panelMenu.Width = 0;
                panelMenu.Visible = false;
                panelFeltolt.Visible = false;
                panelModTorol.Visible = true;
                Global.setAndResetButtonColors(buttonFeltoltes, buttonTorles, buttonModositas, buttonTorles, buttonFeltoltes);
            }
            else
            {
                panelMenu.Visible = true;       //600 széles legyen
                for (int i = 0; i < 30; i++)
                {
                    panelMenu.Width += 20;
                    System.Threading.Thread.Sleep(1);
                }
                panelFeltolt.Visible = true;
                Global.setAndResetButtonColors(buttonFeltoltes, buttonTorles, buttonModositas, buttonTorles, buttonModositas);
                panelModTorol.Visible = false;
                panelTallozMentesujButton.Visible = true;
                panelSzakmaiViszgaTorzslapFileName.Visible = true;
                panelKozepiskolaAnyakonyvFilneName.Visible = true;
                panelErettsegiTanusitvanyFileName.Visible = true;
                panelErettsegiTtorzslapFileName.Visible = true;
                panelSzakmaivizsgaAnyakonyvFileName.Visible = true;
            }
        }

        private void buttonModositas_Click(object sender, EventArgs e)
        {
            if (panelMenu.Visible)
            {
                panelMenu.Width = 0;
                panelMenu.Visible = false;
                panelFeltolt.Visible = false;
                buttonFeltoltes.Visible = true;
                buttonTorles.Visible = true;
                Global.setAndResetButtonColors(buttonModositas, buttonTorles, buttonFeltoltes, buttonTorles, buttonModositas);
            }
            else
            {
                panelMenu.Visible = true;       //600 széles legyen
                for (int i = 0; i < 30; i++)
                {
                    panelMenu.Width += 20;
                    System.Threading.Thread.Sleep(1);
                }
                panelFeltolt.Visible = true;
                Global.setAndResetButtonColors(buttonModositas, buttonTorles, buttonFeltoltes, buttonTorles, buttonFeltoltes);
                panelSzakmaiViszgaTorzslapFileName.Visible = false;
                panelKozepiskolaAnyakonyvFilneName.Visible = false;
                panelErettsegiTanusitvanyFileName.Visible = false;
                panelErettsegiTtorzslapFileName.Visible = false;
                panelSzakmaivizsgaAnyakonyvFileName.Visible = false;
                panelTallozMentesujButton.Visible = false;
                buttonFeltoltes.Visible = false;
                buttonTorles.Visible = false;
                switch (labelMenuKat.Text)
                {
                    case "Szakmai vizsga - törzslap":
                        Global.loadSelectedDataWhenModifying(dataGridView1, textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                                                    textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                                                    numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap, null,
                                                    radioButtonTavaszFeltoltSzakmaiViszgaTorzslap, radioButtonOszFeltoltSzakmaiViszgaTorzslap
                                                    , null);
                        break;
                    case "Érettségi - törzslap":
                        Global.loadSelectedDataWhenModifying(dataGridView1, textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap, numericUpDownViszgaEveFeltoltErettsegiTorzslap, null,
                            radioButtonTavaszFeltoltErettsegiTorzslap, radioButtonOszFeltoltErettsegiTorzslap
                            , null);
                        break;
                    case "Érettségi - tanusítvány":
                        Global.loadSelectedDataWhenModifying(dataGridView1, textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany, numericUpDownVizsgaEveFeltoltErettsegiTanusitvany, textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                            null, null
                            , null);
                        break;
                    case "Szakmai vizsga - anyakönyv":
                        Global.loadSelectedDataWhenModifying(dataGridView1, textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt, numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv, null,
                            null, null
                            , numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv);
                        break;
                    case "Középiskola - anyakönyv":
                        Global.loadSelectedDataWhenModifying(dataGridView1, textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                            textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv, numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv, null,
                            null, null
                            , numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv);
                        break;
                }
                
            }
        }

        private void buttonTorles_Click(object sender, EventArgs e)
        {
            
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Global.ujTorles(dataGridView1.SelectedCells[0].Value.ToString(), "szakmaivizsgaTorzslap", Global.fullPath);
                    break;
                case "Érettségi - törzslap":
                    Global.ujTorles(dataGridView1.SelectedCells[0].Value.ToString(), "erettsegitorzslap", Global.fullPath);
                    break;
                case "Érettségi - tanusítvány":
                    Global.ujTorles(dataGridView1.SelectedCells[0].Value.ToString(), "erettsegitanusitvany", Global.fullPath);
                    break;
                case "Szakmai vizsga - anyakönyv":
                    Global.ujTorles(dataGridView1.SelectedCells[0].Value.ToString(), "szakmaivizsgaanyakonyv", Global.fullPath);
                    break;
                case "Középiskola - anyakönyv":
                    Global.ujTorles(dataGridView1.SelectedCells[0].Value.ToString(), "kozepiskolaanyakonyv", Global.fullPath);
                    break;
            }

        }

        


        private void buttonTalloz_Click(object sender, EventArgs e)
        {
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Global.tallozas(textBoxFileNameFeltoltSzakmaiViszgaTorzslap);
                    break;
                case "Érettségi - törzslap":
                    Global.tallozas(textBoxFileNameFeltoltErettsegiTorzslap);
                    break;
                case "Érettségi - tanusítvány":
                    Global.tallozas(textBoxFileNameFeltoltErettsegiTanusitvany);
                    break;
                case "Szakmai vizsga - anyakönyv":
                    Global.tallozas(textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv);
                    break;
                case "Középiskola - anyakönyv":
                    Global.tallozas(textBoxFileNameFeltoltKozepsikolaAnyakonyv);
                    break;
            }


            //Global.tallozas(textBoxFileNameFeltoltSzakmaiViszgaTorzslap);
        }

        private void buttonMegse_Click(object sender, EventArgs e)
        {
            buttonFeltoltes_Click(sender, e);
        }

        private void buttonMentes_Click(object sender, EventArgs e)
        {
            if (buttonFeltoltes.BackColor == Color.Black)
            {
                MessageBox.Show("Global fullpath" + Global.fullPath);
                MessageBox.Show("Global feltoltendo" + Global.globFeltoltendoFileEleresiUt);

                switch (labelMenuKat.Text)
                {
                    case "Szakmai vizsga - törzslap":
                        if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap, textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                                                textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap, textBoxFileNameFeltoltSzakmaiViszgaTorzslap))
                        {
                            Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap, textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                                                                    Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Szakmai Vizsga\Törzslap\", radioButtonOszFeltoltSzakmaiViszgaTorzslap,
                                                                    numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap,
                                                                    "szakmaivizsgaTorzslap", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                                                    "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        }
                        break;

                    case "Érettségi - törzslap":
                        if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltErettsegiTorzslap, textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap, textBoxFileNameFeltoltErettsegiTorzslap))
                        {
                            Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltErettsegiTorzslap, textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                                                Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Érettségi\Törzslap\", radioButtonOszFeltoltErettsegiTorzslap,
                                                                numericUpDownViszgaEveFeltoltErettsegiTorzslap,
                                                                "erettsegitorzslap", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        }
                        break;

                    case "Érettségi - tanusítvány":
                        if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltErettsegiTanusitvany, textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                                    textBoxTanuloNeveFeltoltErettsegiTanusitvany, textBoxFileNameFeltoltErettsegiTanusitvany))
                        {
                            Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltErettsegiTanusitvany, textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                                                Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Érettségi\Tanusítvány\", textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                                                numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                                                "erettsegitanusitvany", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "tanuloiAzonosito",
                                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        }
                        break;

                    case "Szakmai vizsga - anyakönyv":
                        if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv, textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                                    textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt, textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv))
                        {
                            Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv, textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                                                Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Szakmai Vizsga\Anyakönyv\", numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                                                numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                                                "szakmaivizsgaanyakonyv", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "vizsgaEvKezdet",
                                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        }
                        break;

                    case "Középiskola - anyakönyv":
                        if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv, textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                                    textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv, textBoxFileNameFeltoltKozepsikolaAnyakonyv))
                        {
                            Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv, textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                                                Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Középiskola\Anyakönyv\", numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                                                numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                                                                "kozepiskolaanyakonyv", "tanuloNeve", "anyjaNeve", "szerzo", "vizsgaEvKezdet", "viszgaEvVeg",
                                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        }
                        break;
                }
            }
            else if (buttonModositas.BackColor == Color.Black)
            {
                switch (labelMenuKat.Text)
                {
                    case "Szakmai vizsga - törzslap":
                        Global.modositas(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap, textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap, radioButtonTavaszFeltoltSzakmaiViszgaTorzslap, null, numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap, null,
                                 "szakmaivizsgaTorzslap",
                                 "tanuloNeve", "anyjaNeve", "vizsgaEvVeg", "vizsgaTavasz1Osz0",
                                 dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Érettségi - törzslap":
                        Global.modositas(textBoxAnyjaNeveFeltoltErettsegiTorzslap, textBoxTanuloNeveFeltoltErettsegiTorzslap, radioButtonTavaszFeltoltErettsegiTorzslap, null, numericUpDownViszgaEveFeltoltErettsegiTorzslap, null,
                                 "erettsegitorzslap",
                                 "tanuloNeve", "anyjaNeve", "vizsgaEvVeg", "vizsgaTavasz1Osz0", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Érettségi - tanusítvány":
                        Global.modositas(textBoxAnyjaNeveFeltoltErettsegiTanusitvany, textBoxTanuloNeveFeltoltErettsegiTanusitvany, null, textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany, numericUpDownVizsgaEveFeltoltErettsegiTanusitvany, null, 
                                 "erettsegitanusitvany",
                                 "tanuloNeve", "anyjaNeve", "vizsgaEvVeg", "tanuloiAzonosito", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Szakmai vizsga - anyakönyv":
                        Global.modositas(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv, textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt, null, null, numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv, numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                 "szakmaivizsgaanyakonyv",
                                 "tanuloNeve", "anyjaNeve", "vizsgaEvKezdet", "vizsgaEvVeg", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Középiskola - anyakönyv":
                        Global.modositas(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv, textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv, null, null, numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv, numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                 "kozepiskolaanyakonyv",
                                 "tanuloNeve", "anyjaNeve", "vizsgaEvKezdet", "vizsgaEvVeg", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                }
            }
            buttonFeltoltes_Click(sender, e);
        }

        private void textBoxTanuloNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            Global.textBoxTextChanged(textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap);
        }

        private void textBoxAnyjaNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            Global.textBoxTextChanged(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap);
        }


        private void textBoxFileNameFeltolt_TextChanged(object sender, EventArgs e)
        {
            Global.textBoxTextChanged(textBoxFileNameFeltoltSzakmaiViszgaTorzslap);
        }

        private void numericUpDownKozepiskKezdeteKozepiskolaAnyakonyv_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Value = numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Value + 4;
        }

        private void buttonMentesUj_Click(object sender, EventArgs e)
        {
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap, textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                                            textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap, textBoxFileNameFeltoltSzakmaiViszgaTorzslap))
                    {
                        Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap, textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                                                                Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Szakmai Vizsga\Törzslap\", radioButtonOszFeltoltSzakmaiViszgaTorzslap,
                                                                numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap,
                                                                "szakmaivizsgaTorzslap", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                    }
                    break;

                case "Érettségi - törzslap":
                    if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltErettsegiTorzslap, textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                        textBoxTanuloNeveFeltoltErettsegiTorzslap, textBoxFileNameFeltoltErettsegiTorzslap))
                    {
                        Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltErettsegiTorzslap, textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                                            Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Érettségi\Törzslap\", radioButtonOszFeltoltErettsegiTorzslap,
                                                            numericUpDownViszgaEveFeltoltErettsegiTorzslap,
                                                            "erettsegitorzslap", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                                            "dokLegutobbModositva", "feltoltesIdopontja", "path");
                    }
                    break;

                case "Érettségi - tanusítvány":
                    if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltErettsegiTanusitvany, textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                                textBoxTanuloNeveFeltoltErettsegiTanusitvany, textBoxFileNameFeltoltErettsegiTanusitvany))
                    {
                        Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltErettsegiTanusitvany, textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                                            Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Érettségi\Tanusítvány\", textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                                            numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                                            "erettsegitanusitvany", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "tanuloiAzonosito",
                                                            "dokLegutobbModositva", "feltoltesIdopontja", "path");
                    }
                    break;

                case "Szakmai vizsga - anyakönyv":
                    if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv, textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                                textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt, textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv))
                    {
                        Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv, textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                                            Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Szakmai Vizsga\Anyakönyv\", numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                                            numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                                            "szakmaivizsgaanyakonyv", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "vizsgaEvKezdet",
                                                            "dokLegutobbModositva", "feltoltesIdopontja", "path");
                    }
                    break;

                case "Középiskola - anyakönyv":
                    if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv, textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                                textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv, textBoxFileNameFeltoltKozepsikolaAnyakonyv))
                    {
                        Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv, textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                                            Global.globFeltoltendoFileEleresiUt, Global.fullPath + @"\Adatok\Középiskola\Anyakönyv\", numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                                            numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                                                            "kozepiskolaanyakonyv", "tanuloNeve", "anyjaNeve", "szerzo", "vizsgaEvKezdet", "viszgaEvVeg",
                                                            "dokLegutobbModositva", "feltoltesIdopontja", "path");
                    }
                    break;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Global.fileKereseseFajlkezeloben(Global.fullPath + @"\Adatok\Szakmai vizsga\Törzslap\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), "szakmaivizsgaTorzslap");
                    break;

                case "Érettségi - törzslap":
                    Global.fileKereseseFajlkezeloben(Global.fullPath + @"\Adatok\Érettségi\Törzslap\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), "erettsegitorzslap");
                    break;

                case "Érettségi - tanusítvány":
                    Global.fileKereseseFajlkezeloben(Global.fullPath + @"\Adatok\Érettségi\Tanusítvány\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), "erettsegitanusitvany");
                    break;

                case "Szakmai vizsga - anyakönyv":
                    Global.fileKereseseFajlkezeloben(Global.fullPath + @"\Adatok\Szakmai vizsga\Anyakönyv\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), "szakmaivizsgaanyakonyv");
                    break;

                case "Középiskola - anyakönyv":
                    Global.fileKereseseFajlkezeloben(Global.fullPath + @"\Adatok\Középiskola\Anyakönyv\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), "kozepiskolaanyakonyv");
                    break;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (var item in Global.torlendoMappak)
                {
                    System.IO.Directory.Delete(item, true);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("A program futása közben megnyitott fájl még meg van nyitva, így nem tud leállni a program.\nA megnyitott file-t mentsd másként vagy zárd be.");
                e.Cancel = true;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                panelModTorol.Visible = true;
            }
            else
            {
                panelModTorol.Visible = false;
            }
        }
    }
}
