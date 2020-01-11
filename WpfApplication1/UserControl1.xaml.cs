using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GraphSharp.Controls;
using System.ComponentModel;
using System.Linq;
using DataMiningOffLine;
using DM.Data;
using DM.DecisionTree;

namespace DM.WPF_Tree
{
    public class PocGraphLayout : GraphLayout<PocVertex, PocEdge, PocGraph> { }
    /// <summary>
    /// Логика инициализации и set'ов
    /// </summary>
    public partial class MyControl1 : UserControl
    {
        public object tree;
        public string lang;
        public MyControl1(object test_value, object test_item, object language)
        {
            InitializeComponent();
            lang = language.ToString();
            GraphControl vm = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.test_value = (TreeNode)test_value;
            if (test_value == null)
            {
                vm = new GraphControl(v_neuro, 4, test_value, "", test_item,lang);

            }
            else if (test_item != null)
            {
                this.test_item = (OneRow)test_item;
                tree = (TreeNode)test_value;
                vm = new GraphControl(v_neuro, 3, test_value, "", test_item, lang);
            }
            else
            {
                tree = (TreeNode)test_value;
                vm = new GraphControl(v_neuro, 2, test_value, "", test_item, lang);
            }
            this.DataContext = vm;
        }
        public void Init(object sender, EventArgs e)
        {
        }

        //классы-переключатели веток
        public bool v_neuro = true;
        public bool graph_neuro
        {
            set { v_neuro = value; }
        }

        public bool branche = false;
        public bool branch_change
        {
            set { branche = value; }
        }
        //передача дерева
        TreeNode test_value;
        public DM.DecisionTree.TreeNode test_value_teleport
        {
            set { test_value = value; }
        }
        OneRow test_item;
        public OneRow test_item_teleport
        {
            set { test_item = value; }
        }

