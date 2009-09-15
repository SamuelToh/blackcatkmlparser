using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UISelectFileMapInfo : BlackCatParserUI
    {
        public UISelectFileMapInfo(BlackCatParserUI previous)
        {
            InitializeComponent();
            this.previous = previous;
            this.next = new UISelectAdditionalInfoMapInfo(this);
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
                int response = controller.loadMapInfo(txtMidFilePath.Text, txtMifFilePath.Text, progressLoading);
                if (response == 0)
                    showNext();
                else
                {
                    //TODO: specific error messages
                    MessageBox.Show(Messages.MAPINFO_LOAD_ERROR);
                }
            }
        }

        private void btnMidBrowse_Click(object sender, EventArgs e)
        {
            openMidFileDialog.Filter = "MID files (*.mid)|*.mid";
            openMidFileDialog.ShowDialog();
        }

        private void btnMifBrowse_Click(object sender, EventArgs e)
        {
            openMifFileDialog.Filter = "MIF files (*.mif)|*.mif"; ;
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
    }
}
