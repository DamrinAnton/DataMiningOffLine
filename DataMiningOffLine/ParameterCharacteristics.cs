using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    public class ParameterCharacteristics
    {
        private string _parameterName;

        public string ParameterName
        {
            get { return _parameterName; }
            set { _parameterName = value; }
        }

        private decimal _min;

        public decimal Min
        {
            get { return _min; }
            set { _min = value; }
        }

        private decimal _max;

        public decimal Max
        {
            get { return _max; }
            set { _max = value; }
        }

        private decimal _mean;

        public decimal Mean
        {
            get { return _mean; }
            set { _mean = value; }
        }

        private decimal _deviation;

        public decimal Deviation
        {
            get { return _deviation; }
            set { _deviation = value; }
        }

        private decimal _sum;

        public decimal Sum
        {
            get { return _sum; }
            set { _sum = value; }
        }

        public ParameterCharacteristics(string name, decimal min, decimal max, decimal mean, decimal sum, decimal deviation)
        {
            ParameterName = name;
            Min = min;
            Max = max;
            Mean = mean;
            Sum = sum;
            Deviation = deviation;
            
        }
    }
}
