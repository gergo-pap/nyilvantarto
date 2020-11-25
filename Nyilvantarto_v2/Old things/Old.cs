using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyilvantarto_v2.Old_things
{
    class Old
    {
        public static void osszetettKeres(string rowTneve, string rowKezdet, string rowTvOsz, string rowAnyja,
    string from, string likeText1, string likeText2,
    ListBox listBoxId, ListBox listBoxTanuloneve, ListBox ListBoxVKezdete, ListBox listBoxTvOsz, ListBox listBoxanyjaNeve)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
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
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
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
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
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
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
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

        public static void setVariablesFromSelecteditem(ListBox lb, string rowNev, string rowEvKezdet, string rowTavaszVOsz,
                                                        string rowAnyja, string rowFormatum, string tableName)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
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
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
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

        public static void torles(ListBox listBoxId, string from)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
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

        public static void randomGeneralas()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");
            MySqlCommand cmd = new MySqlCommand();

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

                if (conn.State != ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
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
