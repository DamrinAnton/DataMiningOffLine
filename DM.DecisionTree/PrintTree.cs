using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.DecisionTree
{
    public class PrintTree
    {
        #region Properties
        int n;
        static List<int> data = new List<int>();
        #endregion

        #region Methods
        public static List<int> PrintNode(TreeNode root)
        {
            if (root.attribute.AttributeName != -1)
                data.Add(root.attribute.AttributeName);
            else if ((bool)root.attribute.LabelName == true)
                data.Add(1);
            else
                data.Add(0);


            if (root.attribute.values != null)
            {
                for (int i = 0; i < root.attribute.values.Count; i++)
                {
                    TreeNode childNode = root.getChildByBranchName(root.attribute.values[i]);
                    PrintNode(childNode);
                }
            }
            return data;
        }
        #endregion
    }
}
