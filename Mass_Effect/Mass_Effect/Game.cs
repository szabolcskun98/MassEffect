using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mass_Effect
{
    class Game
    {
        Graph<StellarSystem> starsystem;

        public Game()
        {
            this.starsystem = new Graph<StellarSystem>();
        }

        public Graph<StellarSystem> Starsystem { get => starsystem; set => starsystem = value; }

        public void Start()
        {
            UnsortedLinkedList<StellarSystem> ssCollection = new UnsortedLinkedList<StellarSystem>();
            try
            {
                using (StreamReader reader = new StreamReader(".\\Solution Items\\graph_dataset.csv"))
                {
                    string stelarsysTmp = "";
                    UnsortedLinkedList<Mission> l = new UnsortedLinkedList<Mission>();
                    while (!reader.EndOfStream)
                    {
                        string[] s = reader.ReadLine().Split('\t');
                        
                        if (stelarsysTmp.CompareTo(s[0]) != 0)
                        {
                            if (l.Count != 0)
                            {
                                StellarSystem ss = new StellarSystem(stelarsysTmp, l);
                                starsystem.AddNode(ss);
                                ssCollection.Add(ss);
                                l = new UnsortedLinkedList<Mission>();
                            }
                            stelarsysTmp = s[0];
                        }
                        Mission m = new Mission(s[1], s[2], int.Parse(s[3]), int.Parse(s[4]));
                        l.Add(m);

                        if (reader.EndOfStream)
                        {
                            StellarSystem ss = new StellarSystem(stelarsysTmp, l);
                            starsystem.AddNode(ss);
                            ssCollection.Add(ss);
                            l = new UnsortedLinkedList<Mission>();
                        }
                    }
                    reader.Close();
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error>: {ex.ToString()}");
            }

            try
            {
                using (StreamReader reader = new StreamReader(".\\Solution Items\\graph_connection.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] s = reader.ReadLine().Split('\t');
                        starsystem.AddEdge(ssCollection.Where(x => x.Name.CompareTo(s[0]) == 0).First(), ssCollection.Where(x => x.Name.CompareTo(s[1]) == 0).First());
                    }
                    reader.Close();
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error>: {ex.ToString()}");
            }

            //Console.WriteLine(starsystem.ToString());
        }
    }
}
