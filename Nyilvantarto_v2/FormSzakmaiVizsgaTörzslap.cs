using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;

namespace Nyilvantarto_v2
{
    public partial class FormSzakmaiVizsgaTörzslap : Form
    {
        string kiterjesztes;
        MySqlConnection conn;
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        private static readonly char[] SpecialChars = "!@#$%^&*()-".ToCharArray();
        string destPath = Globális.path + @"\Adatok\Szakmai vizsga\Törzslap\";
        //string filePath = destPath + nev + '_' + evKezdet + '_' + tavaszVosz + '_' + anyja + "." + formatum;
        string globNev;
        string globEvKezdet;
        int globTavaszVoszInt;
        string globTavaszVoszTrueOrFalse;
        string globTavaszVOszString;
        string globAnyja;
        string globFormatum;
        public FormSzakmaiVizsgaTörzslap()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;") ;
            conn.Open();
            CreateTableDokumentumok();
            getDestPathFromDatabase();
        }

        private void getDestPathFromDatabase()
        {
            var command = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select s_value From settings Where s_var = @Title";
            cmd.Parameters.AddWithValue("@Title", "eleresiUt");
            var result = cmd.ExecuteScalar();
            Globális.path = result.ToString() + @"\Adatok\Szakmai Vizsga\Anyakönyv\";
        }

        private void CreateTableDokumentumok()
        {
            var command = conn.CreateCommand();
            command.CommandText = @"
                                CREATE TABLE IF NOT EXISTS 
                                    szakmaivizsgaTorzslap 
                                    (
                                    id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                    szvt_tanuloNeve TEXT NOT NULL,
                                    szvt_AnyjaNeve TEXT NOT NULL,
                                    szvt_szerzo TEXT NOT NULL,
                                    szvt_viszgaEvKezdet INT NOT NULL,
                                    szvt_viszgaTavasz1Osz0 BOOLEAN NOT NULL,
                                    szvt_dokumentumNev TEXT NOT NULL,
                                    szvt_dokLegutobbModositva DATETIME NOT NULL,
                                    szvt_feltoltesIdopontja DATETIME NOT NULL,
                                    szvt_formatum TEXT NOT NULL,
                                    szvt_path TEXT NULL
                                    );
                                    ";
            command.ExecuteNonQuery();
        }

        private void getFilesCount()
        {
            //DirectoryInfo d = new DirectoryInfo(destPath);//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.*"); //Getting files
            string[] fileArray = Directory.GetFiles(destPath);
            labelFileokSzama.Text = fileArray.Length.ToString();
        }

