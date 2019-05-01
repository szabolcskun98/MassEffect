using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Effect
{
    class StellarSystem
    {
        string name;
        SingleLinkedList<Mission> missions;

        public StellarSystem(string name, SingleLinkedList<Mission> missions)
        {
            this.name = name;
            this.missions = missions;
        }

        public string Name { get => name; set => name = value; }
        internal SingleLinkedList<Mission> Missions { get => missions; set => missions = value; }

        public int LenghtOfMissions()
        {
            return Missions.Count();
        }

        public double SumOfWeight()
        {
            return Missions.Sum(x => x.Weight);
        }
    }
}
