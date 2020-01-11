using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    public class Measurements
    {
        private decimal _value;

        private DateTime _timeStamp;

        private string parName;

        private int parameterID;

        public int ParID
        {
            get { return parameterID; }
            set { parameterID = value; }
        }
        

        public string ParameterName
        {
            get { return parName; }
            set { parName = value; }
        }


        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }


        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Measurements(decimal value, DateTime timeStamp, string par, int parID)
        {
            _value = value;
            _timeStamp = timeStamp;
            ParameterName = par;
            ParID = parID;
        }

    }
}
