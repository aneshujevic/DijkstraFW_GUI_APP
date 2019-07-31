namespace ASP_grafovi
{
    partial class Grafovi
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
            this.addGraphBtn = new MetroFramework.Controls.MetroButton();
            this.graphCanvasContainer = new System.Windows.Forms.Panel();
            this.dijkstraBtn = new MetroFramework.Controls.MetroButton();
            this.floydBtn = new MetroFramework.Controls.MetroButton();
            this.tezineToggle = new MetroFramework.Controls.MetroToggle();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.speedTrckBr = new MetroFramework.Controls.MetroTrackBar();
            this.speedLbl = new MetroFramework.Controls.MetroLabel();
            this.prviCvorLbl = new MetroFramework.Controls.MetroLabel();
            this.drugiCvorLbl = new MetroFramework.Controls.MetroLabel();
            this.prviCvorCb = new MetroFramework.Controls.MetroComboBox();
            this.drugiCvorCb = new MetroFramework.Controls.MetroComboBox();
            this.prikazPutaBtn = new MetroFramework.Controls.MetroButton();
            this.negativneGraneToggle = new MetroFramework.Controls.MetroToggle();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.prikazSredisteBtn = new MetroFramework.Controls.MetroButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.podaciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sačuvajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.učitajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.korisničkoUputstvoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.korisničkoUputstvoTSP = new System.Windows.Forms.ToolStripMenuItem();
            this.opisAlgoritamaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oNamaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addGraphBtn
            // 
            this.addGraphBtn.Location = new System.Drawing.Point(20, 97);
            this.addGraphBtn.Name = "addGraphBtn";
            this.addGraphBtn.Size = new System.Drawing.Size(201, 71);
            this.addGraphBtn.TabIndex = 0;
            this.addGraphBtn.Text = "Novi graf";
            this.addGraphBtn.UseSelectable = true;
            this.addGraphBtn.Click += new System.EventHandler(this.addGraphBtn_Click);
            // 
            // graphCanvasContainer
            // 
            this.graphCanvasContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphCanvasContainer.Location = new System.Drawing.Point(259, 97);
            this.graphCanvasContainer.MinimumSize = new System.Drawing.Size(596, 439);
            this.graphCanvasContainer.Name = "graphCanvasContainer";
            this.graphCanvasContainer.Size = new System.Drawing.Size(776, 579);
            this.graphCanvasContainer.TabIndex = 2;
            // 
            // dijkstraBtn
            // 
            this.dijkstraBtn.Location = new System.Drawing.Point(16, 289);
            this.dijkstraBtn.Name = "dijkstraBtn";
            this.dijkstraBtn.Size = new System.Drawing.Size(201, 75);
            this.dijkstraBtn.TabIndex = 3;
            this.dijkstraBtn.Text = "Dijkstrin algoritam";
            this.dijkstraBtn.UseSelectable = true;
            this.dijkstraBtn.Click += new System.EventHandler(this.dijkstraBtn_Click);
            // 
            // floydBtn
            // 
            this.floydBtn.Location = new System.Drawing.Point(16, 381);
            this.floydBtn.Name = "floydBtn";
            this.floydBtn.Size = new System.Drawing.Size(201, 75);
            this.floydBtn.TabIndex = 3;
            this.floydBtn.Text = "Flojdov algoritam";
            this.floydBtn.UseSelectable = true;
            this.floydBtn.Click += new System.EventHandler(this.floydBtn_Click);
            // 
            // tezineToggle
            // 
            this.tezineToggle.AutoSize = true;
            this.tezineToggle.Location = new System.Drawing.Point(67, 207);
            this.tezineToggle.Name = "tezineToggle";
            this.tezineToggle.Size = new System.Drawing.Size(80, 17);
            this.tezineToggle.TabIndex = 4;
            this.tezineToggle.Text = "Off";
            this.tezineToggle.UseSelectable = true;
            this.tezineToggle.CheckedChanged += new System.EventHandler(this.metroToggle1_CheckedChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(20, 185);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(176, 19);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Mod za izmenu težina grana";
            // 
            // speedTrckBr
            // 
            this.speedTrckBr.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.speedTrckBr.BackColor = System.Drawing.Color.Transparent;
            this.speedTrckBr.Location = new System.Drawing.Point(455, 680);
            this.speedTrckBr.Maximum = 500;
            this.speedTrckBr.Minimum = 1;
            this.speedTrckBr.Name = "speedTrckBr";
            this.speedTrckBr.Size = new System.Drawing.Size(75, 23);
            this.speedTrckBr.TabIndex = 6;
            this.speedTrckBr.Text = "Brzina";
            this.speedTrckBr.Value = 500;
            // 
            // speedLbl
            // 
            this.speedLbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.speedLbl.AutoSize = true;
            this.speedLbl.Location = new System.Drawing.Point(345, 680);
            this.speedLbl.Name = "speedLbl";
            this.speedLbl.Size = new System.Drawing.Size(104, 19);
            this.speedLbl.TabIndex = 7;
            this.speedLbl.Text = "Brzina animacije";
            // 
            // prviCvorLbl
            // 
            this.prviCvorLbl.AutoSize = true;
            this.prviCvorLbl.Location = new System.Drawing.Point(15, 505);
            this.prviCvorLbl.Name = "prviCvorLbl";
            this.prviCvorLbl.Size = new System.Drawing.Size(61, 19);
            this.prviCvorLbl.TabIndex = 8;
            this.prviCvorLbl.Text = "Prvi čvor";
            // 
            // drugiCvorLbl
            // 
            this.drugiCvorLbl.AutoSize = true;
            this.drugiCvorLbl.Location = new System.Drawing.Point(133, 505);
            this.drugiCvorLbl.Name = "drugiCvorLbl";
            this.drugiCvorLbl.Size = new System.Drawing.Size(70, 19);
            this.drugiCvorLbl.TabIndex = 8;
            this.drugiCvorLbl.Text = "Drugi čvor";
            // 
            // prviCvorCb
            // 
            this.prviCvorCb.FormattingEnabled = true;
            this.prviCvorCb.ItemHeight = 23;
            this.prviCvorCb.Location = new System.Drawing.Point(16, 473);
            this.prviCvorCb.Name = "prviCvorCb";
            this.prviCvorCb.Size = new System.Drawing.Size(79, 29);
            this.prviCvorCb.TabIndex = 9;
            this.prviCvorCb.UseSelectable = true;
            // 
            // drugiCvorCb
            // 
            this.drugiCvorCb.FormattingEnabled = true;
            this.drugiCvorCb.ItemHeight = 23;
            this.drugiCvorCb.Location = new System.Drawing.Point(133, 473);
            this.drugiCvorCb.Name = "drugiCvorCb";
            this.drugiCvorCb.Size = new System.Drawing.Size(82, 29);
            this.drugiCvorCb.TabIndex = 9;
            this.drugiCvorCb.UseSelectable = true;
            // 
            // prikazPutaBtn
            // 
            this.prikazPutaBtn.Location = new System.Drawing.Point(14, 536);
            this.prikazPutaBtn.Name = "prikazPutaBtn";
            this.prikazPutaBtn.Size = new System.Drawing.Size(201, 56);
            this.prikazPutaBtn.TabIndex = 3;
            this.prikazPutaBtn.Text = "Prikaži put";
            this.prikazPutaBtn.UseSelectable = true;
            this.prikazPutaBtn.Click += new System.EventHandler(this.floydBtn_Click);
            // 
            // negativneGraneToggle
            // 
            this.negativneGraneToggle.AutoSize = true;
            this.negativneGraneToggle.Location = new System.Drawing.Point(67, 258);
            this.negativneGraneToggle.Name = "negativneGraneToggle";
            this.negativneGraneToggle.Size = new System.Drawing.Size(80, 17);
            this.negativneGraneToggle.TabIndex = 10;
            this.negativneGraneToggle.Text = "Off";
            this.negativneGraneToggle.UseSelectable = true;
            this.negativneGraneToggle.CheckedChanged += new System.EventHandler(this.floydGraphToggle_CheckedChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(20, 236);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(150, 19);
            this.metroLabel2.TabIndex = 12;
            this.metroLabel2.Text = "Dozvoli negativne težine";
            // 
            // prikazSredisteBtn
            // 
            this.prikazSredisteBtn.Location = new System.Drawing.Point(16, 620);
            this.prikazSredisteBtn.Name = "prikazSredisteBtn";
            this.prikazSredisteBtn.Size = new System.Drawing.Size(201, 56);
            this.prikazSredisteBtn.TabIndex = 3;
            this.prikazSredisteBtn.Text = "Prikaži središte";
            this.prikazSredisteBtn.UseSelectable = true;
            this.prikazSredisteBtn.Click += new System.EventHandler(this.floydBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.podaciToolStripMenuItem,
            this.korisničkoUputstvoToolStripMenuItem,
            this.oNamaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // podaciToolStripMenuItem
            // 
            this.podaciToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sačuvajToolStripMenuItem,
            this.učitajToolStripMenuItem});
            this.podaciToolStripMenuItem.Name = "podaciToolStripMenuItem";
            this.podaciToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.podaciToolStripMenuItem.Text = "Graf";
            // 
            // sačuvajToolStripMenuItem
            // 
            this.sačuvajToolStripMenuItem.Name = "sačuvajToolStripMenuItem";
            this.sačuvajToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.sačuvajToolStripMenuItem.Text = "Sačuvaj";
            this.sačuvajToolStripMenuItem.Click += new System.EventHandler(this.sačuvajToolStripMenuItem_Click);
            // 
            // učitajToolStripMenuItem
            // 
            this.učitajToolStripMenuItem.Name = "učitajToolStripMenuItem";
            this.učitajToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.učitajToolStripMenuItem.Text = "Učitaj";
            this.učitajToolStripMenuItem.Click += new System.EventHandler(this.učitajToolStripMenuItem_Click);
            // 
            // korisničkoUputstvoToolStripMenuItem
            // 
            this.korisničkoUputstvoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.korisničkoUputstvoTSP,
            this.opisAlgoritamaToolStripMenuItem});
            this.korisničkoUputstvoToolStripMenuItem.Name = "korisničkoUputstvoToolStripMenuItem";
            this.korisničkoUputstvoToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.korisničkoUputstvoToolStripMenuItem.Text = "Pomoć";
            // 
            // korisničkoUputstvoTSP
            // 
            this.korisničkoUputstvoTSP.Name = "korisničkoUputstvoTSP";
            this.korisničkoUputstvoTSP.Size = new System.Drawing.Size(179, 22);
            this.korisničkoUputstvoTSP.Text = "Korisničko uputstvo";
            this.korisničkoUputstvoTSP.Click += new System.EventHandler(this.korisničkoUputstvoToolStripMenuItem_Click);
            // 
            // opisAlgoritamaToolStripMenuItem
            // 
            this.opisAlgoritamaToolStripMenuItem.Name = "opisAlgoritamaToolStripMenuItem";
            this.opisAlgoritamaToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.opisAlgoritamaToolStripMenuItem.Text = "Opis algoritama";
            // 
            // oNamaToolStripMenuItem
            // 
            this.oNamaToolStripMenuItem.Name = "oNamaToolStripMenuItem";
            this.oNamaToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.oNamaToolStripMenuItem.Text = "O nama";
            this.oNamaToolStripMenuItem.Click += new System.EventHandler(this.oNamaToolStripMenuItem_Click);
            // 
            // Grafovi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 714);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.negativneGraneToggle);
            this.Controls.Add(this.drugiCvorCb);
            this.Controls.Add(this.prviCvorCb);
            this.Controls.Add(this.drugiCvorLbl);
            this.Controls.Add(this.prviCvorLbl);
            this.Controls.Add(this.speedLbl);
            this.Controls.Add(this.speedTrckBr);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.tezineToggle);
            this.Controls.Add(this.prikazSredisteBtn);
            this.Controls.Add(this.prikazPutaBtn);
            this.Controls.Add(this.floydBtn);
            this.Controls.Add(this.dijkstraBtn);
            this.Controls.Add(this.graphCanvasContainer);
            this.Controls.Add(this.addGraphBtn);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(836, 525);
            this.Name = "Grafovi";
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Grafovi";
            this.TransparencyKey = System.Drawing.Color.CornflowerBlue;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Grafovi_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton addGraphBtn;
        private System.Windows.Forms.Panel graphCanvasContainer;
        private MetroFramework.Controls.MetroButton dijkstraBtn;
        private MetroFramework.Controls.MetroButton floydBtn;
        private MetroFramework.Controls.MetroToggle tezineToggle;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        public MetroFramework.Controls.MetroTrackBar speedTrckBr;
        private MetroFramework.Controls.MetroLabel speedLbl;
        private MetroFramework.Controls.MetroLabel prviCvorLbl;
        private MetroFramework.Controls.MetroLabel drugiCvorLbl;
        private MetroFramework.Controls.MetroComboBox prviCvorCb;
        private MetroFramework.Controls.MetroComboBox drugiCvorCb;
        private MetroFramework.Controls.MetroButton prikazPutaBtn;
        private MetroFramework.Controls.MetroToggle negativneGraneToggle;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton prikazSredisteBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem korisničkoUputstvoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oNamaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem korisničkoUputstvoTSP;
        private System.Windows.Forms.ToolStripMenuItem opisAlgoritamaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem podaciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sačuvajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem učitajToolStripMenuItem;
    }
}