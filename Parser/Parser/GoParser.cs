using AngleSharp.Parser.Html;
using AngleSharp;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            List<cities> result = new List<cities>();
            List<countries> countries = this.GetCountries();
            foreach (var country in countries)
            {
                List<string> regions =  this.GetRegions(country.url);
                foreach (var region in regions)
                {
                    Console.WriteLine(region);
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
                var regions = tables[3].QuerySelectorAll("a.link");
                if (regions != null)
                {
                    foreach (var region in regions)
                    {
                        if (region.InnerHtml != "Все")
                        {
                            result.Add(region.GetAttribute("href"));
                        }
                    }
                }
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
