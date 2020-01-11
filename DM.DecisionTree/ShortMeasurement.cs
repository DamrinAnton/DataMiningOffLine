using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.DecisionTree
{
    public class ShortMeasurement
    {
        private decimal _value;
        private int _parameterID;
        private string _parameter;

        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int ParameterId
        {
            get { return _parameterID; }
            set { _parameterID = value; }
        }

        public string Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        public ShortMeasurement(int parameterID, decimal value)
        {
            this.Value = value;
            this.ParameterId = parameterID;
        }

        public ShortMeasurement(string parameter, decimal value)
        {
            this.Value = value;
            this.Parameter = parameter;
        }
    }
}
