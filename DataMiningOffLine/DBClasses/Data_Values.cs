using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningOffLine.DBClasses
{
    public class Data_Values
    {
        public string data { get; private set; }
        public string value { get; private set; }

        public Data_Values(string data, string value) {
            this.data = data;
            this.value = value;
        }
    }
}
