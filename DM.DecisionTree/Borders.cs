using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.Data;

namespace DM.DecisionTree
{
    public class Borders
    {
        private Entropy _entropy;
        private Attribute _attribute;
        private int _maxTuples;

        public int MaxTuples
        {
            get { return _maxTuples; }
            set { _maxTuples = value; }
        }

        public Borders(Attribute attribute, int maxCurves)
        {
            _attribute = attribute;
            _maxTuples = maxCurves;
        }


        /// <summary>
        /// Создает массив пороговых значений
        /// </summary>
        /// <param name="key">ключ атрибута у которого строятся пороговые значения</param>
        /// <param name="samples">Таблица с измерениями</param>
        /// <returns>Массив пороговых значений </returns>
        public static TupleList<decimal, decimal> AddBordersValue(int key, List<OneRow> samples, int parametersCount, int curveCount, int keyOutput)
        {
            //TODO: переделать критерий разбиений. Разбиение должно быть более осмысленное.
            TupleList<decimal, decimal> groceryList = new TupleList<decimal, decimal>();
            Dictionary<decimal, int> borderValuesTrue = new Dictionary<decimal, int>();
            Dictionary<decimal, int> borderValuesFalse = new Dictionary<decimal, int>();
            //Создается множество уникальных значений
            Dictionary<decimal, int> borderValues = new Dictionary<decimal, int>();
            for (int i = 0; i < samples.Count; i++)
            {
                decimal valueParameter = samples[i].Input.First(paramater => paramater.Key == key).Value;
                if (samples[i].OutputBool[keyOutput])
                {
                    if (borderValuesTrue.ContainsKey(valueParameter))
                        borderValuesTrue[valueParameter] += 1;
                    else
                        borderValuesTrue.Add(valueParameter, 1);
                }
                if (!samples[i].OutputBool[keyOutput])
                {
                    if (borderValuesFalse.ContainsKey(valueParameter))
                        borderValuesFalse[valueParameter] += 1;
                    else
                        borderValuesFalse.Add(valueParameter, 1);
                }

                if (borderValues.ContainsKey(valueParameter))
                    borderValues[valueParameter] += 1;
                else
                    borderValues.Add(valueParameter, 1);
            }
            List<decimal> bordersValuesTrueList = borderValuesTrue.Keys.ToList();
            List<decimal> bordersValuesFalseList = borderValuesFalse.Keys.ToList();
            bordersValuesTrueList.Sort();
            bordersValuesFalseList.Sort();
            List<decimal> newWaveBorderValues = new List<decimal>();
            //создается множество уникальных значений
            int indexTrue = 0;
            int indexFalse = 0;
            int automation = 0;

            indexTrue = ReworkBorderValues(bordersValuesTrueList, indexTrue, bordersValuesFalseList, newWaveBorderValues, ref indexFalse, ref automation);
            bool end = true;
            while (end)
            {


                if ((indexTrue == bordersValuesTrueList.Count) && (indexFalse == bordersValuesFalseList.Count))
                    break;

                if (indexTrue == bordersValuesTrueList.Count)
                {
                    if (indexFalse == bordersValuesFalseList.Count - 1)
                    {
                        newWaveBorderValues.Add(bordersValuesFalseList[bordersValuesFalseList.Count - 1]);
                        break;
                    }
                    newWaveBorderValues.Add(bordersValuesFalseList[indexFalse]);
                    newWaveBorderValues.Add(bordersValuesFalseList[bordersValuesFalseList.Count - 1]);
                    break;
                }
                if (indexFalse == bordersValuesFalseList.Count)
                {
                    if (indexTrue == bordersValuesTrueList.Count - 1)
                    {
                        newWaveBorderValues.Add(bordersValuesTrueList[bordersValuesTrueList.Count - 1]);
                        break;
                    }
                    newWaveBorderValues.Add(bordersValuesTrueList[indexTrue]);
                    newWaveBorderValues.Add(bordersValuesTrueList[bordersValuesTrueList.Count - 1]);
                    break;
                }
                decimal trueNumber = bordersValuesTrueList[indexTrue];
                decimal falseNumber = bordersValuesFalseList[indexFalse];
                switch (automation)
                {
                    case 0://Case когда на прошлом этапе мы выбрали False элемент
                        if (falseNumber < trueNumber)
                        {
                            indexFalse++;
                        }
                        else if (falseNumber > trueNumber)
                        {
                            newWaveBorderValues.Add(trueNumber);
                            indexTrue++;
                            automation = 1;
                        }
                        else if (falseNumber == trueNumber)
                        {
                            newWaveBorderValues.Add(falseNumber);
                            indexTrue++;
                            indexFalse++;
                            automation = 2;
                        }
                        break;
                    case 1:
                        if (falseNumber > trueNumber)
                        {
                            indexTrue++;
                        }
                        else if (falseNumber < trueNumber)
                        {
                            newWaveBorderValues.Add(falseNumber);
                            indexFalse++;
                            automation = 0;
                        }
                        else if (falseNumber == trueNumber)
                        {
                            newWaveBorderValues.Add(falseNumber);
                            indexTrue++;
                            indexFalse++;
                            automation = 2;
                        }
                        break;
                    case 2:
                        if (falseNumber > trueNumber)
                        {
                            newWaveBorderValues.Add(trueNumber);
                            indexTrue++;
                            automation = 1;
                        }
                        else if (falseNumber < trueNumber)
                        {
                            newWaveBorderValues.Add(falseNumber);
                            indexFalse++;
                            automation = 0;
                        }
                        else if (falseNumber == trueNumber)
                        {
                            newWaveBorderValues.Add(trueNumber);
                            indexTrue++;
                            indexFalse++;
                            automation = 2;
                        }
                        break;
                }

            }
            newWaveBorderValues = newWaveBorderValues.Distinct().ToList();
            Dictionary<decimal, int> borderValuesWithPartipition = new Dictionary<decimal, int>();
            int countTrue = 0;
            int countFalse = 0;
            if (borderValuesTrue.ContainsKey(newWaveBorderValues[0]))
                countTrue = borderValuesTrue[newWaveBorderValues[0]];
            if (borderValuesFalse.ContainsKey(newWaveBorderValues[0]))
                countFalse = borderValuesFalse[newWaveBorderValues[0]];
            borderValuesWithPartipition.Add(newWaveBorderValues[0], countTrue + countFalse);
            for (int index = 1; index < newWaveBorderValues.Count; index++)
            {
                borderValuesWithPartipition.Add(newWaveBorderValues[index], 0);
                foreach (KeyValuePair<decimal, int> pair in borderValuesTrue)
                {
                    if ((pair.Key > newWaveBorderValues[index - 1]) && (pair.Key <= newWaveBorderValues[index]))
                    {
                        borderValuesWithPartipition[newWaveBorderValues[index]] += pair.Value;
                    }
                }
                foreach (KeyValuePair<decimal, int> pair in borderValuesFalse)
                {
                    if ((pair.Key > newWaveBorderValues[index - 1]) && (pair.Key <= newWaveBorderValues[index]))
                    {
                        borderValuesWithPartipition[newWaveBorderValues[index]] += pair.Value;
                    }
                }
            }

            //List<decimal> bordersValues = borderValues.Keys.ToList();
            List<decimal> bordersValues = borderValuesWithPartipition.Keys.ToList();
            bordersValues.Sort();
            if ((bordersValues.Count > parametersCount) || (bordersValues.Count > curveCount))
                if (parametersCount >= curveCount)
                    bordersValues = ThresHoldValue(borderValues, curveCount, samples.Count);
                else
                    bordersValues = ThresHoldValue(borderValues, parametersCount, samples.Count);
            else if (bordersValues.Count == 1)
            {

            }

            //int numberBorders = NumberBorders(bordersValues.Count);

            // TODO: Если количество разветлений будет больше 3 то надо будет переписать весь последующий кусок кода в более адекватном формате
            if ((bordersValues.Count > parametersCount) || (bordersValues.Count > curveCount))
                if (parametersCount >= curveCount)
                    groceryList = ChangeBorder(bordersValues, curveCount);
                else
                    groceryList = ChangeBorder(bordersValues, parametersCount);
            else if (bordersValues.Count == 1)
            {
                groceryList.Add(bordersValues[0], bordersValues[0]);
            }
            else
            {
                for (int i = 0; i < bordersValues.Count - 1; i++)
                {
                    if (i != bordersValues.Count - 2)
                        groceryList.Add(bordersValues[i], bordersValues[i + 1] - 0.001M);
                    else
                        groceryList.Add(bordersValues[i], bordersValues[i + 1]);
                }
            }
            return groceryList;
        }

