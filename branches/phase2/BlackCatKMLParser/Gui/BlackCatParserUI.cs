using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace BlackCat
{
    public partial class BlackCatParserUI : Form
    {
        private static UIMain main;
        protected BlackCatParserUI previous;
        protected BlackCatParserUI next;
        protected IKMLParserControl controller;
        protected static string outputFilePath;
        protected ILog log; 
        
        public BlackCatParserUI()
        {
            this.controller = KMLParserControl.Instance();
            InitializeComponent();
            log = LogManager.GetLogger(this.Name);
        }

        public UIMain MainForm
        {
            set { main = value; }
        }

        // Show the Form referred to by the "next" data member
        // Pre: next is not null
        // Post: this form is hidden, the next is shown.
        protected void showNext()
        {
            if (next != null)
            {
                this.Hide();
                next.Show();
            }
        }

        // Show the Form referred to by the "previous" data member
        // Pre: previous is not null
        // Post: this form is hidden, the previous is shown.
        protected void showPrevious()
        {
            if (previous != null)
            {
                this.Hide();
                previous.Show();
            }
        }

        private void BlackCatParserUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.Restart();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            main.Restart();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            showPrevious();
        }

        //Shared Validation methods
        public bool folderIsWritable(String folderURL)
        {
            DirectoryInfo directory = new DirectoryInfo(folderURL);
            try
            {
                DirectorySecurity dirsecurity = directory.GetAccessControl();
            }
            catch
            {
                return false;
            }
            string attributes = directory.Attributes.ToString();
            string[] attributesList = attributes.Split(',', ' ');
            foreach (string attribute in attributesList)
            {
                if (attribute == "ReadOnly")
                {
                    return false;
                }
            }
            return true;
        }

        public bool folderExists(String folderURL)
        {
            if (Directory.Exists(folderURL) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool fileIsReadable(String fileURL)
        {
            FileInfo fileInfo = new FileInfo(fileURL);
            try
            {
                FileSecurity filesecurity = fileInfo.GetAccessControl();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool fileExists(String fileURL)
        {
            if (File.Exists(fileURL) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
