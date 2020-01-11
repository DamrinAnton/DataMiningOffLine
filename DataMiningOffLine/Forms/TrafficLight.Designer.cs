namespace DataMiningOffLine
{
    partial class TrafficLight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrafficLight));
            this.splitContainerControl1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteAllToSecondCoordinate = new System.Windows.Forms.Button();
            this.deleteOneToSecondCoordinate = new System.Windows.Forms.Button();
            this.addAllToSecondCoordinate = new System.Windows.Forms.Button();
            this.addOneToSecondCoordinate = new System.Windows.Forms.Button();
            this.ListAddError = new System.Windows.Forms.ListBox();
            this.ListAdd = new System.Windows.Forms.ListBox();
            this.ListDelete = new System.Windows.Forms.ListBox();
            this.ListDeleteError = new System.Windows.Forms.ListBox();
            this.addOneCoordinate = new System.Windows.Forms.Button();
            this.addAllToFirstCoordinate = new System.Windows.Forms.Button();
            this.deleteToFirstCoordinate = new System.Windows.Forms.Button();
            this.deleteAllToFirstCoordinate = new System.Windows.Forms.Button();
            this.ShowThrends = new System.Windows.Forms.Button();
            this.parameterCondition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.uppLimit = new System.Windows.Forms.NumericUpDown();
            this.Chart = new ScottPlot.FormsPlot();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uppLimit)).BeginInit();
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
            this.splitContainerControl1.Panel2.Controls.Add(this.Chart);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.deleteAllToSecondCoordinate, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.deleteOneToSecondCoordinate, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.addAllToSecondCoordinate, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.addOneToSecondCoordinate, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.ListAddError, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ListAdd, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ListDelete, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.ListDeleteError, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.addOneCoordinate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.addAllToFirstCoordinate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.deleteToFirstCoordinate, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.deleteAllToFirstCoordinate, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.ShowThrends, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.parameterCondition, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.uppLimit, 3, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // deleteAllToSecondCoordinate
            // 
            resources.ApplyResources(this.deleteAllToSecondCoordinate, "deleteAllToSecondCoordinate");
            this.deleteAllToSecondCoordinate.Name = "deleteAllToSecondCoordinate";
            this.deleteAllToSecondCoordinate.Click += new System.EventHandler(this.DeleteErrorAllItems);
            // 
            // deleteOneToSecondCoordinate
            // 
            resources.ApplyResources(this.deleteOneToSecondCoordinate, "deleteOneToSecondCoordinate");
            this.deleteOneToSecondCoordinate.Name = "deleteOneToSecondCoordinate";
            this.deleteOneToSecondCoordinate.Click += new System.EventHandler(this.DeleteErrorOneItem);
            // 
            // addAllToSecondCoordinate
            // 
            resources.ApplyResources(this.addAllToSecondCoordinate, "addAllToSecondCoordinate");
            this.addAllToSecondCoordinate.Name = "addAllToSecondCoordinate";
            this.addAllToSecondCoordinate.Click += new System.EventHandler(this.MoveErrorItems);
            // 
            // addOneToSecondCoordinate
            // 
            resources.ApplyResources(this.addOneToSecondCoordinate, "addOneToSecondCoordinate");
            this.addOneToSecondCoordinate.Name = "addOneToSecondCoordinate";
            this.addOneToSecondCoordinate.Click += new System.EventHandler(this.MoveErrorItem);
            // 
            // ListAddError
            // 
            resources.ApplyResources(this.ListAddError, "ListAddError");
            this.ListAddError.Name = "ListAddError";
            this.tableLayoutPanel1.SetRowSpan(this.ListAddError, 6);
            // 
            // ListAdd
            // 
            resources.ApplyResources(this.ListAdd, "ListAdd");
            this.ListAdd.Name = "ListAdd";
            this.tableLayoutPanel1.SetRowSpan(this.ListAdd, 6);
            // 
            // ListDelete
            // 
            resources.ApplyResources(this.ListDelete, "ListDelete");
            this.ListDelete.Name = "ListDelete";
            this.tableLayoutPanel1.SetRowSpan(this.ListDelete, 6);
            // 
            // ListDeleteError
            // 
            resources.ApplyResources(this.ListDeleteError, "ListDeleteError");
            this.ListDeleteError.Name = "ListDeleteError";
            this.tableLayoutPanel1.SetRowSpan(this.ListDeleteError, 6);
            // 
            // addOneCoordinate
            // 
            resources.ApplyResources(this.addOneCoordinate, "addOneCoordinate");
            this.addOneCoordinate.Name = "addOneCoordinate";
            this.addOneCoordinate.Click += new System.EventHandler(this.MoveItem);
            // 
            // addAllToFirstCoordinate
            // 
            resources.ApplyResources(this.addAllToFirstCoordinate, "addAllToFirstCoordinate");
            this.addAllToFirstCoordinate.Name = "addAllToFirstCoordinate";
            this.addAllToFirstCoordinate.Click += new System.EventHandler(this.MoveItems);
            // 
            // deleteToFirstCoordinate
            // 
            resources.ApplyResources(this.deleteToFirstCoordinate, "deleteToFirstCoordinate");
            this.deleteToFirstCoordinate.Name = "deleteToFirstCoordinate";
            this.deleteToFirstCoordinate.Click += new System.EventHandler(this.DeleteOneItem);
            // 
            // deleteAllToFirstCoordinate
            // 
            resources.ApplyResources(this.deleteAllToFirstCoordinate, "deleteAllToFirstCoordinate");
            this.deleteAllToFirstCoordinate.Name = "deleteAllToFirstCoordinate";
            this.deleteAllToFirstCoordinate.Click += new System.EventHandler(this.DeleteAllItems);
            // 
            // ShowThrends
            // 
            resources.ApplyResources(this.ShowThrends, "ShowThrends");
            this.ShowThrends.Name = "ShowThrends";
            this.ShowThrends.Click += new System.EventHandler(this.ShowThrends_Click);
            // 
            // parameterCondition
            // 
            resources.ApplyResources(this.parameterCondition, "parameterCondition");
            this.parameterCondition.Name = "parameterCondition";
            this.parameterCondition.SelectedIndexChanged += new System.EventHandler(this.parameterCondition_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // uppLimit
            // 
            this.uppLimit.DecimalPlaces = 2;
            this.uppLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.uppLimit, "uppLimit");
            this.uppLimit.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.uppLimit.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.uppLimit.Name = "uppLimit";
            this.uppLimit.Value = new decimal(new int[] {
            40,
            0,
            0,
            65536});
            // 
            // Chart
            // 
            resources.ApplyResources(this.Chart, "Chart");
            this.Chart.LegendFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Chart.Name = "Chart";
            this.Chart.ShowTooltipOnMouseMoved = true;
            this.Chart.ShowVAxisLineOnMouseMoved = true;
            this.Chart.ToolTipShowStyle = ScottPlot.FormsPlot.ToolTipShowStyleEnum.ShowXValueOnInstance;
            // 
            // TrafficLight
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "TrafficLight";
            this.Shown += new System.EventHandler(this.TrafficLight_Shown);
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uppLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteOneToSecondCoordinate;
        private System.Windows.Forms.SplitContainer splitContainerControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button deleteAllToSecondCoordinate;
        private System.Windows.Forms.Button addAllToSecondCoordinate;
        private System.Windows.Forms.Button addOneToSecondCoordinate;
        private System.Windows.Forms.ListBox ListAddError;
        private System.Windows.Forms.ListBox ListAdd;
        private System.Windows.Forms.ListBox ListDelete;
        private System.Windows.Forms.ListBox ListDeleteError;
        private System.Windows.Forms.Button addOneCoordinate;
        private System.Windows.Forms.Button addAllToFirstCoordinate;
        private System.Windows.Forms.Button deleteToFirstCoordinate;
        private System.Windows.Forms.Button deleteAllToFirstCoordinate;
        private System.Windows.Forms.Button ShowThrends;
        private System.Windows.Forms.ComboBox parameterCondition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown uppLimit;
        private ScottPlot.FormsPlot Chart;
    }
}