using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

namespace ASP_grafovi
{
    public enum mode { izmenaTezina = 0, dodavanjeCvora = 1  }                  // mod vezan za glavnu formu, da li se unose cvorovi ili menjaju tezine grana
    public enum who { algoritmi = 0, program = 1 }                              // vezan za uputstvo 0 za algoritme 1 za sam program
    public partial class Grafovi : MetroForm
    {
        public Platno ucCanvas;                                                 // Platno za iscrtavanje grafa i ostalog
        public static mode mod;
        public string username;
        public List<Node> nodes;                                                // lista cvorova

        // neophodni za ispis matrica u odvojenoj formi
        public resultsMatrices traversalForm;                                   // Dajkstra matrica puta            
        public resultsMatrices distanceForm;                                    // Dajkstra matrica rastojanja
        public double[,] warshallMatrix;                                        // Warshall matrica rastojanja
        public bool negativneGrane;                                             // dozvola unosa negativnih grana
        loginForm pf;

        public Grafovi(loginForm parent)
        {
            InitializeComponent();
            nodes = new List<Node>();
            username = parent.username;
            mod = mode.dodavanjeCvora;
            this.pf = parent;
            negativneGrane = false;
        }

        // dugme za novi graf
        private void addGraphBtn_Click(object sender, EventArgs e)
        {
            // ako postoji graf, pitamo se da li zelimo da ga izbrisemo i napravimo novi
            if (ucCanvas != null)
            {
                var result = MessageBox.Show("Da li ste sigurni da želite da napravite novi graf?", "Potvrda", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    newDrawingCanvas();
                }
             // ako ne postoji onda samo pravimo novi graf
            } else {
                ucCanvas = new Platno(this);
                Platno.nodes = nodes;
                ucCanvas.Parent = graphCanvasContainer;
                ucCanvas.Show();
            }

            tezineToggle.Checked = false;
        }

        // menjanje moda za izmenu tezina, un-select-ujemo sve node-ove koji su selektovani i invalidejtujemo formu
        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            mod = mod == mode.izmenaTezina ? mode.dodavanjeCvora : mode.izmenaTezina;
            
            foreach (var node in nodes)
                node.selected = false;

            if (ucCanvas == null) return;
            ucCanvas.reset_selected();
            ucCanvas.Invalidate();
        }

        // dugme za Dajkstrin algoritam
        private async void dijkstraBtn_Click(object sender, EventArgs e)
        {
            var dijkstraObj = new Dijkstra(this);
            ucCanvas.reset_colors();                                        // stavljamo boje na default

            try
            {
                if (Platno.nodes.Any())                                     // ukoliko postoji makar jedan cvor Dajkstra moze da radi
                {
                    int pocetni;

                    while (true)
                    {
                        var nova = new cvorForma();
                        nova.ShowDialog();
                        if (nova.mNode == -1)
                            return;

                        pocetni = nova.mNode;
                        try
                        {
                            nodes.First(x => x.ID == pocetni);
                            break;
                        }
                        catch { MessageBox.Show("Izabrani čvor ne postoji. Birajte ponovo."); }
                    }

                    if (distanceForm != null)                                   // Gasimo vec postojece forme za put i rastojanje
                        distanceForm.Close();

                    if (traversalForm != null)
                        traversalForm.Close();
                                                                                // Pravimo nove forme sa odgovarajucim koordinatama
                    distanceForm = new resultsMatrices(dijkstraObj, Platno.numberOfNodes, 1, pocetni, this);
                    traversalForm = new resultsMatrices(dijkstraObj, Platno.numberOfNodes, 0, pocetni, this);

                    distanceForm.StartPosition = FormStartPosition.Manual;
                    traversalForm.StartPosition = FormStartPosition.Manual;

                    distanceForm.Location = new Point(50, 0);
                    traversalForm.Location = new Point(50, distanceForm.Height);

                    traversalForm.Show();
                    distanceForm.Show();

                    controlUI(false);                                                          // Kada pocinje da radi zabranimo koriscenje svih dugmadi i ostalog
                    var y = await Task.Run(() => dijkstraObj.dijkstraAlgorithm(pocetni));      // Cekamo da se algoritam zavrsi i ponovo dozvoljavamo koriscenje interfejsa
                    controlUI(y);
                }
                else { throw new Exception(); }
            } catch {
                MessageBox.Show("Graf je prazan.");                                             // Ukoliko je graf prazan ispisujemo poruku
            }
        }

