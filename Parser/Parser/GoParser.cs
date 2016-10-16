using AngleSharp.Parser.Html;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{

    public class GoParser
    {
        private readonly string category = "view_subsection.php?id_subsection=146";

        private GoPrarserEntities context = new GoPrarserEntities();
        private HtmlParser htmlParser = new HtmlParser();

        public GoParser()
        {

        }

        public void ParseCities()
        {
            List<countries> countries = this.GetCountries();
            foreach (var item in countries)
            {
                string url = String.Format("{0}{1}", item.url, this.category);
                Program.logger.Trace(url);
            }
        }

        private List<countries> GetCountries()
        {
           return context.countries.ToList();
        }
    }
}
