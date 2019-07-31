using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASP_grafovi
{ 
    public partial class Manual : MetroForm
    {
        string path;                                                                            // putanja do uputstva
        public Manual(who Mode)
        {
            InitializeComponent();

            path = Mode == who.algoritmi ? @"manuals\Algoritmi.jpg" : @"manuals\ProgMan.jpg";   // da li se prikazuje uputstvo za algoritme ili program

            panelSlika.Dock = DockStyle.Fill;                                                   // panel sa slikom
            panelSlika.AutoScroll = true;

            slikaPB.SizeMode = PictureBoxSizeMode.AutoSize;                                     // inicijalizacija picturebox-a za automatsko prosirivanje

            try { slikaPB.Image = Image.FromFile(path); } 
            catch { MessageBox.Show("Fajl sa korisničkim uputstvima ne postoji. :("); }

            if (Mode == who.program)
                this.Size = new Size(1140, 720);
            else
                this.Size = new Size(1100, 720);

            calcBtnLoc(null, null);
        }

        // lokacija dugmeta za stampanje
        private void calcBtnLoc(object sender, EventArgs e)
        {
            printBtn.Location = new Point(Size.Width / 2 - 52, 25);
        }

        // Dugme za stampanje
        private void printBtn_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPage;
            pd.Print();
        }

        // stampanje slike u buffer sa odgovarajucim try catch za nepostojanje uputstva
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            try {
                Image image = Image.FromFile(path);
                e.Graphics.DrawImage(image, new Point(0, 0));
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Korisničko uputstvo ne postoji :(");
            }
            
        }
    }
}
