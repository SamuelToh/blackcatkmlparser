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
    public partial class UILinkDataSource : Form
    {
        public UILinkDataSource()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UISelectOutput c_kml = new UISelectOutput();
            c_kml.Show();
            this.Dispose();
        }

        private void Function1n2LinkDataSource_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
