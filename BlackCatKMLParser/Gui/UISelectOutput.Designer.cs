namespace BlackCat
{
    partial class UISelectOutput
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
            this.btnOutputBrowse = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.saveKMLFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.progressGenerating = new System.Windows.Forms.ProgressBar();
            this.lblConverting = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDestinationFolder
            // 
            this.lblDestinationFolder.BackColor = System.Drawing.Color.MintCream;
            this.lblDestinationFolder.ForeColor = System.Drawing.Color.Black;
            // 
            // lblAddAdditionalInput
            // 
            this.lblAddAdditionalInput.BackColor = System.Drawing.Color.Teal;
            this.lblAddAdditionalInput.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblStepDescriptor
            // 
            this.lblStepDescriptor.Size = new System.Drawing.Size(202, 13);
            this.lblStepDescriptor.Text = "Step #3 Choose your output folder";
            // 
            // btnOutputBrowse
            // 
            this.btnOutputBrowse.Location = new System.Drawing.Point(474, 186);
            this.btnOutputBrowse.Name = "btnOutputBrowse";
            this.btnOutputBrowse.Size = new System.Drawing.Size(56, 28);
            this.btnOutputBrowse.TabIndex = 117;
            this.btnOutputBrowse.Text = "&Browse";
            this.btnOutputBrowse.UseVisualStyleBackColor = true;
            this.btnOutputBrowse.Click += new System.EventHandler(this.btnOutputBrowse_Click);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(234, 147);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(296, 20);
            this.txtOutputPath.TabIndex = 116;
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(231, 114);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(71, 13);
            this.lblOutputFolder.TabIndex = 115;
            this.lblOutputFolder.Text = "Output Folder";
            // 
            // saveKMLFileDialog
            // 
            this.saveKMLFileDialog.Filter = "KML file|*.kml";
            this.saveKMLFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveKMLFileDialog_FileOk);
            // 
            // progressGenerating
            // 
            this.progressGenerating.Location = new System.Drawing.Point(238, 219);
            this.progressGenerating.Name = "progressGenerating";
            this.progressGenerating.Size = new System.Drawing.Size(289, 23);
            this.progressGenerating.TabIndex = 119;
            this.progressGenerating.Visible = false;
            // 
            // lblConverting
            // 
            this.lblConverting.AutoSize = true;
            this.lblConverting.Location = new System.Drawing.Point(235, 201);
            this.lblConverting.Name = "lblConverting";
            this.lblConverting.Size = new System.Drawing.Size(67, 13);
            this.lblConverting.TabIndex = 118;
            this.lblConverting.Text = "Converting...";
            this.lblConverting.Visible = false;
            // 
            // UISelectOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.progressGenerating);
            this.Controls.Add(this.lblConverting);
            this.Controls.Add(this.btnOutputBrowse);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.lblOutputFolder);
            this.Name = "UISelectOutput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.lblOutputFolder, 0);
            this.Controls.SetChildIndex(this.txtOutputPath, 0);
            this.Controls.SetChildIndex(this.btnOutputBrowse, 0);
            this.Controls.SetChildIndex(this.lblConverting, 0);
            this.Controls.SetChildIndex(this.progressGenerating, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOutputBrowse;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.SaveFileDialog saveKMLFileDialog;
        private System.Windows.Forms.ProgressBar progressGenerating;
        private System.Windows.Forms.Label lblConverting;
    }
}
