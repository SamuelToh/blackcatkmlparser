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
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtOutputPath.Text == null || txtOutputPath.Text.Length == 0)
                MessageBox.Show(Messages.NO_OUTPUT_FILE_PATH);
            else
            {
                int response = controller.validateFolder(txtOutputPath.Text);
                if (response == 0)
                {
                    outputFilePath = txtOutputPath.Text;
                    showNext();
                }
                else
                    MessageBox.Show(Messages.OUTPUT_FOLDER_INVALID);
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
    }
}
