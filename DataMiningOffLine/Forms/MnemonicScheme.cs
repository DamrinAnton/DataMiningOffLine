using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataMiningOffLine.Forms;
using DM.Data;
using ScottPlot;

namespace DataMiningOffLine
{
    public partial class MnemonicScheme : Form
    {
        public MnemonicScheme()
        {
            //Выбор языка меню
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Languages))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
            }
            InitializeComponent();
            //TODO: Если данных нету в файле будет ошибка. Нужно сделать try/catch. Когда будет не лень.
            if (TrainData.Train.Count != 0)
            {
                int parameterID = new int();
                //Считываем первый элемент
                OneRow sample = TrainData.Train.First(); // Обращение к первому элементу коллекции (В будущем по времени будем искать -- онлайн)
                TextBoxFeeling(sample,parameterID); // заполнение значений в текстбоксах текущими значениями технологических параметров
                parameterID = FindIDInDictionary(OPC_IA_shnek_K02.Name); // Поиск параметра в локальной базе данных
                //
                OPC_IA_shnek_K02.Text = Convert.ToString(sample.Input[parameterID]) + ConvertName(OPC_IA_shnek_K02.Name);
                parameterID = FindIDInDictionary(OPC_I_voronka_K02.Name);
                OPC_I_voronka_K02.Text = Convert.ToString(sample.Input[parameterID]) + ConvertName(OPC_I_voronka_K02.Name);
                parameterID = FindIDInDictionary(OPC_V_shnek_K02.Name);
                OPC_V_shnek_K02.Text = Convert.ToString(sample.Input[parameterID]) + ConvertName(OPC_V_shnek_K02.Name);
                parameterID = FindIDInDictionary(OPC_V_voronka_K02.Name);
                OPC_V_voronka_K02.Text = Convert.ToString(sample.Input[parameterID]) + ConvertName(OPC_V_voronka_K02.Name);
                CalculateCP();
            }
            else
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
        }

        /// <summary>
        /// Заполнения значений технологических параметров для всех textBox'ов
        /// </summary>
        /// <param name="sample"></param>
        /// <param name="parameterID"></param>
        private void TextBoxFeeling(OneRow sample, int parameterID)
        {
            //Добавление всех значений каландрового вала
            foreach (var textBoxControl in calenderRollPanel.Controls)
            {
                var t = textBoxControl as TextBox;
                if (t != null)
                {
                    parameterID = FindIDInDictionary(t.Name);
                    t.Text = Convert.ToString(sample.Input[parameterID]) + ConvertName(t.Name);
                }
            }
            //Добавление всех значений съемных валов
            foreach (var textBoxControl in takeOffRollPanel.Controls)
            {
                var t = textBoxControl as TextBox;
                if (t != null)
                {
                    parameterID = FindIDInDictionary(t.Name);
                    t.Text = Convert.ToString(sample.Input[parameterID]) + ConvertName(t.Name);
                }
            }
            //Добавление всех значений темперирующих валов
            foreach (var textBoxControl in temperingRollPanel.Controls)
            {
                var t = textBoxControl as TextBox;
                if (t != null)
                {
                    parameterID = FindIDInDictionary(t.Name);
                    t.Text = Convert.ToString(sample.Input[parameterID]) + ConvertName(t.Name);
                }
            }
            //Добавление всех значений стадии намотки
            foreach (var textBoxControl in winder.Controls)
            {
                var t = textBoxControl as TextBox;
                if (t != null)
                {
                    parameterID = FindIDInDictionary(t.Name);
                    t.Text = Convert.ToString(sample.Input[parameterID]) + ConvertName(t.Name);
                }
            }
            //Показатели качества добавляются сюда
            foreach (var textBoxControl in qualityPanel.Controls)
            {
                var t = textBoxControl as TextBox;
                if ((t != null) && (!t.Name.EndsWith("LCL")||!t.Name.EndsWith("UCL")|| !t.Name.EndsWith("CP"))) 
                {
                    string name = t.Name.Replace("_", ".");
                    try
                    {
                        parameterID = FindIDInDictionary(name);
                        t.Text = Convert.ToString(sample.Output[parameterID]) + ConvertName(t.Name);
                    }
                    catch(Exception ex)
                    {
                        //усадку не ищем в базе, а считаем
                    }
                }
            }
        }
        //Поиск названия в базе данных
        private int FindIDInDictionary(string name)
        {
            int parameterID = TrainData.nameParameter.Single(o => o.Value == name).Key;
            return parameterID;
        }

        private void ShowThrend(object sender)
        {
            FormsPlot chartControl = chartControl1;
            try
            {
                List<double> xs = new List<double>();
                List<double> ys = new List<double>();
                // Процедура поиска текущего контрола для отрисовки трендов
                if (extruderTabPage.Visible)
                {
                    chartControl = chartTrends;
                }
                if (calenderRollTabPage.Visible)
                {
                    chartControl = chartCalenderRoll;
                }
                if (takeoffRollTabPage.Visible)
                {
                    chartControl = chartTakeOffRoll;
                }
                if (temperingRollTabPage.Visible)
                {
                    chartControl = chartTemperingRoll;
                }
                if (winderTabPage.Visible)
                {
                    chartControl = chartWinder;
                }
                if (qualityTabPage.Visible) // Ситуация когда мы находимся на вкладке показателей качества является нетривиальной
                {
                    chartControl = chartControl1;
                    if (sender is TextBox)
                    {
                        //Поиск текстбокса и указание ограничений по времени
                        TextBox edit = sender as TextBox;
                        DateTime timeBefore = TrainData.Train.OrderBy(o => o.Date).First().Date;
                        DateTime timeAfter = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
                        chartControl.plt.Clear();
                        if (edit.Name.Contains("Shrinkage")) //если выбрана усадка
                        {
                            TimeSpan ts = new TimeSpan(ProductionTime.TakeOffTime);
                            foreach (OneRow row in TrainData.Train)
                            {
                                DataSet AuthorsDataSet = new DataSet();
                                AuthorsDataSet.ReadXml(XMLWork.PathShrinkage);
                                string shrinkageValues = XMLWork.FindShrinkageWithTimestamp(row.Date);
                                if ((row.Date >= timeBefore) && (row.Date <= timeAfter))
                                {
                                    xs.Add((row.Date - ts).ToOADate());
                                    ys.Add(Convert.ToDouble(shrinkageValues.Replace(".", ",")));
                                }
                            }
                            chartControl.plt.PlotScatter(xs.ToArray(), ys.ToArray(), lineWidth: 2, markerShape: MarkerShape.none, label: "Shrinkage value");
                        }
                        else
                        {
                            //Отрисовка
                            int parameterID = XMLWork.FindID(edit.Name.Replace("_", "."));
                            foreach (OneRow row in TrainData.Train)
                            {
                                if ((row.Date >= timeBefore) && (row.Date <= timeAfter))
                                {
                                    xs.Add(row.Date.ToOADate());
                                    ys.Add(Convert.ToDouble(row.Output[parameterID]));
                                }
                            }
                            chartControl.plt.PlotScatter(xs.ToArray(), ys.ToArray(), lineWidth: 2, markerShape: MarkerShape.none, label: XMLWork.FindNameWithID(parameterID, Properties.Settings.Default.Languages));
                        }
                    }
                    chartControl.plt.Ticks(dateTimeX: true);
                    chartControl.plt.Legend(location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
                    chartControl.plt.AxisAuto();
                    double xDelta = Math.Abs(xs.Max() - xs.Min());
                    double yDelta = Math.Abs(ys.Max() - ys.Min());
                    chartControl.plt.Axis(xs.Min() - xDelta * 0.05, xs.Max() + xDelta * 0.05, ys.Min() - yDelta * 0.05, ys.Max() + yDelta * 0.05); chartControl.Render();
                    return;
                }
                if (sender is TextBox)
                {
                    //Поиск текстбокса и указание ограничений по времени
                    TextBox edit = sender as TextBox;
                    DateTime timeBefore = TrainData.Train.OrderBy(o => o.Date).First().Date;
                    DateTime timeAfter = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
                    chartControl.plt.Clear();
                    //Отрисовка
                    int parameterID = XMLWork.FindID(edit.Name);
                    foreach (OneRow row in TrainData.Train)
                    {
                        if ((row.Date >= timeBefore) && (row.Date <= timeAfter) && (!qualityTabPage.Visible))
                        {
                            xs.Add(row.Date.ToOADate());
                            ys.Add(Convert.ToDouble(row.Input[parameterID]));
                        }
                    }
                    chartControl.plt.PlotScatter(xs.ToArray(), ys.ToArray(), lineWidth: 2, markerShape: MarkerShape.none, label: XMLWork.FindNameWithID(parameterID, Properties.Settings.Default.Languages));
                    chartControl.plt.Ticks(dateTimeX: true);
                    chartControl.plt.Legend(location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
                    chartControl.plt.AxisAuto();
                    double xDelta = Math.Abs(xs.Max() - xs.Min());
                    double yDelta = Math.Abs(ys.Max() - ys.Min());
                    chartControl.plt.Axis(xs.Min() - xDelta * 0.05, xs.Max() + xDelta * 0.05, ys.Min() - yDelta * 0.05, ys.Max() + yDelta * 0.05);
                    chartControl.Render();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(Localization.MyStrings.SomethingWrong + exception.ToString());
            }
        }

        private void thrend_DoubleClick(object sender, EventArgs e)
        {
            ShowThrend(sender);

           /* ChartControl chartControl = new ChartControl();
            try
            {
                // Процедура поиска текущего контрола для отрисовки трендов
                if (extruderTabPage.Visible)
                {
                    chartControl = chartTrends;
                }
                if (calenderRollTabPage.Visible)
                {
                    chartControl = chartCalenderRoll;
                }
                if (takeoffRollTabPage.Visible)
                {
                    chartControl = chartTakeOffRoll;
                }
                if (temperingRollTabPage.Visible)
                {
                    chartControl = chartTemperingRoll;
                }
                if (winderTabPage.Visible)
                {
                    chartControl = chartWinder;
                }
                if (qualityTabPage.Visible) // Ситуация когда мы находимся на вкладке показателей качества является нетривиальной
                {
                    SeriesPoint points;                    
                    chartControl = chartControl1;
                    if (sender is TextBox)
                    {
                        //Поиск текстбокса и указание ограничений по времени
                        TextBox edit = sender as TextBox;
                        DateTime timeBefore = TrainData.Train.OrderBy(o => o.Date).First().Date;
                        DateTime timeAfter = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
                        chartControl.Series.Clear();
                        if (edit.Name.Contains("Shrinkage")) //если выбрана усадка
                        {
                            var series = CreateSeries("Shrinkage value");
                            TimeSpan ts = new TimeSpan( ProductionTime.TakeOffTime);
                            foreach (OneRow row in TrainData.Train)
                            {
                                DataSet AuthorsDataSet = new DataSet();

                                AuthorsDataSet.ReadXml(XMLWork.PathShrinkage);
                                string shrinkageValues = XMLWork.FindShrinkageWithTimestamp(row.Date);
                                if ((row.Date >= timeBefore) && (row.Date <= timeAfter))
                                {

                                    points = new SeriesPoint(row.Date - ts, Convert.ToDecimal(shrinkageValues.Replace(".", ",")));
                                    series.Points.Add(points);
                                }
                            }
                            chartControl.Series.Add(series); // Построение тренда
                        }
                        else
                        {
                            //Отрисовка
                            int parameterID = XMLWork.FindID(edit.Name.Replace("_", "."));
                            var series = CreateSeries(XMLWork.FindNameWithID(parameterID, Properties.Settings.Default.Languages));
                            foreach (OneRow row in TrainData.Train)
                            {
                                if ((row.Date >= timeBefore) && (row.Date <= timeAfter))
                                {
                                    points = new SeriesPoint(row.Date, row.Output[parameterID]);
                                    series.Points.Add(points);
                                }
                            }
                            chartControl.Series.Add(series); // Построение тренда
                        }
                    }
                    return;
                }
                if (sender is TextBox)
                {
                    //Поиск текстбокса и указание ограничений по времени
                    TextBox edit = sender as TextBox;
                    DateTime timeBefore = TrainData.Train.OrderBy(o => o.Date).First().Date;
                    DateTime timeAfter = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
                    chartControl.Series.Clear();
                    //Отрисовка
                    int parameterID = XMLWork.FindID(edit.Name);
                    SeriesPoint points;
                    var series = CreateSeries(XMLWork.FindNameWithID(parameterID, Properties.Settings.Default.Languages));
                    foreach (OneRow row in TrainData.Train)
                    {
                        if ((row.Date >= timeBefore) && (row.Date <= timeAfter) && (!qualityTabPage.Visible))
                        {
                            points = new SeriesPoint(row.Date, row.Input[parameterID]);
                            series.Points.Add(points);
                        }
                    }
                    chartControl.Series.Add(series); // Построение тренда
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(Localization.MyStrings.SomethingWrong + exception.ToString());
            }
            
            */
        }

       
        //Построения графика. Основные характеристики хранятся здесь
       /* private Series CreateSeries(string parName)
        {
            var series = new Series(parName, ViewType.Line);
            ((LineSeriesView)series.View).LineMarkerOptions.Visible = false;

            // Set the scale type for the series' arguments and values.
            series.ArgumentScaleType = ScaleType.DateTime;
            series.ValueScaleType = ScaleType.Numerical;

            series.SeriesPointsSorting = SortingMode.Ascending;
            series.SeriesPointsSortingKey = SeriesPointKey.Argument;

            return series;
        }*/
        /// <summary>
        /// Конвертация имени по системе СИ
        /// </summary>
        /// <param name="parName"></param>
        /// <returns></returns>
        private string ConvertName(string parName)
        {
            if (parName.Contains("IA_shnek") || parName.Contains("I_voronka")) // Секорость в экструдере
                return  Localization.MyStrings.ExtruderForce;
            else if (parName.Contains("XI")) // Нагрузка
                return Localization.MyStrings.Force;
            else if (parName.Contains("XV")) // Скорость
                return Localization.MyStrings.Velocity;
            else if (parName.Contains("XT")) // Температура
                return Localization.MyStrings.Temperature;
            else if (parName.Contains("shnek") || parName.Contains("voronka")) // Скорость в экструдере
                return  Localization.MyStrings.ExtruderVelocity;
            else if (parName.Contains("RBW")) // Контр-изгиб
                return Localization.MyStrings.RollBending; 
            else if (parName.Contains("Defect")) // Дефекты
                return Localization.MyStrings.Defects;
            return "null";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// Расчет Cp and Cpk
        /// </summary>
        public void CalculateCP()
        {
            Dictionary<int, decimal> UCLDaTa = new Dictionary<int, decimal>();
            UCLDaTa.Add(2, Convert.ToDecimal(Defects_Roll10Sqm_DefMap0_UCL.Text));
            UCLDaTa.Add(3, Convert.ToDecimal(Defects_Roll10Sqm_DefMap1_UCL.Text));
            UCLDaTa.Add(4, Convert.ToDecimal(Defects_Roll10Sqm_DefMap2_UCL.Text));
            UCLDaTa.Add(5, 0.0m);
            UCLDaTa.Add(6, Convert.ToDecimal(Defects_Roll10Sqm_DefMap4_UCL.Text));
            UCLDaTa.Add(7, Convert.ToDecimal(Defects_Roll10Sqm_DefMap5_UCL.Text));
            UCLDaTa.Add(8, Convert.ToDecimal(Defects_Roll10Sqm_DefMap6_UCL.Text));
            UCLDaTa.Add(9, Convert.ToDecimal(Defects_Roll10Sqm_DefMap7_UCL.Text));
            UCLDaTa.Add(10, 0.0m);
            UCLDaTa.Add(11, 0.0m);
            UCLDaTa.Add(12, 0.0m);
            UCLDaTa.Add(13, 0.0m);
            UCLDaTa.Add(14, 0.0m);
            UCLDaTa.Add(15, 0.0m);
            UCLDaTa.Add(16, 0.0m);
            UCLDaTa.Add(17, 0.0m);
            UCLDaTa.Add(18, 0.0m);
            UCLDaTa.Add(19, 0.0m);
            UCLDaTa.Add(20, 0.0m);
            UCLDaTa.Add(21, 0.0m);

            Dictionary<int, decimal> LCLDaTa = new Dictionary<int, decimal>();
            LCLDaTa.Add(2, Convert.ToDecimal(Defects_Roll10Sqm_DefMap0_LCL.Text));
            LCLDaTa.Add(3, Convert.ToDecimal(Defects_Roll10Sqm_DefMap1_LCL.Text));
            LCLDaTa.Add(4, Convert.ToDecimal(Defects_Roll10Sqm_DefMap2_LCL.Text));
            LCLDaTa.Add(5, 0.0m);
            LCLDaTa.Add(6, Convert.ToDecimal(Defects_Roll10Sqm_DefMap4_LCL.Text));
            LCLDaTa.Add(7, Convert.ToDecimal(Defects_Roll10Sqm_DefMap5_LCL.Text));
            LCLDaTa.Add(8, Convert.ToDecimal(Defects_Roll10Sqm_DefMap6_LCL.Text));
            LCLDaTa.Add(9, Convert.ToDecimal(Defects_Roll10Sqm_DefMap7_LCL.Text));
            LCLDaTa.Add(10, 0.0m);
            LCLDaTa.Add(11, 0.0m);
            LCLDaTa.Add(12, 0.0m);
            LCLDaTa.Add(13, 0.0m);
            LCLDaTa.Add(14, 0.0m);
            LCLDaTa.Add(15, 0.0m);
            LCLDaTa.Add(16, 0.0m);
            LCLDaTa.Add(17, 0.0m);
            LCLDaTa.Add(18, 0.0m);
            LCLDaTa.Add(19, 0.0m);
            LCLDaTa.Add(20, 0.0m);
            LCLDaTa.Add(21, 0.0m);
            Dictionary<int,decimal> sumOfDefect = new Dictionary<int, decimal>();
            Dictionary<int, decimal> sumOfDispersion = new Dictionary<int, decimal>();
            foreach (OneRow row in TrainData.Train)
            {
                foreach (var qualityParameter in row.Output)
                {
                    if (!sumOfDefect.ContainsKey(qualityParameter.Key))
                        sumOfDefect.Add(qualityParameter.Key, qualityParameter.Value);
                    else
                        sumOfDefect[qualityParameter.Key] += qualityParameter.Value;
                }
            }

            foreach (var defect in sumOfDefect)
            {
                if (!TrainData.OutputAverage.ContainsKey(defect.Key))
                    TrainData.OutputAverage.Add(defect.Key, sumOfDefect[defect.Key] / TrainData.Train.Count);
                else
                    TrainData.OutputAverage[defect.Key] = sumOfDefect[defect.Key] / TrainData.Train.Count;
                
            }

            foreach (OneRow row in TrainData.Train)
            {
                foreach (var qualityParameter in row.Output)
                {
                    if (!sumOfDispersion.ContainsKey(qualityParameter.Key))
                        sumOfDispersion.Add(qualityParameter.Key, (qualityParameter.Value - TrainData.OutputAverage[qualityParameter.Key]) * (qualityParameter.Value - TrainData.OutputAverage[qualityParameter.Key]));
                    else
                        sumOfDispersion[qualityParameter.Key] += (qualityParameter.Value - TrainData.OutputAverage[qualityParameter.Key]) * (qualityParameter.Value - TrainData.OutputAverage[qualityParameter.Key]);
                }
            }


            foreach (var defect in sumOfDispersion)
            {
                if (!TrainData.OutputSigma.ContainsKey(defect.Key)){
                    
                    TrainData.OutputSigma.Add(defect.Key, Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(sumOfDispersion[defect.Key] / TrainData.Train.Count))));
                }
                    else
                    TrainData.OutputSigma[defect.Key] = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(sumOfDispersion[defect.Key] / TrainData.Train.Count)));


                

                if ((!TrainData.CPU.ContainsKey(defect.Key)) &&(sumOfDispersion[defect.Key]>0.0m))
                {
                    TrainData.CPU.Add(defect.Key,
                        Convert.ToDecimal((UCLDaTa[defect.Key] - TrainData.OutputAverage[defect.Key]) / (3 * TrainData.OutputSigma[defect.Key])));
                }

                else
                {
                    if (sumOfDispersion[defect.Key] > 0.0m)
                        TrainData.CPU[defect.Key] = (Convert.ToDecimal(UCLDaTa[defect.Key] - TrainData.OutputAverage[defect.Key]) / (3 * TrainData.OutputSigma[defect.Key]));
             
                }

                if ((!TrainData.CPL.ContainsKey(defect.Key)) &&(sumOfDispersion[defect.Key]>0.0m))
                {
                    TrainData.CPL.Add(defect.Key,
                        Convert.ToDecimal((TrainData.OutputAverage[defect.Key] - LCLDaTa[defect.Key]) / (3 * TrainData.OutputSigma[defect.Key])));
                }

                else
                {
                    if (sumOfDispersion[defect.Key] > 0.0m)
                        TrainData.CPL[defect.Key] = (Convert.ToDecimal(TrainData.OutputAverage[defect.Key] - LCLDaTa[defect.Key]) / (3 * TrainData.OutputSigma[defect.Key]));
                }


            }


            foreach (var textBoxControl in qualityPanel.Controls)
            {
                var t = textBoxControl as TextBox;
                if ((t != null) && (t.Name.EndsWith("CP")))
                {
                    string name = t.Name.Replace("_", ".");
                    try
                    {
                        string nameTruncate = name.Remove(name.Length - 3, 3);
                        int parameterID = FindIDInDictionary(nameTruncate);
                        
                                
                                if (TrainData.OutputSigma[parameterID] > 0.0m)
                                {
                                    decimal value = ((UCLDaTa[parameterID] - -LCLDaTa[parameterID]) / (6 * TrainData.OutputSigma[parameterID]));
                                    t.Text = Convert.ToString(Math.Round(value,3));
                                }


                    }
                    catch (Exception ex)
                    {
                        //усадку не ищем в базе, а считаем
                        MessageBox.Show("Усадку не считаем в базе");
                    }
                }
            }
            foreach (var textBoxControl in qualityPanel.Controls)
            {
                var t = textBoxControl as TextBox;
                if ((t != null) && (t.Name.EndsWith("CPL")))
                {
                    string name = t.Name.Replace("_", ".");
                    try
                    {
                        string nameTruncate = name.Remove(name.Length - 4, 4);
                        int parameterID = FindIDInDictionary(nameTruncate);

                        
                        if (TrainData.OutputSigma[parameterID] > 0.0m)
                        {
                            t.Text = Convert.ToString(Math.Round(Math.Min(TrainData.CPL[parameterID], TrainData.CPU[parameterID]),3));
                        }


                    }
                    catch (Exception ex)
                    {
                        //усадку не ищем в базе, а считаем
                    }
                }
            }
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            Dictionary<int, decimal> UCLDaTa = new Dictionary<int, decimal>();
            UCLDaTa.Add(2, Convert.ToDecimal(Defects_Roll10Sqm_DefMap0_UCL.Text));
            UCLDaTa.Add(3, Convert.ToDecimal(Defects_Roll10Sqm_DefMap1_UCL.Text));
            UCLDaTa.Add(4, Convert.ToDecimal(Defects_Roll10Sqm_DefMap2_UCL.Text));
            UCLDaTa.Add(5, 0.0m);
            UCLDaTa.Add(6, Convert.ToDecimal(Defects_Roll10Sqm_DefMap4_UCL.Text));
            UCLDaTa.Add(7, Convert.ToDecimal(Defects_Roll10Sqm_DefMap5_UCL.Text));
            UCLDaTa.Add(8, Convert.ToDecimal(Defects_Roll10Sqm_DefMap6_UCL.Text));
            UCLDaTa.Add(9, Convert.ToDecimal(Defects_Roll10Sqm_DefMap7_UCL.Text));
            UCLDaTa.Add(10, 0.0m);
            UCLDaTa.Add(11, 0.0m);
            UCLDaTa.Add(12, 0.0m);
            UCLDaTa.Add(13, 0.0m);
            UCLDaTa.Add(14, 0.0m);
            UCLDaTa.Add(15, 0.0m);
            UCLDaTa.Add(16, 0.0m);
            UCLDaTa.Add(17, 0.0m);
            UCLDaTa.Add(18, 0.0m);
            UCLDaTa.Add(19, 0.0m);
            UCLDaTa.Add(20, 0.0m);
            UCLDaTa.Add(21, 0.0m);

            Dictionary<int, decimal> LCLDaTa = new Dictionary<int, decimal>();
            LCLDaTa.Add(2, Convert.ToDecimal(Defects_Roll10Sqm_DefMap0_LCL.Text));
            LCLDaTa.Add(3, Convert.ToDecimal(Defects_Roll10Sqm_DefMap1_LCL.Text));
            LCLDaTa.Add(4, Convert.ToDecimal(Defects_Roll10Sqm_DefMap2_LCL.Text));
            LCLDaTa.Add(5, 0.0m);
            LCLDaTa.Add(6, Convert.ToDecimal(Defects_Roll10Sqm_DefMap4_LCL.Text));
            LCLDaTa.Add(7, Convert.ToDecimal(Defects_Roll10Sqm_DefMap5_LCL.Text));
            LCLDaTa.Add(8, Convert.ToDecimal(Defects_Roll10Sqm_DefMap6_LCL.Text));
            LCLDaTa.Add(9, Convert.ToDecimal(Defects_Roll10Sqm_DefMap7_LCL.Text));
            LCLDaTa.Add(10, 0.0m);
            LCLDaTa.Add(11, 0.0m);
            LCLDaTa.Add(12, 0.0m);
            LCLDaTa.Add(13, 0.0m);
            LCLDaTa.Add(14, 0.0m);
            LCLDaTa.Add(15, 0.0m);
            LCLDaTa.Add(16, 0.0m);
            LCLDaTa.Add(17, 0.0m);
            LCLDaTa.Add(18, 0.0m);
            LCLDaTa.Add(19, 0.0m);
            LCLDaTa.Add(20, 0.0m);
            LCLDaTa.Add(21, 0.0m);
            CalculateCP();
        }

        private void textBoxShrinkage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
