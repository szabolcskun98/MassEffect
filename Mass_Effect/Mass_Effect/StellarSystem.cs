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
        public SingleLinkedList<Mission> Missions { get => missions; set => missions = value; }

        public (string, Mission, double) BestMission
        {
            get
            {
                Mission m = missions.Where(x => x.Weight == missions.Max(y => y.Weight)).First();
                return (name, m, m.Weight);
            }
        }

        public int LenghtOfMissions()
        {
            return Missions.Count();
        }

        public double SumOfWeight()
        {
            return Missions.Sum(x => x.Weight);
        }

        public override string ToString()
        {
            return $"{{name: {name}, missions:{missions.ToString()}}}";
        }
    }
}
