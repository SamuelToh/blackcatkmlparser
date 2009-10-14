using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public partial class UISelectOutput : BlackCat.BlackCatParserUI
    {
        private enum DisplayMode {OUTPUT_PATH,CONVERTING};
        private DisplayMode displayMode = DisplayMode.OUTPUT_PATH;

        public UISelectOutput(BlackCatParserUI previous)
        {
            this.previous = previous;
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (displayMode == DisplayMode.OUTPUT_PATH)
            {
                if (txtOutputPath.Text == null || txtOutputPath.Text.Length == 0)
                    MessageBox.Show(Messages.NO_OUTPUT_FILE_PATH);
                else
                {
                    String path = txtOutputPath.Text;
                    path = path.Substring(0, path.LastIndexOf('\\'));
                    if (folderExists(path) == false)
                    {
                        MessageBox.Show(Messages.FOLDER_INVALID_1);
                    }
                    else if (folderIsWritable(path) == false)
                    {
                        MessageBox.Show(Messages.FOLDER_INVALID_2);
                    }
                    else if (hasSufficientDiskSpace(path) == false)
                    {
                        MessageBox.Show(Messages.FOLDER_INVALID_3);
                    }
                    else if (urlLengthIsValid(txtOutputPath.Text) == false)
                    {
                        MessageBox.Show(Messages.FOLDER_INVALID_4);
                    }
                    else if (validationFileFormat(txtOutputPath.Text, FileFormat.KML) == false)
                    {
                        MessageBox.Show(Messages.KML_Format);
                    }
                    else
                    {
                        outputFilePath = txtOutputPath.Text;
                        log.Debug("Output path entered - " + outputFilePath);
                        SwitchDisplayMode();
                    }
                }
            }
        }

        private void saveKMLFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtOutputPath.Text = saveKMLFileDialog.FileName;
        }

        private void btnOutputBrowse_Click(object sender, EventArgs e)
        {
            saveKMLFileDialog.Filter = "KML files (*.kml)|*.kml"; 
            saveKMLFileDialog.ShowDialog();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (displayMode == DisplayMode.CONVERTING)
                SwitchDisplayMode();
            else
            {
                showPrevious();
            }
        }

        private void SwitchDisplayMode()
        {
            log.Debug("Switching display mode from " + displayMode.ToString());
            String step3Label = "Step #3 Choose your output folder";
            String step4Label = "Step #4 - Generating your new KML file";
            if (displayMode == DisplayMode.OUTPUT_PATH)
            {
                //Switch to step 4 layout
                lblStepDescriptor.Text = step4Label;
                lblOutputFolder.Visible = false;
                txtOutputPath.Visible = false;
                btnOutputBrowse.Visible = false;
                lblConverting.Visible = true;
                progressGenerating.Visible = true;
                btnNext.Enabled = false;
                lblConvertNow.BackColor = lblDestinationFolder.BackColor;
                lblConvertNow.ForeColor = lblDestinationFolder.ForeColor;
                lblDestinationFolder.BackColor = lblAddAdditionalInput.BackColor;
                lblDestinationFolder.ForeColor = lblAddAdditionalInput.ForeColor;
                displayMode = DisplayMode.CONVERTING;

                GenerateKML();
            }
            else
            {
                //Switch to step 3 layout
                lblStepDescriptor.Text = step3Label;
                lblOutputFolder.Visible = true;
                txtOutputPath.Visible = true;
                btnOutputBrowse.Visible = true;
                lblConverting.Visible = false;
                progressGenerating.Visible = false;
                btnNext.Enabled = true;
                lblDestinationFolder.BackColor = lblConvertNow.BackColor;
                lblDestinationFolder.ForeColor = lblConvertNow.ForeColor;
                lblConvertNow.BackColor = lblAddAdditionalInput.BackColor;
                lblConvertNow.ForeColor = lblAddAdditionalInput.ForeColor;
                displayMode = DisplayMode.OUTPUT_PATH;
            }
        }

        private void GenerateKML()
        {
            log.Debug("Generating KML file");
            ProgressWrapper progress = new ProgressWrapper(progressGenerating);
            int response = controller.GenerateKMLFile(outputFilePath, progress);
            if (response == 0)
            {
                lblConverting.Text = "Complete";
                btnCancel.Text = "Finish";
                btnPrevious.Enabled = false;
            }
            else
            {
                MessageBox.Show(Messages.OUTPUT_FAILED);
            }
        }
    }
}
