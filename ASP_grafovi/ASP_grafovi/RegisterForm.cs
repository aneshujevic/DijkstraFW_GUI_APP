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
    public partial class RegisterForm : MetroForm
    {
        private grafoviEntities ctx;
        public RegisterForm()
        {
            InitializeComponent();
            ctx = new grafoviEntities();
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            var x = new korisnici();
            x.username = usernameTb.Text;
            x.name = nameTb.Text;
            x.surname = surnameTb.Text;
            x.email = emailTb.Text;
            x.password1 = passwordTb.Text;

            if (!ctx.korisnici.Any(y => y.username == usernameTb.Text))
            {
                if (x.username.Length == 0 || x.name.Length == 0 || x.surname.Length == 0|| x.email.Length == 0 || x.password1.Length == 0)
                {
                    MessageBox.Show("Sva polja moraju biti popunjena!");
                    return;
                }

                if (x.password1.Length < 8)
                {
                    MessageBox.Show("Šifra mora imati makar 8 karaktera..");
                    return;
                }

                ctx.korisnici.Add(x);
                ctx.SaveChanges();
                MessageBox.Show("Uspešno registrovan korisnik!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Već postoji korisnik sa datim imenom!");
                passwordTb.Clear();
                usernameTb.Clear();
                return;
            }
        }
    }
}
