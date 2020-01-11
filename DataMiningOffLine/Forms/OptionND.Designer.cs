namespace DataMiningOffLine
{
    partial class OptionND
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OnLogRadioButton = new System.Windows.Forms.RadioButton();
            this.OffLogRadioButton = new System.Windows.Forms.RadioButton();
            this.InverselyLogRadioButton = new System.Windows.Forms.RadioButton();
            this.MinMaxRadioButton = new System.Windows.Forms.RadioButton();
            this.SigmaRadioButton = new System.Windows.Forms.RadioButton();
            this.CriterionsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SignLevelTextBox = new System.Windows.Forms.TextBox();
            this.NoTransformRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InverselyLogRadioButton);
            this.groupBox1.Controls.Add(this.OffLogRadioButton);
            this.groupBox1.Controls.Add(this.OnLogRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 83);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logarithm";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NoTransformRadioButton);
            this.groupBox2.Controls.Add(this.SigmaRadioButton);
            this.groupBox2.Controls.Add(this.MinMaxRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(13, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 83);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data transformation";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SignLevelTextBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.CriterionsCheckedListBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 112);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Criterions";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(13, 319);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(143, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(171, 319);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(143, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OnLogRadioButton
            // 
            this.OnLogRadioButton.AutoSize = true;
            this.OnLogRadioButton.Location = new System.Drawing.Point(26, 37);
            this.OnLogRadioButton.Name = "OnLogRadioButton";
            this.OnLogRadioButton.Size = new System.Drawing.Size(39, 17);
            this.OnLogRadioButton.TabIndex = 0;
            this.OnLogRadioButton.TabStop = true;
            this.OnLogRadioButton.Text = "On";
            this.OnLogRadioButton.UseVisualStyleBackColor = true;
            // 
            // OffLogRadioButton
            // 
            this.OffLogRadioButton.AutoSize = true;
            this.OffLogRadioButton.Location = new System.Drawing.Point(124, 37);
            this.OffLogRadioButton.Name = "OffLogRadioButton";
            this.OffLogRadioButton.Size = new System.Drawing.Size(39, 17);
            this.OffLogRadioButton.TabIndex = 1;
            this.OffLogRadioButton.TabStop = true;
            this.OffLogRadioButton.Text = "Off";
            this.OffLogRadioButton.UseVisualStyleBackColor = true;
            // 
            // InverselyLogRadioButton
            // 
            this.InverselyLogRadioButton.AutoSize = true;
            this.InverselyLogRadioButton.Location = new System.Drawing.Point(217, 37);
            this.InverselyLogRadioButton.Name = "InverselyLogRadioButton";
            this.InverselyLogRadioButton.Size = new System.Drawing.Size(67, 17);
            this.InverselyLogRadioButton.TabIndex = 2;
            this.InverselyLogRadioButton.TabStop = true;
            this.InverselyLogRadioButton.Text = "Inversely";
            this.InverselyLogRadioButton.UseVisualStyleBackColor = true;
            // 
            // MinMaxRadioButton
            // 
            this.MinMaxRadioButton.AutoSize = true;
            this.MinMaxRadioButton.Location = new System.Drawing.Point(26, 38);
            this.MinMaxRadioButton.Name = "MinMaxRadioButton";
            this.MinMaxRadioButton.Size = new System.Drawing.Size(85, 17);
            this.MinMaxRadioButton.TabIndex = 3;
            this.MinMaxRadioButton.TabStop = true;
            this.MinMaxRadioButton.Text = "Min and max";
            this.MinMaxRadioButton.UseVisualStyleBackColor = true;
            // 
            // SigmaRadioButton
            // 
            this.SigmaRadioButton.AutoSize = true;
            this.SigmaRadioButton.Location = new System.Drawing.Point(124, 38);
            this.SigmaRadioButton.Name = "SigmaRadioButton";
            this.SigmaRadioButton.Size = new System.Drawing.Size(83, 17);
            this.SigmaRadioButton.TabIndex = 4;
            this.SigmaRadioButton.TabStop = true;
            this.SigmaRadioButton.Text = "Three sigma";
            this.SigmaRadioButton.UseVisualStyleBackColor = true;
            // 
            // CriterionsCheckedListBox
            // 
            this.CriterionsCheckedListBox.FormattingEnabled = true;
            this.CriterionsCheckedListBox.HorizontalScrollbar = true;
            this.CriterionsCheckedListBox.Location = new System.Drawing.Point(6, 21);
            this.CriterionsCheckedListBox.Name = "CriterionsCheckedListBox";
            this.CriterionsCheckedListBox.Size = new System.Drawing.Size(158, 79);
            this.CriterionsCheckedListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Significance level";
            // 
            // SignLevelTextBox
            // 
            this.SignLevelTextBox.Location = new System.Drawing.Point(184, 53);
            this.SignLevelTextBox.Name = "SignLevelTextBox";
            this.SignLevelTextBox.Size = new System.Drawing.Size(100, 20);
            this.SignLevelTextBox.TabIndex = 2;
            // 
            // NoTransformRadioButton
            // 
            this.NoTransformRadioButton.AutoSize = true;
            this.NoTransformRadioButton.Location = new System.Drawing.Point(217, 38);
            this.NoTransformRadioButton.Name = "NoTransformRadioButton";
            this.NoTransformRadioButton.Size = new System.Drawing.Size(39, 17);
            this.NoTransformRadioButton.TabIndex = 5;
            this.NoTransformRadioButton.TabStop = true;
            this.NoTransformRadioButton.Text = "No";
            this.NoTransformRadioButton.UseVisualStyleBackColor = true;
            // 
            // OptionND
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 354);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "OptionND";
            this.Text = "OptionND";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton InverselyLogRadioButton;
        private System.Windows.Forms.RadioButton OffLogRadioButton;
        private System.Windows.Forms.RadioButton OnLogRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton SigmaRadioButton;
        private System.Windows.Forms.RadioButton MinMaxRadioButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox SignLevelTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox CriterionsCheckedListBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.RadioButton NoTransformRadioButton;
    }
}