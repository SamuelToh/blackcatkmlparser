using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UISelectFileB : Form
    {
        public UISelectFileB()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            UISelectAdditionalInfo addInfo = new UISelectAdditionalInfo();
            addInfo.Show();
        }

        private void Function2SelectFilesA_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
