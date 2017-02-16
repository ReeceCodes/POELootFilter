using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemScraper.Classes
{
    //ideally I would rather this be in a database but I don't have one yet so this is just the list of links I found to scrape
    //urls there is no data for: flasks, uniques
    class Url
    {
        public Dictionary<string,int> URLS = new Dictionary<string,int>(); //using the int to filter by type of html returned. I'm sure it's not the best way but better than checking each url by string

        public Url()
        {
            URLS.Add("https://www.pathofexile.com/item-data/prefixmod",1);
            URLS.Add("https://www.pathofexile.com/item-data/suffixmod", 2);
            URLS.Add("https://www.pathofexile.com/item-data/weapon", 3);
            URLS.Add("https://www.pathofexile.com/item-data/armour", 4);
            URLS.Add("https://www.pathofexile.com/item-data/jewelry", 5);
            URLS.Add("https://www.pathofexile.com/item-data/currency", 6);
            URLS.Add("https://www.pathofexile.com/skills/supportgems", 7);
            URLS.Add("https://www.pathofexile.com/skills/intelligence", 8);
            URLS.Add("https://www.pathofexile.com/skills/dexterity", 9);
            URLS.Add("https://www.pathofexile.com/skills/strength", 10);

        }
    }
}
