using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASP_grafovi
{
    public partial class resultsMatrices : MetroForm
    {
        private int numberOfNodes;                                      // broj cvorova
        private int sizeOfCell;                                         // velicina celije matrice
        private Dijkstra dijkstraObj;                                   // instanca Dajkstra objekta
        private int indicatorResult;                                    // da li ispisujemo matricu puta ili rastojanja Dajkstra
                                                                        // ili iscrtavamo floyd-warshallovu matricu 
                                                                        // redom oznaceni matrica put,matrica  rastojanja, fw matrica
        private int startNode;                                          // pocetni cvor
        private Grafovi parentForm;

        public resultsMatrices(Dijkstra dijkstraObj, int numberOfNodes, int indicator, int startNode, Grafovi PF)
        {
            InitializeComponent();

            this.parentForm = PF;
            this.numberOfNodes = numberOfNodes;
            this.indicatorResult = indicator;
            this.dijkstraObj = dijkstraObj;
            this.startNode = startNode;
            sizeOfCell = 40;

            // odgovarajuca velicina forme i minimalna velicina forme
            ClientSize = new Size(sizeOfCell * (numberOfNodes + 1), sizeOfCell * (numberOfNodes + 4));
            MinimumSize = new Size(200, 200);
        }


        private void resultPnl_Paint(object sender, PaintEventArgs e)
        {
            if (numberOfNodes == 1) return;

            // ukoliko je indicator result = 2 onda je ova forma za warshallovu matricu
            if (indicatorResult == 2)
            {
                warshallDraw(sender, e);
                return;
            }

            bool once = false;                               // potrebno za naslov forme

            for (var col = 0; col < numberOfNodes; col++)
            { 
                for (var row = 0; row < numberOfNodes + 2; row++)
                {
                    string stringOut = " ";
                    if (row > 1  && row <= numberOfNodes + 1)
                    {
                        if (col > 0)                        // ukoliko su kolone vece od nula
                        {
                            double value;
                            if (col - 1 == startNode)       // menjamo odgovarajucu kolonu sa kolonom pocetnog cvora
                                value = indicatorResult == 1 ? dijkstraObj.distanceMatrix[row - 2, numberOfNodes - 1] : dijkstraObj.traversalMatrix[row - 2, numberOfNodes - 1];
                            else                            // u suprotnom pristupamo odgovarajucoj matrici rastojanja
                                value = indicatorResult == 1 ? dijkstraObj.distanceMatrix[row - 2, col - 1] : dijkstraObj.traversalMatrix[row - 2, col - 1];

                            // ukoliko je vrednost 0 ili beskonacno onda ispisujemo simbol za beskonacno
                            // u suprotnom ispisujemo broj
                            if (value == double.PositiveInfinity || value == 0 && indicatorResult == 1)
                                stringOut = "∞";
                            else
                                stringOut = Math.Floor(value).ToString();
                        }
                        else if (col == 0)                  // ukoliko smo u nultoj koloni ispisujemo cvorove po redu po kojem smo ih obilazili
                        {
                            try
                            { 
                                stringOut = dijkstraObj.traversalList[row - 2].ToString();
                            }
                            catch 
                            {
                                stringOut = "-"; 
                            };
                        }
                    }
                    else if (row == 0)                      // ukoliko smo u nultom redu ispisujemo naslov forme
                    {
                        if (once == false && col == (numberOfNodes - 1) / 2)
                        {
                            stringOut = indicatorResult == 1 ? "Distance" : "Traversal";
                            once = true;
                        }
                    }
                    else if (row == 1)                      // ukoliko smo u prvom redu ispisujemo odgovarajuce indekse kolona
                    {
                        if (col == 0)
                            stringOut = "";
                        else
                            stringOut = (col - 1 == startNode) ? (numberOfNodes - 1).ToString() : (col - 1).ToString();

                    }

                    e.Graphics.DrawString(
                                stringOut,
                                new Font("Arial", 16),
                                new SolidBrush(Color.Black),
                                new Point(sizeOfCell * col, sizeOfCell * row)
                                );
                }
            }

           // iscrtavamo linije za matricu
           e.Graphics.DrawLine(new Pen(Color.Black), new Point(0, sizeOfCell * 2), new Point(sizeOfCell * numberOfNodes, sizeOfCell * 2));
           e.Graphics.DrawLine(new Pen(Color.Black), new Point(sizeOfCell, sizeOfCell), new Point(sizeOfCell, sizeOfCell * (numberOfNodes + 2)));
        }

        // za iscrtavanje warshall matrice rastojanja
        // na odgovarajuci nacin rasporedjujemo elemente na slican nacin kao i pre
        private void warshallDraw(object sender, PaintEventArgs e)
        {
            this.Text = "Floyd - Warshall distance";
            this.MinimumSize = new Size(325, 200);
            var strOut = " ";

            for (var col = 0; col < numberOfNodes + 1; col++)
            {
                strOut = " ";
                for (var row = 0; row < numberOfNodes + 1; row++)
                {
                    if (row == 0)
                    {
                        if (col > 0)
                            strOut = (col - 1).ToString();
                    }
                    else if (col == 0)
                    {
                        strOut = (row - 1).ToString();
                    }
                    else
                        strOut = parentForm.warshallMatrix[row - 1, col - 1] == double.PositiveInfinity ? "∞" : Math.Floor(parentForm.warshallMatrix[row - 1, col - 1]).ToString();

                    e.Graphics.DrawString(
                        strOut,
                        new Font("Arial", 16),
                        new SolidBrush(Color.Black),
                        new Point(sizeOfCell * col, sizeOfCell * row)
                    );
                }
            }

            e.Graphics.DrawLine(new Pen(Color.Black), new Point(0, sizeOfCell), new Point(sizeOfCell * (numberOfNodes + 1), sizeOfCell));
            e.Graphics.DrawLine(new Pen(Color.Black), new Point(sizeOfCell, 0), new Point(sizeOfCell, sizeOfCell * (numberOfNodes + 1)));
            this.Invalidate();
        }

        // ponovno iscrtavanje panela
        public void newResults()
        {
            this.resultPnl.Invalidate();
        }
    }
}
