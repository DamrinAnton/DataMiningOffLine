using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DM.DecisionTree
{
    [Serializable]
    public class ListRandomForests : ISerializable
    {

        public List<TreeNode> rndForests;

        public List<TreeNode> RndForests
        {
            get { return rndForests; }
            set { rndForests = value; }
        }

        public ListRandomForests()
        { }

        public ListRandomForests(SerializationInfo sInfo, StreamingContext contextArg)
        {
            this.rndForests = (List<TreeNode>)sInfo.GetValue("rndForests", typeof(List<TreeNode>));
        }

        public void GetObjectData(SerializationInfo sInfo, StreamingContext contextArg)
        {
            sInfo.AddValue("rndForests", this.rndForests);
        }

    }
}
