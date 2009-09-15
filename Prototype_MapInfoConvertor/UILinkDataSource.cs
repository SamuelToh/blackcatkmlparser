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
        public UILinkDataSource(BlackCatParserUI previous, IKMLParserControl controller)
            : base(controller)
        {
            this.next = new UIConvertKML(this, controller);
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bool canLink = controller.setLinkFields(cbbGeographicalField.SelectedText, cbbExcelField.SelectedText);
            if (canLink)
                showNext();
            else
                MessageBox.Show(Messages.FIELD_LINK_ERROR);
        }

        private void UILinkDataSource_Load(object sender, EventArgs e)
        {
            String[] socioFields = controller.getSociologicalDataFields();
            if(socioFields != null)
                cbbExcelField.Items.AddRange(socioFields);
            String[] geoFields = controller.getGeographicalDataFields();
            if(geoFields != null)
                cbbGeographicalField.Items.AddRange(geoFields);
        }
    }
}
