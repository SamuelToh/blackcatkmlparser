namespace BlackCat
{
    partial class UISelectFileMapInfo
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
            this.btnMifBrowse = new System.Windows.Forms.Button();
            this.btnMidBrowse = new System.Windows.Forms.Button();
            this.txtMifFilePath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMidFilePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.openMidFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openMifFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblLoading = new System.Windows.Forms.Label();
            this.progressLoading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblConvertNow
            // 
            this.lblConvertNow.ForeColor = System.Drawing.Color.White;
            // 
            // lblDestinationFolder
            // 
            this.lblDestinationFolder.ForeColor = System.Drawing.Color.White;
            // 
            // lblAddAdditionalInput
            // 
            this.lblAddAdditionalInput.BackColor = System.Drawing.Color.Teal;
            this.lblAddAdditionalInput.ForeColor = System.Drawing.Color.White;
            // 
            // lblSelectFiles
            // 
            this.lblSelectFiles.BackColor = System.Drawing.Color.MintCream;
            this.lblSelectFiles.ForeColor = System.Drawing.Color.Black;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Enabled = false;
            // 
            // btnNext
            // 
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblStepDescriptor
            // 
            this.lblStepDescriptor.Size = new System.Drawing.Size(254, 13);
            this.lblStepDescriptor.Text = "Step #1 - Select MapInfo geographical files";
            // 
            // btnMifBrowse
            // 
            this.btnMifBrowse.Location = new System.Drawing.Point(472, 240);
            this.btnMifBrowse.Name = "btnMifBrowse";
            this.btnMifBrowse.Size = new System.Drawing.Size(56, 28);
            this.btnMifBrowse.TabIndex = 120;
            this.btnMifBrowse.Text = "B&rowse";
            this.btnMifBrowse.UseVisualStyleBackColor = true;
            this.btnMifBrowse.Click += new System.EventHandler(this.btnMifBrowse_Click);
            // 
            // btnMidBrowse
            // 
            this.btnMidBrowse.Location = new System.Drawing.Point(472, 163);
            this.btnMidBrowse.Name = "btnMidBrowse";
            this.btnMidBrowse.Size = new System.Drawing.Size(56, 28);
            this.btnMidBrowse.TabIndex = 119;
            this.btnMidBrowse.Text = "&Browse";
            this.btnMidBrowse.UseVisualStyleBackColor = true;
            this.btnMidBrowse.Click += new System.EventHandler(this.btnMidBrowse_Click);
            // 
            // txtMifFilePath
            // 
            this.txtMifFilePath.Location = new System.Drawing.Point(236, 214);
            this.txtMifFilePath.Name = "txtMifFilePath";
            this.txtMifFilePath.Size = new System.Drawing.Size(289, 20);
            this.txtMifFilePath.TabIndex = 118;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(233, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 117;
            this.label9.Text = "Mif File";
            // 
            // txtMidFilePath
            // 
            this.txtMidFilePath.Location = new System.Drawing.Point(236, 137);
            this.txtMidFilePath.Name = "txtMidFilePath";
            this.txtMidFilePath.Size = new System.Drawing.Size(289, 20);
            this.txtMidFilePath.TabIndex = 116;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(233, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 115;
            this.label8.Text = "Mid File";
            // 
            // openMidFileDialog
            // 
            this.openMidFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openMidFileDialog_FileOk);
            // 
            // openMifFileDialog
            // 
            this.openMifFileDialog.FileName = "openFileDialog2";
            this.openMifFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openMifFileDialog_FileOk);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(280, 277);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(45, 13);
            this.lblLoading.TabIndex = 122;
            this.lblLoading.Text = "Loading";
            this.lblLoading.Visible = false;
            // 
            // progressLoading
            // 
            this.progressLoading.Location = new System.Drawing.Point(280, 296);
            this.progressLoading.Name = "progressLoading";
            this.progressLoading.Size = new System.Drawing.Size(237, 23);
            this.progressLoading.TabIndex = 121;
            this.progressLoading.Visible = false;
            // 
            // UISelectFileMapInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.progressLoading);
            this.Controls.Add(this.btnMifBrowse);
            this.Controls.Add(this.btnMidBrowse);
            this.Controls.Add(this.txtMifFilePath);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtMidFilePath);
            this.Controls.Add(this.label8);
            this.Name = "UISelectFileMapInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtMidFilePath, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtMifFilePath, 0);
            this.Controls.SetChildIndex(this.btnMidBrowse, 0);
            this.Controls.SetChildIndex(this.btnMifBrowse, 0);
            this.Controls.SetChildIndex(this.progressLoading, 0);
            this.Controls.SetChildIndex(this.lblLoading, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMifBrowse;
        private System.Windows.Forms.Button btnMidBrowse;
        private System.Windows.Forms.TextBox txtMifFilePath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMidFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog openMidFileDialog;
        private System.Windows.Forms.OpenFileDialog openMifFileDialog;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.ProgressBar progressLoading;
    }
}
