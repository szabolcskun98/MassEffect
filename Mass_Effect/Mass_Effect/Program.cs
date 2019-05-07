using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Effect
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();

            g.Start();


            Console.WriteLine(g.Starsystem.ToString());

            Console.WriteLine(g.BestPath("Wolf 359", 20));

            Console.ReadKey();
        }
    }
}
