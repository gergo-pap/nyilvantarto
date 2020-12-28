using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Nyilvantarto_v2.Categories;

namespace Nyilvantarto_v2
{
    public partial class FormMain : Form
    {
        private List<Button> buttons;
        private List<Panel> panels;
        private Category selectedCategory;
        private bool feltoltTrueModFalse;

        public FormMain()
        {
            InitializeComponent();

            buttons = new List<Button>
            {
                buttonErettsegiTanusitvany,
                buttonKozepiskolaAnyakonyv,
                buttonErettsegiTorzslap,
                buttonSzakmaiVizsgaTorzslap,
                buttonSzakmaiVizsgaAnyakonyv
            };


            panels = new List<Panel>
            {
                panelSzakmaiVizsgaAnyakonyvFeltolt,
                panelSzakmaiVizsgaTorzslapFeltolt,
                panelErettsegiTanusitvanyFeltolt,
                panelErettsegiTorzslapFeltolt,
                panelKozepiskolaAnyakonyvFeltolt
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
            Controll.dataGridViewBasicSettings(dataGridView1, panelKeres);
            Controll.FirstClickShow(panelFeltModTorl, panelKeres, dataGridView1);
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

        private void ElsoInditasSetup()
        {
            Controll.Tallozas(labelMentesiHely.Text);
            Controll.SetPathInDB(labelMentesiHely.Text, groupBoxEleresi, "eleresiUt");
            Controll.CreateDirectiories(labelMentesiHely.Text);
            Controll.loadFileStorageRelativePath();
        }


        //Feltolt, modosít, törölt gombok eseményei
        private void ButtonModositas_Click(object sender, EventArgs e)
        {
            feltoltTrueModFalse = false;
            if (panelMenu.Visible)
            {
                HidePanelMenu();
            }
            else
            {
                labelMenuKat.Text = selectedCategory.categoryPrettyName;
                ShowPanelMenu();
                buttonFeltoltes.Visible = false;
                buttonTorles.Visible = false;
                //MessageBox.Show(selectedCategory.sqlTableName);
                switch (selectedCategory.sqlTableName)
                {
                    case "szakmaivizsgatorzslap":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                            numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                            null,
                            radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                            radioButtonOszFeltoltSzakmaiVizsgaTorzslap,
                            null);
                        break;
                    case "erettsegitorzslap":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                            numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                            null,
                            radioButtonTavaszFeltoltErettsegiTorzslap,
                            radioButtonOszFeltoltErettsegiTorzslap,
                            null);
                        break;
                    case "erettsegitanusitvany":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                            numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                            textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                            null,
                            null,
                            null);
                        break;
                    case "szakmaivizsgaanyakonyv":
                        Controll.LoadSelectedDataWhenModifying(dataGridView1,
                            textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                            numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                            null,
                            null,
                            null,
                            numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv);
                        break;
                    case "kozepiskolaanyakonyv":
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
            feltoltTrueModFalse = true;
            labelMenuKat.Text = selectedCategory.categoryPrettyName;
            TogglePanelMenu();
        }
        private void ButtonTorles_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"id: {dataGridView1.SelectedCells[0].Value.ToString()} sqltable name:{selectedCategory.sqlTableName}, path: {selectedCategory.relativePath}");
            Controll.Torles(
                dataGridView1.SelectedCells[0].Value.ToString(),
                selectedCategory.sqlTableName,
                selectedCategory.relativePath
                );
            UpdateResultSet();
        }


