namespace DataMiningOffLine.Forms
{
    partial class ESAlgs
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
            this.ESList = new System.Windows.Forms.ListBox();
            this.ESpb = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.ESpb)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ESList
            // 
            this.ESList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ESList.FormattingEnabled = true;
            this.ESList.ItemHeight = 16;
            this.ESList.Location = new System.Drawing.Point(0, 0);
            this.ESList.Name = "ESList";
            this.ESList.Size = new System.Drawing.Size(142, 565);
            this.ESList.TabIndex = 2;
            this.ESList.SelectedIndexChanged += new System.EventHandler(this.ESList_SelectedIndexChanged);
            // 
            // ESpb
            // 
            this.ESpb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ESpb.Location = new System.Drawing.Point(3, 3);
            this.ESpb.Name = "ESpb";
            this.ESpb.Size = new System.Drawing.Size(100, 50);
            this.ESpb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ESpb.TabIndex = 0;
            this.ESpb.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.ESpb);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(142, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(964, 565);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // ESAlgs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 565);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ESList);
            this.Name = "ESAlgs";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ESAlgs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ESpb)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox ESList;
        private System.Windows.Forms.PictureBox ESpb;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}