using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataMiningOffLine
{
    public partial class ParentForMaps : Form
    {
        public static List<TestForm> a = new List<TestForm>();
        decimal[,] neuronsValue;
        List<Stat> inputStatistics;
        NetworkSettings inputSettings;
        List<string> nameParam;

        public int childX, childY;
        //public event changeXYHendler changeXY;

        public ParentForMaps(decimal[,] neuronsValue, List<Stat> inputStatistics, NetworkSettings inputSettings, List<string> nameParam)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Languages))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
            }
            InitializeComponent();
            this.neuronsValue = neuronsValue;
            this.inputStatistics = inputStatistics;
            this.inputSettings = inputSettings;
            this.nameParam = nameParam;
            textBoxHexSide.Text = "15";
            textBoxXOffset.Text = "5";
            textBoxtYOffset.Text = "5";
            textBoxPenWidth.Text = "2";
        }

        private void runMap_Click(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren)
            {
                child.Close();
            }

            MapSettings currentMapSettings = new MapSettings(Convert.ToInt32(textBoxHexSide.Text), Convert.ToInt32(textBoxXOffset.Text), Convert.ToInt32(textBoxtYOffset.Text), Convert.ToInt32(textBoxPenWidth.Text));
            double widht = (Math.Sqrt(3) / 2 * Convert.ToDouble(textBoxHexSide.Text)) + (Math.Sqrt(3) / 2 * Convert.ToDouble(textBoxHexSide.Text)) * 2 * inputSettings.XMap + Convert.ToDouble(textBoxXOffset.Text) * 4;
            double height = Convert.ToDouble(textBoxHexSide.Text) * inputSettings.YMap * 2 + Convert.ToDouble(textBoxtYOffset.Text) * 2 + 25;

            for (int i = 0; i < inputSettings.DimentionOfVector; i++)
            {

            }

            for (int i = 0, x = 0, y = 0; i < inputSettings.DimentionOfVector; i++)
            {
                if (i != 0) x += Convert.ToInt32(widht) + Convert.ToInt32(textBoxXOffset.Text);
                if (x + widht > this.Width)
                {
                    y += Convert.ToInt32(height);
                    x = 0;
                }

                TestForm testMDIChild = new TestForm(neuronsValue, inputStatistics, inputSettings, currentMapSettings, i, a);
                testMDIChild.MdiParent = this;
                testMDIChild.Size = new System.Drawing.Size(Convert.ToInt32(widht) + Convert.ToInt32(textBoxXOffset.Text) * 2, Convert.ToInt32(height) + Convert.ToInt32(textBoxXOffset.Text));
                Point location = new Point(x, y);
                testMDIChild.Text = nameParam[i];
                testMDIChild.Name = Localization.MyStrings.Parameter + Convert.ToString(i + 1);
                testMDIChild.Location = location;
                testMDIChild.Show();
                testMDIChild.Refresh();
                a.Add(testMDIChild);
            }


        }

        private void labelXP_TextChanged(object sender, EventArgs e)
        {
            foreach (TestForm testMDIChild in MdiChildren)
            {
                testMDIChild.ParentForm_ChangeXY(childX, childY);
                testMDIChild.Refresh();
            }
        }
    }
}
