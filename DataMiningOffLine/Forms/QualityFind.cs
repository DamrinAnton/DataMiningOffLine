using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DM.Data;
using ScottPlot;

namespace DataMiningOffLine.Forms
{
    public partial class QualityFind : Form
    {
        public QualityFind()
        {
            InitializeComponent();
        }
        private void QualityFind_Load(object sender, EventArgs e)
        {
            CalculateAndVisualiseShrinkage2();
            CalculateAndVisualiseBlackPoint2();
        }

        private void CalculateAndVisualiseShrinkage2()
        {
            DateTime takeTimeBefore = TrainData.Train.OrderBy(o => o.Date).First().Date;
            DateTime takeTimeAfter = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
            double startOADate = takeTimeBefore.ToOADate();
            double endOADate = takeTimeAfter.ToOADate();
            List<double> xs = new List<double>();
            List<double> ys = new List<double>();
            shrinkageControl1.plt.Clear();
            TimeSpan ts = new TimeSpan(ProductionTime.TakeOffTime);
            foreach (OneRow row in TrainData.Train)
            {
                DataSet AuthorsDataSet = new DataSet();
                try
                {
                    AuthorsDataSet.ReadXml(XMLWork.PathShrinkage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    string shrinkageValues = XMLWork.FindShrinkageWithTimestamp(row.Date);
                    if (Properties.Settings.Default.Languages.Equals("en-US"))
                    {
                        xs.Add((row.Date - ts).ToOADate());
                        ys.Add(Convert.ToDouble(shrinkageValues));
                    }
                    else
                    {
                        xs.Add((row.Date - ts).ToOADate());
                        ys.Add(Convert.ToDouble(shrinkageValues.Replace(".", ",")));
                    }
                }
            }
            shrinkageControl1.plt.PlotScatter(xs.ToArray(), ys.ToArray(), markerShape: MarkerShape.none, lineWidth: 2, label: "Shrinkage value");
            #region Constant line
            shrinkageControl1.plt.PlotHLine(Convert.ToDouble(nominalValueShrinkage.Value) * (1 + 0.01 * Convert.ToDouble(upPercentShrinkage.Value)), lineWidth: 2, label: "Upper Limit");
            shrinkageControl1.plt.PlotHLine(Convert.ToDouble(nominalValueShrinkage.Value) * (1 - 0.01 * Convert.ToDouble(lowPercentShrinkage.Value)), lineWidth: 2, label: "Low Limit");
            #endregion

            Dictionary<int, string> neededSeries = new Dictionary<int, string>()
            {
                {67, "Take-off rolls 1-2 - Speed" },
                {70,  "Take-off rolls 3-6 - Speed"},
                {73,  "Take-off rolls 7-10 - Speed"},
                {66,  "Take-off rolls 1-2 - Temperature"},
                {69,  "Take-off rolls 3-6 - Temperature"},
                {72,  "Take-off rolls 7-10 - Temperature"}
            };
            foreach (var series in neededSeries)
            {
                xs.Clear();
                ys.Clear();
                foreach (OneRow row in TrainData.Train)
                {
                    if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                    {
                        xs.Add(row.Date.ToOADate());
                        ys.Add(Convert.ToDouble(row.Input[series.Key]));
                    }
                }
                shrinkageControl1.plt.PlotScatter(xs.ToArray(), ys.ToArray(), markerShape: MarkerShape.none, lineWidth: 2, label: series.Value);
            }
            shrinkageControl1.plt.Ticks(dateTimeX: true);
            shrinkageControl1.plt.Legend(location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            shrinkageControl1.plt.AxisAuto();
            shrinkageControl1.plt.Axis(startOADate, endOADate);
            shrinkageControl1.Render();
        }

        /*private void CalculateAndVisualiseShrinkage()
        {
            DateTime takeTimeBefore = TrainData.Train.OrderBy(o => o.Date).First().Date;
            DateTime takeTimeAfter = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
            shrinkageControl1.Series.Clear();
            TimeSpan ts = new TimeSpan(ProductionTime.TakeOffTime);
            var series = CreateSeries("Shrinkage value");
            foreach (OneRow row in TrainData.Train)
            {
                DataSet AuthorsDataSet = new DataSet();
                try
                {
                    AuthorsDataSet.ReadXml(XMLWork.PathShrinkage);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    string shrinkageValues = XMLWork.FindShrinkageWithTimestamp(row.Date);
                    if (Properties.Settings.Default.Languages.Equals("en-US"))
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
            shrinkageControl1.Series.Add(series); // Построение тренда

            #region Constant Line

            var ser = CreateSeries("Upper Limit");
            ser.Points.Add(new SeriesPoint(takeTimeBefore,
                new double[] { Convert.ToDouble(nominalValueShrinkage.Value) * (1 + 0.01 * Convert.ToDouble(upPercentShrinkage.Value)) }));
            ser.Points.Add(new SeriesPoint(takeTimeAfter,
                new double[] { Convert.ToDouble(nominalValueShrinkage.Value) * (1 + 0.01 * Convert.ToDouble(upPercentShrinkage.Value)) }));
            shrinkageControl1.Series.Add(ser);


            ser = CreateSeries("Low Limit");
            ser.Points.Add(new SeriesPoint(takeTimeBefore,
                new double[]
                {Convert.ToDouble(nominalValueShrinkage.Value) * (1 - 0.01*Convert.ToDouble(lowPercentShrinkage.Value))}));
            ser.Points.Add(new SeriesPoint(takeTimeAfter,
                new double[] { Convert.ToDouble(nominalValueShrinkage.Value) * (1 - 0.01 * Convert.ToDouble(lowPercentShrinkage.Value)) }));
            shrinkageControl1.Series.Add(ser);
            var view = (LineSeriesView)shrinkageControl1.Series[shrinkageControl1.Series.Count - 1].View;
            view.Color = Color.Red;
            view = (LineSeriesView)shrinkageControl1.Series[shrinkageControl1.Series.Count - 2].View;

            view.Color = Color.Red;

            #endregion
            series = CreateSeries("Take-off rolls 1-2 - Speed");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[67]);
                    series.Points.Add(points);
                }
            }
            shrinkageControl1.Series.Add(series);

            series = CreateSeries("Take-off rolls 3-6 - Speed");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[70]);
                    series.Points.Add(points);
                }
            }
            shrinkageControl1.Series.Add(series);

            series = CreateSeries("Take-off rolls 7-10 - Speed");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[73]);
                    series.Points.Add(points);
                }
            }
            shrinkageControl1.Series.Add(series);

            series = CreateSeries("Take-off rolls 1-2 - Temperature");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[66]);
                    series.Points.Add(points);
                }
            }
            shrinkageControl1.Series.Add(series);

            series = CreateSeries("Take-off rolls 3-6 - Temperature");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[69]);
                    series.Points.Add(points);
                }
            }
            shrinkageControl1.Series.Add(series);

            series = CreateSeries("Take-off rolls 7-10 - Temperature");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[72]);
                    series.Points.Add(points);
                }
            }
            shrinkageControl1.Series.Add(series);


        }
        */

        private void CalculateAndVisualiseBlackPoint2()
        {
            DateTime takeTimeBefore = TrainData.Train.OrderBy(o => o.Date).First().Date;
            DateTime takeTimeAfter = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
            double startOADate = takeTimeBefore.ToOADate();
            double endOADate = takeTimeAfter.ToOADate();
            List<double> xs = new List<double>();
            List<double> ys = new List<double>();
            blackPointControl1.plt.Clear();
            TimeSpan ts = new TimeSpan(ProductionTime.TakeOffTime);
            Dictionary<Func<OneRow, decimal>, string> neededSeries = new Dictionary<Func<OneRow, decimal>, string>()
            {
                {row => row.Output[3], "Black Point" },
                {row => row.Input[45], "Funnel - Force"},
                {row => row.Input[47], "Screw - Force"},
                {row => row.Input[46], "Funnel - Speed"},
                {row => row.Input[48], "Screw - Speed"}
            };
            foreach (var series in neededSeries)
            {
                xs.Clear();
                ys.Clear();
                foreach (OneRow row in TrainData.Train)
                {
                    if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                    {
                        xs.Add(row.Date.ToOADate());
                        ys.Add(Convert.ToDouble(series.Key(row)));
                    }
                }
                blackPointControl1.plt.PlotScatter(xs.ToArray(), ys.ToArray(), markerShape: MarkerShape.none, lineWidth: 2, label: series.Value);
            }

            #region Constant line
            blackPointControl1.plt.PlotHLine(Convert.ToDouble(nominalValueBlackPoint.Value) + Convert.ToDouble(nominalValueBlackPoint.Value) * 0.01 * Convert.ToDouble(upPercentBlackPoint.Value), lineWidth: 2, label: "Upper Limit");
            blackPointControl1.plt.PlotHLine(Convert.ToDouble(nominalValueBlackPoint.Value) - Convert.ToDouble(nominalValueBlackPoint.Value) * 0.01 * Convert.ToDouble(lowPercentBlackPoint.Value), lineWidth: 2, label: "Low Limit");
            #endregion

            blackPointControl1.plt.Ticks(dateTimeX: true);
            blackPointControl1.plt.Legend(location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            blackPointControl1.plt.AxisAuto();
            blackPointControl1.plt.Axis(startOADate, endOADate);
            blackPointControl1.Render();
        }

        /*private void CalculateAndVisualiseBlackPoint()
        {
            DateTime takeTimeBefore = TrainData.Train.OrderBy(o => o.Date).First().Date;
            DateTime takeTimeAfter = TrainData.Train.OrderByDescending(o => o.Date).First().Date;
            blackPointControl1.Series.Clear();
            var series = CreateSeries("Black Point");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Output[3]);
                    series.Points.Add(points);
                }
            }
            blackPointControl1.Series.Add(series);

            series = CreateSeries("Funnel - Force");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[45]);
                    series.Points.Add(points);
                }
            }
            blackPointControl1.Series.Add(series);
            series = CreateSeries("Screw - Force");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[47]);
                    series.Points.Add(points);
                }
            }
            blackPointControl1.Series.Add(series);
            series = CreateSeries("Funnel - Speed");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[46]);
                    series.Points.Add(points);
                }
            }
            blackPointControl1.Series.Add(series);
            series = CreateSeries("Screw - Speed");
            foreach (OneRow row in TrainData.Train)
            {
                if ((row.Date >= takeTimeBefore) && (row.Date <= takeTimeAfter))
                {
                    var points = new SeriesPoint(row.Date, row.Input[48]);
                    series.Points.Add(points);
                }
            }
            blackPointControl1.Series.Add(series);





            #region Constant Line

            var ser = CreateSeries("Upper Limit");
            ser.Points.Add(new SeriesPoint(takeTimeBefore,
                new double[] { Convert.ToDouble(nominalValueBlackPoint.Value) + Convert.ToDouble(nominalValueBlackPoint.Value) * 0.01 * Convert.ToDouble(upPercentBlackPoint.Value) }));
            ser.Points.Add(new SeriesPoint(takeTimeAfter,
                new double[] { Convert.ToDouble(nominalValueBlackPoint.Value) + Convert.ToDouble(nominalValueBlackPoint.Value) * 0.01 * Convert.ToDouble(upPercentBlackPoint.Value) }));
            blackPointControl1.Series.Add(ser);


            ser = CreateSeries("Low Limit");
            ser.Points.Add(new SeriesPoint(takeTimeBefore,
                new double[] { Convert.ToDouble(nominalValueBlackPoint.Value) - Convert.ToDouble(nominalValueBlackPoint.Value) * 0.01 * Convert.ToDouble(lowPercentBlackPoint.Value) }));
            ser.Points.Add(new SeriesPoint(takeTimeAfter,
                new double[] { Convert.ToDouble(nominalValueBlackPoint.Value) - Convert.ToDouble(nominalValueBlackPoint.Value) * 0.01 * Convert.ToDouble(lowPercentBlackPoint.Value) }));
            blackPointControl1.Series.Add(ser);
            var view = (LineSeriesView)blackPointControl1.Series[blackPointControl1.Series.Count - 1].View;
            view.Color = Color.Red;
            view = (LineSeriesView)blackPointControl1.Series[blackPointControl1.Series.Count - 2].View;

            view.Color = Color.Red;


            #endregion

        }

        private Series CreateSeries(string parName)
        {
            var series = new Series(parName, ViewType.Line);
            ((LineSeriesView)series.View).LineMarkerOptions.Visible = false;

            // Set the scale type for the series' arguments and values.
            series.ArgumentScaleType = ScaleType.DateTime;
            series.ValueScaleType = ScaleType.Numerical;

            series.SeriesPointsSorting = SortingMode.Ascending;
            series.SeriesPointsSortingKey = SeriesPointKey.Argument;

            return series;
        }
        */

        private void CalculateShrinkage_Click(object sender, EventArgs e)
        {
            CalculateAndVisualiseShrinkage2();
        }

        private void CalculateBlackPoint_Click(object sender, EventArgs e)
        {
            CalculateAndVisualiseBlackPoint2();
        }
    }
}
