using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.Data;


namespace DM.DecisionTree
{
    public class Entropy
    {
        private List<OneRow> _samples;
        private Attribute[] _attributes;
        private int _mTotal = 0;
        private double _mEntropySet = 0;

        private int _defectID;

        public int DefectID
        {
            get { return _defectID; }
            set { _defectID = value; }
        }



        public List<OneRow> Samples
        {
            get { return _samples; }
            set { _samples = value; }
        }

        public Attribute[] Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public int RowCount
        {
            get { return _mTotal; }
            set { _mTotal = value; }
        }

        public double EntropySet
        {
            get { return _mEntropySet; }
            set { _mEntropySet = value; }
        }

        public Entropy(List<OneRow> samples, Attribute[] attributes, int row, double entropy, int defectID)
        {
            _samples = samples;
            _attributes = attributes;
            _mTotal = row;
            _mEntropySet = entropy;
            _defectID = defectID;
        }

        public Entropy(List<OneRow> samples, int row, double entropy)
        {
            _samples = samples;
            _mTotal = row;
            _mEntropySet = entropy;
        }

        /// <summary>
        /// Возвращает лучший атрибут.
        /// </summary>
        /// <param name="attributes">Вектор атрибутов</param>
        /// <returns>Возвращает атрибут который имеет более высокую количественную оценку</returns>
        public Attribute GetBestAttribute()
        {
            double maxGain = 0.0;
            Attribute result = null;


            foreach (Attribute attribute in Attributes)
            {
                double aux = Gain(attribute);

                if (aux > maxGain)
                {
                    maxGain = aux;
                    result = attribute;
                }
            }
            return result;
        }
        /// <summary>
        /// Рассчитываем критерий выбора атрибута
        /// </summary>
        /// <param name="attribute">атрибут по которому вычисляем gain</param>
        /// <returns>Возращает значения критерия выбора атрибута</returns>
        public double Gain(Attribute attribute)
        {
            double sum = 0.0;

            for (int j = 0; j < attribute.values.Count; j++)
            {
                int positives = 0;
                int negatives = 0;

                GetValuesToAttribute(Samples, attribute.AttributeName, attribute.values[j].Item1, attribute.values[j].Item2, out positives, out negatives);

                double entropy = CalcEntropy(positives, negatives);
                sum += -(double)(positives + negatives) / RowCount * entropy;
            }

            return EntropySet + sum;
        }
        /// <summary>
        /// Сканируют таблицу по определенному атрибуту, где результатом является количество положительных или отрицательных значений
        /// </summary>
        /// <param name="samples">TrainItemTree с измерениями</param>
        /// <param name="attribute">Атрибут для поиска</param>
        /// <param name="value">Диапазон значений атрибута</param>
        /// <param name="positives">положительный резутат для атрибута</param>
        /// <param name="negatives">отрицательный резутат для атрибута</param>
        private void GetValuesToAttribute(List<OneRow> samples, int k, decimal value1, decimal value2, out int positives, out  int negatives)
        {
            positives = 0;
            negatives = 0;
            foreach (var item in samples)
            {
                if ((item.Input[k] >= value1) && (item.Input[k] <= value2))
                {
                    if (item.OutputBool[DefectID] == true)
                        positives++;
                    else
                        negatives++;
                }
            }
        }

        /// <summary>
        /// Вычисляют энтропию по следующей формуле
        /// -p+log2p+ - p-log2p-
        /// 
        /// где: p+ вероятность того, что случайное значение примет положительный результат
        ///		  p- вероятность того, что случайное значение примет отрицательный результат
        /// </summary>
        /// <param name="positives">Количество положительных значений</param>
        /// <param name="negatives">Количество отрицательных значений</param>
        /// <returns>Возвращает значение энтропии</returns>
        private double CalcEntropy(int positives, int negatives)
        {
            int total = positives + negatives;
            double ratioPositive = (double)positives / total;
            double ratioNegative = (double)negatives / total;

            if (ratioPositive != 0.0)
                ratioPositive = -(ratioPositive) * System.Math.Log(ratioPositive, 2);
            if (ratioNegative != 0.0)
                ratioNegative = -(ratioNegative) * System.Math.Log(ratioNegative, 2);

            double result = ratioPositive + ratioNegative;

            return result;
        }
        public static double Calculate(int positives, int negatives)
        {
            int total = positives + negatives;
            double ratioPositive = (double)positives / total;
            double ratioNegative = (double)negatives / total;

            if (ratioPositive != 0.0)
                ratioPositive = -(ratioPositive) * System.Math.Log(ratioPositive, 2);
            if (ratioNegative != 0.0)
                ratioNegative = -(ratioNegative) * System.Math.Log(ratioNegative, 2);

            double result = ratioPositive + ratioNegative;

            return result;
        }
    }
}
