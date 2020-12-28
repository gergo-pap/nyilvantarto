using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Nyilvantarto_v2
{
    class Controll
    {
        public static string fileStorageRelativePath;
        public static string globKiterjesztes;
        public static bool globIsaMessageBoxOpen;
        public static string globSelectedButton;
        public static string globFeltoltendoFileEleresiUt;
        public static List<string> torlendoMappak = new List<string>();

        private static MySqlConnection __conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");

        private static readonly char[] SpecialChars = "!@#$%^&*()".ToCharArray();

        //Create tables
        public static void CreateTables()
        {
            CreateTableSettings();
            CreateTableKozepiskolaAnyakonyv();
            CreateTableSzakmaivizsgaAnyakonyv();
            CreateTableSzakmaivizsgaTorzslap();
            CreateTableErettsegiTanusitvany();
            CreateTableErettsegiTorzslap();
        }
        public static void CreateTableSettings()
        {
            var command = createCommand(@"
                            CREATE TABLE IF NOT EXISTS 
                                settings 
                                (
                                id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                var TEXT NOT NULL,
                                value TEXT NOT NULL
                                );"
            );
            command.ExecuteNonQuery();
        }
        public static void CreateTableKozepiskolaAnyakonyv()
        {
            var command = createCommand(
                    @"
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
                                filename TEXT NULL
                                );"
                );
            command.ExecuteNonQuery();
        }
        public static void CreateTableSzakmaivizsgaAnyakonyv()
        {
            var command = createCommand(@"
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
                                filename TEXT NULL
                                );
                                ");
            command.ExecuteNonQuery();
        }
        public static void CreateTableSzakmaivizsgaTorzslap()
        {

            var command = createCommand(
                @"
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
                                filename TEXT NULL
                                );
                                ");
            command.ExecuteNonQuery();
        }
        public static void CreateTableErettsegiTanusitvany()
        {
            var command = createCommand(@"
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
                                filename TEXT NULL
                                );
                                ");
            command.ExecuteNonQuery();
        }
        public static void CreateTableErettsegiTorzslap()
        {
            var command = createCommand(@"
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
                                filename TEXT NULL
                                );
                                ");
            command.ExecuteNonQuery();
        }


        //Directiories
        public static void CreateDirectiories(string labelMentesiHely)
        {
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Középiskola\Anyakönyv\");
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Szakmai Vizsga\Anyakönyv\");
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Szakmai Vizsga\Törzslap\");
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Érettségi\Törzslap\");
            Directory.CreateDirectory(labelMentesiHely + @"\Adatok\Érettségi\Tanusítvány\");
            fileStorageRelativePath = labelMentesiHely;
        }
        public static void CheckDirs(GroupBox groupBoxEleresi, Label labelMentesiHely, Panel panel, string varString)
        {
            string query = $"SELECT value FROM settings WHERE var = '{varString}'; ";
            MySqlCommand command = createCommand(query);
            object result = command.ExecuteScalar();
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
                loadFileStorageRelativePath();
                // fileStorageRelativePath = labelMentesiHely.Text;
            }
        }
        public static void DeleteDirsInTemp()
        {
            foreach (var mappa in torlendoMappak)
            {
                Directory.Delete(mappa, true); //TODO
                MessageBox.Show("Mappa törlése:  " + mappa);
            }
        }


        //Database függvények
        public static void SetPathInDB(string labelMentesiHely, GroupBox groupBoxEleresi, string eleresiUt)
        {
            try
            {
                if (!pathContainSpecChars(labelMentesiHely, labelMentesiHely))
                {
                    string SQL = "INSERT INTO " +
                                 "settings " +
                                 "VALUES" +
                                 "(" +
                                 "NULL, " +
                                 "@var, " +
                                 "@value " +
                                 ")";

                    var cmd = createCommand(SQL);
                    cmd.Parameters.AddWithValue("@var", eleresiUt);
                    cmd.Parameters.AddWithValue("@value", labelMentesiHely);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("SQL hiba " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            groupBoxEleresi.Visible = false;
        } 
        private static bool isConnectionOpen()
        {
            return __conn.State == ConnectionState.Open;
        }
        public static MySqlConnection __getConnection()
        {
            if (!isConnectionOpen())
            {
                __conn.Close();
                __conn.Open();
            }

            return __conn;
        }
        public static MySqlCommand createCommand(string sqlQuery)
        {
            return new MySqlCommand(sqlQuery, __getConnection());
        }
        public static bool CheckDB_Conn(bool messageBox)
        {
            try
            {
                __getConnection();
                return true;
            }
            catch (ArgumentException a_ex)
            {
                globIsaMessageBoxOpen = true;
                if (messageBox)
                {
                    MessageBox.Show($"Hibás connection string!\n{a_ex.Message}\n{a_ex}");
                }
                globIsaMessageBoxOpen = false;
            }
            catch (MySqlException ex)
            {
                string sqlErrorMessage = $"Üzenet: {ex.Message}\nForrás: {ex.Source}\nSzám: {ex.Number}";
                globIsaMessageBoxOpen = true;
                if (messageBox)
                {
                    MessageBox.Show(sqlErrorMessage);
                    globIsaMessageBoxOpen = false;
                    switch (ex.Number)
                    {
                        case 1042:
                            MessageBox.Show("Nem lehet csatlakozni a MySql hosthoz! (Check Server, Port)");
                            break;
                        case 0:
                            MessageBox.Show("Hozzáférés megtagadva! (Check DB name, username, password)");
                            break;
                        default:
                            break;
                    }
                }
            }

            return false;
        }


        //Datagridview függvények
        public static void dataGridViewBasicSettings(DataGridView dataGridView, Panel panelKeres)
        {
            //Global.dataGridViewKeresesEredmenyeiClear(dataGridView1);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            if (!dataGridView.Visible || !panelKeres.Visible)
            {
                dataGridView.Visible = true;
                panelKeres.Visible = true;
            }
        }
        public static void dataGridViewClear(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
        }
        public static void datagridviewKeres(
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

            var command = createCommand(
                $"SELECT id, {rowTneve}, {rowVkezdet}, {rowExtra}, {rowAnyja} " +
                    $"FROM {from} " +
                    $"WHERE {rowTneve} like '%{likeTanuloNeve}%' " +
                    $"AND {rowAnyja} like '%{likeAnyjaNeve}%' " +
                    $"AND {rowVkezdet} like '%{likeVkezdet}%'"
            );

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
        }
        public static void DataGridViewOffline(DataGridView dataGridView, Panel panel1, Panel panel2)
        {
            dataGridView.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
        }
        



        //Feltölt, töröl, módosít
        public static void FileFeltolteseBDreESFileMozgatasa(TextBox textBoxAnyjaNeveFeltolt,
                    TextBox textBoxTanuloNeveFeltolt,
                    string hovaMasolja,
                    dynamic radioButtonOszFeltolt,
                    dynamic numericUpDownEvFeltoltKezdet,
                    string @into,
                    string rowTneve,
                    string rowAnyja,
                    string rowSzerzo,
                    string rowVevKezdet,
                    string rowTavaszVosz,
                    string rowDokLegutobbModositva,
                    string rowFeltoltesIdopontja,
                    string rowFilename)
        {
            try
            {

                byte tavaszOsz = 0;
                int returnValue = -1;
                if (radioButtonOszFeltolt is RadioButton)
                {
                    tavaszOsz = radioButtonOszFeltolt.Checked ? (byte)0 : (byte)1;
                }

                if (!pathContainSpecChars(rowFilename, hovaMasolja))
                {
                    string SQL = "INSERT INTO " +
                            $"{into} " +
                            "VALUES" +
                            "(" +
                                    $"NULL, " +
                                    $"@{rowTneve}, " +
                                    $"@{rowAnyja}, " +
                                    $"@{rowSzerzo}, " +
                                    $"@{rowVevKezdet}, " +
                                    $"@{rowTavaszVosz}, " +
                                    $"@{rowDokLegutobbModositva}," +
                                    $"@{rowFeltoltesIdopontja}," +
                                    $"@{rowFilename} " +
                            ");" +

                            "SELECT LAST_INSERT_ID();"
                            ;

                    var cmd = createCommand(SQL);
                    cmd.Parameters.AddWithValue($"@{rowTneve}", textBoxTanuloNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue($"@{rowAnyja}", textBoxAnyjaNeveFeltolt.Text);
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
                        cmd.Parameters.AddWithValue($"@{rowTavaszVosz}", tavaszOsz);
                    }
                    else if (radioButtonOszFeltolt is NumericUpDown)
                    {
                        cmd.Parameters.AddWithValue($"@{rowTavaszVosz}", radioButtonOszFeltolt.Value);
                    }
                    else if (radioButtonOszFeltolt is TextBox)
                    {
                        cmd.Parameters.AddWithValue($"@{rowTavaszVosz}", int.Parse(radioButtonOszFeltolt.Text));
                    }

                    cmd.Parameters.AddWithValue($"@{rowDokLegutobbModositva}", File.GetLastWriteTime(Controll.globFeltoltendoFileEleresiUt));
                    cmd.Parameters.AddWithValue($"@{rowFeltoltesIdopontja}", DateTime.Now);
                    cmd.Parameters.AddWithValue($"@{rowFilename}", Path.GetFileName(Controll.globFeltoltendoFileEleresiUt));

                    object modified = cmd.ExecuteScalar();
                    if (modified != null)
                    {
                        int.TryParse(modified.ToString(), out returnValue);
                    }
                    //MessageBox.Show("Feltöltés köv id: " + returnValue.ToString());
                    string destination = Controll.fileStorageRelativePath + hovaMasolja + returnValue + ".dat";
                    File.Copy(Controll.globFeltoltendoFileEleresiUt, destination);


                    MessageBox.Show("Sikeres feltöltés!",
                    "Siker!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cmd.Parameters.Clear();
                }
                else
                {
                    MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*");
                }
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
        public static void Torles(string id, string from, string destination)
        {
            string torlendo = $@"{fileStorageRelativePath}{destination}\{id}.dat";
            try
            {
                string SQL = "DELETE FROM " +
                             $"{from} " +
                             "WHERE " +
                             "id =" + id
                    ;
                var cmd = createCommand(SQL);
                cmd.Parameters.AddWithValue($"@{from}", from);

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
        public static void Modositas(TextBox textBoxAnyjaNeveModositas,
                                TextBox textBoxNevModositas,
                                RadioButton radioButtonTavaszModosit,
                                TextBox tbOther,
                                NumericUpDown numericUpDownEvKezdetModositas,
                                NumericUpDown numericUpDown2,
                                string update,
                                string rowTneve,
                                string rowAnyjaNeve,
                                string rowVkezdet,
                                string rowTavaszVosz,
                                string rowId)
        {
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
                tavaszVosz = int.Parse(tbOther.Text);
            }
            if (numericUpDown2 != null)
            {
                tavaszVosz = int.Parse(numericUpDown2.Value.ToString());
            }
            if (!pathContainSpecChars(textBoxAnyjaNeveModositas.Text, textBoxNevModositas.Text))
            {
                try
                {
                    string SQL =
                                "UPDATE " +
                                $"{update} " +
                                "SET " +
                                $"{rowTneve} = '" + textBoxNevModositas.Text + "', " +
                                $"{rowAnyjaNeve} = '" + textBoxAnyjaNeveModositas.Text + "', " +
                                $"{rowVkezdet} = {numericUpDownEvKezdetModositas.Value} , " +
                                $"{rowTavaszVosz} = {tavaszVosz}  " +
                                "WHERE " +
                                $"id = '{rowId}';"
                                ;
                    var cmd = createCommand(SQL);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Sikeres módosítás");
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
        public static void LoadSelectedDataWhenModifying(DataGridView dataGridView,
                                TextBox tbAnyja,
                                TextBox tbTan,
                                NumericUpDown numericUpDown,
                                TextBox tb,
                                RadioButton radioTavaszTrue,
                                RadioButton radioOszTrue,
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



        //File keresése
        public static void SearchFileInFileExplorer(string specDir, string id, string tableName)
        {
            var getDataCommand = createCommand($"SELECT filename FROM {tableName} WHERE id = @id");
            getDataCommand.Parameters.AddWithValue("@id", id);
            var originalFileName = getDataCommand.ExecuteScalar().ToString();
            MessageBox.Show($"original filename from db: {originalFileName}");
            string kiterjFromDB = Path.GetExtension(originalFileName);
            string filePathInDb = $"{Controll.fileStorageRelativePath + specDir}\\{id}.dat";
            if (!File.Exists(filePathInDb))
            {
                MessageBox.Show("Nincs meg a File!" + filePathInDb);
                return;
            }
            string tempDirectory = Path.Combine(Path.GetTempPath(), "nyilvantarto_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
            Directory.CreateDirectory(tempDirectory);
            string filePathInTemp = Path.Combine(tempDirectory, originalFileName);
            try
            {
                File.Copy(filePathInDb, filePathInTemp);

                //Process fileopener = new Process();
                //fileopener.StartInfo.FileName = fileInTemp;
                //fileopener.StartInfo.Arguments = argument;
                //fileopener.Start();


                // eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                MessageBox.Show($"ezt nyitja meg: {filePathInTemp}");
                Process.Start(filePathInTemp);
                torlendoMappak.Add(tempDirectory);
                MessageBox.Show($"tempodir: {tempDirectory}");
            }

            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
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
        public static bool pathContainSpecChars(string s1, string s2)
        {
            return s1.IndexOfAny(SpecialChars) != -1 || s2.IndexOfAny(SpecialChars) != -1;
        }

        public static bool CheckIfEmptyInput4TextBox(TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4)
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

        public static void loadFileStorageRelativePath()
        {
            MySqlCommand cmd = createCommand("Select value From settings Where var = @var");
            cmd.Parameters.AddWithValue("@var", "eleresiUt");
            var result = cmd.ExecuteScalar();
            fileStorageRelativePath = result.ToString();
            cmd.Parameters.Clear();
            //MessageBox.Show("Elérési út: " + fileStorageRelativePath);
        }
        public static void tallozas(TextBox textBoxFilename)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                MessageBox.Show("Hiba");
            }
            else
            {
                string fileNameWExtension = Path.GetFileName(openFileDialog1.FileName);

                globKiterjesztes = Path.GetExtension(fileNameWExtension);
                textBoxFilename.Text = fileNameWExtension;

                globFeltoltendoFileEleresiUt = openFileDialog1.FileName;
            }
        }
        public static void buttonClickClear(TextBox textBoxTanuloNeve, TextBox textBoxAnyjaNeve, NumericUpDown numericUpDown, CheckBox checkBox)
        {
            textBoxTanuloNeve.Clear();
            textBoxAnyjaNeve.Clear();
            numericUpDown.ResetText();
            checkBox.Checked = false;
        }
        public static void FeltoltUrit(TextBox t1,
                                        TextBox t2,
                                        NumericUpDown n1,
                                        NumericUpDown n2,
                                        RadioButton rTrue,
                                        RadioButton rFalse,
                                        TextBox t3,
                                        TextBox t4)
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
            if (t3 != null)
            {
                t3.Clear();
            }
        }
        public static void Tallozas(string labelMentesiHely)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath;
                labelMentesiHely = sSelectedPath;
            }
            else
            {
                MessageBox.Show("Hiba");
            }
        }

        public static void FirstClickShow(
            Panel panel1,
            Panel panel2,
            DataGridView dataGridView)
        {
            if (!panel1.Visible)
            {
                panel1.Visible = true;
                panel2.Visible = true;
                dataGridView.Visible = true;
            }
        }
    }
}
