using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DM.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataMiningOffLine
{
    public partial class RegressionModelcs : Form
    {
        public RegressionModelcs()
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
            
        }
        /// <summary>
        /// Get Parameters Condition (Quality)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegressionModelcs_Shown(object sender, EventArgs e)
        {
            var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
            foreach (var parameter in parameters)
            {
                parameterCondition.Items.Add(XMLWork.FindNameWithScada(parameter, Properties.Settings.Default.Languages));
            }
            if (parameterCondition.Items.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.WithoutDefect);
            }
            if (parameterCondition.Items.Count != 0)
                parameterCondition.SelectedIndex = 0;
        }
        /// <summary>
        /// Button for calculate Fisher Critery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            FisherCritery();
            CalculateDispersion();
        }

        /// <summary>
        /// Calculate Dispersion all parameters
        /// </summary>
        private void CalculateDispersion()
        {
            int countElements = 10;
            Dictionary<int, decimal> mathematicalExpectation = new Dictionary<int, decimal>();
            Dictionary<int, decimal> dispersion = new Dictionary<int, decimal>();
            Dictionary<int, decimal> bestValues = new Dictionary<int, decimal>();
            ProcessingData.MathematicalExpectation(TrainData.Train, mathematicalExpectation);
            ProcessingData.Dispersion(TrainData.Train, mathematicalExpectation, dispersion);
            List<FisherCritery> elements = new List<FisherCritery>();
            foreach (var @decimal in dispersion)
            {
                elements.Add(new FisherCritery(@decimal.Key, @decimal.Value));
            }
            ListOfElements dispoersionElements = new ListOfElements(elements);
            dispoersionElements.InsertionSort();
            for (int i = 0; i < countElements; i++)
            {
                bestValues.Add(dispoersionElements.Fisher[i].Key, dispoersionElements.Fisher[i].Value);
            }

            this.chart2.Series.Clear();
            this.chart2.Titles.Clear();
            this.chart2.ChartAreas[0].AxisY.Maximum = Math.Round(Convert.ToDouble(bestValues.First().Value), 0) + 25;
            this.chart2.ChartAreas[0].AxisY.Minimum = Math.Round(Convert.ToDouble(bestValues.Last().Value), 0) - 25;

            // Set palette.
            this.chart2.Palette = ChartColorPalette.SeaGreen;

            // Set title.
            this.chart2.Titles.Add("Deviation");
            double numberOfParameters = 1.0;

            foreach (var item in bestValues)
            {
                // Add series.
                Series series = this.chart2.Series.Add(XMLWork.FindNameWithID(item.Key, Properties.Settings.Default.Languages));
                // Add point.
                //series.Points.Add(Convert.ToDouble(item.Value));
                series.Points.AddXY(numberOfParameters, item.Value);
                numberOfParameters += 0.1;
            }
            // Add series.
        }
        /// <summary>
        /// Implement FisherCritery
        /// </summary>
        private void FisherCritery()
        {
            if (parameterCondition.Items.Count != 0)
            {
                int countElements = 10;
                List<OneRow> trainData = TrainData.Train;
                Dictionary<int, decimal> bestValues = new Dictionary<int, decimal>();
                List<FisherCritery> fishers = new List<FisherCritery>();
                try
                {
                    int parameterID = XMLWork.FindIDWithName((string) parameterCondition.SelectedItem, Properties.Settings.Default.Languages);
                    decimal averageY = AverageOutput(trainData, parameterID);

                    //Fisher Critery
                    foreach (var item in trainData[0].Input)
                    {
                        Fisher fisher = new Fisher(trainData, item.Key, parameterID, averageY);
                        fisher.CalculateFisher();
                        fishers.Add(new FisherCritery(item.Key, fisher.Critery));
                    }

                    //Sort of data
                    ListOfElements fisherElements = new ListOfElements(fishers);
                    fisherElements.InsertionSort();
                    //Give Best Value
                    for (int i = 0; i < countElements; i++)
                    {
                        bestValues.Add(fisherElements.Fisher[i].Key, fisherElements.Fisher[i].Value);
                    }

                    //Clear Palette
                    this.chart1.Series.Clear();
                    this.chart1.Titles.Clear();
                    this.chart1.ChartAreas[0].AxisY.Maximum = Math.Round(Convert.ToDouble(bestValues.First().Value), 0) + 25;
                    this.chart1.ChartAreas[0].AxisY.Minimum = Math.Round(Convert.ToDouble(bestValues.Last().Value), 0) - 25;

                    // Set palette.
                    this.chart1.Palette = ChartColorPalette.SeaGreen;

                    // Set title.
                    this.chart1.Titles.Add("Critery F-Test");
                    double numberOfParameters = 1.0;

                    foreach (var item in bestValues)
                    {
                        // Add series.
                        Series series = this.chart1.Series.Add(XMLWork.FindNameWithID(item.Key, Properties.Settings.Default.Languages));
                        // Add point.
                        //series.Points.Add(Convert.ToDouble(item.Value));
                        series.Points.AddXY(numberOfParameters, item.Value);
                        numberOfParameters += 0.1;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(Localization.MyStrings.SelectCriterion);
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.ToString());
                }

                
            }
            else
            {
                MessageBox.Show(Localization.MyStrings.WithoutDefect);
            }
            // Add series.
        }


        /// <summary>
        /// Calculate the average value for one parameter
        /// </summary>
        /// <param name="outputData"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public decimal AverageOutput(List<OneRow> outputData, int id)
        {
            decimal sum = 0;
            foreach (var outputItem in outputData)
            {
                sum += outputItem.Output[id];
            }
            if (sum != 0)
                return sum / outputData.Count;
            return 0;
        }


        private int FindIDInDictionary(string name)
        {
            string t = name;
            string parameterDefect = TrainData.PairsRus.FirstOrDefault(o => o.Value == t).Key;
            int parameterID = TrainData.nameParameter.Single(o => o.Value == parameterDefect).Key;
            return parameterID;
        }

        private void RegressionModelcs_Load(object sender, EventArgs e)
        {

        }
    }
}
