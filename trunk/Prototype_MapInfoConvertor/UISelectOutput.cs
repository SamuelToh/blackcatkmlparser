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
    public partial class UISelectOutput : Form
    {
        public KMLParserControl KMLParserControl = KMLParserControl.Instance();
        public string outPutPath;
        public string outPutKMLFilePath;
        public UISelectOutput()
        {
            InitializeComponent();
            outPutKMLFilePath = Path.GetDirectoryName(textBox3.Text);
            outPutKMLFilePath = textBox3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*this.Hide();
            Function1Convert_KML ck = new Function1Convert_KML();
            ck.Show();*/
        }

        private void Function1Destination_Folder_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (outPutPath == "" || outPutPath == null)
            {            
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("The path you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else if (KMLParserControl.validateFolder(outPutPath) > 0)
            {
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("The path you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                UIConvertKML fc = new UIConvertKML();
                fc.Show();
                this.Dispose();
            }

            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            UILinkDataSource UILinkDataSource = new UILinkDataSource();
            UILinkDataSource.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveKMLFile.InitialDirectory = "C:\\";
            saveKMLFile.Filter = "KML File (*.kml)|*.kml";
            saveKMLFile.FileName = "";
            saveKMLFile.ShowDialog();
            outPutPath = Path.GetDirectoryName(saveKMLFile.FileName);
            textBox3.Text = saveKMLFile.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UIMain UIMain = new UIMain();
            UIMain.Show();
            this.Dispose();
        }
    }
}
