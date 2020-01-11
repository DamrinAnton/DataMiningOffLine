using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningOffLine.DBClasses
{
    class Recomend
    {
        public long reason_id { get; private set; }
        public int number_in { get; private set; }
        public string text_st { get; private set; }
        public string param_st { get; private set; }
        public string place_st { get; private set; }

        public Recomend(long reason_id, int number_in, string text_st, string param_st, string place_st) {
            this.reason_id = reason_id;
            this.number_in = number_in;
            this.text_st = text_st;
            this.param_st = param_st;
            this.place_st = place_st;
        }
    }
}
