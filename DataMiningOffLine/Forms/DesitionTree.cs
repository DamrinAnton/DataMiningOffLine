using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using DM.Data;
using DM.DecisionTree;
using DM.WPF_Tree;

namespace DataMiningOffLine
{
    public partial class DesitionTree : Form
    {

        DM.DecisionTree.TreeNode _root = new DM.DecisionTree.TreeNode();
        Random rand = new Random((int)DateTime.Now.Ticks);
        private List<DM.DecisionTree.TreeNode> rndForests;

        public List<DM.DecisionTree.TreeNode> RandForests { get { return rndForests; } set { rndForests = value; } }
        public DesitionTree()
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

        private void DesitionTree_Shown(object sender, EventArgs e)
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
        private void parameterCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListAdd.Items.Clear();
            ListDelete.Items.Clear();
            List<int> keys = new List<int>();
            int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
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
            foreach (int key in keys)
            {
                ListAdd.Items.Add(XMLWork.FindNameWithID(key, Properties.Settings.Default.Languages));
            }
            if (ListAdd.Items.Count != 0)
                ListAdd.SelectedIndex = 0;
            if (ListDelete.Items.Count != 0)
                ListDelete.SelectedIndex = 0;
        }

        private void trainButton_Click(object sender, EventArgs e)
        {
            if ((parameterCondition.Items.Count != 0) && (ListDelete.Items.Count != 0) && (parameterCondition.SelectedIndex != -1))
            {

                try
                {

                    GetBooleanForOutput(TrainData.Train, criterion.Value);
                    bool falseData = false;
                    bool trueData = false;
                    int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
                    foreach (OneRow oneRow in TrainData.Train)
                    {
                        
                        if ((oneRow.OutputBool[parameterID] == true) && (trueData == false))
                            trueData = true;
                        else if((oneRow.OutputBool[parameterID] == false) && (falseData == false))
                            falseData = true;
                    }

                    if ((falseData) && (trueData))
                        PreprocessToWorkTree();
                    else
                        MessageBox.Show(Localization.MyStrings.AnotherCriterion);
                }
                catch
                    (InvalidOperationException ex)
                {
                    MessageBox.Show(Localization.MyStrings.SelectCriterion);
                    criterion.BackColor = Color.White;
                    ListDelete.BackColor = Color.White;
                    parameterCondition.BackColor = Color.Red;
                }
                catch
                    (Exception excep)
                {
                    if (criterion.Text.Trim() == "")
                    {
                        MessageBox.Show(Localization.MyStrings.SelectPartitioninCriterion);
                        criterion.BackColor = Color.Red;
                        ListDelete.BackColor = Color.White;
                        parameterCondition.BackColor = Color.White;
                    }
                    else if (ListDelete.Items.Count == 0)
                    {
                        MessageBox.Show(Localization.MyStrings.AddParameter);
                        criterion.BackColor = Color.White;
                        ListDelete.BackColor = Color.Red;
                        parameterCondition.BackColor = Color.White;
                    }
                    else
                    {
                        MessageBox.Show(excep.ToString());
                        criterion.BackColor = Color.White;
                        ListDelete.BackColor = Color.White;
                        parameterCondition.BackColor = Color.White;
                    }
                }
            }
            else
            {
                if (parameterCondition.Items.Count == 0)
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);
                else if ((parameterCondition.SelectedIndex == -1))
                {
                    MessageBox.Show(Localization.MyStrings.SelectCriterion);
                    criterion.BackColor = Color.White;
                    ListDelete.BackColor = Color.White;
                    parameterCondition.BackColor = Color.Red;
                }
                if (ListDelete.Items.Count == 0)
                {
                    MessageBox.Show(Localization.MyStrings.AddParameter);
                    criterion.BackColor = Color.White;
                    ListDelete.BackColor = Color.Red;
                    parameterCondition.BackColor = Color.White;
                }
            }
            // Add series.
        }

        private void GetBooleanForOutput(List<OneRow> trainData, decimal criterion)
        {
            int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
            foreach (OneRow oneRow in trainData)
            {
                if (oneRow.Output[parameterID] >= criterion)
                    oneRow.OutputBool[parameterID] = true;
                else oneRow.OutputBool[parameterID] = false;

            }
        }

        private void PreprocessToWorkTree()
        {
            var c45 = new DecisionTreeC45();
            int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
            _root = c45.MountTree(TrainData.Train, TrainData.nameParameter, parameterID, Convert.ToInt32(textEdit1.Value) + 1);
            MessageBox.Show(Localization.MyStrings.Over);
        }
        /// <summary>
        /// Модуль очищения панели и построения WPF окна заново с построением дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                panel1.Controls[i].Enabled = false;
                panel1.Controls[i].Dispose();
                i--;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ElementHost test_wpf;
            MyControl1 test_component1;
            if (_root.MChilds != null)
            {
                test_wpf = new ElementHost();
                test_wpf.Dock = DockStyle.Fill;
                panel1.Controls.Clear();
                panel1.Controls.Add(test_wpf);
                test_component1 = new DM.WPF_Tree.MyControl1(_root, null, Properties.Settings.Default.Languages);
                test_component1.InitializeComponent();
                test_wpf.Child = test_component1;
            }
            else { System.Windows.MessageBox.Show(Localization.MyStrings.TreeBuilding); }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DecisionTreeRandomForest();
        }

        private void DecisionTreeRandomForest()
        {
            var checkData = new TrainData();
            var analysParameter = new Dictionary<int, string>();
            var trainData = new TrainData();
            var trainedData = new TrainData();

            if ((parameterCondition.Items.Count != 0) && (ListDelete.Items.Count != 0) &&
                (parameterCondition.SelectedIndex != -1))
            {

                try
                {
                    GetBooleanForOutput(TrainData.Train, Convert.ToDecimal(criterion.Text));
                    analysParameter = TrainData.nameParameter.Where(o => !o.Value.StartsWith("Def"))
                        .ToDictionary(o => o.Key, o => o.Value);
                    bool falseData = false;
                    bool trueData = false;
                    int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
                    foreach (OneRow oneRow in TrainData.Train)
                    {
                        
                        if ((oneRow.OutputBool[parameterID] == true) && (trueData == false))
                            trueData = true;
                        else if ((oneRow.OutputBool[parameterID] == false) && (falseData == false))
                            falseData = true;
                    }
                    if ((falseData) && (trueData))
                    {
                        PreprocessToWorkTreeRandomForest(trainedData, checkData, analysParameter);
                        System.Windows.Forms.MessageBox.Show(Localization.MyStrings.Over);
                    }
                    else
                        MessageBox.Show(Localization.MyStrings.AnotherCriterion);
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show(Localization.MyStrings.SelectPartitioninCriterion);
                    criterion.BackColor = Color.Red;
                    ListDelete.BackColor = Color.White;
                    parameterCondition.BackColor = Color.White;
                }
                catch
                    (InvalidOperationException ex)
                {
                    MessageBox.Show(Localization.MyStrings.SelectCriterion);
                    criterion.BackColor = Color.White;
                    ListDelete.BackColor = Color.White;
                    parameterCondition.BackColor = Color.Red;
                }
                catch (Exception excep)
                {

                    if (ListDelete.Items.Count == 0)
                    {
                        MessageBox.Show(Localization.MyStrings.AddParameter);
                        criterion.BackColor = Color.White;
                        ListDelete.BackColor = Color.Red;
                        parameterCondition.BackColor = Color.White;
                    }
                    else
                    {
                        MessageBox.Show(excep.ToString());
                        criterion.BackColor = Color.White;
                        ListDelete.BackColor = Color.White;
                        parameterCondition.BackColor = Color.White;
                    }
                }
            }
            else
            {
                if (parameterCondition.Items.Count == 0)
                    MessageBox.Show(Localization.MyStrings.WithoutDefect);
                else if ((parameterCondition.SelectedIndex == -1))
                {
                    MessageBox.Show(Localization.MyStrings.SelectCriterion);
                    criterion.BackColor = Color.White;
                    ListDelete.BackColor = Color.White;
                    parameterCondition.BackColor = Color.Red;
                }
                if (ListDelete.Items.Count == 0)
                {
                    MessageBox.Show(Localization.MyStrings.AddParameter);
                    criterion.BackColor = Color.White;
                    ListDelete.BackColor = Color.Red;
                    parameterCondition.BackColor = Color.White;
                }
            }

        }

        private void PreprocessToWorkTreeRandomForest(TrainData trainedData, TrainData checkData,
            Dictionary<int, string> analysParameter)
        {
            int numberOfForest = Convert.ToInt32(numberOfForests.Value);
            RandForests = new List<DM.DecisionTree.TreeNode>();
            //double accuracy = 0.0;
            //do
            //{
            //Random rand = new Random((int) (DateTime.Now.Ticks));
            //DivideSet(rand, trainData, trainedData, checkData);
            List<DM.DecisionTree.TreeNode> rndForests = new List<DM.DecisionTree.TreeNode>();
            int parameterID = XMLWork.FindIDWithName(parameterCondition.SelectedItem.ToString(), Properties.Settings.Default.Languages);
            int restrictCountOfParameters = Convert.ToInt32(Math.Sqrt(analysParameter.Count)) + 1;
            Dictionary<int, string> newParameters = new Dictionary<int, string>();


            for (int forestsCount = 0; forestsCount < numberOfForest; )
            {
                DM.DecisionTree.RandomForest random = new RandomForest();
                List<int> parametersName = new List<int>();
                foreach (KeyValuePair<int, string> keyValuePair in analysParameter)
                {
                    if (!keyValuePair.Value.StartsWith("Def"))
                        parametersName.Add(keyValuePair.Key);

                }
                for (int i = 0; i < restrictCountOfParameters; i++)
                {
                    int j = rand.Next(0, analysParameter.Count);
                    if (!newParameters.ContainsKey(parametersName[j]))
                        newParameters.Add(parametersName[j], analysParameter[parametersName[j]]);
                }
                List<OneRow> trainData = new List<OneRow>();
                trainData = GetRandomData(TrainData.Train, newParameters, parameterID);
                _root = random.MountTree(trainData, newParameters, parameterID, Convert.ToInt32(textEdit1.Value) + 1);
                if (_root.attributeName != "False")
                {
                    RandForests.Add(_root);
                    forestsCount++;
                }
                newParameters.Clear();
            }
        }
        private List<OneRow> GetRandomData(List<OneRow> samples, Dictionary<int, string> parameterNames,
            int defectParameterID)
        {
            List<OneRow> trainData = new List<OneRow>();
            int countOfRandomData = GetCount(samples.Count);
            List<int> trainItemID = new List<int>();
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < countOfRandomData; )
            {
                int j = rand.Next(0, samples.Count);
                while (!trainItemID.Contains(j))
                {
                    trainItemID.Add(j);
                    i++;
                }

            }
            trainItemID.Sort();
            foreach (var row in trainItemID)
            {
                OneRow findRowWithParameters = new OneRow();
                findRowWithParameters.Date = samples[row].Date;
                findRowWithParameters.Output[defectParameterID] = samples[row].Output[defectParameterID];
                findRowWithParameters.OutputBool[defectParameterID] = samples[row].OutputBool[defectParameterID];
                foreach (KeyValuePair<int, string> parameterName in parameterNames)
                {
                    findRowWithParameters.Input[parameterName.Key] = samples[row].Input[parameterName.Key];
                }
                trainData.Add(findRowWithParameters);
            }
            return trainData;
        }

        private int GetCount(int rows)
        {
            double count = (double)rows;
            double countRows = rows * Math.Pow(1 - (1 / count), rows);
            return Convert.ToInt32(countRows);
        }

        private void histogramm_Click(object sender, EventArgs e)
        {
            try
            {
                if (RandForests == null)
                {
                    ListRandomForests rndForests2 =
                        SaveXML.DeserializeObjectRandomForests(Directory.GetCurrentDirectory() + @"\" + "output2.txt");
                    RandForests = rndForests2.RndForests;
                }
                _root = rndForests[0];
                Diagramm diag = new Diagramm(RandForests);
                diag.Show();
            }
            catch(FileNotFoundException )
            {
                MessageBox.Show(Localization.MyStrings.FileNotFoundMathematical);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        
    }
}
