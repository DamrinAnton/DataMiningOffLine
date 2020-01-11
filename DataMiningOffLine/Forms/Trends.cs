using DataMiningOffLine.DBClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace DataMiningOffLine.Forms
{
    public partial class Trends : Form
    {
        public Trends(string param, string number, string reason) {
            InitializeComponent();
            DrawGraph(param, number, reason);
        }

        private void DrawGraph(string param, string number, string reason)
        {
            Queries.GetMeasurmentsByParam(param);
            zedGraph.IsShowPointValues = true;
            zedGraph.PointValueFormat = "0.00";
            zedGraph.PointDateFormat = "T";
            // Получим панель для рисования
            GraphPane pane = zedGraph.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            pane.Title.Text = number + ": " + reason.ToUpper();
            pane.XAxis.Type = AxisType.Date;
            pane.YAxis.Title.Text = Queries.GetParamName(param);
            pane.XAxis.Title.Text = "TIME";
            pane.XAxis.Scale.Format = "HH:mm:ss";
            pane.XAxis.Scale.MajorUnit = DateUnit.Minute;
            pane.XAxis.Scale.MinorUnit = DateUnit.Minute;

            // Создадим список точек
            PointPairList list = new PointPairList();
            PointPairList avglist = new PointPairList();
            PointPairList avgUP = new PointPairList();
            PointPairList avgLOW = new PointPairList();

            //===========================
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string querry = "SELECT M.TIME_ST, M.VALUE FROM MEASUREMENTS M"
                + " JOIN PARAMETERS P ON P.NAME_ST = " + "'" + param + "'"
                + " WHERE P.ID = M.PARAM_ID";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            List<double> values = new List<double>();
            double upLim = Queries.GetLimit("upperLimitPercent", param);
            double lowLim = Queries.GetLimit("lowerLimitPercent", param);

            foreach (DbDataRecord record in reader)
            {
                list.Add(DateTime.Parse(record["TIME_ST"].ToString()).ToOADate(), Convert.ToDouble(record["VALUE"]));
                avgUP.Add(DateTime.Parse(record["TIME_ST"].ToString()).ToOADate(), upLim);
                avgLOW.Add(DateTime.Parse(record["TIME_ST"].ToString()).ToOADate(), lowLim);
            }

            connection.Close();
            //===========================

            LineItem myCurve = pane.AddCurve(param, list, Color.Blue, SymbolType.Diamond);

            LineItem avgUPLI = pane.AddCurve("UpperLimit (" + upLim + ")", avgUP, Color.DarkRed, SymbolType.None);
            LineItem avgLOWLI = pane.AddCurve("LowerLimit (" + lowLim + ")", avgLOW, Color.DarkRed, SymbolType.None);

            avgUPLI.Line.Width = 2;
            avgLOWLI.Line.Width = 2;
            // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
            // В противном случае на рисунке будет показана только часть графика, 
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
        }
    }
}