        private static int ReworkBorderValues(List<decimal> bordersValuesTrueList, int indexTrue, List<decimal> bordersValuesFalseList,
            List<decimal> newWaveBorderValues, ref int indexFalse, ref int automation)
        {
            if ((bordersValuesTrueList.Count == 0) && (bordersValuesFalseList.Count == 1))
            {
                newWaveBorderValues.Add(bordersValuesFalseList[0]);
                return 0;
            }
            if ((bordersValuesFalseList.Count == 0) && (bordersValuesTrueList.Count == 1))
            {
                newWaveBorderValues.Add(bordersValuesTrueList[0]);
                return 1;
            }
            if (bordersValuesFalseList.Count == 0)
            {
                newWaveBorderValues.Add(bordersValuesTrueList[0]);
                newWaveBorderValues.Add(bordersValuesTrueList[bordersValuesTrueList.Count - 1]);
                return bordersValuesTrueList.Count;
            }
            if (bordersValuesTrueList.Count == 0)
            {
                newWaveBorderValues.Add(bordersValuesFalseList[0]);
                newWaveBorderValues.Add(bordersValuesFalseList[bordersValuesFalseList.Count - 1]);
                return 0;
            }
            if (bordersValuesTrueList[indexTrue] > bordersValuesFalseList[indexFalse])
            {
                automation = 0;
                newWaveBorderValues.Add(bordersValuesFalseList[indexFalse]);
                indexFalse++;
            }
            else if (bordersValuesTrueList[indexTrue] < bordersValuesFalseList[indexFalse])
            {
                automation = 1;
                newWaveBorderValues.Add(bordersValuesTrueList[indexFalse]);
                indexTrue++;
            }
            else if (bordersValuesTrueList[indexTrue] == bordersValuesFalseList[indexFalse])
            {
                automation = 2;
                newWaveBorderValues.Add(bordersValuesTrueList[indexFalse]);
                indexTrue++;
                indexFalse++;
            }
            return indexTrue;
        }

