using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using Parser.EF;
using AngleSharp.Dom;
using xNet;

namespace Parser
{
    public class GoParser
    {
        private readonly string category = "view_subsection.php?id_subsection=146";

        private GoDB db = new GoDB();
        private HtmlParser htmlParser = new HtmlParser();

        public GoParser() { }

        public void ParseCities()
        {
            List<countries> countries = this.GetCountries();
            foreach (var country in countries)
            {
                List<string> regions =  this.GetRegions(country.url);
                foreach (var region in regions)
                {
                    HashSet<string> cities = GetCitiesFromRegion(region);
                    foreach (var item in cities)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
        }

        private int ClearCities()
        {
            this.db.cities.RemoveRange(this.db.cities);
            return this.db.SaveChanges();
        }

        private List<string> GetRegions(string url)
        {
            List<string> result = new List<string>();
            var document = this.GetPage(url);
            var tables = document.QuerySelectorAll("body>table");
            if (tables != null)
            {
                var regions = tables[3].QuerySelectorAll("td>a.link");
                if (regions != null)
                {
                    foreach (var region in regions)
                    {
                        if (region.InnerHtml != "Все" && region.GetAttribute("href") != url)
                        {
                            result.Add(region.GetAttribute("href"));
                        }
                    }
                }
            }

            return result;
        }

        private HashSet<string> GetCitiesFromRegion(string url)
        {
            HashSet<string> result = new HashSet<string>();
            var document = this.GetPage(url);
            var cities = document.QuerySelectorAll("td>li>a.link");
            foreach (var city in cities)
            {
                result.Add(city.GetAttribute("href"));
            }

            return result;
        }

        private List<countries> GetCountries()
        {
           var tmp = this.db.countries;

           return db.countries.ToList();
        }

        private IDocument GetPage(string url)
        {
            string content = String.Empty;
            using (var request = new HttpRequest())
            {
                content = request.Get(url).ToString();
            }

            return this.htmlParser.Parse(content);
        }
    }
}
