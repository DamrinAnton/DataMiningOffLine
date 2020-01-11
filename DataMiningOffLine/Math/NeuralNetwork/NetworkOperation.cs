using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    class NetworkOperation
    {

        public decimal[,] OperationOfKohonenLayer(decimal[,] normalizingData, decimal[,] outputNeurons, decimal[,] inputData,
            NetworkSettings ns)
        {
            decimal[,] valueOfNeurons = new decimal[ns.DimentionOfVector, ns.NumberOfNeurons];
            
            int[] numberOfNeuronWinner = new int[ns.NumberOfVectors]; //номер нейрона победителя
            
            decimal[] minEuclideanDistance = new decimal[ns.NumberOfVectors]; //минимальное евклидово расстояние
            int[] numberOfWins = new int[ns.NumberOfNeurons]; //количество побед
            //numberOfWins = null; 
            for (int i = 0; i < ns.NumberOfVectors; i++)
            {
                minEuclideanDistance[i] = decimal.MaxValue;
            }
            for (int j = 0; j < ns.NumberOfVectors; j++)
                //нахождение евклидова расстояния между входным вектором и нейронами
            {
                decimal[] euclideanDistance = new decimal[ns.NumberOfNeurons]; //евклидова метрика
                for (int k = 0; k < ns.NumberOfNeurons; k++)
                {
                    
                    for (int i = 0; i < ns.DimentionOfVector; i++)
                    {
                        euclideanDistance[k] += (normalizingData[i, j] - outputNeurons[i, k])*
                                                (normalizingData[i, j] - outputNeurons[i, k]);
                    }
                    euclideanDistance[k] = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(euclideanDistance[k])));
                    if (minEuclideanDistance[j] > euclideanDistance[k])
                    {
                        minEuclideanDistance[j] = euclideanDistance[k];
                        numberOfNeuronWinner[j] = k;

                    }
                }
                numberOfWins[numberOfNeuronWinner[j]]++;
                for (int m = 0; m < ns.DimentionOfVector; m++)
                {
                         valueOfNeurons[m,numberOfNeuronWinner[j]]+= inputData[m, j];
                }
            }
            for (int m = 0; m < ns.DimentionOfVector; m++)
            {
                for (int k = 0; k < ns.NumberOfNeurons; k++)
                {
                    if (numberOfWins[k] != 0)
                    {
                        valueOfNeurons[m, k] /= numberOfWins[k];
                        valueOfNeurons[m, k] = Math.Round(valueOfNeurons[m, k], 2);
                    }
                }
            }
                for (int i = 0; i < ns.NumberOfNeurons; i++)
            {   
                if ((valueOfNeurons[0,i]) != 0)
                {
                    for (int j = 0; j < ns.DimentionOfVector ; j++)
                        //valueOfNeurons.Rows[i][j] = Math.Round((Convert.ToDouble(valueOfNeurons.Rows[i][j]) / Convert.ToDouble(valueOfNeurons.Rows[i][0])), 2);
                        valueOfNeurons[j,i] = (valueOfNeurons[j,i]);
                }
                else
                {
                    for (int j = 0; j < ns.DimentionOfVector; j++)
                        valueOfNeurons[j,i] = -1;
                }
            }
            return valueOfNeurons;
        }




    }
}
