using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.Data;

namespace DataMiningOffLine
{
    public class ProcessingData
    {
        public static void MathematicalExpectation(List<OneRow> trainData, Dictionary<int, decimal> mathematicalExpectation)
        {
            if (trainData.Count != 0)
            {
                int measurementsCount = trainData.Count;

                foreach (var key in trainData[0].Input.Keys)
                {
                    mathematicalExpectation.Add(key, 0.0M);
                    decimal sum = 0.0M;
                    foreach (var row in trainData)
                    {
                        sum += row.Input[key];
                    }
                    mathematicalExpectation[key] = sum / measurementsCount;
                }
            }
            else
            {
                throw new Exception("Обучающая выборка содержит пустое множество!!!!");
            }
        }

        public static void Dispersion(List<OneRow> trainData, Dictionary<int, decimal> mathematicalExpectation, Dictionary<int, decimal> dispersion)
        {
            if (trainData.Count != 0)
            {
                int measurementsCount = trainData.Count;
                foreach (var key in trainData[0].Input.Keys)
                {
                    decimal differenceSquares = trainData.Sum(row => (row.Input[key] * row.Input[key]) - (mathematicalExpectation[key] * mathematicalExpectation[key]));
                    dispersion[key] = differenceSquares / measurementsCount;
                }
            }
            else
            {
                throw new Exception("Обучающая выборка содержит пустое множество!!!!");
            }

        }
    }
}
