namespace DataMiningOffLine
{
    partial class TestForm
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
            this.min = new System.Windows.Forms.Label();
            this.max = new System.Windows.Forms.Label();
            this.currentValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // min
            // 
            this.min.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                    ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.min.AutoSize = true;
            this.min.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.min.Location = new System.Drawing.Point(2, 141);
            this.min.Name = "min";
            this.min.Size = new System.Drawing.Size(23, 13);
            this.min.TabIndex = 12;
            this.min.Text = "min";
            this.min.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // max
            // 
            this.max.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                    ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.max.AutoSize = true;
            this.max.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.max.Location = new System.Drawing.Point(185, 141);
            this.max.Name = "max";
            this.max.Size = new System.Drawing.Size(26, 13);
            this.max.TabIndex = 13;
            this.max.Text = "max";
            this.max.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // currentValue
            // 
            this.currentValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.currentValue.AutoSize = true;
            this.currentValue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.currentValue.Location = new System.Drawing.Point(100, 140);
            this.currentValue.Name = "currentValue";
            this.currentValue.Size = new System.Drawing.Size(0, 13);
            this.currentValue.TabIndex = 14;
            this.currentValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 164);
            this.Controls.Add(this.currentValue);
            this.Controls.Add(this.max);
            this.Controls.Add(this.min);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Карты";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label min;
        private System.Windows.Forms.Label max;
        private System.Windows.Forms.Label currentValue;
    }
}