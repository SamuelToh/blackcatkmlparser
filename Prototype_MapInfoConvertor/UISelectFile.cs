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
    public partial class UISelectFile : Form
    {
        public UISelectFile()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // Function1Additional_Item ai = new Function1Additional_Item();
            //this.Hide();
            //ai.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            UISelectAdditionalInfo fd = new UISelectAdditionalInfo();
            fd.Show();
            this.Dispose();
        }

        private void Function1SelectFiles_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
