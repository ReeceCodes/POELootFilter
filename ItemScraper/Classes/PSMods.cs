using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemScraper.Classes
{
    class PSMods
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Stat { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public PSMods() { }

        public PSMods(string name, string level, string stat, string value, string type)
        {
            Name = name;
            Level = level;
            Stat = stat;
            Value = value;
            Type = type;
        }


    }
}
