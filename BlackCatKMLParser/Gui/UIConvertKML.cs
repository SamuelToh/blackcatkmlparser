using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UIConvertKML : BlackCat.BlackCatParserUI
    {

        public UIConvertKML(BlackCatParserUI previous)
        {
            this.previous = previous;
            InitializeComponent();
        }

        private void UIConvertKML_Load(object sender, EventArgs e)
        {
            int response = controller.GenerateKMLFile(outputFilePath, progressGenerating);
            if (response == 0)
            {
                lblConverting.Text = "Complete";
                progressGenerating.Value = progressGenerating.Maximum;
            }
            else
            {
                MessageBox.Show(Messages.OUTPUT_FAILED);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

        }

        /*
        private void UIConvertKML_Load(object sender, EventArgs e)
        {
            //int response = controller.generateKMLFile(outputFilePath, progressGenerating);
        }
        
        public void generateKML()
        {
            int response = controller.generateKMLFile(outputFilePath, progressGenerating);
        }

        private void UIConvertKML_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                int response = controller.generateKMLFile(outputFilePath, progressGenerating);
            }
        }*/
        
    }
}
