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
    public partial class UILinkDataSource : Form
    {
        public KMLParserControl KMLParserControl = new KMLParserControl();
        public UILinkDataSource()
        {
            InitializeComponent();
            UISelectFileB UISelectFileB = new UISelectFileB();
            if (UISelectFileB.KMLFilePath != null) {
                foreach (string tempString in KMLParserControl.getKMLDataFields())
                {
                    comboBox1.Items.Add(tempString);
                }
            }
            else
            {
                foreach (string tempString in KMLParserControl.getGeographicalDataFields())
                {
                    comboBox1.Items.Add(tempString);
                }
            }
            foreach (string temp in KMLParserControl.getSociologicalDataFields()) {
                comboBox2.Items.Add(temp);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!KMLParserControl.canLink(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString()))
            {
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("The fields you selected can not be linked.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (MsgBoxResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                if (!KMLParserControl.linkGeographicalAndSocialData(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString()))
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The fields are not linked successful.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (MsgBoxResult == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    UISelectOutput c_kml = new UISelectOutput();
                    c_kml.Show();
                    this.Dispose();
                }
            }
        }

        private void Function1n2LinkDataSource_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UISelectAdditionalInfo UISelectAdditionalInfo = new UISelectAdditionalInfo();
            UISelectAdditionalInfo.Show();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UIMain UIMain = new UIMain();
            UIMain.Show();
            this.Dispose();
        }
    }
}
