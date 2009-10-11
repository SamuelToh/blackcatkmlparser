namespace BlackCat
{
    partial class UISelectSociologicalData
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtnShowNone = new System.Windows.Forms.RadioButton();
            this.rbtnShowWinner = new System.Windows.Forms.RadioButton();
            this.rbtnShowSafety = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.lblStepDescriptor.Location = new System.Drawing.Point(234, 77);
            this.lblStepDescriptor.Size = new System.Drawing.Size(232, 13);
            this.lblStepDescriptor.Text = "Step #2 - Select your sociological data.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(234, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 44);
            this.label2.TabIndex = 115;
            this.label2.Text = "Please select one of the following to be displayed on your output KML.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(237, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 116;
            this.label3.Text = "Sociological data";
            // 
            // rbtnShowNone
            // 
            this.rbtnShowNone.AutoSize = true;
            this.rbtnShowNone.Location = new System.Drawing.Point(246, 177);
            this.rbtnShowNone.Name = "rbtnShowNone";
            this.rbtnShowNone.Size = new System.Drawing.Size(221, 17);
            this.rbtnShowNone.TabIndex = 117;
            this.rbtnShowNone.TabStop = true;
            this.rbtnShowNone.Text = "Show none (Plain geographical data only)";
            this.rbtnShowNone.UseVisualStyleBackColor = true;
            // 
            // rbtnShowWinner
            // 
            this.rbtnShowWinner.AutoSize = true;
            this.rbtnShowWinner.Location = new System.Drawing.Point(246, 201);
            this.rbtnShowWinner.Name = "rbtnShowWinner";
            this.rbtnShowWinner.Size = new System.Drawing.Size(109, 17);
            this.rbtnShowWinner.TabIndex = 118;
            this.rbtnShowWinner.TabStop = true;
            this.rbtnShowWinner.Text = "Show seat winner";
            this.rbtnShowWinner.UseVisualStyleBackColor = true;
            // 
            // rbtnShowSafety
            // 
            this.rbtnShowSafety.AutoSize = true;
            this.rbtnShowSafety.Location = new System.Drawing.Point(246, 225);
            this.rbtnShowSafety.Name = "rbtnShowSafety";
            this.rbtnShowSafety.Size = new System.Drawing.Size(106, 17);
            this.rbtnShowSafety.TabIndex = 119;
            this.rbtnShowSafety.TabStop = true;
            this.rbtnShowSafety.Text = "Show seat safety";
            this.rbtnShowSafety.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(246, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 51);
            this.label4.TabIndex = 120;
            this.label4.Text = "Sorry, the loaded regions do not match the database sociological data.";
            this.label4.Visible = false;
            // 
            // UISelectSociologicalData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 370);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbtnShowWinner);
            this.Controls.Add(this.rbtnShowNone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rbtnShowSafety);
            this.Name = "UISelectSociologicalData";
            this.Text = "UISelectSociologicalData";
            this.VisibleChanged += new System.EventHandler(this.UISelectSociologicalData_VisibleChanged);
            this.Controls.SetChildIndex(this.rbtnShowSafety, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.rbtnShowNone, 0);
            this.Controls.SetChildIndex(this.rbtnShowWinner, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblStepDescriptor, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtnShowNone;
        private System.Windows.Forms.RadioButton rbtnShowWinner;
        private System.Windows.Forms.RadioButton rbtnShowSafety;
        private System.Windows.Forms.Label label4;
    }
}