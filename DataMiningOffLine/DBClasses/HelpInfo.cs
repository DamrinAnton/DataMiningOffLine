using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningOffLine.DBClasses
{
    class HelpInfo
    {
        public string Esnumber { get; private set; }
        public string Reason_text { get; private set; }

        public HelpInfo(string esnumber, string reason_text)
        {
            Esnumber = esnumber;
            Reason_text = reason_text;
        }
    }
}
