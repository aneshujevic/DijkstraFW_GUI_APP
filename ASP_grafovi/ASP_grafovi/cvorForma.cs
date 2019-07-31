using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASP_grafovi
{
    public partial class cvorForma : MetroForm
    {
        public int mNode;
        
        // forma za izbor pocetnog cvora za Dajkstrin algoritam
        public cvorForma()
        {
            InitializeComponent();

            mNode = -1;

            List<int> listica = new List<int>();
            foreach (var node in Platno.nodes)
            {
                listica.Add(node.ID);
            }
            cvorBox.DataSource = listica;
        }

        // ukoliko je izbor zavrsen odgovarajuci edge - case provere
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var x = int.Parse(cvorBox.SelectedIndex.ToString());
                
                if (x < 0)
                    throw new Exception();
                else
                {
                    mNode = x;
                }
            }
            catch
            {
                MessageBox.Show("Nevažeći čvor. Izaberite čvor putem broja napisanom na čvoru.");
            }

            this.Close();
        }
    }
}

