using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemScraper.Classes
{
    class Weapon
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Damage { get; set; }
        public string APS { get; set; }
        public string DPS { get; set; }
        public string ReqStr { get; set; }
        public string ReqDex { get; set; }
        public string ReqInt { get; set; }
        public string Implicit { get; set; }
        public string ImplicitValue { get; set; }

        public Weapon() { }

    }
}
