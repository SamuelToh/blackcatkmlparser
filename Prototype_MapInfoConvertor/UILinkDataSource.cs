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
            this.next = new UIConvertKML(this);
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bool canLink = controller.canLink(cbbGeographicalField.SelectedText, cbbExcelField.SelectedText);
            if (canLink)
                showNext();
            else
                MessageBox.Show(Messages.FIELD_LINK_ERROR);
        }

        private void UILinkDataSource_Load(object sender, EventArgs e)
        {
            cbbExcelField.Items.AddRange(controller.getSociologicalDataFields().ToArray());
            cbbGeographicalField.Items.AddRange(controller.getGeographicalDataFields().ToArray());
        }
    }
}
