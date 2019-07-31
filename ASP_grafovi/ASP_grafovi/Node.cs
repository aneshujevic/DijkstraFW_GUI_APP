using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASP_grafovi
{
    public class Node                                                   // klasa koja predstavlja jedan cvor
    {
        private List<int> connectedTo;                                  // lista id-eva sa kojim je cvor povezan
        public List<KeyValuePair<int,double>> weights;                  // lista key-value pair-ova sa odgovarajucim id:tezina
        public List<KeyValuePair<int, Color>> connectionColors;         // isto kao prethodno samo za boje
        private Point position;                                         // pozicija na ,,platnu''
        private Color color;                                            // boja cvora
        private int id;                                                 // id cvora
        public bool selected;                                           // da li je selektovan
        public bool toBeDeleted;                                        // da li je za brisanje
        
        // inicijalizacija pomocu parametarskog konstruktora
        public Node(Point pos, Color col, int id)
        {
            position = pos;
            color = col;
            this.id = id;
            connectedTo = new List<int>();
            weights = new List<KeyValuePair<int, double>>();
            connectionColors = new List<KeyValuePair<int, Color>>();
            selected = false;
            toBeDeleted = false;
        }

        // setteri i getteri za tezinu
        public double getWeight(int key)
        {
            return weights.First(x => x.Key == key).Value;
        }
        
        public void setWeight(int key, int val)
        {
            KeyValuePair<int, double> kvp = new KeyValuePair<int, double>(key, val);
            weights.Remove(weights.First(x => x.Key == key));
            weights.Add(kvp);
        }

        // property za ID
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        // property za position
        public Point Position 
        {
            get { return position; }
            set { position = value; }
        }

        // property za listu connectedto
        public List<int> Connected
        {
            get { return connectedTo; }
        }

        // property za boje
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        // funkcija za konektovanje naseg cvora sa novim uz odgovarajuce provere
        public void connect (int newNodeID)
        {
            var connectedNode = Platno.nodes.First(y => y.ID == newNodeID);
            
            // ukoliko smo vec konektovani a pokusavamo opet znaci da zapravo 
            // pokusavamo da diskonektujemo nas cvor sa drugim cvorom
            // pa to ovde i radimo
            foreach (var x in Connected)
                if (newNodeID == x)
                {
                    Connected.Remove(newNodeID);
                    weights.Remove(weights.Find(k => k.Key == newNodeID));
                    connectionColors.Remove(connectionColors.Find(l => l.Key == newNodeID));
                    return;
                }
 
            // racunamo poziciju gde ce se prikazati tezina date grane i konektujemo sa tim cvorom nas cvor
            // ubacujemo i boje i tezinu
            double length = Math.Sqrt(Math.Pow(Position.X - connectedNode.Position.X, 2) + Math.Pow(Position.Y - connectedNode.Position.Y, 2)) / 8;
            Connected.Add(newNodeID);

            weights.Add(new KeyValuePair<int, double>(newNodeID, length));
            connectionColors.Add(new KeyValuePair<int, Color>(newNodeID, Color.ForestGreen));
        }

        // za iscrtavanje cvora
        public void drawNode(System.Windows.Forms.PaintEventArgs e)
        {
            // ako se cvor brise onda pozivamo funkciju za ,,iscrtavanje'' konekcija
            // koja ce u ovom slucaju da izbrise postojece konekcije graficki
            if (toBeDeleted == true) { this.drawConnection(e); return; }

            Pen pen = new Pen(this.Color);
            Rectangle rect = new Rectangle(this.Position.X - 20, this.Position.Y - 20, 40, 40);
            pen.Width = 4;

            drawConnection(e);
            e.Graphics.DrawEllipse(pen, rect);

            pen.Dispose();
        }

        // za iscrtavanje tezina
        public void drawWeights(System.Windows.Forms.PaintEventArgs e, Pen pen)
        {
            // za svaki konektovani cvor pretvaramo tezinu u integer i ukoliko je razlicita od nule
            // u zavisnosti od pozicije cvora stavljamo tezinu na odgovarajucu lokaciju graficki
            foreach (var connected in Connected)
            {
                var nodic = Platno.nodes.FirstOrDefault(x => x.ID == connected);

                int tezina = 0;
                foreach (var tezinica in weights)
                    if (tezinica.Key == connected)
                        tezina = (int)(tezinica.Value);

                if (tezina != 0)
                {
                    Point tackaTezina = new Point();
                    var yPocetna = Position.Y > nodic.Position.Y ? nodic.position.Y : Position.Y;
                    var xPocetna = Position.X > nodic.Position.X ? nodic.position.X : Position.X;
                    tackaTezina.X = xPocetna + Math.Abs(Position.X - nodic.Position.X) / 3;
                    tackaTezina.Y = yPocetna + Math.Abs(Position.Y - nodic.Position.Y) / 3;

                    e.Graphics.DrawString(
                        tezina.ToString(),
                        new Font("Cambria Bold", 20f),
                        new SolidBrush(Color.Red),
                        tackaTezina
                        );
                }
            }
        }

        // iscrtavanje konekcija cvora sa ostalim od pozicije trenutnog cvora do svih ostalih
        public void drawConnection(System.Windows.Forms.PaintEventArgs e)
        {
            Pen pen = null;
            foreach (var connected in Connected)
            {
                var boja = connectionColors.Find(x => x.Key == connected).Value;
                pen = new Pen(boja, 4);
                pen.CustomEndCap = new AdjustableArrowCap(4, 8);
                var nodic = Platno.nodes.FirstOrDefault(x => x.ID == connected);
                if (nodic == null) return;
                e.Graphics.DrawLine(pen, Position, nodic.Position);
                nodic.fillNode(e, null);
            }
            drawWeights(e, pen);
        }


        // ispunjavanje cvora bojom
        // ako se brise cvor onda ,,iscrtavamo'' bojom pozadine
        // ako je selektovan onda ,,iscrtavamo'' zelenom bojom
        // u suprotnom ,,iscrtavamo'' bojom koja mu je dodeljena
        public void fillNode(System.Windows.Forms.PaintEventArgs e, SolidBrush brushie)
        {
            if (toBeDeleted)
            {
                Rectangle rect = new Rectangle(this.position.X - 20, this.position.Y - 20, 40, 40);
                SolidBrush brush = brushie == null ? new SolidBrush(Color.AliceBlue) : brushie;
                e.Graphics.FillEllipse(brush, rect);
            }

            if (selected)
            {
                SolidBrush brush = brushie == null ? new SolidBrush(Color.ForestGreen) : brushie;
                Rectangle rect = new Rectangle(this.position.X - 20, this.position.Y - 20, 40, 40);
                e.Graphics.FillEllipse(brush, rect);
                brush.Dispose();
            }
            else 
            {
                SolidBrush brush = brushie == null ? new SolidBrush(this.Color) : brushie;
                Rectangle rect = new Rectangle(this.position.X - 20, this.position.Y - 20, 40, 40);
                e.Graphics.FillEllipse(brush, rect);
                brush.Dispose();
            } 

            // dajemo lokaciju za ispis id naseg trenutnog cvora
            Point p = ID > 9 ? new Point(Position.X - 18, Position.Y - 15) : new Point(Position.X - 10, Position.Y - 12);
            e.Graphics.DrawString(ID.ToString(), new Font("Times New Roman Bold", 20f), new SolidBrush(Color.White), p);
        }
    }
}
