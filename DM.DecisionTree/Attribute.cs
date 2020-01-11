using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DM.DecisionTree
{
    /// <summary>
    /// Класс Атрибут
    /// </summary>
    [Serializable]
    public class Attribute : ISerializable
    {

        #region Properties
        public TupleList<decimal, decimal> mValues;
        private int mName;
        private object mLabel;
        #endregion

        #region Constructors
        public Attribute(SerializationInfo sInfo, StreamingContext contextArg)
        {
            this.mLabel = (object)sInfo.GetValue("mLabel", typeof(object));
            this.mName = (int)sInfo.GetValue("mName", typeof(int));
            this.mValues = (TupleList<decimal, decimal>)sInfo.GetValue("MValues", typeof(TupleList<decimal, decimal>));
        }

        /// <summary>
        /// Инициализация нового класса атрибут
        /// </summary>
        /// <param name="name">Указывает имя атрибута</param>
        /// <param name="values">Указывает пороговые значения этого атрибута</param>
        public Attribute(int name, TupleList<decimal, decimal> values)
        {
            mName = name;
            mValues = values;
            mValues.Sort();
        }
        /// <summary>
        /// Инициализация нового класса атрибут
        /// </summary>
        /// <param name="label"> может принимать два значения true или false</param>
        public Attribute(object Label)
        {
            mLabel = Label;
            mName = -1;
            mValues = null;
        }
        #endregion

        #region Methods
        public void GetObjectData(SerializationInfo sInfo, StreamingContext contextArg)
        {
            sInfo.AddValue("mLabel", this.mLabel);
            sInfo.AddValue("mName", this.mName);
            sInfo.AddValue("MValues", (TupleList<decimal, decimal>)this.mValues);
        }

        public TupleList<decimal, decimal> MValues
        {
            get { return this.mValues; }
            set { this.mValues = value; }
        }


        public void ChangeValue(TupleList<decimal, decimal> value)
        {
            mValues = value;
        }

        /// <summary>
        /// Указывает имя атрибута
        /// </summary>
        public int AttributeName
        {
            get
            {
                return mName;
            }
        }

        public object LabelName
        {
            get
            {
                return mLabel;
            }
        }

        /// <summary>
        /// Возвращает список значений атрибута
        /// </summary>
        public TupleList<decimal, decimal> values
        {
            get
            {
                if (mValues != null)
                    return mValues;
                else
                    return null;
            }
        }
        /// <summary>
        /// Возвращает значение индекса
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Значение положения индекса</returns>
        public int IndexValue(Tuple<decimal, decimal> value)
        {
            if (mValues != null)
                return mValues.BinarySearch(value);
            else
                return -1;
        }
        /// <summary>
        /// Determines whether the provided value is accepted for the type of property through basic type checking
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns></returns>
        public bool IsValidValue(Tuple<decimal, decimal> value)
        {
            return IndexValue(value) >= 0;
        }
        #endregion
    }
}
