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

namespace DataMiningOffLine.Forms
{
    public partial class tableRolls : Form
    {
        public tableRolls()
        {
            InitializeComponent();
            FillData();
        }

        private DataTable _issue = new DataTable("Coodinates");

        private void addInformation_Click(object sender, EventArgs e)
        {
            try
            {
                _issue.Rows.Add("2 Каландровый вал", 0.550*3*3.14/(2.0)); // 1 - 2 Каландровый вал
                _issue.Rows.Add("3 Каландровый вал", 0.550*3.14); // 3 Каландровый вал
                _issue.Rows.Add("4 Каландровый вал", 0.550*3.14/(2.0)); // 4 Каландровый вал
                _issue.Rows.Add(" ", 0.006);
                _issue.Rows.Add("1 съемный вал", 0.05*3.14); //1  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("2 съемный вал", 0.05 * 3.14); //2  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("3 съемный вал", 0.05 * 3.14); //3  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("4 съемный вал", 0.05 * 3.14); //4  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("5 съемный вал", 0.05 * 3.14); //5  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("6 съемный вал", 0.05 * 3.14); //6  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("7 съемный вал", 0.05 * 3.14); //7  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("8 съемный вал", 0.05 * 3.14); //8  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("9 съемный вал", 0.05 * 3.14); //9  съемный вал
                _issue.Rows.Add(" ", 0.003);
                _issue.Rows.Add("10 съемный вал", 0.05 * 3.14 / (2.0)); //10 съемный вал
                _issue.Rows.Add(" ", 0.08);
                _issue.Rows.Add("1 темперирующий вал", 0.13 * 3.14); //1 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("2 темперирующий вал", 0.13 * 3.14); //2 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("3 темперирующий вал", 0.13 * 3.14); //3 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("4 темперирующий вал", 0.13 * 3.14); //4 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("5 темперирующий вал", 0.13 * 3.14); //5 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("6 темперирующий вал", 0.13 * 3.14); //6 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("7 темперирующий вал", 0.13 * 3.14); //7 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("8 темперирующий вал", 0.13 * 3.14); //8 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("9 темперирующий вал", 0.13 * 3.14); //9 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("10 темперирующий вал", 0.13 * 3.14); //10 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("11 темперирующий вал", 0.13 * 3.14); //11 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("12 темперирующий вал", 0.13 * 3.14); //12 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("13 темперирующий вал", 0.13 * 3.14); //13 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("14 темперирующий вал", 0.13 * 3.14); //14 темперирующий вал
                _issue.Rows.Add(" ", 0.004);
                _issue.Rows.Add("15 темперирующий вал", 0.13*3.14); //15 темперирующий вал
                _issue.Rows.Add(" ", 0.5);
                _issue.Rows.Add("тянущее устройства", 0.2*3.14); //тянущее устройство
                _issue.Rows.Add(" ", 0.05);
                _issue.Rows.Add("тянущее устройства", 0.2*3.14); //тянущее устройство
                _issue.Rows.Add(" ", 1.2);
                _issue.Rows.Add("Тянущее устройства", 0.12*3.14); //тянущее устройство
                _issue.Rows.Add(" ", 1.25);
                _issue.Rows.Add(" ", 0.5);
                _issue.Rows.Add(" ", 0.27);

                gridControl1.DataSource = _issue;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Неправильный формат строки" + ex.ToString());
            }


        }

        private void FillData()
        {
            _issue.Columns.Add("Place", typeof (String));
            _issue.Columns.Add("Length", typeof (double));
            _issue.Columns[0].ColumnName = "Расположение, м";
            _issue.Columns[1].ColumnName = "Длина пути, м";

        }

        private void deleteRoll_Click(object sender, EventArgs e)
        {
            if ((gridControl1.DataSource as DataTable) == null || gridControl1.SelectedCells.Count == 0) return;



            DataRow[] rows = new DataRow[gridControl1.SelectedCells.Count];

            for (int i = 0; i < gridControl1.SelectedCells.Count; i++)

                rows[i] = (gridControl1.DataSource as DataTable).Rows[gridControl1.SelectedCells[i].RowIndex];


            //gridControl1.BeginSort();

            try
            {

                foreach (DataRow row in rows)

                    row.Delete();

            }

            finally
            {

               // gridView1.EndSort();

            }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            if (gridControl1.RowCount == 0)
            {
                MessageBox.Show("Нету значений");
            }
            else
            {
                OneRow row = new OneRow();
                if (TrainData.Train.Count != 0)
                {

                    foreach (OneRow oneRow in TrainData.Train)
                    {
                        row = oneRow;
                        break;
                    }
                    //TODO: Поставить нормальное определение времени
                    ProductionTime time = new ProductionTime(row.Input[60], row.Input[73], row.Input[87]); 
                    decimal timeProduct = 0.0M;
                    for (int i = gridControl1.RowCount - 1; i >= 0; i--)
                    {
                        if (i > 23)
                        {
                            //decimal s = Convert.ToDecimal(gridView1.GetRowCellValue(i, gridView1.Columns[1]));
                            decimal s = Convert.ToDecimal((gridControl1.DataSource as DataTable).Rows[i].ItemArray[1]);
                            timeProduct += s/time.TemperingVelocity;
                            if (i == 24)
                            {
                                decimal convertedTime = timeProduct*60;
                                time.SetTempTime(convertedTime);
                            }
                        }
                        else if (i > 3 && i <= 23)
                        {
                            //decimal s = Convert.ToDecimal(gridView1.GetRowCellValue(i, gridView1.Columns[1]));
                            decimal s = Convert.ToDecimal((gridControl1.DataSource as DataTable).Rows[i].ItemArray[1]);
                            timeProduct += s/time.TakeOffVelocity;
                            if (i == 4)
                            {
                                decimal convertedTime = timeProduct*60;
                                time.SetTakeOffTime(convertedTime);
                            }
                        }
                        else
                        {
                            //decimal s = Convert.ToDecimal(gridView1.GetRowCellValue(i, gridView1.Columns[1]));
                            decimal s = Convert.ToDecimal((gridControl1.DataSource as DataTable).Rows[i].ItemArray[1]);
                            timeProduct += s/time.CalenderVelocity;
                            if (i == 0)
                            {
                                decimal convertedTime = timeProduct*60;
                                time.SetCalenderTime(convertedTime);
                                time.SetExtruderTime(ProductionTime.CalenderTime);
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Все очень плохо! Выборка не собрана");
                }
            }

            //gridView1.GetRowCellValue(0, gridView1.Columns[1]);
        }

        private void GridControl1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            gridControl1.CancelEdit();
        }

        void EnableDoubleBuffering()
        {
            if (!SystemInformation.TerminalServerSession)
            {
                Type dgvType = gridControl1.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(gridControl1, true, null);
            }
        }
    }
}