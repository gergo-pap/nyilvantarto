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
            this.buttonErettsegi = new System.Windows.Forms.Button();
            this.buttonSzkmaiVizsga = new System.Windows.Forms.Button();
            this.buttonKozepiskola = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonErettsegi
            // 
            this.buttonErettsegi.Location = new System.Drawing.Point(246, 180);
            this.buttonErettsegi.Name = "buttonErettsegi";
            this.buttonErettsegi.Size = new System.Drawing.Size(75, 23);
            this.buttonErettsegi.TabIndex = 0;
            this.buttonErettsegi.Text = "Érettségi";
            this.buttonErettsegi.UseVisualStyleBackColor = true;
            this.buttonErettsegi.Click += new System.EventHandler(this.buttonErettsegi_Click);
            // 
            // buttonSzkmaiVizsga
            // 
            this.buttonSzkmaiVizsga.Location = new System.Drawing.Point(381, 179);
            this.buttonSzkmaiVizsga.Name = "buttonSzkmaiVizsga";
            this.buttonSzkmaiVizsga.Size = new System.Drawing.Size(112, 23);
            this.buttonSzkmaiVizsga.TabIndex = 1;
            this.buttonSzkmaiVizsga.Text = "Szakmai vizsga";
            this.buttonSzkmaiVizsga.UseVisualStyleBackColor = true;
            this.buttonSzkmaiVizsga.Click += new System.EventHandler(this.buttonSzkmaiVizsga_Click);
            // 
            // buttonKozepiskola
            // 
            this.buttonKozepiskola.Location = new System.Drawing.Point(550, 179);
            this.buttonKozepiskola.Name = "buttonKozepiskola";
            this.buttonKozepiskola.Size = new System.Drawing.Size(75, 23);
            this.buttonKozepiskola.TabIndex = 2;
            this.buttonKozepiskola.Text = "Középiskola";
            this.buttonKozepiskola.UseVisualStyleBackColor = true;
            this.buttonKozepiskola.Click += new System.EventHandler(this.buttonKozepiskola_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonKozepiskola);
            this.Controls.Add(this.buttonSzkmaiVizsga);
            this.Controls.Add(this.buttonErettsegi);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nyílvántartó";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonErettsegi;
        private System.Windows.Forms.Button buttonSzkmaiVizsga;
        private System.Windows.Forms.Button buttonKozepiskola;
    }
}

