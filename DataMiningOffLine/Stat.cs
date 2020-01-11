using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    public class Stat : IComparable<Stat>
    {
        private decimal min;
        private decimal max;
        private decimal mean;

        public decimal Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
            }
        }

        public decimal Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
            }
        }

        public decimal Mean
        {
            get
            {
                return mean;
            }
            set
            {
                mean = value;
            }
        }

        public Stat(decimal inputMin, decimal inputMax, decimal inputMean)
        {
            Min = inputMin;
            Max = inputMax;
            Mean = inputMean;
        }

        public int CompareTo(Stat comparePoint)
        {
            return this.mean.CompareTo(comparePoint.Mean);
        }
    }
}
