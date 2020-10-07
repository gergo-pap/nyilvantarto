using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;
using System.Data;
using System.Drawing;

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
        private static MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");
        private static MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        private static readonly char[] SpecialChars = "!@#$%^&*()-".ToCharArray();

        public static void loadFuggvenyek(GroupBox gbOsszetett, GroupBox gbRandom)
        {
            gbOsszetett.Visible = false;
            gbRandom.Visible = true;
        }

        public static void createTable(string tableName, string rowTanuloneve, string rowAnyjaneve, string rowSzerzo, string rowVevKezdet, string rowVizsgaTavaszvOsz,
                                        string rowdokNeve, string rowdokLegutobbModositva, string rowFeltoltIdopontja, string rowFormatum, string rowPath)
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = $@"
                                CREATE TABLE IF NOT EXISTS 
                                    {tableName} 
                                    (
                                    id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                    {rowTanuloneve} TEXT NOT NULL,
                                    {rowAnyjaneve} TEXT NOT NULL,
                                    {rowSzerzo} TEXT NOT NULL,
                                    {rowVevKezdet} INT NOT NULL,
                                    {rowVizsgaTavaszvOsz} BOOLEAN NOT NULL,
                                    {rowdokNeve} TEXT NOT NULL,
                                    {rowdokLegutobbModositva} DATETIME NOT NULL,
                                    {rowFeltoltIdopontja} DATETIME NOT NULL,
                                    {rowFormatum} TEXT NOT NULL,
                                    {rowPath} TEXT NULL
                                    );
                                    ";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void getDestPathFromDatabase(string folders)
        {
            conn.Open();
            var command = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select s_value From settings Where s_var = @var";
            cmd.Parameters.AddWithValue("@var", "eleresiUt");
            var result = cmd.ExecuteScalar();
            fullPath = result.ToString() + folders;
            fixPath = result.ToString();
            cmd.Parameters.Clear();
            conn.Close();
            // TODO: path vége nem lesz jó az összes formnál
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

        public static void checkMissingFile(string destPath, string selectRow1, string selectRow2, string selectRow3, string selectRow4, string from, Label l1, Label l2)
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
            conn.Open();
            var command = conn.CreateCommand();
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
                    var szvt_AnyjaNeve2 = reader.GetValue(row4).ToString();
                    string oneDataDB = row1G + "_" + row2G + "_" + boolConvertToTavaszOszString(bool.Parse(row3G)) + "_" + szvt_AnyjaNeve2;
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
                    "`szvt_tanuloNeve` = '" + row[0] + "'and " +
                    "`szvt_AnyjaNeve` = '" + row[3] + "' and " +
                    "`szvt_viszgaEvKezdet` = " + row[1]
                    ;
                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public static void fileKereseseFajlkezeloben()
        {
            try
            {
                string filePath = fullPath + globNev + '_' + globEvKezdet + '_' + globTavaszVOszString + '_' + globAnyja + "." + globKiterjesztes;
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

        public static void setVariablesFromSelecteditem(ListBox lb, string rowNev, string rowEvKezdet, string rowTavaszVOsz, string rowAnyja, string rowFormatum, string tableName)
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
                    if (globTavaszVoszTrueOrFalse  == true)
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

        public static void tallozas(TextBox textBoxFilename, TextBox textBoxEleresi)
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
                string fileName = "";

                if (tombFile.Length > 2)
                {
                    for (int i = 0; i < tombFile.Length - 1; i++)
                    {
                        fileName += tombFile[i];
                    }
                }
                else
                {
                    fileName = tombFile.First();
                }

                globKiterjesztes = tombFile.Last();
                textBoxFilename.Text = fileName;
                textBoxEleresi.Text = openFileDialog1.FileName;
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
                                 ListBox ListBid, ListBox listBoxTanuloNeve, ListBox listBoxVKezdete, ListBox listBoxVVege, ListBox listBoxAnyjaNeve)
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = $"SELECT id,{rowTneveEsWhere},{rowVkezdet},{rowViszgaTaVOsz},{rowAnyja}  FROM {from} WHERE {rowTneveEsWhere} like '%" + textboxText + "%'";
              using (var reader = command.ExecuteReader())
              {
                  var id = reader.GetOrdinal("id");
                  var szvt_tanuloNeve = reader.GetOrdinal(rowTneveEsWhere);
                  var szvt_viszgaEvKezdet = reader.GetOrdinal(rowVkezdet);
                  var szvt_viszgaTavasz1Osz0 = reader.GetOrdinal(rowViszgaTaVOsz);
                  var szvt_AnyjaNeve = reader.GetOrdinal(rowAnyja);
                  int i = 0;
                  while (reader.Read() && i < 19)
                  {
                      var id2 = reader.GetValue(id).ToString();
                      var szvt_tanuloNeve2 = reader.GetValue(szvt_tanuloNeve).ToString();
                      var szvt_viszgaEvKezdet2 = reader.GetValue(szvt_viszgaEvKezdet).ToString();
                      var szvt_viszgaTavasz1Osz02 = reader.GetValue(szvt_viszgaTavasz1Osz0).ToString();
                      var szvt_AnyjaNeve2 = reader.GetValue(szvt_AnyjaNeve).ToString();
                      ListBid.Items.Add(id2);
                      listBoxTanuloNeve.Items.Add(szvt_tanuloNeve2);
                      listBoxVKezdete.Items.Add(szvt_viszgaEvKezdet2);
                      listBoxVVege.Items.Add(boolConvert(bool.Parse(szvt_viszgaTavasz1Osz02)));
                      listBoxAnyjaNeve.Items.Add(szvt_AnyjaNeve2);
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

        public static void osszetettKeres(string rowTneve, string rowKezdet, string rowTvOsz, string rowAnyja, 
            string from, string likeText1, string likeText2,
            ListBox listBoxId, ListBox listBoxTanuloneve, ListBox ListBoxVKezdete, ListBox listBoxTvOsz, ListBox listBoxAnyjaNeve)
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
                    listBoxAnyjaNeve.Items.Add(rowAnyjaData2);
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

        public static void fileFeltolteseBDreESFileMozgatasa(TextBox textBoxAnyjaNeveFeltolt, TextBox textBoxTanuloNeveFeltolt, TextBox textBoxEleresi, RadioButton radioButtonOszFeltolt,
            NumericUpDown numericUpDownEvFeltoltKezdet, string into, string rowTneve, string rowAnyja, string rowSzerzo, string rowVevKezdet, string rowtavaszVosz, string rowDokumentumnev,
            string rowDokLegutobbModositva, string rowFeltoltesIdopontja, string rowKiterjesztes, string rowPath)
        {
            try
            {
                conn.Open();
                int indexOf = textBoxAnyjaNeveFeltolt.Text.IndexOfAny(SpecialChars);
                int indexOf2 = textBoxTanuloNeveFeltolt.Text.IndexOfAny(SpecialChars);
                byte tavaszOsz;
                string tavaszVOsz;
                if (radioButtonOszFeltolt.Checked)
                {
                    tavaszOsz = 0;
                    tavaszVOsz = "Ősz";
                }
                else
                {
                    tavaszOsz = 1;
                    tavaszVOsz = "Tavasz";
                }
                if (indexOf == -1 && indexOf2 == -1)
                {
                    string eleresiUt = textBoxEleresi.Text;
                    FileStream fs = new FileStream(eleresiUt, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    string fileName = textBoxTanuloNeveFeltolt.Text + "_" + numericUpDownEvFeltoltKezdet.Value.ToString() + "_" + tavaszVOsz + "_" + textBoxAnyjaNeveFeltolt.Text;
                    fs.Close();
                    string destination = fullPath + fileName + "." + globKiterjesztes;

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
                                    $"@{rowDokumentumnev}, " +
                                    $"@{rowDokLegutobbModositva}," +
                                    $"@{rowFeltoltesIdopontja}," +
                                    $"@{rowKiterjesztes}, " +
                                    $"@{rowPath} " +
                            ")";

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue($"@{rowTneve}", textBoxTanuloNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue($"@{rowAnyja}", textBoxAnyjaNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue($"@{rowSzerzo}", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    cmd.Parameters.AddWithValue($"@{rowVevKezdet}", numericUpDownEvFeltoltKezdet.Value);
                    cmd.Parameters.AddWithValue($"@{rowtavaszVosz}", tavaszOsz);
                    cmd.Parameters.AddWithValue($"@{rowDokumentumnev}", fileName);
                    cmd.Parameters.AddWithValue($"@{rowDokLegutobbModositva}", File.GetLastWriteTime(eleresiUt));
                    cmd.Parameters.AddWithValue($"@{rowFeltoltesIdopontja}", DateTime.Now);
                    cmd.Parameters.AddWithValue($"@{rowKiterjesztes}", globKiterjesztes);
                    cmd.Parameters.AddWithValue($"@{rowPath}", destination);

                    cmd.ExecuteNonQuery();

                    string source = eleresiUt;
                    //MessageBox.Show("Forrás: " + source + "\nCél: " + destination);

                    System.IO.File.Copy(source, destination);

                    MessageBox.Show("Sikeres feltöltés!",
                    "Siker!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        public static void modositas(TextBox textBoxAnyjaneveModositas, TextBox textBoxNevModositas, RadioButton radioButtonTavaszModosit, NumericUpDown numericUpDownEvKezdetModositas,
            string update, string rowTneve, string rowAnyjaNeve, string rowVkezdet, string rowTavaszVosz)
        {
            conn.Open();
            int indexOf = textBoxAnyjaneveModositas.Text.IndexOfAny(SpecialChars);
            int indexOf2 = textBoxNevModositas.Text.IndexOfAny(SpecialChars);
            byte tavaszVosz;
            string tavaszVOszModosit;
            if (radioButtonTavaszModosit.Checked)
            {
                tavaszVosz = 1;
                tavaszVOszModosit = "Tavasz";
            }
            else
            {
                tavaszVosz = 0;
                tavaszVOszModosit = "Ősz";
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
                                $"{rowAnyjaNeve} = '" + textBoxAnyjaneveModositas.Text + "', " +
                                $"{rowVkezdet} = " + numericUpDownEvKezdetModositas.Value + ", " +
                                $"{rowTavaszVosz} = " + tavaszVosz + " " +
                                "WHERE " +
                                $"{rowTneve} = '" + globNev + "' AND " +
                                $"{rowAnyjaNeve} =  '" + globAnyja + "' AND " +
                                $"{rowVkezdet} = " + globEvKezdet + " AND " +
                                $"{rowTavaszVosz} = " + globTavaszVoszInt + ";"
                                ;

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;

                    cmd.ExecuteNonQuery();


                    string destFileName = textBoxNevModositas.Text + '_' + numericUpDownEvKezdetModositas.Value + '_' + tavaszVOszModosit + '_' + textBoxAnyjaneveModositas.Text + '.';
                    string sourceFileName = globNev + '_' + globEvKezdet + '_' + globTavaszVOszString + '_' + globAnyja + '.';
                    System.IO.File.Move(fullPath + sourceFileName + globKiterjesztes, fullPath + destFileName  + globKiterjesztes);

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

        }

        public static bool checkIfEmptyInput(TextBox textBoxAnyjaNeveFeltolt, TextBox textBoxDokumentumNeve, TextBox textBoxTanuloNeveFeltolt, TextBox textBoxEleresi)
        {
            bool joE = true;
            if (textBoxAnyjaNeveFeltolt.Text.Length == 0)
            {
                textBoxAnyjaNeveFeltolt.BackColor = Color.Red;
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

    }
}
