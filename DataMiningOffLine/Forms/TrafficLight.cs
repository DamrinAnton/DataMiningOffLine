using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using DM.Data;
using ScottPlot;

namespace DataMiningOffLine
{
    public partial class TrafficLight : Form
    {
        public TrafficLight()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Languages))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
            }
            InitializeComponent();

        }

        private void TrafficLight_Shown(object sender, EventArgs e)
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
            if (ListAdd.Items.Count != 0)
                ListAdd.SelectedIndex = 0;
            if (ListDelete.Items.Count != 0)
                ListDelete.SelectedIndex = 0;
            if (ListAddError.Items.Count != 0)
                ListAddError.SelectedIndex = 0;
            if (ListDeleteError.Items.Count != 0)
                ListDeleteError.SelectedIndex = 0;
        }

        #region Move Items
        /// <summary>
        /// Adds a parameter to the list of analytes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveItems(object sender, EventArgs e)
        {
            MoveAllItems(ListAdd, ListDelete);
        }

        /// <summary>
        /// Adds a parameter to the list of analytes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveItem(object sender, EventArgs e)
        {
            if (ListAdd.Items.Count != 0)
                MoveListBoxItems(ListAdd, ListDelete);
        }

        /// <summary>
        /// Move selected items
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        private void MoveListBoxItems(ListBox source, ListBox destination)
        {
            try
            {
                destination.Items.Add(source.SelectedItem);
                source.Items.RemoveAt(source.SelectedIndex);
                if (source.Items.Count != 0)
                    source.SelectedIndex = 0;
                if (destination.Items.Count != 0)
                    destination.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Move All Elements
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        private void MoveAllItems(ListBox source, ListBox destination)
        {
            for (int i = 0; i < source.Items.Count; i++)
            {
                destination.Items.Add(source.Items[i].ToString());
            }
            source.Items.Clear();
            if (source.Items.Count != 0)
                source.SelectedIndex = 0;
            if (destination.Items.Count != 0)
                destination.SelectedIndex = 0;
        }

        /// <summary>
        /// Adds a parameter to the list of analytes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveErrorItems(object sender, EventArgs e)
        {
            MoveAllItems(ListAddError, ListDeleteError);
        }

        /// <summary>
        /// Adds a parameter to the list of analytes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveErrorItem(object sender, EventArgs e)
        {
            if (ListAddError.Items.Count != 0)
                MoveListBoxItems(ListAddError, ListDeleteError);
        }


        #region Delete Items

        /// <summary>
        /// Delete a parameter from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteOneItem(object sender, EventArgs e)
        {
            if (ListDelete.Items.Count != 0)
                MoveListBoxItems(ListDelete, ListAdd);
        }

        /// <summary>
        /// Delete All Elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAllItems(object sender, EventArgs e)
        {
            MoveAllItems(ListDelete, ListAdd);
        }

        /// <summary>
        /// Delete a parameter from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteErrorOneItem(object sender, EventArgs e)
        {
            if (ListDeleteError.Items.Count != 0)
                MoveListBoxItems(ListDeleteError, ListAddError);
        }

        /// <summary>
        /// Delete All Elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteErrorAllItems(object sender, EventArgs e)
        {
            MoveAllItems(ListDeleteError, ListAddError);
        }

        #endregion


        #endregion

        #region DrawItem
        /// <summary>
        /// Selects parameters in orange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            ListBoxControl control = (ListBoxControl)sender;
            if (e.Index == (control.SelectedIndex))
            {
                e.Appearance.BackColor = Color.Orange;
            }
        }*/

        #endregion


        private void parameterCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListAdd.Items.Clear();
            ListDelete.Items.Clear();
            ListAddError.Items.Clear();
            ListDeleteError.Items.Clear();
            List<int> keys = new List<int>();
            int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
            Dictionary<int, decimal> firstRow = TrainData.Train.First(o => o.Output[parameterID] > (Convert.ToDecimal(uppLimit.Value) * 0.9M)).Input;
            foreach (OneRow data in TrainData.Train)
            {

                if (data.Output[parameterID] > 1.0M)
                {
                    foreach (var item in data.Input)
                    {
                        if (keys.Contains(item.Key))
                            continue;
                        if (item.Key != firstRow[item.Key])
                            keys.Add(item.Key);
                    }

                }
            }
            foreach (int key in keys)
            {
                ListAdd.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
                ListAddError.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
            }
            addAllToFirstCoordinate.Enabled = true;
            addAllToSecondCoordinate.Enabled = true;
            addOneCoordinate.Enabled = true;
            addOneToSecondCoordinate.Enabled = true;
            deleteAllToFirstCoordinate.Enabled = true;
            deleteAllToSecondCoordinate.Enabled = true;
            deleteOneToSecondCoordinate.Enabled = true;
            deleteToFirstCoordinate.Enabled = true;
            if (parameterCondition.SelectedIndex != -1)
                parameterCondition.BackColor = Color.White;

            if (ListAdd.Items.Count != 0)
                ListAdd.SelectedIndex = 0;
            if (ListDelete.Items.Count != 0)
                ListDelete.SelectedIndex = 0;
            if (ListAddError.Items.Count != 0)
                ListAddError.SelectedIndex = 0;
            if (ListDeleteError.Items.Count != 0)
                ListDeleteError.SelectedIndex = 0;
        }

        void ShowThrends2()
        {
            if ((ListDelete.Items.Count > 10) || (ListDeleteError.Items.Count > 10))
            {
                MessageBox.Show(
                    Localization.MyStrings.ParametersSelected);
                return;
            }
            if (parameterCondition.SelectedIndex == -1)
            {
                MessageBox.Show(Localization.MyStrings.SelectCriterion);
                parameterCondition.BackColor = Color.Red;
                return;
            }
            int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
            Chart.plt.Clear();
            List<OneRow> trainData = TrainData.Train;
            List<double> xs1 = new List<double>(), ys1 = new List<double>(), xs2 = new List<double>(), ys2 = new List<double>();
            decimal minX = 0.0M;
            decimal maxX = 0.0M;
            decimal minY = 0.0M;
            decimal maxY = 0.0M;
            foreach (OneRow data in trainData)
            {
                decimal pointX = 1.0M;
                decimal pointY = 1.0M;
                if (data.Output[parameterID] > (Convert.ToDecimal(uppLimit.Value) * 0.9M))
                {
                    GetPoints(ref pointX, data, ref pointY);
                    maxX = pointX;
                    maxY = pointY;
                    minX = pointX;
                    minY = pointY;
                }
                else
                {
                    GetPoints(ref pointX, data, ref pointY);
                    maxX = pointX;
                    maxY = pointY;
                    minX = pointX;
                    minY = pointY;
                }
                break;
            }
            foreach (OneRow data in trainData)
            {
                decimal pointX = 1.0M;
                decimal pointY = 1.0M;
                if (data.Output[parameterID] > (Convert.ToDecimal(uppLimit.Value) * 0.9M))
                {
                    GetPoints(ref pointX, data, ref pointY);
                    if (pointX > maxX)
                        maxX = pointX;
                    if (pointY > maxY)
                        maxY = pointY;
                    if (pointX < minX)
                        minX = pointX;
                    if (pointY < minY)
                        minY = pointY;
                    xs1.Add(Convert.ToDouble(pointX));
                    ys1.Add(Convert.ToDouble(pointY));
                }
                else
                {
                    GetPoints(ref pointX, data, ref pointY);
                    if (pointX > maxX)
                        maxX = pointX;
                    if (pointY > maxY)
                        maxY = pointY;
                    if (pointX < minX)
                        minX = pointX;
                    if (pointY < minY)
                        minY = pointY;
                    xs2.Add(Convert.ToDouble(pointX));
                    ys2.Add(Convert.ToDouble(pointY));
                }
            }
            Chart.plt.PlotScatter(xs1.ToArray(), ys1.ToArray(), lineWidth: 0, markerShape: MarkerShape.filledCircle, label: Localization.MyStrings.WithD);
            Chart.plt.PlotScatter(xs2.ToArray(), ys2.ToArray(), lineWidth: 0, markerShape: MarkerShape.filledCircle, label: Localization.MyStrings.WithoutD);
            Chart.plt.Legend(location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            Chart.plt.AxisAuto();
            decimal deltaX = Math.Abs(maxX - minX);
            decimal deltaY = Math.Abs(maxY - minY);
            Chart.plt.Axis(Convert.ToDouble(minX - deltaX * 0.05m), Convert.ToDouble(maxX + deltaX * 0.05m),
                Convert.ToDouble(minY - deltaY * 0.05m), Convert.ToDouble(maxY + deltaY * 0.05m));
            Chart.Render();
        }
        
        private void ShowThrends_Click(object sender, EventArgs e)
        {
            ShowThrends2();
           /* if ((ListDelete.Items.Count > 10) || (ListDeleteError.Items.Count > 10))
            {
                MessageBox.Show(
                    Localization.MyStrings.ParametersSelected);
            }
            else
            {

                if (parameterCondition.SelectedIndex == -1)
                {
                    MessageBox.Show(Localization.MyStrings.SelectCriterion);
                    parameterCondition.BackColor = Color.Red;
                }
                else if ((ListDeleteError.Items.Count == 0) || (ListDeleteError.Items.Count == 0))
                {
                    MessageBox.Show(Localization.MyStrings.AddParameter);
                }
                else
                {
                    int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
                    Chart.Series.Clear();
                    List<OneRow> trainData = TrainData.Train;
                    List<SeriesPoint> points = new List<SeriesPoint>();
                    List<SeriesPoint> points2 = new List<SeriesPoint>();
                    decimal minX = 0.0M;
                    decimal maxX = 0.0M;
                    decimal minY = 0.0M;
                    decimal maxY = 0.0M;
                    foreach (OneRow data in trainData)
                    {
                        decimal pointX = 1.0M;
                        decimal pointY = 1.0M;
                        if (data.Output[parameterID] > (Convert.ToDecimal(uppLimit.Value) * 0.9M))
                        {
                            GetPoints(ref pointX, data, ref pointY);
                            maxX = pointX;
                            maxY = pointY;
                            minX = pointX;
                            minY = pointY;
                        }
                        else
                        {
                            GetPoints(ref pointX, data, ref pointY);
                            maxX = pointX;
                            maxY = pointY;
                            minX = pointX;
                            minY = pointY;
                        }
                        break;
                    }
                    foreach (OneRow data in trainData)
                    {
                        decimal pointX = 1.0M;
                        decimal pointY = 1.0M;
                        if (data.Output[parameterID] > (Convert.ToDecimal(uppLimit.Value) * 0.9M))
                        {
                            GetPoints(ref pointX, data, ref pointY);
                            if (pointX > maxX)
                                maxX = pointX;
                            if (pointY > maxY)
                                maxY = pointY;
                            if (pointX < minX)
                                minX = pointX;
                            if (pointY < minY)
                                minY = pointY;
                            points.Add(new SeriesPoint(pointX, pointY));
                        }
                        else
                        {
                            GetPoints(ref pointX, data, ref pointY);
                            if (pointX > maxX)
                                maxX = pointX;
                            if (pointY > maxY)
                                maxY = pointY;
                            if (pointX < minX)
                                minX = pointX;
                            if (pointY < minY)
                                minY = pointY;
                            points2.Add(new SeriesPoint(pointX, pointY));
                        }
                    }
                    var series = CreateSeries(Localization.MyStrings.WithD);
                    series.Points.AddRange(points.ToArray());
                    Chart.Series.Add(series);
                    var view = (PointSeriesView)Chart.Series[0].View;
                    view.Color = Color.Red;


                    var series2 = CreateSeries(Localization.MyStrings.WithoutD);
                    series2.Points.AddRange(points2.ToArray());
                    Chart.Series.Add(series2);
                    var view2 = (PointSeriesView)Chart.Series[1].View;
                    view.Color = Color.Blue;


                    XYDiagram diagram = (XYDiagram)Chart.Diagram;
                    diagram.AxisX.Range.SetMinMaxValues(minX - 0.1M * minX, maxX + 0.1M * maxX);
                    diagram.AxisY.Range.SetMinMaxValues(minY - 0.1M * minY, maxY + 0.1M * maxY);
                }
            }*/
        }
        

        private void GetPoints(ref decimal pointX, OneRow data, ref decimal pointY)
        {
            foreach (var item in ListDelete.Items)
            {
                int parameterID = XMLWork.FindIDWithName(item.ToString(), Properties.Settings.Default.Languages);
                pointX = pointX * data.Input[parameterID]; //234 232
            }
            foreach (var item in ListDeleteError.Items)
            {
                int parameterID = XMLWork.FindIDWithName(item.ToString(), Properties.Settings.Default.Languages);
                pointY = pointY * data.Input[parameterID]; //234 232
            }
        }

       /* private DevExpress.XtraCharts.Series CreateSeries(string parName)
        {
            var series = new DevExpress.XtraCharts.Series(parName, ViewType.Point);

            // Set the scale type for the series' arguments and values.
            series.ArgumentScaleType = ScaleType.Numerical;
            series.ValueScaleType = ScaleType.Numerical;

            series.SeriesPointsSorting = SortingMode.Ascending;
            series.SeriesPointsSortingKey = SeriesPointKey.Argument;

            return series;
        }*/
    }
}
