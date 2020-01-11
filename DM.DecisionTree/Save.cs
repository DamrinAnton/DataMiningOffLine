using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.DecisionTree
{
    public class Save
    {
        private int measurementNumber;
        private TreeNode tree;
        private ListRandomForests rndForests;
        private decimal criterion;
        private int order;
        private string descript;
        private int parID;

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public string Description
        {
            get { return descript; }
            set { descript = value; }
        }

        public int ParID
        {
            get { return parID; }
            set { parID = value; }
        }
        public int Measure
        {
            get { return measurementNumber; }
            set { measurementNumber = value; }
        }

        public TreeNode Tree
        {
            get { return tree; }
            set { tree = value; }
        }

        public ListRandomForests RandomForests
        {
            get { return rndForests; }
            set { rndForests = value; }
        }

        public decimal Critery
        {
            get { return criterion; }
            set { criterion = value; }
        }
    }
}
