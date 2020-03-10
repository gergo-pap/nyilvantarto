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
        private static readonly char[] SpecialChars = "!@#$%^&*()".ToCharArray();

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
                                    szakmaivizsgatorzslap 
                                    (
                                    id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                    szvt_tanuloNeve TEXT NOT NULL,
                                    szvt_AnyjaNeve TEXT NOT NULL,
                                    szvt_szerzo TEXT NOT NULL,
                                    szvt_erettsegiEvKezdet INT NOT NULL,
                                    szvt_erettsegiEvVeg INT NOT NULL,
                                    szvt_DokumentumNev TEXT NOT NULL,
                                    szvt_DokLegutobbModositva DATETIME NOT NULL,
                                    szvt_FeltoltesIdopontja DATETIME NOT NULL,
                                    szvt_formatum TEXT NOT NULL,
                                    szvt_path TEXT NULL
                                    );
                                    ";
            command.ExecuteNonQuery();
        }

        private void betolt()
        {
            try
            {
                //MySql.Data.MySqlClient.MySqlDataReader myData;
                string[] keresesKijelolt = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
                string nev = keresesKijelolt[0];
                string evKezdet = keresesKijelolt[1];
                string evVeg = keresesKijelolt[2];
                string anyja = keresesKijelolt[3];

                var command = conn.CreateCommand();

                command.CommandText = "SELECT szvt_formatum " +
                    "FROM " +
                    "szakmaivizsgatorzslap " +
                    "WHERE " +
                    "szvt_tanuloNeve = '" + nev +
                    "' AND " +
                    "szvt_AnyjaNeve = '" + anyja +
                    "' AND " +
                    "szvt_erettsegiEvKezdet = " + evKezdet +
                    " AND " +
                    "szvt_erettsegiEvVeg = " + evVeg
                    ;

                var result = command.ExecuteScalar();

                var formatum = result.ToString();

                string filePath = destPath + nev + '_' + evKezdet + '_' + evVeg + '_' + anyja + "." + formatum;


                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Nincs meg a File!");
                    return;
                }
                string argument = "/select, \"" + filePath + "\"";
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Hiba " + ex.Number + " lépett fel: " + ex.Message,
                    "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            listBoxKeresesEredmenye.Items.Clear();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT szvt_tanuloNeve,szvt_erettsegiEvKezdet,szvt_erettsegiEvVeg,szvt_AnyjaNeve  FROM szakmaivizsgatorzslap WHERE " + column + " like '%" + textboxText + "%'";
            using (var reader = command.ExecuteReader())
            {
                var szvt_tanuloNeve = reader.GetOrdinal("szvt_tanuloNeve");
                var szvt_erettsegiEvKezdet = reader.GetOrdinal("szvt_erettsegiEvKezdet");
                var szvt_erettsegiEvVeg = reader.GetOrdinal("szvt_erettsegiEvVeg");
                var szvt_AnyjaNeve = reader.GetOrdinal("szvt_AnyjaNeve");

                while (reader.Read())
                {
                    var szvt_tanuloNeve2 = reader.GetValue(szvt_tanuloNeve).ToString();
                    var szvt_erettsegiEvKezdet2 = reader.GetValue(szvt_erettsegiEvKezdet).ToString();
                    var szvt_erettsegiEvVeg2 = reader.GetValue(szvt_erettsegiEvVeg).ToString();
                    var szvt_AnyjaNeve2 = reader.GetValue(szvt_AnyjaNeve).ToString();
                    listBoxKeresesEredmenye.Items.Add(szvt_tanuloNeve2 + "-" + szvt_erettsegiEvKezdet2 + "-" + szvt_erettsegiEvVeg2 + "-" + szvt_AnyjaNeve2);
                }
            }
        }
        private void hozzaad()
        {
            try
            {
                int indexOf = textBoxAnyjaNeveFeltolt.Text.IndexOfAny(SpecialChars);
                int indexOf2 = textBoxTanuloNeveFeltolt.Text.IndexOfAny(SpecialChars);
                if (indexOf == -1 && indexOf2 == -1)
                {
                    string szvt_eleresiUt = textBoxEleresi.Text;
                    FileStream fs = new FileStream(szvt_eleresiUt, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    string fileName = textBoxTanuloNeveFeltolt.Text + "_" + numericUpDownEvFeltoltKezdet.Value.ToString() + "_" + numericUpDownEvFeltoltVeg.Value.ToString() + "_" + textBoxAnyjaNeveFeltolt.Text;
                    fs.Close();

                    string SQL = "INSERT INTO " +
                        "szakmaivizsgatorzslap " +
                        "VALUES" +
                        "(" +
                            "NULL, " +
                            "@szvt_tanuloNeve, " +
                            "@szvt_AnyjaNeve, " +
                            "@szvt_szerzo, " +
                            "@szvt_erettsegiEvKezdet, " +
                            "@szvt_erettsegiEvVeg, " +
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
                    cmd.Parameters.AddWithValue("@szvt_szerzo", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    cmd.Parameters.AddWithValue("@szvt_erettsegiEvKezdet", numericUpDownEvFeltoltKezdet.Value);
                    cmd.Parameters.AddWithValue("@szvt_erettsegiEvVeg", numericUpDownEvFeltoltVeg.Value);
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
                string[] s = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
                string fileName = s[0] + "_" + s[1] + "_" + s[2] + "_" + s[3];
                string[] keresesKijelolt = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
                string nev = keresesKijelolt[0];
                string evKezdet = keresesKijelolt[1];
                string evVeg = keresesKijelolt[2];
                string anyja = keresesKijelolt[3];

                var command = conn.CreateCommand();

                command.CommandText = "SELECT szvt_formatum " +
                    "FROM " +
                    "szakmaivizsgatorzslap " +
                    "WHERE " +
                    "szvt_tanuloNeve = '" + nev +
                    "' AND " +
                    "szvt_AnyjaNeve = '" + anyja +
                    "' AND " +
                    "szvt_erettsegiEvKezdet = " + evKezdet +
                    " AND " +
                    "szvt_erettsegiEvVeg = " + evVeg
                    ;

                var result = command.ExecuteScalar();

                var szvt_formatum2 = result.ToString();

                string SQL = "DELETE FROM " +
                        "szakmaivizsgatorzslap " +
                        "WHERE " +
                        "szvt_tanuloNeve = '" + nev + "' AND " +
                        "szvt_AnyjaNeve =  '" + anyja + "' AND " +
                        "szvt_erettsegiEvKezdet = " + evKezdet + " AND " +
                        "szvt_erettsegiEvVeg = " + evVeg +
                        ";"
                        ;

                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();



                string destination = destPath + fileName + "." + szvt_formatum2;

                System.IO.File.Delete(destination);

                MessageBox.Show("Sikeres törlés");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nincs kijelölve semmi!");
            }
        }

        private void modositas()
        {
            int indexOf = textBoxAnyjaneveModositas.Text.IndexOfAny(SpecialChars);
            int indexOf2 = textBoxNevModositas.Text.IndexOfAny(SpecialChars);
            if (indexOf == -1 && indexOf2 == -1)
            {
                try
                {
                    string[] keresesKijelolt = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
                    string nev = keresesKijelolt[0];
                    string evKezdet = keresesKijelolt[1];
                    string evVeg = keresesKijelolt[2];
                    string anyja = keresesKijelolt[3];

                    string SQL =
                                "UPDATE " +
                                "szakmaivizsgatorzslap " +
                                "SET " +
                                "szvt_tanuloNeve = '" + textBoxNevModositas.Text + "', " +
                                "szvt_AnyjaNeve = '" + textBoxAnyjaneveModositas.Text + "', " +
                                "szvt_erettsegiEvKezdet = " + numericUpDownEvKezdetModositas.Value + ", " +
                                "szvt_erettsegiEvVeg = " + numericUpDownEvVegModositas.Value + " " +
                                "WHERE " +
                                "szvt_tanuloNeve = '" + nev + "' AND " +
                                "szvt_AnyjaNeve =  '" + anyja + "' AND " +
                                "szvt_erettsegiEvKezdet = " + evKezdet + " AND " +
                                "szvt_erettsegiEvVeg = " + evVeg + ";"
                                ;

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;

                    cmd.ExecuteNonQuery();

                    var command = conn.CreateCommand();

                    command.CommandText = "SELECT szvt_formatum " +
                        "FROM " +
                        "szakmaivizsgatorzslap " +
                        "WHERE " +
                        "szvt_tanuloNeve = '" + textBoxNevModositas.Text +
                        "' AND " +
                        "szvt_AnyjaNeve = '" + textBoxAnyjaneveModositas.Text +
                        "' AND " +
                        "szvt_erettsegiEvKezdet = " + numericUpDownEvKezdetModositas.Value +
                        " AND " +
                        "szvt_erettsegiEvVeg = " + numericUpDownEvVegModositas.Value
                        ;

                    var result = command.ExecuteScalar();

                    var szvt_formatum2 = result.ToString();

                    string destFileName = textBoxNevModositas.Text + '_' + numericUpDownEvKezdetModositas.Value + '_' + numericUpDownEvVegModositas.Value + '_' + textBoxAnyjaneveModositas.Text;
                    string[] s = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
                    System.IO.File.Move(destPath + nev + '_' + evKezdet + '_' + evVeg + '_' + anyja + '.' + szvt_formatum2, destPath + destFileName + '.' + szvt_formatum2);

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

        private void numericUpDownEvFeltoltKezdet_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownEvFeltoltVeg.Value = numericUpDownEvFeltoltKezdet.Value + 4;
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
            keres("szvt_erettsegiEvKezdet", numericUpDownVizsgaKezdetKeres.Value.ToString());
        }


        private void numericUpDownVizsgaVegKeres_ValueChanged(object sender, EventArgs e)
        {
            keres("szvt_erettsegiEvVeg", numericUpDownVizsgaVegKeres.Value.ToString());
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
                listBoxKeresesEredmenye.Items.Clear();
            }
        }

        private void buttonModositas_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                modositas();
                groupBox1.Visible = false;
                listBoxKeresesEredmenye.Items.Clear();
            }
            else
            {
                groupBox1.Visible = true;
            }
        }
    }
}
