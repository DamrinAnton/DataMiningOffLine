using DM.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataMiningOffLine.Forms.RegressionAnalysis;
using DataMiningOffLine.Helpers;

namespace DataMiningOffLine
{
    struct PointD
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointD(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public partial class RegressionAnalysisNew : Form
    {
        const int LineChartXAxisIntervalsCount = 20;//Количество интервалов на графике линейной регрессии по оси X
        const int LineChartYAxisIntervalsCount = 15;//Количество интервалов на графике линейной регрессии по оси Y
        const int EquasionSignificantDigitsCount = 4;//Количество значащих цифр при выводе уравнения линейной регрессии
        const int RMSESignificantDigitsCount = 5;   //Количество значащих цифр при выводе RMSE
        const int MultipleRegressionEquasionSignificantDigitsCount = 4;//Количество значащих цифр при выводе RMSE для множественной регрессии
        const int ErrorIntervalsCount = 10;//Количество интервалов на графике ошибок линейной регрессии
        const double SubscriptFontScaleFactor = 1.5;//Делитель размера шрифта для вывода надстрочных/подстрочных знаков
        const int BestSimpleRegressionsShowingCount = 10;//Количество отображаемых моделей при выводе линейных регрессий
        const double DataSensitivityThreshhold = 0.01;

        private string[] _inputParams, _qualityParams;
        private RegressionModelEquation _linearEquation, _multipleRegressionEquation;
        private RegressionModelsDatabaseHelper _databaseHelper;

        public RegressionAnalysisNew()
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
            _databaseHelper = new RegressionModelsDatabaseHelper();
        }
        #region HandlersPage1
        private void buttonShow_Click(object sender, EventArgs e)
        {
            if (comboBoxInputParams.Items.Count == 0 || comboBoxQualityParams.Items.Count == 0)
            {
                MessageBox.Show($"No data loaded!", "Exception", MessageBoxButtons.OK);
                return;
            }
            GetLinearRegressionModel(comboBoxQualityParams.SelectedItem.ToString(), comboBoxInputParams.SelectedItem.ToString());
        }
        #endregion

        #region HanglersPage2
        private void buttonCheckParameters_Click(object sender, EventArgs e)
        {
            GetDataForRSquaredGraph(comboBoxQualityParmetersPage2.SelectedItem.ToString());
        }

        private void buttonSelectAllInputParameters_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxInputParametersToCheck.Items.Count; i++)
                checkedListBoxInputParametersToCheck.SetItemChecked(i, true);
        }

        private void buttonUnselectAllInputParameters_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxInputParametersToCheck.Items.Count; i++)
                checkedListBoxInputParametersToCheck.SetItemChecked(i, false);
        }

        private void buttonFindOptimalInputParameters_Click(object sender, EventArgs e)
        {
            buttonSelectAllInputParameters_Click(null, null);
            buttonCheckParameters_Click(null, null);
            buttonUnselectAllInputParameters_Click(null, null);
        }
        #endregion

        #region HandlersPage3
        private void buttonBuildModelPage3_Click(object sender, EventArgs e)
        {
            GetMultipleRegressionModel(comboBoxQualityParametersPage3.SelectedItem.ToString());
        }

        private void buttonSelectAllInputParametersPage3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxInputParametersToCheckPage3.Items.Count; i++)
                checkedListBoxInputParametersToCheckPage3.SetItemChecked(i, true);
        }

        private void buttonSelectNoneInputParametersPage3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxInputParametersToCheckPage3.Items.Count; i++)
                checkedListBoxInputParametersToCheckPage3.SetItemChecked(i, false);
        }
        #endregion

        #region MethodsPage1
        void ShowProcessParameters()
        {
            string[] input, quality;
            input = _inputParams.Select(item => XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages)).ToArray();
            quality = _qualityParams.Select(item => XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages)).ToArray();

            comboBoxInputParams.Items.Clear();
            comboBoxInputParams.Items.AddRange(input);
            comboBoxInputParams.SelectedIndex = 0;
            comboBoxQualityParams.Items.Clear();
            comboBoxQualityParams.Items.AddRange(quality);
            comboBoxQualityParams.SelectedIndex = 0;
        }

        private void ShowLinearRegressionModel()
        {
            tabControlMain.SelectedIndex = 0;
            string qualityParam = XMLWork.FindNameWithID(_linearEquation.OutputParameter.Id.Value,
                Properties.Settings.Default.Languages);
            double[] coefficients =
                _linearEquation.InputParameters.Select(item => item.Coefficient).ToArray();
            string inputParam = XMLWork.FindNameWithID(_linearEquation.InputParameters[1].Id.Value,
                Properties.Settings.Default.Languages);

            for (int i = 0; i < comboBoxInputParams.Items.Count; i++)
                if (comboBoxInputParams.Items[i].Equals(inputParam))
                {
                    comboBoxInputParams.SelectedIndex = i;
                    break;
                }
            for (int i = 0; i < comboBoxQualityParams.Items.Count; i++)
                if (comboBoxQualityParams.Items[i].Equals(qualityParam))
                {
                    comboBoxQualityParams.SelectedIndex = i;
                    break;
                }
            labelRMSE.Text = _linearEquation.RMSEString;
            checkBoxNormalizeValuesPage1.Checked = _linearEquation.NormalizeValues;
            GetLinearRegressionModel(qualityParam, inputParam, false);
        }

        bool GetLinearRegressionModel(string qualityParam, string inputParam, bool writeModel = true)
        { 
            int inputParameterID = XMLWork.FindIDWithName(inputParam, Properties.Settings.Default.Languages);
            int qualityParameterID = XMLWork.FindIDWithName(qualityParam, Properties.Settings.Default.Languages);

            try
            {
                var generalData = (from row in TrainData.Train
                                   select new PointD { X = (double)row.Input[inputParameterID], Y = (double)row.Output[qualityParameterID] }).ToList();
                if (!generalData.Any(item => Math.Abs(item.X) > DataSensitivityThreshhold && Math.Abs(item.Y) > DataSensitivityThreshhold))
                    throw new Exception("Incorrect input data");
                DrawMainChart(generalData, writeModel);
                DrawErrorsChart(generalData);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing data: \"{ex.Message}\"", "Exception", MessageBoxButtons.OK);
                return false;
            }
        }

        void DrawMainChart(List<PointD> points, bool writeModel)
        {
            double[] linearRegression = Regression.LinearRegression(points, checkBoxNormalizeValuesPage1.Checked);
            var rmse = Regression.RMSE(points, linearRegression[1], linearRegression[0]);
            var series = chartRegressionModel.Series["SourceData"];
            series.Points.Clear();
            foreach (var point in points)
                series.Points.AddXY(point.X, point.Y, point.Y, linearRegression[0] + linearRegression[1] * point.X);
            double minX = (int)points.Min(item => item.X);
            double maxX = (int)points.Max(item => item.X);
            double minY = (int)points.Min(item => item.Y);
            double maxY = (int)points.Max(item => item.Y);
            if (writeModel)
            {
                _linearEquation = new RegressionModelEquation("DefaultName", linearRegression, rmse, minX, maxX, minY,
                    maxY,
                    comboBoxInputParams.SelectedItem.ToString(), comboBoxQualityParams.SelectedItem.ToString(), checkBoxNormalizeValuesPage1.Checked);
            }

            _linearEquation.GetLinearRegressionModelInfo(out string modelText, out string xAxisTitle, out string yAxisTitle, out double[] coefficients, out minX, out maxX, out string rmseString, out bool normalizeValues);
            labelEquasion.Text = modelText;
            labelRMSE.Text = rmseString;
            checkBoxNormalizeValuesPage1.Checked = normalizeValues;

            chartRegressionModel.ChartAreas["ChartAreaMain"].AxisX.Title = xAxisTitle;
            chartRegressionModel.ChartAreas["ChartAreaMain"].AxisY.Title = yAxisTitle;
            chartRegressionModel.ChartAreas["ChartAreaMain"].AxisX.Minimum = minX - 1;
            chartRegressionModel.ChartAreas["ChartAreaMain"].AxisX.Maximum = maxX + 1;
            chartRegressionModel.ChartAreas["ChartAreaMain"].AxisY.Minimum = minY - 1;
            chartRegressionModel.ChartAreas["ChartAreaMain"].AxisY.Maximum = maxY + 1;
            chartRegressionModel.ChartAreas["ChartAreaMain"].AxisX.Interval = (maxX - minX + 2) / LineChartXAxisIntervalsCount;
            chartRegressionModel.ChartAreas["ChartAreaMain"].AxisY.Interval = (maxY - minY + 2) / LineChartYAxisIntervalsCount;

            DrawMainChartLine(linearRegression, minX - 1, maxX + 1, minY - 1, maxY + 1);
        }

        void DrawMainChartLine(double[] functionCoefficients, double minX, double maxX, double minY, double maxY)
        {
            var series = chartRegressionModel.Series["RegressionLine"];
            series.Points.Clear();
            series.Points.AddXY(minX, functionCoefficients[0] + functionCoefficients[1] * minX);
            series.Points.AddXY(maxX, functionCoefficients[0] + functionCoefficients[1] * maxX);
        }

        void DrawErrorsChart(List<PointD> points)
        {
            int intervalsCount = ErrorIntervalsCount;
            List<double> errors = new List<double>();
            var linearRegression = Regression.LinearRegression(points, checkBoxNormalizeValuesPage1.Checked);
            foreach (var point in points)
                errors.Add(point.Y - (linearRegression[0] + linearRegression[1] * point.X));
            var minX = errors.Min();
            var maxX = errors.Max();
            double deltaError = (maxX - minX) / intervalsCount;
            var series = chartResiduals.Series["Histogram of residuals"];
            series.Points.Clear();
            for (int i = 0; i < intervalsCount; i++)
            {
                double startX = minX + i * deltaError;
                double endX = startX + deltaError;
                int count = errors.Count(item => item >= startX && item < endX);
                series.Points.AddXY(startX, 0, (double)count / points.Count / deltaError);
                series.Points.AddXY(endX, 0, (double)count / points.Count / deltaError);
            }
            chartResiduals.ChartAreas["ChartAreaMain"].AxisX.Interval = (maxX - minX) / intervalsCount;
            if (Properties.Settings.Default.Languages.Equals("en-US"))
            {
                chartResiduals.ChartAreas["ChartAreaMain"].AxisX.Title = "Residuals intervals";
                chartResiduals.ChartAreas["ChartAreaMain"].AxisY.Title = "Density";
            }
            else
            {
                chartResiduals.ChartAreas["ChartAreaMain"].AxisX.Title = "Интервалы отклонений";
                chartResiduals.ChartAreas["ChartAreaMain"].AxisY.Title = "Плотность";
            }
        }
        #endregion

        #region MethodsPage2
        void ShowParametersOnPage2()
        {
            if (checkedListBoxInputParametersToCheck.SelectedItems.Count != 0)
                return;
            string[] input, quality;
            input = _inputParams.Select(item => XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages)).ToArray();
            quality = _qualityParams.Select(item => XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages)).ToArray();
            comboBoxQualityParmetersPage2.Items.Clear();
            comboBoxQualityParmetersPage2.Items.AddRange(quality);
            comboBoxQualityParmetersPage2.SelectedIndex = 0;
            checkedListBoxInputParametersToCheck.Items.Clear();
            checkedListBoxInputParametersToCheck.Items.AddRange(input);
        }

        bool GetDataForRSquaredGraph(string qualityParam)
        {
            var allData = new Dictionary<string, KeyValuePair<double, double>>();
            var inputParametersToCheck = checkedListBoxInputParametersToCheck.CheckedItems.Cast<string>();

            int qualityParameterID = XMLWork.FindIDWithName(qualityParam, Properties.Settings.Default.Languages);

            try
            {
                foreach (var inputParam in inputParametersToCheck)
                {
                    int inputParameterID = XMLWork.FindIDWithName(inputParam, Properties.Settings.Default.Languages);

                    double rSquared, Fisher;
                    var generalData = (from row in TrainData.Train
                                       select new PointD { X = (double)row.Input[inputParameterID], Y = (double)row.Output[qualityParameterID] }).ToList();
                    if (!generalData.Any(item => Math.Abs(item.X) > DataSensitivityThreshhold && Math.Abs(item.Y) > DataSensitivityThreshhold))
                        throw new Exception("Incorrect input data");

                    Regression.Correlation(generalData, checkBoxNormalizeValuesPage2.Checked, out rSquared, out Fisher);
                    allData.Add(inputParam, new KeyValuePair<double, double>(rSquared, Fisher));

                }
                allData = allData.Where(item => !IsNanOrInfinity(item.Value)).OrderByDescending(item => item.Value.Key)
                    .Take(BestSimpleRegressionsShowingCount).ToDictionary(item => item.Key, item => item.Value);
                ShowRSquaredGraph(allData);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing data: \"{ex.Message}\"", "Exception", MessageBoxButtons.OK);
                return false;
            }
        }

        bool IsNanOrInfinity(KeyValuePair<double, double> pair)
        {
            return IsNanOrInfinity(pair.Key) | IsNanOrInfinity(pair.Value);
        }

        bool IsNanOrInfinity(double item)
        {
            if (double.IsNaN(item) || double.IsInfinity(item) || double.IsNegativeInfinity(item))
                return true;
            return false;
        }

        void ShowRSquaredGraph(Dictionary<string, KeyValuePair<double, double>> allData)
        {
            var seriesRSquared = chartRSquared.Series["R-Squared"];
            var seriesFisher = chartFisherCriteria.Series["Fisher criteria"];
            seriesRSquared.Points.Clear();
            seriesFisher.Points.Clear();
            foreach (var point in allData)
            {
                seriesRSquared.Points.AddXY(point.Key, point.Value.Key);
                seriesFisher.Points.AddXY(point.Key, point.Value.Value);
            }
        }
        #endregion

        #region Methodspage3
        void ShowParametersOnPage3()
        {
            if (checkedListBoxInputParametersToCheckPage3.SelectedItems.Count != 0)
                return;
            string[] input, quality;
            input = _inputParams.Select(item => XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages)).ToArray();
            quality = _qualityParams.Select(item => XMLWork.FindNameWithScada(item, Properties.Settings.Default.Languages)).ToArray();
            comboBoxQualityParametersPage3.Items.Clear();
            comboBoxQualityParametersPage3.Items.AddRange(quality);
            comboBoxQualityParametersPage3.SelectedIndex = 0;
            checkedListBoxInputParametersToCheckPage3.Items.Clear();
            checkedListBoxInputParametersToCheckPage3.Items.AddRange(input);
        }

        private void ShowMultipleRegressionModel()
        {
            tabControlMain.SelectedIndex = 2;
            string qualityParam = XMLWork.FindNameWithID(_multipleRegressionEquation.OutputParameter.Id.Value,
                Properties.Settings.Default.Languages);
            double[] coefficients =
                _multipleRegressionEquation.InputParameters.Select(item => item.Coefficient).ToArray();
            string[] inputParametersToCheck = _multipleRegressionEquation.InputParameters.Skip(1)
                .Select(item => XMLWork.FindNameWithID(item.Id.Value, Properties.Settings.Default.Languages)).ToArray();

            for (int i = 0; i < comboBoxQualityParametersPage3.Items.Count; i++)
                if (comboBoxQualityParametersPage3.Items[i].Equals(qualityParam))
                {
                    comboBoxQualityParametersPage3.SelectedIndex = i;
                    break;
                }
            for (int i = 0; i < checkedListBoxInputParametersToCheckPage3.Items.Count; i++)
                checkedListBoxInputParametersToCheckPage3.SetItemCheckState(i,
                    inputParametersToCheck.Contains(checkedListBoxInputParametersToCheckPage3.Items[i])
                        ? CheckState.Checked
                        : CheckState.Unchecked);
            labelRMSEPage3.Text = _multipleRegressionEquation.RMSEString;
            checkBoxNormalizeValuesPage3.Checked = _multipleRegressionEquation.NormalizeValues;
            ShowRegressionEquasion(qualityParam, coefficients);
            ShowRegressionEquasionHelp(inputParametersToCheck);
        }

        bool GetMultipleRegressionModel(string qualityParam)
        {
            double[,] processParametersData;
            double[] qualityParameterData;

            var inputParametersToCheck = checkedListBoxInputParametersToCheckPage3.CheckedItems.Cast<string>().ToList();
            int qualityParameterID = XMLWork.FindIDWithName(qualityParam, Properties.Settings.Default.Languages);
            processParametersData = new double[TrainData.Train.Count, inputParametersToCheck.Count + 1];
            qualityParameterData = new double[TrainData.Train.Count];
            //Setting first column of input values of regression matrix X with "1" values
            for (int i = 0; i < processParametersData.GetLength(0); i++)
                processParametersData[i, 0] = 1;

            List<double> parametersLowerBounds = new List<double>(), parametersUpperBounds = new List<double>();

            //Setting all input data
            for (int paramNumber = 0; paramNumber < inputParametersToCheck.Count; paramNumber++)
            {
                double minValue = double.MaxValue, maxValue = double.MinValue;
                int inputParameterID = XMLWork.FindIDWithName(inputParametersToCheck[paramNumber], Properties.Settings.Default.Languages);
                for (int rowNumber = 0; rowNumber < TrainData.Train.Count; rowNumber++)
                {
                    processParametersData[rowNumber, paramNumber + 1] =
                        (double)TrainData.Train[rowNumber].Input[inputParameterID];
                    if (processParametersData[rowNumber, paramNumber + 1] < minValue)
                        minValue = processParametersData[rowNumber, paramNumber + 1];
                    if (processParametersData[rowNumber, paramNumber + 1] > maxValue)
                        maxValue = processParametersData[rowNumber, paramNumber + 1];
                }
                parametersLowerBounds.Add(minValue);
                parametersUpperBounds.Add(maxValue);
            }

            //Setting output data
            for (int i = 0; i < TrainData.Train.Count; i++)
                qualityParameterData[i] = (double)TrainData.Train[i].Output[qualityParameterID];

            //Call for multiple regression calculation method
            try
            {
                for (int j = 1; j < processParametersData.GetLength(1); j++)
                {
                    bool throwData = true;
                    for (int i = 0; i < processParametersData.GetLength(0); i++)
                        if (Math.Abs(processParametersData[i, j]) > DataSensitivityThreshhold)
                        {
                            throwData = false;
                            break;
                        }
                    if (throwData)
                        throw new Exception("Incorrect input data");
                }
                if (!qualityParameterData.Any(item => Math.Abs(item) > DataSensitivityThreshhold))
                    throw new Exception("Incorrect input data");
                var coefficients = Regression.MultipleRegression(processParametersData, qualityParameterData, checkBoxNormalizeValuesPage3.Checked);

                double rmse = Regression.MultipleRMSE(processParametersData, qualityParameterData, coefficients);
                labelRMSEPage3.Text = $"RMSE = {Math.Round(rmse, RMSESignificantDigitsCount)}";

                _multipleRegressionEquation = new RegressionModelEquation("DefaultName", qualityParam,
                    inputParametersToCheck, rmse, coefficients,
                    inputParametersToCheck.ToArray(), parametersLowerBounds.ToArray(), parametersUpperBounds.ToArray(), checkBoxNormalizeValuesPage3.Checked);

                ShowRegressionEquasion(qualityParam, coefficients);
                ShowRegressionEquasionHelp(inputParametersToCheck.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception thrown in multiple regression calculation: \"{ex.Message}\"!");
                return false;
            }
        }

        void ShowRegressionEquasion(string qualityParam, double[] equationCoefficients)
        {
            Font font = richTextBoxRegressionEquasionPage3.Font;
            richTextBoxRegressionEquasionPage3.Clear();
            richTextBoxRegressionEquasionPage3.AppendText($"{qualityParam} = ");
            int pw;
            richTextBoxRegressionEquasionPage3.SelectedText = GetText(equationCoefficients[0], out pw);
            richTextBoxRegressionEquasionPage3.SelectionCharOffset = (int)(font.Size / SubscriptFontScaleFactor);
            richTextBoxRegressionEquasionPage3.SelectionFont = new Font(font.FontFamily, (int)(font.Size / SubscriptFontScaleFactor));
            richTextBoxRegressionEquasionPage3.SelectedText = pw.ToString();
            richTextBoxRegressionEquasionPage3.SelectionCharOffset = 0;
            richTextBoxRegressionEquasionPage3.SelectionFont = font;
            for (int i = 1; i < equationCoefficients.Length; i++)
            {
                richTextBoxRegressionEquasionPage3.AppendText(equationCoefficients[i] >= 0 ? " + " : " - ");
                richTextBoxRegressionEquasionPage3.SelectedText = GetText(Math.Abs(equationCoefficients[i]), out pw);
                richTextBoxRegressionEquasionPage3.SelectionCharOffset = (int)(font.Size / SubscriptFontScaleFactor);
                richTextBoxRegressionEquasionPage3.SelectionFont = new Font(font.FontFamily, (int)(font.Size / SubscriptFontScaleFactor));
                richTextBoxRegressionEquasionPage3.SelectedText = pw.ToString();
                richTextBoxRegressionEquasionPage3.SelectionCharOffset = 0;
                richTextBoxRegressionEquasionPage3.SelectionFont = font;
                richTextBoxRegressionEquasionPage3.SelectedText = "·X";
                richTextBoxRegressionEquasionPage3.SelectionCharOffset = (int)(-font.Size / SubscriptFontScaleFactor);
                richTextBoxRegressionEquasionPage3.SelectionFont = new Font(font.FontFamily, (int)(font.Size / SubscriptFontScaleFactor));
                richTextBoxRegressionEquasionPage3.SelectedText = i.ToString();
                richTextBoxRegressionEquasionPage3.SelectionCharOffset = 0;
                richTextBoxRegressionEquasionPage3.SelectionFont = font;
            }
        }

        void ShowRegressionEquasionHelp(string[] parametersNames)
        {
            Font font = richTextBoxRegressionEquasionPage3.Font;
            richTextBoxRegressionHelpPage3.Clear();
            for (int i = 0; i < parametersNames.Length; i++)
            {
                richTextBoxRegressionHelpPage3.SelectedText = "X";
                richTextBoxRegressionHelpPage3.SelectionCharOffset = (int)(-font.Size / 1.5);
                richTextBoxRegressionHelpPage3.SelectionFont = new Font(font.FontFamily, (int)(font.Size / 1.5));
                richTextBoxRegressionHelpPage3.SelectedText = (i + 1).ToString();
                richTextBoxRegressionHelpPage3.SelectionCharOffset = 0;
                richTextBoxRegressionHelpPage3.SelectionFont = font;
                richTextBoxRegressionHelpPage3.AppendText($" - \"{parametersNames[i]}\";{Environment.NewLine}");
            }
        }

        string GetText(double koeff, out int power)
        {
            string text = string.Format("{0:#." + new string('#', MultipleRegressionEquasionSignificantDigitsCount) + "E+00}", koeff);
            string resultText = text.Substring(0, text.IndexOf("E")) + "·10";
            power = int.Parse(text.Substring(text.IndexOf("E") + 1));
            return resultText;
        }
        #endregion

        private void RegressionAnalysisNew_Shown(object sender, EventArgs e)
        {
            _qualityParams = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
            _inputParams = TrainData.nameParameter.Values.Where(o => o.StartsWith("OPC")).ToArray();

            comboBoxInputParams.Items.Clear();
            comboBoxQualityParams.Items.Clear();
            foreach (var qualityParam in _qualityParams)
                comboBoxQualityParams.Items.Add(XMLWork.FindNameWithScada(qualityParam, Properties.Settings.Default.Languages));
            foreach (var inputParam in _inputParams)
                comboBoxInputParams.Items.Add(XMLWork.FindNameWithScada(inputParam, Properties.Settings.Default.Languages));
            comboBoxInputParams.SelectedIndex = comboBoxQualityParams.SelectedIndex = 0;
        }

        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 1: ShowParametersOnPage2(); break;
                case 2: ShowParametersOnPage3(); break;
            }
        }

        private void saveCurrentModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (tabControlMain.SelectedIndex)
            {
                case 0:
                    SaveRegressionModel(_linearEquation);
                    break;
                case 2:
                    SaveRegressionModel(_multipleRegressionEquation);
                    break;
            }
        }

        private void SaveRegressionModel(RegressionModelEquation model)
        {
            if (model == null)
            {
                MessageBox.Show("There is no model to save!");
                return;
            }
            ModelNameDateSelection.GetRegressionModelSaveInfo(out string name, out DateTime creationDate);
            _databaseHelper.AddRegressionModel(model, name, creationDate);
            MessageBox.Show($"Regression model {name} successfully saved!");
        }

        private void openRegressionModelsDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var allModels = _databaseHelper.GetAllRegressionModels();
            var loginResult = new AuthorizationRegressionAnalysis().ShowDialog();
            RegressionModelsDatabase databaseForm;
            switch (loginResult)
            {
                case DialogResult.Yes:
                    databaseForm = new RegressionModelsDatabase(_databaseHelper, allModels, true);
                    break;
                case DialogResult.No:
                    databaseForm = new RegressionModelsDatabase(_databaseHelper, allModels, false);
                    break;
                default: return;
            }
            var result = databaseForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                var selectedModel = databaseForm.SelectedEquation;
                if (selectedModel.IsLinearRegression)
                {
                    _linearEquation = selectedModel;
                    ShowLinearRegressionModel();
                }
                else
                {
                    _multipleRegressionEquation = selectedModel;
                    ShowMultipleRegressionModel();
                }
            }
        }
    }
}