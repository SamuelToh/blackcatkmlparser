using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BlackCat
{
    public partial class UISelectFileB : Form
    {
        public KMLParserControl KMLParserControl = KMLParserControl.Instance();
        public string KMLFilePath;
        public UISelectFileB()
        {
            InitializeComponent();
            KMLFilePath = textBox3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProgressBar ProgressBar = new ProgressBar();
            if(KMLParserControl.validateFolder(Path.GetDirectoryName(KMLFilePath)) > 0){
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("The folder you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else if (KMLParserControl.loadKML(KMLFilePath, ProgressBar) > 0)
            {
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("The KML file you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                this.Visible = false;
                UISelectAdditionalInfoB addInfo = new UISelectAdditionalInfoB();
                addInfo.Show();
            }
        }

        private void Function2SelectFilesA_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openExistKMLFile.InitialDirectory = "C:\\";
            openExistKMLFile.Filter = "KML File (*.kml)|*.kml";
            openExistKMLFile.FileName = "";
            openExistKMLFile.ShowDialog();
            KMLFilePath = openExistKMLFile.FileName;
            textBox3.Text = KMLFilePath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UIMain UIMain = new UIMain();
            UIMain.Show();
            this.Dispose();
        }
    }
}
