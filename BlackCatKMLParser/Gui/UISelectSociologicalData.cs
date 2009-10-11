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
    public partial class UISelectSociologicalData : BlackCat.BlackCatParserUI
    {
        public UISelectSociologicalData(BlackCatParserUI previous)
        {
            InitializeComponent();
            this.previous = previous;
            this.next = new UISelectOutput(this);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true){
                controller.SociologicalDataChoice = SociologicalDataSelection.NONE;
            }
            else if(radioButton2.Checked == true){
                controller.SociologicalDataChoice = SociologicalDataSelection.WINNING_PARTY;
            }
            else if (radioButton3.Checked == true) {
                controller.SociologicalDataChoice = SociologicalDataSelection.SEAT_SAFETY;
            }
            this.next = new UISelectOutput(this);
            showNext();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            showPrevious();
        }

        private void UISelectSociologicalData_Load(object sender, EventArgs e)
        {

            if (previous.GetType().ToString() == "BlackCat.UISelectFileKML")
            {
                radioButton1.Enabled = false;
                radioButton2.Checked = true;
                if(controller .CanAddSociologicalData () == false){
                    MessageBox.Show(Messages.GeoSoc_NoMatch);
                    this.next = new UISelectFileKML(this);
                    showNext();
                    this.Dispose();
                }
            }
            else {
                radioButton1.Checked = true;
                if (controller.CanAddSociologicalData() == false)
                {
                    label4.Visible = true;
                    
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                }
            }
        }
    }
}
