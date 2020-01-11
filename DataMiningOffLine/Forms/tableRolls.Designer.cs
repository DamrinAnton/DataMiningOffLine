namespace DataMiningOffLine.Forms
{
    partial class tableRolls
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
            this.calculate = new System.Windows.Forms.Button();
            this.deleteRoll = new System.Windows.Forms.Button();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.coord = new System.Windows.Forms.Label();
            this.addInformation = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridControl1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.length = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.length)).BeginInit();
            this.SuspendLayout();
            // 
            // calculate
            // 
            this.calculate.Location = new System.Drawing.Point(34, 254);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(131, 23);
            this.calculate.TabIndex = 16;
            this.calculate.Text = "Calculate Residence time";
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // deleteRoll
            // 
            this.deleteRoll.Location = new System.Drawing.Point(34, 138);
            this.deleteRoll.Name = "deleteRoll";
            this.deleteRoll.Size = new System.Drawing.Size(100, 23);
            this.deleteRoll.TabIndex = 15;
            this.deleteRoll.Text = "Delete Scheme";
            this.deleteRoll.Click += new System.EventHandler(this.deleteRoll_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(43, 182);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(91, 16);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Gap Height";
            // 
            // height
            // 
            this.height.DecimalPlaces = 2;
            this.height.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.height.Location = new System.Drawing.Point(34, 201);
            this.height.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.height.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(100, 20);
            this.height.TabIndex = 13;
            // 
            // coord
            // 
            this.coord.Location = new System.Drawing.Point(34, 30);
            this.coord.Name = "coord";
            this.coord.Size = new System.Drawing.Size(100, 13);
            this.coord.TabIndex = 7;
            this.coord.Text = "Roll Length";
            // 
            // addInformation
            // 
            this.addInformation.Location = new System.Drawing.Point(34, 109);
            this.addInformation.Name = "addInformation";
            this.addInformation.Size = new System.Drawing.Size(100, 23);
            this.addInformation.TabIndex = 12;
            this.addInformation.Text = "Add Scheme";
            this.addInformation.Click += new System.EventHandler(this.addInformation_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1286, 671);
            this.splitContainer1.SplitterDistance = 1078;
            this.splitContainer1.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.AllowUserToAddRows = false;
            this.gridControl1.AllowUserToDeleteRows = false;
            this.gridControl1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridControl1.BackgroundColor = System.Drawing.Color.White;
            this.gridControl1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MultiSelect = false;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridControl1.Size = new System.Drawing.Size(1078, 671);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GridControl1_DataError);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.calculate);
            this.groupBox1.Controls.Add(this.deleteRoll);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.height);
            this.groupBox1.Controls.Add(this.addInformation);
            this.groupBox1.Controls.Add(this.coord);
            this.groupBox1.Controls.Add(this.length);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 671);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add roll";
            // 
            // length
            // 
            this.length.DecimalPlaces = 2;
            this.length.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.length.Location = new System.Drawing.Point(34, 58);
            this.length.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.length.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.length.Name = "length";
            this.length.Size = new System.Drawing.Size(100, 20);
            this.length.TabIndex = 6;
            // 
            // tableRolls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 671);
            this.Controls.Add(this.splitContainer1);
            this.Name = "tableRolls";
            this.Text = "tableRolls";
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.length)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button calculate;
        private System.Windows.Forms.Button deleteRoll;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.NumericUpDown height;
        private System.Windows.Forms.Label coord;
        private System.Windows.Forms.Button addInformation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown length;
        private System.Windows.Forms.DataGridView gridControl1;
    }
}