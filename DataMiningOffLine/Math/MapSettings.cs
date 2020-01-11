using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningOffLine
{
    public class MapSettings : IComparable<MapSettings>//интерфейс реализующий сортировку
    {
        //скрываем все важные для объекта переменные
        private int hexSide; //Длина стороны шестиугольника
        private int xOffset; //координата Х начала карты
        private int yOffset; //координата Y начала карты
        private int penWidth; //толщина границы ячейки


        public int HexSide //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return hexSide; }
            set
            {
                hexSide = value;
            }
        }

        public int XOffset //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return xOffset; }
            set
            {
                xOffset = value;
            }
        }

        public int YOffset //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return yOffset; }
            set
            {
                yOffset = value;
            }
        }

        public int PenWidth //свойство - уровень проверки входного значения объекта на достоверность
        {
            get { return penWidth; }
            set
            {
                penWidth = value;
            }
        }

        public MapSettings(int inputHexSide, int inputXOfSiade, int inputYOfSide, int inputPenWidth)//конструктор
        {
            HexSide = inputHexSide; //this означает, что мы обращаемся к переменным текущего класса при совпадении имён (в этом случае можно не использовать)
            XOffset = inputXOfSiade;
            YOffset = inputYOfSide;
            PenWidth = inputPenWidth;
        }

        public int CompareTo(MapSettings comparePoint) //метод реализубщий интерфейс сортировки
        {

            return this.hexSide.CompareTo(comparePoint.HexSide);
            //this.Otklonenie.CompareTo(comparePoint.Otklonenie);
            //this.Koncentraciya.CompareTo(comparePoint.Koncentraciya);
        }
    }
}
