using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Nyilvantarto_v2
{
	class Database
	{
		private static MySqlConnection _conn =
			new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");

		public static bool GlobIsaMessageBoxOpen;

		public static MySqlCommand CreateCommand(string sqlQuery)
		{
			return new MySqlCommand(sqlQuery, __getConnection());
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

		private static bool IsConnectionOpen()
		{
			return _conn.State == ConnectionState.Open;
		}

		public static void SetPathInDb(string labelMentesiHely, GroupBox groupBoxEleresi, string eleresiUt)
		{
			try
			{
				if (!Controll.PathsContainSpecChars(labelMentesiHely, labelMentesiHely))
				{
					InsertMentesihelyToDb(labelMentesiHely, eleresiUt);
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

		public static object GetFileStoragePathFromDb(string varString)
		{
			string query = $"SELECT value FROM settings WHERE var = @var; ";
			MySqlCommand cmd = CreateCommand(query);
			cmd.Parameters.AddWithValue("@var", varString);
			return cmd.ExecuteScalar();
		}


		public static MySqlCommand GetSearchResultFromDb(
                                                        string rowVkezdet,
                                                        string rowExtra,
			                                            string @from,
                                                        string likeTanuloNeve,
                                                        string likeAnyjaNeve,
                                                        string likeVkezdet
            )

		{
			var cmd = CreateCommand(
				                        $"SELECT id, tanuloNeve, {rowVkezdet}, {rowExtra}, anyjaNeve " +
				                                    $"FROM {@from} " +
				                                    $"WHERE tanuloNeve like '%{likeTanuloNeve}%' " +
				                                    $"AND anyjaNeve like '%{likeAnyjaNeve}%' " +
				                                    $"AND {rowVkezdet} like '%{likeVkezdet}%'"
			);
			return cmd;
		}

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

		private static void ThrowArgumentException(bool messageBox, ArgumentException aEx)
		{
			GlobIsaMessageBoxOpen = true;
			if (messageBox)
			{
				MessageBox.Show($"Hibás connection string!\n{aEx.Message}\n{aEx}");
			}

			GlobIsaMessageBoxOpen = false;
		}


		public static void InsertMentesihelyToDb(string labelMentesiHely, string eleresiUt)
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
				}
			}
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

		public static void CreateTableSettings()
		{
			var command = CreateCommand(
                                @"CREATE TABLE IF NOT EXISTS 
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
			                    @"CREATE TABLE IF NOT EXISTS 
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
			var command = CreateCommand(
                                @"CREATE TABLE IF NOT EXISTS 
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
				                @"CREATE TABLE IF NOT EXISTS 
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
			var command = CreateCommand(
                                @"CREATE TABLE IF NOT EXISTS 
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
			var command = CreateCommand(
                                @"CREATE TABLE IF NOT EXISTS 
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

		public static string SetSqlCommandUpdate(
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

		public static string SetSqlCommandInsertInto(
			                                        string into,
			                                        string rowVevKezdet,
			                                        string rowTavaszVosz,
			                                        string rowDokLegutobbModositva
		)
		{
			return "INSERT INTO " +
						 $"{@into} " +
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

		public static int GetLastIdFromDb(string tableName)
		{
			string sql = $"SELECT MAX(id) FROM {tableName};";
			var cmd = CreateCommand(sql);
			return int.Parse(cmd.ExecuteScalar().ToString());
		}

		public static void FileFeltoltese(
			                            string textBoxAnyjaNeveFeltolt,
			                            string textBoxTanuloNeveFeltolt,
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


				var sql = SetSqlCommandInsertInto(@into, rowVevKezdet, rowTavaszVosz, rowDokLegutobbModositva);

				var cmd = CreateCommand(sql);

				AddParametersToCmdVizsgaEvKezdet(numericUpDownEvFeltoltKezdet, rowVevKezdet, cmd);

				AddParametersToCmdTavaszOrOsz(radioButtonOszFeltolt, rowTavaszVosz, cmd, tavaszOsz);

				AddParametersToCmdRemaining(textBoxAnyjaNeveFeltolt, textBoxTanuloNeveFeltolt, rowDokLegutobbModositva, cmd);

				object modified = cmd.ExecuteScalar();
				returnValue = TryGetLastId(modified, returnValue);
				//MessageBox.Show("Feltöltés köv id: " + returnValue.ToString());


				MessageBox.Show("Sikeres feltöltés!",
					"Siker!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				cmd.Parameters.Clear();
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
			cmd.Parameters.AddWithValue($"@rowSzerzo", WindowsIdentity.GetCurrent().Name);
			cmd.Parameters.AddWithValue($"@{rowDokLegutobbModositva}", File.GetLastWriteTime(Controll.GlobFeltoltendoFileEleresiUt));
			cmd.Parameters.AddWithValue($"@rowFeltoltesIdopontja", DateTime.Now);
			cmd.Parameters.AddWithValue($"@filename", Path.GetFileName(Controll.GlobFeltoltendoFileEleresiUt));
		}

		private static int TryGetLastId(object modified, int returnValue)
		{
			if (modified != null)
			{
				int.TryParse(modified.ToString(), out returnValue);
			}
			return returnValue;
		}
	}
}