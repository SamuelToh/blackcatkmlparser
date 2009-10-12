using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net.Config;
using log4net.Appender;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UIMain(KMLParserControl.Instance()));
            InitialiseLogging();
        }
        
        static void InitialiseLogging()
        {
            //BasicConfigurator.Configure();
            XmlConfigurator.Configure(new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory()));

        }

    }
}
