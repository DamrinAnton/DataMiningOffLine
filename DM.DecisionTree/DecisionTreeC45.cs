using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.Data;

namespace DM.DecisionTree
{
    public class DecisionTreeC45
    {
        #region Properties
        private int mTotalPositives = 0;
        private int mTotal = 0;
        private double mEntropySet = 0;
        private List<OneRow> mSamples;
        #endregion

        #region Function

        /// <summary>
        /// Создает массив атрибутов, на основе которых и будем строить дерево решений
        /// </summary>
        ///  <param name="samples">Таблица c измерениями</param>
        ///  <param name="counOfParameter">ключ первого параметра</param>
        /// <param name="parametersCount">Количество параметров учавствующих в анализе</param>
        /// <param name="curveCount">Количество дуг,задаваемые пользователем</param>
        /// <returns>Массив атрибутов</returns>
        private Attribute[] CreateAttribute(List<OneRow> samples, int parametersCount, int curveCount, int defectParameter)
        {
            Attribute[] attributes = new Attribute[samples[0].Input.Count];
            int i = 0;
            foreach (var item in samples[0].Input)
            {

                TupleList<decimal, decimal> bordersValue = Borders.AddBordersValue(item.Key, samples, parametersCount, curveCount, defectParameter);
                attributes[i] = new Attribute(item.Key, bordersValue);
                i++;
            }
            return attributes;
        }

        /// <summary>
        /// Строит дерево решений на основе представленных измерений
        /// </summary>
        /// <param name="samples">Таблица с измерениями</param>
        /// <returns>Дерево решений</returns></returns?>
        public TreeNode MountTree(List<OneRow> samples, Dictionary<int, string> parameterName, int defectParameterID, int curveCount)
        {
            int parametersCount = samples[0].Input.First().Key;
            Attribute[] attributes = new Attribute[samples[0].Input.Count];
            attributes = CreateAttribute(samples, parametersCount, curveCount, defectParameterID);
            TreeNode root = IMountTree(samples, attributes, parameterName, defectParameterID, curveCount, 1);
            return root;
        }


        /// <summary>
        /// Этот метод является основным в построении дерева решений
        /// </summary>
        /// <param name="samples">Строит дерево решений на основе предоставленных измерений</param>
        /// <param Name="attributes"> Список атрибутов на основе которых мы строим дерево</param>
        /// <returns>Узел  дерева </returns></returns?>
        private TreeNode IMountTree(List<OneRow> samples, Attribute[] attributes, Dictionary<int, string> parameterName, int defectParameterID, int curveCount, int depth)
        {
            if (AllSamplesPositives(samples, defectParameterID))
                return new TreeNode(new Attribute(true), parameterName, samples.Count, 0, depth);

            if (AllSamplesNegatives(samples, defectParameterID))
                return new TreeNode(new Attribute(false), parameterName, 0, samples.Count, depth);

            mTotal = samples.Count;
            mTotalPositives = CountTotalPositives(samples, defectParameterID);
            mEntropySet = Entropy.Calculate(mTotalPositives, mTotal - mTotalPositives);

            Entropy entropy = new Entropy(samples, attributes, mTotal, mEntropySet, defectParameterID);
            Attribute bestAttribute = entropy.GetBestAttribute();
            //Attribute bestAttribute = GetBestAttribute(samples, attributes);
            if (bestAttribute == null)
            {
                double percent = (double)mTotalPositives / (double)mTotal;
                if (percent > 0.5)
                {
                    return new TreeNode(new Attribute(true), parameterName, mTotalPositives, mTotal - mTotalPositives, depth);
                }
                return new TreeNode(new Attribute(false), parameterName, mTotalPositives, mTotal - mTotalPositives, depth);
            }

            TreeNode root = new TreeNode(bestAttribute, parameterName, mTotalPositives, mTotal - mTotalPositives, depth);

            for (int i = 0; i < bestAttribute.values.Count; i++)
            {
                int newDepth = depth + 1; // В рекурсии для сохранения значения глубины дерева.
                List<OneRow> aSample = new List<OneRow>();

                //Выбирает все элементы с отрезком значений этого атрибута 
                var query = samples.Where(measurement => (measurement.Input[bestAttribute.AttributeName] >= bestAttribute.values[i].Item1 && measurement.Input[bestAttribute.AttributeName] <= bestAttribute.values[i].Item2));
                foreach (var item in query)
                {
                    aSample.Add(item);
                }
                //Выбирает все элементы с отрезком значений этого атрибута 


                //Создает новый список атрибутов, убирая из этого списка лучший атрибут
                List<Attribute> aAttributes = new List<Attribute>(attributes.Length - 1);
                for (int j = 0; j < attributes.Length; j++)
                {
                    if (attributes[j].AttributeName != bestAttribute.AttributeName)
                    {

                        //редактирует пороговые значения атрибута
                        TupleList<decimal, decimal> bordersValue = Borders.AddBordersValue(attributes[j].AttributeName, aSample, aAttributes.Capacity, curveCount, defectParameterID);
                        Attribute att = new Attribute(attributes[j].AttributeName, bordersValue);
                        aAttributes.Add(att);
                        //редактирует пороговые значения атрибута
                    }
                }
                //Создает новый список атрибутов, убирая из этого списка лучший атрибут

                DecisionTreeC45 c45 = new DecisionTreeC45();
                TreeNode childNode = c45.IMountTree(aSample, (Attribute[])aAttributes.ToArray(), parameterName, defectParameterID, curveCount, newDepth);
                root.AddTreeNode(childNode, bestAttribute.values[i]);
            }
            return root;
        }

        /// <summary>
        /// Возвращает истину, если все примеры выборки являются положительными
        /// </summary>
        /// <param name="samples">List c измерениями</param>
        /// <param name="targetAttribute">Атрибут(столбец) который будет проверятся</param>
        /// <returns>True если все измерения являются положительными</returns>
        private bool AllSamplesPositives(List<OneRow> samples, int defectParameterID)
        {
            foreach (var value in samples)
            {
                if ((value.OutputBool[defectParameterID] == false))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Возвращает истину, если все примеры выборки являются отрицательными
        /// </summary>
        /// <param name="samples">List c измерениями</param>
        /// <param name="targetAttribute">Атрибут(столбец) который будет проверятся</param>
        /// <returns>True если все измерения являются отрицательными    </returns>
        private bool AllSamplesNegatives(List<OneRow> samples, int defectParameterID)
        {
            foreach (var value in samples)
            {
                if ((value.OutputBool[defectParameterID] == true))
                    return false;
            }

            return true;
        }

        /// <summary>
        ///  Возвращает общее количество положительных строк в таблице
        /// </summary>
        /// <param name="samples">TrainItemTree со строками</param>
        /// <returns>количество строк удовлетворяющих условию</returns>
        private int CountTotalPositives(List<OneRow> samples, int defectParameterID)
        {
            int result = 0;
            foreach (OneRow item in samples)
            {
                if (item.OutputBool[defectParameterID] == true)
                    result++;
            }
            return result;
        }

        #endregion
    }
}
