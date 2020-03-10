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
        string destPath = @"c:\Users\pap.gergo\Desktop\Teszt\Szakmai vizsga\Törzslap\";

        public FormSzakmaiVizsgaTörzslap()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=");
            conn.Open();
            CreateTableDokumentumok();
        }

        private void CreateTableDokumentumok()
        {
            var command = conn.CreateCommand();
            command.CommandText = @"
                                CREATE TABLE IF NOT EXISTS 
                                    szakmaiVizsgaTorzslap 
                                    (
                                    id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                    szvt_tanuloNeve TEXT NOT NULL,
                                    szvt_erettsegiEve INT NOT NULL,
                                    szvt_szerzo TEXT NOT NULL,
                                    szvt_DokumentumNev TEXT NOT NULL,
                                    szvt_DokLegutobbModositva DATETIME NOT NULL,
                                    szvt_FeltoltesIdopontja DATETIME NOT NULL,
                                    szvt_formatum TEXT NOT NULL,
                                    szvt_file MEDIUMBLOB DEFAULT NULL,
                                    szvt_fileSize INT NOT NULL
                                    );
                                    ";
            command.ExecuteNonQuery();
            //AdatFrissit();
        }

        private void betolt()
        {
            MySql.Data.MySqlClient.MySqlDataReader myData;

            string SQL;

            string[] keresesKijelolt = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
            string nev = keresesKijelolt[0];
            string ev = keresesKijelolt[1];
            string anyja = keresesKijelolt[2];

            SQL = "SELECT * FROM `szakmaiVizsgaTorzslap` WHERE `szvt_tanuloNeve` LIKE '%" + nev + "%' AND `szvt_AnyjaNeve` LIKE '%" + anyja + "%' AND `szvt_erettsegiEve` LIKE '%" + ev + "%'";

            try
            {
                cmd.Connection = conn;
                cmd.CommandText = SQL;

                myData = cmd.ExecuteReader();

                if (!myData.HasRows)
                {
                    throw new Exception("Nincs egy fájl se az adatbézisban");
                }

                myData.Read();

                var szvt_DokumentumNev = myData.GetOrdinal("szvt_DokumentumNev");
                var szvt_formatum = myData.GetOrdinal("szvt_formatum");
                var szvt_path = myData.GetOrdinal("szvt_path");

                var szvt_DokumentumNev2 = myData.GetValue(szvt_DokumentumNev).ToString();
                var szvt_formatum2 = myData.GetValue(szvt_formatum).ToString();
                var szvt_path2 = myData.GetValue(szvt_path).ToString();

                string filePath = destPath + szvt_DokumentumNev2 + "." + szvt_formatum2;

                int fileNameLenght = szvt_DokumentumNev2.Length + szvt_formatum2.ToString().Length + 1;

                //MessageBox.Show(szvt_path2.Remove(fileNameLenght));

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Nincs meg a File!");
                    return;
                }

                string argument = "/select, \"" + filePath + "\"";

                System.Diagnostics.Process.Start("explorer.exe", argument);


                myData.Close();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cmd.Parameters.Clear();
        }


        private void textBoxFeltoltUrites()
        {
            textBoxDokumentumNeve.Clear();
            textBoxEleresi.Clear();
            textBoxTanuloNeveFeltolt.Clear();
            textBoxAnyjaNeveFeltolt.Clear();
        }

        private void hozzaad()
        {
            try
            {
                string szvt_eleresiUt = textBoxEleresi.Text;
                FileStream fs = new FileStream(szvt_eleresiUt, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                string fileName = textBoxDokumentumNeve.Text.ToString();

                fs.Close();

                string SQL = "INSERT INTO " +
                    "szakmaiVizsgaTorzslap " +
                    "VALUES" +
                    "(" +
                        "NULL, " +
                        "@szvt_tanuloNeve, " +
                        "@szvt_AnyjaNeve, " +
                        "@szvt_szerzo, " +
                        "@szvt_erettsegiEve, " +
                        "@szvt_DokumentumNev, " +
                        "@szvt_DokLegutobbModositva," +
                        "@szvt_FeltoltesIdopontja," +
                        "@szvt_formatum, " +
                        "@szvt_path " +
                    ")";

                cmd.Connection = conn;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@szvt_tanuloNeve", textBoxTanuloNeveFeltolt.Text);
                cmd.Parameters.AddWithValue("@szvt_AnyjaNeve", textBoxAnyjaNeveFeltolt.Text);
                cmd.Parameters.AddWithValue("@szvt_erettsegiEve", numericUpDownFeltolt.Value);
                cmd.Parameters.AddWithValue("@szvt_szerzo", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                cmd.Parameters.AddWithValue("@szvt_DokumentumNev", fileName);
                cmd.Parameters.AddWithValue("@szvt_DokLegutobbModositva", File.GetLastWriteTime(szvt_eleresiUt));
                cmd.Parameters.AddWithValue("@szvt_FeltoltesIdopontja", DateTime.Now);
                cmd.Parameters.AddWithValue("@szvt_formatum", kiterjesztes);
                cmd.Parameters.AddWithValue("@szvt_path", szvt_eleresiUt);

                cmd.ExecuteNonQuery();

                string destination = destPath + fileName + "." + kiterjesztes;
                string source = szvt_eleresiUt;

                //MessageBox.Show("Forrás: " + source + "\nCél: " + destination);
                // To move a file or folder to a new  location:

                System.IO.File.Copy(source, destination);

                MessageBox.Show("File Inserted into database successfully!",
                "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("Ez a File már létezik!");
            }
            cmd.Parameters.Clear();

        }

        private void tallozas()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.ShowDialog();
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

                MessageBox.Show(fileName);
                kiterjesztes = tombFile.Last();
                textBoxDokumentumNeve.Text = fileName;
                textBoxEleresi.Text = openFileDialog1.FileName;
            }
        }

        private void tanuloneveKeres()
        {
            var command = conn.CreateCommand();
            command.CommandText = "SELECT szvt_tanuloNeve,szvt_erettsegiEve,szvt_AnyjaNeve FROM szakmaiVizsgaTorzslap WHERE szvt_tanuloNeve like '%" + textBoxTanuloNeveKeres.Text + "%'";
            using (var reader = command.ExecuteReader())
            {
                var szvt_tanuloNeve = reader.GetOrdinal("szvt_tanuloNeve");
                var szvt_erettsegiEve = reader.GetOrdinal("szvt_erettsegiEve");
                var szvt_AnyjaNeve = reader.GetOrdinal("szvt_AnyjaNeve");

                while (reader.Read())
                {
                    var szvt_tanuloNeve2 = reader.GetValue(szvt_tanuloNeve).ToString();
                    var szvt_erettsegiEve2 = reader.GetValue(szvt_erettsegiEve).ToString();
                    var szvt_AnyjaNeve2 = reader.GetValue(szvt_AnyjaNeve).ToString();
                    listBoxKeresesEredmenye.Items.Add(szvt_tanuloNeve2 + "-" + szvt_erettsegiEve2 + "-" + szvt_AnyjaNeve2);
                }
            }

        }

        private void evszamKeres()
        {
            var command = conn.CreateCommand();
            command.CommandText = "SELECT szvt_tanuloNeve,szvt_erettsegiEve,szvt_AnyjaNeve FROM szakmaiVizsgaTorzslap WHERE szvt_erettsegiEve like '%" + numericUpDownKeres.Value + "%'";

            List<string> lista = new List<string>();

            using (var reader = command.ExecuteReader())
            {
                var szvt_tanuloNeve = reader.GetOrdinal("szvt_tanuloNeve");
                var szvt_erettsegiEve = reader.GetOrdinal("szvt_erettsegiEve");
                var szvt_AnyjaNeve = reader.GetOrdinal("szvt_AnyjaNeve");

                while (reader.Read())
                {
                    var szvt_tanuloNeve2 = reader.GetValue(szvt_tanuloNeve).ToString();
                    var szvt_erettsegiEve2 = reader.GetValue(szvt_erettsegiEve).ToString();
                    var szvt_AnyjaNeve2 = reader.GetValue(szvt_AnyjaNeve).ToString();
                    listBoxKeresesEredmenye.Items.Add(szvt_tanuloNeve2 + "-" + szvt_erettsegiEve2 + "-" + szvt_AnyjaNeve2);
                }
            }
        }

        private void anyjaKeres()
        {
            listBoxKeresesEredmenye.Items.Clear();

            var command = conn.CreateCommand();

            command.CommandText = "SELECT szvt_tanuloNeve,szvt_erettsegiEve,szvt_AnyjaNeve  FROM szakmaiVizsgaTorzslap WHERE szvt_AnyjaNeve like '%" + textBoxAnyjaNeveKeres.Text + "%'";

            using (var reader = command.ExecuteReader())
            {
                var szvt_tanuloNeve = reader.GetOrdinal("szvt_tanuloNeve");
                var szvt_erettsegiEve = reader.GetOrdinal("szvt_erettsegiEve");
                var szvt_AnyjaNeve = reader.GetOrdinal("szvt_AnyjaNeve");

                while (reader.Read())
                {
                    var szvt_tanuloNeve2 = reader.GetValue(szvt_tanuloNeve).ToString();
                    var szvt_erettsegiEve2 = reader.GetValue(szvt_erettsegiEve).ToString();
                    var szvt_AnyjaNeve2 = reader.GetValue(szvt_AnyjaNeve).ToString();
                    listBoxKeresesEredmenye.Items.Add(szvt_tanuloNeve2 + "-" + szvt_erettsegiEve2 + "-" + szvt_AnyjaNeve2);
                }
            }
        }

        private void buttonTallozas_Click(object sender, EventArgs e)
        {
            tallozas();
        }

        private void buttonFeltoltes_Click(object sender, EventArgs e)
        {
            hozzaad();
            textBoxFeltoltUrites();
        }

        private void buttonLetoltes_Click(object sender, EventArgs e)
        {
            betolt();
        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            (new FormMain()).Show(); this.Hide();

        }

        private void textBoxAnyjaNeveKeres_TextChanged_1(object sender, EventArgs e)
        {
            listBoxKeresesEredmenye.Items.Clear();
            anyjaKeres();
        }

        private void textBoxTanuloNeveKeres_TextChanged_1(object sender, EventArgs e)
        {
            listBoxKeresesEredmenye.Items.Clear();
            tanuloneveKeres();
        }

        private void numericUpDownKeres_ValueChanged_1(object sender, EventArgs e)
        {
            listBoxKeresesEredmenye.Items.Clear();
            evszamKeres();
        }

        private void FormKozepiskola_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonTallozas_Click_1(object sender, EventArgs e)
        {
            tallozas();
        }
    }
}
