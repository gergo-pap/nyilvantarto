namespace Nyilvantarto_v2
{
    partial class FormErettsegiTorzslap
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
            // FormErettsegiTorzslap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonVissza);
            this.Name = "FormErettsegiTorzslap";
            this.Text = "FormErettsegiTorzslap";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonVissza;
    }
}