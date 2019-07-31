using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ASP_grafovi
{
    public class Dijkstra
    {
        private double MAX = double.PositiveInfinity;           // maksimalna tezina
        private Platno ucCanvas;                                // platno za iscrtavanje
        private List<Node> nodes;                               // lista cvorova
        private List<int> notVisited;                           // lista nepovezanih cvorova
        private Dictionary<int, double> distance;               // id - razdaljina od pocetnog cvora do cvora sa id-em id
        private Dictionary<int, int> traversal;                 // id1 - id2 preko id2 se od pocetnog stize do id1
        private int animationSpeed;                             // brzina animacije
        private Grafovi parentMainForm;
        public List<int> traversalList;                         // lista cvorova u redosledu kojem ih obilazimo
        private double[,] weightMatrix;                         // tezinska matrica
        public double [,] distanceMatrix;                       // matrica rastojanja
        public double[,] traversalMatrix;                       // matrica puta

        // inicijalizacija preko parametarskog konstruktora
        public Dijkstra(Grafovi mainForm)
        {
            ucCanvas = mainForm.ucCanvas;
            nodes = mainForm.nodes;
            animationSpeed = mainForm.speedTrckBr.Value;
            parentMainForm = mainForm;
        }

        // Dajkstrin algoritam
        public bool dijkstraAlgorithm(int startNode)
        {
            cleanup();

            int milisecs = 1000 - animationSpeed;
            var noNodes = Platno.numberOfNodes;
            Color startNodeColor = Color.CadetBlue;                                 // boja pocetnog cvora
            Color currentNodeColor = Color.BlanchedAlmond;                          // boja trenutnog cvora
            Color visitedNodeColor = Color.DarkSlateGray;                           // boja posecenog cvora
            Color checkingConnectionColor = Color.Yellow;                           // boja veze koja se proverava
            Color acceptedConnectionColor = Color.YellowGreen;                      // boja veze koja je prihvacena
            Color deniedConnectionColor = Color.Red;                                // boja veze koja je odbijena

            // inicijalizacija odgovarajucih atributa
            traversalList = new List<int>(noNodes);
            notVisited = new List<int>(noNodes);
            distance = new Dictionary<int, double>(noNodes);
            traversal = new Dictionary<int, int>(noNodes);
            weightMatrix = new double[noNodes, noNodes];
            distanceMatrix = new double[noNodes, noNodes];
            traversalMatrix = new double[noNodes, noNodes];

            // POCETAK DAJKSTRINOG ALGORITMA

            // inicijalizacija odgovarajucih matrica i listi
            foreach (var node in nodes)
                notVisited.Add(node.ID);

            foreach (var i in nodes)
                foreach (var j in nodes)
                {
                    var vr = i.weights.Find(x => x.Key == j.ID).Value;
                    if (i == j || vr == 0)
                        weightMatrix[i.ID, j.ID] = MAX;
                    else
                        weightMatrix[i.ID, j.ID] = vr;
                }

            foreach (var node in nodes)
            {
                if (node.ID != startNode)
                {
                    distance.Add(node.ID, weightMatrix[startNode, node.ID]);
                    if (weightMatrix[startNode, node.ID] != MAX)
                        traversal.Add(node.ID, startNode);
                    else
                        traversal.Add(node.ID, -1);
                }
            }


            // provera puteva kod pocetnog cvora sa odgovarajucim bojenjem i popunjavanjem matrica
            startColoringThread(startNode, startNodeColor, milisecs);
            foreach (var some in nodes.First(x => x.ID == startNode).Connected.OrderBy(x => weightMatrix[0, x]))
            {
                startColoringThread(startNode, some, checkingConnectionColor, milisecs);
                startColoringThread(startNode, some, acceptedConnectionColor, milisecs);
                fillBufferMatrices(0, noNodes);
                refreshResultsMatrices();
            }
            fillBufferMatrices(0, noNodes);                                         // ukoliko je samo jedan cvor :)
            traversalList.Add(startNode);
            refreshResultsMatrices();
            notVisited.Remove(startNode);

            for (var k = 0; k < noNodes - 1; k++)
            {
                int currentNodeIndex = notVisited.First();
                
                double value = distance[currentNodeIndex];
                foreach (var item in notVisited)
                    if (distance[item] < value)
                    {
                        value = distance[item];
                        currentNodeIndex = item;
                    }

                
                if (distance[currentNodeIndex] == MAX)
                {
                    traversalList.Add(currentNodeIndex);
                    fillBufferMatrices(k + 1, noNodes);
                    refreshResultsMatrices();
                    notVisited.Remove(currentNodeIndex);
                    startColoringThread(currentNodeIndex, currentNodeColor, milisecs);
                    startColoringThread(currentNodeIndex, visitedNodeColor, milisecs);
                    continue;                                                                       // break u originalnom Dijkstra,
                }                                                                                   // ovde continue zbog ilustracije

                traversalList.Add(currentNodeIndex);
                refreshResultsMatrices();
                notVisited.Remove(currentNodeIndex);

                startColoringThread(currentNodeIndex, currentNodeColor, milisecs);
                foreach (var other in notVisited)
                {
                    Color finalColor;
                    startColoringThread(currentNodeIndex, other, checkingConnectionColor, milisecs);

                    if (distance[currentNodeIndex] + weightMatrix[currentNodeIndex, other] < distance[other])
                    {
                        distance[other] = distance[currentNodeIndex] + weightMatrix[currentNodeIndex, other];
                        traversal[other] = currentNodeIndex ;

                        finalColor = acceptedConnectionColor;

                        foreach (var node in nodes.Where(x => x.Connected.Any(y => y == other)))
                            if (node.ID != currentNodeIndex)
                                startColoringThread(node.ID, other, deniedConnectionColor, milisecs);
                    }
                    else
                    {
                        finalColor = deniedConnectionColor;
                    }

                    startColoringThread(currentNodeIndex, other, finalColor, milisecs);
                }

                fillBufferMatrices(k + 1, noNodes);
                refreshResultsMatrices();
                startColoringThread(currentNodeIndex, visitedNodeColor, milisecs);
            }
            return true;                                                // potrebno za dozvolu koriscenje korisnickog interfejsa
        }
        
        // popunjavamo matrice koje sluza kao buffer za forme za prikaz rezultata
        void fillBufferMatrices(int row, int columns)
        {
            for (var i = 0; i < columns; i++)
            {
                distanceMatrix[row, i] = distance.ContainsKey(i) ? distance[i] : MAX;
                traversalMatrix[row, i] = traversal.ContainsKey(i) ? traversal[i] : -1;
            }
        }
 
        // pozivamo funkciju da prikaze nove rezultate na odgovarajucim formama za rezultate
        void refreshResultsMatrices()
        {
            this.parentMainForm.distanceForm.newResults();
            this.parentMainForm.traversalForm.newResults();
        }

        // pokrecemo niti za menjanje boje cvora
        // radi se preko niti da bismo mogli da ih ,,uspavamo'' na odredjeno vreme
        void startColoringThread(int first, int second, Color color, int ms)
        {
            Thread coloringThread = new Thread(() => changeConnectionColor(first, second, color, ms));
            coloringThread.Start();
            coloringThread.Join();
        }

        // pokrecemo niti za menjanje boje cvora
        // radi se preko niti da bismo mogli da ih ,,uspavamo'' na odredjeno vreme
        void startColoringThread(int nodeID, Color color, int ms)
        {
            Thread coloringThread = new Thread(() => changeNodeColor(nodeID, color, ms));
            coloringThread.Start();
            coloringThread.Join();
        }

        // menjamo boju datog cvora
        void changeNodeColor(int index, Color color, int ms)
        {
            ucCanvas.changeNodeColor(index, color, ms);
        }

        // menjamo boju konekcije izmedju datih cvorova
        void changeConnectionColor(int id0, int id1, Color color, int ms)
        {
            ucCanvas.changeConnectionColor(id0, id1, color, ms);
        }
        
        // brisemo vec postojece resurse
        public void cleanup()
        {
            notVisited = null;
            traversal = null;
            traversalList = null;
            weightMatrix = null;
            distance = null;
            distanceMatrix = null;
            traversalMatrix = null;
        }
    }
}
