using System;

namespace DataMiningOffLine.Forms
{
    partial class QualityFind
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
            this.splitContainerControl4 = new System.Windows.Forms.SplitContainer();
            this.xtraTabPage3 = new System.Windows.Forms.TabPage();
            this.xtraTabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainerControl5 = new System.Windows.Forms.SplitContainer();
            this.splitContainerControl11 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lowPercentBlackPoint = new System.Windows.Forms.NumericUpDown();
            this.upPercentBlackPoint = new System.Windows.Forms.NumericUpDown();
            this.nominalValueBlackPoint = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.calculateBlackPoint = new System.Windows.Forms.Button();
            this.splitContainerControl6 = new System.Windows.Forms.SplitContainer();
            this.splitContainerControl3 = new System.Windows.Forms.SplitContainer();
            this.splitContainerControl9 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.upPercentShrinkage = new System.Windows.Forms.NumericUpDown();
            this.nominalValueShrinkage = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.calculateShrinkage = new System.Windows.Forms.Button();
            this.lowPercentShrinkage = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.shrinkageControl1 = new ScottPlot.FormsPlot();
            this.splitContainerControl10 = new System.Windows.Forms.SplitContainer();
            this.xtraTabPage2 = new System.Windows.Forms.TabPage();
            this.xtraTabControl1 = new System.Windows.Forms.TabControl();
            this.blackPointControl1 = new ScottPlot.FormsPlot();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).BeginInit();
            this.splitContainerControl4.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).BeginInit();
            this.splitContainerControl5.Panel1.SuspendLayout();
            this.splitContainerControl5.Panel2.SuspendLayout();
            this.splitContainerControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl11)).BeginInit();
            this.splitContainerControl11.Panel1.SuspendLayout();
            this.splitContainerControl11.Panel2.SuspendLayout();
            this.splitContainerControl11.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lowPercentBlackPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upPercentBlackPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nominalValueBlackPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl6)).BeginInit();
            this.splitContainerControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.Panel1.SuspendLayout();
            this.splitContainerControl3.Panel2.SuspendLayout();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl9)).BeginInit();
            this.splitContainerControl9.Panel1.SuspendLayout();
            this.splitContainerControl9.Panel2.SuspendLayout();
            this.splitContainerControl9.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upPercentShrinkage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nominalValueShrinkage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowPercentShrinkage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl10)).BeginInit();
            this.splitContainerControl10.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl4
            // 
            this.splitContainerControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl4.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl4.Name = "splitContainerControl4";
            this.splitContainerControl4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerControl4.Size = new System.Drawing.Size(1280, 366);
            this.splitContainerControl4.SplitterDistance = 160;
            this.splitContainerControl4.TabIndex = 1;
            this.splitContainerControl4.Text = "splitContainerControl4";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Location = new System.Drawing.Point(0, 0);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(0, 0);
            this.xtraTabPage3.TabIndex = 0;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.splitContainerControl5);
            this.xtraTabPage4.Location = new System.Drawing.Point(4, 22);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1278, 675);
            this.xtraTabPage4.TabIndex = 1;
            this.xtraTabPage4.Text = "Черные точки";
            // 
            // splitContainerControl5
            // 
            this.splitContainerControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl5.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl5.Name = "splitContainerControl5";
            this.splitContainerControl5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerControl5.Panel1
            // 
            this.splitContainerControl5.Panel1.Controls.Add(this.splitContainerControl11);
            this.splitContainerControl5.Panel1.Text = "Panel1";
            // 
            // splitContainerControl5.Panel2
            // 
            this.splitContainerControl5.Panel2.Controls.Add(this.splitContainerControl6);
            this.splitContainerControl5.Panel2.Text = "Panel2";
            this.splitContainerControl5.Size = new System.Drawing.Size(1278, 675);
            this.splitContainerControl5.SplitterDistance = 645;
            this.splitContainerControl5.TabIndex = 0;
            this.splitContainerControl5.Text = "splitContainerControl1";
            // 
            // splitContainerControl11
            // 
            this.splitContainerControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl11.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl11.Name = "splitContainerControl11";
            this.splitContainerControl11.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerControl11.Panel1
            // 
            this.splitContainerControl11.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainerControl11.Panel1.Text = "Panel1";
            // 
            // splitContainerControl11.Panel2
            // 
            this.splitContainerControl11.Panel2.Controls.Add(this.blackPointControl1);
            this.splitContainerControl11.Panel2.Text = "Panel2";
            this.splitContainerControl11.Size = new System.Drawing.Size(1278, 645);
            this.splitContainerControl11.SplitterDistance = 83;
            this.splitContainerControl11.TabIndex = 0;
            this.splitContainerControl11.Text = "splitContainerControl11";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.39063F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.60938F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 227F));
            this.tableLayoutPanel1.Controls.Add(this.lowPercentBlackPoint, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.upPercentBlackPoint, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nominalValueBlackPoint, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.calculateBlackPoint, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1278, 83);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lowPercentBlackPoint
            // 
            this.lowPercentBlackPoint.DecimalPlaces = 2;
            this.lowPercentBlackPoint.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.lowPercentBlackPoint.Location = new System.Drawing.Point(532, 63);
            this.lowPercentBlackPoint.Name = "lowPercentBlackPoint";
            this.lowPercentBlackPoint.Size = new System.Drawing.Size(120, 20);
            this.lowPercentBlackPoint.TabIndex = 41;
            this.lowPercentBlackPoint.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // upPercentBlackPoint
            // 
            this.upPercentBlackPoint.DecimalPlaces = 2;
            this.upPercentBlackPoint.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.upPercentBlackPoint.Location = new System.Drawing.Point(532, 33);
            this.upPercentBlackPoint.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.upPercentBlackPoint.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.upPercentBlackPoint.Name = "upPercentBlackPoint";
            this.upPercentBlackPoint.Size = new System.Drawing.Size(120, 20);
            this.upPercentBlackPoint.TabIndex = 40;
            this.upPercentBlackPoint.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nominalValueBlackPoint
            // 
            this.nominalValueBlackPoint.DecimalPlaces = 2;
            this.nominalValueBlackPoint.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nominalValueBlackPoint.Location = new System.Drawing.Point(532, 3);
            this.nominalValueBlackPoint.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nominalValueBlackPoint.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nominalValueBlackPoint.Name = "nominalValueBlackPoint";
            this.nominalValueBlackPoint.Size = new System.Drawing.Size(120, 20);
            this.nominalValueBlackPoint.TabIndex = 33;
            this.nominalValueBlackPoint.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(523, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Номинальное значение";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(523, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Верхнее отклонение (%)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(523, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Нижнее отклонение (%)";
            // 
            // calculateBlackPoint
            // 
            this.calculateBlackPoint.Location = new System.Drawing.Point(1053, 33);
            this.calculateBlackPoint.Name = "calculateBlackPoint";
            this.calculateBlackPoint.Size = new System.Drawing.Size(95, 23);
            this.calculateBlackPoint.TabIndex = 42;
            this.calculateBlackPoint.Text = "calculate";
            this.calculateBlackPoint.UseVisualStyleBackColor = true;
            this.calculateBlackPoint.Click += new System.EventHandler(this.CalculateBlackPoint_Click);
            // 
            // splitContainerControl6
            // 
            this.splitContainerControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl6.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl6.Name = "splitContainerControl6";
            this.splitContainerControl6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerControl6.Panel1
            // 
            this.splitContainerControl6.Panel1.Text = "Panel1";
            // 
            // splitContainerControl6.Panel2
            // 
            this.splitContainerControl6.Panel2.Text = "Panel2";
            this.splitContainerControl6.Size = new System.Drawing.Size(1278, 26);
            this.splitContainerControl6.SplitterDistance = 25;
            this.splitContainerControl6.TabIndex = 0;
            this.splitContainerControl6.Text = "splitContainerControl2";
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerControl3.Panel1
            // 
            this.splitContainerControl3.Panel1.Controls.Add(this.splitContainerControl9);
            // 
            // splitContainerControl3.Panel2
            // 
            this.splitContainerControl3.Panel2.Controls.Add(this.splitContainerControl10);
            this.splitContainerControl3.Size = new System.Drawing.Size(1278, 675);
            this.splitContainerControl3.SplitterDistance = 645;
            this.splitContainerControl3.TabIndex = 1;
            this.splitContainerControl3.Text = "splitContainerControl3";
            // 
            // splitContainerControl9
            // 
            this.splitContainerControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl9.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl9.Name = "splitContainerControl9";
            this.splitContainerControl9.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerControl9.Panel1
            // 
            this.splitContainerControl9.Panel1.Controls.Add(this.tableLayoutPanel6);
            this.splitContainerControl9.Panel1.Text = "Panel1";
            // 
            // splitContainerControl9.Panel2
            // 
            this.splitContainerControl9.Panel2.Controls.Add(this.shrinkageControl1);
            this.splitContainerControl9.Panel2.Text = "Panel2";
            this.splitContainerControl9.Size = new System.Drawing.Size(1278, 645);
            this.splitContainerControl9.SplitterDistance = 87;
            this.splitContainerControl9.TabIndex = 0;
            this.splitContainerControl9.Text = "splitContainerControl9";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.39063F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.60938F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 261F));
            this.tableLayoutPanel6.Controls.Add(this.upPercentShrinkage, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.nominalValueShrinkage, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label17, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.calculateShrinkage, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.lowPercentShrinkage, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1278, 87);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // upPercentShrinkage
            // 
            this.upPercentShrinkage.DecimalPlaces = 2;
            this.upPercentShrinkage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.upPercentShrinkage.Location = new System.Drawing.Point(515, 33);
            this.upPercentShrinkage.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.upPercentShrinkage.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.upPercentShrinkage.Name = "upPercentShrinkage";
            this.upPercentShrinkage.Size = new System.Drawing.Size(120, 20);
            this.upPercentShrinkage.TabIndex = 38;
            this.upPercentShrinkage.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nominalValueShrinkage
            // 
            this.nominalValueShrinkage.DecimalPlaces = 2;
            this.nominalValueShrinkage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nominalValueShrinkage.Location = new System.Drawing.Point(515, 3);
            this.nominalValueShrinkage.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nominalValueShrinkage.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nominalValueShrinkage.Name = "nominalValueShrinkage";
            this.nominalValueShrinkage.Size = new System.Drawing.Size(120, 20);
            this.nominalValueShrinkage.TabIndex = 33;
            this.nominalValueShrinkage.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(3, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(506, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Номинальное значение";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(3, 38);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(506, 13);
            this.label17.TabIndex = 34;
            this.label17.Text = "Верхнее отклонение (%)";
            // 
            // calculateShrinkage
            // 
            this.calculateShrinkage.Location = new System.Drawing.Point(1019, 33);
            this.calculateShrinkage.Name = "calculateShrinkage";
            this.calculateShrinkage.Size = new System.Drawing.Size(95, 23);
            this.calculateShrinkage.TabIndex = 40;
            this.calculateShrinkage.Text = "calculate";
            this.calculateShrinkage.UseVisualStyleBackColor = true;
            this.calculateShrinkage.Click += new System.EventHandler(this.CalculateShrinkage_Click);
            // 
            // lowPercentShrinkage
            // 
            this.lowPercentShrinkage.DecimalPlaces = 2;
            this.lowPercentShrinkage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.lowPercentShrinkage.Location = new System.Drawing.Point(515, 63);
            this.lowPercentShrinkage.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.lowPercentShrinkage.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.lowPercentShrinkage.Name = "lowPercentShrinkage";
            this.lowPercentShrinkage.Size = new System.Drawing.Size(120, 20);
            this.lowPercentShrinkage.TabIndex = 39;
            this.lowPercentShrinkage.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(3, 68);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(506, 13);
            this.label18.TabIndex = 41;
            this.label18.Text = "Нижнее отклонение (%)";
            // 
            // shrinkageControl1
            // 
            this.shrinkageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shrinkageControl1.LegendFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shrinkageControl1.Location = new System.Drawing.Point(0, 0);
            this.shrinkageControl1.Name = "shrinkageControl1";
            this.shrinkageControl1.ShowTooltipOnMouseMoved = true;
            this.shrinkageControl1.ShowVAxisLineOnMouseMoved = true;
            this.shrinkageControl1.Size = new System.Drawing.Size(1278, 554);
            this.shrinkageControl1.TabIndex = 1;
            // 
            // splitContainerControl10
            // 
            this.splitContainerControl10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl10.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl10.Name = "splitContainerControl10";
            this.splitContainerControl10.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerControl10.Panel1
            // 
            this.splitContainerControl10.Panel1.Text = "Panel1";
            // 
            // splitContainerControl10.Panel2
            // 
            this.splitContainerControl10.Panel2.Text = "Panel2";
            this.splitContainerControl10.Size = new System.Drawing.Size(1278, 26);
            this.splitContainerControl10.SplitterDistance = 25;
            this.splitContainerControl10.TabIndex = 0;
            this.splitContainerControl10.Text = "splitContainerControl10";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.splitContainerControl3);
            this.xtraTabPage2.Location = new System.Drawing.Point(4, 22);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1278, 675);
            this.xtraTabPage2.TabIndex = 0;
            this.xtraTabPage2.Text = "Усадка";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Controls.Add(this.xtraTabPage2);
            this.xtraTabControl1.Controls.Add(this.xtraTabPage4);
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedIndex = 0;
            this.xtraTabControl1.Size = new System.Drawing.Size(1286, 701);
            this.xtraTabControl1.TabIndex = 0;
            // 
            // blackPointControl1
            // 
            this.blackPointControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blackPointControl1.LegendFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.blackPointControl1.Location = new System.Drawing.Point(0, 0);
            this.blackPointControl1.Name = "blackPointControl1";
            this.blackPointControl1.ShowTooltipOnMouseMoved = true;
            this.blackPointControl1.ShowVAxisLineOnMouseMoved = true;
            this.blackPointControl1.Size = new System.Drawing.Size(1278, 558);
            this.blackPointControl1.TabIndex = 2;
            // 
            // QualityFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 701);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "QualityFind";
            this.Text = "QualityFind";
            this.Load += new System.EventHandler(this.QualityFind_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).EndInit();
            this.splitContainerControl4.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.splitContainerControl5.Panel1.ResumeLayout(false);
            this.splitContainerControl5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).EndInit();
            this.splitContainerControl5.ResumeLayout(false);
            this.splitContainerControl11.Panel1.ResumeLayout(false);
            this.splitContainerControl11.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl11)).EndInit();
            this.splitContainerControl11.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lowPercentBlackPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upPercentBlackPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nominalValueBlackPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl6)).EndInit();
            this.splitContainerControl6.ResumeLayout(false);
            this.splitContainerControl3.Panel1.ResumeLayout(false);
            this.splitContainerControl3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            this.splitContainerControl9.Panel1.ResumeLayout(false);
            this.splitContainerControl9.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl9)).EndInit();
            this.splitContainerControl9.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upPercentShrinkage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nominalValueShrinkage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowPercentShrinkage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl10)).EndInit();
            this.splitContainerControl10.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerControl4;

        private System.Windows.Forms.TabPage xtraTabPage3;
        private System.Windows.Forms.TabPage xtraTabPage4;
        private System.Windows.Forms.TabPage xtraTabPage2;
        private System.Windows.Forms.SplitContainer splitContainerControl3;
        private System.Windows.Forms.SplitContainer splitContainerControl9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.NumericUpDown nominalValueShrinkage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.SplitContainer splitContainerControl10;
        private System.Windows.Forms.TabControl xtraTabControl1;
        private System.Windows.Forms.SplitContainer splitContainerControl5;
        private System.Windows.Forms.SplitContainer splitContainerControl11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown lowPercentBlackPoint;
        private System.Windows.Forms.NumericUpDown upPercentBlackPoint;
        private System.Windows.Forms.NumericUpDown nominalValueBlackPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainerControl6;
        private System.Windows.Forms.NumericUpDown lowPercentShrinkage;
        private System.Windows.Forms.NumericUpDown upPercentShrinkage;
        private System.Windows.Forms.Button calculateShrinkage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button calculateBlackPoint;
        private ScottPlot.FormsPlot shrinkageControl1;
        private ScottPlot.FormsPlot blackPointControl1;
    }
}