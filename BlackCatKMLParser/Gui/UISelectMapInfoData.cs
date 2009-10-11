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
    public partial class UISelectMapInfoData : BlackCat.BlackCatParserUI
    {
       
        public UISelectMapInfoData(BlackCatParserUI previous)
        {
            this.previous = previous;
            this.next = new UISelectSociologicalData(this);
            InitializeComponent();
            foreach (string str in controller.GetMapInfoDataFields()){
                checkedListBox1.Items.Add(str.ToString(), false);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            List<string> item=new List<string>() ;
            for (int i = 0; i < checkedListBox1.Items.Count; i++ ) {

                if (checkedListBox1.GetItemChecked(i) == true) {
                   
                    item.Add(checkedListBox1.GetItemText(i).ToString());
                   
                }
            }
            controller.MapInfoDataFieldsToDisplay = item;
            showNext();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            showPrevious();
        }
    }
}
