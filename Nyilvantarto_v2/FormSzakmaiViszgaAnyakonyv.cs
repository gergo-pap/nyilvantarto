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
    public partial class FormSzakmaiViszgaAnyakonyv : Form
    {
        string kiterjesztes;
        MySqlConnection conn;
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        private static readonly char[] SpecialChars = "!@#$%^&*()".ToCharArray();
        string destPath = Global.fixPath;

        public FormSzakmaiViszgaAnyakonyv()
        {
            conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");
            try
            {
                InitializeComponent();
                conn.Open();
                CreateTableDokumentumok();
                getDestPathFromDatabase("eleresiUt");
            }
            catch (MySqlException)
            {
                MessageBox.Show("Sikertelen csatlakozás az adatbázishoz");
            }
        }

        private void getDestPathFromDatabase(string var)
        {
            var command = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"Select value From settings Where var = {var}";
            var result = cmd.ExecuteScalar();
            Global.fixPath = result.ToString();
        }
        private void CreateTableDokumentumok()
        {
            var command = conn.CreateCommand();
            command.CommandText = @"
                                CREATE TABLE IF NOT EXISTS 
                                    szakmaiviszgaanyakonyv 
                                    (
                                    id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                    szva_tanuloNeve TEXT NOT NULL,
                                    szva_anyjaNeve TEXT NOT NULL,
                                    szva_szerzo TEXT NOT NULL,
                                    szva_erettsegiEvKezdet INT NOT NULL,
                                    szva_erettsegiEvVeg INT NOT NULL,
                                    szva_DokumentumNev TEXT NOT NULL,
                                    szva_DokLegutobbModositva DATETIME NOT NULL,
                                    szva_FeltoltesIdopontja DATETIME NOT NULL,
                                    szva_formatum TEXT NOT NULL,
                                    szva_path TEXT NULL
                                    );
                                    ";
            command.ExecuteNonQuery();
        }

        private void fileBetolteseFajlkezeloben()
        {
            try
            {
                string[] keresesKijelolt = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
                string nev = keresesKijelolt[0];
                string evKezdet = keresesKijelolt[1];
                string evVeg = keresesKijelolt[2];
                string anyja = keresesKijelolt[3];

                var command = conn.CreateCommand();

                command.CommandText = "SELECT szva_formatum " +
                    "FROM " +
                    "szakmaiviszgaanyakonyv " +
                    "WHERE " +
                    "szva_tanuloNeve = '" + nev +
                    "' AND " +
                    "szva_anyjaNeve = '" + anyja +
                    "' AND " +
                    "szva_erettsegiEvKezdet = " + evKezdet +
                    " AND " +
                    "szva_erettsegiEvVeg = " + evVeg
                    ;

                var result = command.ExecuteScalar();

                var formatum = result.ToString();

                string filePath = Global.fixPath + nev + '_' + evKezdet + '_' + evVeg + '_' + anyja + "." + formatum; // ?


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
            command.CommandText = "SELECT szva_tanuloNeve,szva_erettsegiEvKezdet,szva_erettsegiEvVeg,szva_anyjaNeve  FROM szakmaiviszgaanyakonyv WHERE " + column + " like '%" + textboxText + "%'";
            using (var reader = command.ExecuteReader())
            {
                var szva_tanuloNeve = reader.GetOrdinal("szva_tanuloNeve");
                var szva_erettsegiEvKezdet = reader.GetOrdinal("szva_erettsegiEvKezdet");
                var szva_erettsegiEvVeg = reader.GetOrdinal("szva_erettsegiEvVeg");
                var szva_anyjaNeve = reader.GetOrdinal("szva_anyjaNeve");

                while (reader.Read())
                {
                    var szva_tanuloNeve2 = reader.GetValue(szva_tanuloNeve).ToString();
                    var szva_erettsegiEvKezdet2 = reader.GetValue(szva_erettsegiEvKezdet).ToString();
                    var szva_erettsegiEvVeg2 = reader.GetValue(szva_erettsegiEvVeg).ToString();
                    var szva_anyjaNeve2 = reader.GetValue(szva_anyjaNeve).ToString();
                    listBoxKeresesEredmenye.Items.Add(szva_tanuloNeve2 + "-" + szva_erettsegiEvKezdet2 + "-" + szva_erettsegiEvVeg2 + "-" + szva_anyjaNeve2);
                }
            }
        }
        private void hozzaad()
        {
            try
            {
                int indexOf = textBoxanyjaNeveFeltolt.Text.IndexOfAny(SpecialChars);
                int indexOf2 = textBoxTanuloNeveFeltolt.Text.IndexOfAny(SpecialChars);
                if (indexOf == -1 && indexOf2 == -1)
                {
                    string szva_eleresiUt = textBoxEleresi.Text;
                    FileStream fs = new FileStream(szva_eleresiUt, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    string fileName = textBoxTanuloNeveFeltolt.Text + "_" + numericUpDownEvFeltoltKezdet.Value.ToString() + "_" + numericUpDownEvFeltoltVeg.Value.ToString() + "_" + textBoxanyjaNeveFeltolt.Text;
                    fs.Close();

                    string SQL = "INSERT INTO " +
                        "szakmaiviszgaanyakonyv " +
                        "VALUES" +
                        "(" +
                            "NULL, " +
                            "@szva_tanuloNeve, " +
                            "@szva_anyjaNeve, " +
                            "@szva_szerzo, " +
                            "@szva_erettsegiEvKezdet, " +
                            "@szva_erettsegiEvVeg, " +
                            "@szva_DokumentumNev, " +
                            "@szva_DokLegutobbModositva," +
                            "@szva_FeltoltesIdopontja," +
                            "@szva_formatum, " +
                            "@szva_path " +
                        ")";

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@szva_tanuloNeve", textBoxTanuloNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue("@szva_anyjaNeve", textBoxanyjaNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue("@szva_szerzo", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    cmd.Parameters.AddWithValue("@szva_erettsegiEvKezdet", numericUpDownEvFeltoltKezdet.Value);
                    cmd.Parameters.AddWithValue("@szva_erettsegiEvVeg", numericUpDownEvFeltoltVeg.Value);
                    cmd.Parameters.AddWithValue("@szva_DokumentumNev", fileName);
                    cmd.Parameters.AddWithValue("@szva_DokLegutobbModositva", File.GetLastWriteTime(szva_eleresiUt));
                    cmd.Parameters.AddWithValue("@szva_FeltoltesIdopontja", DateTime.Now);
                    cmd.Parameters.AddWithValue("@szva_formatum", kiterjesztes);
                    cmd.Parameters.AddWithValue("@szva_path", szva_eleresiUt);

                    cmd.ExecuteNonQuery();

                    string destination = destPath + fileName + "." + kiterjesztes;
                    string source = szva_eleresiUt;
                    //MessageBox.Show("Forrás: " + source + "\nCél: " + destination);
                    // To move a file or folder to a new  location:

                    System.IO.File.Copy(source, destination);

                    //MessageBox.Show("File Inserted into database successfully!",
                    //"Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            catch (IOException e)
            {
                MessageBox.Show("Fájl hiba!\n A fájl már létezik vagy nem található!" + e);
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

                command.CommandText = "SELECT szva_formatum " +
                    "FROM " +
                    "szakmaiviszgaanyakonyv " +
                    "WHERE " +
                    "szva_tanuloNeve = '" + nev +
                    "' AND " +
                    "szva_anyjaNeve = '" + anyja +
                    "' AND " +
                    "szva_erettsegiEvKezdet = " + evKezdet +
                    " AND " +
                    "szva_erettsegiEvVeg = " + evVeg
                    ;

                var result = command.ExecuteScalar();

                var szva_formatum2 = result.ToString();

                string SQL = "DELETE FROM " +
                        "szakmaiviszgaanyakonyv " +
                        "WHERE " +
                        "szva_tanuloNeve = '" + nev + "' AND " +
                        "szva_anyjaNeve =  '" + anyja + "' AND " +
                        "szva_erettsegiEvKezdet = " + evKezdet + " AND " +
                        "szva_erettsegiEvVeg = " + evVeg +
                        ";"
                        ;

                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();

                

                string destination = Global.fixPath + fileName + "." + szva_formatum2; //?

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
            int indexOf = textBoxanyjaNeveModositas.Text.IndexOfAny(SpecialChars);
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
                                "szakmaiviszgaanyakonyv " +
                                "SET " +
                                "szva_tanuloNeve = '" + textBoxNevModositas.Text + "', " +
                                "szva_anyjaNeve = '" + textBoxanyjaNeveModositas.Text + "', " +
                                "szva_erettsegiEvKezdet = " + numericUpDownEvKezdetModositas.Value + ", " +
                                "szva_erettsegiEvVeg = " + numericUpDownEvVegModositas.Value + " " +
                                "WHERE " +
                                "szva_tanuloNeve = '" + nev + "' AND " +
                                "szva_anyjaNeve =  '" + anyja + "' AND " +
                                "szva_erettsegiEvKezdet = " + evKezdet + " AND " +
                                "szva_erettsegiEvVeg = " + evVeg + ";"
                                ;

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;

                    cmd.ExecuteNonQuery();

                    var command = conn.CreateCommand();

                    command.CommandText = "SELECT szva_formatum " +
                        "FROM " +
                        "szakmaiviszgaanyakonyv " +
                        "WHERE " +
                        "szva_tanuloNeve = '" + textBoxNevModositas.Text +
                        "' AND " +
                        "szva_anyjaNeve = '" + textBoxanyjaNeveModositas.Text +
                        "' AND " +
                        "szva_erettsegiEvKezdet = " + numericUpDownEvKezdetModositas.Value +
                        " AND " +
                        "szva_erettsegiEvVeg = " + numericUpDownEvVegModositas.Value
                        ;

                    var result = command.ExecuteScalar();

                    var szva_formatum2 = result.ToString();

                    string destFileName = textBoxNevModositas.Text + '_' + numericUpDownEvKezdetModositas.Value + '_' + numericUpDownEvVegModositas.Value + '_' + textBoxanyjaNeveModositas.Text;
                    string[] s = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
                    System.IO.File.Move(Global.fixPath + nev + '_' + evKezdet + '_' + evVeg + '_' + anyja + '.' + szva_formatum2, Global.fixPath + destFileName + '.' + szva_formatum2);
                    //?
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
            textBoxanyjaNeveFeltolt.Clear();
        }

        private bool checkIfEmptyInput()
        {
            bool joE = true;
            if (textBoxanyjaNeveFeltolt.Text.Length == 0)
            {
                textBoxanyjaNeveFeltolt.BackColor = Color.Red;
                joE =  false;
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
            textBoxanyjaNeveFeltolt.BackColor = Color.White;
            textBoxDokumentumNeve.BackColor = Color.White;
            textBoxTanuloNeveFeltolt.BackColor = Color.White;
            textBoxEleresi.BackColor = Color.White;
        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            (new FormMain()).Show(); this.Hide();
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

        private void buttonTallozas_Click(object sender, EventArgs e)
        {
            tallozas();
        }

        private void buttonLetoltes_Click(object sender, EventArgs e)
        {
            fileBetolteseFajlkezeloben();
        }

        private void textBoxTanuloNeveKeres_TextChanged(object sender, EventArgs e)
        {
            keres("szva_tanuloNeve", textBoxTanuloNeveKeres.Text);
        }

        private void textBoxanyjaNeveKeres_TextChanged(object sender, EventArgs e)
        {
            keres("szva_anyjaNeve", textBoxanyjaNeveKeres.Text);

        }

        private void numericUpDownKeresVizsgaVegKerevalueChanged(object sender, EventArgs e)
        {
            keres("szva_erettsegiEvVeg", numericUpDownVizsgaVegKeres.Value.ToString());
        }

        private void numericUpDownVizsgaKezdetKerevalueChanged(object sender, EventArgs e)
        {
            keres("szva_erettsegiEvKezdet", numericUpDownVizsgaKezdetKeres.Value.ToString());
        }

        private void FormSzakmaiViszgaAnyakonyv_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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

        private void textBoxanyjaNeveFeltolt_TextChanged(object sender, EventArgs e)
        {
            if (textBoxanyjaNeveFeltolt.Text.Length == 0)
            {
                textBoxanyjaNeveFeltolt.BackColor = Color.Red;
            }
            else
            {
                textBoxanyjaNeveFeltolt.BackColor = Color.White;
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

        private void textBoxDokumentumNeve_TextChanged_1(object sender, EventArgs e)
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
            try
            {
                numericUpDownEvFeltoltVeg.Value = numericUpDownEvFeltoltKezdet.Value + 4;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Az érték nem 1900 és 2096 között van");
                numericUpDownEvFeltoltKezdet.Value = 2000;
            }
        }

        private void numericUpDownEvKezdetModositavalueChanged(object sender, EventArgs e)
        {
            try
            {
                numericUpDownEvVegModositas.Value = numericUpDownEvKezdetModositas.Value + 4;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Az érték nem 1900 és 2096 között van");
                numericUpDownEvKezdetModositas.Value = 2000;
            }
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
                    listBoxKeresesEredmenye.Items.Clear();
                }
                e.Handled = true;
            }
        }
    }
}
