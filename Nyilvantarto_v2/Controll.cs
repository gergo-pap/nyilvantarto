using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nyilvantarto_v2
{
  class Controll
    {
        public static string FileStorageRelativePath;
        public static string GlobKiterjesztes;
        public static string GlobSelectedButton;
        public static string GlobFeltoltendoFileEleresiUt;
        public static List<string> FoldersToDeleteList = new List<string>();


        private static readonly char[] SpecialChars = "!@#$%^&*()".ToCharArray();

        //Directiories
        public static void CreateDirectiories(string labelMentesiHely)
        {
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Középiskola\Anyakönyv\");
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Szakmai Vizsga\Anyakönyv\");
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Szakmai Vizsga\Törzslap\");
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Érettségi\Törzslap\");
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Érettségi\Tanusítvány\");
            FileStorageRelativePath = labelMentesiHely;
        }
        public static void CheckDirs(GroupBox groupBoxEleresi, Label labelMentesiHely, Panel panel, string varString)
        {
	        var path = Database.GetFileStoragePathFromDb(varString);
	        HandleResultCheckDirs(groupBoxEleresi, labelMentesiHely, panel, path);
        }

        private static void HandleResultCheckDirs(GroupBox groupBoxEleresi, Label labelMentesiHely, Panel panel, object result)
        {
            if (result == null)
            {
                groupBoxEleresi.Visible = true;
                panel.Visible = false;
            }
            else
            {
                var path = result.ToString();
                labelMentesiHely.Text = path;
                groupBoxEleresi.Visible = false;
                panel.Visible = true;
                LoadFileStorageRelativePath();
                // fileStorageRelativePath = labelMentesiHely.Text;
            }
        }

        public static void DeleteDirsInTemp()
        {
            foreach (var mappa in FoldersToDeleteList)
            {
                Directory.Delete(mappa, recursive: true);
                MessageBox.Show("Mappa törlése:  " + mappa);
            }
        }


        //Database függvények




        //Datagridview függvények
        public static void DataGridViewBasicSettings(DataGridView dataGridView, Panel panelKeres)
        {
            //Global.dataGridViewKeresesEredmenyeiClear(dataGridView1);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            MakeDatagridvievPanelKeresVisible(dataGridView, panelKeres);
        }

        private static void MakeDatagridvievPanelKeresVisible(DataGridView dataGridView, Panel panelKeres)
        {
            if (!dataGridView.Visible || !panelKeres.Visible)
            {
                dataGridView.Visible = true;
                panelKeres.Visible = true;
            }
        }

        public static void DataGridViewClear(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
        }
        public static void DatagridViewKeres(
                                    string rowTneve,
                                    string rowVkezdet,
                                    string rowExtra,
                                    string rowAnyja,
                                    string from,
                                    string likeTanuloNeve,
                                    string likeAnyjaNeve,
                                    string likeVkezdet,
                                    DataGridView dataGridView,
                                    bool checkBoxChecked,
                                    int db
        )
        {
            if (!checkBoxChecked)
            {
                likeVkezdet = "";
            }

            MySqlCommand cmd = Database.GetSearchResultFromDb(rowVkezdet, rowExtra, @from, likeTanuloNeve, likeAnyjaNeve, likeVkezdet);

            FillDataGridViewWithData(rowTneve, rowVkezdet, rowExtra, rowAnyja, dataGridView, db, cmd);
        }

        private static void FillDataGridViewWithData(
                                                    string rowTneve,
                                                    string rowVkezdet,
                                                    string rowExtra,
                                                    string rowAnyja,
                                                    DataGridView dataGridView,
                                                    int db,
                                                    MySqlCommand cmd
        )
        {
            using (var reader = cmd.ExecuteReader())
            {
                var id = GetOriginalData(
                    rowTneve,
                    rowVkezdet,
                    rowExtra,
                    rowAnyja,
                    reader,
                    out var rowTneveData,
                    out var rowVkezdetData,
                    out var rowAnyjaData,
                    out var rowExtraData
                );
                int i = 0;
                while (reader.Read() && i < db)
                {
                    var id2 = GetValuesFromOriginalData(
                        reader,
                        id,
                        rowTneveData,
                        rowVkezdetData,
                        rowAnyjaData,
                        rowExtraData,
                        out var rowTneveData2,
                        out var rowVkezdetData2,
                        out var rowAnyjaData2,
                        out var rowExtraData2
                        );

                    dataGridView.Rows.Add(id2, rowTneveData2, rowAnyjaData2, rowVkezdetData2, rowExtraData2);
                    i++;
                }
            }
        }

        private static string GetValuesFromOriginalData(
                                        MySqlDataReader reader,
                                        int id,
                                        int rowTneveData,
                                        int rowVkezdetData,
                                        int rowAnyjaData,
                                        int rowExtraData,
                                        out string rowTneveData2,
                                        out string rowVkezdetData2,
                                        out string rowAnyjaData2,
                                        out string rowExtraData2
        )
        {
            var id2 = reader.GetValue(id).ToString();
            rowTneveData2 = reader.GetValue(rowTneveData).ToString();
            rowVkezdetData2 = reader.GetValue(rowVkezdetData).ToString();
            rowAnyjaData2 = reader.GetValue(rowAnyjaData).ToString();
            rowExtraData2 = reader.GetValue(rowExtraData).ToString();
            if (rowExtraData2 == "True" || rowExtraData2 == "False")
            {
                rowExtraData2 = rowExtraData2 == "True" ? "Tavasz" : "Ősz";
            }

            return id2;
        }

        private static int GetOriginalData(
                                            string rowTneve,
                                            string rowVkezdet,
                                            string rowExtra,
                                            string rowAnyja,
                                            MySqlDataReader reader,
                                            out int rowTneveData,
                                            out int rowVkezdetData,
                                            out int rowAnyjaData,
                                            out int rowExtraData
        )
        {
            var id = reader.GetOrdinal("id");
            rowTneveData = reader.GetOrdinal(rowTneve);
            rowVkezdetData = reader.GetOrdinal(rowVkezdet);
            rowAnyjaData = reader.GetOrdinal(rowAnyja);
            rowExtraData = reader.GetOrdinal(rowExtra);
            return id;
        }

        public static void DataGridViewOffline(DataGridView dataGridView, Panel panel1, Panel panel2)
        {
            dataGridView.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
        }
        



        //Feltölt, töröl, módosít
        



        public static void CopyFile(string hovaMasolja, int returnValue)
        {
	        if (hovaMasolja.IndexOfAny(SpecialChars) != -1)
	        {
		        MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*\nMásolás nem sikerült!");
            return;
	        }
	        else
	        {
		        string destination = FileStorageRelativePath + hovaMasolja + returnValue + ".dat";
		        File.Copy(GlobFeltoltendoFileEleresiUt, destination);
		        Console.WriteLine("Sikeres File mozgatás");
	        }
	        
        }

        



        

        



        public static void Torles(string id, string from, string destination)
        {
            string torlendo = $@"{FileStorageRelativePath}{destination}\{id}.dat";
            try
            {
                string sql = "DELETE FROM " +
                             $"{from} " +
                             $"WHERE " +
                             $"id = @{id}"
                    ;
                var cmd = Database.CreateCommand(sql);
                cmd.Parameters.AddWithValue($"@{from}", from);
                cmd.Parameters.AddWithValue($"@{id}", id);

                cmd.ExecuteNonQuery();
                MessageBox.Show($"Torlendo: {torlendo}");
                File.Delete(torlendo);

                MessageBox.Show("Sikeres törlés");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
        }
        public static void Modositas(
                                    string textBoxAnyjaNeveModositas,
                                    string textBoxNevModositas,
                                    RadioButton radioButtonTavaszModosit,
                                    string tbOther,
                                    int numericUpDownEvKezdetModositas,
                                    NumericUpDown numericUpDown2,
                                    string update,
                                    string rowVkezdet,
                                    string rowTavaszVosz,
                                    string rowId
        )
        {
            int tavaszVosz = 0;

            tavaszVosz = SetTavaszOrOszValue(radioButtonTavaszModosit, tbOther, numericUpDown2, tavaszVosz);

            if (!PathsContainSpecChars(textBoxAnyjaNeveModositas, textBoxNevModositas))
            {
                try
                {
                    var sql = Database.SetSqlCommandUpdate(
                                                        textBoxAnyjaNeveModositas,
                                                        textBoxNevModositas,
                                                        numericUpDownEvKezdetModositas,
                                                        update,
                                                        rowVkezdet,
                                                        rowTavaszVosz,
                                                        rowId,
                                                        tavaszVosz
                    );
                    var cmd = Database.CreateCommand(sql);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Sikeres módosítás");
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Nincs kijelölve semmi!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Egyéb hiba! " + e);
                }
            }
            else
            {
                MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*");
            }
        }

        

        private static int SetTavaszOrOszValue(
                                                RadioButton radioButtonTavaszModosit,
                                                string tbOther,
                                                NumericUpDown numericUpDown2,
                                                int tavaszVosz
        )
        {
            if (radioButtonTavaszModosit != null && radioButtonTavaszModosit.Checked)
            {
                tavaszVosz = 1;
            }
            else if (radioButtonTavaszModosit != null)
            {
                tavaszVosz = 0;
            }

            if (tbOther != null)
            {
                tavaszVosz = int.Parse(tbOther);
            }

            if (numericUpDown2 != null)
            {
                tavaszVosz = int.Parse(numericUpDown2.Value.ToString());
            }

            return tavaszVosz;
        }

        public static void LoadSelectedDataWhenModifying(
                                                        DataGridView dataGridView,
                                                        TextBox tbAnyja,
                                                        TextBox tbTan,
                                                        NumericUpDown numericUpDown,
                                                        TextBox tb,
                                                        RadioButton radioTavaszTrue,
                                                        RadioButton radioOszTrue,
                                                        NumericUpDown numeric
        )
        {
            tbTan.Text = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            tbAnyja.Text = dataGridView.SelectedRows[0].Cells[2].Value.ToString();
            numericUpDown.Value = decimal.Parse(dataGridView.SelectedRows[0].Cells[3].Value.ToString());
            if (tb != null)
            {
                tb.Text = dataGridView.SelectedRows[0].Cells[4].Value.ToString();
            }
            else if (radioTavaszTrue != null)
            {
                if (dataGridView.SelectedRows[0].Cells[4].Value.ToString() == "Tavasz")
                {
                    radioTavaszTrue.Checked = true;
                }
                else
                {
                    radioOszTrue.Checked = true;
                }
            }
            else if (numeric != null)
            {
                numeric.Value = decimal.Parse(dataGridView.SelectedRows[0].Cells[4].Value.ToString());
            }
        }



        //File keresése
        public static void SearchFileInFileExplorer(string specDir, string id, string tableName)
        {
            var originalFileName = GetOriginalFileName(id, tableName);
            //MessageBox.Show($"original filename from db: {originalFileName}");
            string filePathInDb = $"{FileStorageRelativePath + specDir}\\{id}.dat";
            if (CheckIfFileExists(filePathInDb)) return;
            string tempDirectory = CreateTempDir();
            string filePathInTemp = Path.Combine(tempDirectory, originalFileName);
            try
            {
                File.Copy(filePathInDb, filePathInTemp);
                Process.Start(filePathInTemp);
                FoldersToDeleteList.Add(tempDirectory);
            }

            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
        }

        private static string CreateTempDir()
        {
	        string tempDirectory = Path.Combine(Path.GetTempPath(),
		        "nyilvantarto_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
	        Directory.CreateDirectory(tempDirectory);
	        return tempDirectory;
        }

        private static bool CheckIfFileExists(string filePathInDb)
        {
            if (!File.Exists(filePathInDb))
            {
                MessageBox.Show("Nincs meg a File!" + filePathInDb);
                return true;
            }

            return false;
        }

        private static string GetOriginalFileName(string id, string tableName)
        {
            var cmd = Database.CreateCommand($"SELECT filename FROM {tableName} WHERE id = @id");
            cmd.Parameters.AddWithValue("@id", id);
            var originalFileName = cmd.ExecuteScalar().ToString();
            return originalFileName;
        }

        //Ellenörző függvények
        public static Color CheckTbTextLength(string  text)
        {
            if (text.Length == 0)
            {
                return Color.Red;
            }
            else
            {
                return Color.White;
            }
        }
        public static bool PathsContainSpecChars(string s1, string s2)
        {
            return s1.IndexOfAny(SpecialChars) != -1 || s2.IndexOfAny(SpecialChars) != -1;
        }

        public static bool CheckIfEmptyInput4TextBox(TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4)
        {
            if (tb1.Text.Length == 0)
            {
                tb1.BackColor = Color.Red;
                return false;
            }
            if (tb2.Text.Length == 0)
            {
                tb2.BackColor = Color.Red;
                return false;
            }
            if (tb3.Text.Length == 0)
            {
                tb3.BackColor = Color.Red;
                return false;
            }
            if (tb4.Text.Length == 0)
            {
                tb4.BackColor = Color.Red;
                return false;
            }
            return true;
        }


        //Panel visibility függvények
        public static void Set6PanelsVisibility(Panel p1, Panel p2, Panel p3, Panel p4, Panel p5, Panel p6, bool trueOrFalse)
        {
            p1.Visible = trueOrFalse;
            p2.Visible = trueOrFalse;
            p3.Visible = trueOrFalse;
            p4.Visible = trueOrFalse;
            p5.Visible = trueOrFalse;
            p6.Visible = trueOrFalse;
        }

        public static void LoadFileStorageRelativePath()
        {
            MySqlCommand cmd = Database.CreateCommand("Select value From settings Where var = @var");
            cmd.Parameters.AddWithValue("@var", "eleresiUt");
            FileStorageRelativePath = cmd.ExecuteScalar().ToString();
            cmd.Parameters.Clear();
            //MessageBox.Show("Elérési út: " + fileStorageRelativePath);
        }
        public static void Tallozas(TextBox textBoxFilename)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                MessageBox.Show("Hiba");
            }
            else
            {
                string fileNameWExtension = Path.GetFileName(openFileDialog.FileName);

                GlobKiterjesztes = Path.GetExtension(fileNameWExtension);
                textBoxFilename.Text = fileNameWExtension;

                GlobFeltoltendoFileEleresiUt = openFileDialog.FileName;
            }
        }
        public static void ClearSearchValues(
                                            TextBox textBoxTanuloNeve,
                                            TextBox textBoxAnyjaNeve,
                                            NumericUpDown numericUpDown,
                                            CheckBox checkBox
        )
        {
            textBoxTanuloNeve.Clear();
            textBoxAnyjaNeve.Clear();
            numericUpDown.ResetText();
            checkBox.Checked = false;
        }
        public static void ClearUploadedValues(
                                                TextBox t1,
                                                TextBox t2,
                                                NumericUpDown n1,
                                                NumericUpDown n2,
                                                RadioButton rTrue,
                                                RadioButton rFalse,
                                                TextBox t3,
                                                TextBox t4
        )
        {
            t1.Clear();
            t2.Clear();
            t4.Clear();
            n1.Value = 1904;
            if (n2 != null)
            {
                n2.Value = 1904;
            }
            if (rTrue != null)
            {
                rTrue.Checked = true;
            }
            if (rFalse != null)
            {
                rFalse.Checked = false;
            }
            t3?.Clear();        //if null --> clear
        }
        public static void Tallozas(Label labelMentesiHely)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath;
                labelMentesiHely.Text = sSelectedPath;
            }
            else
            {
                MessageBox.Show("Hiba");
            }
        }

        public static void FirstClickShow(
                                        Panel panel1,
                                        Panel panel2,
                                        DataGridView dataGridView
        )
        {
            if (!panel1.Visible || !panel2.Visible)
            {
                panel1.Visible = true;
                panel2.Visible = true;
                dataGridView.Visible = true;
            }
        }
    }
}