        private void getDBCount()
        {
            int count = 0;
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM szakmaivizsgaTorzslap", conn))
            {
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            labelDBCount.Text = count.ToString();
        }

        private void checkMissingFile()
        {
            List<string> dataOnPC = new List<string>();
            List<string> dataDB = new List<string>();
            string[] fileArray = Directory.GetFiles(destPath);
            foreach (var item in fileArray)
            {
                //MessageBox.Show(item.Split('\\').Last().Split('.').First());
                dataOnPC.Add(item.Split('\\').Last().Split('.').First());
            }
            //MessageBox.Show("PC: " + dataOnPC.Count);
            var command = conn.CreateCommand();
            command.CommandText = "SELECT szvt_tanuloNeve,szvt_viszgaEvKezdet,szvt_viszgaTavasz1Osz0,szvt_AnyjaNeve  FROM szakmaivizsgaTorzslap";
            using (var reader = command.ExecuteReader())
            {
                var szvt_tanuloNeve = reader.GetOrdinal("szvt_tanuloNeve");
                var szvt_viszgaEvKezdet = reader.GetOrdinal("szvt_viszgaEvKezdet");
                var szvt_viszgaTavasz1Osz0 = reader.GetOrdinal("szvt_viszgaTavasz1Osz0");
                var szvt_AnyjaNeve = reader.GetOrdinal("szvt_AnyjaNeve");

                while (reader.Read())
                {
                    var szvt_tanuloNeve2 = reader.GetValue(szvt_tanuloNeve).ToString();
                    var szvt_viszgaEvKezdet2 = reader.GetValue(szvt_viszgaEvKezdet).ToString();
                    var szvt_viszgaTavasz1Osz02 = reader.GetValue(szvt_viszgaTavasz1Osz0).ToString();
                    var szvt_AnyjaNeve2 = reader.GetValue(szvt_AnyjaNeve).ToString();
                    string oneDataDB = szvt_tanuloNeve2 + "_" + szvt_viszgaEvKezdet2 + "_" + boolConvert(bool.Parse(szvt_viszgaTavasz1Osz02)) + "_" + szvt_AnyjaNeve2;
                    //MessageBox.Show(oneDataDB);
                    dataDB.Add(oneDataDB);
                }
            }
            //MessageBox.Show("DB: " + dataDB.Count);
            var listAdatbazisbolHianyzo = dataOnPC.Where(x => !dataDB.Contains(x)).ToList();
            var listPCnHianyzo = dataDB.Where(x => !dataOnPC.Contains(x)).ToList();
            var listUniun = dataOnPC.Union(dataDB);
            string lines = string.Join(Environment.NewLine, listAdatbazisbolHianyzo);
            if (lines.Length > 0)
            {
                if (MessageBox.Show($"Adatbázisból hiányzó fileok: \n{lines}\nSzeretnéd törölni? a PC-ről?", "Hiányzó elemek törlése", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // user clicked yes
                    hianyzoElemeTorlesePCrol(lines);
                }
                else
                {
                    // user clicked no

                }
            }
            lines = string.Join(Environment.NewLine, listPCnHianyzo);
            if (lines.Length > 0)
            {
                if (MessageBox.Show($"PC-ről hiányzó fileok: \n{lines}\nSzeretnéd törölni az adatbázisból a bejegyzést róla?", "Hiányzó elemek törlése", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // user clicked yes
                    hianyzoElemekTorleseDBrol(lines);
                }
                else
                {
                    // user clicked no
                }
            }
        }

        private void hianyzoElemeTorlesePCrol(string lines)
        {
            string[] rows = lines.Split('\n');
            foreach (var item in rows)
            {
                string[] row = item.Split('_');
                string destination = destPath + row[0] + '_' + row[1] + '_' + row[2] + '_' + row[3] + ".*";
                MessageBox.Show(destination);

                string[] files = System.IO.Directory.GetFiles(destPath, item+".*");
                foreach (string f in files)
                {
                    System.IO.File.Delete(f);
                }
            }

        }

        private void hianyzoElemekTorleseDBrol(string lines)
        {
            string[] rows = lines.Split('\n');
            foreach (var item in rows)
            {
                string[] row = item.Split('_');
                string SQL = "DELETE FROM `szakmaivizsgatorzslap` " +
                    "WHERE " +
                    "`szvt_tanuloNeve` = '"+row[0]+"'and " +
                    "`szvt_AnyjaNeve` = '"+row[3]+"' and " +
                    "`szvt_viszgaEvKezdet` = " + row[1]
                    ;
                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();
            }
        }

        private void betolt()
        {
            try
            {
                string filePath = destPath + globNev + '_' + globEvKezdet + '_' + globTavaszVOszString + '_' + globAnyja + "." + globFormatum;
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Nincs meg a File!");
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

        private void tallozas()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK) // Test result.
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

                kiterjesztes = tombFile.Last();
                textBoxDokumentumNeve.Text = fileName;
                textBoxEleresi.Text = openFileDialog1.FileName;
            }
        }


        private void keres(string column, string textboxText)
        {
            listboxKeresesEredmenyeiClear();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT id,szvt_tanuloNeve,szvt_viszgaEvKezdet,szvt_viszgaTavasz1Osz0,szvt_AnyjaNeve  FROM szakmaivizsgaTorzslap WHERE " + column + " like '%" + textboxText + "%'";
            using (var reader = command.ExecuteReader())
            {
                var id = reader.GetOrdinal("id");
                var szvt_tanuloNeve = reader.GetOrdinal("szvt_tanuloNeve");
                var szvt_viszgaEvKezdet = reader.GetOrdinal("szvt_viszgaEvKezdet");
                var szvt_viszgaTavasz1Osz0 = reader.GetOrdinal("szvt_viszgaTavasz1Osz0");
                var szvt_AnyjaNeve = reader.GetOrdinal("szvt_AnyjaNeve");

                while (reader.Read())
                {
                    var id2 = reader.GetValue(id).ToString();
                    var szvt_tanuloNeve2 = reader.GetValue(szvt_tanuloNeve).ToString();
                    var szvt_viszgaEvKezdet2 = reader.GetValue(szvt_viszgaEvKezdet).ToString();
                    var szvt_viszgaTavasz1Osz02 = reader.GetValue(szvt_viszgaTavasz1Osz0).ToString();
                    var szvt_AnyjaNeve2 = reader.GetValue(szvt_AnyjaNeve).ToString();
                    listBoxId.Items.Add(id2);
                    listBoxKeresesEredmenyeTanuloNeve.Items.Add(szvt_tanuloNeve2);
                    listBoxVKezdete.Items.Add(szvt_viszgaEvKezdet2);
                    listBoxVVege.Items.Add(boolConvert(bool.Parse(szvt_viszgaTavasz1Osz02)));
                    listBoxAnyjaNeve.Items.Add(szvt_AnyjaNeve2);
                    // + "-" + szvt_viszgaEvKezdet2 + "-" + boolConvert(bool.Parse(szvt_viszgaTavasz1Osz02)) + "-" + szvt_AnyjaNeve2
                }
            }
        }

        private void listboxKeresesEredmenyeiClear()
        {
            listBoxId.Items.Clear();
            listBoxKeresesEredmenyeTanuloNeve.Items.Clear();
            listBoxVKezdete.Items.Clear();
            listBoxVVege.Items.Clear();
            listBoxAnyjaNeve.Items.Clear();
        }
        private void hozzaad()
        {
            try
            {
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
                    string szvt_eleresiUt = textBoxEleresi.Text;
                    FileStream fs = new FileStream(szvt_eleresiUt, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    string fileName = textBoxTanuloNeveFeltolt.Text + "_" + numericUpDownEvFeltoltKezdet.Value.ToString() + "_" + tavaszVOsz + "_" + textBoxAnyjaNeveFeltolt.Text;
                    fs.Close();

                    string SQL = "INSERT INTO " +
                        "szakmaivizsgaTorzslap " +
                        "VALUES" +
                        "(" +
                            "NULL, " +
                            "@szvt_tanuloNeve, " +
                            "@szvt_AnyjaNeve, " +
                            "@szvt_szerzo, " +
                            "@szvt_viszgaEvKezdet, " +
                            "@szvt_viszgaTavasz1Osz0, " +
                            "@szvt_dokumentumNev, " +
                            "@szvt_dokLegutobbModositva," +
                            "@szvt_feltoltesIdopontja," +
                            "@szvt_formatum, " +
                            "@szvt_path " +
                        ")";

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@szvt_tanuloNeve", textBoxTanuloNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue("@szvt_AnyjaNeve", textBoxAnyjaNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue("@szvt_szerzo", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    cmd.Parameters.AddWithValue("@szvt_viszgaEvKezdet", numericUpDownEvFeltoltKezdet.Value);
                    cmd.Parameters.AddWithValue("@szvt_viszgaTavasz1Osz0", tavaszOsz);
                    cmd.Parameters.AddWithValue("@szvt_dokumentumNev", fileName);
                    cmd.Parameters.AddWithValue("@szvt_dokLegutobbModositva", File.GetLastWriteTime(szvt_eleresiUt));
                    cmd.Parameters.AddWithValue("@szvt_feltoltesIdopontja", DateTime.Now);
                    cmd.Parameters.AddWithValue("@szvt_formatum", kiterjesztes);
                    cmd.Parameters.AddWithValue("@szvt_path", szvt_eleresiUt);

                    cmd.ExecuteNonQuery();

                    string destination = destPath + fileName + "." + kiterjesztes;
                    string source = szvt_eleresiUt;
                    MessageBox.Show("Forrás: " + source + "\nCél: " + destination);
                    // To move a file or folder to a new  location:

                    System.IO.File.Copy(source, destination);

                    MessageBox.Show("File Inserted into database successfully!",
                    "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBoxFeltoltUrites();
                    cmd.Parameters.Clear();
                }
                else
                {
                    MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("Fájl hiba!\n A fájl már létezik vagy nem található!");
            }
        }

        private void torles()
        {
            try
            {
                string id = listBoxId.SelectedItem.ToString();

                string SQL = "DELETE FROM " +
                        "szakmaivizsgaTorzslap " +
                        "WHERE " +
                        "id =" + id
                        ;
                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();

                string destination = destPath + globNev + '_' + globEvKezdet + '_' + globTavaszVOszString + '_' + globAnyja + '.' + globFormatum;

                System.IO.File.Delete(destination);

                MessageBox.Show("Sikeres törlés");
                
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
            getFilesCount();
        }

        private void modositas()
        {
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
                                "szakmaivizsgaTorzslap " +
                                "SET " +
                                "szvt_tanuloNeve = '" + textBoxNevModositas.Text + "', " +
                                "szvt_AnyjaNeve = '" + textBoxAnyjaneveModositas.Text + "', " +
                                "szvt_viszgaEvKezdet = " + numericUpDownEvKezdetModositas.Value + ", " +
                                "szvt_viszgaTavasz1Osz0 = " + tavaszVosz + " " +
                                "WHERE " +
                                "szvt_tanuloNeve = '" + globNev + "' AND " +
                                "szvt_AnyjaNeve =  '" + globAnyja + "' AND " +
                                "szvt_viszgaEvKezdet = " + globEvKezdet + " AND " +
                                "szvt_viszgaTavasz1Osz0 = " + globTavaszVoszInt + ";"
                                ;

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;

                    cmd.ExecuteNonQuery();


                    string destFileName = textBoxNevModositas.Text + '_' + numericUpDownEvKezdetModositas.Value + '_' + tavaszVOszModosit + '_'+ textBoxAnyjaneveModositas.Text;
                    System.IO.File.Move(destPath + globNev + '_' + globEvKezdet + '_' + globTavaszVOszString + '_' + globAnyja + '.' + globFormatum, destPath + destFileName + '.' + globFormatum);

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

        private void textBoxFeltoltUrites()
        {
            textBoxDokumentumNeve.Clear();
            textBoxEleresi.Clear();
            textBoxTanuloNeveFeltolt.Clear();
            textBoxAnyjaNeveFeltolt.Clear();
        }

        private bool checkIfEmptyInput()
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

        private void borderColorReset()
        {
            textBoxAnyjaNeveFeltolt.BackColor = Color.White;
            textBoxDokumentumNeve.BackColor = Color.White;
            textBoxTanuloNeveFeltolt.BackColor = Color.White;
            textBoxEleresi.BackColor = Color.White;
        }

        private string boolConvert(bool value)
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

        private void textBoxTanuloNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            if (textBoxTanuloNeveFeltolt.Text.Length == 0)
            {
                textBoxTanuloNeveFeltolt.BackColor = Color.Red;
            }
            else
            {
                textBoxTanuloNeveFeltolt.BackColor = Color.White;
            }
        }

        private void textBoxAnyjaNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAnyjaNeveFeltolt.Text.Length == 0)
            {
                textBoxAnyjaNeveFeltolt.BackColor = Color.Red;
            }
            else
            {
                textBoxAnyjaNeveFeltolt.BackColor = Color.White;
            }
        }

        private void textBoxDokumentumNeve_TextChanged(object sender, EventArgs e)
        {
            if (textBoxDokumentumNeve.Text.Length == 0)
            {
                textBoxDokumentumNeve.BackColor = Color.Red;
            }
            else
            {
                textBoxDokumentumNeve.BackColor = Color.White;
            }
        }

        private void textBoxEleresi_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEleresi.Text.Length == 0)
            {
                textBoxEleresi.BackColor = Color.Red;
            }
            else
            {
                textBoxEleresi.BackColor = Color.White;
            }
        }

        private void buttonTallozas_Click(object sender, EventArgs e)
        {
            tallozas();
        }

        private void buttonFeltoltes_Click(object sender, EventArgs e)
        {
            if (checkIfEmptyInput())
            {
                hozzaad();
                textBoxFeltoltUrites();
                borderColorReset();
            }
            getFilesCount();
        }

        private void textBoxTanuloNeveKeres_TextChanged(object sender, EventArgs e)
        {
            keres("szvt_tanuloNeve", textBoxTanuloNeveKeres.Text);
        }

        private void textBoxAnyjaNeveKeres_TextChanged(object sender, EventArgs e)
        {
            keres("szvt_AnyjaNeve", textBoxAnyjaNeveKeres.Text);
        }

        private void numericUpDownVizsgaKezdetKeres_ValueChanged(object sender, EventArgs e)
        {
            keres("szvt_viszgaEvKezdet", numericUpDownVizsgaKezdetKeres.Value.ToString());
        }

        private void buttonLetoltes_Click(object sender, EventArgs e)
        {
            betolt();
        }

        private void buttonTorles_Click(object sender, EventArgs e)
        {
            groupBoxJelszo.Visible = true;
            if (textBoxJelszo.Text == "12345")
            {
                torles();
                groupBoxJelszo.Visible = false;
                textBoxJelszo.Clear();
                listBoxKeresesEredmenyeTanuloNeve.Items.Clear();
            }
        }

        private void buttonModositas_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                modositas();
                groupBox1.Visible = false;
                listBoxKeresesEredmenyeTanuloNeve.Items.Clear();
            }
            else
            {
                groupBox1.Visible = true;
            }
        }

        private void FormSzakmaiVizsgaTörzslap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void radioButtonTavaszKeres_CheckedChanged(object sender, EventArgs e)
        {
            keres("szvt_viszgaTavasz1Osz0", (radioButtonTavaszKeres.Checked ? 1 : 1).ToString());
        }

        private void radioButtonOszKeres_CheckedChanged(object sender, EventArgs e)
        {
            keres("szvt_viszgaTavasz1Osz0", (radioButtonOszKeres.Checked ? 0 : 0).ToString());
        }

        private void numericUpDownEvKezdetModositas_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            (new FormMain()).Show(); this.Hide();
        }

        private void FormSzakmaiVizsgaTörzslap_Enter(object sender, EventArgs e)
        {
            torles();
        }

        private void textBoxJelszo_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxJelszo.Text == "12345")
                {
                    torles();
                    groupBoxJelszo.Visible = false;
                    textBoxJelszo.Clear();
                    listboxKeresesEredmenyeiClear();
                }
                e.Handled = true;
            }
            
        }

        private void listBoxId_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var getData = conn.CreateCommand();
            string idIn = listBoxId.SelectedItem.ToString();

            getData.CommandText = "SELECT " +
                "szvt_tanuloNeve," +
                "szvt_viszgaEvKezdet," +
                "szvt_viszgaTavasz1Osz0," +
                "szvt_AnyjaNeve  " +
                "FROM " +
                "szakmaivizsgaTorzslap " +
                "WHERE id = " + idIn
                ;

            //var result = getData.ExecuteScalar();
            using (var reader = getData.ExecuteReader())
            {
                var szvt_tanuloNeve = reader.GetOrdinal("szvt_tanuloNeve");
                var szvt_viszgaEvKezdet = reader.GetOrdinal("szvt_viszgaEvKezdet");
                var szvt_viszgaTavasz1Osz0 = reader.GetOrdinal("szvt_viszgaTavasz1Osz0");
                var szvt_AnyjaNeve = reader.GetOrdinal("szvt_AnyjaNeve");

                while (reader.Read())
                {
                    globNev = reader.GetValue(szvt_tanuloNeve).ToString();
                    globEvKezdet = reader.GetValue(szvt_viszgaEvKezdet).ToString();
                    globTavaszVoszTrueOrFalse = reader.GetValue(szvt_viszgaTavasz1Osz0).ToString();
                    globAnyja = reader.GetValue(szvt_AnyjaNeve).ToString();
                }
                if (globTavaszVoszTrueOrFalse == "True")
                {
                    globTavaszVoszInt = 1;
                    globTavaszVOszString = "Tavasz";
                }
                else
                {
                    globTavaszVoszInt = 0;
                    globTavaszVOszString = "Ősz";
                }
                conn.Close();
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT szvt_formatum " +
                    "FROM " +
                    "szakmaivizsgaTorzslap " +
                    "WHERE " +
                    "szvt_tanuloNeve = '" + globNev +
                    "' AND " +
                    "szvt_AnyjaNeve = '" + globAnyja +
                    "' AND " +
                    "szvt_viszgaEvKezdet = " + globEvKezdet +
                    " AND " +
                    "szvt_viszgaTavasz1Osz0 = '" + globTavaszVoszInt + "'"
                    ;
                var result = command.ExecuteScalar();

                globFormatum = result.ToString();
            }
        }

        private void FormSzakmaiVizsgaTörzslap_Load(object sender, EventArgs e)
        {
            getFilesCount();
            getDBCount();
            checkMissingFile();
        }
    }
}
