using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DM.DecisionTree
{
    /// <summary>
    /// Класс представляющий дерево решений;
    /// </summary>
    [Serializable]
    public class TreeNode : ISerializable
    {

        #region Properties
        private List<TreeNode> mChilds = null;
        private Attribute mAttribute;
        public string attributeName;
        private int _positive;
        private int _negative;
        private int _depth;

        #endregion
        public List<TreeNode> MChilds
        {
            get { return this.mChilds; }
            set { this.mChilds = value; }
        }

        public int Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }

        public int Positive
        {
            get { return _positive; }
            set { _positive = value; }
        }

        public int Negative
        {
            get { return _negative; }
            set { _negative = value; }
        }

        #region Constructors
        public TreeNode(SerializationInfo sInfo, StreamingContext contextArg)
        {
            this.mChilds = (List<TreeNode>)sInfo.GetValue("mChilds", typeof(List<TreeNode>));
            this.mAttribute = (Attribute)sInfo.GetValue("mAttribute", typeof(Attribute));
            this.attributeName = (string)sInfo.GetValue("attributeName", typeof(string));
            this.Positive = (int)sInfo.GetValue("positive", typeof(int));
            this.Negative = (int)sInfo.GetValue("negative", typeof(int));
            this.Depth = (int)sInfo.GetValue("depth", typeof(int));
        }

        /// <summary>
        /// Инициализация нового TreeNode
        /// </summary>
        /// <param name="attribute">Атрибут для которого создается узел</param>
        public TreeNode(Attribute attribute, Dictionary<int, string> parameterName, int positive, int negative, int depth)
        {
            if (attribute == null)
            {

                System.Windows.Forms.MessageBox.Show("Параметров слишком мало для анализа! Добавьте новые!");
            }
            else if (attribute.values != null)
            {
                mChilds = new List<TreeNode>(attribute.values.Count);
                for (int i = 0; i < attribute.values.Count; i++)
                {
                    mChilds.Add(null);
                }
                attributeName = parameterName[attribute.AttributeName];
            }
            else
            {
                mChilds = new List<TreeNode>(1);
                mChilds.Add(null);
                attributeName = attribute.LabelName.ToString();
            }

            mAttribute = attribute;
            _positive = positive;
            _negative = negative;
            _depth = depth;
        }

        public TreeNode() { }
        #endregion

        #region Function
        public void GetObjectData(SerializationInfo sInfo, StreamingContext contextArg)
        {
            sInfo.AddValue("mChilds", this.mChilds);
            sInfo.AddValue("mAttribute", this.mAttribute);
            sInfo.AddValue("attributeName", this.attributeName);
            sInfo.AddValue("positive", this.Positive);
            sInfo.AddValue("negative", this.Negative);
            sInfo.AddValue("depth", this.Depth);
        }

        /// <summary>
        /// Атрибут
        /// </summary>
        public Attribute attribute
        {
            get
            {
                return mAttribute;
            }
        }

        /// <summary>
        /// Возвращает количество детей
        /// </summary>
        public int totalChilds
        {
            get
            {
                return mChilds.Count;
            }
        }
        /// <summary>
        /// Добавляет дочерний TreeNode в этом узле  по имени Tuple ValueName
        /// </summary>
        /// <param name="treeNode">TreeNode ребенок должен быть добавлен</param>
        /// <param name="ValueName">пороговые значения</param>
        public void AddTreeNode(TreeNode treeNode, Tuple<decimal, decimal> ValueName)
        {
            int index = mAttribute.IndexValue(ValueName);
            mChilds[index] = treeNode;
        }


        public TreeNode getChildByBranchName(Tuple<decimal, decimal> branchName)
        {
            int index = mAttribute.IndexValue(branchName);
            return (TreeNode)mChilds[index];
        }
        #endregion

    }
}
