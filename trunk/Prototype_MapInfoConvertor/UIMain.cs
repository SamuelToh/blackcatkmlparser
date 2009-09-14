using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controller;

namespace Prototype_MapInfoConvertor
{
    public partial class UIMain : Form
    {
        public UIMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            UISelectFile sf = new UISelectFile();
            sf.Visible = true;
            sf.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            UISelectFileB addInfo = new UISelectFileB();
            addInfo.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private System.Collections.Hashtable myTable;

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
    }
}
