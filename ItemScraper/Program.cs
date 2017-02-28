using ItemScraper.Classes;
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
        public List<Armour> Arms = new List<Armour>();
        public List<Currency> Currs = new List<Currency>();
        public List<Jewelry> Jews = new List<Jewelry>();
        public List<PSMods> Pss = new List<PSMods>();
        public List<Skills> Skis = new List<Skills>();
        public List<Weapon> Weas = new List<Weapon>();

        public Armour CurrArm = new Armour();
        public Currency CurrCurr = new Currency();
        public Jewelry CurrJew = new Jewelry();
        public PSMods CurrPS = new PSMods();
        public Skills CurrSkills = new Skills();
        public Weapon CurrWeap = new Weapon();

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

            //I didn't think this trough well and I hate this idea but for each td assign a count and based on that set the properties
            //next plan might be to replace the newlines in between <td> and then can do a for loop on them for each line
            int ArmCount = 0;
            int CurrCount = 0;
            int JewCount = 0;
            int PSCount = 0;
            int SkiCount = 0;
            int WeaCount = 0;

            string FilteredData = RemoveHTML(data).Replace("\n", "~1~").Replace("\t", ""); //use my own place holders instead of \n or \t

            foreach (string s in FilteredData.Split(new string[] { "~1~" }, StringSplitOptions.None)) //I would have thought \r\n but apparently not
            {
                if (s.Trim() == "")
                {
                    continue;
                }
                else if (s.Trim().Contains("<tr>") == true) //yeah i know... i just like to be explicit and clear
                {


                    //start next object, complete last object
                    switch (type)
                    {
                        case 1:   //Prefix 
                            SaveClass(type);
                            CurrPS = new PSMods();
                            break;
                        case 2:   //Suffix                         
                            SaveClass(type);
                            CurrPS = new PSMods();
                            break;
                        case 3: //Weapon                                                
                            break;
                        case 4: //Armour
                            CurrArm = new Armour();
                            break;
                        case 5: //Jewelry
                            CurrJew = new Jewelry();
                            break;
                        case 6: //Currency
                            CurrCurr = new Currency();
                            break;
                        case 7: //Skills
                            CurrSkills = new Skills();
                            break;
                        default:
                            break;
                    }
                }
                else if (s.Trim().Contains("<td>") == true)
                {
                    //fill current object
                    switch (type)
                    {
                        case 1: //Prefix
                            PSCount++;
                            switch (PSCount)
                            {
                                case 1:
                                    CurrPS.Name = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 2:
                                    CurrPS.Level = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 3:
                                    CurrPS.Stat = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 4:
                                    CurrPS.Value = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                default:
                                    CurrPS.Type = "prefix";
                                    PSCount = 0;
                                    break;

                            }
                            break;
                        case 2: //Suffix
                            PSCount++;
                            switch (PSCount)
                            {
                                case 1:
                                    CurrPS.Name = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 2:
                                    CurrPS.Level = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 3:
                                    CurrPS.Stat = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 4:
                                    CurrPS.Value = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                default:
                                    CurrPS.Type = "suffix";
                                    PSCount = 0;
                                    break;

                            }
                            break;
                        case 3: //Weapon
                            WeaCount++;
                            switch (WeaCount)
                            {
                                case 1:
                                    //image
                                    break;
                                case 2:
                                    CurrWeap.Name = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 3:
                                    CurrWeap.Level = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 4:
                                    CurrWeap.Damage = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 5:
                                    CurrWeap.APS = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 6:
                                    CurrWeap.DPS = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 7:
                                    CurrWeap.ReqStr = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 8:
                                    CurrWeap.ReqDex = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 9:
                                    CurrWeap.ReqInt = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 10:
                                    CurrWeap.Implicit = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 11:
                                    CurrWeap.ImplicitValue = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                default:
                                    WeaCount = 0;

                                    SaveClass(type);
                                    CurrWeap = new Weapon();

                                    break;

                            }
                            break;
                        case 4: //Armour
                            ArmCount++;
                            switch (ArmCount)
                            {
                                case 1: //image
                                    break;
                                case 2:
                                    CurrArm.Name = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 3:
                                    CurrArm.Level = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 4:
                                    CurrArm.AR = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 5:
                                    CurrArm.EV = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 6:
                                    CurrArm.ES = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 7:
                                    CurrArm.ReqStr = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 8:
                                    CurrArm.ReqDex = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 9:
                                    CurrArm.ReqInt = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 10:
                                    CurrArm.Implicit = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 11:
                                    CurrArm.ImplicitValue = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                default:
                                    ArmCount = 0;

                                    SaveClass(type);
                                    CurrArm = new Armour();
                                    break;

                            }
                            break;
                        case 5: //Jewelry
                            JewCount++;
                            switch (JewCount)
                            {
                                case 1: //image
                                    break;
                                case 2:
                                    CurrJew.Name = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 3:
                                    CurrJew.Level = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 4:
                                    CurrJew.Implicit = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                case 5:
                                    CurrJew.ImplicitValue = s.Replace("<td>", "").Replace("</td>", "");
                                    break;
                                default:
                                    JewCount = 0;
                                    SaveClass(type);
                                    CurrJew = new Jewelry();
                                    break;

                            }
                            break;
                        case 6: //Currency
                            CurrCount++;
                            switch (CurrCount)
                            {
                                case 1:
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                default:
                                    break;

                            }
                            break;
                        case 7: //Skills
                            SkiCount++;
                            switch (SkiCount)
                            {
                                case 1:
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                default:
                                    break;

                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //omg what is this!?
                    continue;
                }
            }

        }

        public void SaveClass(int type)
        {
            switch (type)
            {
                case 1: //Prefix
                    Pss.Add(CurrPS);
                    break;
                case 2: //Suffix
                    Pss.Add(CurrPS);
                    break;
                case 3: //Weapon
                    Weas.Add(CurrWeap);
                    break;
                case 4: //Armour
                    Arms.Add(CurrArm);
                    break;
                case 5: //Jewelry
                    Jews.Add(CurrJew);
                    break;
                case 6: //Currency
                    Currs.Add(CurrCurr);
                    break;
                case 7: //Skills
                    Skis.Add(CurrSkills);
                    break;
                default:
                    break;
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
