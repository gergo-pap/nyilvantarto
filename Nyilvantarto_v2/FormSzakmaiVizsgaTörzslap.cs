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
			Global.getDestPathFromDatabase(@"\Adatok\Szakmai Vizsga\Törzslap\", "eleresiUt");
			f.incrementProgress(10, 5);
			Global.gruopBoxSetDefaultVisibility(groupBoxOsszetettKereses, groupBoxRandom, groupBoxFileokSzama);
			f.incrementProgress(20, 5);
			Global.getCount(labelFileokSzama, labelDBCount, @"\Adatok\Szakmai vizsga\Törzslap\", "szakmaivizsgaTorzslap");
			f.incrementProgress(50, 5);
			//Global.checkMissingFiles(@"\Adatok\Szakmai vizsga\Törzslap\", "tanuloNeve", "vizsgaEvVeg",
			//						"vizsgaTavasz1Osz0", "anyjaNeve", "szakmaivizsgaTorzslap", labelFileokSzama, labelDBCount);
			timer1.Enabled = true;
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
			Global.fileKereseseFajlkezeloben("fos", "fos");
		}

		private void buttonTallozas_Click(object sender, EventArgs e)
		{
			Global.tallozas(textBoxDokumentumNeveFeltolt/*, textBoxEleresiFeltolt*/);
		}

		private void buttonFeltoltes_Click(object sender, EventArgs e)
		{
			if (Global.checkIfEmptyInput(textBoxanyjaNeveFeltolt, textBoxDokumentumNeveFeltolt, textBoxTanuloNeveFeltolt, textBoxEleresiFeltolt))
			{
				Global.fileFeltolteseBDreESFileMozgatasa(textBoxanyjaNeveFeltolt, textBoxTanuloNeveFeltolt, Global.globFeltoltendoFileEleresiUt, Global.globFeltoltendoFileEleresiUt.ToString(), radioButtonOszFeltolt, numericUpDownEvFeltolt,
														"szakmaivizsgaTorzslap", "tanuloNeve", "anyjaNeve", "szerzo", "viszgaEvKezdet", "viszgaTavasz1Osz0", 
														 "dokLegutobbModositva", "feltoltesIdopontja", "path");
				Global.textBoxFeltoltUrites(textBoxDokumentumNeveFeltolt, textBoxEleresiFeltolt, textBoxTanuloNeveFeltolt, textBoxanyjaNeveFeltolt);
				Global.bordercolorReset(textBoxanyjaNeveFeltolt, textBoxDokumentumNeveFeltolt, textBoxTanuloNeveFeltolt, textBoxEleresiFeltolt);
			}
			Global.getCount(labelFileokSzama, labelDBCount, @"\Adatok\Szakmai vizsga\Törzslap\", "szakmaivizsgaTorzslap");
		}

		private void buttonModositas_Click(object sender, EventArgs e)
		{
			if (groupBoxModositas.Visible && Global.checkNumericUpDownValue(numericUpDownEvModosit))
			{
				//Global.modositas(textBoxAnyjanevModosit, textBoxTanuloNevModosit, radioButtonTavaszModosit, numericUpDownEvModosit,
				//				 "szakmaivizsgaTorzslap",
				//				 "tanuloNeve", "anyjaNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0");
				groupBoxModositas.Visible = false;
				panelKeresTorlesModGombok.Visible = false;
				Global.listboxKeresesEredmenyeiClear(listBoxKeresesTanuloNeve, listBoxKeresesanyjaNeve, listboxKeresesIdoszak, listBoxKeresesVKezdete, listBoxKeresesId);
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
				Global.listboxKeresesEredmenyeiClear(listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);
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

		}


		//keresés függvények

		private void textBoxTanuloNeveKeres_TextChanged(object sender, EventArgs e)
		{
			//MessageBox.Show($"sender: {sender}\neventargs: {e}");
			Global.listboxKeresesEredmenyeiClear(listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);
			if (checkBoxOsszetettKeres.Checked)
			{
				Global.osszetettKeresv2("tanuloNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0", "anyjaNeve",
										"szakmaivizsgaTorzslap",
										textBoxTanuloNeveKeres.Text,
										textBoxanyjaNeveKeres.Text,
										numericUpDownEvKeres.Value.ToString(),
										(radioButtonTavaszKeres.Checked ? 1 : 0).ToString(),
										listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);
			}
			else
			{
				if (textBoxanyjaNeveKeres.Text.Length > 0)
				{
					Global.osszetettKeres("tanuloNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0", "anyjaNeve",
										  "szakmaivizsgaTorzslap",
										  textBoxTanuloNeveKeres.Text, textBoxanyjaNeveKeres.Text, listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);

				}
				else if (textBoxTanuloNeveKeres.Text.Length > 3)
				{
					Global.keres("tanuloNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0", "anyjaNeve",
								 "szakmaivizsgaTorzslap",
								 textBoxTanuloNeveKeres.Text, listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);
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

		private void textBoxanyjaNeveKeres_TextChanged(object sender, EventArgs e)
		{
			Global.listboxKeresesEredmenyeiClear(listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);
			if (checkBoxOsszetettKeres.Checked)
			{
				Global.osszetettKeresv2("tanuloNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0", "anyjaNeve", 
										"szakmaivizsgaTorzslap",
										textBoxTanuloNeveKeres.Text, 
										textBoxanyjaNeveKeres.Text,
										numericUpDownEvKeres.Value.ToString(),
										(radioButtonTavaszKeres.Checked ? 1 : 0).ToString(),
										listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);
			}
			else
			{
				if (textBoxTanuloNeveKeres.Text.Length != 0)
				{
					Global.osszetettKeres("tanuloNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0", "anyjaNeve", 
										  "szakmaivizsgaTorzslap", 
										  textBoxTanuloNeveKeres.Text, textBoxanyjaNeveKeres.Text, listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);


				}
				else if (textBoxanyjaNeveKeres.Text.Length > 3)
				{
					Global.keres("anyjaNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0", "anyjaNeve", 
								 "szakmaivizsgaTorzslap", 
								 textBoxanyjaNeveKeres.Text, listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);

				}
			}

		}

		private void textBoxanyjaNeveKeres_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				textBoxanyjaNeveKeres_TextChanged(sender, e);
			}
		}

		private void numericUpDownEvKeres_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				radioButtonTavaszKeres_CheckedChanged(sender, e);
			}
		}

		private void numericUpDownEvKerevalueChanged(object sender, EventArgs e)
		{
			radioButtonTavaszKeres_CheckedChanged(sender, e);
		}

		private void radioButtonTavaszKeres_CheckedChanged(object sender, EventArgs e)
		{
			Global.listboxKeresesEredmenyeiClear(listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);

			Global.osszetettKeresv2("tanuloNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0", "anyjaNeve",
										"szakmaivizsgaTorzslap",
										textBoxTanuloNeveKeres.Text,
										textBoxanyjaNeveKeres.Text,
										numericUpDownEvKeres.Value.ToString(),
										(radioButtonTavaszKeres.Checked ? 1 : 0).ToString(),
										listBoxKeresesId, listBoxKeresesTanuloNeve, listBoxKeresesVKezdete, listboxKeresesIdoszak, listBoxKeresesanyjaNeve);
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

		private void textBoxanyjaNeveFeltolt_TextChanged(object sender, EventArgs e)
		{
			Global.textBoxTextChanged(textBoxanyjaNeveFeltolt);
		}

		private void textBoxEleresiFeltolt_TextChanged(object sender, EventArgs e)
		{
			Global.textBoxTextChanged(textBoxEleresiFeltolt);

		}

		private void listBoxKeresesId_SelectedIndexChanged(object sender, EventArgs e)
		{
			Global.setVariablesFromSelecteditem(listBoxKeresesId, "tanuloNeve", "viszgaEvKezdet", "viszgaTavasz1Osz0",
												"anyjaNeve", "formatum", "szakmaivizsgaTorzslap");
			panelKeresTorlesModGombok.Visible = true;
			panelKeresTorlesGombok.Visible = true;
		}

		private void numericUpDownEvModosit_ValueChanged(object sender, EventArgs e)
		{
			Global.checkNumericUpDownValue(numericUpDownEvModosit);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Interval = 5000;
			if (Global.checkDB_Conn(false))
			{
				labelAdatbazisKapcsolat.Text = "aktív";
				labelAdatbazisKapcsolat.BackColor = Color.LightGreen;
			}
			else
			{
				labelAdatbazisKapcsolat.Text = "offline";
				labelAdatbazisKapcsolat.BackColor = Color.Red;
				timer1.Enabled = false;
				//System.Threading.Thread.Sleep(2000);
				(new FormMain()).Show(); this.Hide();
			}
		}
	}
}