        /// <summary>
        /// Fullscreen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyEvent(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                MyControlEventArgs ret = new MyControlEventArgs(true);
                KeyDown(this, ret);
            }
        }
        public void MouseEnter(object sender, MouseEventArgs e)
        {
            tabControl1.Opacity = 1;
        }
        public void MouseLeave(object sender, MouseEventArgs e)
        { tabControl1.Opacity = 0.1; }

        public delegate void MyControlEventHandler(object sender, MyControlEventArgs args);
        public event MyControlEventHandler KeyDown;

        #region Checked
        public void Short_checked(object sender, RoutedEventArgs e)
        {
            this.test_value = (DM.DecisionTree.TreeNode)tree;
            GraphControl vm = new GraphControl(false, 2, test_value, "", test_item,lang);

            this.DataContext = vm;
            InitializeComponent();
        }

        public void Full_checked(object sender, RoutedEventArgs e)
        {
            this.test_value = (DM.DecisionTree.TreeNode)tree;
            GraphControl vm = new GraphControl(true, 1, test_value, "", test_item, lang);
            this.DataContext = vm;
            InitializeComponent();
        }

        public void True_only(object sender, RoutedEventArgs e)
        {
            GraphControl vm;
            this.test_value = (DM.DecisionTree.TreeNode)tree;
            if (radioButton1.IsChecked == true)
            { vm = new GraphControl(true, 1, test_value, "False", test_item, lang); }
            else
            { vm = new GraphControl(false, 2, test_value, "False", test_item, lang); }

            this.DataContext = vm;
            InitializeComponent();
        }

        public void False_only(object sender, RoutedEventArgs e)
        {
            GraphControl vm;
            this.test_value = (DM.DecisionTree.TreeNode)tree;
            if (radioButton1.IsChecked == true)
            { vm = new GraphControl(true, 1, test_value, "True", test_item, lang); }
            else
            { vm = new GraphControl(false, 2, test_value, "True", test_item, lang); }
            this.DataContext = vm;
            InitializeComponent();
        }
        public void All(object sender, RoutedEventArgs e)
        {
            GraphControl vm;
            this.test_value = (DM.DecisionTree.TreeNode)tree;
            if (radioButton1.IsChecked == true)
            { vm = new GraphControl(true, 1, test_value, "", test_item, lang); }
            else
            { vm = new GraphControl(false, 2, test_value, "", test_item, lang); }
            this.DataContext = vm;
            InitializeComponent();
        }
        #endregion
    }


    /// <summary>
    /// Логика работы с фулскрином
    /// </summary>
    public class MyControlEventArgs : EventArgs
    {

        private bool _IsOK = false;

        public MyControlEventArgs(bool result)
        {
            _IsOK = result;
        }

        public bool IsOK
        {
            get { return _IsOK; }
            set { _IsOK = value; }
        }
    }
    /// <summary>
    /// Логика работы с графом
    /// </summary>
    public partial class GraphControl : INotifyPropertyChanged
    {
        private PocGraph graph;

        private List<String> layoutAlgorithmTypes = new List<string>();
        private string language;

        public GraphControl(bool v_neuro, int caseNumb, object test_value, string groupParam, object test_item,string language)
        {
            this.language = language;
            TreeNode test_tree = (TreeNode)test_value;
            Graph = new PocGraph(true);
            List<IndexVertex> currentVerices = new List<IndexVertex>();

            switch (caseNumb)
            {
                case 1:
                    {
                        #region Full_tree+group
                        currentVerices.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language), -1, null, Brushes.Yellow));
                        // PrintNode(test_tree, currentVerices, 0);

                        PrintNode_group(test_tree, currentVerices, 0, groupParam);
                        foreach (IndexVertex vertex in currentVerices)
                            Graph.AddVertex(vertex.vertex);
                        for (int i = 1; i < currentVerices.Count; i++)
                        {
                            AddNewGraphEdge(currentVerices[currentVerices[i].index].vertex, currentVerices[i].vertex, currentVerices[i].d1);
                        }

                        break;
                        #endregion
                    }
                case 2:
                    {
                        #region Short+group
                        currentVerices.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language) + "\n" + test_tree.Positive + "|" + test_tree.Negative, -1, null, Brushes.Brown));
                        Shortest_TreeGroup(test_tree, currentVerices, 0, groupParam);


                        foreach (IndexVertex vertex in currentVerices)
                            Graph.AddVertex(vertex.vertex);
                        for (int i = 1; i < currentVerices.Count; i++)
                        {
                            AddNewGraphEdge(currentVerices[currentVerices[i].index].vertex, currentVerices[i].vertex, currentVerices[i].d1);
                        }
                        break;
                        #endregion
                    }
                case 3:
                    {
                        #region podsvetka
                        if (Graph != null)
                            Graph.Clear();
                        currentVerices.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language), -1, null, Brushes.Yellow));

                        // PrintNode(test_tree, currentVerices, 0);

                        OneRow test_items = (OneRow)test_item;

                        PrintNode_Color(test_tree, currentVerices, 0, groupParam, test_items, 0);

                        foreach (IndexVertex vertex in currentVerices)
                            Graph.AddVertex(vertex.vertex);

                        for (int i = 1; i < currentVerices.Count; i++)
                        {
                            AddNewGraphEdge(currentVerices[currentVerices[i].index].vertex, currentVerices[i].vertex, currentVerices[i].d1);
                        }
                        break;
                        #endregion
                    }
                case 4:
                    {
                        if (Graph != null)
                            Graph.Clear();
                        Tuple<decimal, decimal> d1 = new Tuple<decimal, decimal>(1.0m, 1.0m);
                        currentVerices.Add(new IndexVertex("Настройки OCS неверны?", 0, d1, Brushes.White));
                        currentVerices.Add(new IndexVertex("Слишком высокая температура смеси?", 0, d1, Brushes.White));
                        currentVerices.Add(new IndexVertex("Неправильная форма расплава?", 1, d1, Brushes.White));
                        currentVerices.Add(new IndexVertex("Выглядит блестяще-черными/красно коричневыми?", 2, d1, Brushes.White));
                        currentVerices.Add(new IndexVertex("Точки выглядят матово-черными?", 3, d1, Brushes.White));
                        currentVerices.Add(new IndexVertex("Черные точки по краям?", 4, d1, Brushes.White));
                        currentVerices.Add(new IndexVertex("Сделать запись QRQC", 5, d1, Brushes.Green));
                        currentVerices.Add(new IndexVertex("Сообщить мастеру", 6, d1, Brushes.Orange));
                        currentVerices.Add(new IndexVertex("Настроить OCS", 0, d1, Brushes.Green));
                        currentVerices.Add(new IndexVertex("Снизить температуру смеси в горячем смесителе или время пребывания в горячем смесителе", 1, d1, Brushes.Green));
                        currentVerices.Add(new IndexVertex("Расплав должен вытекать наружу, подогнать контризгиб. Следить за профилем.", 2, d1, Brushes.Green));
                        currentVerices.Add(new IndexVertex("Снижать скорость пока пленка не станет чистой", 3, d1, Brushes.Green));
                        currentVerices.Add(new IndexVertex("Увеличить стабилизатор не больше допустимого (1.1%)", 11, d1, Brushes.Green));
                        currentVerices.Add(new IndexVertex("Остановить линию", 4, d1, Brushes.Red));
                        currentVerices.Add(new IndexVertex("Почистить нож, головку и корпус кнеттера", 13, d1, Brushes.Green));
                        currentVerices.Add(new IndexVertex("Остановить линию", 5, d1, Brushes.Red));
                        currentVerices.Add(new IndexVertex("Почистить щетки запорные кольца", 15, d1, Brushes.Green));
                        foreach (IndexVertex vertex in currentVerices)
                            Graph.AddVertex(vertex.vertex);
                        for (int i = 1; i < currentVerices.Count; i++)
                        {
                            AddNewGraphEdge(currentVerices[currentVerices[i].index].vertex, currentVerices[i].vertex, currentVerices[i].d1);
                        }
                        AddNewGraphEdge(currentVerices[8].vertex, currentVerices[6].vertex, d1);
                        AddNewGraphEdge(currentVerices[9].vertex, currentVerices[6].vertex, d1);
                        AddNewGraphEdge(currentVerices[10].vertex, currentVerices[6].vertex, d1);
                        AddNewGraphEdge(currentVerices[12].vertex, currentVerices[6].vertex, d1);
                        AddNewGraphEdge(currentVerices[14].vertex, currentVerices[6].vertex, d1);
                        AddNewGraphEdge(currentVerices[16].vertex, currentVerices[6].vertex, d1);
                        break;
                    }
            }


        }
        #region Print Node
        private void PrintNode(DM.DecisionTree.TreeNode test_tree, List<IndexVertex> vertex, int index)
        {
            if (test_tree.attribute.values != null)
            {
                for (int i = 0; i < test_tree.attribute.values.Count; i++)
                {
                    TreeNode childNode = test_tree.getChildByBranchName(test_tree.attribute.values[i]);
                    vertex.Add(new IndexVertex(childNode.attributeName + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.DarkGreen));
                    int number = vertex.Count;
                    PrintNode(childNode, vertex, number - 1);
                }
            }

        }

        private void PrintNode_group(DM.DecisionTree.TreeNode test_tree, List<IndexVertex> vertex, int index, string groupParamName)
        {
            if (test_tree.attribute.values != null)
            {
                for (int i = 0; i < test_tree.attribute.values.Count; i++)
                {
                    TreeNode childNode = test_tree.getChildByBranchName(test_tree.attribute.values[i]);
                    if (childNode.attributeName != groupParamName)
                    {
                        vertex.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language) + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.DarkOrange));
                        int number = vertex.Count;
                        PrintNode_group(childNode, vertex, number - 1, groupParamName);
                    }

                }
            }

        }
        int current_deep = 0;
        decimal newValue;
        private void PrintNode_Color(TreeNode test_tree, List<IndexVertex> vertex, int index, string groupParamName, OneRow test_item, int deep)
        {

            if (test_tree.attribute.values != null)
            {

                for (int i = 0; i < test_tree.attribute.values.Count; i++)
                {
                    if (test_tree.attribute.AttributeName != -1)
                        newValue = test_item.Input[test_tree.attribute.AttributeName];
                    if ((newValue >= test_tree.attribute.values[i].Item1) && (newValue <= test_tree.attribute.values[i].Item2) && (deep == current_deep))
                    {
                        TreeNode childNode = test_tree.getChildByBranchName(test_tree.attribute.values[i]);
                        if (childNode.attributeName != groupParamName)
                        {
                            vertex.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language) + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.Yellow));
                            int number = vertex.Count;
                            current_deep++;
                            PrintNode_Color(childNode, vertex, number - 1, groupParamName, test_item, deep + 1);
                            current_deep--;
                        }
                    }
                    else
                    {
                        TreeNode childNode = test_tree.getChildByBranchName(test_tree.attribute.values[i]);
                        if (childNode.attributeName != groupParamName)
                        {
                            vertex.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language) + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.White));
                            int number = vertex.Count;
                            current_deep++;
                            PrintNode_Color(childNode, vertex, number - 1, groupParamName, test_item, deep);
                            current_deep--;
                        }
                    }

                }
            }

        }


        private void Shortest_Tree(TreeNode test_tree, List<IndexVertex> vertex, int index)
        {
            int numberTrue = 0;
            int numberFalse = 0;
            if (test_tree.attribute.values != null)
            {
                for (int i = 0; i < test_tree.attribute.values.Count; i++)
                {

                    TreeNode childNode = test_tree.getChildByBranchName(test_tree.attribute.values[i]);

                    if (childNode.attributeName == "False")
                        numberFalse++;
                    else if (childNode.attributeName == "True")
                        numberTrue++;
                    else
                    {
                        vertex.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language) + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.DarkRed));
                        int number = vertex.Count;
                        Shortest_Tree(childNode, vertex, number - 1);
                    }
                    if ((numberTrue <= 1) && (numberFalse <= 1))
                    {
                        vertex.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language) + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.DeepPink));
                        int number = vertex.Count;
                        Shortest_Tree(childNode, vertex, number - 1);
                    }
                }
            }
        }
        private void Shortest_TreeGroup(TreeNode test_tree, List<IndexVertex> vertex, int index, string groupParamName)
        {
            int numberTrue = 0;
            int numberFalse = 0;
            if (test_tree.attribute.values != null)
            {
                for (int i = 0; i < test_tree.attribute.values.Count; i++)
                {

                    TreeNode childNode = test_tree.getChildByBranchName(test_tree.attribute.values[i]);
                    if (childNode.attributeName != groupParamName)
                    {
                        if (childNode.attributeName == "False")
                            numberFalse++;
                        else if (childNode.attributeName == "True")
                            numberTrue++;
                        else
                        {
                            vertex.Add(new IndexVertex(XMLWork.FindNameWithScada(test_tree.attributeName, language) + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.Gold));
                            int number = vertex.Count;
                            Shortest_TreeGroup(childNode, vertex, number - 1, groupParamName);
                        }
                        if ((numberTrue == 1) && (childNode.attributeName == "True"))
                        {
                            vertex.Add(new IndexVertex(childNode.attributeName + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.Goldenrod));
                            int number = vertex.Count;
                            Shortest_TreeGroup(childNode, vertex, number - 1, groupParamName);
                        }
                        else if ((numberFalse == 1) && (childNode.attributeName == "False"))
                        {
                            vertex.Add(new IndexVertex(childNode.attributeName + "\n" + childNode.Positive + "|" + childNode.Negative, index, test_tree.attribute.values[i], Brushes.Lime));
                            int number = vertex.Count;
                            Shortest_TreeGroup(childNode, vertex, number - 1, groupParamName);
                        }
                    }
                }
            }
        }
        #endregion

        private PocEdge AddNewGraphEdge(PocVertex from, PocVertex to, Tuple<decimal, decimal> d1)
        {
            string edgeString = string.Format("({1} - {2})", from.ID, Math.Round(d1.Item1, 2), Math.Round(d1.Item2, 2));
            Brush edgeColor;
            if (from.IsBrush == Brushes.Yellow && to.IsBrush == Brushes.Yellow)
            { edgeColor = Brushes.Yellow; }
            else { edgeColor = Brushes.Gray; }
            PocEdge newEdge = new PocEdge(edgeString, from, to, edgeColor);
            Graph.AddEdge(newEdge);
            return newEdge;
        }
        public PocGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                NotifyPropertyChanged("Graph");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }    
}
