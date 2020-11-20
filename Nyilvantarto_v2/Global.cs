using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Timers;
using System.Diagnostics;

namespace Nyilvantarto_v2
{
    class Global
    {
        public static string fullPath;
        public static string fixPath;
        public static string globNev;
        public static string globEvKezdet;
        public static string globAnyja;
        public static string globKiterjesztes;
        public static string globTavaszVOszString;
        public static int globTavaszVoszInt;
        public static bool globTavaszVoszTrueOrFalse;
        public static bool globIsaMessageBoxOpen;
        public static string globSelectedButton;
        public static string globFeltoltendoFileEleresiUt;
        public static List<string> torlendoMappak = new List<string>();
        private static MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");
        private static MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        private static readonly char[] SpecialChars = "!@#$%^&*()".ToCharArray();

        public static void gruopBoxSetDefaultVisibility(GroupBox gbOsszetett, GroupBox gbRandom, GroupBox groupBoxFeltoltott)
        {
            gbOsszetett.Visible = false;
            gbRandom.Visible = true;
            groupBoxFeltoltott.Visible = true;
        }

        public static void getDestPathFromDatabase(string folders, string eleresiUt)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            var command = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select value From settings Where var = @var";
            cmd.Parameters.AddWithValue("@var", eleresiUt);
            var result = cmd.ExecuteScalar();
            //fullPath = result.ToString() + folders;
            fixPath = result.ToString();
            cmd.Parameters.Clear();
            conn.Close();
            //MessageBox.Show("Elérési út: " + fullPath);
        }

        public static void getCount(Label l, Label l2, string pathEnd, string tableName)
        {
            getFilesCount(l, pathEnd);
            getDBCount(l2, tableName);
        }

        public static void getFilesCount(Label l, string destPath)
        {
            string[] fileArray = Directory.GetFiles(fixPath + destPath);
            l.Text = fileArray.Length.ToString();
        }

