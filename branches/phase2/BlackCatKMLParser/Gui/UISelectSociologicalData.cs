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
            if(rbtnShowNone.Checked == true){
                controller.SociologicalDataChoice = SociologicalDataSelection.NONE;
            }
            else if(rbtnShowWinner.Checked == true){
                controller.SociologicalDataChoice = SociologicalDataSelection.WINNING_PARTY;
            }
            else if (rbtnShowSafety.Checked == true) {
                controller.SociologicalDataChoice = SociologicalDataSelection.SEAT_SAFETY;
            }
            this.next = new UISelectOutput(this);
            showNext();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            showPrevious();
        }

        private void SetRadioButtonStates()
        {
            if (previous.GetType().ToString() == "BlackCat.UISelectFileKML")
            {
                rbtnShowNone.Enabled = false;
                rbtnShowWinner.Checked = true;
                if (controller.CanAddSociologicalData() == false)
                {
                    MessageBox.Show(Messages.GeoSoc_NoMatch);
                    this.next = new UISelectFileKML(this);
                    showNext();
                    this.Dispose();
                }
            }
            else
            {
                rbtnShowNone.Checked = true;
                if (controller.CanAddSociologicalData())
                {
                    label4.Visible = false;

                    rbtnShowNone.Enabled = true;
                    rbtnShowWinner.Enabled = true;
                    rbtnShowSafety.Enabled = true;
                }
                else
                {
                    label4.Visible = true;

                    rbtnShowNone.Enabled = true;
                    rbtnShowWinner.Enabled = false;
                    rbtnShowSafety.Enabled = false;
                }
            }
        }

        private void UISelectSociologicalData_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                SetRadioButtonStates();
        }
    }
}
