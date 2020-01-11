using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataMiningOffLine.Forms
{
    public partial class KPI : Form
    {
        public KPI()
        {
            InitializeComponent();

            CreateChartOrders();
            CreateChartRezeptur();
            CreateBestMaterial();
        }

        public void CreateChartOrders()
        {

            Series series = this.chart1.Series.Add("Tegrant Alloyd Brands, Inc.");
            series.Points.Add(6114.0);
            Series series2 = this.chart1.Series.Add("Kaschierbetrieb Lange GmbH & Co.KG");
            series2.Points.Add(5487.0);
            Series series3 = this.chart1.Series.Add("Rheno Plastiques & Papiers S. A. S.");
            series3.Points.Add(2541.0);
            Series series4 = this.chart1.Series.Add("SPX Cooling Technologies Inc");
            series4.Points.Add(2473.0);
            Series series5 = this.chart1.Series.Add("Brentwood Industries, Inc.");
            series5.Points.Add(2222.0);
            Series series6 = this.chart1.Series.Add("PLANUNG");
            series6.Points.Add(2174.0);
            Series series7 = this.chart1.Series.Add("Lager BG");
            series7.Points.Add(2060.0);
        }

        public void CreateChartRezeptur()
        {

            Series series = this.chart2.Series.Add("M252/01-85/4311-A-01-170_190#");
            series.Points.Add(88.4);
            Series series2 = this.chart2.Series.Add("M588J02-71/9400-A-01-50_500_JPspez");
            series2.Points.Add(87.8);
            Series series3 = this.chart2.Series.Add("M401/20-94/3100-A-01-0125_1150");
            series3.Points.Add(86.9);
            Series series4 = this.chart2.Series.Add("M280/06-03/9301-A-03-230_270#");
            series4.Points.Add(73.7);
            Series series5 = this.chart2.Series.Add("M153/07-05/192Z-A-03-100_150#K18,TxMB");
            series5.Points.Add(71.0);
            Series series6 = this.chart2.Series.Add("M401C50-74/5750-A-01-0200_1020 C");
            series6.Points.Add(70.8);
            Series series7 = this.chart2.Series.Add("M225/05-85/3714-A-02-110_130#K40");
            series7.Points.Add(60.0);
        }

        public void CreateBestMaterial()
        {

            Series series = this.chart3.Series.Add("V170/02-R-47-71/9400-200_500");
            series.Points.Add(97.3);
            Series series2 = this.chart3.Series.Add("GR-K-1-CL-B-F-PVC----CLEAR");
            series2.Points.Add(80.0);
            Series series3 = this.chart3.Series.Add("GR-K-1-WH-C-T-PVC-CACO3-CC");
            series3.Points.Add(71.0);
        }
    }
}