        // Metoda za pravljenje novog platna za graf, brisemo sve kontrole, cvorove i pravi se novi graf ciji je roditelj panel graphCanvasContainer
        private void newDrawingCanvas()
        {
            graphCanvasContainer.Controls.Clear();

            if (ucCanvas != null)
            {
                ucCanvas.Dispose();
                nodes.RemoveAll(x => x.Position != null);
            }

            ucCanvas = new Platno(this);
            Platno.nodes = nodes;

            ucCanvas.Parent = graphCanvasContainer;
            ucCanvas.Show();
        }

        // Funkcija za dozvolu koriscenja korisnickog interfejsa
        private void controlUI(bool status)
        {
            dijkstraBtn.Enabled = status;
            speedTrckBr.Enabled = status;
            addGraphBtn.Enabled = status;
            tezineToggle.Enabled = status;
            negativneGraneToggle.Enabled = status;
            floydBtn.Enabled = status;
            prikazPutaBtn.Enabled = status;
            prikazSredisteBtn.Enabled = status;
        }
        // ,,Osvezavamo'' comboboxes novim vrednostima 
        // CBs sluze za izbor prikaza najkraceg puta izmedju dva cvora koja se odaberu
        public void refreshComboBoxes()
        {
            prviCvorCb.Items.Clear();
            drugiCvorCb.Items.Clear();

            foreach (var node in Platno.nodes)
            {
                prviCvorCb.Items.Add(node.ID);
                drugiCvorCb.Items.Add(node.ID);
            }

            if (Platno.nodes.Count > 0)
            { 
                prviCvorCb.SelectedIndex = 0;
                drugiCvorCb.SelectedIndex = 0;
            }
            else
            {
                prviCvorCb.SelectedIndex = -1;
                drugiCvorCb.SelectedIndex = -1;
            }
        }

