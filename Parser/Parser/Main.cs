using Parser.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using xNet;

namespace Parser
{
    public partial class Main : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GoPrarserEntities context = new GoPrarserEntities();
            DbSet<countries> countries = context.countries;
            foreach (var county in countries)
            {
                logger.Trace(county.name);
            }
        }
    }
}
