using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UISelectAdditionalInfoMapInfo : BlackCat.BlackCatParserUI
    {
        public UISelectAdditionalInfoMapInfo(BlackCatParserUI previous) 
        {
            this.previous = previous;
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtExcelFile.Text == null || txtExcelFile.Text.Length == 0)
            {
                next = new UISelectOutput(this);
                showNext();
            }
            else
            {
                setProgressVisible(true);
                int response = controller.loadExcel(txtExcelFile.Text, progressLoading);
                if (response == 0)
                {
                    next = new UILinkDataSource(this);
                    showNext();
                }
                else
                    MessageBox.Show(Messages.EXCEL_LOAD_ERROR);
                setProgressVisible(false);
            }
        }

        private void openExcelFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtExcelFile.Text = openExcelFileDialog.FileName;
        }

        private void setProgressVisible(bool visible)
        {
            lblLoading.Visible = visible;
            progressLoading.Visible = visible;
        }

        private void btnExcelBrowse_Click(object sender, EventArgs e)
        {
            openExcelFileDialog.ShowDialog();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

        }
    }
}
