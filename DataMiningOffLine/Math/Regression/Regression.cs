using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMiningOffLine
{
    static class Regression
    {
        private static void NormalizeValues(List<PointD> points)
        {
            var expectedAvgX = points.Sum(item => item.X) / points.Count;
            var expectedAvgY = points.Sum(item => item.Y) / points.Count;
            var dispersionXSqruared = points.Sum(item => Math.Pow(item.X - expectedAvgX, 2)) / points.Count;
            var dispersionYSquared = points.Sum(item => Math.Pow(item.Y - expectedAvgY, 2)) / points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new PointD() { X = (points[i].X - expectedAvgX) / dispersionXSqruared, Y = (points[i].Y - expectedAvgY) / dispersionYSquared };
            }
        }

        private static void NormalizeValues(double[,] input, double[] output)
        {
            for (int i = 0; i < input.GetLength(0); i++)
                input[i, 0] = 0;
            for (int j = 1; j < input.GetLength(1); j++)
            {
                double expectedAvg = 0;
                for (int i = 0; i < input.GetLength(0); i++)
                    expectedAvg += input[i, j];
                expectedAvg /= input.GetLength(0);
                double dispersionSquared = 0;
                for (int i = 0; i < input.GetLength(0); i++)
                    dispersionSquared += Math.Pow(input[i, j] - expectedAvg, 2);
                dispersionSquared /= input.GetLength(0);
                for (int i = 0; i < input.GetLength(0); i++)
                    input[i, j] = (input[i, j] - expectedAvg) / dispersionSquared;
            }
            double expectedAvgOut = 0;
            for (int i = 0; i < output.Length; i++)
                expectedAvgOut += output[i];
            expectedAvgOut /= output.Length;
            double dispersionSquaredOut = 0;
            for (int i = 0; i < output.Length; i++)
                dispersionSquaredOut += Math.Pow(output[i] - expectedAvgOut, 2);
            dispersionSquaredOut /= output.Length;
            for (int i = 0; i < output.Length; i++)
                output[i] = (output[i] - expectedAvgOut) / dispersionSquaredOut;
        }

        public static double[] LinearRegression(List<PointD> points, bool normalizeValues)
        {
            if (normalizeValues)
                NormalizeValues(points);
            var sumX = points.Sum(item => item.X);
            var sumY = points.Sum(item => item.Y);
            var sumXSquared = points.Sum(item => item.X * item.X);
            var sumXY = points.Sum(item => item.X * item.Y);
            var a = (points.Count * sumXY - sumX * sumY) / (points.Count * sumXSquared - Math.Pow(sumX, 2));
            var b = (sumY - a * sumX) / points.Count;
            return new double[] { b, a };
        }

        public static void Correlation(List<PointD> points, bool normalizeValues, out double RSquared, out double Fisher)
        {
            if (normalizeValues)
                NormalizeValues(points);
            var expectedAvgX = points.Sum(item => item.X) / points.Count; //Выборочное среднее x
            var expectedAvgY = points.Sum(item => item.Y) / points.Count; //Выборочное среднее y
            var expectedAvgXY = points.Sum(item => item.X * item.Y) / points.Count; //Выборочное среднее x*y

            var dispersionXSqruared = points.Sum(item => Math.Pow(item.X - expectedAvgX, 2)) / points.Count;  //Выборочная дисперсия S^2(x)
            var dispersionYSquared = points.Sum(item => Math.Pow(item.Y - expectedAvgY, 2)) / points.Count;  //Выборочная дисперсия S^2(y)


            var correlationKoeffB = (expectedAvgXY - expectedAvgX * expectedAvgY) / dispersionXSqruared; //Коэффициент корреляции b (параметр a уравнения регрессии)?
            var correlationKoeffA = expectedAvgY - correlationKoeffB * expectedAvgX; // (параметр b уравнения регрессии)?

            var rXY = (expectedAvgXY - expectedAvgX * expectedAvgY) / Math.Sqrt(dispersionXSqruared) / Math.Sqrt(dispersionYSquared); // Выборочный линейный коэффициент корреляции
            RSquared = Math.Pow(rXY, 2);
            Fisher = RSquared / (1 - RSquared) * (points.Count - 1 - 1) / 1;
        }

        public static double RMSE(List<PointD> points, double a, double b)
        {
            double mseSum = 0;
            for (int i = 0; i < points.Count; i++)
            {
                mseSum += Math.Pow(points[i].Y - (b + a * points[i].X), 2);
            }
            return Math.Sqrt(mseSum / points.Count);
        }

        public static double MultipleRMSE(double[,] input, double[] output, double[] regressionCoeffs)
        {
            if (input.GetLength(0) != output.Length)
                throw new Exception("Incorrect data in multiple RMSE");
            double mseSum = 0;
            for (int i = 0; i < output.Length; i++)
            {
                double mseSumInternal = 0;
                for (int inputParam = 0; inputParam < input.GetLength(1); inputParam++)
                {
                    mseSumInternal += regressionCoeffs[inputParam] * input[i, inputParam];
                }
                mseSum += Math.Pow(output[i] - mseSumInternal, 2);
            }
            return Math.Sqrt(mseSum / output.Length);
        }

        public static double[] MultipleRegression(double[,] input, double[] output, bool normalizeValues)
        {
            if (input.GetLength(0) != output.Length)
                throw new Exception("Incorrect data size!");
            if (normalizeValues)
                NormalizeValues(input, output);
            double[,] coefficients;
            double[,] newOutput = new double[output.Length, 1];
            for (int i = 0; i < output.Length; i++)
                newOutput[i, 0] = output[i];
            SVDHelper.GetMultipleRegressionСoefficients(input, newOutput, out coefficients);
            double[] returnCoefficients = new double[coefficients.GetLength(0)];
            for (int i = 0; i < returnCoefficients.Length; i++)
                returnCoefficients[i] = coefficients[i, 0];
            return returnCoefficients;
        }
    }
}