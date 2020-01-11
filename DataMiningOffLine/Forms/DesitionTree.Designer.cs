namespace DataMiningOffLine
{
    partial class DesitionTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesitionTree));
            this.splitContainerControl1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.criterion = new System.Windows.Forms.NumericUpDown();
            this.textEdit1 = new System.Windows.Forms.NumericUpDown();
            this.ListDelete = new System.Windows.Forms.ListBox();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.ListAdd = new System.Windows.Forms.ListBox();
            this.simpleButton4 = new System.Windows.Forms.Button();
            this.simpleButton3 = new System.Windows.Forms.Button();
            this.simpleButton2 = new System.Windows.Forms.Button();
            this.simpleButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.parameterCondition = new System.Windows.Forms.ComboBox();
            this.numberOfForests = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.simpleButton8 = new System.Windows.Forms.Button();
            this.simpleButton5 = new System.Windows.Forms.Button();
            this.histogramm = new System.Windows.Forms.Button();
            this.trainButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.criterion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfForests)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.panel1);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.criterion, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.textEdit1, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.ListDelete, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ListAdd, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.simpleButton4, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.simpleButton3, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.simpleButton2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.simpleButton1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.parameterCondition, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numberOfForests, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.simpleButton8, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.simpleButton5, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.histogramm, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.trainButton, 4, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // criterion
            // 
            this.criterion.DecimalPlaces = 2;
            resources.ApplyResources(this.criterion, "criterion");
            this.criterion.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.criterion.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.criterion.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.criterion.Name = "criterion";
            // 
            // textEdit1
            // 
            this.textEdit1.DecimalPlaces = 2;
            resources.ApplyResources(this.textEdit1, "textEdit1");
            this.textEdit1.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textEdit1.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textEdit1.Name = "textEdit1";
            // 
            // ListDelete
            // 
            resources.ApplyResources(this.ListDelete, "ListDelete");
            this.ListDelete.Name = "ListDelete";
            this.tableLayoutPanel1.SetRowSpan(this.ListDelete, 5);
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // ListAdd
            // 
            resources.ApplyResources(this.ListAdd, "ListAdd");
            this.ListAdd.Name = "ListAdd";
            this.tableLayoutPanel1.SetRowSpan(this.ListAdd, 5);
            // 
            // simpleButton4
            // 
            resources.ApplyResources(this.simpleButton4, "simpleButton4");
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Click += new System.EventHandler(this.DeleteAllItems);
            // 
            // simpleButton3
            // 
            resources.ApplyResources(this.simpleButton3, "simpleButton3");
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Click += new System.EventHandler(this.DeleteOneItem);
            // 
            // simpleButton2
            // 
            resources.ApplyResources(this.simpleButton2, "simpleButton2");
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Click += new System.EventHandler(this.MoveItems);
            // 
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.MoveItem);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // parameterCondition
            // 
            resources.ApplyResources(this.parameterCondition, "parameterCondition");
            this.parameterCondition.Name = "parameterCondition";
            this.parameterCondition.SelectedIndexChanged += new System.EventHandler(this.parameterCondition_SelectedIndexChanged);
            // 
            // numberOfForests
            // 
            resources.ApplyResources(this.numberOfForests, "numberOfForests");
            this.numberOfForests.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numberOfForests.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numberOfForests.Name = "numberOfForests";
            this.numberOfForests.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // simpleButton8
            // 
            resources.ApplyResources(this.simpleButton8, "simpleButton8");
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Click += new System.EventHandler(this.simpleButton8_Click);
            // 
            // simpleButton5
            // 
            resources.ApplyResources(this.simpleButton5, "simpleButton5");
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // histogramm
            // 
            resources.ApplyResources(this.histogramm, "histogramm");
            this.histogramm.Name = "histogramm";
            this.histogramm.Click += new System.EventHandler(this.histogramm_Click);
            // 
            // trainButton
            // 
            resources.ApplyResources(this.trainButton, "trainButton");
            this.trainButton.Name = "trainButton";
            this.trainButton.Click += new System.EventHandler(this.trainButton_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // DesitionTree
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "DesitionTree";
            this.Shown += new System.EventHandler(this.DesitionTree_Shown);
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.criterion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfForests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox ListDelete;
        private System.Windows.Forms.ListBox ListAdd;
        private System.Windows.Forms.Button simpleButton4;
        private System.Windows.Forms.Button simpleButton3;
        private System.Windows.Forms.Button simpleButton2;
        private System.Windows.Forms.Button simpleButton1;
        private System.Windows.Forms.Button simpleButton8;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox parameterCondition;
        private System.Windows.Forms.SplitContainer splitContainerControl1;
        private System.Windows.Forms.Button simpleButton5;
        private System.Windows.Forms.NumericUpDown numberOfForests;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button histogramm;
        private System.Windows.Forms.NumericUpDown criterion;
        private System.Windows.Forms.NumericUpDown textEdit1;
        private System.Windows.Forms.Button trainButton;
    }
}