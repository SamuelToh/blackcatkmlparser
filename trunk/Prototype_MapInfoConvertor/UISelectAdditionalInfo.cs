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
    public partial class UISelectAdditionalInfo : Form
    {
        public KMLParserControl KMLParserControl = KMLParserControl.Instance();
        public string excelFilePath;
        public string excelFolderPath;
        public UISelectAdditionalInfo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           /* this.Hide();
            Function1Destination_Folder df = new Function1Destination_Folder();
            df.Show();*/

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProgressBar progressbar = new ProgressBar();
            if (excelFilePath != "")
            {
                if (KMLParserControl.validateFolder(excelFolderPath) > 0)
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The folder you selected is not available.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                    UILinkDataSource lds = new UILinkDataSource();
                    lds.Show();
                    this.Dispose();
                }

            }
            else
            {
                UISelectOutput fd = new UISelectOutput();
                fd.Show();
                this.Dispose();
            }

        }

        private void Function1n2Additional_Item_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            UISelectFile UISelectFile = new UISelectFile();
            UISelectFile.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openXlsFile.InitialDirectory = "C:\\";
            openXlsFile.FileName = "";
            openXlsFile.Filter = "Excel File (*.xls)|*.xls";
            openXlsFile.ShowDialog();
            excelFolderPath = Path.GetDirectoryName(openXlsFile.FileName);
            excelFilePath = openXlsFile.FileName;
            textBox3.Text = excelFilePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UIMain UIMain = new UIMain();
            UIMain.Show();
            this.Dispose();
        }
    }
}
