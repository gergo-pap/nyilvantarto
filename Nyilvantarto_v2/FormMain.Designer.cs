namespace Nyilvantarto_v2
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonErettsegi = new System.Windows.Forms.Button();
            this.buttonSzakmaiVizsga = new System.Windows.Forms.Button();
            this.buttonKozepiskola = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelMentesiHely = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonTallozas = new System.Windows.Forms.Button();
            this.groupBoxEleresi = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.groupBoxButtons = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.labelKapcsolatAdatbazissal = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonErettsegiTorzslap = new System.Windows.Forms.Button();
            this.buttonErettsegiTanusitvany = new System.Windows.Forms.Button();
            this.buttonSzakmaiVizsgaTorzslap = new System.Windows.Forms.Button();
            this.buttonSzakmaiViszgaAnyakonyv = new System.Windows.Forms.Button();
            this.buttonKozepiskolaAnyakonyv = new System.Windows.Forms.Button();
            this.textBoxTanuloNeve = new System.Windows.Forms.TextBox();
            this.textBoxAnyjaNeve = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownViszgaÉve = new System.Windows.Forms.NumericUpDown();
            this.checkBoxViszgaEve = new System.Windows.Forms.CheckBox();
            this.panelKeres = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownTalalatokSzama = new System.Windows.Forms.NumericUpDown();
            this.groupBoxEleresi.SuspendLayout();
            this.groupBoxButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownViszgaÉve)).BeginInit();
            this.panelKeres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTalalatokSzama)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonErettsegi
            // 
            this.buttonErettsegi.Location = new System.Drawing.Point(33, 44);
            this.buttonErettsegi.Name = "buttonErettsegi";
            this.buttonErettsegi.Size = new System.Drawing.Size(75, 23);
            this.buttonErettsegi.TabIndex = 0;
            this.buttonErettsegi.Text = "Érettségi";
            this.buttonErettsegi.UseVisualStyleBackColor = true;
            this.buttonErettsegi.Click += new System.EventHandler(this.buttonErettsegi_Click);
            // 
            // buttonSzakmaiVizsga
            // 
            this.buttonSzakmaiVizsga.Location = new System.Drawing.Point(168, 43);
            this.buttonSzakmaiVizsga.Name = "buttonSzakmaiVizsga";
            this.buttonSzakmaiVizsga.Size = new System.Drawing.Size(112, 23);
            this.buttonSzakmaiVizsga.TabIndex = 1;
            this.buttonSzakmaiVizsga.Text = "Szakmai vizsga";
            this.buttonSzakmaiVizsga.UseVisualStyleBackColor = true;
            this.buttonSzakmaiVizsga.Click += new System.EventHandler(this.buttonSzkmaiVizsga_Click);
            // 
            // buttonKozepiskola
            // 
            this.buttonKozepiskola.Location = new System.Drawing.Point(337, 43);
            this.buttonKozepiskola.Name = "buttonKozepiskola";
            this.buttonKozepiskola.Size = new System.Drawing.Size(75, 23);
            this.buttonKozepiskola.TabIndex = 2;
            this.buttonKozepiskola.Text = "Középiskola";
            this.buttonKozepiskola.UseVisualStyleBackColor = true;
            this.buttonKozepiskola.Click += new System.EventHandler(this.buttonKozepiskola_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mentési hely:";
            // 
            // labelMentesiHely
            // 
            this.labelMentesiHely.AutoSize = true;
            this.labelMentesiHely.Location = new System.Drawing.Point(81, 46);
            this.labelMentesiHely.Name = "labelMentesiHely";
            this.labelMentesiHely.Size = new System.Drawing.Size(0, 13);
            this.labelMentesiHely.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Módosítás:";
            // 
            // buttonTallozas
            // 
            this.buttonTallozas.Location = new System.Drawing.Point(84, 69);
            this.buttonTallozas.Name = "buttonTallozas";
            this.buttonTallozas.Size = new System.Drawing.Size(75, 23);
            this.buttonTallozas.TabIndex = 6;
            this.buttonTallozas.Text = "Tallózás";
            this.buttonTallozas.UseVisualStyleBackColor = true;
            this.buttonTallozas.Click += new System.EventHandler(this.buttonTallozas_Click);
            // 
            // groupBoxEleresi
            // 
            this.groupBoxEleresi.Controls.Add(this.label3);
            this.groupBoxEleresi.Controls.Add(this.label1);
            this.groupBoxEleresi.Controls.Add(this.labelPath);
            this.groupBoxEleresi.Controls.Add(this.buttonTallozas);
            this.groupBoxEleresi.Controls.Add(this.labelMentesiHely);
            this.groupBoxEleresi.Controls.Add(this.label2);
            this.groupBoxEleresi.Location = new System.Drawing.Point(12, 12);
            this.groupBoxEleresi.Name = "groupBoxEleresi";
            this.groupBoxEleresi.Size = new System.Drawing.Size(297, 147);
            this.groupBoxEleresi.TabIndex = 7;
            this.groupBoxEleresi.TabStop = false;
            this.groupBoxEleresi.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Első indítás, válaszd ki hova mentsem a program";
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(9, 99);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(34, 13);
            this.labelPath.TabIndex = 9;
            this.labelPath.Text = "path?";
            // 
            // groupBoxButtons
            // 
            this.groupBoxButtons.Controls.Add(this.buttonSzakmaiVizsga);
            this.groupBoxButtons.Controls.Add(this.buttonErettsegi);
            this.groupBoxButtons.Controls.Add(this.buttonKozepiskola);
            this.groupBoxButtons.Location = new System.Drawing.Point(826, 12);
            this.groupBoxButtons.Name = "groupBoxButtons";
            this.groupBoxButtons.Size = new System.Drawing.Size(430, 100);
            this.groupBoxButtons.TabIndex = 8;
            this.groupBoxButtons.TabStop = false;
            this.groupBoxButtons.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(615, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Kapcsolat az adatbázissal:";
            // 
            // labelKapcsolatAdatbazissal
            // 
            this.labelKapcsolatAdatbazissal.AutoSize = true;
            this.labelKapcsolatAdatbazissal.Location = new System.Drawing.Point(754, 28);
            this.labelKapcsolatAdatbazissal.Name = "labelKapcsolatAdatbazissal";
            this.labelKapcsolatAdatbazissal.Size = new System.Drawing.Size(32, 13);
            this.labelKapcsolatAdatbazissal.TabIndex = 11;
            this.labelKapcsolatAdatbazissal.Text = "aktív";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(96, 276);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(945, 470);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.Visible = false;
            // 
            // buttonErettsegiTorzslap
            // 
            this.buttonErettsegiTorzslap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonErettsegiTorzslap.Location = new System.Drawing.Point(347, 118);
            this.buttonErettsegiTorzslap.Name = "buttonErettsegiTorzslap";
            this.buttonErettsegiTorzslap.Size = new System.Drawing.Size(160, 23);
            this.buttonErettsegiTorzslap.TabIndex = 13;
            this.buttonErettsegiTorzslap.Text = "Érettségi - törzslap";
            this.buttonErettsegiTorzslap.UseVisualStyleBackColor = true;
            this.buttonErettsegiTorzslap.Click += new System.EventHandler(this.buttonErettsegiTorzslap_Click);
            // 
            // buttonErettsegiTanusitvany
            // 
            this.buttonErettsegiTanusitvany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonErettsegiTanusitvany.Location = new System.Drawing.Point(513, 118);
            this.buttonErettsegiTanusitvany.Name = "buttonErettsegiTanusitvany";
            this.buttonErettsegiTanusitvany.Size = new System.Drawing.Size(160, 23);
            this.buttonErettsegiTanusitvany.TabIndex = 14;
            this.buttonErettsegiTanusitvany.Text = "Érettségi - tanusítvány";
            this.buttonErettsegiTanusitvany.UseVisualStyleBackColor = true;
            this.buttonErettsegiTanusitvany.Click += new System.EventHandler(this.buttonErettsegiTanusitvany_Click);
            // 
            // buttonSzakmaiVizsgaTorzslap
            // 
            this.buttonSzakmaiVizsgaTorzslap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSzakmaiVizsgaTorzslap.Location = new System.Drawing.Point(679, 118);
            this.buttonSzakmaiVizsgaTorzslap.Name = "buttonSzakmaiVizsgaTorzslap";
            this.buttonSzakmaiVizsgaTorzslap.Size = new System.Drawing.Size(160, 23);
            this.buttonSzakmaiVizsgaTorzslap.TabIndex = 15;
            this.buttonSzakmaiVizsgaTorzslap.Text = "Szakmai vizsga - törzslap";
            this.buttonSzakmaiVizsgaTorzslap.UseVisualStyleBackColor = true;
            this.buttonSzakmaiVizsgaTorzslap.Click += new System.EventHandler(this.buttonSzakmaiVizsgaTorzslap_Click);
            // 
            // buttonSzakmaiViszgaAnyakonyv
            // 
            this.buttonSzakmaiViszgaAnyakonyv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSzakmaiViszgaAnyakonyv.Location = new System.Drawing.Point(845, 118);
            this.buttonSzakmaiViszgaAnyakonyv.Name = "buttonSzakmaiViszgaAnyakonyv";
            this.buttonSzakmaiViszgaAnyakonyv.Size = new System.Drawing.Size(160, 23);
            this.buttonSzakmaiViszgaAnyakonyv.TabIndex = 16;
            this.buttonSzakmaiViszgaAnyakonyv.Text = "Szakmai vizsga - anyakönyv";
            this.buttonSzakmaiViszgaAnyakonyv.UseVisualStyleBackColor = true;
            this.buttonSzakmaiViszgaAnyakonyv.Click += new System.EventHandler(this.buttonSzakmaiViszgaAnyakonyv_Click);
            // 
            // buttonKozepiskolaAnyakonyv
            // 
            this.buttonKozepiskolaAnyakonyv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKozepiskolaAnyakonyv.Location = new System.Drawing.Point(1011, 118);
            this.buttonKozepiskolaAnyakonyv.Name = "buttonKozepiskolaAnyakonyv";
            this.buttonKozepiskolaAnyakonyv.Size = new System.Drawing.Size(160, 23);
            this.buttonKozepiskolaAnyakonyv.TabIndex = 17;
            this.buttonKozepiskolaAnyakonyv.Text = "Középiskola - anyakönyv";
            this.buttonKozepiskolaAnyakonyv.UseVisualStyleBackColor = true;
            this.buttonKozepiskolaAnyakonyv.Click += new System.EventHandler(this.buttonKozepiskolaAnyakonyv_Click);
            // 
            // textBoxTanuloNeve
            // 
            this.textBoxTanuloNeve.Location = new System.Drawing.Point(12, 33);
            this.textBoxTanuloNeve.Name = "textBoxTanuloNeve";
            this.textBoxTanuloNeve.Size = new System.Drawing.Size(100, 20);
            this.textBoxTanuloNeve.TabIndex = 18;
            this.textBoxTanuloNeve.TextChanged += new System.EventHandler(this.textBoxTanuloNeve_TextChanged);
            // 
            // textBoxAnyjaNeve
            // 
            this.textBoxAnyjaNeve.Location = new System.Drawing.Point(178, 33);
            this.textBoxAnyjaNeve.Name = "textBoxAnyjaNeve";
            this.textBoxAnyjaNeve.Size = new System.Drawing.Size(100, 20);
            this.textBoxAnyjaNeve.TabIndex = 19;
            this.textBoxAnyjaNeve.TextChanged += new System.EventHandler(this.textBoxAnyjaNeve_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tanuló neve";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(175, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Anyja neve";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(392, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Vizsga éve";
            // 
            // numericUpDownViszgaÉve
            // 
            this.numericUpDownViszgaÉve.Location = new System.Drawing.Point(368, 32);
            this.numericUpDownViszgaÉve.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownViszgaÉve.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownViszgaÉve.Name = "numericUpDownViszgaÉve";
            this.numericUpDownViszgaÉve.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownViszgaÉve.TabIndex = 25;
            this.numericUpDownViszgaÉve.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownViszgaÉve.ValueChanged += new System.EventHandler(this.numericUpDownViszgaÉve_ValueChanged);
            // 
            // checkBoxViszgaEve
            // 
            this.checkBoxViszgaEve.AutoSize = true;
            this.checkBoxViszgaEve.Location = new System.Drawing.Point(368, 12);
            this.checkBoxViszgaEve.Name = "checkBoxViszgaEve";
            this.checkBoxViszgaEve.Size = new System.Drawing.Size(15, 14);
            this.checkBoxViszgaEve.TabIndex = 26;
            this.checkBoxViszgaEve.UseVisualStyleBackColor = true;
            this.checkBoxViszgaEve.CheckedChanged += new System.EventHandler(this.checkBoxViszgaEve_CheckedChanged);
            // 
            // panelKeres
            // 
            this.panelKeres.Controls.Add(this.label5);
            this.panelKeres.Controls.Add(this.checkBoxViszgaEve);
            this.panelKeres.Controls.Add(this.textBoxTanuloNeve);
            this.panelKeres.Controls.Add(this.numericUpDownViszgaÉve);
            this.panelKeres.Controls.Add(this.textBoxAnyjaNeve);
            this.panelKeres.Controls.Add(this.label7);
            this.panelKeres.Controls.Add(this.label6);
            this.panelKeres.Location = new System.Drawing.Point(391, 190);
            this.panelKeres.Name = "panelKeres";
            this.panelKeres.Size = new System.Drawing.Size(502, 69);
            this.panelKeres.TabIndex = 27;
            this.panelKeres.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Találatom száma:";
            // 
            // numericUpDownTalalatokSzama
            // 
            this.numericUpDownTalalatokSzama.Location = new System.Drawing.Point(115, 245);
            this.numericUpDownTalalatokSzama.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownTalalatokSzama.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownTalalatokSzama.Name = "numericUpDownTalalatokSzama";
            this.numericUpDownTalalatokSzama.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownTalalatokSzama.TabIndex = 29;
            this.numericUpDownTalalatokSzama.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownTalalatokSzama.ValueChanged += new System.EventHandler(this.numericUpDownTalalatokSzama_ValueChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 729);
            this.Controls.Add(this.numericUpDownTalalatokSzama);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panelKeres);
            this.Controls.Add(this.buttonKozepiskolaAnyakonyv);
            this.Controls.Add(this.buttonSzakmaiViszgaAnyakonyv);
            this.Controls.Add(this.buttonSzakmaiVizsgaTorzslap);
            this.Controls.Add(this.buttonErettsegiTanusitvany);
            this.Controls.Add(this.buttonErettsegiTorzslap);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelKapcsolatAdatbazissal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBoxButtons);
            this.Controls.Add(this.groupBoxEleresi);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Érettségi - ";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxEleresi.ResumeLayout(false);
            this.groupBoxEleresi.PerformLayout();
            this.groupBoxButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownViszgaÉve)).EndInit();
            this.panelKeres.ResumeLayout(false);
            this.panelKeres.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTalalatokSzama)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonErettsegi;
        private System.Windows.Forms.Button buttonSzakmaiVizsga;
        private System.Windows.Forms.Button buttonKozepiskola;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMentesiHely;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonTallozas;
        private System.Windows.Forms.GroupBox groupBoxEleresi;
        private System.Windows.Forms.GroupBox groupBoxButtons;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelKapcsolatAdatbazissal;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonErettsegiTorzslap;
        private System.Windows.Forms.Button buttonErettsegiTanusitvany;
        private System.Windows.Forms.Button buttonSzakmaiVizsgaTorzslap;
        private System.Windows.Forms.Button buttonSzakmaiViszgaAnyakonyv;
        private System.Windows.Forms.Button buttonKozepiskolaAnyakonyv;
        private System.Windows.Forms.TextBox textBoxTanuloNeve;
        private System.Windows.Forms.TextBox textBoxAnyjaNeve;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownViszgaÉve;
        private System.Windows.Forms.CheckBox checkBoxViszgaEve;
        private System.Windows.Forms.Panel panelKeres;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownTalalatokSzama;
    }
}

