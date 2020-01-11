using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DataMiningOffLine.Forms;
using ExcelDataReader;
using DM.Data;
using System.Threading;

namespace DataMiningOffLine
{
    public partial class Data : Form
    {
        Dictionary<string, int> elements = new Dictionary<string, int>();
        List<Measurements> measurementses = new List<Measurements>();
        private IFormatProvider _formatProvider;
        private Int64 _counter = 0;

        public Int64 RowsProcessed
        {
            get
            {
                return _counter;
            }
        }
        public Data()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Languages))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
            }
            //костыль с базой данных для статистического анализа
            DataBaseND db = new DataBaseND() { nameDB = "Resources\\BDUsersND.db" };
            db.UpdateUserStatus();
            db.CloseConnectionDB();
            InitializeComponent();
        }

        private void OpenExcel_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                path.Text = openFileDialog.FileName;
            }
            try
            {
                if (path.Text.Trim() == "")
                {
                    MessageBox.Show(Localization.MyStrings.PathWay);
                }
                else
                {
                    ListAdd.Items.Clear();
                    ListDelete.Items.Clear();
                    ReadExcel(path.Text.ToString());
                    if (ListAdd.Items.Count != 0)
                        ListAdd.SelectedIndex = 0;
                    if (ListDelete.Items.Count != 0)
                        ListDelete.SelectedIndex = 0;
                    DBClasses.ParseExcel.excelPath = path.Text.Trim(new char[] { '"' });
                }
            }
            catch (IOException ioex)
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void addListBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (path.Text.Trim() == "")
                {
                    MessageBox.Show(Localization.MyStrings.PathWay);
                }
                else
                {
                    ListAdd.Items.Clear();
                    ListDelete.Items.Clear();
                    ReadExcel(path.Text.ToString());
                }
            }
            catch (IOException ioex)
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        public void ReadExcel(string filePath)
        {
            elements.Clear();
            var culture = new CultureInfo("");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            _formatProvider = culture;
            bool check = true;
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            var eReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            try
            {
                while (eReader.Read())
                {
                    ++_counter;
                    int i = 1;
                    var str = eReader.GetString(1);
                    if (Object.Equals(str, "Uhrzeit"))
                    {
                        while (check)
                        {
                            str = eReader.GetString(i + 1);
                            if (Object.Equals(str, "Material"))
                            {
                                check = false;
                                break;
                            }
                            else
                            {
                                i = i + 1;
                            }
                        }

                        int j = 2;

                        while (j <= i)
                        {
                            #region Add Parameter

                            string name = eReader.GetString(j);
                            if ((!name.StartsWith("Trigger")) && (!name.StartsWith("Process")))
                            {
                                ListAdd.Items.Add((object)eReader.GetString(j));
                                elements.Add(eReader.GetString(j), j);
                                j++;
                            }
                            else j++;

                            #endregion
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                eReader.Close();
            }
        }

        private void AddElement(object sender, EventArgs e)
        {
            MoveListBoxItems(ListAdd, ListDelete);
        }

        private void AddAll_Click(object sender, EventArgs e)
        {
            MoveAllItems(ListAdd, ListDelete);
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
                if ((source.Items.Count != 0))
                {
                    destination.Items.Add(source.SelectedItem);
                    source.Items.RemoveAt(source.SelectedIndex);
                    if (source.Items.Count != 0)
                        source.SelectedIndex = 0;
                    if (destination.Items.Count != 0)
                        destination.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            if (destination.Items.Count != 0)
                destination.SelectedIndex = 0;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(ListDelete, ListAdd);
        }

        private void DeleteAll_Click(object sender, EventArgs e)
        {
            MoveAllItems(ListDelete, ListAdd);
        }
        //Сбор данных
        private void dataAcq_Click(object sender, EventArgs e)
        {
            if (ListDelete.Items.Count != 0)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                TrainData.Train.Clear();
                measurementses.Clear();
                Dictionary<string, int> newElements = new Dictionary<string, int>();
                if (ListDelete.Items.Count != 0)
                {
                    //Сохранения имен параметров и их ID в справочник newElements
                    foreach (var item in ListDelete.Items)
                    {
                        string t = item.ToString();
                        int j = elements.Where(s => s.Key == t).Single().Value;
                        newElements.Add(t, j);
                        //Работа с локальной базой данных(сохранения данных туда)
                        XMLWork.AddRow(j, t);
                    }
                }
                var stream = File.Open(path.Text, FileMode.Open, FileAccess.Read);
                var eReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                bool check = true;
                try
                {
                    _counter = 0;
                    while (eReader.Read()) // Одну строку пропускаем с общей информацией
                    {
                        ++_counter;
                        break;
                    }
                    DateTime lastTime = new DateTime();
                    while (eReader.Read()) // Считывание информации с EXCEL файла
                    {
                        var date = eReader.GetDateTime(0);
                        var time = eReader.GetDateTime(1);
                        TimeSpan ts = new TimeSpan(time.Hour, time.Minute, time.Second);
                        date = date + ts;
                        if (_counter == 1)
                            lastTime = date;
                        else
                        {
                            if (lastTime == date)
                                continue;
                            lastTime = date;
                        }
                        Measurements measurements;
                        foreach (var value in newElements)
                        {

                            Decimal parValue;
                            var val = Convert.ToString(eReader.GetValue(value.Value));
                            val = val.Trim();
                            if (val == "")
                            {
                                parValue = -1;
                            }
                            else
                            {
                                var extA = Convert.ToString(eReader.GetValue(value.Value)).Replace(",", ".");
                                try
                                {
                                    parValue = Decimal.Parse(extA, NumberStyles.Float, _formatProvider);
                                }
                                catch (Exception exe)
                                {
                                    throw new Exception("Invalid value format in row " + _counter, exe);
                                }
                                measurements = new Measurements(parValue, date, value.Key, value.Value); // созеания измерения

                                measurementses.Add(measurements); // и добавление его в коллекцию
                            }

                        }

                        ++_counter;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    eReader.Close();
                }
                try
                {
                    List<OneRow> dataSet = new List<OneRow>();
                    var uniqueTimestamp = measurementses.OrderBy(o => o.TimeStamp).Select(o => o.TimeStamp).Distinct();
                    OneRow lastRow = new OneRow();

                    // Отделение технологических параметров от показателей качество.
                    //TODO: сделать отдельную колонку в бд, в которой хранить краткую информацию о расположении датчика относительно производственной линии
                    foreach (DateTime dateTime in uniqueTimestamp)
                    {
                        var moveData =
                            measurementses.Where(
                                o => ((dateTime == o.TimeStamp) && (!o.ParameterName.StartsWith("Def"))))
                                .ToDictionary(o => o.ParID, s => s.Value); // Добавление технологических параметров в дикшионари
                        //температуры для усадки
                        var temperatureData = measurementses.Where(o => ((dateTime == o.TimeStamp) && (o.ParameterName.Contains("XT"))))
                                .ToDictionary(o => o.ParameterName, s => s.Value);
                        //скорости для усадки
                        var velocityData = measurementses.Where(o => ((dateTime == o.TimeStamp) && (o.ParameterName.Contains("XV"))))
                                .ToDictionary(o => o.ParameterName, s => s.Value);
                        OneRow row = new OneRow();
                        row.Input = moveData;
                        row.Temperatures = temperatureData;
                        row.Velocities = velocityData;
                        if (row.Input.Count >= 1)
                        {
                            row.Date = dateTime;
                            var dataOutput =
                                measurementses.Where(
                                    o => ((dateTime == o.TimeStamp) && (o.ParameterName.StartsWith("Def"))))
                                    .ToDictionary(o => o.ParID, s => s.Value);
                            row.Output = dataOutput;
                            dataSet.Add(row); // Добавление показателя качества в ОЗУ
                        }
                        lastRow = row;
                    }

                    var datas =
                        measurementses.Where(o => dataSet.First().Date == o.TimeStamp)
                            .ToDictionary(o => o.ParID, s => s.ParameterName);
                    TrainData.GetData();
                    foreach (KeyValuePair<int, string> keyValuePair in datas)
                    {

                        if (!TrainData.Pairs.ContainsKey(keyValuePair.Value))
                            TrainData.Pairs.Add(keyValuePair.Value, keyValuePair.Value);
                    }
                    TrainData.nameParameter = datas;
                    TrainData.Train = dataSet; // Присвоение статической переменной данных
                }
                catch (InvalidOperationException iex)
                {
                    var parameters = measurementses.Where(o => !o.ParameterName.StartsWith("Def")).ToArray();
                    if (parameters.Count() == 0)
                        MessageBox.Show(
                            Localization.MyStrings.QualityParameters);
                    else MessageBox.Show(iex.ToString());

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                sw.Stop();
                var parametersInput = measurementses.Where(o => o.ParameterName.StartsWith("Def")).ToArray();
                if (parametersInput.Count() == 0)
                    MessageBox.Show(
                        Localization.MyStrings.RegimeParameters, Localization.MyStrings.Warning);
                MessageBox.Show(Localization.MyStrings.TimeParameters + sw.Elapsed);
                string pathToShrinkage = System.AppDomain.CurrentDomain.BaseDirectory + "\\Shrinkage.xml";
                if (File.Exists(pathToShrinkage))
                {
                    File.Delete(pathToShrinkage);
                }
            }
            else
                MessageBox.Show(Localization.MyStrings.SelectedParameters);
        }

        private void regression_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    RegressionModelcs regression = new RegressionModelcs();
                    regression.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    TrafficLight tf = new TrafficLight();
                    tf.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);
            }
        }

        private void Thrends_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                ParameterAnalyzerForm pf = new ParameterAnalyzerForm();
                pf.Show();
            }
        }

        private void DecisionTree_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    DesitionTree dt = new DesitionTree();
                    dt.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);
            }
        }

        private void dictionaryForm_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                DictionaryMeasurement dm = new DictionaryMeasurement();
                dm.Show();
            }
        }
        /// <summary>
        /// Сетевые карты кохонена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kohonen_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                Kohonen koh = new Kohonen();
                koh.Show();
            }
        }

        /// <summary>
        /// Статистический анализ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void статистическийАнализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    
                    NormalDistribution distribution = new NormalDistribution();
                    distribution.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);
            }
        }

        /// <summary>
        /// Регрессия Фишера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void регрессионныйАнализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    RegressionModelcs regression = new RegressionModelcs();
                    regression.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);

            }
        }

        /// <summary>
        /// Новый регрессионный анализ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void регрессионныйАнализ2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    RegressionAnalysisNew regression = new RegressionAnalysisNew();
                    regression.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);
            }
        }

        /// <summary>
        /// Decision Trees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void деревьяПринятияРешенийToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    DesitionTree dt = new DesitionTree();
                    dt.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);
            }
        }
        /// <summary>
        /// Карты кохонена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сетевыеКартыКохоненаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                Kohonen koh = new Kohonen();
                koh.Show();
            }
        }
        /// <summary>
        /// Светофор
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void светофорToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    TrafficLight tf = new TrafficLight();
                    tf.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);
            }
        }
        /// <summary>
        /// Трендовый анализ данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void трендовыйАнализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                ParameterAnalyzerForm pf = new ParameterAnalyzerForm();
                pf.Show();
            }
        }
        /// <summary>
        /// Справочник измерений, если они есть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void справочникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                DictionaryMeasurement dm = new DictionaryMeasurement();
                dm.Show();
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void Data_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Переключение на русский язык
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Languages = System.Globalization.CultureInfo.GetCultureInfo("ru-RU").Name;
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Переключение на английский язык
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void английскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Languages = System.Globalization.CultureInfo.GetCultureInfo("en-US").Name;
            Properties.Settings.Default.Save();
        }
        //Мнемосхема производства
        private void мнемосхемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                MnemonicScheme mnemonic = new MnemonicScheme();
                mnemonic.Show();
            }
        }

        //Справочник параметров
        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(XMLWork.Path))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                DictionaryParameters dp = new DictionaryParameters();
                dp.Show();
            }
        }

        private void схемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(XMLWork.Path))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                tableRolls tr = new tableRolls();
                tr.Show();
            }
        }

        private void анализКачестваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                QualityFind qf = new QualityFind();
                qf.Show();
            }
        }




        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            foreach (OneRow row in TrainData.Train)
            {
                var point = CalculateShrinkageValue(row.Temperatures.Values.ToArray(), row.Velocities.Values.ToArray());
                XMLWork.addShrinkageFile(row.Date, point);
            }
            MessageBox.Show("Данные об усадке собраны");
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
            shrinkage = 0.4m * (shrinkage * 0.03m + velocities[7] * 0.3m);
            return shrinkage;
            //return (-11.32m + 0.23m * velocities[6] - 2.44m * velocities[6] * velocities[6] + 0.13m * temperatures[6])*0.6m;
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

        /* Нештатные ситуации
         * Запуск происходит в отдельном потоке, дабы не мешать работе основной программы.
         */
        private void helperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var result = MessageBox.Show("Выгрузить данные из excel в БД?", "Выгрузить данные из excel в БД?",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Сейчас начнется загрузка данных в БД, это займет некоторое время.");

                    HelperForm f = new HelperForm();
                    f.UploadDB();
                }

                new HelperForm().Show();


            }
        }

        private void kPIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainData.Train.Count == 0)
            {
                MessageBox.Show(Localization.MyStrings.DataNotGiven);
            }
            else
            {
                var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
                if (parameters.Count() != 0)
                {
                    KPI kpi = new KPI();
                    kpi.Show();
                }
                else
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);

            }
        }
    }
}
