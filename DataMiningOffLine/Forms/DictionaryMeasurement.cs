using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DM.Data;

namespace DataMiningOffLine
{
    public partial class DictionaryMeasurement : Form
    {
        public DictionaryMeasurement()
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
        /// Main method 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DictionaryMeasurement_Shown(object sender, EventArgs e)
        {
            int columns = TrainData.Train[0].Input.Count;
            int i;
            DataTable sourceTable = new DataTable();
            sourceTable.Columns.Add("Date");
            foreach (KeyValuePair<int, decimal> keyValuePair in TrainData.Train[0].Input)
            {

                String parName = XMLWork.FindNameWithID(keyValuePair.Key, Properties.Settings.Default.Languages);
                sourceTable.Columns.Add(parName);
            }
            foreach (KeyValuePair<int, decimal> keyValuePair in TrainData.Train[0].Output)
            {
                String parName = XMLWork.FindNameWithID(keyValuePair.Key, Properties.Settings.Default.Languages);
                sourceTable.Columns.Add(parName);
            }
            foreach (OneRow oneRow in TrainData.Train)
            {
                DataRow row = sourceTable.NewRow();
                i = 1;
                foreach (KeyValuePair<int, decimal> valuePair in oneRow.Input.OrderBy(o => o.Key))
                {
                    row[0] = oneRow.Date;
                    row[i] = valuePair.Value;
                    i++;
                }
                foreach (KeyValuePair<int, decimal> valuePair in oneRow.Output.OrderBy(o => o.Key))
                {
                    row[i] = valuePair.Value;
                    i++;
                }
                sourceTable.Rows.Add(row);
            }
            gridMeasurements.DataSource = sourceTable;
            EnableDoubleBuffering();
        }

        void EnableDoubleBuffering()
        {
            if (!SystemInformation.TerminalServerSession)
            {
                Type dgvType = gridMeasurements.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(gridMeasurements, true, null);
            }
        }
    }
}
