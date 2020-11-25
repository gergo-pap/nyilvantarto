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
        private static MySqlCommand cmd = new MySqlCommand();
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
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            using (var cmd = new MySqlCommand($"SELECT COUNT(id) FROM {from}", conn))
            {
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            l.Text = count.ToString();
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
                string fileInTemp = tempDirectory + "\\" + idIn + "." + kiterjFromDB;
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

        public static void buttonClickClear(DataGridView dataGridView, TextBox textBoxTanuloNeve, TextBox textBoxAnyjaNeve, NumericUpDown numericUpDown, CheckBox checkBox)
        {
            textBoxTanuloNeve.Clear();
            textBoxAnyjaNeve.Clear();
            numericUpDown.ResetText();
            checkBox.Checked = false;
        }

        public static void dataGridViewBasicSettings(DataGridView dataGridView1, Panel panel)
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
                //MessageBox.Show(hovaMasolja);
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
            catch (MySqlException ex)
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

        public static void setPanelVisibility6(Panel p1, Panel p2, Panel p3, Panel p4, Panel p5, Panel p6, bool trueOrFalse)
        {
            p1.Visible = trueOrFalse;
            p2.Visible = trueOrFalse;
            p3.Visible = trueOrFalse;
            p4.Visible = trueOrFalse;
            p5.Visible = trueOrFalse;
            p6.Visible = trueOrFalse;
        }

        public static void torles(string id, string from, string destination)
        {
            string torlendo = fullPath + destination + id + ".dat";
            MessageBox.Show(torlendo);
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

                System.IO.File.Delete(torlendo);

                MessageBox.Show("Sikeres törlés");
                conn.Close();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
        }

        public static void setDatagridViewColumns(DataGridView dataGridView, string col2, string col3, string col4, string col5)
        {
            dataGridView.ColumnCount = 5;
            dataGridView.Columns[0].Name = "Id";
            dataGridView.Columns[1].Name = col2;
            dataGridView.Columns[2].Name = col3;
            dataGridView.Columns[3].Name = col4;
            dataGridView.Columns[4].Name = col5;
            dataGridView.Columns[0].Width = 35;
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
            p1.Location = new Point(0, 100);
            p2.Location = new Point(0, 100);
            p3.Location = new Point(0, 100);
            p4.Location = new Point(0, 100);
            p5.Location = new Point(0, 100);
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



        public static bool checkIfEmptyInput4Tb(TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4)
        {
            bool joE = true;
            if (tb1.Text.Length == 0)
            {
                tb1.BackColor = Color.Red;
                joE = false;
            }
            if (tb2.Text.Length == 0)
            {
                tb2.BackColor = Color.Red;
                joE = false;
            }
            if (tb3.Text.Length == 0)
            {
                tb3.BackColor = Color.Red;
                joE = false;
            }
            if (tb4.Text.Length == 0)
            {
                tb4.BackColor = Color.Red;
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
                if (conn.State == ConnectionState.Open)
                {
                    isConn = true;
                }
                //MessageBox.Show(conn.State.ToString());
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

                //new Thread(() =>
                //{
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
                //}).Start();

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


        public static void createTables()
        {
            createTablesettings();
            createTableKozepiskolaAnyakonyv();
            createTableszakmaiviszgaanyakonyv();
            createTableSzakmaivizsgaTorzslap();
            createTableErettsegiTanusitvany();
            createTableErettsegiTorzslap();
        }
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
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
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

        public static void checkDirs(GroupBox groupBoxEleresi, Label labelMentesiHely, Panel panel, string varString)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            string query = $"SELECT value FROM settings WHERE var = '{varString}'; ";
            var command = conn.CreateCommand();
            command.CommandText = query;
            var result = command.ExecuteScalar();
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
                fullPath = labelMentesiHely.Text;
            }
            conn.Close();
        }

        public static void setPathInDB(Label labelMentesiHely, GroupBox groupBoxEleresi, string eleresiUt)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
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
            foreach (var mappa in torlendoMappak)
            {
                File.Delete(mappa);
                MessageBox.Show("Mappa törlése:  " + mappa);
            }
        }

    }
}
