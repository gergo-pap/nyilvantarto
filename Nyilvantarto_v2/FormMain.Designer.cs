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
            this.components = new Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            this.label1 = new Label();
            this.labelMentesiHely = new Label();
            this.label2 = new Label();
            this.buttonTallozas = new Button();
            this.groupBoxEleresi = new GroupBox();
            this.label3 = new Label();
            this.labelPath = new Label();
            this.updateDbStateTimer = new Timer(this.components);
            this.label4 = new Label();
            this.labelKapcsolatAdatbazissal = new Label();
            this.dataGridView1 = new DataGridView();
            this.buttonErettsegiTorzslap = new Button();
            this.buttonErettsegiTanusitvany = new Button();
            this.buttonSzakmaiVizsgaTorzslap = new Button();
            this.buttonSzakmaiVizsgaAnyakonyv = new Button();
            this.buttonKozepiskolaAnyakonyv = new Button();
            this.textBoxTanuloNeveKeres = new TextBox();
            this.textBoxanyjaNeveKeres = new TextBox();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.numericUpDownVizsgaÉveKeres = new NumericUpDown();
            this.checkBoxVizsgaEve = new CheckBox();
            this.panelKeres = new Panel();
            this.numericUpDownTalalatokSzama = new NumericUpDown();
            this.label8 = new Label();
            this.buttonFeltoltes = new Button();
            this.buttonModositas = new Button();
            this.buttonTorles = new Button();
            this.panelMenu = new Panel();
            this.panelFeltolt = new Panel();
            this.panelKozepiskolaAnyakonyvFeltolt = new Panel();
            this.panelKozepiskolaAnyakonyvFilneName = new Panel();
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv = new TextBox();
            this.label30 = new Label();
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv = new NumericUpDown();
            this.label29 = new Label();
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv = new NumericUpDown();
            this.label31 = new Label();
            this.label32 = new Label();
            this.label33 = new Label();
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv = new TextBox();
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv = new TextBox();
            this.panelErettsegiTorzslapFeltolt = new Panel();
            this.panelErettsegiTtorzslapFileName = new Panel();
            this.textBoxFileNameFeltoltErettsegiTorzslap = new TextBox();
            this.label15 = new Label();
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap = new NumericUpDown();
            this.label14 = new Label();
            this.radioButtonOszFeltoltErettsegiTorzslap = new RadioButton();
            this.radioButtonTavaszFeltoltErettsegiTorzslap = new RadioButton();
            this.label16 = new Label();
            this.label17 = new Label();
            this.label18 = new Label();
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap = new TextBox();
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap = new TextBox();
            this.panelSzakmaiVizsgaAnyakonyvFeltolt = new Panel();
            this.panelSzakmaivizsgaAnyakonyvFileName = new Panel();
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv = new TextBox();
            this.label25 = new Label();
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv = new NumericUpDown();
            this.label24 = new Label();
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv = new NumericUpDown();
            this.label26 = new Label();
            this.label27 = new Label();
            this.label28 = new Label();
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv = new TextBox();
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt = new TextBox();
            this.panelTallozMentesujButton = new Panel();
            this.buttonTalloz = new Button();
            this.buttonMentesUj = new Button();
            this.panelErettsegiTanusitvanyFeltolt = new Panel();
            this.panelErettsegiTanusitvanyFileName = new Panel();
            this.textBoxFileNameFeltoltErettsegiTanusitvany = new TextBox();
            this.label20 = new Label();
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany = new TextBox();
            this.label19 = new Label();
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany = new NumericUpDown();
            this.label21 = new Label();
            this.label22 = new Label();
            this.label23 = new Label();
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany = new TextBox();
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany = new TextBox();
            this.panelSzakmaiVizsgaTorzslapFeltolt = new Panel();
            this.panelSzakmaiVizsgaTorzslapFileName = new Panel();
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap = new TextBox();
            this.label12 = new Label();
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap = new NumericUpDown();
            this.label13 = new Label();
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap = new RadioButton();
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap = new RadioButton();
            this.label11 = new Label();
            this.label9 = new Label();
            this.label10 = new Label();
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap = new TextBox();
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap = new TextBox();
            this.buttonMegse = new Button();
            this.labelMenuKat = new Label();
            this.buttonMentes = new Button();
            this.panelFeltModTorl = new Panel();
            this.panelModTorol = new Panel();
            this.groupBoxAlso = new GroupBox();
            this.groupBoxEleresi.SuspendLayout();
            ((ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((ISupportInitialize)(this.numericUpDownVizsgaÉveKeres)).BeginInit();
            this.panelKeres.SuspendLayout();
            ((ISupportInitialize)(this.numericUpDownTalalatokSzama)).BeginInit();
            this.panelMenu.SuspendLayout();
            this.panelFeltolt.SuspendLayout();
            this.panelKozepiskolaAnyakonyvFeltolt.SuspendLayout();
            this.panelKozepiskolaAnyakonyvFilneName.SuspendLayout();
            ((ISupportInitialize)(this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv)).BeginInit();
            ((ISupportInitialize)(this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv)).BeginInit();
            this.panelErettsegiTorzslapFeltolt.SuspendLayout();
            this.panelErettsegiTtorzslapFileName.SuspendLayout();
            ((ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap)).BeginInit();
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.SuspendLayout();
            this.panelSzakmaivizsgaAnyakonyvFileName.SuspendLayout();
            ((ISupportInitialize)(this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv)).BeginInit();
            ((ISupportInitialize)(this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv)).BeginInit();
            this.panelTallozMentesujButton.SuspendLayout();
            this.panelErettsegiTanusitvanyFeltolt.SuspendLayout();
            this.panelErettsegiTanusitvanyFileName.SuspendLayout();
            ((ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany)).BeginInit();
            this.panelSzakmaiVizsgaTorzslapFeltolt.SuspendLayout();
            this.panelSzakmaiVizsgaTorzslapFileName.SuspendLayout();
            ((ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap)).BeginInit();
            this.panelFeltModTorl.SuspendLayout();
            this.panelModTorol.SuspendLayout();
            this.groupBoxAlso.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mentési hely:";
            // 
            // labelMentesiHely
            // 
            this.labelMentesiHely.AutoSize = true;
            this.labelMentesiHely.Location = new Point(81, 46);
            this.labelMentesiHely.Name = "labelMentesiHely";
            this.labelMentesiHely.Size = new Size(0, 13);
            this.labelMentesiHely.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Módosítás:";
            // 
            // buttonTallozas
            // 
            this.buttonTallozas.Location = new Point(84, 69);
            this.buttonTallozas.Name = "buttonTallozas";
            this.buttonTallozas.Size = new Size(75, 23);
            this.buttonTallozas.TabIndex = 6;
            this.buttonTallozas.Text = "Tallózás";
            this.buttonTallozas.UseVisualStyleBackColor = true;
            this.buttonTallozas.Click += new EventHandler(this.ButtonTallozas_Click);
            // 
            // groupBoxEleresi
            // 
            this.groupBoxEleresi.Controls.Add(this.label3);
            this.groupBoxEleresi.Controls.Add(this.label1);
            this.groupBoxEleresi.Controls.Add(this.labelPath);
            this.groupBoxEleresi.Controls.Add(this.buttonTallozas);
            this.groupBoxEleresi.Controls.Add(this.labelMentesiHely);
            this.groupBoxEleresi.Controls.Add(this.label2);
            this.groupBoxEleresi.Location = new Point(828, 13);
            this.groupBoxEleresi.Name = "groupBoxEleresi";
            this.groupBoxEleresi.Size = new Size(297, 127);
            this.groupBoxEleresi.TabIndex = 7;
            this.groupBoxEleresi.TabStop = false;
            this.groupBoxEleresi.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new Point(30, 16);
            this.label3.Name = "label3";
            this.label3.Size = new Size(237, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Első indítás, válaszd ki hova mentsem a program";
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new Point(9, 99);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new Size(34, 13);
            this.labelPath.TabIndex = 9;
            this.labelPath.Text = "path?";
            // 
            // updateDbStateTimer
            // 
            this.updateDbStateTimer.Enabled = true;
            this.updateDbStateTimer.Interval = 2000;
            this.updateDbStateTimer.Tick += new EventHandler(this.updateDbStateTimer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new Point(1410, 17);
            this.label4.Name = "label4";
            this.label4.Size = new Size(132, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Kapcsolat az adatbázissal:";
            // 
            // labelKapcsolatAdatbazissal
            // 
            this.labelKapcsolatAdatbazissal.AutoSize = true;
            this.labelKapcsolatAdatbazissal.Location = new Point(1548, 17);
            this.labelKapcsolatAdatbazissal.Name = "labelKapcsolatAdatbazissal";
            this.labelKapcsolatAdatbazissal.Size = new Size(32, 13);
            this.labelKapcsolatAdatbazissal.TabIndex = 11;
            this.labelKapcsolatAdatbazissal.Text = "aktív";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left) 
                                                         | AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new Point(620, 315);
            this.dataGridView1.Margin = new Padding(10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new Size(950, 507);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // buttonErettsegiTorzslap
            // 
            this.buttonErettsegiTorzslap.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
                                                                    | AnchorStyles.Left) 
                                                                   | AnchorStyles.Right)));
            this.buttonErettsegiTorzslap.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonErettsegiTorzslap.Location = new Point(6, 5);
            this.buttonErettsegiTorzslap.Name = "buttonErettsegiTorzslap";
            this.buttonErettsegiTorzslap.Size = new Size(160, 30);
            this.buttonErettsegiTorzslap.TabIndex = 13;
            this.buttonErettsegiTorzslap.Text = "Érettségi - törzslap";
            this.buttonErettsegiTorzslap.UseVisualStyleBackColor = true;
            this.buttonErettsegiTorzslap.Click += new EventHandler(this.buttonErettsegiTorzslap_Click);
            // 
            // buttonErettsegiTanusitvany
            // 
            this.buttonErettsegiTanusitvany.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
                                                                       | AnchorStyles.Left) 
                                                                      | AnchorStyles.Right)));
            this.buttonErettsegiTanusitvany.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonErettsegiTanusitvany.Location = new Point(195, 5);
            this.buttonErettsegiTanusitvany.Name = "buttonErettsegiTanusitvany";
            this.buttonErettsegiTanusitvany.Size = new Size(160, 30);
            this.buttonErettsegiTanusitvany.TabIndex = 14;
            this.buttonErettsegiTanusitvany.Text = "Érettségi - tanusítvány";
            this.buttonErettsegiTanusitvany.UseVisualStyleBackColor = true;
            this.buttonErettsegiTanusitvany.Click += new EventHandler(this.buttonErettsegiTanusitvany_Click);
            // 
            // buttonSzakmaiVizsgaTorzslap
            // 
            this.buttonSzakmaiVizsgaTorzslap.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
                                                                        | AnchorStyles.Left) 
                                                                       | AnchorStyles.Right)));
            this.buttonSzakmaiVizsgaTorzslap.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonSzakmaiVizsgaTorzslap.Location = new Point(410, 7);
            this.buttonSzakmaiVizsgaTorzslap.Name = "buttonSzakmaiVizsgaTorzslap";
            this.buttonSzakmaiVizsgaTorzslap.Size = new Size(160, 30);
            this.buttonSzakmaiVizsgaTorzslap.TabIndex = 15;
            this.buttonSzakmaiVizsgaTorzslap.Text = "Szakmai vizsga - törzslap";
            this.buttonSzakmaiVizsgaTorzslap.UseVisualStyleBackColor = true;
            this.buttonSzakmaiVizsgaTorzslap.Click += new EventHandler(this.buttonSzakmaiVizsgaTorzslap_Click);
            // 
            // buttonSzakmaiVizsgaAnyakonyv
            // 
            this.buttonSzakmaiVizsgaAnyakonyv.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
                                                                         | AnchorStyles.Left) 
                                                                        | AnchorStyles.Right)));
            this.buttonSzakmaiVizsgaAnyakonyv.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonSzakmaiVizsgaAnyakonyv.Location = new Point(601, 7);
            this.buttonSzakmaiVizsgaAnyakonyv.Name = "buttonSzakmaiVizsgaAnyakonyv";
            this.buttonSzakmaiVizsgaAnyakonyv.Size = new Size(167, 30);
            this.buttonSzakmaiVizsgaAnyakonyv.TabIndex = 16;
            this.buttonSzakmaiVizsgaAnyakonyv.Text = "Szakmai vizsga - anyakönyv";
            this.buttonSzakmaiVizsgaAnyakonyv.UseVisualStyleBackColor = true;
            this.buttonSzakmaiVizsgaAnyakonyv.Click += new EventHandler(this.buttonSzakmaiVizsgaAnyakonyv_Click);
            // 
            // buttonKozepiskolaAnyakonyv
            // 
            this.buttonKozepiskolaAnyakonyv.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
                                                                       | AnchorStyles.Left) 
                                                                      | AnchorStyles.Right)));
            this.buttonKozepiskolaAnyakonyv.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonKozepiskolaAnyakonyv.Location = new Point(782, 5);
            this.buttonKozepiskolaAnyakonyv.Name = "buttonKozepiskolaAnyakonyv";
            this.buttonKozepiskolaAnyakonyv.Size = new Size(160, 30);
            this.buttonKozepiskolaAnyakonyv.TabIndex = 17;
            this.buttonKozepiskolaAnyakonyv.Text = "Középiskola - anyakönyv";
            this.buttonKozepiskolaAnyakonyv.UseVisualStyleBackColor = true;
            this.buttonKozepiskolaAnyakonyv.Click += new EventHandler(this.buttonKozepiskolaAnyakonyv_Click);
            // 
            // textBoxTanuloNeveKeres
            // 
            this.textBoxTanuloNeveKeres.Location = new Point(68, 61);
            this.textBoxTanuloNeveKeres.Name = "textBoxTanuloNeveKeres";
            this.textBoxTanuloNeveKeres.Size = new Size(150, 20);
            this.textBoxTanuloNeveKeres.TabIndex = 18;
            this.textBoxTanuloNeveKeres.TextChanged += new EventHandler(this.TextBoxTanuloNeve_TextChanged);
            // 
            // textBoxanyjaNeveKeres
            // 
            this.textBoxanyjaNeveKeres.Location = new Point(292, 61);
            this.textBoxanyjaNeveKeres.Name = "textBoxanyjaNeveKeres";
            this.textBoxanyjaNeveKeres.Size = new Size(150, 20);
            this.textBoxanyjaNeveKeres.TabIndex = 19;
            this.textBoxanyjaNeveKeres.TextChanged += new EventHandler(this.textBoxAnyjaNeve_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new Point(101, 42);
            this.label5.Name = "label5";
            this.label5.Size = new Size(67, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tanuló neve";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new Point(329, 41);
            this.label6.Name = "label6";
            this.label6.Size = new Size(60, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Anyja neve";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new Point(554, 45);
            this.label7.Name = "label7";
            this.label7.Size = new Size(105, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Vizsga/Érettségi éve";
            // 
            // numericUpDownVizsgaÉveKeres
            // 
            this.numericUpDownVizsgaÉveKeres.Location = new Point(533, 61);
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
            this.numericUpDownVizsgaÉveKeres.Size = new Size(120, 20);
            this.numericUpDownVizsgaÉveKeres.TabIndex = 25;
            this.numericUpDownVizsgaÉveKeres.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownVizsgaÉveKeres.ValueChanged += new EventHandler(this.numericUpDownVizsgaÉve_ValueChanged);
            // 
            // checkBoxVizsgaEve
            // 
            this.checkBoxVizsgaEve.AutoSize = true;
            this.checkBoxVizsgaEve.Location = new Point(533, 43);
            this.checkBoxVizsgaEve.Name = "checkBoxVizsgaEve";
            this.checkBoxVizsgaEve.Size = new Size(15, 14);
            this.checkBoxVizsgaEve.TabIndex = 26;
            this.checkBoxVizsgaEve.UseVisualStyleBackColor = true;
            this.checkBoxVizsgaEve.CheckedChanged += new EventHandler(this.checkBoxVizsgaEve_CheckedChanged);
            // 
            // panelKeres
            // 
            this.panelKeres.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
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
            this.panelKeres.Location = new Point(620, 218);
            this.panelKeres.Name = "panelKeres";
            this.panelKeres.Size = new Size(945, 84);
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
            this.numericUpDownTalalatokSzama.Location = new Point(836, 53);
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
            this.numericUpDownTalalatokSzama.Size = new Size(56, 20);
            this.numericUpDownTalalatokSzama.TabIndex = 29;
            this.numericUpDownTalalatokSzama.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownTalalatokSzama.ValueChanged += new EventHandler(this.numericUpDownTalalatokSzama_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new Point(740, 53);
            this.label8.Name = "label8";
            this.label8.Size = new Size(87, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Találatok száma:";
            // 
            // buttonFeltoltes
            // 
            this.buttonFeltoltes.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonFeltoltes.Location = new Point(17, 6);
            this.buttonFeltoltes.Margin = new Padding(10);
            this.buttonFeltoltes.Name = "buttonFeltoltes";
            this.buttonFeltoltes.Size = new Size(149, 49);
            this.buttonFeltoltes.TabIndex = 28;
            this.buttonFeltoltes.Text = "Feltöltés";
            this.buttonFeltoltes.UseVisualStyleBackColor = true;
            this.buttonFeltoltes.Click += new EventHandler(this.ButtonFeltoltes_Click);
            // 
            // buttonModositas
            // 
            this.buttonModositas.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonModositas.Location = new Point(4, 2);
            this.buttonModositas.Margin = new Padding(10);
            this.buttonModositas.Name = "buttonModositas";
            this.buttonModositas.Size = new Size(149, 49);
            this.buttonModositas.TabIndex = 29;
            this.buttonModositas.Text = "Módosítás";
            this.buttonModositas.UseVisualStyleBackColor = true;
            this.buttonModositas.Click += new EventHandler(this.ButtonModositas_Click);
            // 
            // buttonTorles
            // 
            this.buttonTorles.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonTorles.Location = new Point(4, 59);
            this.buttonTorles.Margin = new Padding(10);
            this.buttonTorles.Name = "buttonTorles";
            this.buttonTorles.Size = new Size(149, 49);
            this.buttonTorles.TabIndex = 30;
            this.buttonTorles.Text = "Törlés";
            this.buttonTorles.UseVisualStyleBackColor = true;
            this.buttonTorles.Click += new EventHandler(this.ButtonTorles_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = AnchorStyles.None;
            this.panelMenu.BackColor = SystemColors.ActiveCaption;
            this.panelMenu.BorderStyle = BorderStyle.Fixed3D;
            this.panelMenu.Controls.Add(this.panelFeltolt);
            this.panelMenu.Location = new Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new Size(600, 822);
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
            this.panelFeltolt.Location = new Point(12, 16);
            this.panelFeltolt.Name = "panelFeltolt";
            this.panelFeltolt.Size = new Size(576, 772);
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
            this.panelKozepiskolaAnyakonyvFeltolt.Location = new Point(53, 389);
            this.panelKozepiskolaAnyakonyvFeltolt.Name = "panelKozepiskolaAnyakonyvFeltolt";
            this.panelKozepiskolaAnyakonyvFeltolt.Size = new Size(550, 170);
            this.panelKozepiskolaAnyakonyvFeltolt.TabIndex = 20;
            this.panelKozepiskolaAnyakonyvFeltolt.Visible = false;
            // 
            // panelKozepiskolaAnyakonyvFilneName
            // 
            this.panelKozepiskolaAnyakonyvFilneName.Controls.Add(this.textBoxFileNameFeltoltKozepsikolaAnyakonyv);
            this.panelKozepiskolaAnyakonyvFilneName.Controls.Add(this.label30);
            this.panelKozepiskolaAnyakonyvFilneName.Location = new Point(14, 135);
            this.panelKozepiskolaAnyakonyvFilneName.Name = "panelKozepiskolaAnyakonyvFilneName";
            this.panelKozepiskolaAnyakonyvFilneName.Size = new Size(522, 32);
            this.panelKozepiskolaAnyakonyvFilneName.TabIndex = 18;
            // 
            // textBoxFileNameFeltoltKozepsikolaAnyakonyv
            // 
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.Location = new Point(214, 5);
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.Name = "textBoxFileNameFeltoltKozepsikolaAnyakonyv";
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.ReadOnly = true;
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.Size = new Size(300, 24);
            this.textBoxFileNameFeltoltKozepsikolaAnyakonyv.TabIndex = 11;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label30.Location = new Point(6, 8);
            this.label30.Name = "label30";
            this.label30.Size = new Size(35, 18);
            this.label30.TabIndex = 12;
            this.label30.Text = "Fájl:";
            // 
            // numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv
            // 
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Location = new Point(222, 105);
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
            this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv.Size = new Size(100, 24);
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
            this.label29.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label29.Location = new Point(11, 108);
            this.label29.Name = "label29";
            this.label29.Size = new Size(97, 18);
            this.label29.TabIndex = 17;
            this.label29.Text = "Érettségi éve:";
            // 
            // numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv
            // 
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Location = new Point(222, 75);
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
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Size = new Size(100, 24);
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.TabIndex = 16;
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv.ValueChanged += new EventHandler(this.numericUpDownKozepiskKezdeteKozepiskolaAnyakonyv_ValueChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label31.Location = new Point(11, 78);
            this.label31.Name = "label31";
            this.label31.Size = new Size(150, 18);
            this.label31.TabIndex = 5;
            this.label31.Text = "Középiskola kezdete:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label32.Location = new Point(11, 18);
            this.label32.Name = "label32";
            this.label32.Size = new Size(88, 18);
            this.label32.TabIndex = 3;
            this.label32.Text = "Tanuló neve";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label33.Location = new Point(11, 48);
            this.label33.Name = "label33";
            this.label33.Size = new Size(78, 18);
            this.label33.TabIndex = 4;
            this.label33.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv
            // 
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Location = new Point(222, 45);
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Name = "textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv";
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.Size = new Size(300, 24);
            this.textBoxAnyjaNeveFeltoltKozepiskolaAnyakonyv.TabIndex = 1;
            // 
            // textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv
            // 
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Location = new Point(222, 15);
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Name = "textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv";
            this.textBoxTanuloNeveFeltoltKozepiskolaAnyakonyv.Size = new Size(300, 24);
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
            this.panelErettsegiTorzslapFeltolt.Location = new Point(30, 392);
            this.panelErettsegiTorzslapFeltolt.Name = "panelErettsegiTorzslapFeltolt";
            this.panelErettsegiTorzslapFeltolt.Size = new Size(550, 170);
            this.panelErettsegiTorzslapFeltolt.TabIndex = 17;
            this.panelErettsegiTorzslapFeltolt.Visible = false;
            // 
            // panelErettsegiTtorzslapFileName
            // 
            this.panelErettsegiTtorzslapFileName.Controls.Add(this.textBoxFileNameFeltoltErettsegiTorzslap);
            this.panelErettsegiTtorzslapFileName.Controls.Add(this.label15);
            this.panelErettsegiTtorzslapFileName.Location = new Point(11, 130);
            this.panelErettsegiTtorzslapFileName.Name = "panelErettsegiTtorzslapFileName";
            this.panelErettsegiTtorzslapFileName.Size = new Size(522, 32);
            this.panelErettsegiTtorzslapFileName.TabIndex = 20;
            // 
            // textBoxFileNameFeltoltErettsegiTorzslap
            // 
            this.textBoxFileNameFeltoltErettsegiTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltErettsegiTorzslap.Location = new Point(219, 5);
            this.textBoxFileNameFeltoltErettsegiTorzslap.Name = "textBoxFileNameFeltoltErettsegiTorzslap";
            this.textBoxFileNameFeltoltErettsegiTorzslap.ReadOnly = true;
            this.textBoxFileNameFeltoltErettsegiTorzslap.Size = new Size(300, 24);
            this.textBoxFileNameFeltoltErettsegiTorzslap.TabIndex = 11;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label15.Location = new Point(8, 8);
            this.label15.Name = "label15";
            this.label15.Size = new Size(35, 18);
            this.label15.TabIndex = 12;
            this.label15.Text = "Fájl:";
            // 
            // numericUpDownVizsgaEveFeltoltErettsegiTorzslap
            // 
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Location = new Point(222, 72);
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
            this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap.Size = new Size(75, 24);
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
            this.label14.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label14.Location = new Point(11, 104);
            this.label14.Name = "label14";
            this.label14.Size = new Size(64, 18);
            this.label14.TabIndex = 15;
            this.label14.Text = "Időszak:";
            // 
            // radioButtonOszFeltoltErettsegiTorzslap
            // 
            this.radioButtonOszFeltoltErettsegiTorzslap.AutoSize = true;
            this.radioButtonOszFeltoltErettsegiTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonOszFeltoltErettsegiTorzslap.Location = new Point(467, 102);
            this.radioButtonOszFeltoltErettsegiTorzslap.Name = "radioButtonOszFeltoltErettsegiTorzslap";
            this.radioButtonOszFeltoltErettsegiTorzslap.Size = new Size(54, 22);
            this.radioButtonOszFeltoltErettsegiTorzslap.TabIndex = 14;
            this.radioButtonOszFeltoltErettsegiTorzslap.Text = "Ősz";
            this.radioButtonOszFeltoltErettsegiTorzslap.UseVisualStyleBackColor = true;
            // 
            // radioButtonTavaszFeltoltErettsegiTorzslap
            // 
            this.radioButtonTavaszFeltoltErettsegiTorzslap.AutoSize = true;
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Checked = true;
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Location = new Point(222, 102);
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Name = "radioButtonTavaszFeltoltErettsegiTorzslap";
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Size = new Size(74, 22);
            this.radioButtonTavaszFeltoltErettsegiTorzslap.TabIndex = 13;
            this.radioButtonTavaszFeltoltErettsegiTorzslap.TabStop = true;
            this.radioButtonTavaszFeltoltErettsegiTorzslap.Text = "Tavasz";
            this.radioButtonTavaszFeltoltErettsegiTorzslap.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label16.Location = new Point(11, 74);
            this.label16.Name = "label16";
            this.label16.Size = new Size(79, 18);
            this.label16.TabIndex = 5;
            this.label16.Text = "Vizsga éve";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label17.Location = new Point(11, 17);
            this.label17.Name = "label17";
            this.label17.Size = new Size(88, 18);
            this.label17.TabIndex = 3;
            this.label17.Text = "Tanuló neve";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label18.Location = new Point(11, 45);
            this.label18.Name = "label18";
            this.label18.Size = new Size(78, 18);
            this.label18.TabIndex = 4;
            this.label18.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltErettsegiTorzslap
            // 
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.Location = new Point(222, 42);
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.Name = "textBoxAnyjaNeveFeltoltErettsegiTorzslap";
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.Size = new Size(300, 24);
            this.textBoxAnyjaNeveFeltoltErettsegiTorzslap.TabIndex = 1;
            // 
            // textBoxTanuloNeveFeltoltErettsegiTorzslap
            // 
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.Location = new Point(222, 14);
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.Name = "textBoxTanuloNeveFeltoltErettsegiTorzslap";
            this.textBoxTanuloNeveFeltoltErettsegiTorzslap.Size = new Size(300, 24);
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
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Location = new Point(10, 396);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Name = "panelSzakmaiVizsgaAnyakonyvFeltolt";
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Size = new Size(550, 166);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.TabIndex = 21;
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.Visible = false;
            // 
            // panelSzakmaivizsgaAnyakonyvFileName
            // 
            this.panelSzakmaivizsgaAnyakonyvFileName.Controls.Add(this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv);
            this.panelSzakmaivizsgaAnyakonyvFileName.Controls.Add(this.label25);
            this.panelSzakmaivizsgaAnyakonyvFileName.Location = new Point(11, 134);
            this.panelSzakmaivizsgaAnyakonyvFileName.Name = "panelSzakmaivizsgaAnyakonyvFileName";
            this.panelSzakmaivizsgaAnyakonyvFileName.Size = new Size(522, 32);
            this.panelSzakmaivizsgaAnyakonyvFileName.TabIndex = 20;
            // 
            // textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv
            // 
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Location = new Point(211, 9);
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Multiline = true;
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Name = "textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv";
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.ReadOnly = true;
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.Size = new Size(300, 20);
            this.textBoxFileNameFeltoltSzakmaiVizsgaAnyakonyv.TabIndex = 11;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label25.Location = new Point(3, 9);
            this.label25.Name = "label25";
            this.label25.Size = new Size(35, 18);
            this.label25.TabIndex = 12;
            this.label25.Text = "Fájl:";
            // 
            // numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv
            // 
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Location = new Point(222, 104);
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
            this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv.Size = new Size(100, 24);
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
            this.label24.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label24.Location = new Point(14, 106);
            this.label24.Name = "label24";
            this.label24.Size = new Size(97, 18);
            this.label24.TabIndex = 17;
            this.label24.Text = "Érettségi éve:";
            // 
            // numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv
            // 
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Location = new Point(222, 72);
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
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Size = new Size(100, 24);
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.TabIndex = 16;
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv.ValueChanged += new EventHandler(this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv_ValueChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label26.Location = new Point(14, 76);
            this.label26.Name = "label26";
            this.label26.Size = new Size(150, 18);
            this.label26.TabIndex = 5;
            this.label26.Text = "Középiskola kezdete:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label27.Location = new Point(14, 17);
            this.label27.Name = "label27";
            this.label27.Size = new Size(88, 18);
            this.label27.TabIndex = 3;
            this.label27.Text = "Tanuló neve";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label28.Location = new Point(14, 47);
            this.label28.Name = "label28";
            this.label28.Size = new Size(78, 18);
            this.label28.TabIndex = 4;
            this.label28.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv
            // 
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Location = new Point(222, 44);
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Name = "textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv";
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.Size = new Size(300, 24);
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaAnyakonyv.TabIndex = 1;
            // 
            // textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt
            // 
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Location = new Point(222, 14);
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Name = "textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt";
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.Size = new Size(300, 24);
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaAnyakonyvFeltolt.TabIndex = 0;
            // 
            // panelTallozMentesujButton
            // 
            this.panelTallozMentesujButton.Controls.Add(this.buttonTalloz);
            this.panelTallozMentesujButton.Controls.Add(this.buttonMentesUj);
            this.panelTallozMentesujButton.Location = new Point(196, 562);
            this.panelTallozMentesujButton.Name = "panelTallozMentesujButton";
            this.panelTallozMentesujButton.Size = new Size(154, 143);
            this.panelTallozMentesujButton.TabIndex = 22;
            // 
            // buttonTalloz
            // 
            this.buttonTalloz.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonTalloz.Location = new Point(0, 10);
            this.buttonTalloz.Margin = new Padding(10);
            this.buttonTalloz.Name = "buttonTalloz";
            this.buttonTalloz.Size = new Size(153, 41);
            this.buttonTalloz.TabIndex = 10;
            this.buttonTalloz.Text = "Tallózás";
            this.buttonTalloz.UseVisualStyleBackColor = true;
            this.buttonTalloz.Click += new EventHandler(this.ButtonTalloz_Click);
            // 
            // buttonMentesUj
            // 
            this.buttonMentesUj.Cursor = Cursors.Default;
            this.buttonMentesUj.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonMentesUj.Location = new Point(0, 102);
            this.buttonMentesUj.Margin = new Padding(10);
            this.buttonMentesUj.Name = "buttonMentesUj";
            this.buttonMentesUj.Size = new Size(153, 41);
            this.buttonMentesUj.TabIndex = 7;
            this.buttonMentesUj.Text = "Mentés új";
            this.buttonMentesUj.UseVisualStyleBackColor = true;
            this.buttonMentesUj.Click += new EventHandler(this.buttonMentesUj_Click);
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
            this.panelErettsegiTanusitvanyFeltolt.Location = new Point(26, 185);
            this.panelErettsegiTanusitvanyFeltolt.Name = "panelErettsegiTanusitvanyFeltolt";
            this.panelErettsegiTanusitvanyFeltolt.Size = new Size(550, 170);
            this.panelErettsegiTanusitvanyFeltolt.TabIndex = 18;
            this.panelErettsegiTanusitvanyFeltolt.Visible = false;
            // 
            // panelErettsegiTanusitvanyFileName
            // 
            this.panelErettsegiTanusitvanyFileName.Controls.Add(this.textBoxFileNameFeltoltErettsegiTanusitvany);
            this.panelErettsegiTanusitvanyFileName.Controls.Add(this.label20);
            this.panelErettsegiTanusitvanyFileName.Location = new Point(14, 135);
            this.panelErettsegiTanusitvanyFileName.Name = "panelErettsegiTanusitvanyFileName";
            this.panelErettsegiTanusitvanyFileName.Size = new Size(522, 32);
            this.panelErettsegiTanusitvanyFileName.TabIndex = 19;
            // 
            // textBoxFileNameFeltoltErettsegiTanusitvany
            // 
            this.textBoxFileNameFeltoltErettsegiTanusitvany.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltErettsegiTanusitvany.Location = new Point(213, 5);
            this.textBoxFileNameFeltoltErettsegiTanusitvany.Name = "textBoxFileNameFeltoltErettsegiTanusitvany";
            this.textBoxFileNameFeltoltErettsegiTanusitvany.ReadOnly = true;
            this.textBoxFileNameFeltoltErettsegiTanusitvany.Size = new Size(300, 24);
            this.textBoxFileNameFeltoltErettsegiTanusitvany.TabIndex = 11;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label20.Location = new Point(5, 2);
            this.label20.Name = "label20";
            this.label20.Size = new Size(35, 18);
            this.label20.TabIndex = 12;
            this.label20.Text = "Fájl:";
            // 
            // textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany
            // 
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Location = new Point(222, 107);
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.MaxLength = 13;
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Name = "textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany";
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.Size = new Size(300, 24);
            this.textBoxTanuloiAzonositoFeltoltErettsegiTanusitvany.TabIndex = 18;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label19.Location = new Point(11, 107);
            this.label19.Name = "label19";
            this.label19.Size = new Size(130, 18);
            this.label19.TabIndex = 17;
            this.label19.Text = "Tanulói azonosító:";
            // 
            // numericUpDownVizsgaEveFeltoltErettsegiTanusitvany
            // 
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Location = new Point(222, 77);
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
            this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany.Size = new Size(100, 24);
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
            this.label21.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label21.Location = new Point(11, 74);
            this.label21.Name = "label21";
            this.label21.Size = new Size(79, 18);
            this.label21.TabIndex = 5;
            this.label21.Text = "Vizsga éve";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label22.Location = new Point(11, 17);
            this.label22.Name = "label22";
            this.label22.Size = new Size(88, 18);
            this.label22.TabIndex = 3;
            this.label22.Text = "Tanuló neve";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label23.Location = new Point(11, 44);
            this.label23.Name = "label23";
            this.label23.Size = new Size(78, 18);
            this.label23.TabIndex = 4;
            this.label23.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltErettsegiTanusitvany
            // 
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Location = new Point(222, 47);
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Name = "textBoxAnyjaNeveFeltoltErettsegiTanusitvany";
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.Size = new Size(300, 24);
            this.textBoxAnyjaNeveFeltoltErettsegiTanusitvany.TabIndex = 1;
            // 
            // textBoxTanuloNeveFeltoltErettsegiTanusitvany
            // 
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.Location = new Point(222, 17);
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.Name = "textBoxTanuloNeveFeltoltErettsegiTanusitvany";
            this.textBoxTanuloNeveFeltoltErettsegiTanusitvany.Size = new Size(300, 24);
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
            this.panelSzakmaiVizsgaTorzslapFeltolt.Location = new Point(142, 9);
            this.panelSzakmaiVizsgaTorzslapFeltolt.Name = "panelSzakmaiVizsgaTorzslapFeltolt";
            this.panelSzakmaiVizsgaTorzslapFeltolt.Size = new Size(550, 170);
            this.panelSzakmaiVizsgaTorzslapFeltolt.TabIndex = 13;
            this.panelSzakmaiVizsgaTorzslapFeltolt.Visible = false;
            // 
            // panelSzakmaiVizsgaTorzslapFileName
            // 
            this.panelSzakmaiVizsgaTorzslapFileName.Controls.Add(this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap);
            this.panelSzakmaiVizsgaTorzslapFileName.Controls.Add(this.label12);
            this.panelSzakmaiVizsgaTorzslapFileName.Location = new Point(14, 128);
            this.panelSzakmaiVizsgaTorzslapFileName.Name = "panelSzakmaiVizsgaTorzslapFileName";
            this.panelSzakmaiVizsgaTorzslapFileName.Size = new Size(522, 39);
            this.panelSzakmaiVizsgaTorzslapFileName.TabIndex = 17;
            // 
            // textBoxFileNameFeltoltSzakmaiVizsgaTorzslap
            // 
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Location = new Point(208, 7);
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Name = "textBoxFileNameFeltoltSzakmaiVizsgaTorzslap";
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.ReadOnly = true;
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Size = new Size(300, 24);
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.TabIndex = 11;
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.TextChanged += new EventHandler(this.textBoxFileNameFeltolt_TextChanged);
            this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap.Validating += new CancelEventHandler(this.textBoxFileNameFeltoltSzakmaiVizsgaTorzslap_Validating);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new Point(7, 10);
            this.label12.Name = "label12";
            this.label12.Size = new Size(35, 18);
            this.label12.TabIndex = 12;
            this.label12.Text = "Fájl:";
            // 
            // numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap
            // 
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Location = new Point(222, 74);
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
            this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap.Size = new Size(100, 24);
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
            this.label13.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label13.Location = new Point(11, 106);
            this.label13.Name = "label13";
            this.label13.Size = new Size(64, 18);
            this.label13.TabIndex = 15;
            this.label13.Text = "Időszak:";
            // 
            // radioButtonOszFeltoltSzakmaiVizsgaTorzslap
            // 
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.AutoSize = true;
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Location = new Point(467, 104);
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Name = "radioButtonOszFeltoltSzakmaiVizsgaTorzslap";
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Size = new Size(54, 22);
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.TabIndex = 14;
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.Text = "Ősz";
            this.radioButtonOszFeltoltSzakmaiVizsgaTorzslap.UseVisualStyleBackColor = true;
            // 
            // radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap
            // 
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.AutoSize = true;
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Checked = true;
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Location = new Point(222, 104);
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Name = "radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap";
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Size = new Size(74, 22);
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.TabIndex = 13;
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.TabStop = true;
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.Text = "Tavasz";
            this.radioButtonTavaszFeltoltSzakmaiVizsgaTorzslap.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new Point(11, 76);
            this.label11.Name = "label11";
            this.label11.Size = new Size(79, 18);
            this.label11.TabIndex = 5;
            this.label11.Text = "Vizsga éve";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new Point(11, 17);
            this.label9.Name = "label9";
            this.label9.Size = new Size(88, 18);
            this.label9.TabIndex = 3;
            this.label9.Text = "Tanuló neve";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new Point(11, 47);
            this.label10.Name = "label10";
            this.label10.Size = new Size(78, 18);
            this.label10.TabIndex = 4;
            this.label10.Text = "Anyja neve";
            // 
            // textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap
            // 
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Location = new Point(222, 44);
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Name = "textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap";
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.Size = new Size(300, 24);
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.TabIndex = 1;
            this.textBoxAnyjaNeveFeltoltSzakmaiVizsgaTorzslap.TextChanged += new EventHandler(this.textBoxAnyjaNeveFeltolt_TextChanged);
            // 
            // textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap
            // 
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Location = new Point(222, 14);
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Name = "textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap";
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.Size = new Size(300, 24);
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.TabIndex = 0;
            this.textBoxTanuloNeveFeltoltSzakmaiVizsgaTorzslap.TextChanged += new EventHandler(this.textBoxTanuloNeveFeltolt_TextChanged);
            // 
            // buttonMegse
            // 
            this.buttonMegse.Cursor = Cursors.Default;
            this.buttonMegse.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonMegse.Location = new Point(30, 664);
            this.buttonMegse.Margin = new Padding(10);
            this.buttonMegse.Name = "buttonMegse";
            this.buttonMegse.Size = new Size(153, 41);
            this.buttonMegse.TabIndex = 8;
            this.buttonMegse.Text = "Mégse";
            this.buttonMegse.UseVisualStyleBackColor = true;
            this.buttonMegse.Click += new EventHandler(this.buttonMegse_Click);
            // 
            // labelMenuKat
            // 
            this.labelMenuKat.AutoSize = true;
            this.labelMenuKat.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(238)));
            this.labelMenuKat.Location = new Point(80, 16);
            this.labelMenuKat.Name = "labelMenuKat";
            this.labelMenuKat.Size = new Size(23, 31);
            this.labelMenuKat.TabIndex = 9;
            this.labelMenuKat.Text = ".";
            this.labelMenuKat.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonMentes
            // 
            this.buttonMentes.Cursor = Cursors.Default;
            this.buttonMentes.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            this.buttonMentes.Location = new Point(376, 664);
            this.buttonMentes.Margin = new Padding(10);
            this.buttonMentes.Name = "buttonMentes";
            this.buttonMentes.Size = new Size(153, 41);
            this.buttonMentes.TabIndex = 6;
            this.buttonMentes.Text = "Mentés";
            this.buttonMentes.UseVisualStyleBackColor = true;
            this.buttonMentes.Click += new EventHandler(this.ButtonMentes_Click);
            // 
            // panelFeltModTorl
            // 
            this.panelFeltModTorl.Controls.Add(this.panelModTorol);
            this.panelFeltModTorl.Controls.Add(this.buttonFeltoltes);
            this.panelFeltModTorl.Location = new Point(620, 21);
            this.panelFeltModTorl.Margin = new Padding(10);
            this.panelFeltModTorl.Name = "panelFeltModTorl";
            this.panelFeltModTorl.Size = new Size(181, 189);
            this.panelFeltModTorl.TabIndex = 32;
            this.panelFeltModTorl.Visible = false;
            // 
            // panelModTorol
            // 
            this.panelModTorol.Controls.Add(this.buttonModositas);
            this.panelModTorol.Controls.Add(this.buttonTorles);
            this.panelModTorol.Location = new Point(12, 62);
            this.panelModTorol.Name = "panelModTorol";
            this.panelModTorol.Size = new Size(165, 118);
            this.panelModTorol.TabIndex = 22;
            // 
            // groupBoxAlso
            // 
            this.groupBoxAlso.Anchor = AnchorStyles.Bottom;
            this.groupBoxAlso.Controls.Add(this.label4);
            this.groupBoxAlso.Controls.Add(this.labelKapcsolatAdatbazissal);
            this.groupBoxAlso.Location = new Point(0, 822);
            this.groupBoxAlso.Name = "groupBoxAlso";
            this.groupBoxAlso.Size = new Size(1586, 40);
            this.groupBoxAlso.TabIndex = 33;
            this.groupBoxAlso.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1584, 861);
            this.Controls.Add(this.groupBoxAlso);
            this.Controls.Add(this.panelFeltModTorl);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelKeres);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBoxEleresi);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Nyílvántartó";
            this.FormClosing += new FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new EventHandler(this.FormMain_Load);
            this.Paint += new PaintEventHandler(this.FormMain_Paint);
            this.groupBoxEleresi.ResumeLayout(false);
            this.groupBoxEleresi.PerformLayout();
            ((ISupportInitialize)(this.dataGridView1)).EndInit();
            ((ISupportInitialize)(this.numericUpDownVizsgaÉveKeres)).EndInit();
            this.panelKeres.ResumeLayout(false);
            this.panelKeres.PerformLayout();
            ((ISupportInitialize)(this.numericUpDownTalalatokSzama)).EndInit();
            this.panelMenu.ResumeLayout(false);
            this.panelFeltolt.ResumeLayout(false);
            this.panelFeltolt.PerformLayout();
            this.panelKozepiskolaAnyakonyvFeltolt.ResumeLayout(false);
            this.panelKozepiskolaAnyakonyvFeltolt.PerformLayout();
            this.panelKozepiskolaAnyakonyvFilneName.ResumeLayout(false);
            this.panelKozepiskolaAnyakonyvFilneName.PerformLayout();
            ((ISupportInitialize)(this.numericUpDownErettsegiEveFeltoltKozepiskolaAnyakonyv)).EndInit();
            ((ISupportInitialize)(this.numericUpDownKozepiskKezdeteFeltoltKozepiskolaAnyakonyv)).EndInit();
            this.panelErettsegiTorzslapFeltolt.ResumeLayout(false);
            this.panelErettsegiTorzslapFeltolt.PerformLayout();
            this.panelErettsegiTtorzslapFileName.ResumeLayout(false);
            this.panelErettsegiTtorzslapFileName.PerformLayout();
            ((ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltErettsegiTorzslap)).EndInit();
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.ResumeLayout(false);
            this.panelSzakmaiVizsgaAnyakonyvFeltolt.PerformLayout();
            this.panelSzakmaivizsgaAnyakonyvFileName.ResumeLayout(false);
            this.panelSzakmaivizsgaAnyakonyvFileName.PerformLayout();
            ((ISupportInitialize)(this.numericUpDownErettsegiEveFeltoltSzakmaiVizsgaAnyakonyv)).EndInit();
            ((ISupportInitialize)(this.numericUpDownKozepiskKezdeteFeltoltSzakmaiVizsgaAnyakonyv)).EndInit();
            this.panelTallozMentesujButton.ResumeLayout(false);
            this.panelErettsegiTanusitvanyFeltolt.ResumeLayout(false);
            this.panelErettsegiTanusitvanyFeltolt.PerformLayout();
            this.panelErettsegiTanusitvanyFileName.ResumeLayout(false);
            this.panelErettsegiTanusitvanyFileName.PerformLayout();
            ((ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltErettsegiTanusitvany)).EndInit();
            this.panelSzakmaiVizsgaTorzslapFeltolt.ResumeLayout(false);
            this.panelSzakmaiVizsgaTorzslapFeltolt.PerformLayout();
            this.panelSzakmaiVizsgaTorzslapFileName.ResumeLayout(false);
            this.panelSzakmaiVizsgaTorzslapFileName.PerformLayout();
            ((ISupportInitialize)(this.numericUpDownVizsgaEveFeltoltSzakmaiVizsgaTorzslap)).EndInit();
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