        //Oldalsó panel menü események
        private void ButtonTalloz_Click(object sender, EventArgs e)
        {
            MessageBox.Show("", selectedCategory.textBoxFileNameFeltolt.Name);
            Controll.tallozas(selectedCategory.textBoxFileNameFeltolt);
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
            textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.BackColor = Controll.CheckTbTextLength(textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Text);
        }
        private void textBoxAnyjaNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.BackColor = Controll.CheckTbTextLength(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Text);
        }
        private void textBoxFileNameFeltolt_TextChanged(object sender, EventArgs e)
        {
            textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.BackColor = Controll.CheckTbTextLength(textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Text);
        }


        //Kategória választó gombok esemémnyei
        private void buttonErettsegiTorzslap_Click(object sender, EventArgs e)
        {
            selectedCategory = new ErettsegiTorzslap();
            selectedCategory.panelFeltolt = panelErettsegiTorzslapFeltolt;
            selectedCategory.textBoxFileNameFeltolt = textBoxFileNameFeltoltErettsegiTorzslap;
            SetAndResetButtonColors(buttonErettsegiTorzslap);
            SetDatagridViewColumns();
            ButtonKeresClick();
            UpdateResultSet();
        }
        private void buttonErettsegiTanusitvany_Click(object sender, EventArgs e)
        {
            selectedCategory = new ErettsegiTanusitvany();
            selectedCategory.panelFeltolt = panelErettsegiTanusitvanyFeltolt;
            selectedCategory.textBoxFileNameFeltolt = textBoxFileNameFeltoltErettsegiTanusitvany;

            SetAndResetButtonColors(buttonErettsegiTanusitvany);
            SetDatagridViewColumns();
            ButtonKeresClick();
            UpdateResultSet();
        }
        private void buttonSzakmaiVizsgaTorzslap_Click(object sender, EventArgs e)
        {
            selectedCategory = new SzakmaiVizsgaTorzslap();
            selectedCategory.panelFeltolt = panelSzakmaiVizsgaTorzslapFeltolt;
            selectedCategory.textBoxFileNameFeltolt = textBoxFileNameFeltoltSzakmaiVizsgaTorzslap;

            SetAndResetButtonColors(buttonSzakmaiVizsgaTorzslap);
            SetDatagridViewColumns();
            ButtonKeresClick();
            UpdateResultSet();
        }
        private void buttonSzakmaiVizsgaAnyakonyv_Click(object sender, EventArgs e)
        {
            selectedCategory = new SzakmaiVizsgaAnyakonyv();
            selectedCategory.panelFeltolt = panelSzakmaiVizsgaAnyakonyvFeltolt;
            selectedCategory.textBoxFileNameFeltolt = textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv;

            SetAndResetButtonColors(buttonSzakmaiVizsgaAnyakonyv);
            SetDatagridViewColumns();
            ButtonKeresClick();
            UpdateResultSet();
        }
        private void buttonKozepiskolaAnyakonyv_Click(object sender, EventArgs e)
        {
            selectedCategory = new KozepiskolaiAnyakonyv();
            selectedCategory.panelFeltolt = panelKozepiskolaAnyakonyvFeltolt;
            selectedCategory.textBoxFileNameFeltolt = textBoxFileNameFeltoltKozepsikolaAnyakonyv;

            SetAndResetButtonColors(buttonKozepiskolaAnyakonyv);
            SetDatagridViewColumns();
            ButtonKeresClick();
            UpdateResultSet();
        }

        public void SetDatagridViewColumns()
        {
            dataGridView1.ColumnCount = selectedCategory.sqlColumns.Count;
            int columnIndex = 0;

            foreach (string columnName in selectedCategory.sqlColumns)
            {
                dataGridView1.Columns[columnIndex++].Name = columnName;
            }

            dataGridView1.Columns[0].Width = 35;
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
            selectedCategory.OpenFile(dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString());
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
            //
            Controll.dataGridViewClear(dataGridView1);
            //
            Controll.buttonClickClear(
                            textBoxTanuloNeveKeres, 
                            textBoxanyjaNeveKeres, 
                            numericUpDownVizsgaÉveKeres,
                            checkBoxVizsgaEve
                );
            labelMenuKat.Text = Controll.globSelectedButton;
            ShowPanel(selectedCategory.panelFeltolt);
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
            from = selectedCategory.sqlTableName;
            row1ErreKeres = selectedCategory.row1Spec;
            row2EztIsKiir = selectedCategory.row2Spec;
            //selectedCategory.sqlTableName
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
            Controll.Set6PanelsVisibility(panelTallozMentesujButton,
                panelSzakmaiVizsgaTorzslapFileName,
                panelKozepiskolaAnyakonyvFilneName,
                panelErettsegiTanusitvanyFileName,
                panelErettsegiTtorzslapFileName,
                panelSzakmaivizsgaAnyakonyvFileName,
                true);
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
            panelMenu.Left = -panelMenu.Width;
            panelMenu.Visible = false;
            panelFeltolt.Visible = false;
            panelModTorol.Visible = true;
            SetAndResetButtonColors(buttonFeltoltes);

            CleanupForm();
        }
        private void SaveChanges()
        {
            if (feltoltTrueModFalse)
            {
                //MessageBox.Show("Global fullpath" + Global.fileStorageRelativePath);
                //MessageBox.Show("Global feltoltendo" + Global.globFeltoltendoFileEleresiUt);

                switch (selectedCategory.sqlTableName)
                {
                    case "szakmaivizsgatorzslap":
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

                    case "erettsegitorzslap":
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

                    case "erettsegitanusitvany":
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

                    case "szakmaivizsgaanyakonyv":
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

                    case "kozepiskolaanyakonyv":
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
            else if (!feltoltTrueModFalse)
            {
                switch (selectedCategory.sqlTableName)
                {
                    case "szakmaivizsgaTorzslap":
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
                    case "erettsegitorzslap":
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
                    case "erettsegitanusitvany":
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
                    case "szakmaivizsgaanyakonyv":
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
                    case "kozepiskolaanyakonyv":
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
            switch (selectedCategory.sqlTableName)
            {
                case "szakmaivizsgaTorzslap":
                    Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                        textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                        numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap, null,
                        radioButtonOszFeltoltSzakmaiVizsgaTorzslap, radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                        null, textBoxFileNameFeltoltSzakmaiVizsgaTorzslap);
                        break;
                case "erettsegitorzslap":
                    Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                        textBoxTanuloNeveFeltoltErettsegiTorzslap,
                        numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                        null,
                        radioButtonOszFeltoltErettsegiTorzslap,
                        radioButtonTavaszFeltoltErettsegiTorzslap,
                        null,
                        textBoxFileNameFeltoltErettsegiTorzslap);
                    break;

                case "erettsegitanusitvany":
                    Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                        textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                        numericUpDownVizsgaEveFeltoltErettsegiTanusitvany, null, null, null,
                        textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                        textBoxFileNameFeltoltErettsegiTanusitvany);
                    break;

                case "szakmaivizsgaanyakonyv":
                    Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                        textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                        numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                        numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                        null,
                        null,
                        null,
                        textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv);
                    break;

                case "kozepiskolaanyakonyv":
                    Controll.FeltoltUrit(textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                        textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                        numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                        numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                        null,
                        null,
                        null,
                        textBoxFileNameFeltoltKozepsikolaAnyakonyv);
                    break;
            }
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

        public void ShowPanel(Panel choosenPanel)
        {
            foreach (Panel panel in panels)
            {
                panel.Visible = panel == choosenPanel;
            }
        }
    }
}
