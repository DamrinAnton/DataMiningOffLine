using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    public class NetworkSettings : IComparable<NetworkSettings>//интерфейс реализующий сортировку
    {
        //скрываем все важные для объекта переменные
        private int dimentionOfVector; //размерность входного вектора
        private int numberOfNeurons; //количество нейронов в выходном слое
        private int numberOfTrainingCycles; //количество итераций обучения (эпох)
        private int xMap; //количество ячееек карты по оси Х
        private int yMap; //количество ячеек на карте по оси У
        private int rangeOfLearning; //радиус обучения
        private int numberOfVectors; //количество входных векторов

        public int RangeOfLearning //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return rangeOfLearning; }
            set
            {
                rangeOfLearning = value;
            }
        }

        public int XMap //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return xMap; }
            set
            {
                xMap = value;
            }
        }

        public int YMap //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return yMap; }
            set
            {
                yMap = value;
            }
        }

        public int DimentionOfVector //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return dimentionOfVector; }
            set
            {
                dimentionOfVector = value;
            }
        }

        public int NumberOfNeurons //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return numberOfNeurons; }
            set
            {
                numberOfNeurons = value;
            }
        }

        public int NumberOfTrainingCycles //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return numberOfTrainingCycles; }
            set
            {
                numberOfTrainingCycles = value;
            }
        }

        public int NumberOfVectors //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return numberOfVectors; }
            set
            {
                numberOfVectors = value;
            }
        }

        private decimal alfa;

        public decimal Alfa
        {
            get { return alfa; }
            set { alfa = value; }
        }

        private decimal[,] normalise;

        public decimal[,] Normalise
        {
            get { return normalise; }
            set { normalise = value; }
        }

        private decimal[,] weights;

        public decimal[,] Weights
        {
            get { return normalise; }
            set { normalise = value; }
        }

        public NetworkSettings(int inputDimentionOfVector, int inputNumberOfNeurons, int inputNumberOfTrainingCycles, int inputXMAP, int inputYMAP, int inpurRangeOfLearning, int inputNumberOfVectors, decimal[,] weights , decimal[,] normalise, decimal alfas )//конструктор
        {
            DimentionOfVector = inputDimentionOfVector; //this означает, что мы обращаемся к переменным текущего класса при совпадении имён (в этом случае можно не использовать)
            NumberOfNeurons = inputNumberOfNeurons;
            NumberOfTrainingCycles = inputNumberOfTrainingCycles;
            XMap = inputXMAP;
            YMap = inputYMAP;
            RangeOfLearning = inpurRangeOfLearning;
            NumberOfVectors = inputNumberOfVectors;
            this.Weights = weights;
            this.Normalise = normalise;
            this.Alfa = alfas;
        }

        public int CompareTo(NetworkSettings comparePoint) //метод реализубщий интерфейс сортировки
        {

            return this.dimentionOfVector.CompareTo(comparePoint.DimentionOfVector);
            //this.Otklonenie.CompareTo(comparePoint.Otklonenie);
            //this.Koncentraciya.CompareTo(comparePoint.Koncentraciya);
        }
    }
}
