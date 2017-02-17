using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ItemScraper
{
    //goal: scrape url for item data. each type has different info so customise to type.
    
    class Program
    {
        static void Main(string[] args)
        {
            ItemScraper.Classes.Url urls = new ItemScraper.Classes.Url();

            Program p = new Program();

            foreach (var u in urls.URLS)
            {
                p.ProcessData(u.Key, u.Value);                
            }

        }

        public string GetHtml(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string OfTheJedi = "";
            using (StreamReader sr = new StreamReader(data))
            {
                OfTheJedi = sr.ReadToEnd();
            }

            return OfTheJedi;
        }

        //type is here so that I might need to change the html removal for each page and to tell which class to build as (some urls will use the same class)
        public void ProcessData(string url, int type)
        {
            string data = GetHtml(url);

            string FilteredData = RemoveHTML(data);

            foreach (string s in FilteredData.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                if (s.Trim() == "")
                {
                    continue;
                }
                else if (s.Trim().Contains("<tr>") == true) //yeah i know... i just like to be explicit and clear
                {
                    //start next object, complete last object
                }
                else if (s.Trim().Contains("<td>") == true)
                {
                    //fill current object
                }
                else
                {
                    //omg what is this!?
                    continue;
                }
            }

        }

        public string RemoveHTML(string data)
        {
            string tmp = "";

            string trimBefore = "<div class=\"wrapper\">";
            string trimAfter = "<div class=\"clear\"";

            tmp = data.Split(new string[] { trimBefore }, StringSplitOptions.None)[1];
            tmp = tmp.Split(new string[] { trimAfter }, StringSplitOptions.None)[0];

            //trim everything before: <div class="wrapper"> //the first one
            //trim everything after: <div class="clear"


            //remove everything that isn't in the table(s), tried to get just the tds but then i was having issue with i got rid of the rows so I didn't know where to create the next object
            Regex replaceDivs = new Regex(@"</?div.*?>");
            Regex replaceAnchors = new Regex(@"<a.*</a>");
            Regex replaceTables = new Regex(@"</?table.*?>");
            Regex replaceAttributes = new Regex(@"<(th|tr|td)(.*?)>");
            Regex replaceSpans = new Regex(@"<span.*?>.*?</span>"); //probably removed with the divs but anyways
            Regex replaceBreaks = new Regex(@"<br\s*?/\s*?>");
            Regex replaceH = new Regex(@"<h.*?>.*?</h[1-6]?>");

            tmp = replaceDivs.Replace(tmp, "");
            tmp = replaceAnchors.Replace(tmp, "");
            tmp = replaceTables.Replace(tmp, "");
            tmp = replaceSpans.Replace(tmp, "");
            tmp = replaceBreaks.Replace(tmp, " / ");
            tmp = replaceH.Replace(tmp, "");
            tmp = replaceAttributes.Replace(tmp, "<$1>");
            
            return tmp.Trim(); //still needs a little massaging but it's enough to work with
        }
    }
}
