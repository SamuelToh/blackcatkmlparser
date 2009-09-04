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
    public partial class UISelectOutput : Form
    {
        public UISelectOutput()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*this.Hide();
            Function1Convert_KML ck = new Function1Convert_KML();
            ck.Show();*/
        }

        private void Function1Destination_Folder_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UIConvertKML fc = new UIConvertKML();
            fc.Show();
            this.Dispose();
            
        }
    }
}
