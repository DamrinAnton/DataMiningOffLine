using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.DecisionTree
{
    [Serializable]
    public class TupleList<T1, T2> : List<Tuple<T1, T2>>
    {
        #region Methods
        public void Add(T1 item, T2 item2)
        {
            Add(new Tuple<T1, T2>(item, item2));
        }
        #endregion
    }
}
