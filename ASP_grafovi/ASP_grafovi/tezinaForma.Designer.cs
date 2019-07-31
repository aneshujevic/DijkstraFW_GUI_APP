namespace ASP_grafovi
{
    partial class tezinaForma
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
            this.tezinaTb = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // tezinaTb
            // 
            // 
            // 
            // 
            this.tezinaTb.CustomButton.Image = null;
            this.tezinaTb.CustomButton.Location = new System.Drawing.Point(99, 1);
            this.tezinaTb.CustomButton.Name = "";
            this.tezinaTb.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tezinaTb.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tezinaTb.CustomButton.TabIndex = 1;
            this.tezinaTb.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tezinaTb.CustomButton.UseSelectable = true;
            this.tezinaTb.CustomButton.Visible = false;
            this.tezinaTb.Lines = new string[0];
            this.tezinaTb.Location = new System.Drawing.Point(117, 67);
            this.tezinaTb.MaximumSize = new System.Drawing.Size(121, 23);
            this.tezinaTb.MaxLength = 32767;
            this.tezinaTb.MinimumSize = new System.Drawing.Size(121, 23);
            this.tezinaTb.Name = "tezinaTb";
            this.tezinaTb.PasswordChar = '\0';
            this.tezinaTb.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tezinaTb.SelectedText = "";
            this.tezinaTb.SelectionLength = 0;
            this.tezinaTb.SelectionStart = 0;
            this.tezinaTb.ShortcutsEnabled = true;
            this.tezinaTb.Size = new System.Drawing.Size(121, 23);
            this.tezinaTb.TabIndex = 0;
            this.tezinaTb.UseSelectable = true;
            this.tezinaTb.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tezinaTb.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(52, 67);
            this.metroLabel1.MaximumSize = new System.Drawing.Size(53, 19);
            this.metroLabel1.MinimumSize = new System.Drawing.Size(53, 19);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(53, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Težina :";
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(117, 101);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(121, 23);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "OK";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // tezinaForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 147);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.tezinaTb);
            this.MaximumSize = new System.Drawing.Size(298, 147);
            this.MinimumSize = new System.Drawing.Size(298, 147);
            this.Name = "tezinaForma";
            this.Resizable = false;
            this.Text = "Unos težine";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox tezinaTb;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}