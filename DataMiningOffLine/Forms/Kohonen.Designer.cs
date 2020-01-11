namespace DataMiningOffLine
{
    partial class Kohonen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kohonen));
            this.splitContainerControl1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.yMap = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.sampleSize = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rangeOfLearning = new System.Windows.Forms.TextBox();
            this.xMap = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numberOfTrainingCycles = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numberOfNeurons = new System.Windows.Forms.TextBox();
            this.dimensionOfVector = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mapRun = new System.Windows.Forms.Button();
            this.loadMeasurements = new System.Windows.Forms.Button();
            this.alfa = new System.Windows.Forms.TextBox();
            this.splitContainerControl2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ListDelete = new System.Windows.Forms.ListBox();
            this.simpleButton4 = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addAllButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.ListAdd = new System.Windows.Forms.ListBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            this.splitContainerControl1.SplitterDistance = 162;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.mapRun, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.loadMeasurements, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.alfa, 2, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.yMap);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.sampleSize);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rangeOfLearning);
            this.groupBox1.Controls.Add(this.xMap);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numberOfTrainingCycles);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numberOfNeurons);
            this.groupBox1.Controls.Add(this.dimensionOfVector);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // yMap
            // 
            resources.ApplyResources(this.yMap, "yMap");
            this.yMap.Name = "yMap";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // sampleSize
            // 
            resources.ApplyResources(this.sampleSize, "sampleSize");
            this.sampleSize.Name = "sampleSize";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // rangeOfLearning
            // 
            resources.ApplyResources(this.rangeOfLearning, "rangeOfLearning");
            this.rangeOfLearning.Name = "rangeOfLearning";
            // 
            // xMap
            // 
            resources.ApplyResources(this.xMap, "xMap");
            this.xMap.Name = "xMap";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // numberOfTrainingCycles
            // 
            resources.ApplyResources(this.numberOfTrainingCycles, "numberOfTrainingCycles");
            this.numberOfTrainingCycles.Name = "numberOfTrainingCycles";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // numberOfNeurons
            // 
            resources.ApplyResources(this.numberOfNeurons, "numberOfNeurons");
            this.numberOfNeurons.Name = "numberOfNeurons";
            // 
            // dimensionOfVector
            // 
            resources.ApplyResources(this.dimensionOfVector, "dimensionOfVector");
            this.dimensionOfVector.Name = "dimensionOfVector";
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
            // mapRun
            // 
            resources.ApplyResources(this.mapRun, "mapRun");
            this.mapRun.Name = "mapRun";
            this.mapRun.UseVisualStyleBackColor = true;
            this.mapRun.Click += new System.EventHandler(this.mapRun_Click);
            // 
            // loadMeasurements
            // 
            resources.ApplyResources(this.loadMeasurements, "loadMeasurements");
            this.loadMeasurements.Name = "loadMeasurements";
            this.loadMeasurements.UseVisualStyleBackColor = true;
            this.loadMeasurements.Click += new System.EventHandler(this.loadMeasurements_Click);
            // 
            // alfa
            // 
            resources.ApplyResources(this.alfa, "alfa");
            this.alfa.Name = "alfa";
            // 
            // splitContainerControl2
            // 
            resources.ApplyResources(this.splitContainerControl2, "splitContainerControl2");
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.splitContainerControl2.Panel1, "splitContainerControl2.Panel1");
            this.splitContainerControl2.Panel2.Controls.Add(this.dataGridView2);
            resources.ApplyResources(this.splitContainerControl2.Panel2, "splitContainerControl2.Panel2");
            this.splitContainerControl2.SplitterDistance = 593;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.ListDelete, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.simpleButton4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.deleteButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.addAllButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.addButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ListAdd, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // ListDelete
            // 
            resources.ApplyResources(this.ListDelete, "ListDelete");
            this.ListDelete.Name = "ListDelete";
            this.tableLayoutPanel1.SetRowSpan(this.ListDelete, 5);
            // 
            // simpleButton4
            // 
            resources.ApplyResources(this.simpleButton4, "simpleButton4");
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Click += new System.EventHandler(this.DeleteAllItems);
            // 
            // deleteButton
            // 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Click += new System.EventHandler(this.DeleteOneItem);
            // 
            // addAllButton
            // 
            resources.ApplyResources(this.addAllButton, "addAllButton");
            this.addAllButton.Name = "addAllButton";
            this.addAllButton.Click += new System.EventHandler(this.MoveItems);
            // 
            // addButton
            // 
            resources.ApplyResources(this.addButton, "addButton");
            this.addButton.Name = "addButton";
            this.addButton.Click += new System.EventHandler(this.MoveItem);
            // 
            // ListAdd
            // 
            resources.ApplyResources(this.ListAdd, "ListAdd");
            this.ListAdd.Name = "ListAdd";
            this.tableLayoutPanel1.SetRowSpan(this.ListAdd, 5);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            resources.ApplyResources(this.dataGridView2, "dataGridView2");
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Kohonen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "Kohonen";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerControl1;
        private System.Windows.Forms.SplitContainer splitContainerControl2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox ListAdd;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button addAllButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button simpleButton4;
        private System.Windows.Forms.ListBox ListDelete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox yMap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox sampleSize;
        private System.Windows.Forms.Label label9;
        protected internal System.Windows.Forms.TextBox rangeOfLearning;
        private System.Windows.Forms.TextBox xMap;
        private System.Windows.Forms.Label label6;
        protected internal System.Windows.Forms.TextBox numberOfTrainingCycles;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        protected internal System.Windows.Forms.TextBox numberOfNeurons;
        protected internal System.Windows.Forms.TextBox dimensionOfVector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button mapRun;
        private System.Windows.Forms.Button loadMeasurements;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TextBox alfa;
        private System.Windows.Forms.Label label3;
    }
}