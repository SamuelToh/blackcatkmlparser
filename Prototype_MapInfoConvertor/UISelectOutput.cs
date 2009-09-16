using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UISelectOutput : BlackCat.BlackCatParserUI
    {
        public UISelectOutput(BlackCatParserUI previous)
        {
            this.previous = previous;
            this.next = new UIConvertKML(this);
            InitializeComponent();
            //txtOutputPath.Text = @"C:\Users\Sabers Father\Desktop\Output\test2.kml"; //Testing
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtOutputPath.Text == null || txtOutputPath.Text.Length == 0)
                MessageBox.Show(Messages.NO_OUTPUT_FILE_PATH);
            else
            {
                String path = txtOutputPath.Text;
                path = path.Substring(0, path.LastIndexOf('\\'));
                int response = controller.validateFolder(path);
                if (response == 0)
                {
                    outputFilePath = txtOutputPath.Text;
                    showNext();
                }
                else
                {
                    string error = Messages.OUTPUT_PATH_INVALID;
                    switch (response)
                    {
                        case 1: error = Messages.FOLDER_INVALID_1;
                            break;
                        case 2: error = Messages.FOLDER_INVALID_2;
                            break;
                        case 3: error = Messages.FOLDER_INVALID_3;
                            break;
                        case 4: error = Messages.FOLDER_INVALID_4;
                            break;
                        default:
                            break;
                    }
                    MessageBox.Show(error);
                }
            }
        }

        private void saveKMLFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtOutputPath.Text = saveKMLFileDialog.FileName;
        }

        private void btnOutputBrowse_Click(object sender, EventArgs e)
        {
            saveKMLFileDialog.ShowDialog();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

        }
    }
}
