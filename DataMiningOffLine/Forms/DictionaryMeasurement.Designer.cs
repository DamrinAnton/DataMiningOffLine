namespace DataMiningOffLine
{
    partial class DictionaryMeasurement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictionaryMeasurement));
            this.gridMeasurements = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridMeasurements)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMeasurements
            // 
            this.gridMeasurements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridMeasurements.BackgroundColor = System.Drawing.Color.White;
            this.gridMeasurements.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridMeasurements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridMeasurements, "gridMeasurements");
            this.gridMeasurements.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridMeasurements.MultiSelect = false;
            this.gridMeasurements.Name = "gridMeasurements";
            this.gridMeasurements.ReadOnly = true;
            this.gridMeasurements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridMeasurements.ShowCellErrors = false;
            this.gridMeasurements.ShowCellToolTips = false;
            this.gridMeasurements.ShowEditingIcon = false;
            this.gridMeasurements.ShowRowErrors = false;
            // 
            // DictionaryMeasurement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridMeasurements);
            this.Name = "DictionaryMeasurement";
            this.Shown += new System.EventHandler(this.DictionaryMeasurement_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridMeasurements)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMeasurements;
    }
}