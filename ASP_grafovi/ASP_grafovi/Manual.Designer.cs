namespace ASP_grafovi
{
    partial class Manual
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
            this.panelSlika = new System.Windows.Forms.Panel();
            this.slikaPB = new System.Windows.Forms.PictureBox();
            this.printBtn = new MetroFramework.Controls.MetroButton();
            this.panelSlika.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slikaPB)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSlika
            // 
            this.panelSlika.Controls.Add(this.slikaPB);
            this.panelSlika.Location = new System.Drawing.Point(23, 63);
            this.panelSlika.Name = "panelSlika";
            this.panelSlika.Size = new System.Drawing.Size(225, 153);
            this.panelSlika.TabIndex = 0;
            // 
            // slikaPB
            // 
            this.slikaPB.Location = new System.Drawing.Point(0, 0);
            this.slikaPB.Name = "slikaPB";
            this.slikaPB.Size = new System.Drawing.Size(225, 153);
            this.slikaPB.TabIndex = 0;
            this.slikaPB.TabStop = false;
            // 
            // printBtn
            // 
            this.printBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.printBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.printBtn.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.printBtn.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.printBtn.Location = new System.Drawing.Point(129, 23);
            this.printBtn.Name = "printBtn";
            this.printBtn.Size = new System.Drawing.Size(107, 32);
            this.printBtn.Style = MetroFramework.MetroColorStyle.Lime;
            this.printBtn.TabIndex = 1;
            this.printBtn.Text = "Štampaj";
            this.printBtn.UseCustomBackColor = true;
            this.printBtn.UseSelectable = true;
            this.printBtn.Click += new System.EventHandler(this.printBtn_Click);
            // 
            // Manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(271, 239);
            this.Controls.Add(this.printBtn);
            this.Controls.Add(this.panelSlika);
            this.Name = "Manual";
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Uputstvo";
            this.Resize += new System.EventHandler(this.calcBtnLoc);
            this.panelSlika.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slikaPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSlika;
        private System.Windows.Forms.PictureBox slikaPB;
        private MetroFramework.Controls.MetroButton printBtn;
    }
}