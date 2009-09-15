namespace BlackCat
{
    partial class UILinkDataSource
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
            this.cbbExcelField = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbGeographicalField = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblStepDescriptor
            // 
            this.lblStepDescriptor.Size = new System.Drawing.Size(143, 15);
            this.lblStepDescriptor.Text = "Step #2b - Data Linking";
            // 
            // cbbExcelField
            // 
            this.cbbExcelField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbExcelField.FormattingEnabled = true;
            this.cbbExcelField.Location = new System.Drawing.Point(236, 259);
            this.cbbExcelField.Name = "cbbExcelField";
            this.cbbExcelField.Size = new System.Drawing.Size(284, 21);
            this.cbbExcelField.TabIndex = 136;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(233, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(294, 18);
            this.label8.TabIndex = 135;
            this.label8.Text = "Excel data field";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(233, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(294, 36);
            this.label7.TabIndex = 132;
            this.label7.Text = "Link the data sources together by choosing the data that is common to both files";
            // 
            // cbbGeographicalField
            // 
            this.cbbGeographicalField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGeographicalField.FormattingEnabled = true;
            this.cbbGeographicalField.Location = new System.Drawing.Point(236, 189);
            this.cbbGeographicalField.Name = "cbbGeographicalField";
            this.cbbGeographicalField.Size = new System.Drawing.Size(284, 21);
            this.cbbGeographicalField.TabIndex = 134;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(233, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(294, 18);
            this.label6.TabIndex = 133;
            this.label6.Text = "Geographical data field";
            // 
            // UILinkDataSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.cbbExcelField);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbbGeographicalField);
            this.Controls.Add(this.label6);
            this.Name = "UILinkDataSource";
            this.Load += new System.EventHandler(this.UILinkDataSource_Load);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cbbGeographicalField, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.cbbExcelField, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbExcelField;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbGeographicalField;
        private System.Windows.Forms.Label label6;
    }
}
