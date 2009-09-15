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
        public UISelectAdditionalInfoMapInfo(BlackCatParserUI previous, IKMLParserControl controller)
            : base(controller)
        {
            this.previous = previous;
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtExcelFile.Text == null || txtExcelFile.Text.Length == 0)
            {
                setProgressVisible(true);
                int response = controller.loadExcel(txtExcelFile.Text, progressLoading);
                if (response == 0)
                {
                    next = new UISelectOutput(this, controller);
                    showNext();
                }
                else
                    MessageBox.Show(Messages.EXCEL_LOAD_ERROR);
                setProgressVisible(false);
            }
            else
            {
                next = new UILinkDataSource(this, controller);
                showNext();
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
    }
}