        // Dugme za Floyd Warshallov algoritam
        private void floydBtn_Click(object sender, EventArgs e)
        {
            ucCanvas.reset_colors();                                                    // Resetujemo boje 
            var noNodes = Platno.numberOfNodes;                                         // Makar dva cvora neophodna za pocetak rada
            if (noNodes < 2)
            {
                MessageBox.Show("Ubacite makar dva čvora, zatim pokušajte ponovo.");
                return;
            }

            warshallMatrix = new double[noNodes, noNodes];                              // Matrica rastojanja
            double[,] path = new double[noNodes, noNodes];                              // Matrica puta

            // Inicijalizacija tezinske matrice i matrice puta
            foreach (var i in nodes)
                foreach (var j in nodes)
                {
                    var vr = i.weights.Find(x => x.Key == j.ID).Value;
                    if (i == j)
                        warshallMatrix[i.ID, j.ID] = 0;
                    else if (vr == 0)
                        warshallMatrix[i.ID, j.ID] = double.PositiveInfinity;
                    else
                        warshallMatrix[i.ID, j.ID] = vr;

                    if (i.Connected.Any(a => a == j.ID))
                        path[i.ID, j.ID] = j.ID;
                    else
                        path[i.ID, j.ID] = double.PositiveInfinity;
                }


            for (int i = 0; i < noNodes; i++)
                path[i, i] = i;

            for (var i = 0; i < noNodes; i++)                                                           // Novi cvor preko koga se ide
                for (var j = 0; j < noNodes; j++)                                                       // sledece dve petlje prolaze kroz sve puteve izmedju cvorova
                    for (var k = 0; k < noNodes; k++)
                        if (warshallMatrix[j, k] > warshallMatrix[j, i] + warshallMatrix[i, k])         // pitamo se da li je kraci put preko novog cvora 
                        {
                            warshallMatrix[j, k] = warshallMatrix[j, i] + warshallMatrix[i, k];         // ako jeste, on postaje novi put (distance)
                            path[j, k] = path[j, i];                                                    // dodajemo u putanju odgovarajuci cvor
                        }

            // Ako je ovu funkciju pozvalo dugme za Floyd-ov algoritam ispisujemo matricu rastojanja
            if (sender == floydBtn)
            {
                var resForm = new resultsMatrices(null, noNodes, 2, -1, this);
                resForm.Show();
            }
            else if (sender == prikazSredisteBtn)               // ukoliko ju je pozvalo dugme za srediste radimo sledece
            {
                var rowsMaxes = new List<double>(noNodes);      // lista maksimalnih vrednosti potrebna za odredjivanje sredista grafa

                for (var i = 0; i < noNodes; i++)
                {
                    // Ukoliko je rastojanje bilo plus beskonacno pretvaramo ga u minus beskonacno i dodajemo u listu, u suprotnom samo dodajemo pocetne vrednosti u listu
                    if (warshallMatrix[i, 0] == double.PositiveInfinity)
                    {
                        rowsMaxes.Add(double.NegativeInfinity);
                    }
                    else
                        rowsMaxes.Add(warshallMatrix[i, 0]);

                    // proveravamo da li u ostatku redova matrice postoji neka vrednost veca od odgovarajuce vrednosti u listi maksimalnih
                    // za odgovarajucu vrstu, ukoliko postoji onda ona postaje nova maksimalna vrednost
                    for (var j = 1; j < noNodes; j++)
                    {
                        if (warshallMatrix[i, j] > rowsMaxes[i] && warshallMatrix[i,j] != double.PositiveInfinity)
                            rowsMaxes[i] = warshallMatrix[i, j];
                    }
                }
                
                // inicijalizujemo minimalnu vrednost na prvi element liste
                var minValue = rowsMaxes[0];
                // ukoliko je slucajno prva vrednost bila -beskonacno trazimo prvu koja nije to i nju postavljamo kao minimalnu liste
                for (var i = 1; i < noNodes; i++)
                    if (rowsMaxes[i] != double.NegativeInfinity)
                    {
                        minValue = rowsMaxes[i];
                        break;
                    }

                // ako postoji vise sredista
                var sredistaLista = new List<int>();

                // gledamo da li postoji manja vrednost od nase prethodne, inicijalizovane
                for (var i = 0; i < noNodes; i++)
                {
                    if (rowsMaxes[i] == double.NegativeInfinity)
                        continue;

                    if (rowsMaxes[i] < minValue)
                        minValue = rowsMaxes[i];

                }

                StringBuilder sb = new StringBuilder();                         // pravimo poruku o sredistima grafa
                for (var i = 0; i < noNodes; i++)
                    if (rowsMaxes[i] == minValue)                               // sredista su ako imaju minimalnu vrednost u nizu maksimalnih vrednosti svake vrste
                    {
                        ucCanvas.changeNodeColor(i, Color.Goldenrod, 0);        // menjamo im boju i dodajemo u poruku
                        sb.Append(" " + i.ToString());
                    }

                MessageBox.Show("Središte grafa je čvor " + sb.ToString());     // ispis poruke
            }
            else                                                                // Ukoliko je funkcija pozvana od strane dugmeta za prikaz puta
            {
                var start = int.Parse(prviCvorCb.SelectedItem.ToString());
                var end = int.Parse(drugiCvorCb.SelectedItem.ToString());
                var travel = new List<int>();
                var noWay = false;

                if (start == end) { MessageBox.Show("Ne postoji najkraći put od čvora do samog sebe."); return; }

                // Prolazimo kroz matricu puta i u niz travel koji predstavlja ,,put'' od cvora start do end
                // dodajemo sve indekse ostalih cvorova preko kojih idemo
                travel.Add(start);
                while (start != end)
                {
                    if (path[start, end] == double.PositiveInfinity)
                    {
                        noWay = true; break;                                    // ukoliko naidjemo na +beskonacno ne postoji put od start do end cvora
                    }
                    start = (int)path[start, end];
                    travel.Add(start);
                }

                // ukoliko je put veci od jedan i postoji put od cvora do cvora promenicemo boje izmedju njih u suprotnom ne postoji put
                if (travel.Count != 1 && noWay == false)
                {
                    int i;
                    for (i = 0; i < travel.Count - 1; i++)
                    { 
                        ucCanvas.changeConnectionColor(travel[i], travel[i + 1], Color.Orange, 0);
                    }
                    ucCanvas.changeConnectionColor(travel[i-1], end, Color.Orange, 0);
                }
                else if (noWay == true)
                {
                    MessageBox.Show("Ne postoji put između odabranih čvorova."); return;
                }
            }
        }

