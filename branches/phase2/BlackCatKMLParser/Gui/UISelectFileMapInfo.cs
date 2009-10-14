using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BlackCat;

namespace BlackCat
{
    public partial class UISelectFileMapInfo : BlackCat.BlackCatParserUI
    {
        public UISelectFileMapInfo(BlackCatParserUI previous)
        {
            InitializeComponent();
            this.previous = previous;
            this.next = new UISelectMapInfoData(this);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtMidFilePath.Text == null || txtMidFilePath.Text.Length == 0)
            {
                MessageBox.Show(Messages.NO_MID_FILE_PATH);
            }
            else if (txtMifFilePath.Text == null || txtMifFilePath.Text.Length == 0)
            {
                MessageBox.Show(Messages.NO_MIF_FILE_PATH);
            }
            else
            {
                if (fileExists(txtMifFilePath.Text) == false  )
                {
                    MessageBox.Show(Messages.NO_MIF_FILE_PATH);
                }
                else if (fileExists(txtMidFilePath.Text) == false) {
                    MessageBox.Show(Messages.NO_MID_FILE_PATH);
                }
                else if (fileIsReadable(txtMidFilePath.Text) == false)
                {
                    MessageBox.Show(Messages.MID_UnReadable);
                }
                else if (fileIsReadable(txtMifFilePath.Text) == false)
                {
                    MessageBox.Show(Messages.MIF_UnReadable);
                }
                else if (validationFileFormat(txtMidFilePath.Text, FileFormat.MID) == false) {
                    MessageBox.Show(Messages.MID_Format); 
                }
                else if (validationFileFormat(txtMifFilePath.Text, FileFormat.MIF) == false) {
                    MessageBox.Show(Messages.MIF_Format);
                }
                else 
                {
                    LoadMapInfo();
                }
            }
        }

        private void btnMidBrowse_Click(object sender, EventArgs e)
        {
            openMidFileDialog.Filter = "MID files (*.mid)|*.mid|All Files |*";
            openMidFileDialog.ShowDialog();
        }

        private void btnMifBrowse_Click(object sender, EventArgs e)
        {
            openMifFileDialog.Filter = "MIF files (*.mif)|*.mif|All Files |*"; 
            openMifFileDialog.ShowDialog();
        }

        private void openMidFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtMidFilePath.Text = openMidFileDialog.FileName;
        }

        private void openMifFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtMifFilePath.Text = openMifFileDialog.FileName;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            showPrevious();
        }

        private void LoadMapInfo()
        {
            ProgressWrapper progress = new ProgressWrapper(progressLoading);

            SetLoadProgressVisibility(true);
            int response = controller.LoadMapInfo(txtMidFilePath.Text, txtMifFilePath.Text, progress);
            if (response == 0)
            {
                showNext();
            }
            else
            {
                //TODO: specific error messages?
                MessageBox.Show(Messages.MAPINFO_LOAD_ERROR);
            }
        }

        private void SetLoadProgressVisibility(bool visible)
        {
            lblLoading.Visible = visible;
            progressLoading.Visible = visible;
        }

    }
}
