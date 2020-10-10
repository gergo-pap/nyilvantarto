using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Nyilvantarto_v2
{
	public partial class FormSzakmaiVizsgaTörzslap : Form
	{
		public FormSzakmaiVizsgaTörzslap()
		{
			InitializeComponent();
		}

		private void FormSzakmaiVizsgaTörzslap_Load(object sender, EventArgs e)
		{
			FormProgressbar f = new FormProgressbar();
			f.setProgressbarMax(100);
			f.Show();
			f.incrementProgress(10, 5);
			Global.getDestPathFromDatabase(@"\Adatok\Szakmai Vizsga\Törzslap\");
			f.incrementProgress(10, 5);
			Global.gruopBoxSetDefaultVisibility(groupBoxOsszetettKereses, groupBoxRandom, groupBoxFileokSzama);
			f.incrementProgress(20, 5);
			Global.getCount(labelFileokSzama, labelDBCount, @"\Adatok\Szakmai vizsga\Törzslap\", "szakmaivizsgaTorzslap");
			f.incrementProgress(50, 5);
			Global.checkMissingFiles(@"\Adatok\Szakmai vizsga\Törzslap\", "szvt_tanuloNeve", "szvt_viszgaEvKezdet",
									"szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve", "szakmaivizsgaTorzslap", labelFileokSzama, labelDBCount);
			f.incrementProgress(10, 5);
			f.Close();
		}

		private void FormSzakmaiVizsgaTörzslap_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		//gombok
		private void buttonKereses_Click(object sender, EventArgs e)
		{
			Global.fileKereseseFajlkezeloben();
		}

		private void buttonTallozas_Click(object sender, EventArgs e)
		{
			Global.tallozas(textBoxDokumentumNeveFeltolt, textBoxEleresiFeltolt);
		}

		private void buttonFeltoltes_Click(object sender, EventArgs e)
		{
			if (Global.checkIfEmptyInput(textBoxAnyjaNeveFeltolt, textBoxDokumentumNeveFeltolt, textBoxTanuloNeveFeltolt, textBoxEleresiFeltolt))
			{
				Global.fileFeltolteseBDreESFileMozgatasa(textBoxAnyjaNeveFeltolt, textBoxTanuloNeveFeltolt, textBoxEleresiFeltolt, radioButtonOszFeltolt, numericUpDownEvFeltolt,
														"szakmaivizsgaTorzslap", "szvt_tanuloNeve", "szvt_AnyjaNeve", "szvt_szerzo", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", 
														"szvt_dokumentumNev", "szvt_dokLegutobbModositva", "szvt_feltoltesIdopontja", "szvt_formatum", "szvt_path");
				Global.textBoxFeltoltUrites(textBoxDokumentumNeveFeltolt, textBoxEleresiFeltolt, textBoxTanuloNeveFeltolt, textBoxAnyjaNeveFeltolt);
				Global.bordercolorReset(textBoxAnyjaNeveFeltolt, textBoxDokumentumNeveFeltolt, textBoxTanuloNeveFeltolt, textBoxEleresiFeltolt);
			}
			Global.getCount(labelFileokSzama, labelDBCount, @"\Adatok\Szakmai vizsga\Törzslap\", "szakmaivizsgaTorzslap");
		}

		private void buttonModositas_Click(object sender, EventArgs e)
		{
			if (groupBoxModositas.Visible && Global.checkNumericUpDownValue(numericUpDownEvModosit))
			{
				Global.modositas(textBoxAnyjanevModosit, textBoxTanuloNevModosit, radioButtonTavaszModosit, numericUpDownEvModosit,
								 "szakmaivizsgaTorzslap",
								 "szvt_tanuloNeve", "szvt_AnyjaNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0");
				groupBoxModositas.Visible = false;
				panelKeresTorlesModGombok.Visible = false;
				Global.listboxKeresesEredmenyeiClear(listBoxKeresesTanuloNeve, listBoxKeresesAnyjaNeve, listboxKeresesIdoszak, listBoxKeresesVKezdete, listBoxKeresesId);
			}
			else
			{
				panelKeresTorlesGombok.Visible = false;
				groupBoxModositas.Visible = true;
				textBoxTanuloNevModosit.Text = Global.globNev;
				textBoxAnyjanevModosit.Text = Global.globAnyja;
				numericUpDownEvModosit.Value = int.Parse(Global.globEvKezdet);
				if (Global.globTavaszVOszString == "Tavasz")
				{
					radioButtonTavaszModosit.Checked = true;
				}
				else
				{
					radioButtonOszModosit.Checked = true;
				}
			}
		}

		private void buttonVissza_Click(object sender, EventArgs e)
		{
			(new FormMain()).Show(); this.Hide();
		}

		private void buttonTorles_Click(object sender, EventArgs e)
		{
			groupBoxJelszo.Visible = true;
			if (textBoxJelszo.Text == "NemMondomMeg")
			{
				Global.torles(listBoxKeresesId, "szakmaivizsgaTorzslap");
				Global.getCount(labelFileokSzama, labelDBCount, @"\Adatok\Szakmai vizsga\Törzslap\", "szakmaivizsgaTorzslap");

				groupBoxJelszo.Visible = false;
				textBoxJelszo.Clear();
				Global.listboxKeresesEredmenyeiClear(listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);
				panelKeresTorlesModGombok.Visible = false;
			}
		}

		private void textBoxJelszo_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonTorles_Click(sender, e);
			}
		}

		private void buttonRandomGeneralas_Click(object sender, EventArgs e)
		{
			MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nyilvantartas;Uid=root;Pwd=;CharSet=utf8;");
			MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();

			string[] vezetekNevek = { "Pap", "Timár", "Gulyás", "Szabó", "Horváth", "Kis", "Kiss", "Nagy"
								,"Tóth","Kristóf","Tim","Krajcsi","Beke","Gachovetz","Móricz","Dantesz","Bánki ","Baracsiné"
								,"Darányi","Hatvani","Földes","Sebők","Skordai","Szűcs","Zalai","Zsuppán","Borzok","Bráda"
								,"Almássy","Tim-Bartha","Hazenfratz","Langenbacher"};

			string[] keresztNevek = { "Gyula", "Julianna", "Márta", "Ilona", "Mária", "Csaba", "Attila", "Tamás"
								,"Gabriella","Katalin","Károly","Vanda","István","Ernő","Norbert","Krisztina","Kriszta","Mariann"
								,"Tibor","Kitti","Lajos","Nóra","Ágnes","Gergő","Klára","Zoltán","Sándor", "Elek"
								,"Alexandra","Anasztázia","Boldizsár","Liliána","Benedek","Adrienne"};

			new Thread(() =>
			{
				Thread.CurrentThread.IsBackground = true;
				Random r = new Random();
				string tanuloNeve;
				string anyjaNeve;
				int ev;
				byte tavaszOsz;
				string tavaszVOsz;
				string szvt_eleresiUt;
				string fileName;
				string destination;
				string SQL;
				string source;
				conn.Open();
				for (int j = 0; j < 1000; j++)
				{
					for (int i = 0; i < int.Parse(textBoxRandom.Text); i++)
					{
						cmd.Parameters.Clear();

						tanuloNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
						anyjaNeve = vezetekNevek[r.Next(0, vezetekNevek.Length)] + " " + keresztNevek[r.Next(0, keresztNevek.Length)];
						ev = r.Next(1900, 2100);
						try
						{

							if (r.Next(0, 2) == 0)
							{
								tavaszOsz = 0;
								tavaszVOsz = "Ősz";
							}
							else
							{
								tavaszOsz = 1;
								tavaszVOsz = "Tavasz";
							}
							szvt_eleresiUt = @"C:\Users\Pap Gergő\Documents\Database1.accdb";
							fileName = tanuloNeve + "_" + ev + "_" + tavaszVOsz + "_" + anyjaNeve;
							destination = Global.fullPath + fileName + ".txt";

							SQL = "INSERT INTO " +
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
							cmd.Parameters.AddWithValue("@szvt_tanuloNeve", tanuloNeve);
							cmd.Parameters.AddWithValue("@szvt_AnyjaNeve", anyjaNeve);
							cmd.Parameters.AddWithValue("@szvt_szerzo", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
							cmd.Parameters.AddWithValue("@szvt_viszgaEvKezdet", ev);
							cmd.Parameters.AddWithValue("@szvt_viszgaTavasz1Osz0", tavaszOsz);
							cmd.Parameters.AddWithValue("@szvt_dokumentumNev", fileName);
							cmd.Parameters.AddWithValue("@szvt_dokLegutobbModositva", File.GetLastWriteTime(szvt_eleresiUt));
							cmd.Parameters.AddWithValue("@szvt_feltoltesIdopontja", DateTime.Now);
							cmd.Parameters.AddWithValue("@szvt_formatum", "txt");
							cmd.Parameters.AddWithValue("@szvt_path", destination);

							cmd.ExecuteNonQuery();

							source = szvt_eleresiUt;
							System.IO.File.WriteAllBytes(destination, new byte[1]);
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
						Thread.Sleep(25);
					}
				}
			}).Start();
			conn.Close();
		}


		//keresés függvények

		private void textBoxTanuloNeveKeres_TextChanged(object sender, EventArgs e)
		{
			//MessageBox.Show($"sender: {sender}\neventargs: {e}");
			Global.listboxKeresesEredmenyeiClear(listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);
			if (checkBoxOsszetettKeres.Checked)
			{
				Global.osszetettKeresv2("szvt_tanuloNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve",
										"szakmaivizsgaTorzslap",
										textBoxTanuloNeveKeres.Text,
										textBoxAnyjaNeveKeres.Text,
										numericUpDownEvKeres.Value.ToString(),
										(radioButtonTavaszKeres.Checked ? 1 : 0).ToString(),
										listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);
			}
			else
			{
				if (textBoxAnyjaNeveKeres.Text.Length > 0)
				{
					Global.osszetettKeres("szvt_tanuloNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve",
										  "szakmaivizsgaTorzslap",
										  textBoxTanuloNeveKeres.Text, textBoxAnyjaNeveKeres.Text, listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);

				}
				else if (textBoxTanuloNeveKeres.Text.Length > 3)
				{
					Global.keres("szvt_tanuloNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve",
								 "szakmaivizsgaTorzslap",
								 textBoxTanuloNeveKeres.Text, listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);
				}
			}

		}

		private void textBoxTanuloNeveKeres_KeyUp(object sender, KeyEventArgs e)
		{
			//MessageBox.Show($"sender: {sender}\neventargs: {e}");
			if (e.KeyCode == Keys.Enter)
			{
				textBoxTanuloNeveKeres_TextChanged(sender, e);
			}
		}

		private void textBoxAnyjaNeveKeres_TextChanged(object sender, EventArgs e)
		{
			Global.listboxKeresesEredmenyeiClear(listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);
			if (checkBoxOsszetettKeres.Checked)
			{
				Global.osszetettKeresv2("szvt_tanuloNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve", 
										"szakmaivizsgaTorzslap",
										textBoxTanuloNeveKeres.Text, 
										textBoxAnyjaNeveKeres.Text,
										numericUpDownEvKeres.Value.ToString(),
										(radioButtonTavaszKeres.Checked ? 1 : 0).ToString(),
										listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);
			}
			else
			{
				if (textBoxTanuloNeveKeres.Text.Length != 0)
				{
					Global.osszetettKeres("szvt_tanuloNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve", 
										  "szakmaivizsgaTorzslap", 
										  textBoxTanuloNeveKeres.Text, textBoxAnyjaNeveKeres.Text, listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);


				}
				else if (textBoxAnyjaNeveKeres.Text.Length > 3)
				{
					Global.keres("szvt_AnyjaNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve", 
								 "szakmaivizsgaTorzslap", 
								 textBoxAnyjaNeveKeres.Text, listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);

				}
			}

		}

		private void textBoxAnyjaNeveKeres_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				textBoxAnyjaNeveKeres_TextChanged(sender, e);
			}
		}

		private void numericUpDownEvKeres_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				radioButtonTavaszKeres_CheckedChanged(sender, e);
			}
		}

		private void numericUpDownEvKeres_ValueChanged(object sender, EventArgs e)
		{
			radioButtonTavaszKeres_CheckedChanged(sender, e);
		}

		private void radioButtonTavaszKeres_CheckedChanged(object sender, EventArgs e)
		{
			Global.listboxKeresesEredmenyeiClear(listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);

			Global.osszetettKeresv2("szvt_tanuloNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0", "szvt_AnyjaNeve",
										"szakmaivizsgaTorzslap",
										textBoxTanuloNeveKeres.Text,
										textBoxAnyjaNeveKeres.Text,
										numericUpDownEvKeres.Value.ToString(),
										(radioButtonTavaszKeres.Checked ? 1 : 0).ToString(),
										listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesAnyjaNeve);
		}

		private void radioButtonOszKeres_CheckedChanged(object sender, EventArgs e)
		{
			radioButtonTavaszKeres_CheckedChanged(sender, e);
		}

		//--------

		private void checkBoxOsszetett_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxOsszetettKeres.Checked)
			{
				groupBoxOsszetettKereses.Visible = true;
			}
			else
			{
				groupBoxOsszetettKereses.Visible = false;
			}

		}

		private void textBoxTanuloNeveFeltolt_TextChanged(object sender, EventArgs e)
		{
			Global.textBoxTextChanged(textBoxTanuloNeveFeltolt);
		}

		private void textBoxDokumentumNeveFeltolt_TextChanged(object sender, EventArgs e)
		{
			Global.textBoxTextChanged(textBoxDokumentumNeveFeltolt);
		}

		private void textBoxAnyjaNeveFeltolt_TextChanged(object sender, EventArgs e)
		{
			Global.textBoxTextChanged(textBoxAnyjaNeveFeltolt);
		}

		private void textBoxEleresiFeltolt_TextChanged(object sender, EventArgs e)
		{
			Global.textBoxTextChanged(textBoxEleresiFeltolt);

		}

		private void listBoxKeresesId_SelectedIndexChanged(object sender, EventArgs e)
		{
			Global.setVariablesFromSelecteditem(listBoxKeresesId, "szvt_tanuloNeve", "szvt_viszgaEvKezdet", "szvt_viszgaTavasz1Osz0",
												"szvt_AnyjaNeve", "szvt_formatum", "szakmaivizsgaTorzslap");
			panelKeresTorlesModGombok.Visible = true;
			panelKeresTorlesGombok.Visible = true;
		}

		private void numericUpDownEvModosit_ValueChanged(object sender, EventArgs e)
		{
			Global.checkNumericUpDownValue(numericUpDownEvModosit);
		}
	}
}
