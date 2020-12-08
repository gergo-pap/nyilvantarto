using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Nyilvantarto_v2
{
    public partial class FormMain : Form
    {
        private List<Button> buttons;

        public FormMain()
        {
            InitializeComponent();

            buttons = new List<Button> {
                buttonErettsegiTanusitvany,
                buttonKozepiskolaAnyakonyv,
                buttonErettsegiTorzslap,
                buttonSzakmaiVizsgaTorzslap,
                buttonSzakmaiVizsgaAnyakonyv
            };
        }


        //Form load, closing
        private void FormMain_Load(object sender, EventArgs e)
        {
            panelMenu.Left = - panelMenu.Width;

            if (!Controll.CheckDB_Conn(true))
            {
                return;
            }

            Controll.CreateTables();
            Controll.CheckDirs(groupBoxEleresi, labelMentesiHely, panelKeres, "eleresiUt");
            labelPath.Text = Controll.fileStorageRelativePath;
            //ActivateErettsegiTorzslap();

            Controll.loadFileStorageRelativePath();
        }
        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            SetControlPosition();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Controll.DeleteDirsInTemp();
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


        //Első induláskori tallózás
        private void ButtonTallozas_Click(object sender, EventArgs e)
        {
            ElsoInditasSetup();
        }


        //Feltolt, modosít, törölt gombok eseményei
        private void ButtonModositas_Click(object sender, EventArgs e)
        {
            if (panelMenu.Visible)
            {
                HidePanelMenu();
            }
            else
            {
                ShowPanelMenu();

                buttonFeltoltes.Visible = false;
                buttonTorles.Visible = false;

                switch (labelMenuKat.Text)
                {
                    case "Szakmai vizsga - törzslap":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                            numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                            null,
                            radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                            radioButtonOszFeltoltSzakmaiVizsgaTorzslap,
                            null);
                        break;
                    case "Érettségi - törzslap":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                            null,
                            radioButtonTavaszFeltoltErettsegiTorzslap,
                            radioButtonOszFeltoltErettsegiTorzslap,
                            null);
                        break;
                    case "Érettségi - tanusítvány":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                            numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                            textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                            null,
                            null,
                            null);
                        break;
                    case "Szakmai vizsga - anyakönyv":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                            null,
                            null,
                            null,
                            numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv);
                        break;
                    case "Középiskola - anyakönyv":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
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
        private void ButtonFeltoltes_Click(object sender, EventArgs e)
        {
            TogglePanelMenu();
        }
        private void ButtonTorles_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.SelectedCells[0].Value.ToString();
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Controll.Torles(id, "szakmaivizsgatorzslap", @"\Adatok\Szakmai Vizsga\Törzslap\");
                    break;
                case "Érettségi - törzslap":
                    Controll.Torles(id, "erettsegitorzslap", @"\Adatok\Érettségi\Törzslap\");
                    break;
                case "Érettségi - tanusítvány":
                    Controll.Torles(id, "erettsegitanusitvany", @"\Adatok\Érettségi\Tanusítvány\");
                    break;
                case "Szakmai vizsga - anyakönyv":
                    Controll.Torles(id, "szakmaivizsgaanyakonyv", @"\Adatok\Szakmai Vizsga\Anyakönyv\");
                    break;
                case "Középiskola - anyakönyv":
                    Controll.Torles(id, "kozepiskolaanyakonyv", @"\Adatok\Középiskola\Anyakönyv\");
                    break;
            }

            UpdateResultSet();
        }


        //Oldalsó panel menü események
        private void ButtonTalloz_Click(object sender, EventArgs e)
        {
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Controll.tallozas(textBoxFileNameFeltoltSzakmaiVizsgaTorzslap);
                    break;
                case "Érettségi - törzslap":
                    Controll.tallozas(textBoxFileNameFeltoltErettsegiTorzslap);
                    break;
                case "Érettségi - tanusítvány":
                    Controll.tallozas(textBoxFileNameFeltoltErettsegiTanusitvany);
                    break;
                case "Szakmai vizsga - anyakönyv":
                    Controll.tallozas(textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv);
                    break;
                case "Középiskola - anyakönyv":
                    Controll.tallozas(textBoxFileNameFeltoltKozepsikolaAnyakonyv);
                    break;
            }
        }
        private void buttonMentesUj_Click(object sender, EventArgs e)
        {
            SaveChanges();
            CleanupForm();
        }
        private void ButtonMentes_Click(object sender, EventArgs e)
        {
            buttonFeltoltes.Visible = true;
            buttonTorles.Visible = true;

            SaveChanges();
            HidePanelMenu();
        }
        private void buttonMegse_Click(object sender, EventArgs e)
        {
            TogglePanelMenu();
            buttonFeltoltes.Visible = true;
            buttonTorles.Visible = true;
        }
        private void numericUpDownKozepiskKezdeteKozepiskolaAnyakonyv_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Value =
                numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Value + 4;
        }
        private void numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Value =
                numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Value + 4;
        }


        //Oldalsó panel validálás
        private void textBoxTanuloNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            Controll.CheckTbTextLength(textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap);
        }
        private void textBoxAnyjaNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            Controll.CheckTbTextLength(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap);
        }
        private void textBoxFileNameFeltolt_TextChanged(object sender, EventArgs e)
        {
            Controll.CheckTbTextLength(textBoxFileNameFeltoltSzakmaiVizsgaTorzslap);
        }


        //Kategória választó gombok esemémnyei
        private void buttonErettsegiTorzslap_Click(object sender, EventArgs e)
        {
            ActivateErettsegiTorzslap();
        }
        private void buttonErettsegiTanusitvany_Click(object sender, EventArgs e)
        {
            SetAndResetButtonColors(buttonErettsegiTanusitvany);
            Controll.SetDatagridViewColumns(dataGridView1, "Vizsga éve", "Tanulói azonosító");
            ButtonKeresClick();
            UpdateResultSet();
        }
        private void buttonSzakmaiVizsgaTorzslap_Click(object sender, EventArgs e)
        {
            SetAndResetButtonColors(buttonSzakmaiVizsgaTorzslap);
            Controll.SetDatagridViewColumns(dataGridView1, "Vizsga éve", "Vizsga időszaka");
            ButtonKeresClick();
            UpdateResultSet();
        }
        private void buttonSzakmaiVizsgaAnyakonyv_Click(object sender, EventArgs e)
        {
            SetAndResetButtonColors(buttonSzakmaiVizsgaAnyakonyv);
            Controll.SetDatagridViewColumns(dataGridView1, "Középiskola kezdete", "Érettségi éve");
            ButtonKeresClick();
            UpdateResultSet();
        }
        private void buttonKozepiskolaAnyakonyv_Click(object sender, EventArgs e)
        {
            SetAndResetButtonColors(buttonKozepiskolaAnyakonyv);
            Controll.SetDatagridViewColumns(dataGridView1, "Középiskola kezdete", "Érettségi éve");
            ButtonKeresClick();
            UpdateResultSet();
        }


        //Keresés a datagridview-ban események
        private void TextBoxTanuloNeve_TextChanged(object sender, EventArgs e)
        {
            UpdateResultSet();
        }
        private void textBoxAnyjaNeve_TextChanged(object sender, EventArgs e)
        {
            UpdateResultSet();
        }
        private void numericUpDownVizsgaÉve_ValueChanged(object sender, EventArgs e)
        {
            UpdateResultSet();
        }
        private void checkBoxVizsgaEve_CheckedChanged(object sender, EventArgs e)
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
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Controll.SearchFileInFileExplorer(@"\Adatok\Szakmai vizsga\Törzslap",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "szakmaivizsgaTorzslap"
                    );
                    break;

                case "Érettségi - törzslap":
                    Controll.SearchFileInFileExplorer(@"\Adatok\Érettségi\Törzslap",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "erettsegitorzslap"
                    );
                    break;

                case "Érettségi - tanusítvány":
                    Controll.SearchFileInFileExplorer(@"\Adatok\Érettségi\Tanusítvány",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "erettsegitanusitvany"
                    );
                    break;

                case "Szakmai vizsga - anyakönyv":
                    Controll.SearchFileInFileExplorer(@"\Adatok\Szakmai vizsga\Anyakönyv",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "szakmaivizsgaanyakonyv"
                    );
                    break;

                case "Középiskola - anyakönyv":
                    Controll.SearchFileInFileExplorer(@"\Adatok\Középiskola\Anyakönyv",
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        "kozepiskolaanyakonyv"
                    );
                    break;
            }
        }


        //Timer tick
        private void updateDbStateTimer_Tick(object sender, EventArgs e)
        {
            if (Controll.CheckDB_Conn(false))
            {
                labelKapcsolatAdatbazissal.Text = "aktív";
                labelKapcsolatAdatbazissal.BackColor = Color.LightGreen;
                Controll.dataGridViewBasicSettings(dataGridView1, panelKeres);
            }
            else
            {
                labelKapcsolatAdatbazissal.Text = "offline";
                labelKapcsolatAdatbazissal.BackColor = Color.Red;
                Controll.DataGridViewOffline(dataGridView1, panelKeres, panelFeltModTorl);
            }
        }


        //Egyéb
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
            panelSzakmaiVizsgaTorzslapFeltolt.Location = new Point(0, 100);
            panelSzakmaiVizsgaAnyakonyvFeltolt.Location = new Point(0, 100);
            //56,84
        }
        private void ButtonKeresClick()
        {
            Controll.dataGridViewBasicSettings(dataGridView1, panelKeres);
            Controll.dataGridViewClear(dataGridView1);
            Controll.FirstClickShow(panelFeltModTorl, panelKeres, dataGridView1);
            Controll.buttonClickClear(textBoxTanuloNeveKeres, textBoxanyjaNeveKeres, numericUpDownVizsgaÉveKeres,
                checkBoxVizsgaEve);
            labelMenuKat.Text = Controll.globSelectedButton;
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    Controll.ShowFirstHideRestPanels(panelSzakmaiVizsgaTorzslapFeltolt,
                        panelErettsegiTanusitvanyFeltolt,
                        panelErettsegiTorzslapFeltolt,
                        panelSzakmaiVizsgaAnyakonyvFeltolt,
                        panelKozepiskolaAnyakonyvFeltolt);
                    break;
                case "Érettségi - törzslap":
                    Controll.ShowFirstHideRestPanels(panelErettsegiTorzslapFeltolt,
                        panelSzakmaiVizsgaTorzslapFeltolt,
                        panelErettsegiTanusitvanyFeltolt,
                        panelSzakmaiVizsgaAnyakonyvFeltolt,
                        panelKozepiskolaAnyakonyvFeltolt);
                    break;
                case "Érettségi - tanusítvány":
                    Controll.ShowFirstHideRestPanels(panelErettsegiTanusitvanyFeltolt,
                        panelErettsegiTorzslapFeltolt,
                        panelSzakmaiVizsgaTorzslapFeltolt,
                        panelSzakmaiVizsgaAnyakonyvFeltolt,
                        panelKozepiskolaAnyakonyvFeltolt);
                    break;
                case "Szakmai vizsga - anyakönyv":
                    Controll.ShowFirstHideRestPanels(panelSzakmaiVizsgaAnyakonyvFeltolt,
                        panelErettsegiTanusitvanyFeltolt,
                        panelErettsegiTorzslapFeltolt,
                        panelSzakmaiVizsgaTorzslapFeltolt,
                        panelKozepiskolaAnyakonyvFeltolt);
                    break;
                case "Középiskola - anyakönyv":
                    Controll.ShowFirstHideRestPanels(panelKozepiskolaAnyakonyvFeltolt,
                        panelSzakmaiVizsgaTorzslapFeltolt,
                        panelErettsegiTanusitvanyFeltolt,
                        panelErettsegiTorzslapFeltolt,
                        panelSzakmaiVizsgaAnyakonyvFeltolt);
                    break;
            }
        }
        private void UpdateResultSet()
        {
            CalculateSearchConditions(out var from, out var row1ErreKeres, out var row2EztIsKiir);

            Controll.dataGridViewClear(dataGridView1);
            Controll.datagridviewKeres(
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
            else if (buttonSzakmaiVizsgaAnyakonyv.BackColor == Color.Black)
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
        private void TogglePanelMenu()
        {
            //dataGridView1.SelectedCells[0].Value.ToString() --> id
            if (panelMenu.Visible)
            {
                HidePanelMenu();
            }
            else
            {
                ShowPanelMenu();
            }
        }
        private void ShowPanelMenu()
        {
            panelFeltolt.Visible = true;
            SetAndResetButtonColors(buttonFeltoltes);
            panelModTorol.Visible = false;
            Controll.SetPanelVisibility(panelTallozMentesujButton,
                panelSzakmaiVizsgaTorzslapFileName,
                panelKozepiskolaAnyakonyvFilneName,
                panelErettsegiTanusitvanyFileName,
                panelErettsegiTtorzslapFileName,
                panelSzakmaivizsgaAnyakonyvFileName,
                true);
            ;

            panelMenu.BringToFront();
            panelMenu.Visible = true;

            while (panelMenu.Left < -10)
            {
                panelMenu.Left += Math.Max(1, (int)((double)Math.Abs(panelMenu.Left) * 0.25));
                panelMenu.Refresh();
            }

            panelMenu.Left = 0;

        }
        private void HidePanelMenu()
        {
            panelMenu.Left = - panelMenu.Width;
            panelMenu.Visible = false;
            panelFeltolt.Visible = false;
            panelModTorol.Visible = true;
            SetAndResetButtonColors(buttonFeltoltes);

            CleanupForm();
        }
        private void SaveChanges()
        {
            if (buttonFeltoltes.BackColor == Color.Black)
            {
                //MessageBox.Show("Global fullpath" + Global.fileStorageRelativePath);
                //MessageBox.Show("Global feltoltendo" + Global.globFeltoltendoFileEleresiUt);

                switch (labelMenuKat.Text)
                {
                    case "Szakmai vizsga - törzslap":
                        if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                            textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                            textBoxFileNameFeltoltSzakmaiVizsgaTorzslap))
                        {
                            Controll.FileFeltolteseBDreESFileMozgatasa(
                                textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                                textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                                @"\Adatok\Szakmai Vizsga\Törzslap\",
                                radioButtonOszFeltoltSzakmaiVizsgaTorzslap,
                                numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                                "szakmaivizsgaTorzslap",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "vizsgaEvVeg",
                                "vizsgaTavasz1Osz0",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");

                            Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                                textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                                numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                                null,
                                radioButtonOszFeltoltSzakmaiVizsgaTorzslap,
                                radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                                null,
                                textBoxFileNameFeltoltSzakmaiVizsgaTorzslap);
                        }

                        break;

                    case "Érettségi - törzslap":
                        if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            textBoxFileNameFeltoltErettsegiTorzslap))
                        {
                            Controll.FileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                @"\Adatok\Érettségi\Törzslap\",
                                radioButtonOszFeltoltErettsegiTorzslap,
                                numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                                "erettsegitorzslap",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "vizsgaEvVeg",
                                "vizsgaTavasz1Osz0",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");

                            Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                                null,
                                radioButtonOszFeltoltErettsegiTorzslap,
                                radioButtonTavaszFeltoltErettsegiTorzslap,
                                null,
                                textBoxFileNameFeltoltErettsegiTorzslap);
                        }

                        break;

                    case "Érettségi - tanusítvány":
                        if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                            textBoxFileNameFeltoltErettsegiTanusitvany))
                        {
                            Controll.FileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                @"\Adatok\Érettségi\Tanusítvány\",
                                textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                "erettsegitanusitvany",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "vizsgaEvVeg",
                                "tanuloiAzonosito",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");
                            Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
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
                        if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv))
                        {
                            Controll.FileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                                textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                @"\Adatok\Szakmai Vizsga\Anyakönyv\",
                                numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                "szakmaivizsgaanyakonyv",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "vizsgaEvVeg",
                                "vizsgaEvKezdet",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");
                            Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
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
                        if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                            textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                            textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv, textBoxFileNameFeltoltKozepsikolaAnyakonyv))
                        {
                            Controll.FileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                @"\Adatok\Középiskola\Anyakönyv\",
                                numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                                "kozepiskolaanyakonyv",
                                "tanuloNeve",
                                "anyjaNeve",
                                "szerzo",
                                "vizsgaEvKezdet",
                                "vizsgaEvVeg",
                                "dokLegutobbModositva",
                                "feltoltesIdopontja",
                                "filename");

                            Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
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
                        Controll.Modositas(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                            radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                            null,
                            numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                            null,
                            "szakmaivizsgaTorzslap",
                            "tanuloNeve",
                            "anyjaNeve",
                            "vizsgaEvVeg",
                            "vizsgaTavasz1Osz0",
                            dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Érettségi - törzslap":
                        Controll.Modositas(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            radioButtonTavaszFeltoltErettsegiTorzslap,
                            null,
                            numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                            null,
                            "erettsegitorzslap",
                            "tanuloNeve",
                            "anyjaNeve",
                            "vizsgaEvVeg",
                            "vizsgaTavasz1Osz0",
                            dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case "Érettségi - tanusítvány":
                        Controll.Modositas(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
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
                        Controll.Modositas(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
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
                        Controll.Modositas(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
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
        private void CleanupForm()
        {
            switch (labelMenuKat.Text)
            {
                case "Szakmai vizsga - törzslap":
                    if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                        textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                        textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                        textBoxFileNameFeltoltSzakmaiVizsgaTorzslap))
                    {
                        Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                            numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap, null,
                            radioButtonOszFeltoltSzakmaiVizsgaTorzslap, radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                            null, textBoxFileNameFeltoltSzakmaiVizsgaTorzslap);
                    }

                    break;

                case "Érettségi - törzslap":
                    if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                        textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                        textBoxTanuloNeveFeltoltErettsegiTorzslap,
                        textBoxFileNameFeltoltErettsegiTorzslap))
                    {
                        Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                            null,
                            radioButtonOszFeltoltErettsegiTorzslap,
                            radioButtonTavaszFeltoltErettsegiTorzslap,
                            null,
                            textBoxFileNameFeltoltErettsegiTorzslap);
                    }

                    break;

                case "Érettségi - tanusítvány":
                    if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                        textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                        textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                        textBoxFileNameFeltoltErettsegiTanusitvany))
                    {
                        Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                            numericUpDownVizsgaEveFeltoltErettsegiTanusitvany, null, null, null,
                            textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                            textBoxFileNameFeltoltErettsegiTanusitvany);
                    }

                    break;

                case "Szakmai vizsga - anyakönyv":
                    if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                        textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                        textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                        textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv))
                    {
                        Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
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
                    if (Controll.CheckIfEmptyInput4TextBox(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                        textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                        textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                        textBoxFileNameFeltoltKozepsikolaAnyakonyv))
                    {
                        Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
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
        private void ActivateErettsegiTorzslap()
        {
            SetAndResetButtonColors(buttonErettsegiTorzslap);
            Controll.SetDatagridViewColumns(dataGridView1, "Tanuló neve", "Vizsga időszaka");
            ButtonKeresClick();
            UpdateResultSet();
        }
        private void ElsoInditasSetup()
        {
            Controll.Tallozas(labelMentesiHely);
            Controll.SetPathInDB(labelMentesiHely, groupBoxEleresi, "eleresiUt");
            Controll.CreateDirectiories(labelMentesiHely);
        }

        private void textBoxFileNameFeltoltSzakmaiVizsgaTorzslap_Validating(object sender, CancelEventArgs e)
        {

        }

        public void SetAndResetButtonColors(Button activeButton)
        {
            foreach (Button button in buttons)
            {
                if (button == activeButton)
                {
                    button.BackColor = Color.Black;
                    button.ForeColor = Color.White;
                }
                else
                {
                    button.ForeColor = Color.Black;
                    button.BackColor = default;
                }
            }
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
