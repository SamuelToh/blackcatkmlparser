using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prototype_MapInfoConvertor
{
    public partial class UIDisplayMapInfo : Form
    {
        public UIDisplayMapInfo()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UISelectSociologicalData socio = new UISelectSociologicalData();
            socio.Show();
            this.Hide();
        }
    }
}
