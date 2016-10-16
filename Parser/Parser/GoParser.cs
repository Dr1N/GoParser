using AngleSharp.Parser.Html;
using AngleSharp;
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

        private async void GetPage()
        {
            // Setup the configuration to support document loading
            var config = Configuration.Default.WithDefaultLoader();
            // Load the names of all The Big Bang Theory episodes from Wikipedia
            var address = "https://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(address);
            // This CSS selector gets the desired content
            var cellSelector = "tr.vevent td:nth-child(3)";
            // Perform the query to get all cells with the content
            var cells = document.QuerySelectorAll(cellSelector);
            // We are only interested in the text - select it with LINQ
            var titles = cells.Select(m => m.TextContent);
        }
    }
}
