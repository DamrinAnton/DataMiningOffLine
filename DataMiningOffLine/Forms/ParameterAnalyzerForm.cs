using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataMiningOffLine.Forms;
using DM.Data;
using DM.Helper;
using ScottPlot;

namespace DataMiningOffLine
{
  public partial class ParameterAnalyzerForm : Form
    {

        Cursor defCursor;
        bool dragging = false;
       // ChartControl chart;
       // XYDiagram diagram;
        //ConstantLine line;
        public ParameterAnalyzerForm()
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
            Errors();
        }

        private void ParameterAnalyzerForm_Shown(object sender, EventArgs e)
        {
            try
            {
                DateTime takeTime = TrainData.Train.OrderBy(o => o.Date).First().Date;
                timeBeforeExtruderPage0.Format = DateTimePickerFormat.Custom;
                timeBeforeExtruderPage0.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeBeforeExtruderPage0.Value = takeTime;
                timeBeforeCalenderPage1.Format = DateTimePickerFormat.Custom;
                timeBeforeCalenderPage1.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeBeforeCalenderPage1.Value = takeTime;
                timeBeforeTakePage2.Format = DateTimePickerFormat.Custom;
                timeBeforeTakePage2.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeBeforeTakePage2.Value = takeTime;
                timeBeforeTemperingPage3.Format = DateTimePickerFormat.Custom;
                timeBeforeTemperingPage3.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeBeforeTemperingPage3.Value = takeTime;
                timeBeforeWinderPage4.Format = DateTimePickerFormat.Custom;
                timeBeforeWinderPage4.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeBeforeWinderPage4.Value = takeTime;
                takeTime = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
                timeAfterExtruderPage0.Format = DateTimePickerFormat.Custom;
                timeAfterExtruderPage0.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeAfterExtruderPage0.Value = takeTime;
                checkedListBoxParametersPage0.Items.Clear();
                checkedListBoxControlExtruderPage0.Items.Clear();
                timeAfterCalenderPage1.Format = DateTimePickerFormat.Custom;
                timeAfterCalenderPage1.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeAfterCalenderPage1.Value = takeTime;
                timeAfterTakePage2.Format = DateTimePickerFormat.Custom;
                timeAfterTakePage2.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeAfterTakePage2.Value = takeTime;
                timeAfterTemperingPage3.Format = DateTimePickerFormat.Custom;
                timeAfterTemperingPage3.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeAfterTemperingPage3.Value = takeTime;
                timeAfterWinderPage4.Format = DateTimePickerFormat.Custom;
                timeAfterWinderPage4.CustomFormat = "dd/MM/yyyy HH-mm-ss";
                timeAfterWinderPage4.Value = takeTime;
                checkedListBoxControlQualityCalenderPage1.Items.Clear();
                checkedListBoxControlCalenderPage1.Items.Clear();
                checkedListBoxControlQualityTakePage2.Items.Clear();
                checkedListBoxControlTakePage2.Items.Clear();
                checkedListBoxControlQualityTemperingPage3.Items.Clear();
                checkedListBoxControlTemperingPage3.Items.Clear();
                checkedListBoxControlQualityWinderPage4.Items.Clear();
                checkedListBoxControlWinderPage4.Items.Clear();

                try
                {
                    List<int> keys = new List<int>();
                    Dictionary<int, decimal> firstRow = TrainData.Train.First().Input;

                    foreach (var item in firstRow)
                    {
                        keys.Add(item.Key);
                    }
                    foreach (int key in keys)
                    {
                        string parName = XMLWork.FindScadaNameWithID(key);
                        if ((parName.StartsWith("OPC_I") || (parName.StartsWith("OPC_V"))))
                            checkedListBoxControlExtruderPage0.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                        else if (parName.StartsWith("OPC_W"))
                            checkedListBoxControlCalenderPage1.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                        else if (parName.StartsWith("OPC_ABZ"))
                            checkedListBoxControlTakePage2.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                        else if (parName.StartsWith("OPC_TW"))
                            checkedListBoxControlTemperingPage3.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                        else
                            checkedListBoxControlWinderPage4.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                    }
                    List<int> keysOutpput = new List<int>();
                    Dictionary<int, decimal> firstRowOutput = TrainData.Train.First().Output;
                    foreach (var item in firstRowOutput)
                    {
                        keysOutpput.Add(item.Key);
                    }
                    foreach (int key in keysOutpput)
                    {
                        checkedListBoxParametersPage0.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                        checkedListBoxControlQualityCalenderPage1.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                        checkedListBoxControlQualityTakePage2.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                        checkedListBoxControlQualityTemperingPage3.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                        checkedListBoxControlQualityWinderPage4.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.ToString());
            }
        }

        /*private DevExpress.XtraCharts.Series CreateSeries(string parName)
        {
            var series = new DevExpress.XtraCharts.Series(parName, ViewType.Line);
            ((LineSeriesView)series.View).LineMarkerOptions.Visible = false;

            // Set the scale type for the series' arguments and values.
            series.ArgumentScaleType = ScaleType.DateTime;
            series.ValueScaleType = ScaleType.Numerical;

            series.SeriesPointsSorting = SortingMode.Ascending;
            series.SeriesPointsSortingKey = SeriesPointKey.Argument;

            return series;
        }*/

        private void buttonShowTrends_Click(object sender, EventArgs e)
        {
            try
            {
                
                #region First

                AddThrend2(uppLimitExtruderPage0, checkedListBoxParametersPage0, checkedListBoxControlExtruderPage0, chartTrendsPage0, chartErrorPage0, timeBeforeExtruderPage0, timeAfterExtruderPage0, upperDeviationPage0, lowerDeviationPage0);

                #endregion Firts

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void threndShows_Click(object sender, EventArgs e)
        {
            chartQualityCalenderPage1.plt.Clear();
            try
            {

                #region First

                AddThrend2(nominalValueCalenderPage1, checkedListBoxControlQualityCalenderPage1, checkedListBoxControlCalenderPage1, chartQualityCalenderPage1, chartCalenderPage1, timeBeforeCalenderPage1, timeAfterCalenderPage1, upLimitCalenderPage1, lowLimitCalenderPage1);


                #endregion Firts

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// Рисует графики для каждой стадии
        /// </summary>
        /// <param name="boxControl1">Список параметров(Как правило показателей качества первого checkboxa)</param>
        /// <param name="boxControl2">Список параметров(Как правило технологических показателей первого checkboxa)</param>
        /// <param name="chart1">Control для создания графиков(как правило показателей качества)</param>
        /// <param name="chart2">Control для создания графиков(как правило технологические параметры)</param>
        /// <param name="dt1">Время ограниченное снизу</param>
        /// <param name="dt2">Время ограниченное сверху</param>
       /* private void AddThrend(NumericUpDown nominalValue, CheckedListBox boxControl1, CheckedListBox boxControl2, ChartControl chart1, ChartControl chart2, DateTimePicker dt1, DateTimePicker dt2, NumericUpDown upPercent, NumericUpDown lowPercent)
        {
            double value = Convert.ToDouble(nominalValue.Value);
            #region First
            var sumPar = new List<string>();
            var parameters = new List<string>();
            //Добавление всех параметров чекнутых в первом листбоксе
            for (int i = 0; i < boxControl1.Items.Count; i++)
            {
                if (boxControl1.GetItemChecked(i))
                {
                    var parameter = boxControl1.Items[i].ToString();
                    string parName =XMLWork.FindScadaNameWithAnotherName(parameter,Properties.Settings.Default.Languages);
                    if (!parameters.Contains(parName))
                    {
                        parameters.Add(parName);
                        sumPar.Add(parName);
                    }
                }
            }
            //Добавление всех параметров чекнутых во втором листбоксе
            var parameters2 = new List<string>();
            for (int i = 0; i < boxControl2.Items.Count; i++)
            {
                if (boxControl2.GetItemChecked(i))
                {
                    var parameter = boxControl2.Items[i].ToString();
                    string parName = XMLWork.FindScadaNameWithAnotherName(parameter, Properties.Settings.Default.Languages);
                    if (!parameters.Contains(parName))
                    {
                        parameters2.Add(parName);
                        sumPar.Add(parName);
                    }
                }
            }

            //formate string with parameter identifiers
            var strBuilder = new StringBuilder();
            foreach (var par in parameters)
            {
                int parameterID = XMLWork.FindID(par);
                if (strBuilder.Length != 0)
                {
                    strBuilder.Append(", ");
                }
                strBuilder.Append(parameterID);
            }
            //formate string with parameter identifiers
            foreach (var par in parameters2)
            {
                int parameterID = XMLWork.FindID(par);
                if (strBuilder.Length != 0)
                {
                    strBuilder.Append(", ");
                }
                strBuilder.Append(parameterID);
            }

            //Построение графиков
            foreach (var parameter in parameters)
            {
                int parameterID = XMLWork.FindID(parameter);
                SeriesPoint points;
                var series = CreateSeries(XMLWork.FindNameWithID(parameterID, Properties.Settings.Default.Languages));
                foreach (OneRow row in TrainData.Train)
                {
                    if ((row.Date >= dt1.Value) && (row.Date <= dt2.Value))
                    {
                        points = new SeriesPoint(row.Date, row.Output[parameterID]);
                        series.Points.Add(points);
                    }
                }
                chart1.Series.Add(series);
            }

            //Построение графиков
            foreach (var parameter in parameters2)
            {
                int parameterID = XMLWork.FindID(parameter);
                SeriesPoint points;
                var series = CreateSeries(XMLWork.FindNameWithID(parameterID, Properties.Settings.Default.Languages));
                foreach (OneRow row in TrainData.Train)
                {
                    if ((row.Date >= dt1.Value) && (row.Date <= dt2.Value))
                    {
                        points = new SeriesPoint(row.Date, row.Input[parameterID]);
                        series.Points.Add(points);
                    }
                }
                chart2.Series.Add(series);
            }

            // Draw Limit Line (Constant)
            #region Constant Line
            var ser = CreateSeries("Upper Limit");
            ser.Points.Add(new SeriesPoint(dt1.Value, new double[] { value + value * 0.01 * Convert.ToDouble(upPercent.Value) }));
            ser.Points.Add(new SeriesPoint(dt2.Value, new double[] { value + value * 0.01 * Convert.ToDouble(upPercent.Value) }));
            chart1.Series.Add(ser);

            

            ser = CreateSeries("Low Limit");
            ser.Points.Add(new SeriesPoint(dt1.Value, new double[] { value - value * 0.01 * Convert.ToDouble(lowPercent.Value) }));
            ser.Points.Add(new SeriesPoint(dt2.Value, new double[] { value - value * 0.01 * Convert.ToDouble(lowPercent.Value) }));
            chart1.Series.Add(ser);
            var view = (LineSeriesView)chart1.Series[chart1.Series.Count - 1].View;
            view = (LineSeriesView)chart1.Series[chart1.Series.Count - 2].View;

            view.Color = Color.Red;
            #endregion
            #endregion Firts

        }*/
        /// <summary>
        /// Создает тренды по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threndShowsTakeOff_Click(object sender, EventArgs e)
        {
            try
            {
                AddThrend2(uppLimitTakePage2, checkedListBoxControlQualityTakePage2, checkedListBoxControlTakePage2, chartQualityTakePage2, chartTakePage2,timeBeforeTakePage2,timeAfterTakePage2, upPercentTakeOffRollPage2, lowPercentTakeOffRollPage2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// Создает тренды по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threndShowsTempering_Click(object sender, EventArgs e)
        {
            try
            {
                AddThrend2(uppLimitTemperingRollPage3, checkedListBoxControlQualityTemperingPage3, checkedListBoxControlTemperingPage3, chartQualityTemperingPage3, chartTemperingPage3, timeBeforeTemperingPage3, timeAfterTemperingPage3, upPercentTemperingRollPage3, lowPercentTemperingRollPage3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// Создает тренды по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threndShowsWinder_Click(object sender, EventArgs e)
        {
            try
            {
                AddThrend2(upperLimitWinderPage4, checkedListBoxControlQualityWinderPage4, checkedListBoxControlWinderPage4, chartQualityWinderPage4, chartWinderPage4, timeBeforeWinderPage4, timeAfterWinderPage4, upPercentWinderPage4, lowPercentWinderPage4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #region ClickChart
        /*private void chartTrends_Click(object sender, EventArgs e)
        {
            /*ChartControl chartControl = sender as ChartControl;
            XYDiagram diagram = chartControl.Diagram as XYDiagram;
            diagram.AxisX.ConstantLines.Clear();
            MouseEventArgs me = e as MouseEventArgs;
            if (diagram == null)
                return;

            // Get the information about the clicked point.
            DiagramCoordinates coords = diagram.PointToDiagram(me.Location);

            if (coords != null)
            {
                ConstantLine constantLine1 = new ConstantLine("Constant Line 1");
                diagram.AxisX.ConstantLines.Add(constantLine1);

                // Define its axis value.
                TimeSpan ts = new TimeSpan(0, 0, ProductionTime.ExtruderTime);
                constantLine1.AxisValue = coords.DateTimeArgument;

                // Customize the behavior of the constant line.
                constantLine1.Visible = true;
                constantLine1.ShowInLegend = true;
                constantLine1.LegendText = "Some Threshold";
                constantLine1.ShowBehind = false;

                // Customize the constant line's title.
                constantLine1.Title.Visible = true;
                constantLine1.Title.Text = " ";

                constantLine1.Title.TextColor = Color.Red;
                constantLine1.Title.Antialiasing = false;
                constantLine1.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                constantLine1.Title.ShowBelowLine = true;
                constantLine1.Title.Alignment = ConstantLineTitleAlignment.Far;

                // Customize the appearance of the constant line.
                constantLine1.Color = Color.Red;
                constantLine1.LineStyle.DashStyle = DashStyle.Dash;
                constantLine1.LineStyle.Thickness = 2;

                ConstantLine constantLine2 = new ConstantLine("Constant Line 2");
                XYDiagram diagram2 = chartErrorPage0.Diagram as XYDiagram;
                diagram2.AxisX.ConstantLines.Clear();
                diagram2.AxisX.ConstantLines.Add(constantLine2);

                // Define its axis value.
                constantLine2.AxisValue = coords.DateTimeArgument - ts;

                // Customize the behavior of the constant line.
                constantLine2.Visible = true;
                constantLine2.ShowInLegend = true;
                constantLine2.LegendText = "Some Threshold";
                constantLine2.ShowBehind = false;

                // Customize the constant line's title.
                constantLine2.Title.Visible = true;
                constantLine2.Title.Text = " ";

                constantLine2.Title.TextColor = Color.Red;
                constantLine2.Title.Antialiasing = false;
                constantLine2.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                constantLine2.Title.ShowBelowLine = true;
                constantLine2.Title.Alignment = ConstantLineTitleAlignment.Far;

                // Customize the appearance of the constant line.
                constantLine2.Color = Color.Red;
                constantLine2.LineStyle.DashStyle = DashStyle.Dash;
                constantLine2.LineStyle.Thickness = 2;
            }            
        }*/
        private void ChartTrendsPage0_MouseClicked(object sender, MouseEventArgs e)
        {
            //Get production time
            TimeSpan ts = new TimeSpan(0, 0, ProductionTime.ExtruderTime);
            double xOADate = chartTrendsPage0.plt.CoordinateFromPixelD(e.X, 0).X;
            chartTrendsPage0.PlaceSingleConstantLine(xOADate, "Some threshold");
            if (chartErrorPage0.plt.GetPlottables().Count > 2)
            {
                chartErrorPage0.PlaceSingleConstantLine((DateTime.FromOADate(xOADate) - ts).ToOADate(), "Some threshold");
                chartErrorPage0.Render();
            }
        }

        /* private void chartQualityCalender_Click(object sender, EventArgs e)
         {
             ChartControl chartControl = sender as ChartControl;
             XYDiagram diagram = chartControl.Diagram as XYDiagram;
             diagram.AxisX.ConstantLines.Clear();
             MouseEventArgs me = e as MouseEventArgs;
             if (diagram == null)
                 return;

             // Get the information about the clicked point.
             DiagramCoordinates coords = diagram.PointToDiagram(me.Location);

             if (coords != null)
             {
                 ConstantLine constantLine1 = new ConstantLine("Constant Line 1");
                 diagram.AxisX.ConstantLines.Add(constantLine1);

                 // Define its axis value.
                 TimeSpan ts = new TimeSpan(0, 0, ProductionTime.CalenderTime);
                 constantLine1.AxisValue = coords.DateTimeArgument;

                 // Customize the behavior of the constant line.
                 constantLine1.Visible = true;
                 constantLine1.ShowInLegend = true;
                 constantLine1.LegendText = "Some Threshold";
                 constantLine1.ShowBehind = false;

                 // Customize the constant line's title.
                 constantLine1.Title.Visible = true;
                 constantLine1.Title.Text = " ";

                 constantLine1.Title.TextColor = Color.Red;
                 constantLine1.Title.Antialiasing = false;
                 constantLine1.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                 constantLine1.Title.ShowBelowLine = true;
                 constantLine1.Title.Alignment = ConstantLineTitleAlignment.Far;

                 // Customize the appearance of the constant line.
                 constantLine1.Color = Color.Red;
                 constantLine1.LineStyle.DashStyle = DashStyle.Dash;
                 constantLine1.LineStyle.Thickness = 2;

                 ConstantLine constantLine2 = new ConstantLine("Constant Line 2");
                 XYDiagram diagram2 = chartCalenderPage1.Diagram as XYDiagram;
                 diagram2.AxisX.ConstantLines.Clear();
                 diagram2.AxisX.ConstantLines.Add(constantLine2);

                 // Define its axis value.
                 constantLine2.AxisValue = coords.DateTimeArgument - ts;

                 // Customize the behavior of the constant line.
                 constantLine2.Visible = true;
                 constantLine2.ShowInLegend = true;
                 constantLine2.LegendText = "Some Threshold";
                 constantLine2.ShowBehind = false;

                 // Customize the constant line's title.
                 constantLine2.Title.Visible = true;
                 constantLine2.Title.Text = " ";

                 constantLine2.Title.TextColor = Color.Red;
                 constantLine2.Title.Antialiasing = false;
                 constantLine2.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                 constantLine2.Title.ShowBelowLine = true;
                 constantLine2.Title.Alignment = ConstantLineTitleAlignment.Far;

                 // Customize the appearance of the constant line.
                 constantLine2.Color = Color.Red;
                 constantLine2.LineStyle.DashStyle = DashStyle.Dash;
                 constantLine2.LineStyle.Thickness = 2;
             }
         }*/

        private void ChartQualityCalenderPage1_MouseClicked(object sender, MouseEventArgs e)
        {
            //Get production time
            TimeSpan ts = new TimeSpan(0, 0, ProductionTime.CalenderTime);
            double xOADate = chartQualityCalenderPage1.plt.CoordinateFromPixelD(e.X, 0).X;
            chartQualityCalenderPage1.PlaceSingleConstantLine(xOADate, "Some threshold");
            if (chartCalenderPage1.plt.GetPlottables().Count > 2)
            {
                chartCalenderPage1.PlaceSingleConstantLine((DateTime.FromOADate(xOADate) - ts).ToOADate(), "Some threshold");
                chartCalenderPage1.Render();
            }
        }
        /*private void chartQualityTake_Click(object sender, EventArgs e)
        {
            ChartControl chartControl = sender as ChartControl;
            XYDiagram diagram = chartControl.Diagram as XYDiagram;
            diagram.AxisX.ConstantLines.Clear();
            MouseEventArgs me = e as MouseEventArgs;
            if (diagram == null)
                return;

            // Get the information about the clicked point.
            DiagramCoordinates coords = diagram.PointToDiagram(me.Location);

            if (coords != null)
            {
                ConstantLine constantLine1 = new ConstantLine("Constant Line 1");
                diagram.AxisX.ConstantLines.Add(constantLine1);

                // Define its axis value.
                TimeSpan ts = new TimeSpan(0, 0, ProductionTime.TakeOffTime);
                constantLine1.AxisValue = coords.DateTimeArgument;

                // Customize the behavior of the constant line.
                constantLine1.Visible = true;
                constantLine1.ShowInLegend = true;
                constantLine1.LegendText = "Some Threshold";
                constantLine1.ShowBehind = false;

                // Customize the constant line's title.
                constantLine1.Title.Visible = true;
                constantLine1.Title.Text = " ";

                constantLine1.Title.TextColor = Color.Red;
                constantLine1.Title.Antialiasing = false;
                constantLine1.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                constantLine1.Title.ShowBelowLine = true;
                constantLine1.Title.Alignment = ConstantLineTitleAlignment.Far;

                // Customize the appearance of the constant line.
                constantLine1.Color = Color.Red;
                constantLine1.LineStyle.DashStyle = DashStyle.Dash;
                constantLine1.LineStyle.Thickness = 2;
                try
                {
                    ConstantLine constantLine2 = new ConstantLine("Constant Line 2");
                    XYDiagram diagram2 = chartTakePage2.Diagram as XYDiagram;
                    diagram2.AxisX.ConstantLines.Clear();
                    diagram2.AxisX.ConstantLines.Add(constantLine2);
                    // Define its axis value.
                    constantLine2.AxisValue = coords.DateTimeArgument - ts;

                    // Customize the behavior of the constant line.
                    constantLine2.Visible = true;
                    constantLine2.ShowInLegend = true;
                    constantLine2.LegendText = "Some Threshold";
                    constantLine2.ShowBehind = false;

                    // Customize the constant line's title.
                    constantLine2.Title.Visible = true;
                    constantLine2.Title.Text = " ";

                    constantLine2.Title.TextColor = Color.Red;
                    constantLine2.Title.Antialiasing = false;
                    constantLine2.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                    constantLine2.Title.ShowBelowLine = true;
                    constantLine2.Title.Alignment = ConstantLineTitleAlignment.Far;

                    // Customize the appearance of the constant line.
                    constantLine2.Color = Color.Red;
                    constantLine2.LineStyle.DashStyle = DashStyle.Dash;
                    constantLine2.LineStyle.Thickness = 2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }*/

        private void ChartQualityTakePage2_MouseClicked(object sender, MouseEventArgs e)
        {
            //Get production time
            TimeSpan ts = new TimeSpan(0, 0, ProductionTime.TakeOffTime);
            double xOADate = chartQualityTakePage2.plt.CoordinateFromPixelD(e.X, 0).X;
            chartQualityTakePage2.PlaceSingleConstantLine(xOADate, "Some threshold");
            if (chartTakePage2.plt.GetPlottables().Count > 2)
            {
                chartTakePage2.PlaceSingleConstantLine((DateTime.FromOADate(xOADate) - ts).ToOADate(), "Some threshold");
                chartTakePage2.Render();
            }
        }

        /*private void chartQualityTempering_Click(object sender, EventArgs e)
        {
            ChartControl chartControl = sender as ChartControl;
            XYDiagram diagram = chartControl.Diagram as XYDiagram;
            diagram.AxisX.ConstantLines.Clear();
            MouseEventArgs me = e as MouseEventArgs;
            if (diagram == null)
                return;

            // Get the information about the clicked point.
            DiagramCoordinates coords = diagram.PointToDiagram(me.Location);

            if (coords != null)
            {
                ConstantLine constantLine1 = new ConstantLine("Constant Line 1");
                diagram.AxisX.ConstantLines.Add(constantLine1);

                // Define its axis value.
                TimeSpan ts = new TimeSpan(0, 0, ProductionTime.TemperingTime);
                constantLine1.AxisValue = coords.DateTimeArgument;

                // Customize the behavior of the constant line.
                constantLine1.Visible = true;
                constantLine1.ShowInLegend = true;
                constantLine1.LegendText = "Some Threshold";
                constantLine1.ShowBehind = false;

                // Customize the constant line's title.
                constantLine1.Title.Visible = true;
                constantLine1.Title.Text = " ";

                constantLine1.Title.TextColor = Color.Red;
                constantLine1.Title.Antialiasing = false;
                constantLine1.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                constantLine1.Title.ShowBelowLine = true;
                constantLine1.Title.Alignment = ConstantLineTitleAlignment.Far;

                // Customize the appearance of the constant line.
                constantLine1.Color = Color.Red;
                constantLine1.LineStyle.DashStyle = DashStyle.Dash;
                constantLine1.LineStyle.Thickness = 2;

                ConstantLine constantLine2 = new ConstantLine("Constant Line 2");
                XYDiagram diagram2 = chartTemperingPage3.Diagram as XYDiagram;
                diagram2.AxisX.ConstantLines.Clear();
                diagram2.AxisX.ConstantLines.Add(constantLine2);

                // Define its axis value.
                constantLine2.AxisValue = coords.DateTimeArgument - ts;

                // Customize the behavior of the constant line.
                constantLine2.Visible = true;
                constantLine2.ShowInLegend = true;
                constantLine2.LegendText = "Some Threshold";
                constantLine2.ShowBehind = false;

                // Customize the constant line's title.
                constantLine2.Title.Visible = true;
                constantLine2.Title.Text = " ";

                constantLine2.Title.TextColor = Color.Red;
                constantLine2.Title.Antialiasing = false;
                constantLine2.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                constantLine2.Title.ShowBelowLine = true;
                constantLine2.Title.Alignment = ConstantLineTitleAlignment.Far;

                // Customize the appearance of the constant line.
                constantLine2.Color = Color.Red;
                constantLine2.LineStyle.DashStyle = DashStyle.Dash;
                constantLine2.LineStyle.Thickness = 2;
            }
        }*/

        private void ChartQualityTemperingPage3_MouseClicked(object sender, MouseEventArgs e)
        {
            //Get production time
            TimeSpan ts = new TimeSpan(0, 0, ProductionTime.TemperingTime);
            double xOADate = chartQualityTemperingPage3.plt.CoordinateFromPixelD(e.X, 0).X;
            chartQualityTemperingPage3.PlaceSingleConstantLine(xOADate, "Some threshold");
            if (chartTemperingPage3.plt.GetPlottables().Count > 2)
            {
                chartTemperingPage3.PlaceSingleConstantLine((DateTime.FromOADate(xOADate) - ts).ToOADate(), "Some threshold");
                chartTemperingPage3.Render();
            }
        }
        #endregion

        #region WorkWithHelper
        private List<RecomendationsStruct> _error_311 = ExeptionsData.getInstance().GetList_error_311_RU();
        private List<RecomendationsStruct> _error_715 = ExeptionsData.getInstance().GetList_error_715_RU();

        /*  Вызов методов для занесения стандартных параметров в
         *  -Перечень ошибок
         */
        

        // Перечень ошибок и их названия
        private void Errors() {
            errorsNamesDataGridView.Rows.Add(new String[] { "311", "ЧЕРНЫЕ ТОЧКИ" });
            errorsNamesDataGridView.Rows.Add(new String[] { "715/716", "НЕПРАВИЛЬНАЯ УСАДКА ПРИ НАГРЕВАНИИ" });            
        }

        // Создание колонок и привязка списка для ошибок ### к View DataGrid
        private void CreatingGrid(List<RecomendationsStruct> errorList)
        {
            RecomendsDataGridView.AutoGenerateColumns = false;
            RecomendsDataGridView.AllowUserToAddRows = false;
            RecomendsDataGridView.DataSource = errorList;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "Reason";
            column1.HeaderText = "ПРИЧИНА";
            column1.DataPropertyName = "Reason";
            RecomendsDataGridView.Columns.Add(column1);

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "Actions";
            column2.HeaderText = "ДЕЙСТВИЕ НА ПРИЧИНУ";
            column2.DataPropertyName = "Actions";
            RecomendsDataGridView.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "ControlParameter";
            column3.HeaderText = "КОНТРОЛИРУЕМЫЕ ПАРАМЕТРЫ";
            column3.DataPropertyName = "ControlParameter";
            RecomendsDataGridView.Columns.Add(column3);
        }

        // Обработка выбранной позиции из списка ошибок (Errors)
        private void errorsNamesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                    RecomendsDataGridView.Columns.RemoveAt(0);
            }
            catch { }

            if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("311"))
            {
                CreatingGrid(_error_311);
            }
            else if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("715/716"))
            {
                CreatingGrid(_error_715);
            }
        }
#endregion

        private void shrinkageCalculate_Click(object sender, EventArgs e)
        {
            /* chartQualityTakePage2.Series.Clear();
             var series = CreateSeries("Shrinkage value");
             TimeSpan ts = new TimeSpan(ProductionTime.TakeOffTime);
             foreach (OneRow row in TrainData.Train)
             {
                 DataSet AuthorsDataSet = new DataSet();

                 AuthorsDataSet.ReadXml(XMLWork.PathShrinkage);
                 String lang = Properties.Settings.Default.Languages;

                 if ((row.Date >= timeBeforeTakePage2.Value) && (row.Date <= timeAfterTakePage2.Value))
                 {
                     string shrinkageValues = XMLWork.FindShrinkageWithTimestamp(row.Date);
                     if (lang.Equals("en-US"))
                     {
                         var points = new SeriesPoint(row.Date - ts, Convert.ToDecimal(shrinkageValues));
                         series.Points.Add(points);
                     }
                     else
                     {
                         var points = new SeriesPoint(row.Date - ts, Convert.ToDecimal(shrinkageValues.Replace(".", ",")));
                         series.Points.Add(points);
                     }
                 }
             }

             chartQualityTakePage2.Series.Add(series); // Построение тренда
             #region Constant Line
             var ser = CreateSeries("Upper Limit");
             ser.Points.Add(new SeriesPoint(timeBeforeTakePage2.Value, new double[] { Convert.ToDouble(uppLimitTakePage2.Text.Replace(".", ",")) + 0.01 * Convert.ToDouble(upPercentTakeOffRollPage2.Text) }));
             ser.Points.Add(new SeriesPoint(timeAfterTakePage2.Value, new double[] { Convert.ToDouble(uppLimitTakePage2.Text.Replace(".", ",")) + 0.01 * Convert.ToDouble(upPercentTakeOffRollPage2.Text) }));
             chartQualityTakePage2.Series.Add(ser);



             ser = CreateSeries("Low Limit");
             ser.Points.Add(new SeriesPoint(timeBeforeTakePage2.Value, new double[] { Convert.ToDouble(uppLimitTakePage2.Text.Replace(".", ",")) - 0.01 * Convert.ToDouble(lowPercentTakeOffRollPage2.Text) }));
             ser.Points.Add(new SeriesPoint(timeAfterTakePage2.Value, new double[] { Convert.ToDouble(uppLimitTakePage2.Text.Replace(".", ",")) - 0.01 * Convert.ToDouble(lowPercentTakeOffRollPage2.Text) }));
             chartQualityTakePage2.Series.Add(ser);
             var view = (LineSeriesView)chartQualityTakePage2.Series[chartQualityTakePage2.Series.Count - 1].View;
             view.Color = Color.Red;
             view = (LineSeriesView)chartQualityTakePage2.Series[chartQualityTakePage2.Series.Count - 2].View;

             view.Color = Color.Red;
             #endregion*/
            ShrinkageCalculate();
        }

        void ShrinkageCalculate()
        {
            chartQualityTakePage2.plt.Clear();
            chartQualityTakePage2.RemoveSingleConstantLine();
            chartTakePage2.RemoveSingleConstantLine();
            TimeSpan ts = new TimeSpan(ProductionTime.TakeOffTime);
            double startOADate = double.PositiveInfinity;
            double endOADate = double.NegativeInfinity;
            List<double> xs = new List<double>();
            List<double> ys = new List<double>();
            double minShrinkage = double.PositiveInfinity;
            double maxShrinkage = double.NegativeInfinity;
            foreach (OneRow row in TrainData.Train)
            {
                DataSet AuthorsDataSet = new DataSet();

                AuthorsDataSet.ReadXml(XMLWork.PathShrinkage);
                String lang = Properties.Settings.Default.Languages;

                if ((row.Date >= timeBeforeTakePage2.Value) && (row.Date <= timeAfterTakePage2.Value))
                {
                    double oaDate = row.Date.ToOADate();
                    if (oaDate < startOADate)
                        startOADate = oaDate;
                    if (oaDate > endOADate)
                        endOADate = oaDate;
                    string shrinkageValues = XMLWork.FindShrinkageWithTimestamp(row.Date);
                    if (lang.Equals("en-US"))
                    {
                        xs.Add((row.Date - ts).ToOADate());
                        double values = Convert.ToDouble(shrinkageValues);
                        if (values > maxShrinkage)
                            maxShrinkage = values;
                        if (values < minShrinkage)
                            minShrinkage = values;
                        ys.Add(values);
                    }
                    else
                    {
                        double values = Convert.ToDouble(shrinkageValues.Replace(".", ","));
                        if (values > maxShrinkage)
                            maxShrinkage = values;
                        if (values < minShrinkage)
                            minShrinkage = values;
                        xs.Add((row.Date - ts).ToOADate());
                        ys.Add(Convert.ToDouble(values));
                    }
                }
            }
            chartQualityTakePage2.plt.Ticks(dateTimeX: true);
            chartQualityTakePage2.plt.PlotScatter(xs.ToArray(), ys.ToArray(), lineWidth: 2, markerShape: MarkerShape.none, label: "Shrinkage value");
            chartQualityTakePage2.plt.Legend(location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            chartQualityTakePage2.plt.Axis(startOADate, endOADate, minShrinkage - 5, maxShrinkage + 5);

            chartQualityTakePage2.plt.PlotHLine(Convert.ToDouble(uppLimitTakePage2.Text.Replace(".", ",")) + 0.01 * Convert.ToDouble(upPercentTakeOffRollPage2.Text), lineWidth: 2, label: "Upper Limit");
            chartQualityTakePage2.plt.PlotHLine(Convert.ToDouble(uppLimitTakePage2.Text.Replace(".", ",")) - 0.01 * Convert.ToDouble(lowPercentTakeOffRollPage2.Text), lineWidth: 2, label: "Upper Limit");
            chartQualityTakePage2.Render();
        }

        //функция для вычисления величины усадки
        private decimal CalculateShrinkageValue(decimal[] temperatures, decimal[] velocities)
        {
            double[] L = new double[] { 0.74, 0.74, 0.74, 0.4, 0.08, 0.4, 0.08, 0.7, 0.08, 0.08, 0.7, 0.4, 0.7, 0.4, 0.7, 0.3, 0.7, 0.3, 0.3, 0.3 };
            double n = 0.4;
            double mu0 = 10000;
            double b = 0.04;
            double Tr = 170;
            double A = 7.46;
            //
            double B = -0.0014;
            double C = 17.68;
            var x1 = Math.Pow(2, n + 1);
            //double Thickness = 0.001, Width = 2;
            var gSum = 0.0;
            var result = new List<double>();
            double goalShrinkage = 60;
            double sMin = 0, sMax = goalShrinkage * 1.2 / (temperatures.Length - 1);
            for (var i = 0; i < temperatures.Length - 1; ++i)
            {
                var x2 = Math.Pow(((double)velocities[i] * n) / (L[i] * (1 - n)), n) * Math.Pow(2, n + 1);
                var x3 = Math.Pow(1 - (double)velocities[i] / (double)velocities[i + 1], 1 - n);
                var temperature = (double)temperatures[i + 1] - Tr;
                var mu = mu0 * Math.Exp(-b * temperature);
                var G = x1 * x2 * x3 * mu;
                if (!double.IsNaN(G) && !double.IsInfinity(G))
                    gSum = G;
                const double step = 0.0005;
                var sum = gSum;

                var root = ScanRoot(Sl => (C * Math.Pow(Sl, 6) + A * Math.Pow(Sl, 4) + (B - C - sum) * Math.Pow(Sl, 3) - A * Sl * Sl - B), sMin, sMax, step, 1000);
                sMin += root;
                sMax += root;
                if (!double.IsNaN(root) && Math.Abs(Math.Round(root, 5) - 1) > 0.001)
                    result.Add(root);

            }
            var shrinkage = (decimal)result.Sum(x => (x));
            return (shrinkage * 0.03m + velocities[6] * 0.1m);
            //return velocities[6]*0.2m;
        }
        //вспомогательная функция для вычисления усадки: поиск корня уравнения
        public static double ScanRoot(Func<double, double> phi, double a, double b, double eps, int n)
        {
            //double x = (a + b) / 2;
            int nMax = n;
            double x = a;
            while (nMax > 0 && eps > 10e-6)
            {
                for (; x < b; x += eps)
                {
                    double f = phi(x);
                    if (Math.Abs(f) < eps)
                        return x;
                    --nMax;
                }
                eps /= 10;
            }
            return a;
        }

        private void AddThrend2(NumericUpDown nominalValue, CheckedListBox boxControl1, CheckedListBox boxControl2, FormsPlot chart1, FormsPlot chart2, DateTimePicker dt1, DateTimePicker dt2, NumericUpDown upPercent, NumericUpDown lowPercent)
        {
            double value = Convert.ToDouble(nominalValue.Value);
            #region First
            var sumPar = new List<string>();
            var parameters = new List<string>();
            //Добавление всех параметров чекнутых в первом листбоксе
            for (int i = 0; i < boxControl1.Items.Count; i++)
            {
                if (boxControl1.GetItemChecked(i))
                {
                    var parameter = boxControl1.Items[i].ToString();
                    string parName = XMLWork.FindScadaNameWithAnotherName(parameter, Properties.Settings.Default.Languages);
                    if (!parameters.Contains(parName))
                    {
                        parameters.Add(parName);
                        sumPar.Add(parName);
                    }
                }
            }
            //Добавление всех параметров чекнутых во втором листбоксе
            var parameters2 = new List<string>();
            for (int i = 0; i < boxControl2.Items.Count; i++)
            {
                if (boxControl2.GetItemChecked(i))
                {
                    var parameter = boxControl2.Items[i].ToString();
                    string parName = XMLWork.FindScadaNameWithAnotherName(parameter, Properties.Settings.Default.Languages);
                    if (!parameters.Contains(parName))
                    {
                        parameters2.Add(parName);
                        sumPar.Add(parName);
                    }
                }
            }

            //formate string with parameter identifiers
            var strBuilder = new StringBuilder();
            foreach (var par in parameters)
            {
                int parameterID = XMLWork.FindID(par);
                if (strBuilder.Length != 0)
                {
                    strBuilder.Append(", ");
                }
                strBuilder.Append(parameterID);
            }
            //formate string with parameter identifiers
            foreach (var par in parameters2)
            {
                int parameterID = XMLWork.FindID(par);
                if (strBuilder.Length != 0)
                {
                    strBuilder.Append(", ");
                }
                strBuilder.Append(parameterID);
            }

            chart1.plt.Clear();
            //Построение графиков
            foreach (var parameter in parameters)
            {
                int parameterID = XMLWork.FindID(parameter);
               // SeriesPoint points;
                string seriesName = XMLWork.FindNameWithID(parameterID, Properties.Settings.Default.Languages);
                double startOADate = double.PositiveInfinity;
                double endOADate = double.NegativeInfinity;
                List<double> xs = new List<double>();
                List<double> ys = new List<double>();
                foreach (OneRow row in TrainData.Train)
                {
                    if ((row.Date >= dt1.Value) && (row.Date <= dt2.Value))
                    {
                        double oaDate = row.Date.ToOADate();
                        if (oaDate < startOADate)
                            startOADate = oaDate;
                        if (oaDate > endOADate)
                            endOADate = oaDate;
                        xs.Add(oaDate);
                        ys.Add((double)row.Output[parameterID]);

                    }
                }
                chart1.plt.Ticks(dateTimeX: true);
                chart1.plt.PlotScatter(xs.ToArray(), ys.ToArray(), markerShape: MarkerShape.none, lineWidth: 2, label: seriesName);
                chart1.plt.Legend(location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
                chart1.plt.AxisAuto();
                chart1.plt.Axis(startOADate, endOADate);
            }

            chart2.plt.Clear();
            //Построение графиков
            foreach (var parameter in parameters2)
            {
                int parameterID = XMLWork.FindID(parameter);
                string seriesName = XMLWork.FindNameWithID(parameterID, Properties.Settings.Default.Languages);
                double startOADate = double.PositiveInfinity;
                double endOADate = double.NegativeInfinity;
                List<double> xs = new List<double>();
                List<double> ys = new List<double>();
                foreach (OneRow row in TrainData.Train)
                {
                    if ((row.Date >= dt1.Value) && (row.Date <= dt2.Value))
                    {
                        double oaDate = row.Date.ToOADate();
                        if (oaDate < startOADate)
                            startOADate = oaDate;
                        if (oaDate > endOADate)
                            endOADate = oaDate;
                        xs.Add(oaDate);
                        ys.Add((double)row.Input[parameterID]);
                    }
                }
                chart2.plt.Ticks(dateTimeX: true);
                chart2.plt.PlotScatter(xs.ToArray(), ys.ToArray(), lineWidth: 2, markerShape: MarkerShape.none, label: seriesName);
                chart2.plt.Legend(location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
                chart2.plt.AxisAuto();
                chart2.plt.Axis(startOADate, endOADate);
            }

            // Draw Limit Line (Constant)
            #region Constant Line
            chart1.plt.PlotHLine(value + value * 0.01 * Convert.ToDouble(upPercent.Value), lineWidth: 2, label: "Upper Limit");
            //   ser.Points.Add(new SeriesPoint(dt1.Value, new double[] { value + value * 0.01 * Convert.ToDouble(upPercent.Value) }));
            //  ser.Points.Add(new SeriesPoint(dt2.Value, new double[] { value + value * 0.01 * Convert.ToDouble(upPercent.Value) }));
            // chart1.Series.Add(ser);


            chart1.plt.PlotHLine(value - value * 0.01 * Convert.ToDouble(lowPercent.Value), lineWidth: 2, label: "Low Limit");
            // ser = CreateSeries("Low Limit");
            // ser.Points.Add(new SeriesPoint(dt1.Value, new double[] { value - value * 0.01 * Convert.ToDouble(lowPercent.Value) }));
            //  ser.Points.Add(new SeriesPoint(dt2.Value, new double[] { value - value * 0.01 * Convert.ToDouble(lowPercent.Value) }));
            // chart1.Series.Add(ser);
            //  var view = (LineSeriesView)chart1.Series[chart1.Series.Count - 1].View;
            // view = (LineSeriesView)chart1.Series[chart1.Series.Count - 2].View;

            //  view.Color = Color.Red;
            chart1.Render();
            chart2.Render();

            #endregion
            #endregion Firts

        }
    }
}
