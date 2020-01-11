using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DM.Data;
using DM.DecisionTree;

namespace DataMiningOffLine
{
    public partial class Diagramm : Form
    {
        public Diagramm(List<DM.DecisionTree.TreeNode> rndForests)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Languages))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
            }
            InitializeComponent();
            TrainData.GetData();
            List<string> dictForestsSort = new List<string>();
            List<string> secForestsSort = new List<string>();
            Dictionary<string, int> dictForests = new Dictionary<string, int>();
            Dictionary<string, int> secondForests = new Dictionary<string, int>();
            foreach (DM.DecisionTree.TreeNode rndForest in rndForests)
            {
                if (!dictForests.ContainsKey(rndForest.attributeName))
                {
                    dictForests.Add(rndForest.attributeName, 1);
                }
                else dictForests[rndForest.attributeName]++;
            }
            dictForestsSort = dictForests.Keys.ToList();

            InsertionSort(dictForestsSort, dictForests);


            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();


            // Set palette.
            this.chart1.Palette = ChartColorPalette.SeaGreen;

            // Set title.
            this.chart1.Titles.Add("1st Node");
            double numberOfParameters = 1.0;

            foreach (var item in dictForestsSort)
            {
                //TODO Russian name 
                if ((item != "False") && (item != "True"))
                {
                    var name = TrainData.nameParameter.SingleOrDefault(parameter => parameter.Value == item).Value;
                    // Add series.

                    Series series = this.chart1.Series.Add(XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages));
                    // Add point.
                    //series.Points.Add(Convert.ToDouble(item.Value));
                    series.Points.Add(dictForests[item]);
                }
            }

            foreach (var rndForest in rndForests)
            {

                foreach (var treeNode in rndForest.MChilds)
                {
                    if ((treeNode != null) && (treeNode.attributeName != "False") && ((treeNode.attributeName != "True")))
                    {
                        if (!secondForests.ContainsKey(treeNode.attributeName))
                        {
                            secondForests.Add(treeNode.attributeName, 1);
                        }
                        else secondForests[treeNode.attributeName]++;
                    }

                }
            }

            secForestsSort = secondForests.Keys.ToList();
            InsertionSort(secForestsSort, secondForests);
            this.chart2.Series.Clear();
            this.chart2.Titles.Clear();


            // Set palette.
            this.chart2.Palette = ChartColorPalette.SeaGreen;

            // Set title.
            this.chart2.Titles.Add("2nd Node");

            foreach (var item in secForestsSort)
            {
                //TODO Russian name 
                var name = TrainData.nameParameter.Single(parameter => parameter.Value == item).Value;
                // Add series.
                Series series = this.chart2.Series.Add(XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages));
                // Add point.
                series.Points.Add(secondForests[item]);
            }

        }

        private static void InsertionSort(List<string> dictForestsSort, Dictionary<string, int> dictForests)
        {
            for (int i = 1; i < dictForestsSort.Count; i++)
            {
                string key = dictForestsSort[i];
                int j = i - 1;
                while (j >= 0 && dictForests[dictForestsSort[j]] < dictForests[key])
                {
                    dictForestsSort[j + 1] = dictForestsSort[j];
                    j--;
                }
                dictForestsSort[j + 1] = key;
            }
        }
    }
}
