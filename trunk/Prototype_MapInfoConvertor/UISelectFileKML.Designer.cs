namespace BlackCat
{
    partial class UISelectFileKML
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
            this.btnKMLBrowse = new System.Windows.Forms.Button();
            this.txtKmlFilePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.openKMLFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.lblStepDescriptor.Size = new System.Drawing.Size(150, 15);
            this.lblStepDescriptor.Text = "Step #1 - Select KML file";
            // 
            // btnKMLBrowse
            // 
            this.btnKMLBrowse.Location = new System.Drawing.Point(472, 179);
            this.btnKMLBrowse.Name = "btnKMLBrowse";
            this.btnKMLBrowse.Size = new System.Drawing.Size(56, 28);
            this.btnKMLBrowse.TabIndex = 122;
            this.btnKMLBrowse.Text = "&Browse";
            this.btnKMLBrowse.UseVisualStyleBackColor = true;
            this.btnKMLBrowse.Click += new System.EventHandler(this.btnKMLBrowse_Click);
            // 
            // txtKmlFilePath
            // 
            this.txtKmlFilePath.Location = new System.Drawing.Point(236, 153);
            this.txtKmlFilePath.Name = "txtKmlFilePath";
            this.txtKmlFilePath.Size = new System.Drawing.Size(289, 20);
            this.txtKmlFilePath.TabIndex = 121;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(233, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 120;
            this.label8.Text = "KML File";
            // 
            // openKMLFileDialog
            // 
            this.openKMLFileDialog.Filter = "\"KML files|*.kml\"";
            this.openKMLFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openKMLFileDialog_FileOk);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(280, 275);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(45, 13);
            this.lblLoading.TabIndex = 124;
            this.lblLoading.Text = "Loading";
            this.lblLoading.Visible = false;
            // 
            // progressLoading
            // 
            this.progressLoading.Location = new System.Drawing.Point(280, 294);
            this.progressLoading.Name = "progressLoading";
            this.progressLoading.Size = new System.Drawing.Size(237, 23);
            this.progressLoading.TabIndex = 123;
            this.progressLoading.Visible = false;
            // 
            // UISelectFileKML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.progressLoading);
            this.Controls.Add(this.btnKMLBrowse);
            this.Controls.Add(this.txtKmlFilePath);
            this.Controls.Add(this.label8);
            this.Name = "UISelectFileKML";
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtKmlFilePath, 0);
            this.Controls.SetChildIndex(this.btnKMLBrowse, 0);
            this.Controls.SetChildIndex(this.progressLoading, 0);
            this.Controls.SetChildIndex(this.lblLoading, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKMLBrowse;
        private System.Windows.Forms.TextBox txtKmlFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog openKMLFileDialog;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.ProgressBar progressLoading;
    }
}
