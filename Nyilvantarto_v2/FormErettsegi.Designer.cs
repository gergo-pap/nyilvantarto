namespace Nyilvantarto_v2
{
    partial class FormErettsegi
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
            this.buttonTorzslap = new System.Windows.Forms.Button();
            this.buttonTanusitvany = new System.Windows.Forms.Button();
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
            // buttonTorzslap
            // 
            this.buttonTorzslap.Location = new System.Drawing.Point(260, 172);
            this.buttonTorzslap.Name = "buttonTorzslap";
            this.buttonTorzslap.Size = new System.Drawing.Size(75, 23);
            this.buttonTorzslap.TabIndex = 2;
            this.buttonTorzslap.Text = "Törzslap";
            this.buttonTorzslap.UseVisualStyleBackColor = true;
            this.buttonTorzslap.Click += new System.EventHandler(this.buttonTorzslap_Click);
            // 
            // buttonTanusitvany
            // 
            this.buttonTanusitvany.Location = new System.Drawing.Point(469, 172);
            this.buttonTanusitvany.Name = "buttonTanusitvany";
            this.buttonTanusitvany.Size = new System.Drawing.Size(75, 23);
            this.buttonTanusitvany.TabIndex = 3;
            this.buttonTanusitvany.Text = "Tanusítvány";
            this.buttonTanusitvany.UseVisualStyleBackColor = true;
            this.buttonTanusitvany.Click += new System.EventHandler(this.buttonTanusitvany_Click);
            // 
            // FormErettsegi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonTanusitvany);
            this.Controls.Add(this.buttonTorzslap);
            this.Controls.Add(this.buttonVissza);
            this.Name = "FormErettsegi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Érettségi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormErettsegi_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonVissza;
        private System.Windows.Forms.Button buttonTorzslap;
        private System.Windows.Forms.Button buttonTanusitvany;
    }
}