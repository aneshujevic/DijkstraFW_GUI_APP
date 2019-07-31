namespace ASP_grafovi
{
    partial class Platno
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Platno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "Platno";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Platno_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Platno_KeyUp);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
