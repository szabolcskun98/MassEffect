using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Effect
{
    class Mission
    {
        string name;
        string details;
        int credit;
        int riskLevel;

        public Mission(string name, string details, int credit, int riskLevel)
        {
            this.name = name;
            this.details = details;
            this.credit = credit;
            this.riskLevel = riskLevel;
        }

        public string Name { get => name; set => name = value; }
        public string Details { get => details; set => details = value; }
        public int Credit { get => credit; set => credit = value; }
        public int RiskLevel { get => riskLevel; set => riskLevel = value; }

        public int Weight
        {
            get
            {
                return credit / riskLevel;
            }
        }
    }
}
d