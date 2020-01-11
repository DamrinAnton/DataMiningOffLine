using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.Data;

namespace DM.DecisionTree
{
    class CheckTree
    {

        #region Constructors
        public CheckTree()
        {
        }
        #endregion

        #region Function
        public void Tree(TreeNode root, OneRow item, ref int positive)
        {
            if (root.attribute.values != null)
            {
                for (int i = 0; i < root.attribute.values.Count; i++)
                {
                    if ((root.attribute.values[i].Item1 <= item.Input[root.attribute.AttributeName]) && ((root.attribute.values[i].Item2) >= item.Input[root.attribute.AttributeName]))
                    {
                        TreeNode childNode = root.getChildByBranchName(root.attribute.values[i]);
                        Tree(childNode, item, ref positive);
                    }

                }
            }
            else if (root.attribute.values == null)
            {
                if ((bool)root.attribute.LabelName == item.OutputBool[0])
                    positive = positive + 1;
            }
        }


        public void TreeWithAddParameter(TreeNode root, OneRow item, List<string> parameters)
        {
            try
            {
                if (root.attribute.values != null)
                {
                    for (int i = 0; i < root.attribute.values.Count; i++)
                    {
                        if ((root.attribute.values[i].Item1 <= item.Input[root.attribute.AttributeName]) &&
                            ((root.attribute.values[i].Item2) >= item.Input[root.attribute.AttributeName]))
                        {
                            parameters.Add(root.attributeName);
                            TreeNode childNode = root.getChildByBranchName(root.attribute.values[i]);
                            TreeWithAddParameter(childNode, item, parameters);
                        }

                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public void NewTree(TreeNode root, OneRow item, Dictionary<string, Tuple<decimal, decimal>> l)
        {
            if (root.attribute.values != null)
            {
                for (int i = 0; i < root.attribute.values.Count; i++)
                {
                    if ((root.attribute.values[i].Item1 <= item.Input[root.attribute.AttributeName]) && ((root.attribute.values[i].Item2) >= item.Input[root.attribute.AttributeName]))
                    {
                        var borderValues = new Tuple<decimal, decimal>(root.attribute.values[i].Item1, root.attribute.values[i].Item2);
                        l.Add(root.attributeName, borderValues);
                        TreeNode childNode = root.getChildByBranchName(root.attribute.values[i]);
                        NewTree(childNode, item, l);
                    }

                }
            }
        }
        #endregion
    }
}
