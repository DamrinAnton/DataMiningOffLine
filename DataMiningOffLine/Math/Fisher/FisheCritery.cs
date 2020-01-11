using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    public class FisherCritery
    {
        private int _key;
        private decimal _value;

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public FisherCritery(int key, decimal value)
        {
            Key = key;
            Value = value;
        }
    }
}
