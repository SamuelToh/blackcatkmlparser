using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UISelectAdditionalInfoKML : BlackCat.BlackCatParserUI
    {
        public UISelectAdditionalInfoKML(BlackCatParserUI previous)
        {
            this.previous = previous;
            this.next = new UILinkDataSource(this);
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //If the user has chosen additional info, next is link page
            // Else next is output page
            if (txtExcelFile.Text == null || txtExcelFile.Text.Length == 0)
            {
                MessageBox.Show(Messages.NO_EXCEL_FILE_PATH);
            }
            else
            {
                setProgressVisible(true);
                int response = controller.loadExcel(txtExcelFile.Text, progressLoading);
                if (response == 0)
                    showNext();
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openExcelFileDialog.ShowDialog();
        }

        private void UISelectAdditionalInfoKML_Load(object sender, EventArgs e)
        {

        }
    }
}
