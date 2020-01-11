using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataMiningOffLine
{
    static class Calculation
    {
        public static string stringName = null;
        static ComponentResourceManager manager = new ComponentResourceManager(typeof(NormalDistribution));

        //12333
        public static void DoStuff(List<KeyValuePair<DateTime, double>> data, int columnsCount,
            out List<KeyValuePair<double, double>> line, out List<KeyValuePair<string, double>> columns,
            out double expectedValue, out double standartDeviation, out double criterionHi, out double criticalCriterionHi, out double criterionKS, out double criticalCriterionKS, out double criterionF, out double criticalCriterionF, string parametrName, Option options)
        {
            stringName = parametrName;
            line = new List<KeyValuePair<double, double>>();
            columns = new List<KeyValuePair<string, double>>();
            double paramMin = data.Min(item => item.Value);
            double paramMax = data.Max(item => item.Value);
            double delta = (paramMax - paramMin) / columnsCount;

            // Debug.WriteLine(delta);
            // Debug.WriteLine(data.Count);
            for (int i = 0; i < columnsCount; i++)
            {
                int itemsInColumn = data.Count(item => item.Value >= paramMin + delta * i && item.Value < paramMin + delta * (i + 1));
                columns.Add(new KeyValuePair<string, double>($"{Math.Round(paramMin + delta * i, 2)} - {Math.Round(paramMin + delta * (i + 1), 2)}", (double)itemsInColumn / (data.Count * delta)));
            }

            int itemsInLastColumn = data.Count(item => item.Value == paramMax);
            //Исправить добавление максиимума
            columns[columns.Count - 1] = new KeyValuePair<string, double>(columns[columns.Count - 1].Key, (columns[columns.Count - 1].Value + itemsInLastColumn) / (data.Count * delta));
            //
            expectedValue = data.Sum(item => item.Value) / data.Count;
            double exp = expectedValue;
            standartDeviation = Math.Sqrt(data.Sum(item => Math.Pow(item.Value - exp, 2)) / (data.Count - 1));
            double stand = standartDeviation;
            //columns.Select(item => new KeyValuePair<string, double>(item.Key, item.Value / data.Count));
            Func<double, double> normalDistribution = x => Math.Exp(-Math.Pow(x - exp, 2) / (2 * stand * stand)) / (Math.Sqrt(2 * Math.PI) * stand);
            //Магическая 500
            double step = (paramMax - paramMin) / data.Count();
            //
            if (paramMin == paramMax)
                throw new Exception("Incorrect data!");
            for (double i = paramMin; i <= paramMax; i += step)
                line.Add(new KeyValuePair<double, double>(i, normalDistribution(i)));
            if (options.critHi)
                CalculatePirsonCriteria(data, columns, standartDeviation, expectedValue, delta, out criterionHi, out criticalCriterionHi, options.singLevel);
            else
            {
                criterionHi = double.PositiveInfinity;
                criticalCriterionHi = double.PositiveInfinity;
            }
            if (options.critKS)
                CalculateKolmogorovSmirnovCriterion(data, standartDeviation, expectedValue, out criterionKS, out criticalCriterionKS, options.singLevel);
            else
            {
                criterionKS = double.PositiveInfinity;
                criticalCriterionKS = double.PositiveInfinity;
            }
            if (options.critFrocini)
                CalculareFrociniCriterion(data, standartDeviation, expectedValue, out criterionF, out criticalCriterionF, options.singLevel);
            else
            {
                criterionF = double.PositiveInfinity;
                criticalCriterionF = double.PositiveInfinity;
            }
        }

        public static void CalculateKolmogorovSmirnovCriterion(List<KeyValuePair<DateTime, double>> data, double standartDeviation, double expectedValue, out double criterion, out double criticalCriterion, string signLevel)
        {
            standartDeviation = Math.Sqrt(data.Sum(item => Math.Pow(item.Value - expectedValue, 2)) / (data.Count));
            var z = (from item in data
                     select (item.Value - expectedValue) / standartDeviation);
            DataBaseND db = new DataBaseND() { nameDB = "DBCriterionND.db" };
            db.ConnectionBD();
            int id_laplas = 2;
            Dictionary<double, double> laplasValues = db.CriterionValue("0", id_laplas);
            db.CloseConnectionDB();
            var f = (from item in z
                     select (0.5 + FindLaplFunctionValue(laplasValues, item))).ToList();
            List<double> d_plus = new List<double>();
            List<double> d_minus = new List<double>();
            for (int index = 1; index <= f.Count(); index++)
            {
                d_plus.Add(Convert.ToDouble(index) / Convert.ToDouble(f.Count()) - f[index - 1]);
                d_minus.Add(f[index - 1] - Convert.ToDouble((index - 1)) / Convert.ToDouble(f.Count()));
            }
            double d_n;
            if (d_plus.Max() > d_minus.Max())
                d_n = d_plus.Max();
            else
                d_n = d_minus.Max();
            int id_criterion = 3;
            criticalCriterion = FindCriticalK(0, id_criterion, signLevel);
            double sqrt_countData = Math.Sqrt(f.Count());
            criterion = d_n * (sqrt_countData - 0.01 + (0.85 / sqrt_countData));
        }

        public static void CalculareFrociniCriterion(List<KeyValuePair<DateTime, double>> data, double standartDeviation, double expectedValue, out double criterion, out double criticalCriterion, string signLevel)
        {
            standartDeviation = Math.Sqrt(data.Sum(item => Math.Pow(item.Value - expectedValue, 2)) / (data.Count));
            var z = (from item in data
                     select (item.Value - expectedValue) / standartDeviation);
            DataBaseND db = new DataBaseND() { nameDB = "DBCriterionND.db" };
            db.ConnectionBD();
            int id_laplas = 2;
            Dictionary<double, double> laplasValues = db.CriterionValue("0", id_laplas);
            db.CloseConnectionDB();
            var f = (from item in z
                     select (0.5 + FindLaplFunctionValue(laplasValues, item))).ToList();
            double sum = 0;
            for (int index = 1; index <= f.Count(); index++)
            {
                sum += Math.Abs(f[index - 1] - (Convert.ToDouble(index) - 0.5) / Convert.ToDouble(f.Count()));
            }
            if (f.Count() < 5)
                throw new Exception("Данных слишком мало. Необходимо хотя бы больше 5.");
            int count;
            if (f.Count() > 21)
                count = 21;
            else
                count = f.Count();
            int id_criterion = 4;
            criticalCriterion = FindCriticalK(count, id_criterion, signLevel);
            double sqrt_countData = Math.Sqrt(f.Count());
            criterion = (1 / sqrt_countData) * sum;
        }
        public static void CalculatePirsonCriteria(List<KeyValuePair<DateTime, double>> data, List<KeyValuePair<string, double>> columns,
            double standartDeviation, double expectedValue, double delta, out double criterion, out double criticalCriterion, string signLevel)
        {
            DataBaseND db = new DataBaseND() { nameDB = "DBCriterionND.db" };
            db.ConnectionBD();
            Dictionary<double, double> laplasValues = db.CriterionValue("0", 2);
            db.CloseConnectionDB();
            /*var tmp = manager.GetString("lapl").Split(new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries).Select(item => item.Replace(".", ","));*/
            // var tmp = File.ReadAllLines("lapl.txt").Select(item => item.Replace('.', ','));

            /* foreach (var item in tmp)
                 laplasValues.Add(double.Parse(item.Split(' ')[0], new CultureInfo("ru-RU")), double.Parse(item.Split(' ')[1], new CultureInfo("ru-RU")));*/
            /*var xAvg = (from item in columns
                        let xLeft = double.Parse(item.Key.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[0])
                        let xRight = double.Parse(item.Key.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[1])
                        let xCenter = (xRight + xLeft) / 2
                        select xCenter * item.Value * data.Count).Sum() / data.Count;*/
            var x1 = (from item in columns
                      let xi = double.Parse(item.Key.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[0])
                      select (xi - expectedValue) / standartDeviation);
            var x2 = (from item in columns
                      let xi = double.Parse(item.Key.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[1])
                      select (xi - expectedValue) / standartDeviation);
            var p = (from item in x1.Zip(x2, (first, second) => new { item1 = first, item2 = second })
                     select (FindLaplFunctionValue(laplasValues, item.item2) - FindLaplFunctionValue(laplasValues, item.item1)) * data.Count);
            var k = (from item in columns.Zip(p, (first, second) => new { item1 = first, item2 = second })
                     select Math.Pow((item.item1.Value * data.Count * delta) - item.item2, 2) / item.item2);
            double sumK = k.Sum();
            double critK = FindCriticalK(columns.Count - 2 - 1, 1, signLevel);
            criterion = sumK;
            criticalCriterion = critK;
            var c = (from item in columns
                     select (item.Value * delta * data.Count));
        }

        static double FindCriticalK(int freedomDegree, int ID, string signLevel)
        {
            DataBaseND db = new DataBaseND() { nameDB = "DBCriterionND.db" };
            db.ConnectionBD();
            Dictionary<double, double> criticalValues = db.CriterionValue(signLevel, ID);
            /* var tmp = manager.GetString("xiSQR").Split(new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);
             tmp = tmp.Select(item => item.Replace(".", ",")).ToArray();*/
            /*Dictionary<int, double> criticalValues = new Dictionary<int, double>();
            foreach (var item in tmp)
                criticalValues.Add(int.Parse(item.Split(' ')[0]), double.Parse(item.Split(' ')[1], new CultureInfo("ru-RU")));*/
            if (freedomDegree < 0 || !criticalValues.ContainsKey(Convert.ToDouble(freedomDegree)))
                return double.NegativeInfinity;
            return criticalValues[freedomDegree];
        }

        static double FindLaplFunctionValue(Dictionary<double, double> laplasValues, double x)
        {
            int isMinus = x < 0 ? -1 : 1;
            x = x < 0 ? -x : x;
            if (x >= 5)
                return 0.499997 * isMinus;
            double minDelta = x;
            double minDeltaValue = 0;
            for (int i = 0; i < laplasValues.Count; i++)
                if (Math.Abs(x - laplasValues.Keys.ElementAt(i)) < minDelta)
                {
                    minDelta = Math.Abs(x - laplasValues.Keys.ElementAt(i));
                    minDeltaValue = laplasValues[laplasValues.Keys.ElementAt(i)];
                }
            return minDeltaValue * isMinus;
        }

        public static int GetOptimalColumnsCount(int datacount)
        {
            int count;
            count = datacount <= 100 ? (int)Math.Sqrt(datacount) : (int)(2 * Math.Log10(datacount));
            return count;
        }
    }
}
