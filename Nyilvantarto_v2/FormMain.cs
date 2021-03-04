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
        private List<Button> _buttons;
        private List<Panel> _panels;
        private Category _selectedCategory;
        private bool _feltoltTrueModFalse;

        public FormMain()
        {
            InitializeComponent();

            _buttons = new List<Button>
            {
                buttonErettsegiTanusitvany,
                buttonKozepiskolaAnyakonyv,
                buttonErettsegiTorzslap,
                buttonSzakmaiVizsgaTorzslap,
                buttonSzakmaiVizsgaAnyakonyv
            };


            _panels = new List<Panel>
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
            panelMenu.Left = -panelMenu.Width;

            if (!Database.CheckDB_Conn(true))
            {
                return;
            }

            LoadSetupCheck(sender, e);
        }

        private void LoadSetupCheck(object sender, EventArgs e)
        {
            Database.CreateTables();
            Controll.CheckDirs(groupBoxEleresi, labelMentesiHely, panelKeres, "eleresiUt");
            labelPath.Text = Controll.FileStorageRelativePath;
            Controll.DataGridViewBasicSettings(dataGridView1, panelKeres);
            buttonErettsegiTorzslap_Click(sender, e);
            Controll.FirstClickShow(panelFeltModTorl, panelKeres, dataGridView1);
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            SetControlPosition();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeleteTempDirs(e);
        }

        private static void DeleteTempDirs(FormClosingEventArgs e)
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
            Controll.Tallozas(labelMentesiHely);
            Database.SetPathInDb(labelMentesiHely.Text, groupBoxEleresi, "eleresiUt");
            Controll.CreateDirectiories(labelMentesiHely.Text);
            Controll.LoadFileStorageRelativePath();
        }


        //Feltölt, módosít, töröl gombok eseményei
        private void ButtonModositas_Click(object sender, EventArgs e)
        {
            _feltoltTrueModFalse = false;
            if (panelMenu.Visible)
            {
                HidePanelMenu();
            }
            else
            {
                labelMenuKat.Text = _selectedCategory.categoryPrettyName;
                ShowPanelMenu();
                buttonFeltoltes.Visible = false;
                buttonTorles.Visible = false;
                //MessageBox.Show(selectedCategory.sqlTableName);

                SwitchCaseSelectedCategoryMod();

                //UpdateResultSet();
            }
        }

        private void SwitchCaseSelectedCategoryMod()
        {
            switch (_selectedCategory.sqlTableName)
            {
                case "szakmaivizsgatorzslap":
                    ModSzakmaiVizsgaTorzslap();
                    break;
                case "erettsegitorzslap":
                    ModErettsegiTorzslap();
                    break;
                case "erettsegitanusitvany":
                    ModErettsegiTanusitvany();
                    break;
                case "szakmaivizsgaanyakonyv":
                    ModSzakmaivizsgaAnyakonyv();
                    break;
                case "kozepiskolaanyakonyv":
                    ModKozepiskolaAnyakonyv();
                    break;
            }
        }

        private void ModKozepiskolaAnyakonyv()
        {
            Controll.LoadSelectedDataWhenModifying(
                                                    dataGridView1,
                                                    textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                                    textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                                    numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                                                    null,
                                                    null,
                                                    null,
                                                    numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv
            );
        }

        private void ModSzakmaivizsgaAnyakonyv()
        {
            Controll.LoadSelectedDataWhenModifying(
                                                    dataGridView1,
                                                    textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                                                    textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                                    numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                                    null,
                                                    null,
                                                    null,
                                                    numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv
            );
        }

        private void ModErettsegiTanusitvany()
        {
            Controll.LoadSelectedDataWhenModifying(
                                                    dataGridView1,
                                                    textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                                    textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                                    numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                                    textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                                    null,
                                                    null,
                                                    null
            );
        }

        private void ModErettsegiTorzslap()
        {
            Controll.LoadSelectedDataWhenModifying(
                                                    dataGridView1,
                                                    textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                                    textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                                    numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                                                    null,
                                                    radioButtonTavaszFeltoltErettsegiTorzslap,
                                                    radioButtonOszFeltoltErettsegiTorzslap,
                                                    null
            );
        }

        private void ModSzakmaiVizsgaTorzslap()
        {
            Controll.LoadSelectedDataWhenModifying(
                                                    dataGridView1,
                                                    textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                                                    textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                                                    numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                                                    null,
                                                    radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                                                    radioButtonOszFeltoltSzakmaiVizsgaTorzslap,
                                                    null
            );
        }

        private void ButtonFeltoltes_Click(object sender, EventArgs e)
        {
            _feltoltTrueModFalse = true;
            labelMenuKat.Text = _selectedCategory.categoryPrettyName;
            TogglePanelMenu();
        }

        private void ButtonTorles_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"id: {dataGridView1.SelectedCells[0].Value} sqltable name:{selectedCategory.sqlTableName}, path: {selectedCategory.relativePath}");
            Controll.Torles(
                        dataGridView1.SelectedCells[0].Value.ToString(),
                        _selectedCategory.sqlTableName,
                        _selectedCategory.relativePath
            );
            UpdateResultSet();
        }


        //Oldalsó panel menü események
        private void ButtonTalloz_Click(object sender, EventArgs e)
        {
            Controll.Tallozas(_selectedCategory.textBoxFileNameFeltolt);
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
            CleanupForm();
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
            textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.BackColor =
                Controll.CheckTbTextLength(textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Text);
        }

        private void textBoxAnyjaNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.BackColor =
                Controll.CheckTbTextLength(textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Text);
        }

        private void textBoxFileNameFeltolt_TextChanged(object sender, EventArgs e)
        {
            textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.BackColor =
                Controll.CheckTbTextLength(textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Text);
        }


        //Kategória választó gombok esemémnyei
        private void buttonErettsegiTorzslap_Click(object sender, EventArgs e)
        {
            _selectedCategory = new ErettsegiTorzslap
            {
                panelFeltolt = panelErettsegiTorzslapFeltolt,
                textBoxFileNameFeltolt = textBoxFileNameFeltoltErettsegiTorzslap
            };
            SetAndResetButtonColors(buttonErettsegiTorzslap);

            CategorySelected();
        }


        private void buttonErettsegiTanusitvany_Click(object sender, EventArgs e)
        {
            _selectedCategory = new ErettsegiTanusitvany
            {
                panelFeltolt = panelErettsegiTanusitvanyFeltolt,
                textBoxFileNameFeltolt = textBoxFileNameFeltoltErettsegiTanusitvany
            };
            SetAndResetButtonColors(buttonErettsegiTanusitvany);

            CategorySelected();
        }

        private void buttonSzakmaiVizsgaTorzslap_Click(object sender, EventArgs e)
        {
            _selectedCategory = new SzakmaiVizsgaTorzslap
            {
                panelFeltolt = panelSzakmaiVizsgaTorzslapFeltolt,
                textBoxFileNameFeltolt = textBoxFileNameFeltoltSzakmaiVizsgaTorzslap
            };
            SetAndResetButtonColors(buttonSzakmaiVizsgaTorzslap);

            CategorySelected();
        }

        private void buttonSzakmaiVizsgaAnyakonyv_Click(object sender, EventArgs e)
        {
            _selectedCategory = new SzakmaiVizsgaAnyakonyv
            {
                panelFeltolt = panelSzakmaiVizsgaAnyakonyvFeltolt,
                textBoxFileNameFeltolt = textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv
            };
            SetAndResetButtonColors(buttonSzakmaiVizsgaAnyakonyv);

            CategorySelected();
        }

        private void buttonKozepiskolaAnyakonyv_Click(object sender, EventArgs e)
        {
            _selectedCategory = new KozepiskolaiAnyakonyv
            {
                panelFeltolt = panelKozepiskolaAnyakonyvFeltolt,
                textBoxFileNameFeltolt = textBoxFileNameFeltoltKozepsikolaAnyakonyv
            };
            SetAndResetButtonColors(buttonKozepiskolaAnyakonyv);

            CategorySelected();
        }

        private void CategorySelected()
        {
            SetDatagridViewColumns();
            ClearAndShow();
            UpdateResultSet();
        }

        public void SetDatagridViewColumns()
        {
            dataGridView1.ColumnCount = _selectedCategory.sqlColumns.Count;
            int columnIndex = 0;

            foreach (string columnName in _selectedCategory.sqlColumns)
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
            ResetVizsgaEveValue();
            UpdateResultSet();
        }

        private void ResetVizsgaEveValue()
        {
            if (!checkBoxVizsgaEve.Checked)
            {
                numericUpDownVizsgaÉveKeres.Controls[1].Text = "";
            }

            else
            {
                numericUpDownVizsgaÉveKeres.Controls[1].Text = "1900";
            }
        }

        private void NumericUpDownTalalatokSzama_ValueChanged(object sender, EventArgs e)
        {
            UpdateResultSet();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _selectedCategory.OpenFile(GetIdFromDataGridView(dataGridView1));
        }


        //Timer tick
        private void updateDbStateTimer_Tick(object sender, EventArgs e)
        {
            if (Database.CheckDB_Conn(false))
            {
                ModifyLabelKapcsolatAdatbazissal("aktív", Color.LightGreen);
                Controll.DataGridViewBasicSettings(dataGridView1, panelKeres);
            }
            else
            {
                ModifyLabelKapcsolatAdatbazissal("offline", Color.Red);
                Controll.DataGridViewOffline(dataGridView1, panelKeres, panelFeltModTorl);
            }
        }

        private void ModifyLabelKapcsolatAdatbazissal(string text, Color color)
        {
            labelKapcsolatAdatbazissal.Text = text;
            labelKapcsolatAdatbazissal.BackColor = color;
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
        }

        private void ClearAndShow()
        {
            Controll.DataGridViewClear(dataGridView1);
            Controll.ClearSearchValues(
                                        textBoxTanuloNeveKeres,
                                        textBoxanyjaNeveKeres,
                                        numericUpDownVizsgaÉveKeres,
                                        checkBoxVizsgaEve
            );
            labelMenuKat.Text = Controll.GlobSelectedButton;
            ShowPanel(_selectedCategory.panelFeltolt);
            if (!panelFeltModTorl.Visible)
            {
                panelFeltModTorl.Visible = true;
            }
        }

        private void UpdateResultSet()
        {
            CalculateSearchConditions(out var from, out var row1ErreKeres, out var row2EztIsKiir);

            Controll.DataGridViewClear(dataGridView1);
            Controll.DatagridViewKeres(
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
            from = _selectedCategory.sqlTableName;
            row1ErreKeres = _selectedCategory.row1Spec;
            row2EztIsKiir = _selectedCategory.row2Spec;
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
            Controll.Set6PanelsVisibility(
                                        panelTallozMentesujButton,
                                        panelSzakmaiVizsgaTorzslapFileName,
                                        panelKozepiskolaAnyakonyvFilneName,
                                        panelErettsegiTanusitvanyFileName,
                                        panelErettsegiTtorzslapFileName,
                                        panelSzakmaivizsgaAnyakonyvFileName,
                                        true
            );
            MovePanelMenu();
        }

        private void MovePanelMenu()
        {
            panelMenu.BringToFront();
            panelMenu.Visible = true;

            while (panelMenu.Left < -10)
            {
                panelMenu.Left += Math.Max(1, (int) ((double) Math.Abs(panelMenu.Left) * 0.25));
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
            switch (_feltoltTrueModFalse)
            {
                case true:
                    SwitchCaseSaveCategories();
                    break;
                case false:
                    SwitchCaseModifyCategories();
                    break;
            }

            UpdateResultSet();
        }

        private void SwitchCaseModifyCategories()
        {
            switch (_selectedCategory.sqlTableName)
            {
                case "szakmaivizsgaTorzslap":
                    Controll.Modositas(
                                        textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Text,
                                        textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Text,
                                        radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                                        null,
                                        GetValueFromNumericUpDown(numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap),
                                        null,
                                        "szakmaivizsgaTorzslap",
                                        "vizsgaEvVeg",
                                        "vizsgaTavasz1Osz0",
                                        GetIdFromDataGridView(dataGridView1)
                    );
                    break;
                case "erettsegitorzslap":
                    Controll.Modositas(
                                        textBoxAnyjaNeveFeltoltErettsegiTorzslap.Text,
                                        textBoxTanuloNeveFeltoltErettsegiTorzslap.Text,
                                        radioButtonTavaszFeltoltErettsegiTorzslap,
                                        null,
                                        GetValueFromNumericUpDown(numericUpDownVizsgaEveFeltoltErettsegiTorzslap),
                                        null,
                                        "erettsegitorzslap",
                                        "vizsgaEvVeg",
                                        "vizsgaTavasz1Osz0",
                                        GetIdFromDataGridView(dataGridView1)
                    );
                    break;
                case "erettsegitanusitvany":
                    Controll.Modositas(
                                        textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Text,
                                        textBoxTanuloNeveFeltoltErettsegiTanusitvany.Text,
                                        null,
                                        textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Text,
                                        GetValueFromNumericUpDown(numericUpDownVizsgaEveFeltoltErettsegiTanusitvany),
                                        null,
                                        "erettsegitanusitvany",
                                        "vizsgaEvVeg",
                                        "tanuloiAzonosito",
                                        GetIdFromDataGridView(dataGridView1)
                    );
                    break;
                case "szakmaivizsgaanyakonyv":
                    Controll.Modositas(
                                        textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Text,
                                        textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Text,
                                        null,
                                        null,
                                        GetValueFromNumericUpDown(numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv),
                                        numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                        "szakmaivizsgaanyakonyv",
                                        "vizsgaEvKezdet",
                                        "vizsgaEvVeg",
                                        GetIdFromDataGridView(dataGridView1)
                    );
                    break;
                case "kozepiskolaanyakonyv":
                    Controll.Modositas(
                                        textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Text,
                                        textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Text,
                                        null,
                                        null,
                                        GetValueFromNumericUpDown(numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv),
                                        numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                        "kozepiskolaanyakonyv",
                                        "vizsgaEvKezdet",
                                        "vizsgaEvVeg",
                                        GetIdFromDataGridView(dataGridView1)
                    );
                    break;
            }
        }

        private int GetValueFromNumericUpDown(NumericUpDown numeric)
        {
            return int.Parse(numeric.Value.ToString());
        }

        private string GetIdFromDataGridView(DataGridView gridView)
        {
            return gridView.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void SwitchCaseSaveCategories()
        {
            switch (_selectedCategory.sqlTableName)
            {
                case "szakmaivizsgatorzslap":
                    SaveSzakmaivizsgaTorzslap();
                    break;

                case "erettsegitorzslap":
                    SaveErettsegiTorzslap();
                    break;

                case "erettsegitanusitvany":
                    SaveErettsegitanusitvany();
                    break;

                case "szakmaivizsgaanyakonyv":
                    SaveSzakmaivizsgaAnyakonyv();
                    break;

                case "kozepiskolaanyakonyv":
                    SaveKozepiskolaAnyakonyv();
                    break;
            }
        }

        private void SaveKozepiskolaAnyakonyv()
        {
            if (Controll.CheckIfEmptyInput4TextBox(
                                                    textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                                    textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                                    textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                                    textBoxFileNameFeltoltKozepsikolaAnyakonyv)
            )
            {
                FileFeltolteseBDreKozepiskolaAnyakonyv();
                Controll.CopyFile(@"\Adatok\Középiskola\Anyakönyv\", Database.GetLastIdFromDb("kozepiskolaanyakonyv"));

                Controll.ClearUploadedValues(
                                            textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                            textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                            numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                            numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                                            null,
                                            null,
                                            null,
                                            textBoxFileNameFeltoltKozepsikolaAnyakonyv
                );
            }
        }

        private void SaveSzakmaivizsgaAnyakonyv()
        {
            if (Controll.CheckIfEmptyInput4TextBox(
                                                    textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                                                    textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                                    textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                                    textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv)
            )
            {
                FileFeltolteseBDreSzakmaivizsgaAnyakonyv();
                Controll.CopyFile(@"\Adatok\Szakmai Vizsga\Anyakönyv\",
                    Database.GetLastIdFromDb("szakmaivizsgaanyakonyv"));

                Controll.ClearUploadedValues(
                                            textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                                            textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                            numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                            numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                            null,
                                            null,
                                            null,
                                            textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv
                );
            }
        }

        private void SaveErettsegitanusitvany()
        {
            if (Controll.CheckIfEmptyInput4TextBox(
                                                    textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                                    textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                                    textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                                    textBoxFileNameFeltoltErettsegiTanusitvany)
            )
            {
                FileFeltolteseBDreErettsegitanusitvany();
                Controll.CopyFile(@"\Adatok\Érettségi\Tanusítvány\", Database.GetLastIdFromDb("erettsegitanusitvany"));

                Controll.ClearUploadedValues(
                                            textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                            textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                            numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                            null,
                                            null,
                                            null,
                                            textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                            textBoxFileNameFeltoltErettsegiTanusitvany
                );
            }
        }

        private void SaveErettsegiTorzslap()
        {
            if (Controll.CheckIfEmptyInput4TextBox(
                                                    textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                                    textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                                    textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                                    textBoxFileNameFeltoltErettsegiTorzslap)
            )
            {
                FileFeltolteseBDreErettsegiTorzslap();
                Controll.CopyFile(@"\Adatok\Érettségi\Törzslap\", Database.GetLastIdFromDb("erettsegitorzslap"));

                Controll.ClearUploadedValues(
                                            textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                            textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                            numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                                            null,
                                            radioButtonOszFeltoltErettsegiTorzslap,
                                            radioButtonTavaszFeltoltErettsegiTorzslap,
                                            null,
                                            textBoxFileNameFeltoltErettsegiTorzslap
                );
            }
        }

        private void SaveSzakmaivizsgaTorzslap()
        {
            if (Controll.CheckIfEmptyInput4TextBox(
                                                    textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                                                    textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                                                    textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                                                    textBoxFileNameFeltoltSzakmaiVizsgaTorzslap)
            )
            {
                FileFeltolteseBDreSzakmaivizsgaTorzslap();
                Controll.CopyFile(@"\Adatok\Szakmai Vizsga\Törzslap\",
                    Database.GetLastIdFromDb("szakmaivizsgaTorzslap"));

                Controll.ClearUploadedValues(
                                            textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                                            textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                                            numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                                            null,
                                            radioButtonOszFeltoltSzakmaiVizsgaTorzslap,
                                            radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                                            null,
                                            textBoxFileNameFeltoltSzakmaiVizsgaTorzslap
                );
            }
        }

        private void FileFeltolteseBDreKozepiskolaAnyakonyv()
        {
            Database.FileFeltoltese(
                                    textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Text,
                                    textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Text,
                                    numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                    numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                                    "kozepiskolaanyakonyv",
                                    "vizsgaEvKezdet",
                                    "vizsgaEvVeg",
                                    "dokLegutobbModositva"
            );
        }

        private void FileFeltolteseBDreSzakmaivizsgaAnyakonyv()
        {
            Database.FileFeltoltese(
                                    textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Text,
                                    textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Text,
                                    numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                    numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                    "szakmaivizsgaanyakonyv",
                                    "vizsgaEvVeg",
                                    "vizsgaEvKezdet",
                                    "dokLegutobbModositva"
            );
        }

        private void FileFeltolteseBDreErettsegitanusitvany()
        {
            Database.FileFeltoltese(
                                    textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Text,
                                    textBoxTanuloNeveFeltoltErettsegiTanusitvany.Text,
                                    textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                    numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                    "erettsegitanusitvany",
                                    "vizsgaEvVeg",
                                    "tanuloiAzonosito",
                                    "dokLegutobbModositva"
            );
        }

        private void FileFeltolteseBDreErettsegiTorzslap()
        {
            Database.FileFeltoltese(
                                    textBoxAnyjaNeveFeltoltErettsegiTorzslap.Text,
                                    textBoxTanuloNeveFeltoltErettsegiTorzslap.Text,
                                    radioButtonOszFeltoltErettsegiTorzslap,
                                    numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                                    "erettsegitorzslap",
                                    "vizsgaEvVeg",
                                    "vizsgaTavasz1Osz0",
                                    "dokLegutobbModositva"
            );
        }

        private void FileFeltolteseBDreSzakmaivizsgaTorzslap()
        {
            Database.FileFeltoltese(
                                    textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Text,
                                    textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Text,
                                    radioButtonOszFeltoltSzakmaiVizsgaTorzslap,
                                    numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                                    "szakmaivizsgaTorzslap",
                                    "vizsgaEvVeg",
                                    "vizsgaTavasz1Osz0",
                                    "dokLegutobbModositva"
            );
        }

        private void CleanupForm()
        {
            switch (_selectedCategory.sqlTableName)
            {
                case "szakmaivizsgaTorzslap":
                    FeltoltUritSzakmaivizsgaTorzslap();
                    break;
                case "erettsegitorzslap":
                    FeltoltUritErettsegiTorzslap();
                    break;

                case "erettsegitanusitvany":
                    FeltoltUritErettsegiTanusitvany();
                    break;

                case "szakmaivizsgaanyakonyv":
                    FeltoltUritSzakmaivizsgaAnyakonyv();
                    break;

                case "kozepiskolaanyakonyv":
                    FeltoltUritKozepiskolaAnyakonyv();
                    break;
            }
        }

        private void FeltoltUritKozepiskolaAnyakonyv()
        {
            Controll.ClearUploadedValues(
                                        textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv,
                                        textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv,
                                        numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv,
                                        numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv,
                                        null,
                                        null,
                                        null,
                                        textBoxFileNameFeltoltKozepsikolaAnyakonyv
            );
        }

        private void FeltoltUritSzakmaivizsgaAnyakonyv()
        {
            Controll.ClearUploadedValues(
                                        textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv,
                                        textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt,
                                        numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv,
                                        numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv,
                                        null,
                                        null,
                                        null,
                                        textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv
            );
        }

        private void FeltoltUritErettsegiTanusitvany()
        {
            Controll.ClearUploadedValues(
                                        textBoxAnyjaNeveFeltoltErettsegiTanusitvany,
                                        textBoxTanuloNeveFeltoltErettsegiTanusitvany,
                                        numericUpDownVizsgaEveFeltoltErettsegiTanusitvany,
                                        null,
                                        null,
                                        null,
                                        textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany,
                                        textBoxFileNameFeltoltErettsegiTanusitvany
            );
        }

        private void FeltoltUritErettsegiTorzslap()
        {
            Controll.ClearUploadedValues(
                                        textBoxAnyjaNeveFeltoltErettsegiTorzslap,
                                        textBoxTanuloNeveFeltoltErettsegiTorzslap,
                                        numericUpDownVizsgaEveFeltoltErettsegiTorzslap,
                                        null,
                                        radioButtonOszFeltoltErettsegiTorzslap,
                                        radioButtonTavaszFeltoltErettsegiTorzslap,
                                        null,
                                        textBoxFileNameFeltoltErettsegiTorzslap
            );
        }

        private void FeltoltUritSzakmaivizsgaTorzslap()
        {
            Controll.ClearUploadedValues(
                                        textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap,
                                        textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap,
                                        numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap,
                                        null,
                                        radioButtonOszFeltoltSzakmaiVizsgaTorzslap,
                                        radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap,
                                        null,
                                        textBoxFileNameFeltoltSzakmaiVizsgaTorzslap
            );
        }

        public void SetAndResetButtonColors(Button activeButton)
        {
            foreach (Button button in _buttons)
            {
                SetButtonColor(activeButton, button);
            }
        }

        private void SetButtonColor(Button activeButton, Button button)
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

        public void ShowPanel(Panel choosenPanel)
        {
            foreach (Panel panel in _panels)
            {
                panel.Visible = panel == choosenPanel;
            }
        }
    }
}