        public static void getDBCount(Label l, string from)
        {
            int count = 0;
            conn.Open();
            using (var cmd = new MySqlCommand($"SELECT COUNT(id) FROM {from}", conn))
            {
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            l.Text = count.ToString();
            conn.Close();
        }

        public static void checkMissingFiles(string destPath, string selectRow1, string selectRow2,
                                            string selectRow3, string selectRow4, string from, Label l1, Label l2)
        {
            List<string> dataOnPC = new List<string>();
            List<string> dataDB = new List<string>();

            string[] fileArray = Directory.GetFiles(fullPath);
            Console.Error.WriteLine(fullPath);
            foreach (var item in fileArray)
            {
                //MessageBox.Show(item.Split('\\').Last().Split('.').First());
                dataOnPC.Add(item.Split('\\').Last().Split('.').First());
            }

            var command = conn.CreateCommand();
            conn.Open();
            command.CommandText = $"SELECT {selectRow1}, {selectRow2}, {selectRow3}, {selectRow4} FROM {from}";
            using (var reader = command.ExecuteReader())
            {
                var row1 = reader.GetOrdinal(selectRow1);
                var row2 = reader.GetOrdinal(selectRow2);
                var row3 = reader.GetOrdinal(selectRow3);
                var row4 = reader.GetOrdinal(selectRow4);

                while (reader.Read())
                {
                    var row1G = reader.GetValue(row1).ToString();
                    var row2G = reader.GetValue(row2).ToString();
                    var row3G = reader.GetValue(row3).ToString();
                    var anyjaNeve2 = reader.GetValue(row4).ToString();
                    string oneDataDB = row1G + "_" + row2G + "_" + boolConvertToTavaszOszString(bool.Parse(row3G)) + "_" + anyjaNeve2;
                    //MessageBox.Show(oneDataDB);
                    dataDB.Add(oneDataDB);
                }
            }
            conn.Close();


            //MessageBox.Show("PC: " + dataOnPC.Count);
            //MessageBox.Show("DB: " + dataDB.Count);

            new Thread(() =>
            {

                Thread.CurrentThread.IsBackground = true;
                var listAdatbazisbolHianyzo = dataOnPC.Where(x => !dataDB.Contains(x)).ToList();
                var listPCnHianyzo = dataDB.Where(x => !dataOnPC.Contains(x)).ToList();
                var listUnion = dataOnPC.Union(dataDB);
                //MessageBox.Show($"db hiany: {listAdatbazisbolHianyzo.Count} pc hiany: {listPCnHianyzo.Count}");
                if (listAdatbazisbolHianyzo.Count > 0)
                {
                    string lines = string.Join(Environment.NewLine, listAdatbazisbolHianyzo);
                    if (MessageBox.Show($"Adatbázisból hiányzó fileok: \n{lines}\nSzeretnéd törölni? a PC-ről?", "Hiányzó elemek törlése", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        hianyzoElemeTorlesePCrol(listAdatbazisbolHianyzo, fullPath);
                    }
                }
                if (listPCnHianyzo.Count > 0)
                {
                    string lines = string.Join(Environment.NewLine, listPCnHianyzo);
                    if (MessageBox.Show($"PC-ről hiányzó fileok: \n{lines}\nSzeretnéd törölni az adatbázisból a bejegyzést róla?", "Hiányzó elemek törlése", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        hianyzoElemekTorleseDBrol(listPCnHianyzo, from);
                    }
                }

            }).Start();

            Thread.Sleep(100);
            getCount(l1, l2, destPath, from);
        }

        private static string boolConvertToTavaszOszString(bool value)
        {
            if (value == true)
            {
                return "Tavasz";
            }
            else
            {
                return "Ősz";
            }
        }

        private static void hianyzoElemeTorlesePCrol(List<string> lines, string destPath)
        {
            MessageBox.Show("PC torles fv");
            try
            {
                foreach (var item in lines)
                {
                    string destination = destPath + item + ".*";
                    string file = item + ".*";
                    System.IO.File.Delete(System.IO.Directory.GetFiles(destPath, file)[0].ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba lépett fel a hiányzó elem törlésekkor a számítógépen!");
            }
        }

        private static void hianyzoElemekTorleseDBrol(List<string> lines, string from)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            foreach (var item in lines)
            {
                cmd.Parameters.Clear();
                string[] row = item.Split('_');
                string SQL = $"DELETE FROM `{from}` " +
                    "WHERE " +
                    "`tanuloNeve` = '" + row[0] + "'and " +
                    "`anyjaNeve` = '" + row[3] + "' and " +
                    "`viszgaEvKezdet` = " + row[1]
                    ;
                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public static void fileKereseseFajlkezeloben(string filaPath, string tableName)
        {
            MessageBox.Show("filename: " + filaPath);
            MessageBox.Show("tablename: " + tableName);
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();

            }
            var getData = conn.CreateCommand();

            string idIn = filaPath.Split('\\').Last();
            //MessageBox.Show("id: " + idIn);
            getData.CommandText = "SELECT path " +
                $"FROM {tableName} " +
                "WHERE id = " + idIn;

            var pathFromDB = getData.ExecuteScalar();
            //MessageBox.Show("pathFromDB " + pathFromDB.ToString());
            string kiterjFromDB = pathFromDB.ToString().Split('.').Last();
            //MessageBox.Show("kiterj " + kiterjFromDB);

            try
            {
                string filePath = filaPath + ".dat";
                MessageBox.Show("filepath: " + filePath);
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Nincs meg a File!" + filePath);
                    return;
                }

                string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName().Split('.')[1]);
                Directory.CreateDirectory(tempDirectory);
                MessageBox.Show("Ez az ideiglenes könyvtár: " + tempDirectory);
                string fileInTemp = tempDirectory + "\\"+ idIn +"." + kiterjFromDB;
                MessageBox.Show("Ez a file in temp: " + fileInTemp);

                string destination = Path.Combine(tempDirectory, fileInTemp);
                MessageBox.Show($"from: {filePath}\nto: {destination}");

                System.IO.File.Copy(filePath, destination);

                string argument = destination;

                //Process fileopener = new Process();
                //fileopener.StartInfo.FileName = fileInTemp;
                //fileopener.StartInfo.Arguments = argument;
                //fileopener.Start();


                System.Diagnostics.Process.Start("explorer.exe", argument);
                torlendoMappak.Add(tempDirectory);
            }

            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
            cmd.Parameters.Clear();
            conn.Close();
        }

        public static void setVariablesFromSelecteditem(ListBox lb, string rowNev, string rowEvKezdet, string rowTavaszVOsz,
                                                        string rowAnyja, string rowFormatum, string tableName)
        {
            conn.Open();
            var getData = conn.CreateCommand();
            if (lb.SelectedItem != null)
            {
                string idIn = lb.SelectedItem.ToString();
                getData.CommandText = $"SELECT {rowNev}, " +
                    $"{rowEvKezdet}, {rowTavaszVOsz}, {rowAnyja}, {rowFormatum} " +
                    $"FROM {tableName} " +
                    $"WHERE id = " + idIn;

                //var result = getData.ExecuteScalar();
                using (var reader = getData.ExecuteReader())
                {
                    var rowNev2 = reader.GetOrdinal(rowNev);
                    var rowEvKezdet2 = reader.GetOrdinal(rowEvKezdet);
                    var rowTavaszVOsz2 = reader.GetOrdinal(rowTavaszVOsz);
                    var rowAnyja2 = reader.GetOrdinal(rowAnyja);
                    var rowFormatum2 = reader.GetOrdinal(rowFormatum);

                    while (reader.Read())
                    {
                        globNev = reader.GetValue(rowNev2).ToString();
                        globEvKezdet = reader.GetValue(rowEvKezdet2).ToString();
                        globTavaszVoszTrueOrFalse = bool.Parse(reader.GetValue(rowTavaszVOsz2).ToString());
                        globAnyja = reader.GetValue(rowAnyja2).ToString();
                        globKiterjesztes = reader.GetValue(rowFormatum2).ToString();
                    }
                    if (globTavaszVoszTrueOrFalse == true)
                    {
                        globTavaszVoszInt = 1;
                        globTavaszVOszString = "Tavasz";
                    }
                    else
                    {
                        globTavaszVoszInt = 0;
                        globTavaszVOszString = "Ősz";
                    }
                }
            }
            conn.Close();
        }

        public static void tallozas(TextBox textBoxFilename/*, TextBox textBoxEleresi*/)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                MessageBox.Show("Hiba");
            }
            else
            {
                string[] tomb = openFileDialog1.FileName.Split('\\');
                string fileNameWExtension = tomb.Last();
                string[] tombFile = fileNameWExtension.Split('.');

                globKiterjesztes = tombFile.Last();
                textBoxFilename.Text = fileNameWExtension;
                globFeltoltendoFileEleresiUt = openFileDialog1.FileName;
                /*textBoxEleresi.Text = openFileDialog1.FileName;*/
            }
        }

        public static void textBoxTextChanged(TextBox tb)
        {
            if (tb.Text.Length == 0)
            {
                tb.BackColor = Color.Red;
            }
            else
            {
                tb.BackColor = Color.White;
            }
        }

        public static void bordercolorReset(TextBox anyja, TextBox dokumentum, TextBox tanuloneve, TextBox eleresi)
        {
            anyja.BackColor = Color.White;
            dokumentum.BackColor = Color.White;
            tanuloneve.BackColor = Color.White;
            eleresi.BackColor = Color.White;
        }

        public static void keres(string rowTneveEsWhere, string rowVkezdet, string rowViszgaTaVOsz, string rowAnyja, string from, string textboxText,
                                 ListBox ListBid, ListBox listBoxTanuloNeve, ListBox listBoxVKezdete, ListBox listBoxVVege, ListBox listBoxanyjaNeve)
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = $"SELECT id,{rowTneveEsWhere},{rowVkezdet},{rowViszgaTaVOsz},{rowAnyja}  FROM {from} WHERE {rowTneveEsWhere} like '%" + textboxText + "%'";
            using (var reader = command.ExecuteReader())
            {
                var id = reader.GetOrdinal("id");
                var tanuloNeve = reader.GetOrdinal(rowTneveEsWhere);
                var viszgaEvKezdet = reader.GetOrdinal(rowVkezdet);
                var viszgaTavasz1Osz0 = reader.GetOrdinal(rowViszgaTaVOsz);
                var anyjaNeve = reader.GetOrdinal(rowAnyja);
                int i = 0;
                while (reader.Read() && i < 19)
                {
                    var id2 = reader.GetValue(id).ToString();
                    var tanuloNeve2 = reader.GetValue(tanuloNeve).ToString();
                    var viszgaEvKezdet2 = reader.GetValue(viszgaEvKezdet).ToString();
                    var viszgaTavasz1Osz02 = reader.GetValue(viszgaTavasz1Osz0).ToString();
                    var anyjaNeve2 = reader.GetValue(anyjaNeve).ToString();
                    ListBid.Items.Add(id2);
                    listBoxTanuloNeve.Items.Add(tanuloNeve2);
                    listBoxVKezdete.Items.Add(viszgaEvKezdet2);
                    listBoxVVege.Items.Add(boolConvert(bool.Parse(viszgaTavasz1Osz02)));
                    listBoxanyjaNeve.Items.Add(anyjaNeve2);
                    i++;
                }
            }
            conn.Close();
        }