        // toggle za negativne tezine u grafu
        private void floydGraphToggle_CheckedChanged(object sender, EventArgs e)
        {
            // ukoliko je pritisnut toggle i prethodno nisu bile dozvoljene grane proveravamo da li je korisnik siguran da zeli da 
            // ih dozvoli jer se samom dozvolom pravi novi graf i podaci ce biti izgubljeni
            // a ako platno ne postoji onda imamo slobodu da menjamo kako nam odgovara
            if (negativneGraneToggle.Checked == true && negativneGrane == false)
            {
                if (ucCanvas != null)
                {
                    var result = MessageBox.Show("Da li ste sigurni da želite da dozvolite negativne težine grana?\n (Postojeći graf će biti izbrisan)", "Potvrda", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        this.negativneGrane = true;
                        this.dijkstraBtn.Enabled = false;
                        newDrawingCanvas();
                    }
                    else
                    {
                        negativneGraneToggle.Checked = false;                           // Resetujemo toggle ukoliko smo odlucili da ne zelimo da dozvolimo tezine negativne
                    }
                }
                else
                {
                    this.negativneGrane = true;
                }
            }
            else if (negativneGraneToggle.Checked == false && negativneGrane == true)       // isto kao i za prethodni slucaj, samo inverzno
            {
                if (ucCanvas != null)
                {
                    var result = MessageBox.Show("Da li ste sigurni da želite da zabranite negativne težine grana?\n (Postojeći graf će biti izbrisan)", "Potvrda", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        newDrawingCanvas();
                        this.dijkstraBtn.Enabled = true;
                        this.negativneGrane = false;
                    }
                    else
                    { 
                        negativneGraneToggle.Checked = true;
                    }
                }
                else
                {
                    this.negativneGrane = false;
                }
            }
        }

        // Prikaz korisnickog uputstva za sami program
        private void korisničkoUputstvoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var manForm = new Manual(who.program);
            manForm.Show();
        }

        // Prikaz korisnickog uputstva za algoritme
        private void opisAlgoritamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var manForm = new Manual(who.algoritmi);
            manForm.Show();
        }

        // o nama :D
        private void oNamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by: Anes Hujević \n Feel free to contact me for any ideas/suggestions :) \n e-mail -> anes1996_h@hotmail.com");
        }

        private void Grafovi_FormClosing(object sender, FormClosingEventArgs e)
        {
            pf.Close();
        }

        // Čuvanje podataka u bazi dijalog
        private void sačuvajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ucCanvas == null)
            {
                MessageBox.Show("Pokušavate sačuvati nepostojeći graf, napravite novi graf pa pokušajte ponovo..");
                return;
            }
            DialogResult result = MessageBox.Show("Da li ste sigurni da želite da sačuvate graf?", "Potvrda", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            writeToDB();
            MessageBox.Show("Uspešno sačuvan graf!");
        }

        // Čitanje iz baze podataka dijalog
        private void učitajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Da li ste sigurni da želite da učitate graf?", "Potvrda", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            readFromDB();
            ucCanvas.Invalidate();
        }

        // Čitanje iz baze podataka
        private void readFromDB()
        {
            newDrawingCanvas();
            grafoviEntities gfv = new grafoviEntities();

            foreach (var dbNode in gfv.cvor)
            {
                if (dbNode.username != username)
                    continue; 

                Node nodic = new Node(new Point((int)dbNode.posX, (int)dbNode.posY), Color.Black,dbNode.id);
                nodes.Add(nodic);
                ucCanvas.nodeNumber++;
                Platno.numberOfNodes++;
            }

            foreach (var node in nodes)
            {
                foreach (var x in gfv.konekcije)
                {
                    if (x.startCvor == node.ID && x.username == username)
                    {
                        node.Connected.Add(x.endCvor);
                        node.weights.Add(new KeyValuePair<int, double>(x.endCvor, x.tezina));
                        node.connectionColors.Add(new KeyValuePair<int, Color>(x.endCvor, Color.ForestGreen));
                    }
                }
            }
        }

        private void writeToDB()
        {
            grafoviEntities gfv = new grafoviEntities();

            // Pravimo novi čvor koji dodajemo u bazu
            foreach (Node node in nodes)
            {
                // Brišemo postojeće vrste u bazi 
                // da ne bi ostali za sledeći put kada budemo učitavali iz baze
                cvor postojeci = gfv.cvor.Find(username, node.ID);
                foreach (var k in gfv.konekcije)
                    if (k.username == username)
                        gfv.konekcije.Remove(k);
                if (postojeci != null)
                    gfv.cvor.Remove(postojeci);

                // Pravimo novi čvor i dodajemo ga u bazu
                cvor dbNode = new cvor();
                dbNode.username = username;
                dbNode.id = node.ID;
                dbNode.posX = node.Position.X;
                dbNode.posY = node.Position.Y;
                gfv.cvor.Add(dbNode);

                // Čuvamo sve konekcije od datog čvora ka ostalima u bazu
                foreach (int idTo in node.Connected)
                {    
                    konekcije konekcija = new konekcije();
                    konekcija.username = username;
                    konekcija.startCvor = node.ID;
                    konekcija.endCvor = idTo;
                    konekcija.tezina = node.weights.Find(x => x.Key == idTo).Value;
                    gfv.konekcije.Add(konekcija);
                }
            }

            // Potvrđujemo promene u bazi
            gfv.SaveChanges();
        }
    }
}
