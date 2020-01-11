using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine.Forms
{
    public class ProductionTime
    {
        private decimal calenderVelocity;

        public decimal CalenderVelocity
        {
            get { return calenderVelocity; } 
            set { calenderVelocity = value; }
        }

        private decimal takeOffVelocity;

        public decimal TakeOffVelocity
        {
            get { return takeOffVelocity; }
            set { takeOffVelocity = value; }
        }

        private decimal temperingVelocity;

        public decimal TemperingVelocity
        {
            get { return temperingVelocity; }
            set { temperingVelocity = value; }
        }

        private static int extruderTime;

        public static int ExtruderTime
        {
            get { return extruderTime; }
        }

        private static int calenderTime;

        public static int CalenderTime
        {
            get { return calenderTime; }
        }

        private static int takeOffTime;

        public static int TakeOffTime
        {
            get { return takeOffTime; }
        }

        private static int temperingTime;

        public static int TemperingTime
        {
            get { return temperingTime; }
        }

        public ProductionTime(decimal cVelocity, decimal tVelocity, decimal tempVelocity)
        {
            calenderVelocity = cVelocity;
            takeOffVelocity = tVelocity;
            temperingVelocity = tempVelocity;
        }

        public void SetTempTime(decimal time)
        {
            temperingTime = (int) time;
        }
        public void SetTakeOffTime(decimal time)
        {
            takeOffTime = (int)time;
        }

        public void SetCalenderTime(decimal time)
        {
            calenderTime = (int)time;
        }

        public void SetExtruderTime(int time)
        {
            extruderTime = time + 183;
        }
    }
    }

