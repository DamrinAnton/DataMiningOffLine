using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DM.Data;

namespace DataMiningOffLine
{
    public struct ResultNormalDistribution
    {
        public List<KeyValuePair<DateTime, double>> data;
        public bool normalDistribution;
        public string nameParametr;
    };

    public struct Option
    {
        public bool log;
        public bool unlog;
        public string singLevel;
        public bool language;
        public bool critFrocini;
        public bool critKS;
        public bool critHi;
        public bool minmax;
        public bool sigma;
    }

    public partial class NormalDistribution : Form
    {
        // ExcelParser parser;
        List<string> name_parametrs;
        public static Option options = new Option() { sigma = false, minmax = true, log = false, unlog = false, critHi = true, critFrocini = false, critKS = false, singLevel = "0.05", language = true };
        //   List<KeyValuePair<DateTime, double>> data;
        List<KeyValuePair<DateTime, double>> dataResult = new List<KeyValuePair<DateTime, double>>();
        public NormalDistribution()
        {
            if (Properties.Settings.Default.Languages == System.Globalization.CultureInfo.GetCultureInfo("en-US").Name)
                options.language = false;
            else
                options.language = true;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Languages))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
            }
            InitializeComponent();
            if (options.language)
                SelectedRussianLanguage();
            else
                SelectedEnglishLanguage();
            TrainData.GetData();
        }

        private void NormalDistribution_Shown(object sender, EventArgs e)
        {
            name_parametrs = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToList();// || o.StartsWith("OPC")).ToList();
            ListParam.Items.Clear();
            foreach (var item in name_parametrs)
                ListParam.Items.Add(XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages));
        }

        private void ButtonCalc_Click(object sender, EventArgs e)
        {
            if (ListParam.SelectedItem == null)
            {
                MessageBox.Show("Please select a parameter to display!", "Erroe!", MessageBoxButtons.OK);
                return;
            }
            string defaultName = ListParam.SelectedItem.ToString();
            ShowParameterData(defaultName);
        }

        void ShowParameterData(string parameterName)
        {
            if (Calculation.stringName != parameterName)
            {
                Calculation.stringName = parameterName;
                TextBoxColumnCount.Text = "";
                TextBoxMax.Text = "";
                TextBoxMin.Text = "";
            }
            List<KeyValuePair<DateTime, double>> data;

            int parameterID = XMLWork.FindIDWithName(parameterName, Properties.Settings.Default.Languages);
            var scadaName = XMLWork.FindScadaNameWithID(parameterID);
            if (scadaName.StartsWith("Def"))
            {
                data = (from row in TrainData.Train
                        select new KeyValuePair<DateTime, double>(row.Date, (double)row.Output[parameterID])).ToList();
            }
            else
            {
                data = (from row in TrainData.Train
                        select new KeyValuePair<DateTime, double>(row.Date, (double)row.Input[parameterID])).ToList();
            }

            /* double min, max;
             min = String.IsNullOrWhiteSpace(TextBoxMin.Text) ? data.Min(item => item.Value) : double.Parse(TextBoxMin.Text);
             TextBoxMin.Text = min.ToString();
             max = String.IsNullOrWhiteSpace(TextBoxMax.Text) ? data.Max(item => item.Value) : double.Parse(TextBoxMax.Text);
             TextBoxMax.Text = max.ToString();*/

            if (options.log)
            {
                data = data.Where(item => item.Value != 0).ToList();
                List<KeyValuePair<DateTime, double>> keyValuePairs = new List<KeyValuePair<DateTime, double>>();
                foreach (var d in data)
                    keyValuePairs.Add(new KeyValuePair<DateTime, double>(d.Key, Math.Round(Math.Log(d.Value), 2)));
                data = keyValuePairs;
                if (!String.IsNullOrWhiteSpace(TextBoxMin.Text) || !String.IsNullOrWhiteSpace(TextBoxMin.Text))
                {
                    if (data.Max(n => n.Value) < Convert.ToDouble(TextBoxMax.Text) || data.Min(n => n.Value) > Convert.ToDouble(TextBoxMin.Text))
                    {
                        TextBoxMin.Text = "";
                        TextBoxMax.Text = "";
                    }
                }
            }
            if (options.unlog)
            {
                List<KeyValuePair<DateTime, double>> keyValuePairs = new List<KeyValuePair<DateTime, double>>();
                foreach (var d in data)
                    keyValuePairs.Add(new KeyValuePair<DateTime, double>(d.Key, Math.Round(Math.Exp(d.Value), 2)));
                data = keyValuePairs;
                TextBoxMin.Text = "";
                TextBoxMax.Text = "";
            }
            if (options.sigma)
            {
                List<KeyValuePair<DateTime, double>> dataControl;
                double expectedValue = data.Sum(item => item.Value) / data.Count;
                double standartDeviation = Math.Sqrt(data.Sum(item => Math.Pow(item.Value - expectedValue, 2)) / (data.Count - 1));
                do
                {
                    data = data.Where(item => item.Value > (expectedValue - 3 * standartDeviation) && item.Value < (expectedValue + 3 * standartDeviation)).ToList();
                    expectedValue = data.Sum(item => item.Value) / data.Count;
                    standartDeviation = Math.Sqrt(data.Sum(item => Math.Pow(item.Value - expectedValue, 2)) / (data.Count - 1));
                    double a = expectedValue - 3 * standartDeviation;
                    double b = expectedValue + 3 * standartDeviation;
                    dataControl = data.Where(item => item.Value <= a || item.Value >= b).ToList();
                } while (dataControl.Count() > 0);
            }
            double min, max;
            if (options.language)
            {
                TextBoxMin.Text = TextBoxMin.Text.Replace(".", ",");
                TextBoxMax.Text = TextBoxMax.Text.Replace(".", ",");
            }
            else
            {
                TextBoxMin.Text = TextBoxMin.Text.Replace(",", ".");
                TextBoxMax.Text = TextBoxMax.Text.Replace(",", ".");
            }
            min = String.IsNullOrWhiteSpace(TextBoxMin.Text) ? data.Min(item => item.Value) : double.Parse(TextBoxMin.Text);
            TextBoxMin.Text = min.ToString();
            max = String.IsNullOrWhiteSpace(TextBoxMax.Text) ? data.Max(item => item.Value) : double.Parse(TextBoxMax.Text);
            TextBoxMax.Text = max.ToString();

            if (options.minmax)
            {
                data = data.Where(item => item.Value >= min && item.Value <= max).ToList();
            }
            dataResult = data;
            ShowChart1(data, parameterName);
            ShowChart2(data, parameterName);
        }

        void ShowChart1(List<KeyValuePair<DateTime, double>> data, string parameterName)
        {
            chart2.Series[0].Points.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                chart2.Series[0].Points.AddXY(data[i].Key.ToString(), data[i].Value);
            }
            chart2.ChartAreas[0].AxisY.Title = parameterName;
            if (options.language)
                chart2.ChartAreas[0].AxisX.Title = "Время";
            else
                chart2.ChartAreas[0].AxisX.Title = "Time";
            //chart1YAxis.Title = databaseHandler.GetAlias(parameterName, DatabaseHandler.AliasNameOptions.Default).RussianName;
        }

        void ShowChart2(List<KeyValuePair<DateTime, double>> data, string parameterName)
        {

            List<string> language = new List<string>();
            if (options.language)
            {
                language.Add("Плотность вероятности");
                language.Add("Мат. ожидание");
                language.Add("Среднеквадратичное отклонение");
                language.Add("Критерий Пирсона");
                language.Add("Критерий Колмогорова-Смирнова");
                language.Add("Критерий Форсини");
                language.Add("Гипотеза о нормальном распределении выборки не отклоняется");
                language.Add("Гипотеза о нормальном распределении выборки отклоняется");
            }
            else
            {
                language.Add("Probability density");
                language.Add("Math. expectation");
                language.Add("Standard deviation");
                language.Add("Pirson's criterion");
                language.Add("Criterion of Kolmogorova-Smirnova");
                language.Add("Forsini's criterion");
                language.Add("The hypothesis of the normal distribution of the sample is confirmed");
                language.Add("The hypothesis of the normal distribution of the sample is refuted");
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            try
            {
                List<KeyValuePair<DateTime, double>> oldData = data;
                ResultNormalDistribution result = new ResultNormalDistribution();
                List<KeyValuePair<double, double>> line;
                List<KeyValuePair<string, double>> columns;
                double expectedValue, standartDeviation, criterionHi, criticalCriterionHi, criterionKS, criticalCriterionKS, criterionF, criticalCriterionF;
                int columnscount;
                if (String.IsNullOrWhiteSpace(TextBoxColumnCount.Text))
                {
                    columnscount = Calculation.GetOptimalColumnsCount(data.Count);
                    TextBoxColumnCount.Text = columnscount.ToString();
                }
                else
                {
                    columnscount = int.Parse(TextBoxColumnCount.Text);
                }
                StringBuilder messageCriterions = new StringBuilder();
                Calculation.DoStuff(data, columnscount, out line, out columns, out expectedValue, out standartDeviation, out criterionHi, out criticalCriterionHi, out criterionKS, out criticalCriterionKS, out criterionF, out criticalCriterionF, parameterName, options);
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                chart1.Series[1].BorderWidth = 5;


                foreach (var item in columns)
                {
                    chart1.Series[0].Points.AddXY(item.Key, item.Value);
                }
                foreach (var item in line)
                {
                    chart1.Series[1].Points.AddXY(Math.Round(item.Key, 2), item.Value);
                }

                chart1.Series[0].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                //chart1.ChartAreas[0].AxisX2.
                chart1.ChartAreas[0].AxisX.Title = parameterName;
                chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Math.Round((Convert.ToDouble(TextBoxMax.Text) - Convert.ToDouble(TextBoxMin.Text)) / Convert.ToInt32(TextBoxColumnCount.Text), 2);
                chart1.ChartAreas[0].AxisX.Minimum = Math.Round(Convert.ToDouble(TextBoxMin.Text), 2);
                chart1.ChartAreas[0].AxisX.Maximum = Math.Round(Convert.ToDouble(TextBoxMax.Text), 2);

                chart1.ChartAreas[0].AxisY.Title = language[0];
                label4.Text = $"{language[1]} = {Math.Round(expectedValue, 2)}, {language[2]} = {Math.Round(standartDeviation, 2)}";

                StringBuilder resultCriterion = new StringBuilder();
                if (options.critHi)
                {
                    if (criterionHi > criticalCriterionHi)
                    {
                        resultCriterion.Append($"{language[3]}: \n");
                        resultCriterion.Append($"{language[7]}: {Math.Round(criterionHi, 5)} > {criticalCriterionHi} \n");
                    }
                    else
                    {
                        resultCriterion.Append($"{language[3]}: \n");
                        resultCriterion.Append($"{language[6]}: {Math.Round(criterionHi, 5)} <= {criticalCriterionHi}\n");
                    }
                }
                if (options.critKS)
                {
                    if (criterionKS > criticalCriterionKS)
                    {
                        resultCriterion.Append($"{language[4]}: \n");
                        resultCriterion.Append($"{language[7]}: {Math.Round(criterionKS, 5)} > {criticalCriterionKS} \n");
                    }
                    else
                    {
                        resultCriterion.Append($"{language[4]}: \n");
                        resultCriterion.Append($"{language[6]}: {Math.Round(criterionKS, 5)} <= {criticalCriterionKS}\n");
                    }
                }
                if (options.critFrocini)
                {
                    if (criterionF > criticalCriterionF)
                    {
                        resultCriterion.Append($"{language[5]}: \n");
                        resultCriterion.Append($"{language[7]}: {Math.Round(criterionF, 5)} > {criticalCriterionF} \n");
                    }
                    else
                    {
                        resultCriterion.Append($"{language[5]}: \n");
                        resultCriterion.Append($"{language[6]}: {Math.Round(criterionF, 5)} <= {criticalCriterionF}\n");
                    }
                }
                MessageBox.Show(resultCriterion.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListParam_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ButtonCalc_Click(null, null);
        }

        private void authorizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AuthorizationND authorizationND = new AuthorizationND(options);
                authorizationND.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void interfaceOfDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataBaseND db = new DataBaseND() { nameDB = "BDUsersND.db" };
                db.ConnectionBD();
                if (db.FindUserStatus() != 0)
                {
                    InterfaceAdminBDND interfaceAdminDB = new InterfaceAdminBDND(options);
                    interfaceAdminDB.Show();
                }
                else
                {
                    if (options.language)
                        MessageBox.Show("Вы не авторизовались.");
                    else
                        MessageBox.Show("You are not authorization.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Option newOptions = new Option();
            if (!OptionND.GetOptions(out newOptions, options))
                return;
            options = newOptions;
        }

        private void SelectedRussianLanguage()
        {
            optionsToolStripMenuItem.Text = "Файл";
            testToolStripMenuItem.Text = "Тест";
            authorizationToolStripMenuItem.Text = "Авторизация";
            interfaceOfDBToolStripMenuItem.Text = "Работа с базой данных";
            optionsToolStripMenuItem1.Text = "Настройки";
            label1.Text = "Кол-во интервалов";
            label2.Text = "Макс.";
            label3.Text = "Мин.";
            tabPage1.Text = "Данные";
            tabPage2.Text = "Плотность вероятности";
            ButtonCalc.Text = "Расчет";
        }

        private void SelectedEnglishLanguage()
        {
            optionsToolStripMenuItem.Text = "File";
            testToolStripMenuItem.Text = "Test";
            authorizationToolStripMenuItem.Text = "Authorization";
            interfaceOfDBToolStripMenuItem.Text = "Interface of DB";
            optionsToolStripMenuItem1.Text = "Options";
            label1.Text = "Number of intervals";
            label2.Text = "Max.";
            label3.Text = "Min.";
            tabPage1.Text = "Data";
            tabPage2.Text = "Probability density";
            ButtonCalc.Text = "Calculation";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataResult.Count() > 0)
            {
                try
                {
                    string defaltName;
                    string parameterName = ListParam.SelectedItem.ToString();
                    if (options.language == true)
                        defaltName = TrainData.PairsRus.Where(item => item.Value.Replace(" ", "") == parameterName.Replace(" ", "")).Single().Key;
                    else
                        defaltName = TrainData.Pairs.Where(item => item.Value.Replace(" ", "") == parameterName.Replace(" ", "")).Single().Key;
                    int idParameter = TrainData.nameParameter.Where(item => item.Value == defaltName).Single().Key;
                    List<OneRow> dataSet = new List<OneRow>();
                    foreach (var item in dataResult)
                    {
                        List<OneRow> data = new List<OneRow>();
                        data = TrainData.Train.Where(t => t.Date == item.Key).ToList();
                        foreach (var i in data)
                            dataSet.Add(i);
                    }

                    for (int index = 0; index < dataSet.Count(); index++)
                    {
                        dataSet[index].Output[idParameter] = (decimal)dataResult[index].Value;
                    }
                    TrainData.Train = dataSet;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
