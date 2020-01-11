using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DM.Data;
using System.Threading.Tasks;

namespace DataMiningOffLine
{
    public partial class Kohonen : Form
    {
        private decimal[,] values;

        public decimal[,] Values
        {
            get { return values; }
            set { values = value; }
        }

        private decimal[,] normalise;

        public decimal[,] Normalise
        {
            get { return normalise; }
            set { normalise = value; }
        }

        private decimal[,] weights;

        public decimal[,] Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        private decimal[,] normaliseRandom;

        public decimal[,] NormaliseWeights
        {
            get { return normaliseRandom; }
            set { normaliseRandom = value; }
        }

        private NetworkSettings bestKohonen = null;

        Dictionary<int, ParameterCharacteristics> listParameter = new Dictionary<int, ParameterCharacteristics>();
        
        public Kohonen()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Languages))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Languages);
            }
            InitializeComponent();
            ListAdd.Items.Clear();
            ListDelete.Items.Clear();
            List<int> keys = new List<int>();
            Dictionary<int, decimal> firstRow = TrainData.Train.First().Input;
            foreach (OneRow data in TrainData.Train)
            {
                foreach (var item in data.Input)
                {
                    if (keys.Contains(item.Key))
                        continue;
                    if (item.Key != firstRow[item.Key])
                        keys.Add(item.Key);
                }
                break;
            }
            var parameters = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def")).ToArray();
            foreach (var parameter1 in parameters)
            {
                ListAdd.Items.Add(XMLWork.FindNameWithScada(parameter1, Properties.Settings.Default.Languages));
            }
            foreach (int key in keys)
            {
                ListAdd.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
            }
            if (ListAdd.Items.Count != 0)
                ListAdd.SelectedIndex = 0;
            if (ListDelete.Items.Count != 0)
                ListDelete.SelectedIndex = 0;            
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
            catch (Exception ex)
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



        #region Delete Items

        /// <summary>
        /// Delete a parameter from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteOneItem(object sender, EventArgs e)
        {
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

        #endregion

        #endregion


        private void loadMeasurements_Click(object sender, EventArgs e)
        {
            if (ListDelete.Items.Count != 0)
            {
                dataGridView2.Rows.Clear();
                bestKohonen = new NetworkSettings(Convert.ToInt32(dimensionOfVector.Text),
                    Convert.ToInt32(numberOfNeurons.Text), Convert.ToInt32(numberOfTrainingCycles.Text),
                    Convert.ToInt32(xMap.Text), Convert.ToInt32(yMap.Text), Convert.ToInt32(rangeOfLearning.Text),
                    TrainData.Train.Count, weights, normalise, Convert.ToDecimal(alfa.Text));
                decimal fuhler = decimal.MaxValue;
                Task<decimal> task;
                for (int i = 0; i <= Convert.ToInt32(sampleSize.Text); i++)
                {
                    numberOfNeurons.Text = (Convert.ToInt32(xMap.Text)*Convert.ToInt32(yMap.Text)).ToString();
                    LoadFromDB();
                    NetworkSettings ns = new NetworkSettings(Convert.ToInt32(dimensionOfVector.Text),
                        Convert.ToInt32(numberOfNeurons.Text), Convert.ToInt32(numberOfTrainingCycles.Text),
                        Convert.ToInt32(xMap.Text), Convert.ToInt32(yMap.Text), Convert.ToInt32(rangeOfLearning.Text),
                        TrainData.Train.Count, weights, normalise, Convert.ToDecimal(alfa.Text));

                    decimal error = 0.0M;

                    LearningNetwork finalWeightVectors = new LearningNetwork();
                    task = new Task<decimal>(finalWeightVectors.TrainingOfKohonenLayer, ns);
                    task.Start();
                    task.Wait();
                    //error = finalWeightVectors.TrainingOfKohonenLayer(ref normalise,  ref weights, ns);
                    dataGridView2.Rows.Add();
                    int k = dataGridView2.RowCount;
                    dataGridView2[1, k-1].Value = Math.Round(task.Result,3);
                    dataGridView2[0, k-1].Value = k;
                    if (fuhler > error)
                    {
                        bestKohonen = ns;
                        fuhler = error;
                    }
                }

                int count = dataGridView2.RowCount;
                decimal averageError = 0.0M;
                for (int i = 0; i < count; i++)
                {
                    averageError += Convert.ToDecimal(dataGridView2[1, i].Value);
                }
                averageError /= count;
                dataGridView2[1, count - 1].Value = averageError;
                dataGridView2[0, count - 1].Value = Localization.MyStrings.Average;
            }
            else MessageBox.Show(Localization.MyStrings.AddParameter);
        }
        
        public void LoadFromDB()
        {
            values = null;
            normalise = null;
            weights = null;
            normaliseRandom = null;
            listParameter.Clear();

            values = new decimal[ListDelete.Items.Count, TrainData.Train.Count];
            normalise = new decimal[ListDelete.Items.Count, TrainData.Train.Count];
            normaliseRandom = new decimal[ListDelete.Items.Count, TrainData.Train.Count];
            weights = new decimal[ListDelete.Items.Count,Convert.ToInt32(numberOfNeurons.Text)];
            dimensionOfVector.Text = Convert.ToString(ListDelete.Items.Count);
            
            int count = 0;
            foreach (var item in ListDelete.Items)
            {
                int parameterID = XMLWork.FindIDWithName(item.ToString(), Properties.Settings.Default.Languages);
                OneRow row = TrainData.Train.First();
                decimal value = 0.0M;
                if (row.Input.ContainsKey(parameterID))
                    value = row.Input[parameterID];
                else if (row.Output.ContainsKey(parameterID))
                    value = row.Output[parameterID];
                listParameter.Add(count, new ParameterCharacteristics(item.ToString(), value, value, value, 0.0M, 0.0M));
                count++;
            }
            if (TrainData.Train.Count > 0)
            {
                int j = 0;
                //Вычисление среднего значения каждого атрибута выборки + сумма, минимум и максимум также вычисляются
                foreach (OneRow row in TrainData.Train)
                {
                    for (int i = 0; i < listParameter.Count; i++)
                    {
                        try
                        {
                            int parameterID = XMLWork.FindIDWithName(listParameter[i].ParameterName, Properties.Settings.Default.Languages);
                            if (row.Input.ContainsKey(parameterID))
                                values[i, j] = row.Input[parameterID];
                            else if (row.Output.ContainsKey(parameterID))
                                values[i, j] = row.Output[parameterID];
                            if (values[i, j] < listParameter[i].Min)
                                listParameter[i].Min = values[i, j];
                            if (values[i, j] > listParameter[i].Max)
                                listParameter[i].Max = values[i, j];
                            listParameter[i].Sum += values[i, j];
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        
                    }
                    j++;
                }
                //Поиск среднего значения по выборке
                for (int i = 0; i < listParameter.Count; i++)
                {
                    listParameter[i].Mean = listParameter[i].Sum / Convert.ToDecimal(TrainData.Train.Count);
                    j = 0;
                    decimal differenceSquares =0.0M;
                    foreach (OneRow row in TrainData.Train)
                    {
                        differenceSquares += ((values[i, j] - listParameter[i].Mean) * (values[i, j] - listParameter[i].Mean));
                        j++;
                    }
                    listParameter[i].Deviation = differenceSquares / (decimal)(TrainData.Train.Count - 1);
                }



                //Нормализация данных
                for (int i = 0; i < listParameter.Count; i++)
                {
                    for (int k = 0; k < TrainData.Train.Count; k++)
                    {
                        if (listParameter[i].Max - listParameter[i].Min != 0)
                        {
                            normalise[i, k] = Normalisation.NormaliseWithDeviation(values[i, k], listParameter[i].Mean, listParameter[i].Deviation);
                        }
                        else
                        {
                            normalise[i, k] = 0.0M;
                        }
                    }
                }
                
            }
            else MessageBox.Show(Localization.MyStrings.Absent,  Localization.MyStrings.Warning);


            //
            int step = TrainData.Train.Count / Convert.ToInt32(numberOfNeurons.Text);
            
            //Здесь задаются начальные значения весов
            /*
            for (int j = 0, n = 0; j < TrainData.Train.Count; j = j + step, n++)
                {
                    if (n < Convert.ToInt32(numberOfNeurons.Text))
                    {
                        for (int i = 0; i < listParameter.Count; i++)
                            weights[i,n] = normalise[i,j];
                    }
                }*/
            Random rand = new Random((int) (DateTime.Now.Ticks));
            for (int j = 0; j < Convert.ToInt32(numberOfNeurons.Text); j++)
            {
                for (int k = 0; k < listParameter.Count; k++)
                {
                    weights[k, j] = Randomise.RandomDecimal(rand);
                }
            }
            }

        

        private void mapRun_Click(object sender, EventArgs e)
        {
            if (normalise != null)
            {

                List<Stat> stat = new List<Stat>();
                
                NetworkOperation net = new NetworkOperation();
                decimal[,] valueOfNeurons = new decimal[bestKohonen.DimentionOfVector, bestKohonen.NumberOfNeurons];
                valueOfNeurons = net.OperationOfKohonenLayer(bestKohonen.Normalise, bestKohonen.Weights, Values, bestKohonen);
                List<string> nameParam = new List<string>();
                foreach (KeyValuePair<int, ParameterCharacteristics> parameterCharacteristicse in listParameter)
                {
                    nameParam.Add(parameterCharacteristicse.Value.ParameterName);
                    stat.Add(new Stat(parameterCharacteristicse.Value.Min, parameterCharacteristicse.Value.Max,
                        parameterCharacteristicse.Value.Mean));
                }
                ParentForMaps pearntForm = new ParentForMaps(valueOfNeurons, stat, bestKohonen, nameParam);
                pearntForm.Show();
            }
            else
            {
                MessageBox.Show(Localization.MyStrings.TrainSample);
            }
        }
        

    }
    public static class Randomise
    {
        static readonly double _min = 0.4;
        static readonly double _max = 0.6;

        public static decimal RandomDecimal(Random random)
        {
            return Convert.ToDecimal(random.NextDouble() * (_max - _min) + _min);
        }
    }
}
