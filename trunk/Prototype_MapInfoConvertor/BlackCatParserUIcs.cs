using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public class BlackCatParserUI : Form
    {
        //Output file path
        private string outputFilePath;

        //Path to selected .kml file
        private string kmlFilePath;

        //Path to selected .mid file
        private string midFilePath;

        //Path to selected .mif file
        private string mifFilePath;

        //Path to selected Excel file (does not have to have a value)
        private string excelFilePath;

        //The index of the geographical data column for linking 
        private int linkerColumnA;

        //The index of the sociological data column for linking
        private int linkerColumnB;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BlackCatParserUI
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "BlackCatParserUI";
            this.Load += new System.EventHandler(this.BlackCatParserUI_Load);
            this.ResumeLayout(false);

        }

        private void BlackCatParserUI_Load(object sender, EventArgs e)
        {

        }

        //The Controller object
        //private KMLParserControl controller;
    }
}

