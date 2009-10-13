namespace BlackCat
{
    partial class UIConvertKML
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
            this.progressGenerating = new System.Windows.Forms.ProgressBar();
            this.lblConverting = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblConvertNow
            // 
            this.lblConvertNow.BackColor = System.Drawing.Color.MintCream;
            this.lblConvertNow.ForeColor = System.Drawing.Color.Black;
            // 
            // lblAddAdditionalInput
            // 
            this.lblAddAdditionalInput.BackColor = System.Drawing.Color.Teal;
            this.lblAddAdditionalInput.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Enabled = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblStepDescriptor
            // 
            this.lblStepDescriptor.Size = new System.Drawing.Size(231, 13);
            this.lblStepDescriptor.Text = "Step #4 - Generating your new KML file";
            // 
            // progressGenerating
            // 
            this.progressGenerating.Location = new System.Drawing.Point(236, 174);
            this.progressGenerating.Name = "progressGenerating";
            this.progressGenerating.Size = new System.Drawing.Size(289, 23);
            this.progressGenerating.TabIndex = 116;
            this.progressGenerating.Value = 55;
            // 
            // lblConverting
            // 
            this.lblConverting.AutoSize = true;
            this.lblConverting.Location = new System.Drawing.Point(233, 156);
            this.lblConverting.Name = "lblConverting";
            this.lblConverting.Size = new System.Drawing.Size(67, 13);
            this.lblConverting.TabIndex = 115;
            this.lblConverting.Text = "Converting...";
            // 
            // UIConvertKML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.progressGenerating);
            this.Controls.Add(this.lblConverting);
            this.Name = "UIConvertKML";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UIConvertKML_Load);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.lblConverting, 0);
            this.Controls.SetChildIndex(this.progressGenerating, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressGenerating;
        private System.Windows.Forms.Label lblConverting;
    }
}
