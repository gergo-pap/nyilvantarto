namespace Nyilvantarto_v2
{
    partial class FormSzakmaiVizsgaTörzslap
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
            this.buttonVissza = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAnyjaNeveKeres = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAnyjaNeveFeltolt = new System.Windows.Forms.TextBox();
            this.listBoxKeresesEredmenye = new System.Windows.Forms.ListBox();
            this.numericUpDownKeres = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTanuloNeveKeres = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownFeltolt = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTanuloNeveFeltolt = new System.Windows.Forms.TextBox();
            this.textBoxEleresi = new System.Windows.Forms.TextBox();
            this.buttonTallozas = new System.Windows.Forms.Button();
            this.labelDokumentumFelvétel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDokumentumNeve = new System.Windows.Forms.TextBox();
            this.buttonLetoltes = new System.Windows.Forms.Button();
            this.buttonFeltoltes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFeltolt)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonVissza
            // 
            this.buttonVissza.Location = new System.Drawing.Point(12, 415);
            this.buttonVissza.Name = "buttonVissza";
            this.buttonVissza.Size = new System.Drawing.Size(75, 23);
            this.buttonVissza.TabIndex = 1;
            this.buttonVissza.Text = "Vissza";
            this.buttonVissza.UseVisualStyleBackColor = true;
            this.buttonVissza.Click += new System.EventHandler(this.buttonVissza_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(476, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 82;
            this.label11.Text = "Tanulo neve";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(631, 190);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 81;
            this.label10.Text = "Anyja neve";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(549, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 80;
            this.label9.Text = "Érettségi éve";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(476, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "Anyja neve:";
            // 
            // textBoxAnyjaNeveKeres
            // 
            this.textBoxAnyjaNeveKeres.Location = new System.Drawing.Point(552, 100);
            this.textBoxAnyjaNeveKeres.Name = "textBoxAnyjaNeveKeres";
            this.textBoxAnyjaNeveKeres.Size = new System.Drawing.Size(186, 20);
            this.textBoxAnyjaNeveKeres.TabIndex = 67;
            this.textBoxAnyjaNeveKeres.TextChanged += new System.EventHandler(this.textBoxAnyjaNeveKeres_TextChanged_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 78;
            this.label7.Text = "Anyja neve:";
            // 
            // textBoxAnyjaNeveFeltolt
            // 
            this.textBoxAnyjaNeveFeltolt.Location = new System.Drawing.Point(148, 101);
            this.textBoxAnyjaNeveFeltolt.Name = "textBoxAnyjaNeveFeltolt";
            this.textBoxAnyjaNeveFeltolt.Size = new System.Drawing.Size(178, 20);
            this.textBoxAnyjaNeveFeltolt.TabIndex = 60;
            // 
            // listBoxKeresesEredmenye
            // 
            this.listBoxKeresesEredmenye.FormattingEnabled = true;
            this.listBoxKeresesEredmenye.Location = new System.Drawing.Point(479, 206);
            this.listBoxKeresesEredmenye.Name = "listBoxKeresesEredmenye";
            this.listBoxKeresesEredmenye.Size = new System.Drawing.Size(259, 134);
            this.listBoxKeresesEredmenye.TabIndex = 69;
            // 
            // numericUpDownKeres
            // 
            this.numericUpDownKeres.Location = new System.Drawing.Point(552, 135);
            this.numericUpDownKeres.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownKeres.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownKeres.Name = "numericUpDownKeres";
            this.numericUpDownKeres.Size = new System.Drawing.Size(186, 20);
            this.numericUpDownKeres.TabIndex = 68;
            this.numericUpDownKeres.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownKeres.ValueChanged += new System.EventHandler(this.numericUpDownKeres_ValueChanged_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(474, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 77;
            this.label6.Text = "Érettségi éve:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(476, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "Tanuló neve:";
            // 
            // textBoxTanuloNeveKeres
            // 
            this.textBoxTanuloNeveKeres.Location = new System.Drawing.Point(552, 74);
            this.textBoxTanuloNeveKeres.Name = "textBoxTanuloNeveKeres";
            this.textBoxTanuloNeveKeres.Size = new System.Drawing.Size(186, 20);
            this.textBoxTanuloNeveKeres.TabIndex = 66;
            this.textBoxTanuloNeveKeres.TextChanged += new System.EventHandler(this.textBoxTanuloNeveKeres_TextChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(580, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Keresés";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Érettségi éve:";
            // 
            // numericUpDownFeltolt
            // 
            this.numericUpDownFeltolt.Location = new System.Drawing.Point(148, 154);
            this.numericUpDownFeltolt.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownFeltolt.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownFeltolt.Name = "numericUpDownFeltolt";
            this.numericUpDownFeltolt.Size = new System.Drawing.Size(178, 20);
            this.numericUpDownFeltolt.TabIndex = 62;
            this.numericUpDownFeltolt.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Tanuló neve:";
            // 
            // textBoxTanuloNeveFeltolt
            // 
            this.textBoxTanuloNeveFeltolt.Location = new System.Drawing.Point(148, 74);
            this.textBoxTanuloNeveFeltolt.Name = "textBoxTanuloNeveFeltolt";
            this.textBoxTanuloNeveFeltolt.Size = new System.Drawing.Size(178, 20);
            this.textBoxTanuloNeveFeltolt.TabIndex = 59;
            // 
            // textBoxEleresi
            // 
            this.textBoxEleresi.Location = new System.Drawing.Point(148, 180);
            this.textBoxEleresi.Name = "textBoxEleresi";
            this.textBoxEleresi.Size = new System.Drawing.Size(178, 20);
            this.textBoxEleresi.TabIndex = 63;
            // 
            // buttonTallozas
            // 
            this.buttonTallozas.Location = new System.Drawing.Point(54, 180);
            this.buttonTallozas.Name = "buttonTallozas";
            this.buttonTallozas.Size = new System.Drawing.Size(75, 23);
            this.buttonTallozas.TabIndex = 64;
            this.buttonTallozas.Text = "Tallózás";
            this.buttonTallozas.UseVisualStyleBackColor = true;
            this.buttonTallozas.Click += new System.EventHandler(this.buttonTallozas_Click_1);
            // 
            // labelDokumentumFelvétel
            // 
            this.labelDokumentumFelvétel.AutoSize = true;
            this.labelDokumentumFelvétel.Location = new System.Drawing.Point(180, 47);
            this.labelDokumentumFelvétel.Name = "labelDokumentumFelvétel";
            this.labelDokumentumFelvétel.Size = new System.Drawing.Size(124, 13);
            this.labelDokumentumFelvétel.TabIndex = 72;
            this.labelDokumentumFelvétel.Text = "Új dokumentum felvétele";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Dokumentum neve:";
            // 
            // textBoxDokumentumNeve
            // 
            this.textBoxDokumentumNeve.Location = new System.Drawing.Point(148, 128);
            this.textBoxDokumentumNeve.Name = "textBoxDokumentumNeve";
            this.textBoxDokumentumNeve.Size = new System.Drawing.Size(178, 20);
            this.textBoxDokumentumNeve.TabIndex = 61;
            // 
            // buttonLetoltes
            // 
            this.buttonLetoltes.Location = new System.Drawing.Point(552, 346);
            this.buttonLetoltes.Name = "buttonLetoltes";
            this.buttonLetoltes.Size = new System.Drawing.Size(125, 23);
            this.buttonLetoltes.TabIndex = 70;
            this.buttonLetoltes.Text = "Keresés";
            this.buttonLetoltes.UseVisualStyleBackColor = true;
            this.buttonLetoltes.Click += new System.EventHandler(this.buttonLetoltes_Click);
            // 
            // buttonFeltoltes
            // 
            this.buttonFeltoltes.Location = new System.Drawing.Point(183, 206);
            this.buttonFeltoltes.Name = "buttonFeltoltes";
            this.buttonFeltoltes.Size = new System.Drawing.Size(104, 23);
            this.buttonFeltoltes.TabIndex = 65;
            this.buttonFeltoltes.Text = "Feltöltés";
            this.buttonFeltoltes.UseVisualStyleBackColor = true;
            this.buttonFeltoltes.Click += new System.EventHandler(this.buttonFeltoltes_Click);
            // 
            // FormSzakmaiVizsgaTörzslap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxAnyjaNeveKeres);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxAnyjaNeveFeltolt);
            this.Controls.Add(this.listBoxKeresesEredmenye);
            this.Controls.Add(this.numericUpDownKeres);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTanuloNeveKeres);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownFeltolt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTanuloNeveFeltolt);
            this.Controls.Add(this.textBoxEleresi);
            this.Controls.Add(this.buttonTallozas);
            this.Controls.Add(this.labelDokumentumFelvétel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDokumentumNeve);
            this.Controls.Add(this.buttonLetoltes);
            this.Controls.Add(this.buttonFeltoltes);
            this.Controls.Add(this.buttonVissza);
            this.Name = "FormSzakmaiVizsgaTörzslap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Szakmai vizsga - Törzslap";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFeltolt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonVissza;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxAnyjaNeveKeres;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAnyjaNeveFeltolt;
        private System.Windows.Forms.ListBox listBoxKeresesEredmenye;
        private System.Windows.Forms.NumericUpDown numericUpDownKeres;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTanuloNeveKeres;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownFeltolt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTanuloNeveFeltolt;
        private System.Windows.Forms.TextBox textBoxEleresi;
        private System.Windows.Forms.Button buttonTallozas;
        private System.Windows.Forms.Label labelDokumentumFelvétel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDokumentumNeve;
        private System.Windows.Forms.Button buttonLetoltes;
        private System.Windows.Forms.Button buttonFeltoltes;
    }
}