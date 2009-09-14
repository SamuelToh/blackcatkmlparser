using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BlackCat;

namespace BlackCat
{
    public partial class UISelectAdditionalInfoB : Form
    {
        public KMLParserControl KMLParserControl = KMLParserControl.Instance();
        public string excelFilePath;
        public UISelectAdditionalInfoB()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProgressBar progressbar = new ProgressBar();
            if (this.textBox3.Text != "")
            {
                if (KMLParserControl.validateFolder(Path.GetDirectoryName(excelFilePath)) > 0) {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The Excel file you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (MsgBoxResult == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                else if (KMLParserControl.loadExcel(excelFilePath, progressbar) > 0)
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The Excel file you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (MsgBoxResult == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    this.Visible = false;
                    UILinkDataSource addInfo = new UILinkDataSource();
                    addInfo.Show();
                    this.Dispose();
                }

            }
            else
            {
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("The Excel file you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UISelectFileB UISelectFileB = new UISelectFileB();
            UISelectFileB.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openXlsFile.InitialDirectory = "C:\\";
            openXlsFile.FileName = "";
            openXlsFile.Filter = "Excel File (*.xls)|*.xls";
            openXlsFile.ShowDialog();
            excelFilePath = openXlsFile.FileName;
            textBox3.Text = excelFilePath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UIMain UIMain = new UIMain();
            UIMain.Show();
            this.Dispose();
        }
    }
}