        private static string boolConvert(bool value)
        {
            if (value == true)
            {
                return "Tavasz";
            }
            else
            {
                return "Ősz";
            }
        }

        public static void listboxKeresesEredmenyeiClear(ListBox l1, ListBox l2, ListBox l3, ListBox l4, ListBox l5)
        {
            l1.Items.Clear();
            l2.Items.Clear();
            l3.Items.Clear();
            l4.Items.Clear();
            l5.Items.Clear();
        }

        public static void buttonClickClear(DataGridView dataGridView, TextBox textBoxTanuloNeve, TextBox textBoxAnyjaNeve, NumericUpDown numericUpDown, CheckBox checkBox)
        {
            textBoxTanuloNeve.Clear();
            textBoxAnyjaNeve.Clear();
            numericUpDown.ResetText();
            checkBox.Checked = false;
        }


        public static void dataGridViewBasicSettings(DataGridView dataGridView1, Panel panel, TextBox textBoxTanuloNeve, TextBox textBoxAnyjaNeve, NumericUpDown numericUpDown)
        {
            //Global.dataGridViewKeresesEredmenyeiClear(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            if (!dataGridView1.Visible || !panel.Visible)
            {
                dataGridView1.Visible = true;
                panel.Visible = true;
            }
        }

        public static void dataGridViewClear(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
        }
        public static void osszetettKeres(string rowTneve, string rowKezdet, string rowTvOsz, string rowAnyja,
            string from, string likeText1, string likeText2,
            ListBox listBoxId, ListBox listBoxTanuloneve, ListBox ListBoxVKezdete, ListBox listBoxTvOsz, ListBox listBoxanyjaNeve)
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = $"SELECT id, {rowTneve}, {rowKezdet}, {rowTvOsz}, {rowAnyja}  " +
                $"FROM {from} " +
                $"WHERE {rowTneve} like '%{likeText1}%' " +
                $"AND {rowAnyja} like '%{likeText2}%'";
            //MessageBox.Show(command.CommandText.ToString());
            using (var reader = command.ExecuteReader())
            {
                var id = reader.GetOrdinal("id");
                var rowTneveData = reader.GetOrdinal(rowTneve);
                var rowKezdetData = reader.GetOrdinal(rowKezdet);
                var rowTvOszData = reader.GetOrdinal(rowTvOsz);
                var rowAnyjaData = reader.GetOrdinal(rowAnyja);
                int i = 0;
                while (reader.Read() && i < 19)
                {
                    var id2 = reader.GetValue(id).ToString();
                    var rowTneveData2 = reader.GetValue(rowTneveData).ToString();
                    var rowKezdetData2 = reader.GetValue(rowKezdetData).ToString();
                    var rowTvOszData2 = reader.GetValue(rowTvOszData).ToString();
                    var rowAnyjaData2 = reader.GetValue(rowAnyjaData).ToString();
                    listBoxId.Items.Add(id2);
                    listBoxTanuloneve.Items.Add(rowTneveData2);
                    ListBoxVKezdete.Items.Add(rowKezdetData2);
                    listBoxTvOsz.Items.Add(boolConvert(bool.Parse(rowTvOszData2)));
                    listBoxanyjaNeve.Items.Add(rowAnyjaData2);
                    i++;
                }
            }
            conn.Close();
        }

        public static void osszetettKeresv2(string rowTneve, string rowVkezdet, string rowTavaszOsz, string rowAnyja,
                                            string from, string like1, string like2, string like3, string like4,
                                            ListBox listBoxId, ListBox listBoxTanuloNeve, ListBox listBoxVkezdete, ListBox listBoxIdoszak, ListBox listBoxAnyja)
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = $"SELECT id, {rowTneve}, {rowVkezdet}, {rowTavaszOsz}, {rowAnyja} " +
                                $"FROM {from} " +
                                $"WHERE {rowTneve} like '%{like1}%' " +
                                $"AND {rowAnyja} like '%{like2}%' " +
                                $"AND {rowVkezdet} like '%{like3}%' " +
                                $"AND {rowTavaszOsz} like '%{like4}%'";
            using (var reader = command.ExecuteReader())
            {
                var id = reader.GetOrdinal("id");
                var rowTneveData = reader.GetOrdinal(rowTneve);
                var rowVkezdetData = reader.GetOrdinal(rowVkezdet);
                var rowTavaszOszData = reader.GetOrdinal(rowTavaszOsz);
                var rowAnyjaData = reader.GetOrdinal(rowAnyja);
                int i = 0;
                while (reader.Read() && i < 19)
                {
                    var id2 = reader.GetValue(id).ToString();
                    var rowTneveData2 = reader.GetValue(rowTneveData).ToString();
                    var rowVkezdetData2 = reader.GetValue(rowVkezdetData).ToString();
                    var rowTavaszOszData2 = reader.GetValue(rowTavaszOszData).ToString();
                    var rowAnyjaData2 = reader.GetValue(rowAnyjaData).ToString();
                    listBoxId.Items.Add(id2);
                    listBoxTanuloNeve.Items.Add(rowTneveData2);
                    listBoxVkezdete.Items.Add(rowVkezdetData2);
                    listBoxIdoszak.Items.Add(boolConvert(bool.Parse(rowTavaszOszData2)));
                    listBoxAnyja.Items.Add(rowAnyjaData2);
                    i++;
                }
            }
            conn.Close();
        }

        public static void osszetettKeresDataGridview(string rowTneve, string rowVkezdet, string rowExtra, string rowAnyja,
                                            string from, string likeTanuloNeve, string likeAnyjaNeve, string likeVkezdet,
                                            DataGridView dataGridView, bool checkBoxChecked, int db)
        {
            if (!checkBoxChecked)
            {
                likeVkezdet = "";
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var command = conn.CreateCommand();
            command.CommandText = $"SELECT id, {rowTneve}, {rowVkezdet}, {rowExtra}, {rowAnyja} " +
                                $"FROM {from} " +
                                $"WHERE {rowTneve} like '%{likeTanuloNeve}%' " +
                                $"AND {rowAnyja} like '%{likeAnyjaNeve}%' " +
                                $"AND {rowVkezdet} like '%{likeVkezdet}%' ";

            using (var reader = command.ExecuteReader())
            {
                var id = reader.GetOrdinal("id");
                var rowTneveData = reader.GetOrdinal(rowTneve);
                var rowVkezdetData = reader.GetOrdinal(rowVkezdet);
                var rowAnyjaData = reader.GetOrdinal(rowAnyja);
                var rowExtraData = reader.GetOrdinal(rowExtra);
                int i = 0;
                while (reader.Read() && i < db)
                {
                    var id2 = reader.GetValue(id).ToString();
                    var rowTneveData2 = reader.GetValue(rowTneveData).ToString();
                    var rowVkezdetData2 = reader.GetValue(rowVkezdetData).ToString();
                    var rowAnyjaData2 = reader.GetValue(rowAnyjaData).ToString();
                    var rowExtraData2 = reader.GetValue(rowExtraData).ToString();
                    if (rowExtraData2 == "True" || rowExtraData2 == "False")
                    {
                        rowExtraData2 = rowExtraData2 == "True" ? "Tavasz" : "Ősz";
                    }
                    dataGridView.Rows.Add(id2, rowTneveData2, rowAnyjaData2, rowVkezdetData2, rowExtraData2);
                    i++;
                }
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public static void fileFeltolteseBDreESFileMozgatasa(TextBox textBoxanyjaNeveFeltolt, TextBox textBoxTanuloNeveFeltolt, string feltoltendoElereseiut, string hovaMasolja, dynamic radioButtonOszFeltolt,
            dynamic numericUpDownEvFeltoltKezdet, string into, string rowTneve, string rowAnyja, string rowSzerzo, string rowVevKezdet, string rowtavaszVosz,
            string rowDokLegutobbModositva, string rowFeltoltesIdopontja, string rowPath)
        {
            try
            {
                MessageBox.Show(hovaMasolja);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                int indexOf = textBoxanyjaNeveFeltolt.Text.IndexOfAny(SpecialChars);
                int indexOf2 = textBoxTanuloNeveFeltolt.Text.IndexOfAny(SpecialChars);
                byte tavaszOsz = 0;
                int returnValue = -1;
                if (radioButtonOszFeltolt is RadioButton)
                {
                    if (radioButtonOszFeltolt.Checked)
                    {
                        tavaszOsz = 0;
                    }
                    else
                    {
                        tavaszOsz = 1;
                    }
                }

                if (indexOf == -1 && indexOf2 == -1)
                {
                    //string eleresiUt = @"C:\Users\Pap Gergő\OneDrive\Pictures\Képernyőképek\2020-10-27.png"; // random miatt nem lesz jó rendes használatnál
                    //MessageBox.Show("From: " + eleresiUt);
                    string destination = feltoltendoElereseiut;
                    //MessageBox.Show("Adatbázis path: " + destination);

                    string SQL = "INSERT INTO " +
                            $"{into} " +
                            "VALUES" +
                            "(" +
                                    $"NULL, " +
                                    $"@{rowTneve}, " +
                                    $"@{rowAnyja}, " +
                                    $"@{rowSzerzo}, " +
                                    $"@{rowVevKezdet}, " +
                                    $"@{rowtavaszVosz}, " +
                                    $"@{rowDokLegutobbModositva}," +
                                    $"@{rowFeltoltesIdopontja}," +
                                    $"@{rowPath} " +
                            ");" +

                            "SELECT LAST_INSERT_ID();"
                            ;

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue($"@{rowTneve}", textBoxTanuloNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue($"@{rowAnyja}", textBoxanyjaNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue($"@{rowSzerzo}", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    if (numericUpDownEvFeltoltKezdet is NumericUpDown)
                    {
                        cmd.Parameters.AddWithValue($"@{rowVevKezdet}", numericUpDownEvFeltoltKezdet.Value);

                    }
                    else if (numericUpDownEvFeltoltKezdet is TextBox)
                    {
                        cmd.Parameters.AddWithValue($"@{rowVevKezdet}", int.Parse(numericUpDownEvFeltoltKezdet.Text));
                    }
                    if (radioButtonOszFeltolt is RadioButton)
                    {
                        cmd.Parameters.AddWithValue($"@{rowtavaszVosz}", tavaszOsz);
                    }
                    else if (radioButtonOszFeltolt is NumericUpDown)
                    {
                        cmd.Parameters.AddWithValue($"@{rowtavaszVosz}", radioButtonOszFeltolt.Value);
                    }
                    else if (radioButtonOszFeltolt is TextBox)
                    {
                        cmd.Parameters.AddWithValue($"@{rowtavaszVosz}", int.Parse(radioButtonOszFeltolt.Text));
                    }
                    cmd.Parameters.AddWithValue($"@{rowDokLegutobbModositva}", File.GetLastWriteTime(feltoltendoElereseiut));
                    cmd.Parameters.AddWithValue($"@{rowFeltoltesIdopontja}", DateTime.Now);
                    cmd.Parameters.AddWithValue($"@{rowPath}", destination);

                    object modified = cmd.ExecuteScalar();
                    if (modified != null)
                    {
                        int.TryParse(modified.ToString(), out returnValue);
                    }
                    //MessageBox.Show("Feltöltés köv id: " + returnValue.ToString());
                    destination = hovaMasolja + returnValue + ".dat";

                    //cmd.ExecuteNonQuery();

                    string source = feltoltendoElereseiut;
                    //MessageBox.Show("Forrás: " + source + "\nCél: " + destination);
                    System.IO.File.Copy(source, destination);

                    //MessageBox.Show("Sikeres feltöltés!",
                    //"Siker!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cmd.Parameters.Clear();
                }
                else
                {
                    MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*");
                }

                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Hiba " + ex.Number + " lépett fel: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {

                MessageBox.Show("Fájl hiba!\n A fájl már létezik vagy nem található!");
            }
        }

        public static void textBoxFeltoltUrites(TextBox t1, TextBox t2, TextBox t3, TextBox t4)
        {
            t1.Clear();
            t2.Clear();
            t3.Clear();
            t4.Clear();
        }

        public static void torles(ListBox listBoxId, string from)
        {
            try
            {
                conn.Open();
                string id = listBoxId.SelectedItem.ToString();

                string SQL = "DELETE FROM " +
                                $"{from} " +
                                "WHERE " +
                                "id =" + id
                                ;
                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();

                string destination = fullPath + globNev + '_' + globEvKezdet + '_' + globTavaszVOszString + '_' + globAnyja + '.' + globKiterjesztes;

                System.IO.File.Delete(destination);

                MessageBox.Show("Sikeres törlés");
                conn.Close();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
        }

        public static void ujTorles(string id, string from, string destination)
        {
            MessageBox.Show(destination);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                string SQL = "DELETE FROM " +
                                $"{from} " +
                                "WHERE " +
                                "id =" + id
                                ;
                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();
                destination += id + ".dat";
                System.IO.File.Delete(destination);

                MessageBox.Show("Sikeres törlés");
                conn.Close();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
        }

        public static void modositas(TextBox textBoxanyjaNeveModositas, TextBox textBoxNevModositas, RadioButton radioButtonTavaszModosit, TextBox tbOther, NumericUpDown numericUpDownEvKezdetModositas, NumericUpDown numericUpDown2,
            string update, string rowTneve, string rowanyjaNeve, string rowVkezdet, string rowTavaszVosz, string rowId)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            int indexOf = textBoxanyjaNeveModositas.Text.IndexOfAny(SpecialChars);
            int indexOf2 = textBoxNevModositas.Text.IndexOfAny(SpecialChars);
            int tavaszVosz = 0;
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
                tavaszVosz = int.Parse(tbOther.Text.ToString());
            }
            if (numericUpDown2 != null)
            {
                tavaszVosz = int.Parse(numericUpDown2.Value.ToString());
            }
            if (indexOf == -1 && indexOf2 == -1)
            {
                try
                {
                    string SQL =
                                "UPDATE " +
                                $"{update} " +
                                "SET " +
                                $"{rowTneve} = '" + textBoxNevModositas.Text + "', " +
                                $"{rowanyjaNeve} = '" + textBoxanyjaNeveModositas.Text + "', " +
                                $"{rowVkezdet} = {numericUpDownEvKezdetModositas.Value} , " +
                                $"{rowTavaszVosz} = {tavaszVosz}  " +
                                "WHERE " +
                                $"id = '{rowId}';"
                                ;

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Sikeres módosítás");
                    conn.Close();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Nincs kijelölve semmi!");
                }
            }
            else
            {
                MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*");
            }
            conn.Close();
        }

        public static void setFeltoltPanelPosition(Panel p1, Panel p2, Panel p3, Panel p4, Panel p5)
        {
            p1.Location = new Point(0,100);
            p2.Location = new Point(0,100);
            p3.Location = new Point(0,100);
            p4.Location = new Point(0,100);
            p5.Location = new Point(0,100);
        }
        public static void loadSelectedDataWhenModifying(DataGridView dataGridView, TextBox tbAnyja, TextBox tbTan, 
                                                        NumericUpDown numericUpDown, TextBox tb, 
                                                        RadioButton radioTavaszTrue, RadioButton radioOszTrue,
                                                        NumericUpDown numeric)
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

        public static bool checkNumericUpDownValue(NumericUpDown numericUpDown)
        {
            if (numericUpDown.Value >= 2100 || numericUpDown.Value <= 1900)
            {
                numericUpDown.BackColor = Color.Red;
                return false;
            }
            else
            {
                numericUpDown.BackColor = default;
                return true;
            }
        }

        public static bool checkIfEmptyInput(TextBox textBoxanyjaNeveFeltolt, TextBox textBoxDokumentumNeve, TextBox textBoxTanuloNeveFeltolt, TextBox textBoxEleresi)
        {
            bool joE = true;
            if (textBoxanyjaNeveFeltolt.Text.Length == 0)
            {
                textBoxanyjaNeveFeltolt.BackColor = Color.Red;
                joE = false;
            }
            if (textBoxDokumentumNeve.Text.Length == 0)
            {
                textBoxDokumentumNeve.BackColor = Color.Red;
                joE = false;
            }
            if (textBoxTanuloNeveFeltolt.Text.Length == 0)
            {
                textBoxTanuloNeveFeltolt.BackColor = Color.Red;
                joE = false;
            }
            if (textBoxEleresi.Text.Length == 0)
            {
                textBoxEleresi.BackColor = Color.Red;
                joE = false;
            }
            return joE;
        }

        public static bool checkDB_Conn(bool messageBox)
        {
            var conn_info = "Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;";
            bool isConn = false;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(conn_info);
                conn.Open();
                isConn = true;
            }
            catch (ArgumentException a_ex)
            {
                globIsaMessageBoxOpen = true;
                if (messageBox) MessageBox.Show($"Hibás connection string!\n{a_ex.Message}\n{a_ex}");
                globIsaMessageBoxOpen = false;
            }
            catch (MySqlException ex)
            {
                string sqlErrorMessage = "Üzenet: " + ex.Message + "\n" +
                "Forrás: " + ex.Source + "\n" +
                "Szám: " + ex.Number;

                globIsaMessageBoxOpen = true;

                new Thread(() =>
                {
                    if (messageBox)
                    {
                        MessageBox.Show(sqlErrorMessage);
                        globIsaMessageBoxOpen = false;

                        isConn = false;
                        if (messageBox)
                        {
                            switch (ex.Number)
                            {
                                case 1042:
                                    MessageBox.Show("Nem lehet csatlakozni a MySql hosthoz! (Check Server,Port)");
                                    break;
                                case 0:
                                    MessageBox.Show("Hozzáférés megtagadva! (Check DB name,username,password)");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }).Start();
                
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return isConn;
        }

        //main függvényei

        public static void createTablesettings()
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 
                                settings 
                                (
                                id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                var TEXT NOT NULL,
                                value TEXT NOT NULL
                                );
                                ";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void createTableKozepiskolaAnyakonyv()
        {
            conn.Open();
            var command = conn.CreateCommand();

            command = conn.CreateCommand();
            command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 
                                kozepiskolaanyakonyv 
                                (
                                id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                tanuloNeve TEXT NOT NULL,
                                anyjaNeve TEXT NOT NULL,
                                szerzo TEXT NOT NULL,
                                vizsgaEvKezdet INT NOT NULL,
                                vizsgaEvVeg INT NOT NULL,
                                dokLegutobbModositva DATETIME NOT NULL,
                                feltoltesIdopontja DATETIME NOT NULL,
                                path TEXT NULL
                                );
                                ";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void createTableszakmaiviszgaanyakonyv()
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 
                                szakmaivizsgaanyakonyv 
                                (
                                id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                tanuloNeve TEXT NOT NULL,
                                anyjaNeve TEXT NOT NULL,
                                szerzo TEXT NOT NULL,
                                vizsgaEvKezdet INT NOT NULL,
                                vizsgaEvVeg INT NOT NULL,
                                dokLegutobbModositva DATETIME NOT NULL,
                                feltoltesIdopontja DATETIME NOT NULL,
                                path TEXT NULL
                                );
                                ";
            command.ExecuteNonQuery();
            conn.Close();
        }


        public static void createTableSzakmaivizsgaTorzslap()
        {
            conn.Open();
            var command = conn.CreateCommand();
            command = conn.CreateCommand();
            command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 
                                szakmaivizsgaTorzslap 
                                (
                                id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                tanuloNeve TEXT NOT NULL,
                                anyjaNeve TEXT NOT NULL,
                                szerzo TEXT NOT NULL,
                                vizsgaEvVeg INT NOT NULL,
                                vizsgaTavasz1Osz0 BOOLEAN NOT NULL,
                                dokLegutobbModositva DATETIME NOT NULL,
                                feltoltesIdopontja DATETIME NOT NULL,
                                path TEXT NULL
                                );
                                ";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void createTableErettsegiTanusitvany()
        {
            conn.Open();
            var command = conn.CreateCommand();
            command = conn.CreateCommand();
            command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 
                                erettsegitanusitvany 
                                (
                                id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                tanuloNeve TEXT NOT NULL,
                                anyjaNeve TEXT NOT NULL,
                                szerzo TEXT NOT NULL,
                                vizsgaEvVeg INT NOT NULL,
                                tanuloiAzonosito INT NOT NULL,
                                dokLegutobbModositva DATETIME NOT NULL,
                                feltoltesIdopontja DATETIME NOT NULL,
                                path TEXT NULL
                                );
                                ";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void createTableErettsegiTorzslap()
        {
            conn.Open();
            var command = conn.CreateCommand();
            command = conn.CreateCommand();
            command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 
                                erettsegitorzslap 
                                (
                                id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                tanuloNeve TEXT NOT NULL,
                                anyjaNeve TEXT NOT NULL,
                                szerzo TEXT NOT NULL,
                                vizsgaEvVeg INT NOT NULL,
                                vizsgaTavasz1Osz0 BOOLEAN NOT NULL,
                                dokLegutobbModositva DATETIME NOT NULL,
                                feltoltesIdopontja DATETIME NOT NULL,
                                path TEXT NULL
                                );
                                ";
            command.ExecuteNonQuery();
            conn.Close();
        }


        public static void createDirectiories(Label labelMentesiHely)
        {
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Középiskola\Anyakönyv\");
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Szakmai Vizsga\Anyakönyv\");
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Szakmai Vizsga\Törzslap\");
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Érettségi\Törzslap\");
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Érettségi\Tanusítvány\");
            fullPath = labelMentesiHely.Text;
        }

        public static void checkDirs(GroupBox groupBoxEleresi, GroupBox groupBoxButtons, Label labelMentesiHely, Panel panel, string varString)
        {
            conn.Open();
            string query = $"SELECT value FROM settings WHERE var = '{varString}'; ";
            var command = conn.CreateCommand();
            command.CommandText = query;
            var result = command.ExecuteScalar();
            if (result == null)
            {
                groupBoxEleresi.Visible = true;
                groupBoxButtons.Visible = false;
                panel.Visible = false;
            }
            else
            {
                var path = result.ToString();
                labelMentesiHely.Text = path;
                groupBoxEleresi.Visible = false;
                groupBoxButtons.Visible = true;
                panel.Visible = true;
                fullPath = labelMentesiHely.Text;
            }
            conn.Close();
        }

        public static void setPathInDB(Label labelMentesiHely, GroupBox groupBoxEleresi, GroupBox groupBoxButtons, string eleresiUt)
        {
            try
            {
                conn.Open();
                int indexOf = labelMentesiHely.Text.IndexOfAny(SpecialChars);
                if (indexOf == -1)
                {
                    string SQL = "INSERT INTO " +
                        "settings " +
                        "VALUES" +
                        "(" +
                            "NULL, " +
                            "@var, " +
                            "@value " +
                        ")";
                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@var", eleresiUt);
                    cmd.Parameters.AddWithValue("@value", labelMentesiHely.Text.ToString());

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*");
                }
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("SQL hiba " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            groupBoxEleresi.Visible = false;
            groupBoxButtons.Visible = true;
        }

        public static void tallozas(Label labelMentesiHely)
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

        public static void dataGridViewOffline(DataGridView dataGridView, Panel panel1, Panel panel2)
        {
            dataGridView.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
        }



        public static void setAndResetButtonColors(Button buttonSet, Button buttonReset1, Button buttonReset2, Button buttonReset3, Button buttonReset4)
        {
            globSelectedButton = buttonSet.Text;
            buttonSet.BackColor = Color.Black;
            buttonSet.ForeColor = Color.White;
            buttonReset1.ForeColor = Color.Black;
            buttonReset1.BackColor = default;
            buttonReset2.ForeColor = Color.Black;
            buttonReset2.BackColor = default;
            buttonReset3.ForeColor = Color.Black;
            buttonReset3.BackColor = default;
            buttonReset4.ForeColor = Color.Black;
            buttonReset4.BackColor = default;
        }

        public static void search(DataGridView dataGridView)
        {
            try
            {
                string filePath = fullPath + dataGridView.SelectedCells[0].Value + "." + globKiterjesztes;
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Nincs meg a File!" + filePath);
                    return;
                }
                string argument = "/select, \"" + filePath + "\"";

                System.Diagnostics.Process.Start("explorer.exe", argument);
            }

            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
            cmd.Parameters.Clear();
        }

        public static void firstClickShow(Panel panel1, Panel panel2, DataGridView dataGridView)
        {
            if (!panel1.Visible)
            {
                panel1.Visible = true;
                panel2.Visible = true;
                dataGridView.Visible = true;
            }
        }

        public static void showAndHidePAnels(Panel panelOn, Panel panelOFF1, Panel panelOff2, Panel panelOff3, Panel panelOff4)
        {
            panelOn.Visible = true;
            panelOFF1.Visible = false;
            panelOff2.Visible = false;
            panelOff3.Visible = false;
            panelOff4.Visible = false;
        }

        public static void mappakTorlese()
        {
            foreach (var item in torlendoMappak)
            {
                System.IO.File.Delete(item);
            }
        }

        public static void randomGeneralas()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();

            string[] vezetekNevek = { "Pap", "Timár", "Gulyás", "Szabó", "Horváth", "Kis", "Kiss", "Nagy"
                                ,"Tóth","Kristóf","Tim","Krajcsi","Beke","Gachovetz","Móricz","Dantesz","Bánki ","Baracsiné"
                                ,"Darányi","Hatvani","Földes","Sebők","Skordai","Szűcs","Zalai","Zsuppán","Borzok","Bráda"
                                ,"Almássy","Tim-Bartha","Hazenfratz","Langenbacher"};

            string[] keresztNevek = { "Gyula", "Julianna", "Márta", "Ilona", "Mária", "Csaba", "Attila", "Tamás"
                                ,"Gabriella","Katalin","Károly","Vanda","István","Ernő","Norbert","Krisztina","Kriszta","Mariann"
                                ,"Tibor","Kitti","Lajos","Nóra","Ágnes","Gergő","Klára","Zoltán","Sándor", "Elek"
                                ,"Alexandra","Anasztázia","Boldizsár","Liliána","Benedek","Adrienne"};

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Random r = new Random();
                string tanuloNeve;
                string anyjaNeve;
                int ev;
                string destination;

                conn.Open();
                for (int j = 0; j < 1000; j++)
                {
                    //szakmaivizsgaTorzslap
                    for (int i = 0; i < 100; i++)
                    {
                        destination = Global.fixPath + @"\Adatok\Szakmai Vizsga\Törzslap\";

                        cmd.Parameters.Clear();
                        tanuloNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        anyjaNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        ev = r.Next(1900, 2100);
                        TextBox tAnyja = new TextBox();
                        tAnyja.Text = anyjaNeve;
                        TextBox tTanulo = new TextBox();
                        tTanulo.Text = tanuloNeve;
                        TextBox tTavaszOsz = new TextBox();
                        tTavaszOsz.Text = r.Next(0, 2).ToString();
                        TextBox tEvKezdet = new TextBox();
                        tEvKezdet.Text = ev.ToString();

                        Global.fileFeltolteseBDreESFileMozgatasa(tAnyja, tTanulo,
                                        @"C:\Users\Pap Gergő\Desktop\Új szöveges dokumentum.txt", @"C:\Users\Pap Gergő\Desktop\Adatok\Szakmai Vizsga\Törzslap\", tTavaszOsz,
                                        tEvKezdet,
                                        "szakmaivizsgaTorzslap", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                        "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        Thread.Sleep(25);

                    }
                    for (int i = 0; i < 100; i++)
                    {
                        destination = Global.fixPath + @"\Adatok\Érettségi\Törzslap\";

                        cmd.Parameters.Clear();
                        tanuloNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        anyjaNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        ev = r.Next(1900, 2100);
                        TextBox tAnyja = new TextBox();
                        tAnyja.Text = anyjaNeve;
                        TextBox tTanulo = new TextBox();
                        tTanulo.Text = tanuloNeve;
                        TextBox tTavaszOsz = new TextBox();
                        tTavaszOsz.Text = r.Next(0, 2).ToString();
                        TextBox tEvKezdet = new TextBox();
                        tEvKezdet.Text = ev.ToString();

                        Global.fileFeltolteseBDreESFileMozgatasa(tAnyja, tTanulo,
                                                @"C:\Users\Pap Gergő\Desktop\Új szöveges dokumentum.txt", @"C:\Users\Pap Gergő\Desktop\Adatok\Érettségi\Törzslap\", tTavaszOsz,
                                                tEvKezdet,
                                                "erettsegitorzslap", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        Thread.Sleep(25);
                    }

                    for (int i = 0; i < 100; i++)
                    {
                        destination = Global.fixPath + @"\Adatok\Érettségi\Tanusítvány\";

                        cmd.Parameters.Clear();
                        tanuloNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        anyjaNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        ev = r.Next(1900, 2100);
                        TextBox tAnyja = new TextBox();
                        tAnyja.Text = anyjaNeve;
                        TextBox tTanulo = new TextBox();
                        tTanulo.Text = tanuloNeve;
                        TextBox tTavaszOsz = new TextBox();
                        tTavaszOsz.Text = r.Next(1000000, 9999999).ToString();
                        TextBox tEvKezdet = new TextBox();
                        tEvKezdet.Text = ev.ToString();

                        Global.fileFeltolteseBDreESFileMozgatasa(tAnyja, tTanulo,
                                                @"C:\Users\Pap Gergő\Desktop\Új szöveges dokumentum.txt", @"C:\Users\Pap Gergő\Desktop\Adatok\Érettségi\Tanusítvány\", tTavaszOsz,
                                                tEvKezdet,
                                                "erettsegitanusitvany", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        Thread.Sleep(25);
                    }

                    for (int i = 0; i < 100; i++)
                    {
                        destination = Global.fixPath + @"\Adatok\Szakmai Vizsga\Anyakönyv\";

                        cmd.Parameters.Clear();
                        tanuloNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        anyjaNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        ev = r.Next(1900, 2100);
                        TextBox tAnyja = new TextBox();
                        tAnyja.Text = anyjaNeve;
                        TextBox tTanulo = new TextBox();
                        tTanulo.Text = tanuloNeve;
                        TextBox tTavaszOsz = new TextBox();
                        tTavaszOsz.Text = r.Next(1900, 2100).ToString();
                        TextBox tEvKezdet = new TextBox();
                        tEvKezdet.Text = ev.ToString();

                        Global.fileFeltolteseBDreESFileMozgatasa(tAnyja, tTanulo,
                                                @"C:\Users\Pap Gergő\Desktop\Új szöveges dokumentum.txt", @"C:\Users\Pap Gergő\Desktop\Adatok\Szakmai Vizsga\Anyakönyv\", tTavaszOsz,
                                                tEvKezdet,
                                                "szakmaivizsgaanyakonyv", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        Thread.Sleep(25);

                    }
                    for (int i = 0; i < 100; i++)
                    {
                        destination = Global.fixPath + @"\Adatok\Középiskola\Anyakönyv\";

                        cmd.Parameters.Clear();
                        tanuloNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        anyjaNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
                        ev = r.Next(1900, 2100);
                        TextBox tAnyja = new TextBox();
                        tAnyja.Text = anyjaNeve;
                        TextBox tTanulo = new TextBox();
                        tTanulo.Text = tanuloNeve;
                        TextBox tTavaszOsz = new TextBox();
                        tTavaszOsz.Text = r.Next(1900, 2100).ToString();
                        TextBox tEvKezdet = new TextBox();
                        tEvKezdet.Text = ev.ToString();

                        Global.fileFeltolteseBDreESFileMozgatasa(tAnyja, tTanulo,
                                                @"C:\Users\Pap Gergő\Desktop\Új szöveges dokumentum.txt", @"C:\Users\Pap Gergő\Desktop\Adatok\Középiskola\Anyakönyv\", tTavaszOsz,
                                                tEvKezdet,
                                                "kozepiskolaanyakonyv", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvVeg", "viszgaTavasz1Osz0",
                                                "dokLegutobbModositva", "feltoltesIdopontja", "path");
                        Thread.Sleep(25);

                    }
                }
            }).Start();
            conn.Close();
        }


    }
}
