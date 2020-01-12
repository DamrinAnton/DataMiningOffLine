namespace DataMiningOffLine.Forms.RegressionAnalysis
{
    partial class ModelNameDateSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelNameDateSelection));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxModelName = new System.Windows.Forms.TextBox();
            this.dateTimePickerModelCreationDate = new System.Windows.Forms.DateTimePicker();
            this.buttonSaveModel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Creation date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxModelName
            // 
            this.textBoxModelName.Location = new System.Drawing.Point(105, 8);
            this.textBoxModelName.Name = "textBoxModelName";
            this.textBoxModelName.Size = new System.Drawing.Size(267, 23);
            this.textBoxModelName.TabIndex = 1;
            this.textBoxModelName.Text = "DefaultName";
            // 
            // dateTimePickerModelCreationDate
            // 
            this.dateTimePickerModelCreationDate.Location = new System.Drawing.Point(105, 37);
            this.dateTimePickerModelCreationDate.Name = "dateTimePickerModelCreationDate";
            this.dateTimePickerModelCreationDate.Size = new System.Drawing.Size(267, 23);
            this.dateTimePickerModelCreationDate.TabIndex = 2;
            // 
            // buttonSaveModel
            // 
            this.buttonSaveModel.Location = new System.Drawing.Point(122, 81);
            this.buttonSaveModel.Name = "buttonSaveModel";
            this.buttonSaveModel.Size = new System.Drawing.Size(111, 23);
            this.buttonSaveModel.TabIndex = 3;
            this.buttonSaveModel.Text = "Save model";
            this.buttonSaveModel.UseVisualStyleBackColor = true;
            this.buttonSaveModel.Click += new System.EventHandler(this.buttonSaveModel_Click);
            // 
            // ModelNameDateSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 121);
            this.Controls.Add(this.buttonSaveModel);
            this.Controls.Add(this.dateTimePickerModelCreationDate);
            this.Controls.Add(this.textBoxModelName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelNameDateSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter regression model info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxModelName;
        private System.Windows.Forms.DateTimePicker dateTimePickerModelCreationDate;
        private System.Windows.Forms.Button buttonSaveModel;
    }
}