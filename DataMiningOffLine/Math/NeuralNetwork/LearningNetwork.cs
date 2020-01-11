using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DataMiningOffLine
{
    public class LearningNetwork
    {
        //ref decimal[,] normalizingData,  ref decimal[,] outputNeurons,  NetworkSettings ns, decimal alfa
        public decimal TrainingOfKohonenLayer(object arg)
        {
            decimal[,] normalizingData = ((NetworkSettings)arg).Normalise;
            decimal[,] outputNeurons = ((NetworkSettings)arg).Weights;
            NetworkSettings ns = ((NetworkSettings)arg);
            Random rand = new Random((int)(DateTime.Now.Ticks));
            //DataTable finalWeightVectors = new DataTable();
            int currentNumberOfTrainingCycles;     //номер цикла обучения
            int[] numberOfNeuronWinner = new int[ns.NumberOfVectors];
            decimal[] potenctal = new decimal[ns.NumberOfNeurons];
            int[] winnersCount = new int[ns.NumberOfNeurons];
            decimal pmin = 0.75M;
            int number;
            decimal functionOfSpeedTraining = 0.9M; //функция (коэфициент) скорости обучения
            decimal functionNeighborhood; // функция соседства
            decimal networkError = 0.1M; //погрешность сети (квантования)
            decimal alfaZero = ns.Alfa;
            decimal deltaZero = Convert.ToDecimal(ns.RangeOfLearning);

            for (int i = 0; i < ns.NumberOfNeurons; i++)
            {
                potenctal[i] = 1.0M;
            }



            for (currentNumberOfTrainingCycles = 0; currentNumberOfTrainingCycles < ns.NumberOfTrainingCycles; currentNumberOfTrainingCycles++)
            {
                List<int> numbers = new List<int>();

                for (int k = 0; k < ns.NumberOfNeurons; k++)
                {
                    winnersCount[k] = 0;
                }
                decimal alfa = alfaZero * (1 - (currentNumberOfTrainingCycles / (decimal)ns.NumberOfTrainingCycles)); // Коэффициент скорости обучения
                decimal delta = deltaZero * (1 - (currentNumberOfTrainingCycles / (decimal)ns.NumberOfTrainingCycles)); // Вычисляет радиус обучения используемые функцией 
                decimal[] minEuclideanDistance = new decimal[ns.NumberOfVectors]; //минимальное евклидово расстояние
                for (int i = 0; i < ns.NumberOfVectors; i++)
                {
                    minEuclideanDistance[i] = decimal.MaxValue;
                    numbers.Add(i);
                }


                for (int j = 0; j < ns.NumberOfVectors; j++)
                //нахождение евклидова расстояния между входным вектором и нейронами
                {
                    number = RandomValue.Random(rand, numbers.Count - 1);
                    int numberRow = number;
                    number = numbers[number];
                    numbers.RemoveAt(numberRow);
                    for (int k = 0; k < ns.NumberOfNeurons; k++)
                    {
                        if (potenctal[k] > pmin)
                        {
                            decimal euclideanDistance = 0; //евклидова метрика
                            for (int i = 0; i < ns.DimentionOfVector; i++)
                            {
                                euclideanDistance += (normalizingData[i, number] - outputNeurons[i, k]) *
                                                     (normalizingData[i, number] - outputNeurons[i, k]);
                            }
                            euclideanDistance = Convert.ToDecimal(Math.Sqrt((Convert.ToDouble(euclideanDistance))));

                            if (minEuclideanDistance[number] > euclideanDistance)
                            {
                                minEuclideanDistance[number] = euclideanDistance;
                                numberOfNeuronWinner[number] = k;

                            }
                        }
                    }
                    winnersCount[numberOfNeuronWinner[number]]++; // Сколько раз одержал нейрон победу ( итератор) 
                    //Цикл изменяющий потенциал
                    for (int i = 0; i < ns.NumberOfNeurons; i++)
                    {
                        if (numberOfNeuronWinner[number] == i)
                            potenctal[numberOfNeuronWinner[number]] -= pmin;
                        else if (potenctal[i] >= 1.0M)
                            potenctal[i] = 1.0M;
                        else
                            potenctal[i] += 1 / (decimal)ns.NumberOfNeurons;
                    }


                    int axisX = numberOfNeuronWinner[number] % ns.XMap;
                    int axisY = numberOfNeuronWinner[number] / ns.YMap;
                    Elements elements = new Elements(ns.NumberOfNeurons);
                    int neuronNumber = axisY * ns.YMap + axisX;
                    elements.neighbours[neuronNumber].value = true;
                    ///Матрица поиска соседей
                    /// 
                    for (int i = 1; i <= Convert.ToInt32(delta); i++)
                    {
                        for (int z = 0; z < ns.NumberOfNeurons; z++)
                        {
                            if(true)
                            {
                                if ((elements.neighbours[z].value == true) && (elements.neighbours[z].depth == i - 1))
                                {

                                    if ((z / ns.YMap == ns.YMap - 1) || (z / ns.YMap == 0))
                                    {
                                        for (int l = 0; l < ns.XMap; l++)
                                        {
                                            int row = z / ns.YMap;
                                            int neuronNumbers = row * ns.YMap + l;
                                            if (!elements.neighbours[neuronNumbers].value)
                                            {
                                                elements.neighbours[neuronNumbers].value = true;
                                                elements.neighbours[neuronNumbers].depth = i;
                                            }
                                        }
                                    }
                                    if (z + 1 < ns.YMap * ns.XMap - 1)
                                    {
                                        if ((z % ns.XMap == ns.XMap - 1) && (z - ns.XMap - 1 > 0))
                                        {
                                            if (!elements.neighbours[z - ns.XMap - 1].value)
                                            {
                                                elements.neighbours[z - ns.XMap - 1].value = true;
                                                elements.neighbours[z - ns.XMap - 1].depth = i;
                                            }
                                        }
                                        else if (!elements.neighbours[z + 1].value)
                                        {
                                            elements.neighbours[z + 1].value = true;
                                            elements.neighbours[z + 1].depth = i;
                                        }

                                    }
                                    if (z - 1 >= 0)
                                    {
                                        if ((z % ns.XMap == 0) && (!elements.neighbours[z + ns.XMap - 1].value))
                                        {
                                            elements.neighbours[z + ns.XMap - 1].value = true;
                                            elements.neighbours[z + ns.XMap - 1].depth = i;
                                        }
                                        else if (!elements.neighbours[z - 1].value)
                                        {
                                            elements.neighbours[z - 1].value = true;
                                            elements.neighbours[z - 1].depth = i;
                                        }

                                    }
                                    if ((z + ns.YMap < ns.YMap * ns.XMap - 1) && (!elements.neighbours[z + ns.YMap].value))
                                    {
                                        elements.neighbours[z + ns.YMap].value = true;
                                        elements.neighbours[z + ns.YMap].depth = i;
                                    }
                                    if ((z + ns.YMap + 1 < ns.YMap * ns.XMap - 1) && (!elements.neighbours[z + ns.YMap + 1].value))
                                    {
                                        elements.neighbours[z + ns.YMap + 1].value = true;
                                        elements.neighbours[z + ns.YMap + 1].depth = i;
                                    }

                                    if ((z + ns.YMap - 1 < ns.YMap * ns.XMap - 1) && (!elements.neighbours[z + ns.YMap - 1].value))
                                    {
                                        elements.neighbours[z + ns.YMap - 1].value = true;
                                        elements.neighbours[z + ns.YMap - 1].depth = i;
                                    }
                                    if ((z - ns.YMap >= 0) && (!elements.neighbours[z - ns.YMap].value))
                                    {
                                        elements.neighbours[z - ns.YMap].value = true;
                                        elements.neighbours[z - ns.YMap].depth = i;
                                    }

                                    if ((z - ns.YMap + 1 >= 0) && (!elements.neighbours[z - ns.YMap + 1].value))
                                    {
                                        elements.neighbours[z - ns.YMap + 1].value = true;
                                        elements.neighbours[z - ns.YMap + 1].depth = i;
                                    }
                                    if ((z - ns.YMap - 1 >= 0) && (!elements.neighbours[z - ns.YMap - 1].value))
                                    {
                                        elements.neighbours[z - ns.YMap - 1].value = true;
                                        elements.neighbours[z - ns.YMap - 1].depth = i;
                                    }

                                }

                            }
                        }
                    }

                    for (int i = 0; i < ns.NumberOfNeurons; i++)
                    {
                        if (elements.neighbours[i].value)
                        {
                            for (int m = 0; m < ns.DimentionOfVector; m++)
                            //изменение вектора весов нейронов из радиуса обучения
                            {
                                functionOfSpeedTraining = alfa * Convert.ToDecimal(Math.Exp(
                                            Convert.ToDouble(
                                                -((normalizingData[m, number] - outputNeurons[m, i]) * (normalizingData[m, number] - outputNeurons[m, i]) /
                                                (2 * delta * delta)))));
                                outputNeurons[m, i] =
                                    outputNeurons[m, i] +
                                    functionOfSpeedTraining * (normalizingData[m, number] -
                                    outputNeurons[m, i]);
                            }
                        }
                    }
                }



                if (currentNumberOfTrainingCycles % 200 == 0)
                {
                    decimal errors = 0.0M;
                    for (int j = 0; j < ns.NumberOfVectors; j++)
                    //нахождение евклидова расстояния между входным вектором и нейронами
                    {
                        for (int i = 0; i < ns.DimentionOfVector; i++)
                        {
                            errors += (normalizingData[i, j] - outputNeurons[i, numberOfNeuronWinner[j]]) *
                                                 (normalizingData[i, j] - outputNeurons[i, numberOfNeuronWinner[j]]);
                        }
                    }
                    errors = errors / ns.NumberOfVectors;
                }

            }
            for (int i = 0; i < ns.NumberOfNeurons; i++)
                for (int j = 0; j < ns.DimentionOfVector; j++)
                    outputNeurons[j, i] = Convert.ToDecimal(Math.Round(Convert.ToDouble(outputNeurons[j, i]), 5));


            decimal error = 0.0M;
            for (int j = 0; j < ns.NumberOfVectors; j++)
            //нахождение евклидова расстояния между входным вектором и нейронами
            {
                for (int i = 0; i < ns.DimentionOfVector; i++)
                {
                    error += (normalizingData[i, j] - outputNeurons[i, numberOfNeuronWinner[j]]) *
                                         (normalizingData[i, j] - outputNeurons[i, numberOfNeuronWinner[j]]);
                }
            }
            return error / ns.NumberOfVectors;
        }

        /*
            public decimal BatchLearningSelfOrganizingMaps(ref decimal[,] normalizingData, int rowNormalizingData, int columnNormalizingData, ref decimal[,] outputNeurons, int rowOutputNeurons, int columnOutputNeurons, NetworkSettings ns, decimal alfa)
            {
                int currentNumberOfTrainingCycles;     //номер цикла обучения
                int[] numberOfNeuronWinner = new int[rowNormalizingData];
                decimal[,] hFunction = new decimal[columnNormalizingData, rowNormalizingData];
                decimal[] potenctal = new decimal[rowOutputNeurons];
                int[] winnersCount = new int[rowOutputNeurons];
                decimal pmin = 0.75M;
                decimal alfaZero = 0.75M;
                decimal deltaZero = Convert.ToDecimal(ns.RangeOfLearning);
                for (currentNumberOfTrainingCycles = 0; currentNumberOfTrainingCycles < ns.NumberOfTrainingCycles; currentNumberOfTrainingCycles++)
                {
                    decimal delta = deltaZero * (1 - (currentNumberOfTrainingCycles / (decimal)ns.NumberOfTrainingCycles));
                    for (int i = 0; i < rowOutputNeurons; i++)
                    {
                        potenctal[i] = 1.0M;
                    }
                    for (int k = 0; k < rowOutputNeurons; k++)
                    {
                        winnersCount[k] = 0;
                    }
                    decimal[] minEuclideanDistance = new decimal[rowNormalizingData]; //минимальное евклидово расстояние
                    for (int i = 0; i < rowNormalizingData; i++)
                    {
                        minEuclideanDistance[i] = decimal.MaxValue;
                    }


                    for (int j = 0; j < rowNormalizingData; j++)
                    //нахождение евклидова расстояния между входным вектором и нейронами
                    {
                        for (int k = 0; k < rowOutputNeurons; k++)
                        {
                            if (potenctal[k] > pmin)
                            {
                                decimal euclideanDistance = 0; //евклидова метрика
                                for (int i = 0; i < columnOutputNeurons; i++)
                                {
                                    euclideanDistance += (normalizingData[i, j] - outputNeurons[i, k]) *
                                                         (normalizingData[i, j] - outputNeurons[i, k]);
                                }
                                euclideanDistance = Convert.ToDecimal(Math.Sqrt((Convert.ToDouble(euclideanDistance))));

                                if (minEuclideanDistance[j] > euclideanDistance)
                                {
                                    minEuclideanDistance[j] = euclideanDistance;
                                    numberOfNeuronWinner[j] = k;

                                }
                            }
                        }
                        winnersCount[numberOfNeuronWinner[j]]++; // Сколько раз одержал нейрон победу ( итератор) 
                                                                 //Цикл изменяющий потенциал
                        for (int m = 0; m < columnOutputNeurons; m++)
                        {
                            hFunction[m, j] = Convert.ToDecimal(Math.Exp(
                                                        Convert.ToDouble(
                                                            -((normalizingData[m, j] - outputNeurons[m, numberOfNeuronWinner[j]]) * (normalizingData[m, j] - outputNeurons[m, numberOfNeuronWinner[j]]) /
                                                            (2 * delta * delta)))));
                        }
                        for (int i = 0; i < rowOutputNeurons; i++)
                        {
                            if (numberOfNeuronWinner[j] == i)
                                potenctal[numberOfNeuronWinner[j]] -= pmin;
                            else if (potenctal[i] >= 1.0M)
                                potenctal[i] = 1.0M;
                            else
                                potenctal[i] += 1 / (decimal)rowOutputNeurons;
                        }
                    }
                    decimal[,] newWeights = new decimal[columnOutputNeurons, rowOutputNeurons];
                    decimal[,] newH = new decimal[columnOutputNeurons, rowOutputNeurons];
                    for (int j = 0; j < rowNormalizingData; j++)
                    {
                        for (int k = 0; k < rowOutputNeurons; k++)
                        {

                            for (int i = 0; i < columnOutputNeurons; i++)
                            {
                                newH[i, numberOfNeuronWinner[j]] += hFunction[i, j];
                                newWeights[i, numberOfNeuronWinner[j]] += (normalizingData[i, j] * hFunction[i, numberOfNeuronWinner[j]]);
                            }
                        }
                    }
                    for (int k = 0; k < rowOutputNeurons; k++)
                    {
                        for (int i = 0; i < columnOutputNeurons; i++)
                        {
                            outputNeurons[i, k] = newWeights[i, k] / newH[i, k];
                        }
                    }
                    decimal errors = 0.0M;
                    for (int j = 0; j < rowNormalizingData; j++)
                    //нахождение евклидова расстояния между входным вектором и нейронами
                    {
                        for (int i = 0; i < columnOutputNeurons; i++)
                        {
                            errors += (normalizingData[i, j] - outputNeurons[i, numberOfNeuronWinner[j]]) *
                                                 (normalizingData[i, j] - outputNeurons[i, numberOfNeuronWinner[j]]);
                        }

                    }
                    errors = errors / rowNormalizingData;
                    if (errors <= 0.005M)
                        return errors;
                }
                decimal error = 0.0M;
                for (int j = 0; j < rowNormalizingData; j++)
                //нахождение евклидова расстояния между входным вектором и нейронами
                {
                    for (int i = 0; i < columnOutputNeurons; i++)
                    {
                        error += (normalizingData[i, j] - outputNeurons[i, numberOfNeuronWinner[j]]) *
                                             (normalizingData[i, j] - outputNeurons[i, numberOfNeuronWinner[j]]);
                    }
                }
                return error / rowNormalizingData;

            }
            */

        public static class RandomValue
        {
            static readonly double _min = 0.4;
            static readonly double _max = 0.6;

            public static int Random(Random random, int count)
            {
                return (random.Next(0, count));
            }
        }

        public class Elements
        {
            public Element[] neighbours;
            

            public Elements(int rows )
            {
                neighbours = new Element[rows];
                for (int i = 0; i < rows; i++)
                    neighbours[i] = new Element();
            }
        }

        public class Element
        {
            public bool value;
            public int depth;
            public Element()
            {
                this.value = false;
                this.depth = 0;
            }
        }
    }
}
