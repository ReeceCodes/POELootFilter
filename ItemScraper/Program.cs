using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ItemScraper
{
    //goal: scrape url for item data. each type has different info so customise to type.
    
    class Program
    {
        static void Main(string[] args)
        {
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

        public void ProcessData(string url, int type)
        {
            string data = GetHtml(url);

            string FilteredData = RemoveHTML(data);

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

            return tmp;
        }
    }
}
