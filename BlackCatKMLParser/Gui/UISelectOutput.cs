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
                if(folderExists(path) == false){
                    MessageBox.Show(Messages.FOLDER_INVALID_1);
                }
                else if(folderIsWritable(path) == false){
                    MessageBox.Show(Messages.FOLDER_INVALID_2);
                }
                else if(hasSufficientDiskSpace(path) == false){
                    MessageBox.Show(Messages.FOLDER_INVALID_3);
                }
                else if (urlLengthIsValid(txtOutputPath.Text) == false) {
                    MessageBox.Show(Messages.FOLDER_INVALID_4);
                }
                else if (validationFileFormat(txtOutputPath.Text, ".kml") == false)
                {
                    MessageBox.Show(Messages.KML_Format);
                }
                else {
                    outputFilePath = txtOutputPath.Text;
                    showNext();
                }
            }
        }

        private void saveKMLFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtOutputPath.Text = saveKMLFileDialog.FileName;
        }

        private void btnOutputBrowse_Click(object sender, EventArgs e)
        {
            saveKMLFileDialog.Filter = "KML files (*.kml)|*.kml"; 
            saveKMLFileDialog.ShowDialog();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            showPrevious();
        }
    }
}
