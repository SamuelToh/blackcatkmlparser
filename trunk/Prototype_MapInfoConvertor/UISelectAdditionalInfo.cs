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
    public partial class UISelectAdditionalInfo : Form
    {
        public UISelectAdditionalInfo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           /* this.Hide();
            Function1Destination_Folder df = new Function1Destination_Folder();
            df.Show();*/

        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (this.textBox3.Text != "")
            {
                UILinkDataSource lds = new UILinkDataSource();
                lds.Show();
                this.Dispose();
            }
            else
            {
                UISelectOutput fd = new UISelectOutput();
                fd.Show();
                this.Dispose();
            }

        }

        private void Function1n2Additional_Item_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
