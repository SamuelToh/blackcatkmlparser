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

        public UIConvertKML(BlackCatParserUI previous, IKMLParserControl controller)
            : base(controller)
        {
            this.previous = previous;
            InitializeComponent();
        }

        private void UIConvertKML_Load(object sender, EventArgs e)
        {
            int response = controller.generateKMLFile(outputFilePath, progressGenerating);
        }
        
    }
}
