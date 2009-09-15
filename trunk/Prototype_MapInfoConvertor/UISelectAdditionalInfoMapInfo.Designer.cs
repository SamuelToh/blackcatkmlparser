namespace BlackCat
{
    partial class UISelectAdditionalInfoMapInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label9 = new System.Windows.Forms.Label();
            this.btnExcelBrowse = new System.Windows.Forms.Button();
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.openExcelFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressLoading = new System.Windows.Forms.ProgressBar();
            this.lblLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAddAdditionalInput
            // 
            this.lblAddAdditionalInput.BackColor = System.Drawing.Color.MintCream;
            // 
            // btnNext
            // 
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblStepDescriptor
            // 
            this.lblStepDescriptor.Size = new System.Drawing.Size(191, 15);
            this.lblStepDescriptor.Text = "Step #2 - Select your Excel file.";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(233, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(292, 50);
            this.label9.TabIndex = 118;
            this.label9.Text = "Choose your Excel file to add additional information.";
            // 
            // btnExcelBrowse
            // 
            this.btnExcelBrowse.Location = new System.Drawing.Point(478, 223);
            this.btnExcelBrowse.Name = "btnExcelBrowse";
            this.btnExcelBrowse.Size = new System.Drawing.Size(56, 28);
            this.btnExcelBrowse.TabIndex = 117;
            this.btnExcelBrowse.Text = "&Browse";
            this.btnExcelBrowse.UseVisualStyleBackColor = true;
            this.btnExcelBrowse.Click += new System.EventHandler(this.btnExcelBrowse_Click);
            // 
            // txtExcelFile
            // 
            this.txtExcelFile.Location = new System.Drawing.Point(237, 197);
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.Size = new System.Drawing.Size(298, 20);
            this.txtExcelFile.TabIndex = 116;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(234, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 115;
            this.label7.Text = "Selected Excel File";
            // 
            // openExcelFileDialog
            // 
            this.openExcelFileDialog.Filter = "Excel 98 files|*.xls|All Files|*";
            this.openExcelFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openExcelFileDialog_FileOk);
            // 
            // progressLoading
            // 
            this.progressLoading.Location = new System.Drawing.Point(280, 294);
            this.progressLoading.Name = "progressLoading";
            this.progressLoading.Size = new System.Drawing.Size(237, 23);
            this.progressLoading.TabIndex = 119;
            this.progressLoading.Visible = false;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(280, 275);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(45, 13);
            this.lblLoading.TabIndex = 120;
            this.lblLoading.Text = "Loading";
            this.lblLoading.Visible = false;
            // 
            // UISelectAdditionalInfoMapInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.progressLoading);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnExcelBrowse);
            this.Controls.Add(this.txtExcelFile);
            this.Controls.Add(this.label7);
            this.Name = "UISelectAdditionalInfoMapInfo";
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtExcelFile, 0);
            this.Controls.SetChildIndex(this.btnExcelBrowse, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.progressLoading, 0);
            this.Controls.SetChildIndex(this.lblLoading, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnExcelBrowse;
        private System.Windows.Forms.TextBox txtExcelFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openExcelFileDialog;
        private System.Windows.Forms.ProgressBar progressLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
