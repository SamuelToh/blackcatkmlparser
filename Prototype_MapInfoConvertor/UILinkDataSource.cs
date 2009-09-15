using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UILinkDataSource : BlackCat.BlackCatParserUI
    {
        public UILinkDataSource(BlackCatParserUI previous)
        {
            this.next = new UISelectOutput(this);
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            String socioColumn = cbbExcelField.Text;
            String geoColumn = cbbGeographicalField.Text;
            if (socioColumn != null && geoColumn != null && socioColumn.Length > 0 && geoColumn.Length > 0)
            {
                bool canLink = controller.setLinkFields(geoColumn, socioColumn);
                if (canLink)
                    showNext();
                else
                    MessageBox.Show(Messages.FIELD_LINK_ERROR);
            }
            else
                MessageBox.Show(Messages.MISSING_LINK_FIELD);
        }

        private void UILinkDataSource_Load(object sender, EventArgs e)
        {
            String[] socioFields = controller.getSociologicalDataFields();
            if (socioFields != null)
            {
                cbbExcelField.Items.AddRange(socioFields);
                cbbExcelField.SelectedIndex = 0;
            }
            String[] geoFields = controller.getGeographicalDataFields();
            if (geoFields != null)
            {
                cbbGeographicalField.Items.AddRange(geoFields);
                cbbGeographicalField.SelectedIndex = 0;
            }
        }
    }
}
