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
    public partial class UISelectAdditionalInfoB : Form
    {
        public UISelectAdditionalInfoB()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text != "")
            {
                UILinkDataSource lds = new UILinkDataSource();
                lds.Show();
                this.Dispose();

            }
            else
            {
                this.Visible = false;
                UISelectOutput addInfo = new UISelectOutput();
                addInfo.Show();
                this.Dispose();
            }
            
        }
    }
}
