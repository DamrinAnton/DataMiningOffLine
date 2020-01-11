using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.Data;

namespace DataMiningOffLine
{
    /// <summary>
    /// Main Class For Calculate Fisher Critery
    /// </summary>
    public class Fisher
    {
        private decimal _averageY;
        private decimal _fisher;
        private List<OneRow> _dataSet = new List<OneRow>();
        private int _inputID;
        private int _outputID;


        /// <summary>
        /// Get or Set AverageValue
        /// </summary>
        public decimal AverageOutput
        {
            get { return _averageY; }
            set { _averageY = value; }
        }


        /// <summary>
        /// Get the value of the Fisher Criterion
        /// </summary>
        public decimal Critery
        {
            get { return _fisher; }
        }


        /// <summary>
        /// Data For Analys
        /// </summary>
        public List<OneRow> DataSet
        {
            get { return _dataSet; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataSet">All Data participating in analysis</param>
        /// <param name="inputID">Technological parameters</param>
        /// <param name="outputID">Quality</param>
        /// <param name="average">average Quality</param>
        public Fisher(List<OneRow> dataSet, int inputID, int outputID, decimal average)
        {
            _averageY = average;
            _dataSet = dataSet;
            _inputID = inputID;
            _outputID = outputID;
        }



        /// <summary>
        /// Calculate of Fisher Criterion
        /// </summary>
        public void CalculateFisher()
        {
            decimal averageX = AverageInput(_dataSet, _inputID);
            decimal b1 = CalculateB1(_dataSet, _inputID, _outputID);
            decimal b0 = _averageY - b1 * averageX;
            decimal standartDeviation = CalculateStandardDeviation(b0, b1, _dataSet, _inputID, _outputID);
            decimal regressionSum = RegressionSum(b0, b1, _dataSet, _inputID, _outputID, _averageY);
            decimal regressionError = RegressionError(b0, b1, _dataSet, _inputID, _outputID);
            decimal regressionFull = regressionSum + regressionError;
            if (regressionSum == 0)
            {
                _fisher = 0;
                return;
            }
            decimal determenation = regressionSum / regressionFull;
            decimal Sr = regressionSum;
            decimal Se = regressionError / (_dataSet.Count - 2);
            _fisher = Sr / (Se);
        }

        /// <summary>
        /// Average value for input parameter(Technological Parameter)
        /// </summary>
        /// <param name="outputData"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private decimal AverageInput(List<OneRow> outputData, int id)
        {
            decimal sum = 0;
            foreach (var outputItem in outputData)
            {
                sum += outputItem.Input[id];
            }
            if (sum != 0)
                return sum / outputData.Count;
            return 0;
        }
        /// <summary>
        /// Calculate First Coefficient
        /// </summary>
        /// <param name="outputData"></param>
        /// <param name="inputID"></param>
        /// <param name="outputID"></param>
        /// <returns></returns>
        private decimal CalculateB1(List<OneRow> outputData, int inputID, int outputID)
        {
            decimal multuplySumXY = 0;
            decimal sumX = 0;
            decimal sumY = 0;
            decimal sumSquareX = 0;
            foreach (var surveillance in outputData)
            {
                multuplySumXY += surveillance.Input[inputID] * surveillance.Output[outputID];
                sumX += surveillance.Input[inputID];
                sumY += surveillance.Output[outputID];
                sumSquareX += surveillance.Input[inputID] * surveillance.Input[inputID];
            }
            if (outputData.Count * sumSquareX - sumX * sumX == 0)
                return 0;
            return ((multuplySumXY * outputData.Count - sumX * sumY) / (outputData.Count * sumSquareX - sumX * sumX));
        }
        /// <summary>
        /// Calculate Standart Deviation Sum(YiCalc - Yi)^2 
        ///                              ------------------
        ///                                     n - m - 1 
        /// Where n = number of objects(Measurements), m - in linear line = 1(number of independent variables), 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="outputData"></param>
        /// <param name="inputID"></param>
        /// <param name="outputID"></param>
        /// <returns></returns>
        private decimal CalculateStandardDeviation(decimal b0, decimal b1, List<OneRow> outputData, int inputID, int outputID)
        {
            double methodLeastSquare = 0;
            foreach (var surveillance in outputData)
            {
                decimal yCalc = b0 + b1 * surveillance.Input[inputID];
                methodLeastSquare += Math.Pow(Convert.ToDouble(yCalc - surveillance.Output[outputID]), 2.0);
            }
            return Convert.ToDecimal(methodLeastSquare / (outputData.Count - 2));
        }
        /// <summary>
        /// Get The Sum Of Regression Line  y = b0 + b1*x 
        /// Sum(YiCalc - Yi)^2 
        /// </summary>
        /// <param name="b0">Coefficient b0</param>
        /// <param name="b1">Coefficient b1</param>
        /// <param name="outputData">Quality</param>
        /// <param name="inputID">One of Technological Parameter</param>
        /// <param name="outputID">Output Id of parameter</param>
        /// <returns></returns>
        private decimal RegressionSum(decimal b0, decimal b1, List<OneRow> outputData, int inputID, int outputID, decimal average)
        {
            double regressionSum = 0;
            foreach (var surveillance in outputData)
            {
                decimal yCalc = b0 + b1 * surveillance.Input[inputID];
                regressionSum += Math.Pow(Convert.ToDouble(yCalc - average), 2.0);
            }
            return Convert.ToDecimal(regressionSum);
        }
        /// <summary>
        /// Get The Error Of Regression Line  y = b0 + b1*x
        /// </summary>
        /// <param name="b0">Coefficient b0</param>
        /// <param name="b1">Coefficient b1</param>
        /// <param name="outputData">Quality</param>
        /// <param name="inputID">One of Technological Parameter</param>
        /// <param name="outputID">Output Id of parameter</param>
        /// <returns></returns>
        private decimal RegressionError(decimal b0, decimal b1, List<OneRow> outputData, int inputID, int outputID)
        {
            double regressionSum = 0;
            foreach (var surveillance in outputData)
            {
                decimal yCalc = b0 + b1 * surveillance.Input[inputID];
                regressionSum += Math.Pow(Convert.ToDouble(surveillance.Output[outputID] - yCalc), 2.0);
            }
            return Convert.ToDecimal(regressionSum);
        }
    }
}
