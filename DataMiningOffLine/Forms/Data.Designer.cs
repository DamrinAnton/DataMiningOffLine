namespace DataMiningOffLine
{
    partial class Data
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Data));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.path = new System.Windows.Forms.TextBox();
            this.OpenExcel = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.DeleteAll = new System.Windows.Forms.Button();
            this.AddAll = new System.Windows.Forms.Button();
            this.ListAdd = new System.Windows.Forms.ListBox();
            this.ListDelete = new System.Windows.Forms.ListBox();
            this.dataAcq = new System.Windows.Forms.Button();
            this.simpleButton1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measurementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.схемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.трендыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.трендовыйАнализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.светофорToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анализКачестваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.математическиеМоделиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистическийАнализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.регрессионныйАнализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.регрессионныйАнализ2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.деревьяПринятияРешенийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сетевыеКартыКохоненаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мнемосхемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.языкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.русскийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.английскийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.path, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.OpenExcel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Add, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Delete, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.DeleteAll, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.AddAll, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ListAdd, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ListDelete, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataAcq, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.simpleButton1, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // path
            // 
            resources.ApplyResources(this.path, "path");
            this.path.Name = "path";
            // 
            // OpenExcel
            // 
            resources.ApplyResources(this.OpenExcel, "OpenExcel");
            this.OpenExcel.Name = "OpenExcel";
            this.OpenExcel.UseVisualStyleBackColor = true;
            this.OpenExcel.Click += new System.EventHandler(this.OpenExcel_Click);
            // 
            // Add
            // 
            resources.ApplyResources(this.Add, "Add");
            this.Add.Name = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.AddElement);
            // 
            // Delete
            // 
            resources.ApplyResources(this.Delete, "Delete");
            this.Delete.Name = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // DeleteAll
            // 
            resources.ApplyResources(this.DeleteAll, "DeleteAll");
            this.DeleteAll.Name = "DeleteAll";
            this.DeleteAll.UseVisualStyleBackColor = true;
            this.DeleteAll.Click += new System.EventHandler(this.DeleteAll_Click);
            // 
            // AddAll
            // 
            resources.ApplyResources(this.AddAll, "AddAll");
            this.AddAll.Name = "AddAll";
            this.AddAll.UseVisualStyleBackColor = true;
            this.AddAll.Click += new System.EventHandler(this.AddAll_Click);
            // 
            // ListAdd
            // 
            resources.ApplyResources(this.ListAdd, "ListAdd");
            this.ListAdd.Name = "ListAdd";
            this.tableLayoutPanel1.SetRowSpan(this.ListAdd, 5);
            // 
            // ListDelete
            // 
            resources.ApplyResources(this.ListDelete, "ListDelete");
            this.ListDelete.Name = "ListDelete";
            this.tableLayoutPanel1.SetRowSpan(this.ListDelete, 5);
            // 
            // dataAcq
            // 
            resources.ApplyResources(this.dataAcq, "dataAcq");
            this.dataAcq.Name = "dataAcq";
            this.dataAcq.UseVisualStyleBackColor = true;
            this.dataAcq.Click += new System.EventHandler(this.dataAcq_Click);
            // 
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.трендыToolStripMenuItem,
            this.математическиеМоделиToolStripMenuItem,
            this.мнемосхемаToolStripMenuItem,
            this.языкToolStripMenuItem,
            this.helperToolStripMenuItem,
            this.kPIToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.measurementsToolStripMenuItem,
            this.parametersToolStripMenuItem,
            this.схемаToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            resources.ApplyResources(this.справочникToolStripMenuItem, "справочникToolStripMenuItem");
            // 
            // measurementsToolStripMenuItem
            // 
            this.measurementsToolStripMenuItem.Name = "measurementsToolStripMenuItem";
            resources.ApplyResources(this.measurementsToolStripMenuItem, "measurementsToolStripMenuItem");
            this.measurementsToolStripMenuItem.Click += new System.EventHandler(this.справочникToolStripMenuItem_Click);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            resources.ApplyResources(this.parametersToolStripMenuItem, "parametersToolStripMenuItem");
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
            // 
            // схемаToolStripMenuItem
            // 
            this.схемаToolStripMenuItem.Name = "схемаToolStripMenuItem";
            resources.ApplyResources(this.схемаToolStripMenuItem, "схемаToolStripMenuItem");
            this.схемаToolStripMenuItem.Click += new System.EventHandler(this.схемаToolStripMenuItem_Click);
            // 
            // трендыToolStripMenuItem
            // 
            this.трендыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.трендовыйАнализToolStripMenuItem,
            this.светофорToolStripMenuItem,
            this.анализКачестваToolStripMenuItem});
            this.трендыToolStripMenuItem.Name = "трендыToolStripMenuItem";
            resources.ApplyResources(this.трендыToolStripMenuItem, "трендыToolStripMenuItem");
            // 
            // трендовыйАнализToolStripMenuItem
            // 
            this.трендовыйАнализToolStripMenuItem.Name = "трендовыйАнализToolStripMenuItem";
            resources.ApplyResources(this.трендовыйАнализToolStripMenuItem, "трендовыйАнализToolStripMenuItem");
            this.трендовыйАнализToolStripMenuItem.Click += new System.EventHandler(this.трендовыйАнализToolStripMenuItem_Click);
            // 
            // светофорToolStripMenuItem
            // 
            this.светофорToolStripMenuItem.Name = "светофорToolStripMenuItem";
            resources.ApplyResources(this.светофорToolStripMenuItem, "светофорToolStripMenuItem");
            this.светофорToolStripMenuItem.Click += new System.EventHandler(this.светофорToolStripMenuItem_Click);
            // 
            // анализКачестваToolStripMenuItem
            // 
            this.анализКачестваToolStripMenuItem.Name = "анализКачестваToolStripMenuItem";
            resources.ApplyResources(this.анализКачестваToolStripMenuItem, "анализКачестваToolStripMenuItem");
            this.анализКачестваToolStripMenuItem.Click += new System.EventHandler(this.анализКачестваToolStripMenuItem_Click);
            // 
            // математическиеМоделиToolStripMenuItem
            // 
            this.математическиеМоделиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.статистическийАнализToolStripMenuItem,
            this.регрессионныйАнализToolStripMenuItem,
            this.регрессионныйАнализ2ToolStripMenuItem,
            this.деревьяПринятияРешенийToolStripMenuItem,
            this.сетевыеКартыКохоненаToolStripMenuItem});
            this.математическиеМоделиToolStripMenuItem.Name = "математическиеМоделиToolStripMenuItem";
            resources.ApplyResources(this.математическиеМоделиToolStripMenuItem, "математическиеМоделиToolStripMenuItem");
            // 
            // статистическийАнализToolStripMenuItem
            // 
            this.статистическийАнализToolStripMenuItem.Name = "статистическийАнализToolStripMenuItem";
            resources.ApplyResources(this.статистическийАнализToolStripMenuItem, "статистическийАнализToolStripMenuItem");
            this.статистическийАнализToolStripMenuItem.Click += new System.EventHandler(this.статистическийАнализToolStripMenuItem_Click);
            // 
            // регрессионныйАнализToolStripMenuItem
            // 
            this.регрессионныйАнализToolStripMenuItem.Name = "регрессионныйАнализToolStripMenuItem";
            resources.ApplyResources(this.регрессионныйАнализToolStripMenuItem, "регрессионныйАнализToolStripMenuItem");
            this.регрессионныйАнализToolStripMenuItem.Click += new System.EventHandler(this.регрессионныйАнализToolStripMenuItem_Click);
            // 
            // регрессионныйАнализ2ToolStripMenuItem
            // 
            this.регрессионныйАнализ2ToolStripMenuItem.Name = "регрессионныйАнализ2ToolStripMenuItem";
            resources.ApplyResources(this.регрессионныйАнализ2ToolStripMenuItem, "регрессионныйАнализ2ToolStripMenuItem");
            this.регрессионныйАнализ2ToolStripMenuItem.Click += new System.EventHandler(this.регрессионныйАнализ2ToolStripMenuItem_Click);
            // 
            // деревьяПринятияРешенийToolStripMenuItem
            // 
            this.деревьяПринятияРешенийToolStripMenuItem.Name = "деревьяПринятияРешенийToolStripMenuItem";
            resources.ApplyResources(this.деревьяПринятияРешенийToolStripMenuItem, "деревьяПринятияРешенийToolStripMenuItem");
            this.деревьяПринятияРешенийToolStripMenuItem.Click += new System.EventHandler(this.деревьяПринятияРешенийToolStripMenuItem_Click);
            // 
            // сетевыеКартыКохоненаToolStripMenuItem
            // 
            this.сетевыеКартыКохоненаToolStripMenuItem.Name = "сетевыеКартыКохоненаToolStripMenuItem";
            resources.ApplyResources(this.сетевыеКартыКохоненаToolStripMenuItem, "сетевыеКартыКохоненаToolStripMenuItem");
            this.сетевыеКартыКохоненаToolStripMenuItem.Click += new System.EventHandler(this.сетевыеКартыКохоненаToolStripMenuItem_Click);
            // 
            // мнемосхемаToolStripMenuItem
            // 
            this.мнемосхемаToolStripMenuItem.Name = "мнемосхемаToolStripMenuItem";
            resources.ApplyResources(this.мнемосхемаToolStripMenuItem, "мнемосхемаToolStripMenuItem");
            this.мнемосхемаToolStripMenuItem.Click += new System.EventHandler(this.мнемосхемаToolStripMenuItem_Click);
            // 
            // языкToolStripMenuItem
            // 
            this.языкToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.русскийToolStripMenuItem,
            this.английскийToolStripMenuItem});
            this.языкToolStripMenuItem.Name = "языкToolStripMenuItem";
            resources.ApplyResources(this.языкToolStripMenuItem, "языкToolStripMenuItem");
            // 
            // русскийToolStripMenuItem
            // 
            this.русскийToolStripMenuItem.Name = "русскийToolStripMenuItem";
            resources.ApplyResources(this.русскийToolStripMenuItem, "русскийToolStripMenuItem");
            this.русскийToolStripMenuItem.Click += new System.EventHandler(this.русскийToolStripMenuItem_Click);
            // 
            // английскийToolStripMenuItem
            // 
            this.английскийToolStripMenuItem.Name = "английскийToolStripMenuItem";
            resources.ApplyResources(this.английскийToolStripMenuItem, "английскийToolStripMenuItem");
            this.английскийToolStripMenuItem.Click += new System.EventHandler(this.английскийToolStripMenuItem_Click);
            // 
            // helperToolStripMenuItem
            // 
            this.helperToolStripMenuItem.Name = "helperToolStripMenuItem";
            resources.ApplyResources(this.helperToolStripMenuItem, "helperToolStripMenuItem");
            this.helperToolStripMenuItem.Click += new System.EventHandler(this.helperToolStripMenuItem_Click);
            // 
            // kPIToolStripMenuItem
            // 
            this.kPIToolStripMenuItem.Name = "kPIToolStripMenuItem";
            resources.ApplyResources(this.kPIToolStripMenuItem, "kPIToolStripMenuItem");
            this.kPIToolStripMenuItem.Click += new System.EventHandler(this.kPIToolStripMenuItem_Click);
            // 
            // Data
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Data";
            this.Load += new System.EventHandler(this.Data_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button OpenExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox path;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button DeleteAll;
        private System.Windows.Forms.Button AddAll;
        private System.Windows.Forms.Button dataAcq;
        private System.Windows.Forms.ListBox ListAdd;
        private System.Windows.Forms.ListBox ListDelete;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem трендыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem трендовыйАнализToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem светофорToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem математическиеМоделиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem регрессионныйАнализToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem деревьяПринятияРешенийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сетевыеКартыКохоненаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem языкToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem русскийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem английскийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem мнемосхемаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measurementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem схемаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem анализКачестваToolStripMenuItem;
        private System.Windows.Forms.Button simpleButton1;
        private System.Windows.Forms.ToolStripMenuItem helperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem регрессионныйАнализ2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистическийАнализToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kPIToolStripMenuItem;
    }
}