        /// <summary>
        /// Множество переводим в пороговые значения по формулу
        /// TH = (Vi+Vi+1)/2
        /// </summary>
        /// <param name="border">множество значений для текущего атрибута при заданной sample</param>
        /// <returns>Массив пороговых значений </returns>
        private static List<decimal> ThresHoldValue(Dictionary<decimal, int> border, int parameterCount, int samplesCount)
        {
            List<decimal> parameterValue = border.Keys.ToList();
            parameterValue.Sort();
            Dictionary<decimal, int> borders = new Dictionary<decimal, int>();
            for (int i = 0; i < parameterCount; i++)
            {
                int sum = 0;
                foreach (var value in parameterValue)
                {
                    decimal compareValue = i * samplesCount / (Convert.ToDecimal(parameterCount - 1));
                    sum += border[value];
                    if (sum >= compareValue)
                    {
                        if (!borders.ContainsKey(value))
                        {
                            borders.Add(value, border[value]);
                            break;
                        }
                    }
                }
            }
            List<decimal> newBorder = borders.Keys.ToList();
            // изменить значения чисел равное половине пороговых значений
            /* List<decimal> borderValues = new List<decimal>();
             for (int i = 0; i < border.Count - 1; i++)
             {
                 if (i == 0)
                     borderValues.Add(newBorder[i]);
                 borderValues.Add((newBorder[i] + newBorder[i + 1]) / 2);
                 if (i + 1 == newBorder.Count - 1)
                     borderValues.Add(border[i + 1]);
             }
             if (border.Count == 1)
                 borderValues.Add(border[0]);*/
            return newBorder;
        }

        /// <summary>
        /// Упрощает массив пороговых значений
        /// </summary>
        /// /// <param name="border">Весь список пороговых значений</param>
        /// /// <returns>массив пороговых значений </returns>
        private static TupleList<decimal, decimal> ChangeBorder(List<decimal> border, int parameterNumber)
        {
            double accurateStep = SearchStep(border.Count, parameterNumber);
            var borderValues = new TupleList<decimal, decimal>();
            double sumOfValue = 0;
            for (int i = 0; i < parameterNumber - 1; i++)
            {
                if (i == parameterNumber - 2)
                {
                    borderValues.Add(border[(int)sumOfValue], border[border.Count - 1]);
                    sumOfValue += accurateStep;
                }
                else
                {
                    borderValues.Add(border[(int)sumOfValue], border[(int)(sumOfValue + accurateStep)]);
                    sumOfValue += accurateStep;
                }
            }
            return borderValues;
        }

        /// <summary>
        /// Вычисляет шаг с которым будет строится массив пороговых значений
        /// </summary>
        /// /// <param name="numberOfMeasurements">Количество значений множества</param>
        /// /// <returns>Шаг </returns>
        private static double SearchStep(int numberOfMeasurements, int parameterNumber)
        {
            double number = (numberOfMeasurements - 1) / ((double)parameterNumber);
            return number;

        }
    }
}
