using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.DecisionTree
{
    public class TimestampGrid
    {
        #region Properties
        public string error;
        public DateTime timestamp;
        #endregion

        #region Constructors
        public TimestampGrid() { }
        public TimestampGrid(DateTime timestamp, bool error)
        {
            this.timestamp = timestamp;
            if (error == true)
                this.error = "Дефект Присутствует";
            else
                this.error = "Дефекта нет";
        }
        #endregion
    }
}
