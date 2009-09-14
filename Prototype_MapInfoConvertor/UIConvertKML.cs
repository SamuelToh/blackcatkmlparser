using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlackCat;
using System.IO;

namespace BlackCat
{
    public partial class UIConvertKML : BlackCatParserUI
    {
        public KMLParserControl KMLParserControl = KMLParserControl.Instance();
        private String MidFileURL;
        private String MifFileURL;
        private String KMLFileURL;
        private String ExcelFileURL;

        //Called when user request to enhance his/her kml file
        //Before proceeding to the real function we first create the Validation class
        //Do the necessary validation
        //If pass all the test than we proceed to the real enhance function in ParserEngine package
        private void enhanceKML() { }

        //Called when user request to create a new kml from stratch
        //This function will create and call the validation class 
        //Do the validations by using the validation object created above
        //If pass all validations we call the parser engine to create new kml file.
        private void createKML() { }

        public UIConvertKML()
        {
            UISelectFile UISelectFile = new UISelectFile();
            MidFileURL=UISelectFile.midFilePath;
            MifFileURL = UISelectFile.mifFilePath;
            UISelectFileB UISelectFileB = new UISelectFileB();
            KMLFileURL=UISelectFileB.KMLFilePath;
            UISelectAdditionalInfo UISelectAdditionalInfo = new UISelectAdditionalInfo();
            ExcelFileURL = UISelectAdditionalInfo.excelFilePath;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Converting c = new Converting();
            //c.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UIMain UIMain = new UIMain();
            UIMain.Show();
            this.Dispose(); Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UISelectOutput UISelectOutput = new UISelectOutput();
            if (UISelectOutput.outPutKMLFilePath != null)
            {
                if (KMLParserControl.generateKMLFile(UISelectOutput.outPutKMLFilePath, progressBar1) > 0)
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The KML file can not be created.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (MsgBoxResult == DialogResult.No)
                    {
                        UIMain UIMain = new UIMain();
                        UIMain.Show();
                        this.Dispose();
                    }
                }
                else
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The KML file is created.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (MsgBoxResult == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                }
            }
            else if (KMLFileURL != null) {
                if (KMLParserControl.generateKMLFile(KMLFileURL, progressBar1) > 0)
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The KML file can not be created.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (MsgBoxResult == DialogResult.No)
                    {
                        UIMain UIMain = new UIMain();
                        UIMain.Show();
                        this.Dispose();
                    }
                }
                else
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("The KML file is created.", "Black Cat Parser Application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (MsgBoxResult == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                }
            }
        }

    }
}
