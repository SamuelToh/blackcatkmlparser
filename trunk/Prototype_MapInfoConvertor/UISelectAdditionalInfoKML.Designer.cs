namespace BlackCat
{
    partial class UISelectAdditionalInfoKML
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.openExcelFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblLoading = new System.Windows.Forms.Label();
            this.progressLoading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblStepDescriptor
            // 
            this.lblStepDescriptor.Size = new System.Drawing.Size(191, 15);
            this.lblStepDescriptor.Text = "Step #2 - Select your Excel File";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(477, 232);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(56, 28);
            this.btnBrowse.TabIndex = 118;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(232, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(292, 50);
            this.label9.TabIndex = 117;
            this.label9.Text = "Choose an Excel file if you wish to add additional information. Otherwise press N" +
                "ext to go to step 3";
            // 
            // txtExcelFile
            // 
            this.txtExcelFile.Location = new System.Drawing.Point(236, 206);
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.Size = new System.Drawing.Size(297, 20);
            this.txtExcelFile.TabIndex = 116;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(233, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 115;
            this.label7.Text = "Excel File";
            // 
            // openExcelFileDialog
            // 
            this.openExcelFileDialog.Filter = "\"Excel 98 files|*.xls|All Files|*\"";
            this.openExcelFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openExcelFileDialog_FileOk);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(280, 272);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(45, 13);
            this.lblLoading.TabIndex = 122;
            this.lblLoading.Text = "Loading";
            this.lblLoading.Visible = false;
            // 
            // progressLoading
            // 
            this.progressLoading.Location = new System.Drawing.Point(280, 291);
            this.progressLoading.Name = "progressLoading";
            this.progressLoading.Size = new System.Drawing.Size(237, 23);
            this.progressLoading.TabIndex = 121;
            this.progressLoading.Visible = false;
            // 
            // UISelectAdditionalInfoKML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.progressLoading);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtExcelFile);
            this.Controls.Add(this.label7);
            this.Name = "UISelectAdditionalInfoKML";
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtExcelFile, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.btnBrowse, 0);
            this.Controls.SetChildIndex(this.progressLoading, 0);
            this.Controls.SetChildIndex(this.lblLoading, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtExcelFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openExcelFileDialog;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.ProgressBar progressLoading;
    }
}
