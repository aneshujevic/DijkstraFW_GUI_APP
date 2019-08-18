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
    public partial class loginForm : MetroForm
    {
        private grafoviEntities ctx;
        private Grafovi graf;
        public string username;

        public loginForm()
        {
            InitializeComponent();
            ctx = new grafoviEntities();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            korisnici x = null;

            try { x = ctx.korisnici.First(v => v.username == usernameInput.Text); }
            catch { MessageBox.Show("Ne postoji korisnik sa tim username."); return; }

            if (passInput.Text.Length < 8)
            {
                MessageBox.Show("Polje za šifru ne sme biti prazno i mora imati makar 8 karaktera..");
                return;
            }

            if (x != null && x.password1 == passInput.Text)
            {
                username = usernameInput.Text;
                graf = new Grafovi(this);
                graf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Pogresna sifra!");
                passInput.Clear();
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            RegisterForm registracija = new RegisterForm();
            registracija.ShowDialog();
        }
    }
}
