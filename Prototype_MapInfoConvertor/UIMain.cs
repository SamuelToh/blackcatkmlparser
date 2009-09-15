using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlackCat;

namespace BlackCat
{
    public partial class UIMain : Form
    {
        private BlackCatParserUI firstForm;

        public UIMain()
        {
            InitializeComponent();
        }

        public void Restart()
        {
            firstForm = null;
            this.Show();
            //TODO: controller clean
        }

        private void newKml_Click(object sender, EventArgs e)
        {
            firstForm = new UISelectFileMapInfo(null);
            firstForm.MainForm = this;
            firstForm.Show();
            this.Hide();
        }

        private void addInfo_Click(object sender, EventArgs e)
        {
            firstForm = new UISelectFileKML(null);
            firstForm.MainForm = this;
            firstForm.Show();
            this.Hide();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UIMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }      
    }
}
