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
        public static string FileStorageRelativePath;
        public static string GlobKiterjesztes;
        public static bool GlobIsaMessageBoxOpen;
        public static string GlobSelectedButton;
        public static string GlobFeltoltendoFileEleresiUt;
        public static List<string> TorlendoMappak = new List<string>();

        private static MySqlConnection _conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");

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
            var command = CreateCommand(@"
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
            var command = CreateCommand(
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
            var command = CreateCommand(@"
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

            var command = CreateCommand(
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
            var command = CreateCommand(@"
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
            var command = CreateCommand(@"
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
            FileStorageRelativePath = labelMentesiHely;
        }
        public static void CheckDirs(GroupBox groupBoxEleresi, Label labelMentesiHely, Panel panel, string varString)
        {
            string query = $"SELECT value FROM settings WHERE var = @var; ";
            MySqlCommand cmd = CreateCommand(query);
            cmd.Parameters.AddWithValue("@var", varString);


            object result = cmd.ExecuteScalar();
            HandleReasultCheckDirs(groupBoxEleresi, labelMentesiHely, panel, result);
        }

        private static void HandleReasultCheckDirs(GroupBox groupBoxEleresi, Label labelMentesiHely, Panel panel, object result)
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
            foreach (var mappa in TorlendoMappak)
            {
                Directory.Delete(mappa, recursive: true);
                MessageBox.Show("Mappa törlése:  " + mappa);
            }
        }


        //Database függvények
        public static void SetPathInDb(string labelMentesiHely, GroupBox groupBoxEleresi, string eleresiUt)
        {
            try
            {
                if (!PathsContainSpecChars(labelMentesiHely, labelMentesiHely))
                {
                    string sql = "INSERT INTO " +
                                 "settings " +
                                 "VALUES" +
                                 "(" +
                                 "NULL, " +
                                 "@var, " +
                                 "@value " +
                                 ")";

                    var cmd = CreateCommand(sql);
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

        private static bool IsConnectionOpen()
        {
            return _conn.State == ConnectionState.Open;
        }

        public static MySqlConnection __getConnection()
        {
            if (!IsConnectionOpen())
            {
                _conn.Close();
                _conn.Open();
            }

            return _conn;
        }
        public static MySqlCommand CreateCommand(string sqlQuery)
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
            catch (ArgumentException aEx)
            {
                ThrowArgumentException(messageBox, aEx);
            }
            catch (MySqlException ex)
            {
                ThrowMySqlException(messageBox, ex);
            }

            return false;
        }

        private static void ThrowArgumentException(bool messageBox, ArgumentException aEx)
        {
            GlobIsaMessageBoxOpen = true;
            if (messageBox)
            {
                MessageBox.Show($"Hibás connection string!\n{aEx.Message}\n{aEx}");
            }

            GlobIsaMessageBoxOpen = false;
        }

        private static void ThrowMySqlException(bool showMessageBox, MySqlException ex)
        {
            string sqlErrorMessage = $"Üzenet: {ex.Message}\nForrás: {ex.Source}\nSzám: {ex.Number}";
            GlobIsaMessageBoxOpen = true;
            if (showMessageBox)
            {
                MessageBox.Show(sqlErrorMessage);
                GlobIsaMessageBoxOpen = false;
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

            var cmd = CreateCommand(
                $"SELECT id, {rowTneve}, {rowVkezdet}, {rowExtra}, {rowAnyja} " +
                    $"FROM {from} " +
                    $"WHERE {rowTneve} like '%{likeTanuloNeve}%' " +
                    $"AND {rowAnyja} like '%{likeAnyjaNeve}%' " +
                    $"AND {rowVkezdet} like '%{likeVkezdet}%'"
            );

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
                    var id2 = GetValues(
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

        private static string GetValues(
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
        public static void FileFeltolteseBDreEsFileMozgatasa(
                                                            string textBoxAnyjaNeveFeltolt,
                                                            string textBoxTanuloNeveFeltolt,
                                                            string hovaMasolja,
                                                            dynamic radioButtonOszFeltolt,
                                                            dynamic numericUpDownEvFeltoltKezdet,
                                                            string into,
                                                            string rowVevKezdet,
                                                            string rowTavaszVosz,
                                                            string rowDokLegutobbModositva
        )
        {
            try
            {

                byte tavaszOsz = 0;
                int returnValue = -1;
                tavaszOsz = SetTavaszOrOszValue(radioButtonOszFeltolt, tavaszOsz);

                if (hovaMasolja.IndexOfAny(SpecialChars) == -1)
                {
                    var sql = SetSqlCommandInsertInto(into, rowVevKezdet, rowTavaszVosz, rowDokLegutobbModositva);

                    var cmd = CreateCommand(sql);

                    AddParametersToCmdVizsgaEvKezdet(numericUpDownEvFeltoltKezdet, rowVevKezdet, cmd);

                    AddParametersToCmdTavaszOrOsz(radioButtonOszFeltolt, rowTavaszVosz, cmd, tavaszOsz);

                    AddParametersToCmdRemaining(textBoxAnyjaNeveFeltolt, textBoxTanuloNeveFeltolt, rowDokLegutobbModositva, cmd);

                    object modified = cmd.ExecuteScalar();
                    returnValue = TryGetLastId(modified, returnValue);
                    //MessageBox.Show("Feltöltés köv id: " + returnValue.ToString());
                    string destination = FileStorageRelativePath + hovaMasolja + returnValue + ".dat";
                    File.Copy(GlobFeltoltendoFileEleresiUt, destination);

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

        private static byte SetTavaszOrOszValue(dynamic radioButtonOszFeltolt, byte tavaszOrOsz)
        {
            if (radioButtonOszFeltolt is RadioButton)
            {
                tavaszOrOsz = radioButtonOszFeltolt.Checked ? (byte) 0 : (byte) 1;
            }

            return tavaszOrOsz;
        }

        private static int TryGetLastId(object modified, int returnValue)
        {
            if (modified != null)
            {
                int.TryParse(modified.ToString(), out returnValue);
            }

            return returnValue;
        }

        private static void AddParametersToCmdTavaszOrOsz(
                                                        dynamic radioButtonOszFeltolt,
                                                        string rowTavaszVosz,
                                                        MySqlCommand cmd,
                                                        byte tavaszOsz
        )
        {
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
        }

        private static void AddParametersToCmdVizsgaEvKezdet(
                                                            dynamic numericUpDownEvFeltoltKezdet,
                                                            string rowVevKezdet,
                                                            MySqlCommand cmd
        )
        {
            if (numericUpDownEvFeltoltKezdet is NumericUpDown)
            {
                cmd.Parameters.AddWithValue($"@{rowVevKezdet}", numericUpDownEvFeltoltKezdet.Value);
            }
            else if (numericUpDownEvFeltoltKezdet is TextBox)
            {
                cmd.Parameters.AddWithValue($"@{rowVevKezdet}", int.Parse(numericUpDownEvFeltoltKezdet.Text));
            }
        }

        private static void AddParametersToCmdRemaining(
                                                    string textBoxAnyjaNeveFeltolt,
                                                    string textBoxTanuloNeveFeltolt,
                                                    string rowDokLegutobbModositva,
                                                    MySqlCommand cmd
        )
        {
            cmd.Parameters.AddWithValue($"@rowTanuloNeve", textBoxTanuloNeveFeltolt);
            cmd.Parameters.AddWithValue($"@rowAnyja", textBoxAnyjaNeveFeltolt);
            cmd.Parameters.AddWithValue($"@rowSzerzo", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            cmd.Parameters.AddWithValue($"@{rowDokLegutobbModositva}", File.GetLastWriteTime(GlobFeltoltendoFileEleresiUt));
            cmd.Parameters.AddWithValue($"@rowFeltoltesIdopontja", DateTime.Now);
            cmd.Parameters.AddWithValue($"@filename", Path.GetFileName(GlobFeltoltendoFileEleresiUt));
        }

        private static string SetSqlCommandInsertInto(
                                                    string into,
                                                    string rowVevKezdet,
                                                    string rowTavaszVosz,
                                                    string rowDokLegutobbModositva
        )
        {
            return "INSERT INTO " +
                         $"{into} " +
                         "VALUES" +
                         "(" +
                         $"NULL, " +
                         $"@rowTanuloNeve, " +
                         $"@rowAnyja, " +
                         $"@rowSzerzo, " +
                         $"@{rowVevKezdet}, " +
                         $"@{rowTavaszVosz}, " +
                         $"@{rowDokLegutobbModositva}," +
                         $"@rowFeltoltesIdopontja," +
                         $"@filename " +
                         ");" +
                         "SELECT LAST_INSERT_ID();"
                ;
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
                var cmd = CreateCommand(sql);
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
                    var sql = SetSqlCommandUpdate(
                                                        textBoxAnyjaNeveModositas,
                                                        textBoxNevModositas,
                                                        numericUpDownEvKezdetModositas,
                                                        update,
                                                        rowVkezdet,
                                                        rowTavaszVosz,
                                                        rowId,
                                                        tavaszVosz
                    );
                    var cmd = CreateCommand(sql);

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

        private static string SetSqlCommandUpdate(
                                                string textBoxAnyjaNeveModositas,
                                                string textBoxNevModositas,
                                                int numericUpDownEvKezdetModositas,
                                                string update,
                                                string rowVkezdet,
                                                string rowTavaszVosz,
                                                string rowId,
                                                int tavaszVosz
        )
        {
            return
                    "UPDATE " +
                    $"{update} " +
                    "SET " +
                    $"tanuloNeve = '{textBoxNevModositas}', " +
                    $"anyjaNeve = '{textBoxAnyjaNeveModositas}', " +
                    $"{rowVkezdet} = {numericUpDownEvKezdetModositas} , " +
                    $"{rowTavaszVosz} = {tavaszVosz} " +
                    "WHERE " +
                    $"id = '{rowId}';"
                ;
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
            string tempDirectory = Path.Combine(Path.GetTempPath(), "nyilvantarto_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
            Directory.CreateDirectory(tempDirectory);
            string filePathInTemp = Path.Combine(tempDirectory, originalFileName);
            try
            {
                File.Copy(filePathInDb, filePathInTemp);
                
                Process.Start(filePathInTemp);
                TorlendoMappak.Add(tempDirectory);
            }

            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
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
            var cmd = CreateCommand($"SELECT filename FROM {tableName} WHERE id = @id");
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
            MySqlCommand cmd = CreateCommand("Select value From settings Where var = @var");
            cmd.Parameters.AddWithValue("@var", "eleresiUt");
            FileStorageRelativePath = cmd.ExecuteScalar().ToString();
            cmd.Parameters.Clear();
            //MessageBox.Show("Elérési út: " + fileStorageRelativePath);
        }
        public static void Tallozas(TextBox textBoxFilename)
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

                GlobKiterjesztes = Path.GetExtension(fileNameWExtension);
                textBoxFilename.Text = fileNameWExtension;

                GlobFeltoltendoFileEleresiUt = openFileDialog1.FileName;
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
