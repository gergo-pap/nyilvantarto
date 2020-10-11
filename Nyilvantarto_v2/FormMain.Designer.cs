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
            this.groupBoxButtons = new System.Windows.Forms.GroupBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.labelKapcsolatAdatbazissal = new System.Windows.Forms.Label();
            this.groupBoxEleresi.SuspendLayout();
            this.groupBoxButtons.SuspendLayout();
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
            // groupBoxButtons
            // 
            this.groupBoxButtons.Controls.Add(this.buttonSzakmaiVizsga);
            this.groupBoxButtons.Controls.Add(this.buttonErettsegi);
            this.groupBoxButtons.Controls.Add(this.buttonKozepiskola);
            this.groupBoxButtons.Location = new System.Drawing.Point(221, 165);
            this.groupBoxButtons.Name = "groupBoxButtons";
            this.groupBoxButtons.Size = new System.Drawing.Size(430, 100);
            this.groupBoxButtons.TabIndex = 8;
            this.groupBoxButtons.TabStop = false;
            this.groupBoxButtons.Visible = false;
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelKapcsolatAdatbazissal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBoxButtons);
            this.Controls.Add(this.groupBoxEleresi);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nyílvántartó";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxEleresi.ResumeLayout(false);
            this.groupBoxEleresi.PerformLayout();
            this.groupBoxButtons.ResumeLayout(false);
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
    }
}

