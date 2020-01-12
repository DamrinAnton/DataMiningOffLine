namespace DataMiningOffLine.Forms.RegressionAnalysis
{
    partial class RegressionModelsDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegressionModelsDatabase));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelRegressionModelId = new System.Windows.Forms.Label();
            this.labelRegressionModelCreationDate = new System.Windows.Forms.Label();
            this.labelRegressionModelName = new System.Windows.Forms.Label();
            this.labelOutputParameterName = new System.Windows.Forms.Label();
            this.labelCalculatedRMSEValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridViewInputParametersInfo = new System.Windows.Forms.DataGridView();
            this.buttonSelectFirstModel = new System.Windows.Forms.Button();
            this.buttonSelectPreviousModel = new System.Windows.Forms.Button();
            this.buttonSelectNextModel = new System.Windows.Forms.Button();
            this.buttonSelectLastModel = new System.Windows.Forms.Button();
            this.buttonSelectModel = new System.Windows.Forms.Button();
            this.buttonDeleteCurrentModel = new System.Windows.Forms.Button();
            this.buttonCreateBackup = new System.Windows.Forms.Button();
            this.buttonLoadBackup = new System.Windows.Forms.Button();
            this.openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogMain = new System.Windows.Forms.SaveFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.labelNormalizeValues = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputParametersInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // labelRegressionModelId
            // 
            resources.ApplyResources(this.labelRegressionModelId, "labelRegressionModelId");
            this.labelRegressionModelId.Name = "labelRegressionModelId";
            // 
            // labelRegressionModelCreationDate
            // 
            resources.ApplyResources(this.labelRegressionModelCreationDate, "labelRegressionModelCreationDate");
            this.labelRegressionModelCreationDate.Name = "labelRegressionModelCreationDate";
            // 
            // labelRegressionModelName
            // 
            resources.ApplyResources(this.labelRegressionModelName, "labelRegressionModelName");
            this.labelRegressionModelName.Name = "labelRegressionModelName";
            // 
            // labelOutputParameterName
            // 
            resources.ApplyResources(this.labelOutputParameterName, "labelOutputParameterName");
            this.labelOutputParameterName.Name = "labelOutputParameterName";
            // 
            // labelCalculatedRMSEValue
            // 
            resources.ApplyResources(this.labelCalculatedRMSEValue, "labelCalculatedRMSEValue");
            this.labelCalculatedRMSEValue.Name = "labelCalculatedRMSEValue";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // dataGridViewInputParametersInfo
            // 
            resources.ApplyResources(this.dataGridViewInputParametersInfo, "dataGridViewInputParametersInfo");
            this.dataGridViewInputParametersInfo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewInputParametersInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInputParametersInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewInputParametersInfo.Name = "dataGridViewInputParametersInfo";
            this.dataGridViewInputParametersInfo.RowHeadersVisible = false;
            this.dataGridViewInputParametersInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            // 
            // buttonSelectFirstModel
            // 
            resources.ApplyResources(this.buttonSelectFirstModel, "buttonSelectFirstModel");
            this.buttonSelectFirstModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.buttonSelectFirstModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSelectFirstModel.Name = "buttonSelectFirstModel";
            this.buttonSelectFirstModel.UseVisualStyleBackColor = true;
            this.buttonSelectFirstModel.Click += new System.EventHandler(this.buttonSelectFirstModel_Click);
            // 
            // buttonSelectPreviousModel
            // 
            resources.ApplyResources(this.buttonSelectPreviousModel, "buttonSelectPreviousModel");
            this.buttonSelectPreviousModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.buttonSelectPreviousModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSelectPreviousModel.Name = "buttonSelectPreviousModel";
            this.buttonSelectPreviousModel.UseVisualStyleBackColor = true;
            this.buttonSelectPreviousModel.Click += new System.EventHandler(this.buttonSelectPreviousModel_Click);
            // 
            // buttonSelectNextModel
            // 
            resources.ApplyResources(this.buttonSelectNextModel, "buttonSelectNextModel");
            this.buttonSelectNextModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.buttonSelectNextModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSelectNextModel.Name = "buttonSelectNextModel";
            this.buttonSelectNextModel.UseVisualStyleBackColor = true;
            this.buttonSelectNextModel.Click += new System.EventHandler(this.buttonSelectNextModel_Click);
            // 
            // buttonSelectLastModel
            // 
            resources.ApplyResources(this.buttonSelectLastModel, "buttonSelectLastModel");
            this.buttonSelectLastModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.buttonSelectLastModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSelectLastModel.Name = "buttonSelectLastModel";
            this.buttonSelectLastModel.UseVisualStyleBackColor = true;
            this.buttonSelectLastModel.Click += new System.EventHandler(this.buttonSelectLastModel_Click);
            // 
            // buttonSelectModel
            // 
            resources.ApplyResources(this.buttonSelectModel, "buttonSelectModel");
            this.buttonSelectModel.Name = "buttonSelectModel";
            this.buttonSelectModel.UseVisualStyleBackColor = true;
            this.buttonSelectModel.Click += new System.EventHandler(this.buttonSelectModel_Click);
            // 
            // buttonDeleteCurrentModel
            // 
            resources.ApplyResources(this.buttonDeleteCurrentModel, "buttonDeleteCurrentModel");
            this.buttonDeleteCurrentModel.Name = "buttonDeleteCurrentModel";
            this.buttonDeleteCurrentModel.UseVisualStyleBackColor = true;
            this.buttonDeleteCurrentModel.Click += new System.EventHandler(this.buttonDeleteCurrentModel_Click);
            // 
            // buttonCreateBackup
            // 
            resources.ApplyResources(this.buttonCreateBackup, "buttonCreateBackup");
            this.buttonCreateBackup.Name = "buttonCreateBackup";
            this.buttonCreateBackup.UseVisualStyleBackColor = true;
            this.buttonCreateBackup.Click += new System.EventHandler(this.buttonCreateBackup_Click);
            // 
            // buttonLoadBackup
            // 
            resources.ApplyResources(this.buttonLoadBackup, "buttonLoadBackup");
            this.buttonLoadBackup.Name = "buttonLoadBackup";
            this.buttonLoadBackup.UseVisualStyleBackColor = true;
            this.buttonLoadBackup.Click += new System.EventHandler(this.buttonLoadBackup_Click);
            // 
            // openFileDialogMain
            // 
            this.openFileDialogMain.FileName = "Backup";
            resources.ApplyResources(this.openFileDialogMain, "openFileDialogMain");
            // 
            // saveFileDialogMain
            // 
            this.saveFileDialogMain.FileName = "Backup";
            resources.ApplyResources(this.saveFileDialogMain, "saveFileDialogMain");
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // labelNormalizeValues
            // 
            resources.ApplyResources(this.labelNormalizeValues, "labelNormalizeValues");
            this.labelNormalizeValues.Name = "labelNormalizeValues";
            // 
            // RegressionModelsDatabase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonLoadBackup);
            this.Controls.Add(this.buttonCreateBackup);
            this.Controls.Add(this.buttonDeleteCurrentModel);
            this.Controls.Add(this.buttonSelectModel);
            this.Controls.Add(this.buttonSelectLastModel);
            this.Controls.Add(this.buttonSelectNextModel);
            this.Controls.Add(this.buttonSelectPreviousModel);
            this.Controls.Add(this.buttonSelectFirstModel);
            this.Controls.Add(this.dataGridViewInputParametersInfo);
            this.Controls.Add(this.labelNormalizeValues);
            this.Controls.Add(this.labelCalculatedRMSEValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelOutputParameterName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelRegressionModelName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelRegressionModelCreationDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelRegressionModelId);
            this.Controls.Add(this.label1);
            this.Name = "RegressionModelsDatabase";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputParametersInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelRegressionModelId;
        private System.Windows.Forms.Label labelRegressionModelCreationDate;
        private System.Windows.Forms.Label labelRegressionModelName;
        private System.Windows.Forms.Label labelOutputParameterName;
        private System.Windows.Forms.Label labelCalculatedRMSEValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridViewInputParametersInfo;
        private System.Windows.Forms.Button buttonSelectFirstModel;
        private System.Windows.Forms.Button buttonSelectPreviousModel;
        private System.Windows.Forms.Button buttonSelectNextModel;
        private System.Windows.Forms.Button buttonSelectLastModel;
        private System.Windows.Forms.Button buttonSelectModel;
        private System.Windows.Forms.Button buttonDeleteCurrentModel;
        private System.Windows.Forms.Button buttonCreateBackup;
        private System.Windows.Forms.Button buttonLoadBackup;
        private System.Windows.Forms.OpenFileDialog openFileDialogMain;
        private System.Windows.Forms.SaveFileDialog saveFileDialogMain;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelNormalizeValues;
    }
}