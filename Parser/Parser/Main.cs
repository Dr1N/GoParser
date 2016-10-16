﻿using Parser;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace Parser
{
    public partial class Main : Form
    {
        private GoParser goParser = new GoParser();

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.goParser.ParseCities();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Program.logger.Trace("Test!");
        }
    }
}
