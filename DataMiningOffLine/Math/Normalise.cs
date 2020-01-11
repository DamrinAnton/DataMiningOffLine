using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    public static class Normalisation
    {
        public static decimal StandardNormalise(decimal value, decimal min, decimal max)
        {
            return Math.Round(((value - min) / max -min),5);
        }


        public static decimal NormaliseWithDeviation (decimal value,  decimal mean, decimal deviation )
        {
            double sqrtDeviation = Math.Sqrt(Convert.ToDouble(deviation));
            double submit = Convert.ToDouble(value - mean);
            double parenthesisCalculate = (submit / sqrtDeviation);
            double calculate = 1.0 / (1 + Math.Exp(-parenthesisCalculate));
            return Convert.ToDecimal(calculate);
        }
    }
}
