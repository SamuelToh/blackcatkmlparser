using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net.Config;
using log4net;

namespace BlackCat
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitialiseLogging();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UIMain(KMLParserControl.Instance()));
        }
        
        static void InitialiseLogging()
        {
            BasicConfigurator.Configure();
        }

    }
}
