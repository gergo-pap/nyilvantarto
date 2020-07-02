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

namespace Nyilvantarto_v2
{
    public partial class FormMain : Form
    {

        MySqlConnection conn;
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        private static readonly char[] SpecialChars = "!@#$%^&*()".ToCharArray();

        public FormMain()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;charset = utf8");
            conn.Open();
            createTables();
            checkDirs();
        }

        public void checkDirs()
        {
            string query = "SELECT s_value FROM settings WHERE s_var = 'eleresiUt'; ";
            var command = conn.CreateCommand();
            command.CommandText = query;
            var result = command.ExecuteScalar();
            if (result == null)
            {
                groupBoxEleresi.Visible = true;
            }
            else
            {
                var path = result.ToString();
                labelMentesiHely.Text = path;
                groupBoxEleresi.Visible = false;
                groupBoxButtons.Visible = true;
            }
        }

        private void createDirectiories()
        {
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Középiskola\Anyakönyv\");
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Szakmai Vizsga\Anyakönyv\");
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Szakmai Vizsga\Törzslap\");
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Érettségi\Törzslap\");
            Directory.CreateDirectory(labelMentesiHely.Text + @"\Adatok\Érettségi\Tanusítvány\");
        }

        private void setPath()
        {
            try
            {
                int indexOf = labelMentesiHely.Text.IndexOfAny(SpecialChars);
                if (indexOf == -1)
                {
                    string SQL = "INSERT INTO " +
                        "settings " +
                        "VALUES" +
                        "(" +
                            "NULL, " +
                            "@s_var, " +
                            "@s_value " +
                        ")";
                    MessageBox.Show(labelMentesiHely.Text);
                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@s_var", "eleresiUt");
                    cmd.Parameters.AddWithValue("@s_value", labelMentesiHely.Text.ToString());

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("A fájl neve érvénytelen karaktereket tartalmaz! !@#$%^&*");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("SQL hiba " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            groupBoxEleresi.Visible = true;
            groupBoxButtons.Visible = true;
        }

        private void createTables()
        {
            var command = conn.CreateCommand();
            command.CommandText = @"
                                CREATE TABLE IF NOT EXISTS 
                                    settings 
                                    (
                                    id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                    s_var TEXT NOT NULL,
                                    s_value TEXT NOT NULL
                                    );
                                    ";
            command.ExecuteNonQuery();

            command = conn.CreateCommand();
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

            command = conn.CreateCommand();
            command.CommandText = @"
                                CREATE TABLE IF NOT EXISTS 
                                    szakmaiviszgaanyakonyv 
                                    (
                                    id INTEGER PRIMARY KEY AUTO_INCREMENT,
                                    szva_tanuloNeve TEXT NOT NULL,
                                    szva_AnyjaNeve TEXT NOT NULL,
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

            command = conn.CreateCommand();
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
        private void buttonErettsegi_Click(object sender, EventArgs e)
        {
            (new FormErettsegi()).Show(); this.Hide();
        }

        private void buttonSzkmaiVizsga_Click(object sender, EventArgs e)
        {
            (new FormSzakmaiVizsga()).Show(); this.Hide();

        }

        private void buttonKozepiskola_Click(object sender, EventArgs e)
        {
            (new FormKozepiskolaAnyakonyv()).Show(); this.Hide();
        }

        private void buttonTallozas_Click(object sender, EventArgs e)
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
            setPath();
            createDirectiories();
            MessageBox.Show(labelMentesiHely.Text);
        }
    }
}
