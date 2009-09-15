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
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            // 
            // lblStepDescriptor
            // 
            this.lblStepDescriptor.Size = new System.Drawing.Size(235, 15);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(233, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 115;
            this.label7.Text = "Converting...";
            // 
            // UIConvertKML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.progressGenerating);
            this.Controls.Add(this.label7);
            this.Name = "UIConvertKML";
            this.Load += new System.EventHandler(this.UIConvertKML_Load);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.progressGenerating, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressGenerating;
        private System.Windows.Forms.Label label7;
    }
}
