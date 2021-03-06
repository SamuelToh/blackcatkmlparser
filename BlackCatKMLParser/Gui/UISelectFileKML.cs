﻿using System;
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
            //TODO: this.next = new UISelectAdditionalInfoKML(this);
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
                setProgressVisible(true);
                int response = controller.LoadKML(txtKmlFilePath.Text, progressLoading);
                if (response == 0)
                {
                    showNext();
                }
                else
                {
                    //TODO: specific error messages
                    MessageBox.Show(Messages.MAPINFO_LOAD_ERROR);
                }
                setProgressVisible(false);
            }
        }

        private void setProgressVisible(bool visible)
        {
            lblLoading.Visible = visible;
            progressLoading.Visible = visible;
        }

        private void openKMLFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtKmlFilePath.Text = openKMLFileDialog.FileName;
        }

        private void btnKMLBrowse_Click(object sender, EventArgs e)
        {
            openKMLFileDialog.ShowDialog();
        }
    }
}
