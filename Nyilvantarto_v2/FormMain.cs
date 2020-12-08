using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Nyilvantarto_v2
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void ButtonTallozas_Click(object sender, EventArgs e)
        {
            Global.Tallozas(labelMentesiHely);
            Global.SetPathInDB(labelMentesiHely, groupBoxEleresi, "eleresiUt");
            Global.CreateDirectiories(labelMentesiHely);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            panelMenu.Width = 0;

            if (!Global.CheckDB_Conn(true))
            {
                return;
            }

            Global.CreateTables();
            Global.CheckDirs(groupBoxEleresi, labelMentesiHely, panelKeres, "eleresiUt");
            labelPath.Text = Global.fileStorageRelativePath;
            //ActivateErettsegiTorzslap();

            Global.loadFileStorageRelativePath();
        }

        public void SetControlPosition()
        {
            panelKeres.Location = new Point(20, 20);

            panelFeltModTorl.Location = new Point(
                ClientRectangle.Width - panelFeltModTorl.Width,
                panelKeres.Height
            );

            dataGridView1.Location = new Point(20, panelKeres.Bottom + 20);
            dataGridView1.Size = new Size(
                ClientRectangle.Width - panelFeltModTorl.Width - 20,
                groupBoxAlso.Top - dataGridView1.Top
            );

            panelErettsegiTanusitvanyFeltolt.Location = new Point(0, 100);
            panelErettsegiTorzslapFeltolt.Location = new Point(0, 100);
            panelKozepiskolaAnyakonyvFeltolt.Location = new Point(0, 100);
            panelSzakmaiViszgaTorzslapFeltolt.Location = new Point(0, 100);
            panelSzakmaiVizsgaAnyakonyvFeltolt.Location = new Point(0, 100);
            //56,84
        }

        private void updateDbStateTimer_Tick(object sender, EventArgs e)
        {
            if (Global.CheckDB_Conn(false))
            {
                labelKapcsolatAdatbazissal.Text = "aktív";
                labelKapcsolatAdatbazissal.BackColor = Color.LightGreen;
                Global.dataGridViewBasicSettings(dataGridView1, panelKeres);
            }
            else
            {
                labelKapcsolatAdatbazissal.Text = "offline";
                labelKapcsolatAdatbazissal.BackColor = Color.Red;
                Global.DataGridViewOffline(dataGridView1, panelKeres, panelFeltModTorl);
            }
        }

        private void ButtonKeresClick()
        {
            Global.dataGridViewBasicSettings(dataGridView1, panelKeres);
            Global.dataGridViewClear(dataGridView1);
            Global.FirstClickShow(panelFeltModTorl, panelKeres, dataGridView1);
            Global.buttonClickClear(textBoxTanuloNeveKeres, textBoxanyjaNeveKeres, numericUpDownVizsgaÉveKeres,
                checkBoxVizsgaEve);
            labelMenuKat.Text = Global.globSelectedButton;
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Global.ShowAndHidePAnels(panelSzakmaiViszgaTorzslapFeltolt,
                        panelErettsegiTanusitvanyFeltolt,
                        panelErettsegiTorzslapFeltolt,
                        panelSzakmaiVizsgaAnyakonyvFeltolt,
                        panelKozepiskolaAnyakonyvFeltolt);
                    break;
                case "Érettségi - törzslap":
                    Global.ShowAndHidePAnels(panelErettsegiTorzslapFeltolt,
                        panelSzakmaiViszgaTorzslapFeltolt,
                        panelErettsegiTanusitvanyFeltolt,
                        panelSzakmaiVizsgaAnyakonyvFeltolt,
                        panelKozepiskolaAnyakonyvFeltolt);
                    break;
                case "Érettségi - tanusítvány":
                    Global.ShowAndHidePAnels(panelErettsegiTanusitvanyFeltolt,
                        panelErettsegiTorzslapFeltolt,
                        panelSzakmaiViszgaTorzslapFeltolt,
                        panelSzakmaiVizsgaAnyakonyvFeltolt,
                        panelKozepiskolaAnyakonyvFeltolt);
                    break;
                case "Szakmai vizsga - anyakönyv":
                    Global.ShowAndHidePAnels(panelSzakmaiVizsgaAnyakonyvFeltolt,
                        panelErettsegiTanusitvanyFeltolt,
                        panelErettsegiTorzslapFeltolt,
                        panelSzakmaiViszgaTorzslapFeltolt,
                        panelKozepiskolaAnyakonyvFeltolt);
                    break;
                case "Középiskola - anyakönyv":
                    Global.ShowAndHidePAnels(panelKozepiskolaAnyakonyvFeltolt,
                        panelSzakmaiViszgaTorzslapFeltolt,
                        panelErettsegiTanusitvanyFeltolt,
                        panelErettsegiTorzslapFeltolt,
                        panelSzakmaiVizsgaAnyakonyvFeltolt);
                    break;
            }
        }

        private void TextBoxTanuloNeve_TextChanged(object sender, EventArgs e)
        {
            UpdateResultSet();
        }

        private void UpdateResultSet()
        {
            CalculateSearchConditions(out var from, out var row1ErreKeres, out var row2EztIsKiir);

            Global.dataGridViewClear(dataGridView1);
            Global.OsszetettKeresDataGridview(
                "tanuloNeve",
                row1ErreKeres,
                row2EztIsKiir,
                "anyjaNeve",
                @from,
                textBoxTanuloNeveKeres.Text,
                textBoxanyjaNeveKeres.Text,
                numericUpDownVizsgaÉveKeres.Value.ToString(),
                dataGridView1,
                checkBoxVizsgaEve.Checked,
                int.Parse(numericUpDownTalalatokSzama.Value.ToString())
            );
        }

        private void CalculateSearchConditions(out string from, out string row1ErreKeres, out string row2EztIsKiir)
        {
            from = "";
            row1ErreKeres = "";
            row2EztIsKiir = "";
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
        }

        private void ButtonFeltoltes_Click(object sender, EventArgs e)
        {
            togglePanelMenu();
        }

        private void togglePanelMenu()
        {
            //dataGridView1.SelectedCells[0].Value.ToString() --> id
            if (panelMenu.Visible)
            {
                hidePanelMenu();
            }
            else
            {
                showPanelMenu();
            }
        }

        private void showPanelMenu()
        {
            panelMenu.BringToFront();
            panelMenu.Visible = true;

            for (int i = 0; i < 30; i++) //600 széles legyen
            {
                panelMenu.Width += 20;
                Thread.Sleep(3);
            }

            panelFeltolt.Visible = true;
            Global.SetAndResetButtonColors(buttonFeltoltes, buttonTorles, buttonModositas, buttonTorles,
                buttonModositas);
            panelModTorol.Visible = false;
            Global.SetPanelVisibility(panelTallozMentesujButton,
                panelSzakmaiViszgaTorzslapFileName,
                panelKozepiskolaAnyakonyvFilneName,
                panelErettsegiTanusitvanyFileName,
                panelErettsegiTtorzslapFileName,
                panelSzakmaivizsgaAnyakonyvFileName,
                true);
            ;
        }

        private void hidePanelMenu()
        {
            panelMenu.Width = 0;
            panelMenu.Visible = false;
            panelFeltolt.Visible = false;
            panelModTorol.Visible = true;
            Global.SetAndResetButtonColors(buttonFeltoltes, buttonTorles, buttonModositas, buttonTorles,
                buttonFeltoltes);

            cleanupForm();
        }

        private void ButtonModositas_Click(object sender, EventArgs e)
        {
            if (panelMenu.Visible)
            {
                panelMenu.Width = 0;
                panelMenu.Visible = false;
                panelFeltolt.Visible = false;
                buttonFeltoltes.Visible = true;
                buttonTorles.Visible = true;
                Global.SetAndResetButtonColors(buttonModositas, buttonTorles, buttonFeltoltes, buttonTorles,
                    buttonModositas);
            }
            else
            {
                panelMenu.Visible = true; //600 széles legyen
                panelMenu.BringToFront();

                for (int i = 0; i < 30; i++)
                {
                    panelMenu.Width += 20;
                    Thread.Sleep(1);
                }

                panelFeltolt.Visible = true;
                Global.SetAndResetButtonColors(buttonModositas, buttonTorles, buttonFeltoltes, buttonTorles,
                    buttonFeltoltes);
                Global.SetPanelVisibility(panelTallozMentesujButton,
                    panelSzakmaiViszgaTorzslapFileName,
                    panelKozepiskolaAnyakonyvFilneName,
                    panelErettsegiTanusitvanyFileName,
                    panelErettsegiTtorzslapFileName,
                    panelSzakmaivizsgaAnyakonyvFileName,
                    false);
                buttonFeltoltes.Visible = false;
                buttonTorles.Visible = false;
                switch (labelMenuKat.Text)
                {
                    case "Szakmai vizsga - törzslap":
                        Global.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                            numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap,
                            null,
                            radioButtonTavaszFeltoltSzakmaiViszgaTorzslap,
                            radioButtonOszFeltoltSzakmaiViszgaTorzslap,
                            null);
                        break;
                    case "Érettségi - törzslap":
                        Global.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            numericUpDownViszgaEveFeltoltErettsegiTorzslap,
                            null,
                            radioButtonTavaszFeltoltErettsegiTorzslap,
                            radioButtonOszFeltoltErettsegiTorzslap,
                            null);
                        break;
                    case "Érettségi - tanusítvány":
                        Global.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                            numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                            textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                            null,
                            null,
                            null);
                        break;
                    case "Szakmai vizsga - anyakönyv":
                        Global.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                            null,
                            null,
                            null,
                            numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv);
                        break;
                    case "Középiskola - anyakönyv":
                        Global.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                            textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                            numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                            null,
                            null,
                            null,
                            numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv);
                        break;
                }

                UpdateResultSet();
            }
        }

        private void ButtonTorles_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.SelectedCells[0].Value.ToString();
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Global.Torles(id, "szakmaivizsgaTorzslap", @"\Adatok\Szakmai Vizsga\Törzslap\");
                    break;
                case "Érettségi - törzslap":
                    Global.Torles(id, "erettsegitorzslap", @"\Adatok\Érettségi\Törzslap\");
                    break;
                case "Érettségi - tanusítvány":
                    Global.Torles(id, "erettsegitanusitvany", @"\Adatok\Szakmai Vizsga\Törzslap\");
                    break;
                case "Szakmai vizsga - anyakönyv":
                    Global.Torles(id, "szakmaivizsgaanyakonyv", @"\Adatok\Szakmai Vizsga\Törzslap\");
                    break;
                case "Középiskola - anyakönyv":
                    Global.Torles(id, "kozepiskolaanyakonyv", @"\Adatok\Szakmai Vizsga\Törzslap\");
                    break;
            }

            UpdateResultSet();
        }

        private void ButtonTalloz_Click(object sender, EventArgs e)
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
        }

        private void buttonMegse_Click(object sender, EventArgs e)
        {
            togglePanelMenu();
            buttonFeltoltes.Visible = true;
            buttonTorles.Visible = true;
        }

        private void ButtonMentes_Click(object sender, EventArgs e)
        {
            buttonFeltoltes.Visible = true;
            buttonTorles.Visible = true;

            saveChanges();
            hidePanelMenu();
        }

        private void saveChanges()
        {
            if (buttonFeltoltes.BackColor == Color.Black)
            {
                //MessageBox.Show("Global fullpath" + Global.fileStorageRelativePath);
                //MessageBox.Show("Global feltoltendo" + Global.globFeltoltendoFileEleresiUt);

                switch (labelMenuKat.Text)
                {
                    case "Szakmai vizsga - törzslap":
                        if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                            textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                            textBoxFileNameFeltoltSzakmaiViszgaTorzslap))
                        {
                            Global.FileFeltolteseBDreESFileMozgatasa(
                                textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                                textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                                Global.fileStorageRelativePath + @"\Adatok\Szakmai Vizsga\Törzslap\",
                                radioButtonOszFeltoltSzakmaiViszgaTorzslap,
                                numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap,
                                "szakmaivizsgaTorzslap",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "viszgaEvVeg",
                                "viszgaTavasz1Osz0",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");

                            Global.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                                textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                                numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap,
                                null,
                                radioButtonOszFeltoltSzakmaiViszgaTorzslap,
                                radioButtonTavaszFeltoltSzakmaiViszgaTorzslap,
                                null,
                                textBoxFileNameFeltoltSzakmaiViszgaTorzslap);
                        }

                        break;

                    case "Érettségi - törzslap":
                        if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            textBoxFileNameFeltoltErettsegiTorzslap))
                        {
                            Global.FileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                Global.fileStorageRelativePath + @"\Adatok\Érettségi\Törzslap\",
                                radioButtonOszFeltoltErettsegiTorzslap,
                                numericUpDownViszgaEveFeltoltErettsegiTorzslap,
                                "erettsegitorzslap",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "viszgaEvVeg",
                                "viszgaTavasz1Osz0",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");

                            Global.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                numericUpDownViszgaEveFeltoltErettsegiTorzslap,
                                null,
                                radioButtonOszFeltoltErettsegiTorzslap,
                                radioButtonTavaszFeltoltErettsegiTorzslap,
                                null,
                                textBoxFileNameFeltoltErettsegiTorzslap);
                        }

                        break;

                    case "Érettségi - tanusítvány":
                        if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                            textBoxFileNameFeltoltErettsegiTanusitvany))
                        {
                            Global.FileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                Global.fileStorageRelativePath + @"\Adatok\Érettségi\Tanusítvány\",
                                textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                "erettsegitanusitvany",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "viszgaEvVeg",
                                "tanuloiAzonosito",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");
                            Global.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                null,
                                null,
                                null,
                                textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                textBoxFileNameFeltoltErettsegiTanusitvany);
                        }

                        break;

                    case "Szakmai vizsga - anyakönyv":
                        if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv))
                        {
                            Global.FileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                                textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                Global.fileStorageRelativePath + @"\Adatok\Szakmai Vizsga\Anyakönyv\",
                                numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                "szakmaivizsgaanyakonyv",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "viszgaEvVeg",
                                "vizsgaEvKezdet",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");
                            Global.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                                textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                null,
                                null,
                                null,
                                textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv);
                        }

                        break;

                    case "Középiskola - anyakönyv":
                        if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                            textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                            textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv, textBoxFileNameFeltoltKozepsikolaAnyakonyv))
                        {
                            Global.FileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                Global.fileStorageRelativePath + @"\Adatok\Középiskola\Anyakönyv\",
                                numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                                "kozepiskolaanyakonyv",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "vizsgaEvKezdet",
                                "viszgaEvVeg",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");

                            Global.FeltoltUrit(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv, null, null, null,
                                textBoxFileNameFeltoltKozepsikolaAnyakonyv);
                        }

                        break;
                }
            }
            else if (buttonModositas.BackColor == Color.Black)
            {
                switch (labelMenuKat.Text)
                {
                    case "Szakmai vizsga - törzslap":
                        Global.Modositas(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                            radioButtonTavaszFeltoltSzakmaiViszgaTorzslap,
                            null,
                            numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap,
                            null,
                            "szakmaivizsgaTorzslap",
                            "tanuloNeve",
                            "anyjaNeve",
                            "vizsgaEvVeg",
                            "vizsgaTavasz1Osz0",
                            dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Érettségi - törzslap":
                        Global.Modositas(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            radioButtonTavaszFeltoltErettsegiTorzslap,
                            null,
                            numericUpDownViszgaEveFeltoltErettsegiTorzslap,
                            null,
                            "erettsegitorzslap",
                            "tanuloNeve",
                            "anyjaNeve",
                            "vizsgaEvVeg",
                            "vizsgaTavasz1Osz0",
                            dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Érettségi - tanusítvány":
                        Global.Modositas(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                            null,
                            textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                            numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                            null,
                            "erettsegitanusitvany",
                            "tanuloNeve",
                            "anyjaNeve",
                            "vizsgaEvVeg",
                            "tanuloiAzonosito",
                            dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Szakmai vizsga - anyakönyv":
                        Global.Modositas(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            null,
                            null,
                            numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                            numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                            "szakmaivizsgaanyakonyv",
                            "tanuloNeve",
                            "anyjaNeve",
                            "vizsgaEvKezdet",
                            "vizsgaEvVeg",
                            dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Középiskola - anyakönyv":
                        Global.Modositas(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                            textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                            null,
                            null,
                            numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                            numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                            "kozepiskolaanyakonyv",
                            "tanuloNeve",
                            "anyjaNeve",
                            "vizsgaEvKezdet",
                            "vizsgaEvVeg",
                            dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                }
            }

            UpdateResultSet();
        }


        private void buttonMentesUj_Click(object sender, EventArgs e)
        {
            saveChanges();
            cleanupForm();
        }

        private void cleanupForm()
        {
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                        textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                        textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                        textBoxFileNameFeltoltSzakmaiViszgaTorzslap))
                    {
                        Global.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiViszgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiViszgaTorzslap,
                            numericUpDownViszgaEveFeltoltSzakmaiViszgaTorzslap, null,
                            radioButtonOszFeltoltSzakmaiViszgaTorzslap, radioButtonTavaszFeltoltSzakmaiViszgaTorzslap,
                            null, textBoxFileNameFeltoltSzakmaiViszgaTorzslap);
                    }

                    break;

                case "Érettségi - törzslap":
                    if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                        textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                        textBoxTanuloNeveFeltoltErettsegiTorzslap,
                        textBoxFileNameFeltoltErettsegiTorzslap))
                    {
                        Global.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            numericUpDownViszgaEveFeltoltErettsegiTorzslap,
                            null,
                            radioButtonOszFeltoltErettsegiTorzslap,
                            radioButtonTavaszFeltoltErettsegiTorzslap,
                            null,
                            textBoxFileNameFeltoltErettsegiTorzslap);
                    }

                    break;

                case "Érettségi - tanusítvány":
                    if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                        textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                        textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                        textBoxFileNameFeltoltErettsegiTanusitvany))
                    {
                        Global.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                            numericUpDownVizsgaEveFeltoltErettsegiTanusitvany, null, null, null,
                            textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                            textBoxFileNameFeltoltErettsegiTanusitvany);
                    }

                    break;

                case "Szakmai vizsga - anyakönyv":
                    if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                        textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                        textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                        textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv))
                    {
                        Global.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                            numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                            null,
                            null,
                            null,
                            textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv);
                    }

                    break;

                case "Középiskola - anyakönyv":
                    if (Global.CheckIfEmptyInput4Tb(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                        textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                        textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                        textBoxFileNameFeltoltKozepsikolaAnyakonyv))
                    {
                        Global.FeltoltUrit(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                            textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                            numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                            numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                            null,
                            null,
                            null,
                            textBoxFileNameFeltoltKozepsikolaAnyakonyv);
                    }
                    break;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Global.fileKereseseFajlkezeloben(
                        Global.fileStorageRelativePath + @"\Adatok\Szakmai vizsga\Törzslap", 
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "szakmaivizsgaTorzslap"
                    );
                    break;

                case "Érettségi - törzslap":
                    Global.fileKereseseFajlkezeloben(
                        Global.fileStorageRelativePath + @"\Adatok\Érettségi\Törzslap",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "erettsegitorzslap"
                    );
                    break;

                case "Érettségi - tanusítvány":
                    Global.fileKereseseFajlkezeloben(
                        Global.fileStorageRelativePath + @"\Adatok\Érettségi\Tanusítvány",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "erettsegitanusitvany"
                    );
                    break;

                case "Szakmai vizsga - anyakönyv":
                    Global.fileKereseseFajlkezeloben(
                        Global.fileStorageRelativePath + @"\Adatok\Szakmai vizsga\Anyakönyv",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "szakmaivizsgaanyakonyv"
                    );
                    break;

                case "Középiskola - anyakönyv":
                    Global.fileKereseseFajlkezeloben(
                        Global.fileStorageRelativePath + @"\Adatok\Középiskola\Anyakönyv",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "kozepiskolaanyakonyv"
                    );
                    break;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Global.mappakTorlese();
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "A program futása közben megnyitott fájl még meg van nyitva, így nem tud leállni a program.\nA megnyitott file-t mentsd másként vagy zárd be.");
                e.Cancel = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Sikeretlen törlés a program zárásakkor");
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

        private void buttonErettsegiTorzslap_Click(object sender, EventArgs e)
        {
            ActivateErettsegiTorzslap();
        }

        private void ActivateErettsegiTorzslap()
        {
            Global.SetAndResetButtonColors(buttonErettsegiTorzslap,
                buttonErettsegiTanusitvany,
                buttonSzakmaiVizsgaTorzslap,
                buttonKozepiskolaAnyakonyv,
                buttonSzakmaiViszgaAnyakonyv);
            Global.SetDatagridViewColumns(dataGridView1, "Tanuló neve", "Anyja neve", "Vizsga éve", "Vizsga időszaka");
            ButtonKeresClick();
            UpdateResultSet();
        }

        private void buttonErettsegiTanusitvany_Click(object sender, EventArgs e)
        {
            Global.SetAndResetButtonColors(buttonErettsegiTanusitvany,
                buttonKozepiskolaAnyakonyv,
                buttonErettsegiTorzslap,
                buttonSzakmaiVizsgaTorzslap,
                buttonSzakmaiViszgaAnyakonyv);
            Global.SetDatagridViewColumns(dataGridView1, "Tanuló neve", "Anyja neve", "Vizsga éve",
                "Tanulói azonosító");
            ButtonKeresClick();
            UpdateResultSet();
        }

        private void buttonSzakmaiVizsgaTorzslap_Click(object sender, EventArgs e)
        {
            Global.SetAndResetButtonColors(buttonSzakmaiVizsgaTorzslap,
                buttonErettsegiTanusitvany,
                buttonErettsegiTorzslap,
                buttonKozepiskolaAnyakonyv,
                buttonSzakmaiViszgaAnyakonyv);
            Global.SetDatagridViewColumns(dataGridView1, "Tanuló neve", "Anyja neve", "Vizsga éve", "Vizsga időszaka");
            ButtonKeresClick();
            UpdateResultSet();
        }

        private void buttonSzakmaiViszgaAnyakonyv_Click(object sender, EventArgs e)
        {
            Global.SetAndResetButtonColors(buttonSzakmaiViszgaAnyakonyv,
                buttonErettsegiTanusitvany,
                buttonErettsegiTorzslap,
                buttonKozepiskolaAnyakonyv,
                buttonSzakmaiVizsgaTorzslap);
            Global.SetDatagridViewColumns(dataGridView1, "Tanuló neve", "Anyja neve", "Középiskola kezdete",
                "Érettségi éve");
            ButtonKeresClick();
            UpdateResultSet();
        }

        private void buttonKozepiskolaAnyakonyv_Click(object sender, EventArgs e)
        {
            Global.SetAndResetButtonColors(buttonKozepiskolaAnyakonyv,
                buttonErettsegiTanusitvany,
                buttonErettsegiTorzslap,
                buttonSzakmaiViszgaAnyakonyv,
                buttonSzakmaiVizsgaTorzslap);
            Global.SetDatagridViewColumns(dataGridView1, "Tanuló neve", "Anyja neve", "Középiskola kezdete",
                "Érettségi éve");
            ButtonKeresClick();
            UpdateResultSet();
        }

        private void textBoxanyjaNeve_TextChanged(object sender, EventArgs e)
        {
            UpdateResultSet();
        }

        private void numericUpDownViszgaÉve_ValueChanged(object sender, EventArgs e)
        {
            UpdateResultSet();
        }

        private void checkBoxViszgaEve_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxVizsgaEve.Checked)
            {
                numericUpDownVizsgaÉveKeres.Controls[1].Text = "";
            }

            else
            {
                numericUpDownVizsgaÉveKeres.Controls[1].Text = "1900";
            }

            UpdateResultSet();
        }

        private void numericUpDownTalalatokSzama_ValueChanged(object sender, EventArgs e)
        {
            UpdateResultSet();
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
            numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Value =
                numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Value + 4;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            SetControlPosition();
        }
    }
}
