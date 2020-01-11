using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    class ListOfElements
    {
        private List<FisherCritery> _fisher = new List<FisherCritery>();

        public List<FisherCritery> Fisher
        {
            get { return _fisher; }
            set { _fisher = value; }
        }

        public ListOfElements(List<FisherCritery> fisherCriteries)
        {
            Fisher = fisherCriteries;
        }

        public List<FisherCritery> InsertionSort()
        {
            for (int i = 1; i < Fisher.Count; i++)
            {
                FisherCritery key = Fisher[i];
                int j = i - 1;
                while (j >= 0 && Fisher[j].Value < key.Value)
                {
                    Fisher[j + 1] = Fisher[j];
                    j--;
                }
                Fisher[j + 1] = key;
            }
            return Fisher;
        }
    }
}
