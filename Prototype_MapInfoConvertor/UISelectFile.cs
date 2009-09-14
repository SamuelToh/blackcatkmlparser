using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlackCat;
using System.IO;

namespace BlackCat
{
    public partial class UISelectFile : Form
    {
        public KMLParserControl KMLParserControl = KMLParserControl.Instance();
        public string midFilePath;
        public string mifFilePath;
        public string midFolderPath;
        public string mifFolderPath;
        public UISelectFile()
        {
            InitializeComponent();
            mifFolderPath = Path.GetDirectoryName(textBox4.Text);
            midFolderPath = Path.GetDirectoryName(textBox3.Text);
            midFilePath = textBox3.Text;
            mifFilePath = textBox4.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // Function1Additional_Item ai = new Function1Additional_Item();
            //this.Hide();
            //ai.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox3.Text == "" || textBox3.Text == null || textBox4.Text == "" || textBox4.Text == null) {
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("You did not select MapInfo file.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else if (KMLParserControl.validateFolder(midFolderPath) > 0)
            {
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("The folder you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else if (KMLParserControl.validateFolder(mifFolderPath) > 0)
            {
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("The folder you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else{
                ProgressBar progressbar = new ProgressBar();
                if (KMLParserControl.loadMapInfo(midFilePath, mifFilePath, progressbar) > 0)
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The files you selected are not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (MsgBoxResult == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    UISelectAdditionalInfo fd = new UISelectAdditionalInfo();
                    fd.Show();
                    this.Dispose();
                }
            }
        }

        private void Function1SelectFiles_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UIMain UIMain = new UIMain();
            UIMain.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openMidFile.InitialDirectory = "C:\\";
            openMidFile.FileName = "";
            openMidFile.Filter = "MID File (*.mid)|*.mid";
            openMidFile.ShowDialog();
            midFilePath = openMidFile.FileName;
            midFolderPath = Path.GetDirectoryName(openMidFile.FileName);
            textBox3.Text = midFilePath;  
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openMifFile.InitialDirectory = "C:\\";
            openMifFile.FileName = "";
            openMifFile.Filter = "MIF File (*.mif)|*.mif";
            openMifFile.ShowDialog();
            mifFilePath = openMifFile.FileName;
            mifFolderPath = Path.GetDirectoryName(openMifFile.FileName);
            textBox4.Text = mifFilePath;
        }
    }
}
