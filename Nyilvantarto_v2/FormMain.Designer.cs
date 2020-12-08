using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Nyilvantarto_v2
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.labelMentesiHely = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonTallozas = new System.Windows.Forms.Button();
            this.groupBoxEleresi = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.updateDbStateTimer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.labelKapcsolatAdatbazissal = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonErettsegiTorzslap = new System.Windows.Forms.Button();
            this.buttonErettsegiTanusitvany = new System.Windows.Forms.Button();
            this.buttonSzakmaiVizsgaTorzslap = new System.Windows.Forms.Button();
            this.buttonSzakmaiVizsgaAnyakonyv = new System.Windows.Forms.Button();
            this.buttonKozepiskolaAnyakonyv = new System.Windows.Forms.Button();
            this.textBoxTanuloNeveKeres = new System.Windows.Forms.TextBox();
            this.textBoxanyjaNeveKeres = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownVizsgaÉveKeres = new System.Windows.Forms.NumericUpDown();
            this.checkBoxVizsgaEve = new System.Windows.Forms.CheckBox();
            this.panelKeres = new System.Windows.Forms.Panel();
            this.numericUpDownTalalatokSzama = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonFeltoltes = new System.Windows.Forms.Button();
            this.buttonModositas = new System.Windows.Forms.Button();
            this.buttonTorles = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelFeltolt = new System.Windows.Forms.Panel();
            this.panelKozepiskolaAnyakonyvFeltolt = new System.Windows.Forms.Panel();
            this.panelKozepiskolaAnyakonyvFilneName = new System.Windows.Forms.Panel();
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv = new System.Windows.Forms.TextBox();
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv = new System.Windows.Forms.TextBox();
            this.panelErettsegiTorzslapFeltolt = new System.Windows.Forms.Panel();
            this.panelErettsegiTtorzslapFileName = new System.Windows.Forms.Panel();
            this.textBoxFileNameFeltoltErettsegiTorzslap = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.radioButtonOszFeltoltErettsegiTorzslap = new System.Windows.Forms.RadioButton();
            this.radioButtonTavaszFeltoltErettsegiTorzslap = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap = new System.Windows.Forms.TextBox();
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap = new System.Windows.Forms.TextBox();
            this.panelSzakmaiVizsgaAnyakonyvFeltolt = new System.Windows.Forms.Panel();
            this.panelSzakmaivizsgaAnyakonyvFileName = new System.Windows.Forms.Panel();
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv = new System.Windows.Forms.TextBox();
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt = new System.Windows.Forms.TextBox();
            this.panelTallozMentesujButton = new System.Windows.Forms.Panel();
            this.buttonTalloz = new System.Windows.Forms.Button();
            this.buttonMentesUj = new System.Windows.Forms.Button();
            this.panelErettsegiTanusitvanyFeltolt = new System.Windows.Forms.Panel();
            this.panelErettsegiTanusitvanyFileName = new System.Windows.Forms.Panel();
            this.textBoxFileNameFeltoltErettsegiTanusitvany = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany = new System.Windows.Forms.TextBox();
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany = new System.Windows.Forms.TextBox();
            this.panelSzakmaiVizsgaTorzslapFeltolt = new System.Windows.Forms.Panel();
            this.panelSzakmaiVizsgaTorzslapFileName = new System.Windows.Forms.Panel();
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap = new System.Windows.Forms.RadioButton();
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap = new System.Windows.Forms.TextBox();
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap = new System.Windows.Forms.TextBox();
            this.buttonMegse = new System.Windows.Forms.Button();
            this.labelMenuKat = new System.Windows.Forms.Label();
            this.buttonMentes = new System.Windows.Forms.Button();
            this.panelFeltModTorl = new System.Windows.Forms.Panel();
            this.panelModTorol = new System.Windows.Forms.Panel();
            this.groupBoxAlso = new System.Windows.Forms.GroupBox();
            this.groupBoxEleresi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVizsgaÉveKeres)).BeginInit();
            this.panelKeres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTalalatokSzama)).BeginInit();
            this.panelMenu.SuspendLayout();
            this.panelFeltolt.SuspendLayout();
            this.panelKozepiskolaAnyakonyvFeltolt.SuspendLayout();
            this.panelKozepiskolaAnyakonyvFilneName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv)).BeginInit();
            this.panelErettsegiTorzslapFeltolt.SuspendLayout();
            this.panelErettsegiTtorzslapFileName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap)).BeginInit();
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.SuspendLayout();
            this.panelSzakmaivizsgaAnyakonyvFileName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv)).BeginInit();
            this.panelTallozMentesujButton.SuspendLayout();
            this.panelErettsegiTanusitvanyFeltolt.SuspendLayout();
            this.panelErettsegiTanusitvanyFileName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany)).BeginInit();
            this.panelSzakmaiVizsgaTorzslapFeltolt.SuspendLayout();
            this.panelSzakmaiVizsgaTorzslapFileName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap)).BeginInit();
            this.panelFeltModTorl.SuspendLayout();
            this.panelModTorol.SuspendLayout();
            this.groupBoxAlso.SuspendLayout();
            this.SuspendLayout();
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
            this.buttonTallozas.Click += new System.EventHandler(this.ButtonTallozas_Click);
            // 
            // groupBoxEleresi
            // 
            this.groupBoxEleresi.Controls.Add(this.label3);
            this.groupBoxEleresi.Controls.Add(this.label1);
            this.groupBoxEleresi.Controls.Add(this.labelPath);
            this.groupBoxEleresi.Controls.Add(this.buttonTallozas);
            this.groupBoxEleresi.Controls.Add(this.labelMentesiHely);
            this.groupBoxEleresi.Controls.Add(this.label2);
            this.groupBoxEleresi.Location = new System.Drawing.Point(828, 13);
            this.groupBoxEleresi.Name = "groupBoxEleresi";
            this.groupBoxEleresi.Size = new System.Drawing.Size(297, 127);
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
            // updateDbStateTimer
            // 
            this.updateDbStateTimer.Enabled = true;
            this.updateDbStateTimer.Interval = 2000;
            this.updateDbStateTimer.Tick += new System.EventHandler(this.updateDbStateTimer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1410, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Kapcsolat az adatbázissal:";
            // 
            // labelKapcsolatAdatbazissal
            // 
            this.labelKapcsolatAdatbazissal.AutoSize = true;
            this.labelKapcsolatAdatbazissal.Location = new System.Drawing.Point(1548, 17);
            this.labelKapcsolatAdatbazissal.Name = "labelKapcsolatAdatbazissal";
            this.labelKapcsolatAdatbazissal.Size = new System.Drawing.Size(32, 13);
            this.labelKapcsolatAdatbazissal.TabIndex = 11;
            this.labelKapcsolatAdatbazissal.Text = "aktív";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(620, 315);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(950, 507);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // buttonErettsegiTorzslap
            // 
            this.buttonErettsegiTorzslap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonErettsegiTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonErettsegiTorzslap.Location = new System.Drawing.Point(6, 5);
            this.buttonErettsegiTorzslap.Name = "buttonErettsegiTorzslap";
            this.buttonErettsegiTorzslap.Size = new System.Drawing.Size(160, 30);
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
            this.buttonErettsegiTanusitvany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonErettsegiTanusitvany.Location = new System.Drawing.Point(195, 5);
            this.buttonErettsegiTanusitvany.Name = "buttonErettsegiTanusitvany";
            this.buttonErettsegiTanusitvany.Size = new System.Drawing.Size(160, 30);
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
            this.buttonSzakmaiVizsgaTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSzakmaiVizsgaTorzslap.Location = new System.Drawing.Point(410, 7);
            this.buttonSzakmaiVizsgaTorzslap.Name = "buttonSzakmaiVizsgaTorzslap";
            this.buttonSzakmaiVizsgaTorzslap.Size = new System.Drawing.Size(160, 30);
            this.buttonSzakmaiVizsgaTorzslap.TabIndex = 15;
            this.buttonSzakmaiVizsgaTorzslap.Text = "Szakmai vizsga - törzslap";
            this.buttonSzakmaiVizsgaTorzslap.UseVisualStyleBackColor = true;
            this.buttonSzakmaiVizsgaTorzslap.Click += new System.EventHandler(this.buttonSzakmaiVizsgaTorzslap_Click);
            // 
            // buttonSzakmaiVizsgaAnyakonyv
            // 
            this.buttonSzakmaiVizsgaAnyakonyv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSzakmaiVizsgaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSzakmaiVizsgaAnyakonyv.Location = new System.Drawing.Point(601, 7);
            this.buttonSzakmaiVizsgaAnyakonyv.Name = "buttonSzakmaiVizsgaAnyakonyv";
            this.buttonSzakmaiVizsgaAnyakonyv.Size = new System.Drawing.Size(167, 30);
            this.buttonSzakmaiVizsgaAnyakonyv.TabIndex = 16;
            this.buttonSzakmaiVizsgaAnyakonyv.Text = "Szakmai vizsga - anyakönyv";
            this.buttonSzakmaiVizsgaAnyakonyv.UseVisualStyleBackColor = true;
            this.buttonSzakmaiVizsgaAnyakonyv.Click += new System.EventHandler(this.buttonSzakmaiVizsgaAnyakonyv_Click);
            // 
            // buttonKozepiskolaAnyakonyv
            // 
            this.buttonKozepiskolaAnyakonyv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKozepiskolaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonKozepiskolaAnyakonyv.Location = new System.Drawing.Point(782, 5);
            this.buttonKozepiskolaAnyakonyv.Name = "buttonKozepiskolaAnyakonyv";
            this.buttonKozepiskolaAnyakonyv.Size = new System.Drawing.Size(160, 30);
            this.buttonKozepiskolaAnyakonyv.TabIndex = 17;
            this.buttonKozepiskolaAnyakonyv.Text = "Középiskola - anyakönyv";
            this.buttonKozepiskolaAnyakonyv.UseVisualStyleBackColor = true;
            this.buttonKozepiskolaAnyakonyv.Click += new System.EventHandler(this.buttonKozepiskolaAnyakonyv_Click);
            // 
            // textBoxTanuloNeveKeres
            // 
            this.textBoxTanuloNeveKeres.Location = new System.Drawing.Point(68, 61);
            this.textBoxTanuloNeveKeres.Name = "textBoxTanuloNeveKeres";
            this.textBoxTanuloNeveKeres.Size = new System.Drawing.Size(150, 20);
            this.textBoxTanuloNeveKeres.TabIndex = 18;
            this.textBoxTanuloNeveKeres.TextChanged += new System.EventHandler(this.TextBoxTanuloNeve_TextChanged);
            // 
            // textBoxanyjaNeveKeres
            // 
            this.textBoxanyjaNeveKeres.Location = new System.Drawing.Point(292, 61);
            this.textBoxanyjaNeveKeres.Name = "textBoxanyjaNeveKeres";
            this.textBoxanyjaNeveKeres.Size = new System.Drawing.Size(150, 20);
            this.textBoxanyjaNeveKeres.TabIndex = 19;
            this.textBoxanyjaNeveKeres.TextChanged += new System.EventHandler(this.textBoxAnyjaNeve_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tanuló neve";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(329, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Anyja neve";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(554, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Vizsga/Érettségi éve";
            // 
            // numericUpDownVizsgaÉveKeres
            // 
            this.numericUpDownVizsgaÉveKeres.Location = new System.Drawing.Point(533, 61);
            this.numericUpDownVizsgaÉveKeres.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownVizsgaÉveKeres.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownVizsgaÉveKeres.Name = "numericUpDownVizsgaÉveKeres";
            this.numericUpDownVizsgaÉveKeres.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownVizsgaÉveKeres.TabIndex = 25;
            this.numericUpDownVizsgaÉveKeres.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownVizsgaÉveKeres.ValueChanged += new System.EventHandler(this.numericUpDownVizsgaÉve_ValueChanged);
            // 
            // checkBoxVizsgaEve
            // 
            this.checkBoxVizsgaEve.AutoSize = true;
            this.checkBoxVizsgaEve.Location = new System.Drawing.Point(533, 43);
            this.checkBoxVizsgaEve.Name = "checkBoxVizsgaEve";
            this.checkBoxVizsgaEve.Size = new System.Drawing.Size(15, 14);
            this.checkBoxVizsgaEve.TabIndex = 26;
            this.checkBoxVizsgaEve.UseVisualStyleBackColor = true;
            this.checkBoxVizsgaEve.CheckedChanged += new System.EventHandler(this.checkBoxVizsgaEve_CheckedChanged);
            // 
            // panelKeres
            // 
            this.panelKeres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKeres.Controls.Add(this.numericUpDownTalalatokSzama);
            this.panelKeres.Controls.Add(this.buttonKozepiskolaAnyakonyv);
            this.panelKeres.Controls.Add(this.buttonSzakmaiVizsgaAnyakonyv);
            this.panelKeres.Controls.Add(this.label8);
            this.panelKeres.Controls.Add(this.buttonSzakmaiVizsgaTorzslap);
            this.panelKeres.Controls.Add(this.label5);
            this.panelKeres.Controls.Add(this.buttonErettsegiTanusitvany);
            this.panelKeres.Controls.Add(this.checkBoxVizsgaEve);
            this.panelKeres.Controls.Add(this.buttonErettsegiTorzslap);
            this.panelKeres.Controls.Add(this.textBoxTanuloNeveKeres);
            this.panelKeres.Controls.Add(this.numericUpDownVizsgaÉveKeres);
            this.panelKeres.Controls.Add(this.textBoxanyjaNeveKeres);
            this.panelKeres.Controls.Add(this.label7);
            this.panelKeres.Controls.Add(this.label6);
            this.panelKeres.Location = new System.Drawing.Point(620, 218);
            this.panelKeres.Name = "panelKeres";
            this.panelKeres.Size = new System.Drawing.Size(945, 84);
            this.panelKeres.TabIndex = 27;
            this.panelKeres.Visible = false;
            // 
            // numericUpDownTalalatokSzama
            // 
            this.numericUpDownTalalatokSzama.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownTalalatokSzama.Location = new System.Drawing.Point(836, 53);
            this.numericUpDownTalalatokSzama.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownTalalatokSzama.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownTalalatokSzama.Name = "numericUpDownTalalatokSzama";
            this.numericUpDownTalalatokSzama.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownTalalatokSzama.TabIndex = 29;
            this.numericUpDownTalalatokSzama.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownTalalatokSzama.ValueChanged += new System.EventHandler(this.numericUpDownTalalatokSzama_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(740, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Találatok száma:";
            // 
            // buttonFeltoltes
            // 
            this.buttonFeltoltes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonFeltoltes.Location = new System.Drawing.Point(17, 6);
            this.buttonFeltoltes.Margin = new System.Windows.Forms.Padding(10);
            this.buttonFeltoltes.Name = "buttonFeltoltes";
            this.buttonFeltoltes.Size = new System.Drawing.Size(149, 49);
            this.buttonFeltoltes.TabIndex = 28;
            this.buttonFeltoltes.Text = "Feltöltés";
            this.buttonFeltoltes.UseVisualStyleBackColor = true;
            this.buttonFeltoltes.Click += new System.EventHandler(this.ButtonFeltoltes_Click);
            // 
            // buttonModositas
            // 
            this.buttonModositas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonModositas.Location = new System.Drawing.Point(4, 2);
            this.buttonModositas.Margin = new System.Windows.Forms.Padding(10);
            this.buttonModositas.Name = "buttonModositas";
            this.buttonModositas.Size = new System.Drawing.Size(149, 49);
            this.buttonModositas.TabIndex = 29;
            this.buttonModositas.Text = "Módosítás";
            this.buttonModositas.UseVisualStyleBackColor = true;
            this.buttonModositas.Click += new System.EventHandler(this.ButtonModositas_Click);
            // 
            // buttonTorles
            // 
            this.buttonTorles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonTorles.Location = new System.Drawing.Point(4, 59);
            this.buttonTorles.Margin = new System.Windows.Forms.Padding(10);
            this.buttonTorles.Name = "buttonTorles";
            this.buttonTorles.Size = new System.Drawing.Size(149, 49);
            this.buttonTorles.TabIndex = 30;
            this.buttonTorles.Text = "Törlés";
            this.buttonTorles.UseVisualStyleBackColor = true;
            this.buttonTorles.Click += new System.EventHandler(this.ButtonTorles_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMenu.Controls.Add(this.panelFeltolt);
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(600, 822);
            this.panelMenu.TabIndex = 31;
            this.panelMenu.Visible = false;
            // 
            // panelFeltolt
            // 
            this.panelFeltolt.Controls.Add(this.panelKozepiskolaAnyakonyvFeltolt);
            this.panelFeltolt.Controls.Add(this.panelErettsegiTorzslapFeltolt);
            this.panelFeltolt.Controls.Add(this.panelSzakmaiVizsgaAnyakonyvFeltolt);
            this.panelFeltolt.Controls.Add(this.panelTallozMentesujButton);
            this.panelFeltolt.Controls.Add(this.panelErettsegiTanusitvanyFeltolt);
            this.panelFeltolt.Controls.Add(this.panelSzakmaiVizsgaTorzslapFeltolt);
            this.panelFeltolt.Controls.Add(this.buttonMegse);
            this.panelFeltolt.Controls.Add(this.labelMenuKat);
            this.panelFeltolt.Controls.Add(this.buttonMentes);
            this.panelFeltolt.Location = new System.Drawing.Point(12, 16);
            this.panelFeltolt.Name = "panelFeltolt";
            this.panelFeltolt.Size = new System.Drawing.Size(576, 772);
            this.panelFeltolt.TabIndex = 12;
            this.panelFeltolt.Visible = false;
            // 
            // panelKozepiskolaAnyakonyvFeltolt
            // 
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.panelKozepiskolaAnyakonyvFilneName);
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv);
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.label29);
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv);
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.label31);
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.label32);
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.label33);
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv);
            this.panelKozepiskolaAnyakonyvFeltolt.Controls.Add(this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv);
            this.panelKozepiskolaAnyakonyvFeltolt.Location = new System.Drawing.Point(53, 389);
            this.panelKozepiskolaAnyakonyvFeltolt.Name = "panelKozepiskolaAnyakonyvFeltolt";
            this.panelKozepiskolaAnyakonyvFeltolt.Size = new System.Drawing.Size(550, 170);
            this.panelKozepiskolaAnyakonyvFeltolt.TabIndex = 20;
            this.panelKozepiskolaAnyakonyvFeltolt.Visible = false;
            // 
            // panelKozepiskolaAnyakonyvFilneName
            // 
            this.panelKozepiskolaAnyakonyvFilneName.Controls.Add(this.textBoxFileNameFeltoltKozepsikolaAnyakonyv);
            this.panelKozepiskolaAnyakonyvFilneName.Controls.Add(this.label30);
            this.panelKozepiskolaAnyakonyvFilneName.Location = new System.Drawing.Point(14, 135);
            this.panelKozepiskolaAnyakonyvFilneName.Name = "panelKozepiskolaAnyakonyvFilneName";
            this.panelKozepiskolaAnyakonyvFilneName.Size = new System.Drawing.Size(522, 32);
            this.panelKozepiskolaAnyakonyvFilneName.TabIndex = 18;
            // 
            // textBoxFileNameFeltoltKozepsikolaAnyakonyv
            // 
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.Location = new System.Drawing.Point(214, 5);
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.Name = "textBoxFileNameFeltoltKozepsikolaAnyakonyv";
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.ReadOnly = true;
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.Size = new System.Drawing.Size(300, 24);
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.TabIndex = 11;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label30.Location = new System.Drawing.Point(6, 8);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(35, 18);
            this.label30.TabIndex = 12;
            this.label30.Text = "Fájl:";
            // 
            // numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv
            // 
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Location = new System.Drawing.Point(222, 105);
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Minimum = new decimal(new int[] {
            1904,
            0,
            0,
            0});
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Name = "numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv";
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Size = new System.Drawing.Size(100, 24);
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.TabIndex = 18;
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Value = new decimal(new int[] {
            1904,
            0,
            0,
            0});
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label29.Location = new System.Drawing.Point(11, 108);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(97, 18);
            this.label29.TabIndex = 17;
            this.label29.Text = "Érettségi éve:";
            // 
            // numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv
            // 
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Location = new System.Drawing.Point(222, 75);
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Name = "numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv";
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Size = new System.Drawing.Size(100, 24);
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.TabIndex = 16;
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.ValueChanged += new System.EventHandler(this.numericUpDownKozepiskKezdeteKozepiskolaAnyakonyv_ValueChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label31.Location = new System.Drawing.Point(11, 78);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(150, 18);
            this.label31.TabIndex = 5;
            this.label31.Text = "Középiskola kezdete:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label32.Location = new System.Drawing.Point(11, 18);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(88, 18);
            this.label32.TabIndex = 3;
            this.label32.Text = "Tanuló neve";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label33.Location = new System.Drawing.Point(11, 48);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(78, 18);
            this.label33.TabIndex = 4;
            this.label33.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv
            // 
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Location = new System.Drawing.Point(222, 45);
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Name = "textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv";
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Size = new System.Drawing.Size(300, 24);
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.TabIndex = 1;
            // 
            // textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv
            // 
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Location = new System.Drawing.Point(222, 15);
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Name = "textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv";
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Size = new System.Drawing.Size(300, 24);
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.TabIndex = 0;
            // 
            // panelErettsegiTorzslapFeltolt
            // 
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.panelErettsegiTtorzslapFileName);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.label14);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.radioButtonOszFeltoltErettsegiTorzslap);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.radioButtonTavaszFeltoltErettsegiTorzslap);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.label16);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.label17);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.label18);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.textBoxAnyjaNeveFeltoltErettsegiTorzslap);
            this.panelErettsegiTorzslapFeltolt.Controls.Add(this.textBoxTanuloNeveFeltoltErettsegiTorzslap);
            this.panelErettsegiTorzslapFeltolt.Location = new System.Drawing.Point(30, 392);
            this.panelErettsegiTorzslapFeltolt.Name = "panelErettsegiTorzslapFeltolt";
            this.panelErettsegiTorzslapFeltolt.Size = new System.Drawing.Size(550, 170);
            this.panelErettsegiTorzslapFeltolt.TabIndex = 17;
            this.panelErettsegiTorzslapFeltolt.Visible = false;
            // 
            // panelErettsegiTtorzslapFileName
            // 
            this.panelErettsegiTtorzslapFileName.Controls.Add(this.textBoxFileNameFeltoltErettsegiTorzslap);
            this.panelErettsegiTtorzslapFileName.Controls.Add(this.label15);
            this.panelErettsegiTtorzslapFileName.Location = new System.Drawing.Point(11, 130);
            this.panelErettsegiTtorzslapFileName.Name = "panelErettsegiTtorzslapFileName";
            this.panelErettsegiTtorzslapFileName.Size = new System.Drawing.Size(522, 32);
            this.panelErettsegiTtorzslapFileName.TabIndex = 20;
            // 
            // textBoxFileNameFeltoltErettsegiTorzslap
            // 
            this.textBoxFileNameFeltoltErettsegiTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltErettsegiTorzslap.Location = new System.Drawing.Point(219, 5);
            this.textBoxFileNameFeltoltErettsegiTorzslap.Name = "textBoxFileNameFeltoltErettsegiTorzslap";
            this.textBoxFileNameFeltoltErettsegiTorzslap.ReadOnly = true;
            this.textBoxFileNameFeltoltErettsegiTorzslap.Size = new System.Drawing.Size(300, 24);
            this.textBoxFileNameFeltoltErettsegiTorzslap.TabIndex = 11;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label15.Location = new System.Drawing.Point(8, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 18);
            this.label15.TabIndex = 12;
            this.label15.Text = "Fájl:";
            // 
            // numericUpDownVizsgaEveFeltoltErettsegiTorzslap
            // 
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Location = new System.Drawing.Point(222, 72);
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Name = "numericUpDownVizsgaEveFeltoltErettsegiTorzslap";
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Size = new System.Drawing.Size(75, 24);
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.TabIndex = 16;
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label14.Location = new System.Drawing.Point(11, 104);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 18);
            this.label14.TabIndex = 15;
            this.label14.Text = "Időszak:";
            // 
            // radioButtonOszFeltoltErettsegiTorzslap
            // 
            this.radioButtonOszFeltoltErettsegiTorzslap.AutoSize = true;
            this.radioButtonOszFeltoltErettsegiTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonOszFeltoltErettsegiTorzslap.Location = new System.Drawing.Point(467, 102);
            this.radioButtonOszFeltoltErettsegiTorzslap.Name = "radioButtonOszFeltoltErettsegiTorzslap";
            this.radioButtonOszFeltoltErettsegiTorzslap.Size = new System.Drawing.Size(54, 22);
            this.radioButtonOszFeltoltErettsegiTorzslap.TabIndex = 14;
            this.radioButtonOszFeltoltErettsegiTorzslap.Text = "Ősz";
            this.radioButtonOszFeltoltErettsegiTorzslap.UseVisualStyleBackColor = true;
            // 
            // radioButtonTavaszFeltoltErettsegiTorzslap
            // 
            this.radioButtonTavaszFeltoltErettsegiTorzslap.AutoSize = true;
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Checked = true;
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Location = new System.Drawing.Point(222, 102);
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Name = "radioButtonTavaszFeltoltErettsegiTorzslap";
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Size = new System.Drawing.Size(74, 22);
            this.radioButtonTavaszFeltoltErettsegiTorzslap.TabIndex = 13;
            this.radioButtonTavaszFeltoltErettsegiTorzslap.TabStop = true;
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Text = "Tavasz";
            this.radioButtonTavaszFeltoltErettsegiTorzslap.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label16.Location = new System.Drawing.Point(11, 74);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 18);
            this.label16.TabIndex = 5;
            this.label16.Text = "Vizsga éve";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label17.Location = new System.Drawing.Point(11, 17);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 18);
            this.label17.TabIndex = 3;
            this.label17.Text = "Tanuló neve";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label18.Location = new System.Drawing.Point(11, 45);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 18);
            this.label18.TabIndex = 4;
            this.label18.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltErettsegiTorzslap
            // 
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.Location = new System.Drawing.Point(222, 42);
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.Name = "textBoxAnyjaNeveFeltoltErettsegiTorzslap";
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.Size = new System.Drawing.Size(300, 24);
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.TabIndex = 1;
            // 
            // textBoxTanuloNeveFeltoltErettsegiTorzslap
            // 
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.Location = new System.Drawing.Point(222, 14);
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.Name = "textBoxTanuloNeveFeltoltErettsegiTorzslap";
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.Size = new System.Drawing.Size(300, 24);
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.TabIndex = 0;
            // 
            // panelSzakmaiVizsgaAnyakonyvFeltolt
            // 
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.panelSzakmaivizsgaAnyakonyvFileName);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.label24);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.label26);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.label27);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.label28);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Controls.Add(this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Location = new System.Drawing.Point(10, 396);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Name = "panelSzakmaiVizsgaAnyakonyvFeltolt";
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Size = new System.Drawing.Size(550, 166);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.TabIndex = 21;
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Visible = false;
            // 
            // panelSzakmaivizsgaAnyakonyvFileName
            // 
            this.panelSzakmaivizsgaAnyakonyvFileName.Controls.Add(this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv);
            this.panelSzakmaivizsgaAnyakonyvFileName.Controls.Add(this.label25);
            this.panelSzakmaivizsgaAnyakonyvFileName.Location = new System.Drawing.Point(11, 134);
            this.panelSzakmaivizsgaAnyakonyvFileName.Name = "panelSzakmaivizsgaAnyakonyvFileName";
            this.panelSzakmaivizsgaAnyakonyvFileName.Size = new System.Drawing.Size(522, 32);
            this.panelSzakmaivizsgaAnyakonyvFileName.TabIndex = 20;
            // 
            // textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv
            // 
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Location = new System.Drawing.Point(211, 9);
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Multiline = true;
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Name = "textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv";
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.ReadOnly = true;
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Size = new System.Drawing.Size(300, 20);
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.TabIndex = 11;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label25.Location = new System.Drawing.Point(3, 9);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(35, 18);
            this.label25.TabIndex = 12;
            this.label25.Text = "Fájl:";
            // 
            // numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv
            // 
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Location = new System.Drawing.Point(222, 104);
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Minimum = new decimal(new int[] {
            1902,
            0,
            0,
            0});
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Name = "numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv";
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Size = new System.Drawing.Size(100, 24);
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.TabIndex = 18;
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Value = new decimal(new int[] {
            1904,
            0,
            0,
            0});
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label24.Location = new System.Drawing.Point(14, 106);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(97, 18);
            this.label24.TabIndex = 17;
            this.label24.Text = "Érettségi éve:";
            // 
            // numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv
            // 
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Location = new System.Drawing.Point(222, 72);
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Minimum = new decimal(new int[] {
            1899,
            0,
            0,
            0});
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Name = "numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv";
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Size = new System.Drawing.Size(100, 24);
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.TabIndex = 16;
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.ValueChanged += new System.EventHandler(this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv_ValueChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label26.Location = new System.Drawing.Point(14, 76);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(150, 18);
            this.label26.TabIndex = 5;
            this.label26.Text = "Középiskola kezdete:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label27.Location = new System.Drawing.Point(14, 17);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(88, 18);
            this.label27.TabIndex = 3;
            this.label27.Text = "Tanuló neve";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label28.Location = new System.Drawing.Point(14, 47);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(78, 18);
            this.label28.TabIndex = 4;
            this.label28.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv
            // 
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Location = new System.Drawing.Point(222, 44);
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Name = "textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv";
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Size = new System.Drawing.Size(300, 24);
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.TabIndex = 1;
            // 
            // textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt
            // 
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Location = new System.Drawing.Point(222, 14);
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Name = "textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt";
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Size = new System.Drawing.Size(300, 24);
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.TabIndex = 0;
            // 
            // panelTallozMentesujButton
            // 
            this.panelTallozMentesujButton.Controls.Add(this.buttonTalloz);
            this.panelTallozMentesujButton.Controls.Add(this.buttonMentesUj);
            this.panelTallozMentesujButton.Location = new System.Drawing.Point(196, 562);
            this.panelTallozMentesujButton.Name = "panelTallozMentesujButton";
            this.panelTallozMentesujButton.Size = new System.Drawing.Size(154, 143);
            this.panelTallozMentesujButton.TabIndex = 22;
            // 
            // buttonTalloz
            // 
            this.buttonTalloz.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonTalloz.Location = new System.Drawing.Point(0, 10);
            this.buttonTalloz.Margin = new System.Windows.Forms.Padding(10);
            this.buttonTalloz.Name = "buttonTalloz";
            this.buttonTalloz.Size = new System.Drawing.Size(153, 41);
            this.buttonTalloz.TabIndex = 10;
            this.buttonTalloz.Text = "Tallózás";
            this.buttonTalloz.UseVisualStyleBackColor = true;
            this.buttonTalloz.Click += new System.EventHandler(this.ButtonTalloz_Click);
            // 
            // buttonMentesUj
            // 
            this.buttonMentesUj.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonMentesUj.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonMentesUj.Location = new System.Drawing.Point(0, 102);
            this.buttonMentesUj.Margin = new System.Windows.Forms.Padding(10);
            this.buttonMentesUj.Name = "buttonMentesUj";
            this.buttonMentesUj.Size = new System.Drawing.Size(153, 41);
            this.buttonMentesUj.TabIndex = 7;
            this.buttonMentesUj.Text = "Mentés új";
            this.buttonMentesUj.UseVisualStyleBackColor = true;
            this.buttonMentesUj.Click += new System.EventHandler(this.buttonMentesUj_Click);
            // 
            // panelErettsegiTanusitvanyFeltolt
            // 
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.panelErettsegiTanusitvanyFileName);
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany);
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.label19);
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany);
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.label21);
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.label22);
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.label23);
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany);
            this.panelErettsegiTanusitvanyFeltolt.Controls.Add(this.textBoxTanuloNeveFeltoltErettsegiTanusitvany);
            this.panelErettsegiTanusitvanyFeltolt.Location = new System.Drawing.Point(26, 185);
            this.panelErettsegiTanusitvanyFeltolt.Name = "panelErettsegiTanusitvanyFeltolt";
            this.panelErettsegiTanusitvanyFeltolt.Size = new System.Drawing.Size(550, 170);
            this.panelErettsegiTanusitvanyFeltolt.TabIndex = 18;
            this.panelErettsegiTanusitvanyFeltolt.Visible = false;
            // 
            // panelErettsegiTanusitvanyFileName
            // 
            this.panelErettsegiTanusitvanyFileName.Controls.Add(this.textBoxFileNameFeltoltErettsegiTanusitvany);
            this.panelErettsegiTanusitvanyFileName.Controls.Add(this.label20);
            this.panelErettsegiTanusitvanyFileName.Location = new System.Drawing.Point(14, 135);
            this.panelErettsegiTanusitvanyFileName.Name = "panelErettsegiTanusitvanyFileName";
            this.panelErettsegiTanusitvanyFileName.Size = new System.Drawing.Size(522, 32);
            this.panelErettsegiTanusitvanyFileName.TabIndex = 19;
            // 
            // textBoxFileNameFeltoltErettsegiTanusitvany
            // 
            this.textBoxFileNameFeltoltErettsegiTanusitvany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltErettsegiTanusitvany.Location = new System.Drawing.Point(213, 5);
            this.textBoxFileNameFeltoltErettsegiTanusitvany.Name = "textBoxFileNameFeltoltErettsegiTanusitvany";
            this.textBoxFileNameFeltoltErettsegiTanusitvany.ReadOnly = true;
            this.textBoxFileNameFeltoltErettsegiTanusitvany.Size = new System.Drawing.Size(300, 24);
            this.textBoxFileNameFeltoltErettsegiTanusitvany.TabIndex = 11;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label20.Location = new System.Drawing.Point(5, 2);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(35, 18);
            this.label20.TabIndex = 12;
            this.label20.Text = "Fájl:";
            // 
            // textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany
            // 
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Location = new System.Drawing.Point(222, 107);
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.MaxLength = 13;
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Name = "textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany";
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Size = new System.Drawing.Size(300, 24);
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.TabIndex = 18;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label19.Location = new System.Drawing.Point(11, 107);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(130, 18);
            this.label19.TabIndex = 17;
            this.label19.Text = "Tanulói azonosító:";
            // 
            // numericUpDownVizsgaEveFeltoltErettsegiTanusitvany
            // 
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Location = new System.Drawing.Point(222, 77);
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Name = "numericUpDownVizsgaEveFeltoltErettsegiTanusitvany";
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Size = new System.Drawing.Size(100, 24);
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.TabIndex = 16;
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label21.Location = new System.Drawing.Point(11, 74);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(79, 18);
            this.label21.TabIndex = 5;
            this.label21.Text = "Vizsga éve";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label22.Location = new System.Drawing.Point(11, 17);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 18);
            this.label22.TabIndex = 3;
            this.label22.Text = "Tanuló neve";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label23.Location = new System.Drawing.Point(11, 44);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(78, 18);
            this.label23.TabIndex = 4;
            this.label23.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltErettsegiTanusitvany
            // 
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Location = new System.Drawing.Point(222, 47);
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Name = "textBoxAnyjaNeveFeltoltErettsegiTanusitvany";
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Size = new System.Drawing.Size(300, 24);
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.TabIndex = 1;
            // 
            // textBoxTanuloNeveFeltoltErettsegiTanusitvany
            // 
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.Location = new System.Drawing.Point(222, 17);
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.Name = "textBoxTanuloNeveFeltoltErettsegiTanusitvany";
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.Size = new System.Drawing.Size(300, 24);
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.TabIndex = 0;
            // 
            // panelSzakmaiVizsgaTorzslapFeltolt
            // 
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.panelSzakmaiVizsgaTorzslapFileName);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.label13);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.label11);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.label9);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.label10);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Controls.Add(this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Location = new System.Drawing.Point(142, 9);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Name = "panelSzakmaiVizsgaTorzslapFeltolt";
            this.panelSzakmaiVizsgaTorzslapFeltolt.Size = new System.Drawing.Size(550, 170);
            this.panelSzakmaiVizsgaTorzslapFeltolt.TabIndex = 13;
            this.panelSzakmaiVizsgaTorzslapFeltolt.Visible = false;
            // 
            // panelSzakmaiVizsgaTorzslapFileName
            // 
            this.panelSzakmaiVizsgaTorzslapFileName.Controls.Add(this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap);
            this.panelSzakmaiVizsgaTorzslapFileName.Controls.Add(this.label12);
            this.panelSzakmaiVizsgaTorzslapFileName.Location = new System.Drawing.Point(14, 128);
            this.panelSzakmaiVizsgaTorzslapFileName.Name = "panelSzakmaiVizsgaTorzslapFileName";
            this.panelSzakmaiVizsgaTorzslapFileName.Size = new System.Drawing.Size(522, 39);
            this.panelSzakmaiVizsgaTorzslapFileName.TabIndex = 17;
            // 
            // textBoxFileNameFeltoltSzakmaiVizsgaTorzslap
            // 
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Location = new System.Drawing.Point(208, 7);
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Name = "textBoxFileNameFeltoltSzakmaiVizsgaTorzslap";
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.ReadOnly = true;
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Size = new System.Drawing.Size(300, 24);
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.TabIndex = 11;
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.TextChanged += new System.EventHandler(this.textBoxFileNameFeltolt_TextChanged);
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap_Validating);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(7, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 18);
            this.label12.TabIndex = 12;
            this.label12.Text = "Fájl:";
            // 
            // numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap
            // 
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Location = new System.Drawing.Point(222, 74);
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Name = "numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap";
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Size = new System.Drawing.Size(100, 24);
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.TabIndex = 16;
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.Location = new System.Drawing.Point(11, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 18);
            this.label13.TabIndex = 15;
            this.label13.Text = "Időszak:";
            // 
            // radioButtonOszFeltoltSzakmaiVizsgaTorzslap
            // 
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.AutoSize = true;
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Location = new System.Drawing.Point(467, 104);
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Name = "radioButtonOszFeltoltSzakmaiVizsgaTorzslap";
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Size = new System.Drawing.Size(54, 22);
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.TabIndex = 14;
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Text = "Ősz";
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.UseVisualStyleBackColor = true;
            // 
            // radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap
            // 
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.AutoSize = true;
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Checked = true;
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Location = new System.Drawing.Point(222, 104);
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Name = "radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap";
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Size = new System.Drawing.Size(74, 22);
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.TabIndex = 13;
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.TabStop = true;
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Text = "Tavasz";
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(11, 76);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 18);
            this.label11.TabIndex = 5;
            this.label11.Text = "Vizsga éve";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(11, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 18);
            this.label9.TabIndex = 3;
            this.label9.Text = "Tanuló neve";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(11, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 18);
            this.label10.TabIndex = 4;
            this.label10.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap
            // 
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Location = new System.Drawing.Point(222, 44);
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Name = "textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap";
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Size = new System.Drawing.Size(300, 24);
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.TabIndex = 1;
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.TextChanged += new System.EventHandler(this.textBoxAnyjaNeveFeltolt_TextChanged);
            // 
            // textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap
            // 
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Location = new System.Drawing.Point(222, 14);
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Name = "textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap";
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Size = new System.Drawing.Size(300, 24);
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.TabIndex = 0;
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.TextChanged += new System.EventHandler(this.textBoxTanuloNeveFeltolt_TextChanged);
            // 
            // buttonMegse
            // 
            this.buttonMegse.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonMegse.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonMegse.Location = new System.Drawing.Point(30, 664);
            this.buttonMegse.Margin = new System.Windows.Forms.Padding(10);
            this.buttonMegse.Name = "buttonMegse";
            this.buttonMegse.Size = new System.Drawing.Size(153, 41);
            this.buttonMegse.TabIndex = 8;
            this.buttonMegse.Text = "Mégse";
            this.buttonMegse.UseVisualStyleBackColor = true;
            this.buttonMegse.Click += new System.EventHandler(this.buttonMegse_Click);
            // 
            // labelMenuKat
            // 
            this.labelMenuKat.AutoSize = true;
            this.labelMenuKat.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMenuKat.Location = new System.Drawing.Point(80, 16);
            this.labelMenuKat.Name = "labelMenuKat";
            this.labelMenuKat.Size = new System.Drawing.Size(23, 31);
            this.labelMenuKat.TabIndex = 9;
            this.labelMenuKat.Text = ".";
            this.labelMenuKat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonMentes
            // 
            this.buttonMentes.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonMentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonMentes.Location = new System.Drawing.Point(376, 664);
            this.buttonMentes.Margin = new System.Windows.Forms.Padding(10);
            this.buttonMentes.Name = "buttonMentes";
            this.buttonMentes.Size = new System.Drawing.Size(153, 41);
            this.buttonMentes.TabIndex = 6;
            this.buttonMentes.Text = "Mentés";
            this.buttonMentes.UseVisualStyleBackColor = true;
            this.buttonMentes.Click += new System.EventHandler(this.ButtonMentes_Click);
            // 
            // panelFeltModTorl
            // 
            this.panelFeltModTorl.Controls.Add(this.panelModTorol);
            this.panelFeltModTorl.Controls.Add(this.buttonFeltoltes);
            this.panelFeltModTorl.Location = new System.Drawing.Point(620, 21);
            this.panelFeltModTorl.Margin = new System.Windows.Forms.Padding(10);
            this.panelFeltModTorl.Name = "panelFeltModTorl";
            this.panelFeltModTorl.Size = new System.Drawing.Size(181, 189);
            this.panelFeltModTorl.TabIndex = 32;
            this.panelFeltModTorl.Visible = false;
            // 
            // panelModTorol
            // 
            this.panelModTorol.Controls.Add(this.buttonModositas);
            this.panelModTorol.Controls.Add(this.buttonTorles);
            this.panelModTorol.Location = new System.Drawing.Point(12, 62);
            this.panelModTorol.Name = "panelModTorol";
            this.panelModTorol.Size = new System.Drawing.Size(165, 118);
            this.panelModTorol.TabIndex = 22;
            // 
            // groupBoxAlso
            // 
            this.groupBoxAlso.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBoxAlso.Controls.Add(this.label4);
            this.groupBoxAlso.Controls.Add(this.labelKapcsolatAdatbazissal);
            this.groupBoxAlso.Location = new System.Drawing.Point(0, 822);
            this.groupBoxAlso.Name = "groupBoxAlso";
            this.groupBoxAlso.Size = new System.Drawing.Size(1586, 40);
            this.groupBoxAlso.TabIndex = 33;
            this.groupBoxAlso.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.groupBoxAlso);
            this.Controls.Add(this.panelFeltModTorl);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelKeres);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBoxEleresi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nyílvántartó";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
            this.groupBoxEleresi.ResumeLayout(false);
            this.groupBoxEleresi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVizsgaÉveKeres)).EndInit();
            this.panelKeres.ResumeLayout(false);
            this.panelKeres.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTalalatokSzama)).EndInit();
            this.panelMenu.ResumeLayout(false);
            this.panelFeltolt.ResumeLayout(false);
            this.panelFeltolt.PerformLayout();
            this.panelKozepiskolaAnyakonyvFeltolt.ResumeLayout(false);
            this.panelKozepiskolaAnyakonyvFeltolt.PerformLayout();
            this.panelKozepiskolaAnyakonyvFilneName.ResumeLayout(false);
            this.panelKozepiskolaAnyakonyvFilneName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv)).EndInit();
            this.panelErettsegiTorzslapFeltolt.ResumeLayout(false);
            this.panelErettsegiTorzslapFeltolt.PerformLayout();
            this.panelErettsegiTtorzslapFileName.ResumeLayout(false);
            this.panelErettsegiTtorzslapFileName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap)).EndInit();
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.ResumeLayout(false);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.PerformLayout();
            this.panelSzakmaivizsgaAnyakonyvFileName.ResumeLayout(false);
            this.panelSzakmaivizsgaAnyakonyvFileName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv)).EndInit();
            this.panelTallozMentesujButton.ResumeLayout(false);
            this.panelErettsegiTanusitvanyFeltolt.ResumeLayout(false);
            this.panelErettsegiTanusitvanyFeltolt.PerformLayout();
            this.panelErettsegiTanusitvanyFileName.ResumeLayout(false);
            this.panelErettsegiTanusitvanyFileName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany)).EndInit();
            this.panelSzakmaiVizsgaTorzslapFeltolt.ResumeLayout(false);
            this.panelSzakmaiVizsgaTorzslapFeltolt.PerformLayout();
            this.panelSzakmaiVizsgaTorzslapFileName.ResumeLayout(false);
            this.panelSzakmaiVizsgaTorzslapFileName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap)).EndInit();
            this.panelFeltModTorl.ResumeLayout(false);
            this.panelModTorol.ResumeLayout(false);
            this.groupBoxAlso.ResumeLayout(false);
            this.groupBoxAlso.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Label label1;
        private Label labelMentesiHely;
        private Label label2;
        private Button buttonTallozas;
        private GroupBox groupBoxEleresi;
        private Label label3;
        private Label labelPath;
        private Timer updateDbStateTimer;
        private Label label4;
        private Label labelKapcsolatAdatbazissal;
        private DataGridView dataGridView1;
        private Button buttonErettsegiTorzslap;
        private Button buttonErettsegiTanusitvany;
        private Button buttonSzakmaiVizsgaTorzslap;
        private Button buttonSzakmaiVizsgaAnyakonyv;
        private Button buttonKozepiskolaAnyakonyv;
        private TextBox textBoxTanuloNeveKeres;
        private TextBox textBoxanyjaNeveKeres;
        private Label label5;
        private Label label6;
        private Label label7;
        private NumericUpDown numericUpDownVizsgaÉveKeres;
        private CheckBox checkBoxVizsgaEve;
        private Panel panelKeres;
        private Label label8;
        private NumericUpDown numericUpDownTalalatokSzama;
        private Button buttonFeltoltes;
        private Button buttonModositas;
        private Button buttonTorles;
        private Panel panelMenu;
        private Label label11;
        private Label label10;
        private Label label9;
        private TextBox textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap;
        private TextBox textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap;
        private Button buttonMentes;
        private Button buttonMegse;
        private Button buttonMentesUj;
        private Label labelMenuKat;
        private Panel panelFeltModTorl;
        private Button buttonTalloz;
        private TextBox textBoxFileNameFeltoltSzakmaiVizsgaTorzslap;
        private Panel panelFeltolt;
        private Panel panelSzakmaiVizsgaTorzslapFeltolt;
        private Label label13;
        private RadioButton radioButtonOszFeltoltSzakmaiVizsgaTorzslap;
        private RadioButton radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap;
        private Label label12;
        private NumericUpDown numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap;
        private Panel panelErettsegiTanusitvanyFeltolt;
        private NumericUpDown numericUpDownVizsgaEveFeltoltErettsegiTanusitvany;
        private TextBox textBoxFileNameFeltoltErettsegiTanusitvany;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private TextBox textBoxAnyjaNeveFeltoltErettsegiTanusitvany;
        private TextBox textBoxTanuloNeveFeltoltErettsegiTanusitvany;
        private Panel panelErettsegiTorzslapFeltolt;
        private NumericUpDown numericUpDownVizsgaEveFeltoltErettsegiTorzslap;
        private Label label14;
        private RadioButton radioButtonOszFeltoltErettsegiTorzslap;
        private RadioButton radioButtonTavaszFeltoltErettsegiTorzslap;
        private TextBox textBoxFileNameFeltoltErettsegiTorzslap;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private TextBox textBoxAnyjaNeveFeltoltErettsegiTorzslap;
        private TextBox textBoxTanuloNeveFeltoltErettsegiTorzslap;
        private Panel panelKozepiskolaAnyakonyvFeltolt;
        private NumericUpDown numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv;
        private TextBox textBoxFileNameFeltoltKozepsikolaAnyakonyv;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private TextBox textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv;
        private TextBox textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv;
        private NumericUpDown numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv;
        private Label label29;
        private Panel panelSzakmaiVizsgaAnyakonyvFeltolt;
        private NumericUpDown numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv;
        private Label label24;
        private NumericUpDown numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv;
        private TextBox textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv;
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private TextBox textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv;
        private TextBox textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt;
        private TextBox textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany;
        private Label label19;
        private Panel panelModTorol;
        private Panel panelSzakmaiVizsgaTorzslapFileName;
        private Panel panelTallozMentesujButton;
        private Panel panelKozepiskolaAnyakonyvFilneName;
        private Panel panelErettsegiTtorzslapFileName;
        private Panel panelSzakmaivizsgaAnyakonyvFileName;
        private Panel panelErettsegiTanusitvanyFileName;
        private GroupBox groupBoxAlso;
    }
}

