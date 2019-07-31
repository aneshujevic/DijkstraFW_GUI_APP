namespace ASP_grafovi
{
    partial class resultsMatrices
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
            this.resultPnl = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // resultPnl
            // 
            this.resultPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPnl.Location = new System.Drawing.Point(20, 60);
            this.resultPnl.Name = "resultPnl";
            this.resultPnl.Size = new System.Drawing.Size(260, 220);
            this.resultPnl.TabIndex = 0;
            this.resultPnl.Paint += new System.Windows.Forms.PaintEventHandler(this.resultPnl_Paint);
            // 
            // resultsMatrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.resultPnl);
            this.Name = "resultsMatrices";
            this.Text = "Rezultati";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel resultPnl;
    }
}