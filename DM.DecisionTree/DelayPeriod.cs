using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.DecisionTree
{
    public class DelayPeriod
    {
        private TimeSpan _delayPeriod = new TimeSpan();

        public TimeSpan DelayPeriods
        {
            get { return _delayPeriod; }
            set { _delayPeriod = value; }
        }

        public DelayPeriod()
        {
            _delayPeriod = new TimeSpan(0, 5, 0);
        }
    }
}
