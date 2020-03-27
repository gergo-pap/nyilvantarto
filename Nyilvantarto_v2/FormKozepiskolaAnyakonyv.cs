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
    public partial class FormKozepiskolaAnyakonyv : Form
    {
        string kiterjesztes;
        MySqlConnection conn;
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        string destPath = @"c:\Users\Pap Gergő\Desktop\Teszt\Középiskola\Anyakönyv\";
        private static readonly char[] SpecialChars = "!@#$%^&*()".ToCharArray();


        public FormKozepiskolaAnyakonyv()
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
                                    kozepiskolaanyakonyv 
                                    (
                                    id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                    ka_tanuloNeve TEXT NOT NULL,
                                    ka_AnyjaNeve TEXT NOT NULL,
                                    ka_szerzo TEXT NOT NULL,
                                    ka_erettsegiEvKezdet INT NOT NULL,
                                    ka_erettsegiEvVeg INT NOT NULL,
                                    ka_DokumentumNev TEXT NOT NULL,
                                    ka_DokLegutobbModositva DATETIME NOT NULL,
                                    ka_FeltoltesIdopontja DATETIME NOT NULL,
                                    ka_formatum TEXT NOT NULL,
                                    ka_path TEXT NULL
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

                command.CommandText = "SELECT ka_formatum " +
                    "FROM " +
                    "kozepiskolaanyakonyv " +
                    "WHERE " +
                    "ka_tanuloNeve = '" + nev +
                    "' AND " +
                    "ka_AnyjaNeve = '" + anyja +
                    "' AND " +
                    "ka_erettsegiEvKezdet = " + evKezdet +
                    " AND " +
                    "ka_erettsegiEvVeg = " + evVeg
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
            command.CommandText = "SELECT ka_tanuloNeve,ka_erettsegiEvKezdet,ka_erettsegiEvVeg,ka_AnyjaNeve  FROM kozepiskolaanyakonyv WHERE " + column + " like '%" + textboxText + "%'";
            using (var reader = command.ExecuteReader())
            {
                var ka_tanuloNeve = reader.GetOrdinal("ka_tanuloNeve");
                var ka_erettsegiEvKezdet = reader.GetOrdinal("ka_erettsegiEvKezdet");
                var ka_erettsegiEvVeg = reader.GetOrdinal("ka_erettsegiEvVeg");
                var ka_AnyjaNeve = reader.GetOrdinal("ka_AnyjaNeve");

                while (reader.Read())
                {
                    var ka_tanuloNeve2 = reader.GetValue(ka_tanuloNeve).ToString();
                    var ka_erettsegiEvKezdet2 = reader.GetValue(ka_erettsegiEvKezdet).ToString();
                    var ka_erettsegiEvVeg2 = reader.GetValue(ka_erettsegiEvVeg).ToString();
                    var ka_AnyjaNeve2 = reader.GetValue(ka_AnyjaNeve).ToString();
                    listBoxKeresesEredmenye.Items.Add(ka_tanuloNeve2 + "-" + ka_erettsegiEvKezdet2 + "-" + ka_erettsegiEvVeg2 + "-" + ka_AnyjaNeve2);
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
                    string ka_eleresiUt = textBoxEleresi.Text;
                    FileStream fs = new FileStream(ka_eleresiUt, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    string fileName = textBoxTanuloNeveFeltolt.Text + "_" + numericUpDownEvFeltoltKezdet.Value.ToString() + "_" + numericUpDownEvFeltoltVeg.Value.ToString() + "_" + textBoxAnyjaNeveFeltolt.Text;
                    fs.Close();

                    string SQL = "INSERT INTO " +
                        "kozepiskolaanyakonyv " +
                        "VALUES" +
                        "(" +
                            "NULL, " +
                            "@ka_tanuloNeve, " +
                            "@ka_AnyjaNeve, " +
                            "@ka_szerzo, " +
                            "@ka_erettsegiEvKezdet, " +
                            "@ka_erettsegiEvVeg, " +
                            "@ka_DokumentumNev, " +
                            "@ka_DokLegutobbModositva," +
                            "@ka_FeltoltesIdopontja," +
                            "@ka_formatum, " +
                            "@ka_path " +
                        ")";

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@ka_tanuloNeve", textBoxTanuloNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue("@ka_AnyjaNeve", textBoxAnyjaNeveFeltolt.Text);
                    cmd.Parameters.AddWithValue("@ka_szerzo", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    cmd.Parameters.AddWithValue("@ka_erettsegiEvKezdet", numericUpDownEvFeltoltKezdet.Value);
                    cmd.Parameters.AddWithValue("@ka_erettsegiEvVeg", numericUpDownEvFeltoltVeg.Value);
                    cmd.Parameters.AddWithValue("@ka_DokumentumNev", fileName);
                    cmd.Parameters.AddWithValue("@ka_DokLegutobbModositva", File.GetLastWriteTime(ka_eleresiUt));
                    cmd.Parameters.AddWithValue("@ka_FeltoltesIdopontja", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ka_formatum", kiterjesztes);
                    cmd.Parameters.AddWithValue("@ka_path", ka_eleresiUt);

                    cmd.ExecuteNonQuery();

                    string destination = destPath + fileName + "." + kiterjesztes;
                    string source = ka_eleresiUt;

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

                command.CommandText = "SELECT ka_formatum " +
                    "FROM " +
                    "kozepiskolaanyakonyv " +
                    "WHERE " +
                    "ka_tanuloNeve = '" + nev +
                    "' AND " +
                    "ka_AnyjaNeve = '" + anyja +
                    "' AND " +
                    "ka_erettsegiEvKezdet = " + evKezdet +
                    " AND " +
                    "ka_erettsegiEvVeg = " + evVeg
                    ;

                var result = command.ExecuteScalar();

                var ka_formatum2 = result.ToString();

                string SQL = "DELETE FROM " +
                        "kozepiskolaanyakonyv " +
                        "WHERE " +
                        "ka_tanuloNeve = '" + nev + "' AND " +
                        "ka_AnyjaNeve =  '" + anyja + "' AND " +
                        "ka_erettsegiEvKezdet = " + evKezdet + " AND " +
                        "ka_erettsegiEvVeg = " + evVeg +
                        ";"
                        ;

                cmd.Connection = conn;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();



                string destination = destPath + fileName + "." + ka_formatum2;

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
                                "kozepiskolaanyakonyv " +
                                "SET " +
                                "ka_tanuloNeve = '" + textBoxNevModositas.Text + "', " +
                                "ka_AnyjaNeve = '" + textBoxAnyjaneveModositas.Text + "', " +
                                "ka_erettsegiEvKezdet = " + numericUpDownEvKezdetModositas.Value + ", " +
                                "ka_erettsegiEvVeg = " + numericUpDownEvVegModositas.Value + " " +
                                "WHERE " +
                                "ka_tanuloNeve = '" + nev + "' AND " +
                                "ka_AnyjaNeve =  '" + anyja + "' AND " +
                                "ka_erettsegiEvKezdet = " + evKezdet + " AND " +
                                "ka_erettsegiEvVeg = " + evVeg + ";"
                                ;

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;

                    cmd.ExecuteNonQuery();

                    var command = conn.CreateCommand();

                    command.CommandText = "SELECT ka_formatum " +
                        "FROM " +
                        "kozepiskolaanyakonyv " +
                        "WHERE " +
                        "ka_tanuloNeve = '" + textBoxNevModositas.Text +
                        "' AND " +
                        "ka_AnyjaNeve = '" + textBoxAnyjaneveModositas.Text +
                        "' AND " +
                        "ka_erettsegiEvKezdet = " + numericUpDownEvKezdetModositas.Value +
                        " AND " +
                        "ka_erettsegiEvVeg = " + numericUpDownEvVegModositas.Value
                        ;

                    var result = command.ExecuteScalar();

                    var ka_formatum2 = result.ToString();

                    string destFileName = textBoxNevModositas.Text + '_' + numericUpDownEvKezdetModositas.Value + '_' + numericUpDownEvVegModositas.Value + '_' + textBoxAnyjaneveModositas.Text;
                    string[] s = listBoxKeresesEredmenye.SelectedItem.ToString().Split('-');
                    System.IO.File.Move(destPath + nev + '_' + evKezdet + '_' + evVeg + '_' + anyja + '.' + ka_formatum2, destPath + destFileName + '.' + ka_formatum2);

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

        private void textBoxAnyjaNeveKeres_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTanuloNeveKeres_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonTallozas_Click(object sender, EventArgs e)
        {

        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {

        }

        private void buttonTallozas_Click_1(object sender, EventArgs e)
        {
            tallozas();
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

        private void numericUpDownEvFeltoltKezdet_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownEvFeltoltVeg.Value = numericUpDownEvFeltoltKezdet.Value + 4;
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

        private void buttonFeltoltes_Click(object sender, EventArgs e)
        {
            if (checkIfEmptyInput())
            {
                hozzaad();
                textBoxFeltoltUrites();
                borderColorReset();
            }
        }

        private void textBoxTanuloNeveKeres_TextChanged_1(object sender, EventArgs e)
        {
            keres("ka_tanuloNeve", textBoxTanuloNeveKeres.Text);
        }

        private void textBoxAnyjaNeveKeres_TextChanged_1(object sender, EventArgs e)
        {
            keres("ka_AnyjaNeve", textBoxAnyjaNeveKeres.Text);
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

        private void buttonVissza_Click_1(object sender, EventArgs e)
        {
            (new FormMain()).Show(); this.Hide();
        }

        private void numericUpDownEvKezdetModositas_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownEvVegModositas.Value = numericUpDownEvKezdetModositas.Value + 4;
        }

        private void numericUpDownVizsgaKezdetKeres_ValueChanged(object sender, EventArgs e)
        {
            keres("szva_erettsegiEvKezdet", numericUpDownVizsgaKezdetKeres.Value.ToString());
        }

        private void numericUpDownVizsgaVegKeres_ValueChanged(object sender, EventArgs e)
        {
            keres("szva_erettsegiEvVeg", numericUpDownVizsgaVegKeres.Value.ToString());
        }
    }
}
