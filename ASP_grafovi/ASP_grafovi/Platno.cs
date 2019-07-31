using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ASP_grafovi
{
    // platno za iscrtavanje grafa
    public partial class Platno : UserControl
    {
        public static List<Node> nodes;                         // lista cvorova
        public static int numberOfNodes;                        // broj cvorova
        public int nodeNumber;                                         // broj cvora
        int selectedID;                                         // id selektovanog cvora
        short selected;                                         // broj selektovanih
        private Grafovi parentForm;                             // roditeljska forma grafovi

        // pocetna inicijalizacija
        public Platno(Grafovi parentForm)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.AliceBlue;
            nodeNumber = 0;
            numberOfNodes = 0;
            this.parentForm = parentForm;
            reset_selected();
        }

        // f-ja za iscrtavanje platna
        private void Platno_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.AliceBlue);                      // boja pozadine
            foreach (var node in nodes)                             // iscrtavanje svih cvorova
            {
                node.drawNode(e);
                node.fillNode(e, null);
            }
        }

        private void Platno_MouseUp(object sender, MouseEventArgs e)
        {
            bool dodaj = true;                                  // pocetna inicijalizacija za dodavanje cvora

            foreach (var node in nodes)
            {
                // provera da li pokusavamo da napravimo cvor ,,preko'' cvora ili selektujemo cvor
                if (Math.Sqrt(Math.Pow(e.Location.X - node.Position.X, 2) + Math.Pow(node.Position.Y - e.Location.Y, 2)) <= 37)
                {
                    dodaj = false;
                    if (e.Button == MouseButtons.Right && Grafovi.mod == mode.dodavanjeCvora)   // ukoliko selektujemo cvor
                    {                                                                           // naznacavamo da je odabran
                        node.selected = !node.selected;
                        selected++;                                                             // i inkrementiramo broj selektovanih

                        if (selected == 2 && selectedID != node.ID)                             // ukoliko je vec selektovan cvor i nije izabran isti kao prvi
                        {
                            var sel = nodes.First(x => x.ID == selectedID);                     // vrsimo konekciju odgovarajucih cvorova
                            sel.connect(node.ID);                                               // i un-select-ujemo ih
                            node.selected = false;
                            sel.selected = false;
                            reset_selected();
                        }
                        else if (selected == 1)
                        {
                            selectedID = node.ID;                                               // ukoliko je ovo prvi cvor koji se selektujemo dajemo mu naznaku da je selektovan
                        }
                        else if (selected >= 2)                                                 // selektovano vise od dva resetujemo sve
                        {
                            nodes.First(x => x.ID == selectedID).selected = false;
                            node.selected = false;
                            reset_selected();                                                   // jer se tezina menja samo izmedju dva selektovana cvora
                        }

                    }
                    else if (e.Button == MouseButtons.Right && Grafovi.mod == mode.izmenaTezina)    // ukoliko smo u modu za menjanje tezina
                    {
                        node.selected = !node.selected;
                        selected++;

                        if (selected == 2 && selectedID != node.ID)                                 // i izabrana su dva razl. cvora
                        {
                            try
                            {
                                var form = new tezinaForma(this.parentForm);                        // kreiramo novu formu za upit nove vrednosti grane izmedju dva odabrana cvora
                                var nodic = nodes.Find(x => x.ID == selectedID);                    // proveravamo da li su povezani cvorovi
                                nodic.Connected.First(x => x == node.ID);

                                form.ShowDialog();

                                var tezinaGrane = tezinaForma.mWeight;                              // uzimamo dobijenu vrednost od forme
                                if (tezinaGrane == 0)
                                {
                                    node.selected = false;
                                    nodes.First(x => x.ID == selectedID).selected = false;
                                    reset_selected();
                                    return;
                                }

                                // proveravamo da li je tezina pozitivna ili su nam dozvoljene negativne tezine 
                                if (tezinaGrane >= 0 || (parentForm.negativneGrane == true))
                                {
                                    // pokusavamo da izmenimo za dvosmernu vezu ako ne uspe pokusavamo pojedinacno, za usmerene veze
                                    try
                                    {
                                        node.getWeight(selectedID);
                                        node.setWeight(selectedID, tezinaForma.mWeight);

                                        nodic.getWeight(node.ID);
                                        nodic.setWeight(node.ID, tezinaForma.mWeight);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            node.getWeight(selectedID);
                                            node.setWeight(selectedID, tezinaForma.mWeight);
                                        }
                                        catch { }

                                        try
                                        {
                                            nodic.getWeight(node.ID);
                                            nodic.setWeight(node.ID, tezinaForma.mWeight);
                                        }
                                        catch { }
                                    }

                                }

                                // resetujemo selekcije na default
                                node.selected = false;
                                nodic.selected = false;
                                reset_selected();
                            }
                            catch
                            {
                                // ukoliko nije postojala veza izmedju dva cvora ispis i resetovanje selektovanih
                                MessageBox.Show("Ne postoji usmerena veza između dva odabrana čvora.");
                                node.selected = false;
                                nodes.Find(x => x.ID == selectedID).selected = false;
                                reset_selected();
                            }
                        }
                        else if (selected == 1)                                         // ukoliko je samo jedan selektovan naznacavamo
                        {
                            selectedID = node.ID;
                        }
                        else if (selected >= 2)                                         // selektovano vise od dva onda selektujemo ponovo
                        {
                            nodes.First(x => x.ID == selectedID).selected = false;
                            node.selected = false;
                            reset_selected();
                        }
                    }
                }
                this.Invalidate();
            }

            // ukoliko treba da dodamo node, dodajemo ga (nije izmenjen u prvom uslovu if-a  u prethodnom grananju)
            if (dodaj)
            {
                nodes.Add(new Node(e.Location, Color.Black, nodeNumber++));
                numberOfNodes++;
                this.Invalidate();
            }

            parentForm.refreshComboBoxes();
        }

        // resetujemo selekcije
        public void reset_selected()
        {
            selected = 0;
            selectedID = 0;
        }

        // f-ja za brisanje cvora 
        // ukoliko je pritisnut delete i izabran je jedan cvor 
        // smanjujemo broj cvorova, brisemo postojeci cvor 
        // i sve preostale cvorove koji imaju indeks veci od njega smanjujemo za jedan
        private void Platno_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selected == 1)
            {
                try
                {
                    var nodic = nodes.First(x => x.ID == selectedID);
                    nodic.toBeDeleted = true;

                    foreach (var x in nodes)
                        x.Connected.Remove(selectedID);
                    nodes.Remove(nodic);

                    numberOfNodes--;
                    nodeNumber--;
                    this.Invalidate();

                    foreach (var node in nodes.Where(x => x.ID > selectedID))
                        swapNodes(node.ID - 1, node.ID);

                    parentForm.refreshComboBoxes();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
                finally
                {
                    reset_selected();
                }
            }
        }

        // menjanje boja konekcije
        // prima dva ID od dva cvora, novu boju veze i vreme za cekanje
        // potrebno za graficki prikaz
        public void changeConnectionColor(int id1, int id2, Color iColor, int ms)
        {
            var firstNode = nodes.Find(x => x.ID == id1);
            var secondNode = nodes.Find(x => x.ID == id2);

            if (firstNode != null && secondNode != null)
            {
                bool cekaj = false; 

                if (firstNode.connectionColors.Any(x => x.Key == id2))
                {
                    firstNode.connectionColors.Remove(firstNode.connectionColors.Find(x => x.Key == id2));
                    firstNode.connectionColors.Add(new KeyValuePair<int, Color>(id2, iColor));
                    cekaj = true;
                }
                
                if (cekaj)
                    Thread.Sleep(ms);
            }
            else
                return;

            this.Invalidate();
        }

        // funkcija za zamenu cvorova medjusobno i u svim ostalim konekcijama gde postoje
        public void swapNodes(int newID, int oldID)
        {
            var newNode = nodes.First(x => x.ID == oldID);
            newNode.ID = newID;
            newNode.toBeDeleted = false;

            foreach (var x in nodes)
            {
                if (x.connectionColors.Any(y => y.Key == oldID))
                {
                    var color = x.connectionColors.Find(y => y.Key == oldID).Value;
                    x.connectionColors.Remove(x.connectionColors.First(y => y.Key == oldID));
                    if (x.connectionColors.Any(y => y.Key == newID))
                        x.connectionColors.Remove(x.connectionColors.First(y => y.Key == newID));
                    x.connectionColors.Add(new KeyValuePair<int, Color>(newID, color));
                }

                if (x.Connected.Any(y => y == oldID))
                {
                    x.Connected.Remove(oldID);
                    x.Connected.Add(newID);
                }

                if (x.weights.Any(y => y.Key == oldID))
                {
                    var weight = x.weights.First(y => y.Key == oldID).Value;
                    x.weights.Remove(x.weights.First(y => y.Key == oldID));
                    if(x.weights.Any(y => y.Key == newID))
                        x.weights.Remove(x.weights.First(y => y.Key == newID));
                    x.weights.Add(new KeyValuePair<int, double>(newID, weight));
                }

                this.Invalidate();
            }
        }

        // menjanje boje cvora
        public void changeNodeColor(int id, Color iColor, int ms)
        {
            var node = nodes.Find(x => x.ID == id);
            node.Color = iColor;
            Thread.Sleep(ms);
            this.Invalidate();
        }

        // resetovanje boja na default boje
        // pomocu reda
        // jer je key-value pair immutable 
        public void reset_colors()
        {
            foreach (var node in nodes)
            {
                changeNodeColor(node.ID, Color.Black, 0);

                Queue<int> helpQueue = new Queue<int>();

                foreach (var connection in node.connectionColors)
                    helpQueue.Enqueue(connection.Key);

                foreach (var smth in helpQueue)
                    node.connectionColors.Remove(node.connectionColors.First(x => x.Key == smth));

                while (helpQueue.Count != 0)
                    node.connectionColors.Add(new KeyValuePair<int, Color>(helpQueue.Dequeue(), Color.ForestGreen));

                this.Invalidate();
            }
        }
    }
}
