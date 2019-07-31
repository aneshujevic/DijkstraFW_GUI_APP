using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace ASP_grafovi
{
    public partial class tezinaForma : MetroForm
    {
        public static int mWeight;                                      // tezina kojoj se pristupa iz drugih fajlova
                                                                        // tj. nova tezina grane
        private Grafovi parentForm;

        public tezinaForma(Grafovi parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        // kada se aktivira dugme proveravamo da li je tezina pozitivna i ako jeste postavljamo je u mWeight
        // ako je negativna i nisu dozvoljene negativne obavestavamo korisnika da tezina mora biti pozitivna
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var x = int.Parse(tezinaTb.Text);
                if (x <= 0 && parentForm.negativneGrane == false)
                    throw new Exception();
                else
                {
                    mWeight = x;
                }
            }
            catch
            {
                MessageBox.Show("Težina mora biti pozitivan broj.");
            }
            
            this.Close();
        }
    }
}
