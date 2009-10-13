using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UISelectFileKML : BlackCat.BlackCatParserUI
    {
        public UISelectFileKML(BlackCatParserUI previous)
        {
            this.previous = previous;
            this.next = new UISelectSociologicalData(this);
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtKmlFilePath.Text == null || txtKmlFilePath.Text.Length == 0)
            {
                MessageBox.Show(Messages.NO_KML_FILE_PATH);
            }
            else
            {
                if(fileExists(txtKmlFilePath.Text)==false){
                    MessageBox.Show(Messages.NO_KML_FILE_PATH);
                }
                else if (fileIsReadable(txtKmlFilePath.Text) == false) {
                    MessageBox.Show(Messages.NO_KML_UnReadable);
                }
                else if (fileIsReadable(txtKmlFilePath.Text) == false) {
                    MessageBox.Show(Messages.NO_KML_UnReadable);
                }
                else if (validationFileFormat(txtKmlFilePath.Text, FileFormat.KML) == false)
                {
                    MessageBox.Show(Messages.KML_Format);
                }
                else
                {
                    LoadKml();
                }

            }
        }

        private void openKMLFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtKmlFilePath.Text = openKMLFileDialog.FileName;

        }

        private void btnKMLBrowse_Click(object sender, EventArgs e)
        {
            openKMLFileDialog.Filter = "KML files (*.kml)|*.kml"; 
            openKMLFileDialog.ShowDialog();
        }

        private void LoadKml()
        {
            log.Debug("loading kml file");
            SetLoadProgressVisibility(true);
            ProgressWrapper progress = new ProgressWrapper(progressLoading);
            int response = controller.LoadKML(txtKmlFilePath.Text, progress);
            if (response == 0)
            {
                showNext();
            }
            else
            {
                //TODO: specific error messages?
                MessageBox.Show(Messages.KML_LOAD_ERROR);
            }
        }

        private void SetLoadProgressVisibility(bool visible)
        {
            lblLoading.Visible = visible;
            progressLoading.Visible = visible;
        }
    }
}
