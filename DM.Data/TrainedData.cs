using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DM.Data
{
    public interface ITrainedDataSet
    {
        #region Properties
        Dictionary<int, decimal> Input { get; set; }

        Dictionary<int, decimal> Output { get; set; }

        Dictionary<int, bool> OutputBool { get; set; }

        DateTime Date { get; set; }

        #endregion
    }

    [Serializable]
    public class OneRow : ISerializable, ITrainedDataSet
    {

        #region Properties
        private DateTime _date;

        private Dictionary<int, decimal> _input = new Dictionary<int, decimal>();

        private Dictionary<int, decimal> _output = new Dictionary<int, decimal>();



        private Dictionary<int, bool> _outputBool = new Dictionary<int, bool>();

        private Dictionary<string, decimal> _temperatures = new Dictionary<string, decimal>();
        private Dictionary<string, decimal> _velocities = new Dictionary<string, decimal>();

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Dictionary<int, decimal> Input
        {
            get { return _input; }
            set { _input = value; }
        }

        public Dictionary<int, decimal> Output
        {
            get { return _output; }
            set { _output = value; }
        }



        public Dictionary<int, bool> OutputBool
        {
            get { return _outputBool; }
            set { _outputBool = value; }
        }
        public Dictionary<string, decimal> Temperatures
        {
            get { return _temperatures; }
            set { _temperatures = value; }
        }
        public Dictionary<string, decimal> Velocities
        {
            get { return _velocities; }
            set { _velocities = value; }
        }
        #endregion
        public OneRow()
        { }
        public OneRow(List<OneRow> item)
        {
            foreach (var items in item)
            {
                foreach (var s in items.Input)
                {
                    this.Input.Add(s.Key, s.Value);
                }
                this.Output.Add(0, items.Output[0]);
            }
        }

        public OneRow(SerializationInfo sInfo, StreamingContext contextArg)
        {
            this.Date = (DateTime)sInfo.GetValue("date", typeof(DateTime));
            this.Input = (Dictionary<int, decimal>)sInfo.GetValue("input", typeof(Dictionary<int, decimal>));
            this.Output = (Dictionary<int, decimal>)sInfo.GetValue("output", typeof(Dictionary<int, decimal>));
        }


        #region Methods
        public void GetObjectData(SerializationInfo sInfo, StreamingContext contextArg)
        {
            sInfo.AddValue("date", this.Date);
            sInfo.AddValue("input", (Dictionary<int, decimal>)this.Input);
            sInfo.AddValue("output", (Dictionary<int, decimal>)this.Output);
        }
        #endregion
    }

    public class TrainData : IEnumerable
    {
        #region Properties
        private static List<OneRow> _trainData = new List<OneRow>();
        private static Dictionary<string, string> pairs = new Dictionary<string, string> { };
        private static Dictionary<string, string> pairsRus = new Dictionary<string, string> { };
        private static Dictionary<int, decimal> _outputAverage = new Dictionary<int, decimal>();

        private static Dictionary<int, decimal> _outputSigma = new Dictionary<int, decimal>();


        private static Dictionary<int, decimal> _cpu = new Dictionary<int, decimal>();
        private static Dictionary<int, decimal> _cpl = new Dictionary<int, decimal>();

        public static Dictionary<int, decimal> CPU
        {
            get { return _cpu; }
            set { _cpu = value; }
        }
        public static Dictionary<int, decimal> CPL
        {
            get { return _cpl; }
            set { _cpl = value; }
        }

        public static Dictionary<int, decimal> OutputAverage
        {
            get { return _outputAverage; }
            set { _outputAverage = value; }
        }

        public static Dictionary<int, decimal> OutputSigma
        {
            get { return _outputSigma; }
            set { _outputSigma = value; }
        }

        public static Dictionary<string, string> Pairs
        {
            get { return pairs; }
            set { pairs = value; }
        }
        public static Dictionary<string, string> PairsRus
        {
            get { return pairsRus; }
            set { pairsRus = value; }
        }


        public static void GetData()
        {
            if (pairs.Count == 0)
            {
                pairs.Add("Defects.Roll10Sqm.DefMap0", "Gels ");
                pairs.Add("Defects.Roll10Sqm.DefMap1", "Black Point ");
                pairs.Add("Defects.Roll10Sqm.DefMap2", "Air Marks ");
                pairs.Add("Defects.Roll10Sqm.DefMap3", "Fiber ");
                pairs.Add("Defects.Roll10Sqm.DefMap4", "Hole ");
                pairs.Add("Defects.Roll10Sqm.DefMap5", "Defect > 15mm²");
                pairs.Add("Defects.Roll10Sqm.DefMap6", "Burning Stripe");
                pairs.Add("Defects.Roll10Sqm.DefMap7", "Defect7");
                pairs.Add("Defects.Roll10Sqm.DefMap8", "Defect8");
                pairs.Add("Defects.Roll10Sqm.DefMap9", "Defect9");
                pairs.Add("Defects.Roll10Sqm.DefMap10", "Defect10");
                pairs.Add("Defects.Roll10Sqm.DefMap11", "Defect11");
                pairs.Add("Defects.Roll10Sqm.DefMap12", "Defect12");
                pairs.Add("Defects.Roll10Sqm.DefMap13", "Defect13");
                pairs.Add("Defects.Roll10Sqm.DefMap14", "Defect14");
                pairs.Add("Defects.Roll10Sqm.DefMap15", "Defect15");
                pairs.Add("Defects.Roll10Sqm.DefMap16", "Defect16");
                pairs.Add("Defects.Roll10Sqm.DefMap17", "Defect17");
                pairs.Add("Defects.Roll10Sqm.DefMap18", "Defect18");
                pairs.Add("Defects.Roll10Sqm.DefMap19", "Defect19");
                pairs.Add("OPC_W1_XI_K02", "Calender Roll 1 - Force ");
                pairs.Add("OPC_W1_XT_K02", "Calender Roll 1 - Temperature ");
                pairs.Add("OPC_W1_XV_K02", "Calender Roll 1 - Speed ");
                pairs.Add("OPC_W2_XI_K02", "Calender Roll 2 - Force ");
                pairs.Add("OPC_W2_XT_K02", "Calender Roll 2 - Temperature ");
                pairs.Add("OPC_W2_XV_K02", "Calender Roll 2 - Speed ");
                pairs.Add("OPC_W3_XI_K02", "Calender Roll 3 - Force ");
                pairs.Add("OPC_W3_XT_K02", "Calender Roll 3 - Temperature ");
                pairs.Add("OPC_W3_XV_K02", "Calender Roll 3 - Speed ");
                pairs.Add("OPC_W4_XI_K02", "Calender Roll 4 - Force ");
                pairs.Add("OPC_W4_XT_K02", "Calender Roll 4 - Temperature ");
                pairs.Add("OPC_W4_XV_K02", "Calender Roll 4 - Speed ");
                pairs.Add("OPC_ABZ12_XI_K02", "Take-off rolls 1-2 - Force ");
                pairs.Add("OPC_ABZ12_XT_K02", "Take-off rolls 1-2 - Temperature ");
                pairs.Add("OPC_ABZ12_XV_K02", "Take-off rolls 1-2 - Speed ");
                pairs.Add("OPC_ABZ36_XI_K02", "Take-off rolls 3-6 - Force ");
                pairs.Add("OPC_ABZ36_XT_K02", "Take-off rolls 3-6 - Temperature ");
                pairs.Add("OPC_ABZ36_XV_K02", "Take-off rolls 3-6 - Speed ");
                pairs.Add("OPC_ABZ710_XI_K02", "Take-off rolls 7-10 - Force ");
                pairs.Add("OPC_ABZ710_XT_K02", "Take-off rolls 7-10 - Temperature ");
                pairs.Add("OPC_ABZ710_XV_K02", "Take-off rolls 7-10 - Speed ");
                pairs.Add("OPC_TW12_XI_K02", "Tempering rolls 1-2 - Force ");
                pairs.Add("OPC_TW12_XV_K02", "Tempering rolls 1-2 - Speed ");
                pairs.Add("OPC_TW13_XT_K02", "Tempering rolls 1-3 - Temperature ");
                pairs.Add("OPC_TW3_XI_K02", "Tempering rolls 3 - Force ");
                pairs.Add("OPC_TW3_XV_K02", "Tempering rolls 3 - Speed ");
                pairs.Add("OPC_TW47_XI_K02", "Tempering rolls 4-7 - Force ");
                pairs.Add("OPC_TW47_XT_K02", "Tempering rolls 4-7 - Temperature ");
                pairs.Add("OPC_TW47_XV_K02", "Tempering rolls 4-7 - Speed ");
                pairs.Add("OPC_TW811_XI_K02", "Tempering rolls 8-11 - Force ");
                pairs.Add("OPC_TW811_XT_K02", "Tempering rolls 8-11 - Temperature ");
                pairs.Add("OPC_TW811_XV_K02", "Tempering rolls 8-11 - Speed ");
                pairs.Add("OPC_TW1215_XI_K02", "Tempering rolls 12-15 - Force ");
                pairs.Add("OPC_TW1215_XT_K02", "Tempering rolls 12-15 - Temperature ");
                pairs.Add("OPC_TW1215_XV_K02", "Tempering rolls 12-15 - Speed ");
                pairs.Add("OPC_ASZ1_XI_K02", "Winder 1 - Force ");
                pairs.Add("OPC_ASZ1_XV_K02", "Winder 1 - Speed ");
                pairs.Add("OPC_ASZ2_XI_K02", "Winder 2 - Force ");
                pairs.Add("OPC_ASZ2_XV_K02", "Winder 2 - Speed ");
                pairs.Add("OPC_XI51_P1_K02", "OPC_XI51_P1_K02 ");
                pairs.Add("OPC_XI51_P2_K02", "OPC_XI51_P2_K02 ");
                pairs.Add("OPC_XT51_K02", "OPC_XT51_K02");
                pairs.Add("OPC_XI52_KALW_K02", "OPC_XI52_KALW_K02");
                pairs.Add("OPC_HB_XV_K02", "OPC_HB_XV_K02");
                pairs.Add("OPC_KLW_XV_K02", "OPC_KLW_XV_K02");
                pairs.Add("OPC_WP85_RBW4GS_R_K02", "Calender Roll 4 - Roll bending GS ");
                pairs.Add("OPC_WP85_RBW4HS_R_K02", "Calender Roll 4 - Roll bending HS  ");
                pairs.Add("OPC_I_voronka_K02", "Funnel - Force");
                pairs.Add("OPC_V_voronka_K02", "Funnel - Speed");
                pairs.Add("OPC_IA_shnek_K02", "Screw - Force");
                pairs.Add("OPC_V_shnek_K02", "Screw - Speed");
                pairs.Add("Process.RelTransparency", "Process.RelTransparency");
                pairs.Add("Process.AbsTransparency", "Process.AbsTransparency");
                pairs.Add("Process.Noise", "Process.Noise");
                pairs.Add("Process.WebSpeed", "Process.WebSpeed");
            }
            if (pairsRus.Count == 0)
            {
                pairsRus.Add("Defects.Roll10Sqm.DefMap0", "Гелики ");
                pairsRus.Add("Defects.Roll10Sqm.DefMap1", "Черные точки ");
                pairsRus.Add("Defects.Roll10Sqm.DefMap2", "Воздушные включения ");
                pairsRus.Add("Defects.Roll10Sqm.DefMap3", "Волокна ");
                pairsRus.Add("Defects.Roll10Sqm.DefMap4", "Дыра ");
                pairsRus.Add("Defects.Roll10Sqm.DefMap5", "Дефект > 15мм²");
                pairsRus.Add("Defects.Roll10Sqm.DefMap6", "Деструкционные полосы");
                pairsRus.Add("Defects.Roll10Sqm.DefMap7", "Дефект7");
                pairsRus.Add("Defects.Roll10Sqm.DefMap8", "Дефект8");
                pairsRus.Add("Defects.Roll10Sqm.DefMap9", "Дефект9");
                pairsRus.Add("Defects.Roll10Sqm.DefMap10", "Дефект10");
                pairsRus.Add("Defects.Roll10Sqm.DefMap11", "Дефект11");
                pairsRus.Add("Defects.Roll10Sqm.DefMap12", "Дефект12");
                pairsRus.Add("Defects.Roll10Sqm.DefMap13", "Дефект13");
                pairsRus.Add("Defects.Roll10Sqm.DefMap14", "Дефект14");
                pairsRus.Add("Defects.Roll10Sqm.DefMap15", "Дефект15");
                pairsRus.Add("Defects.Roll10Sqm.DefMap16", "Дефект16");
                pairsRus.Add("Defects.Roll10Sqm.DefMap17", "Дефект17");
                pairsRus.Add("Defects.Roll10Sqm.DefMap18", "Дефект18");
                pairsRus.Add("Defects.Roll10Sqm.DefMap19", "Дефект19");
                pairsRus.Add("OPC_W1_XI_K02", "Каландровый вал № 1 - Нагрузка ");
                pairsRus.Add("OPC_W1_XT_K02", "Каландровый вал № 1 - Температура ");
                pairsRus.Add("OPC_W1_XV_K02", "Каландровый вал № 1 - Скорость ");
                pairsRus.Add("OPC_W2_XI_K02", "Каландровый вал № 2 - Нагрузка ");
                pairsRus.Add("OPC_W2_XT_K02", "Каландровый вал № 2 - Температура ");
                pairsRus.Add("OPC_W2_XV_K02", "Каландровый вал № 2 - Скорость ");
                pairsRus.Add("OPC_W3_XI_K02", "Каландровый вал № 3 - Нагрузка ");
                pairsRus.Add("OPC_W3_XT_K02", "Каландровый вал № 3 - Температура ");
                pairsRus.Add("OPC_W3_XV_K02", "Каландровый вал № 3 - Скорость ");
                pairsRus.Add("OPC_W4_XI_K02", "Каландровый вал № 4 - Нагрузка ");
                pairsRus.Add("OPC_W4_XT_K02", "Каландровый вал № 4 - Температура ");
                pairsRus.Add("OPC_W4_XV_K02", "Каландровый вал № 4 - Скорость ");
                pairsRus.Add("OPC_ABZ12_XI_K02", "Съемный вал № 1-2 - Нагрузка ");
                pairsRus.Add("OPC_ABZ12_XT_K02", "Съемный вал № 1-2 - Температура ");
                pairsRus.Add("OPC_ABZ12_XV_K02", "Съемный вал № 1-2 - Скорость ");
                pairsRus.Add("OPC_ABZ36_XI_K02", "Съемный вал № 3-6 - Нагрузка ");
                pairsRus.Add("OPC_ABZ36_XT_K02", "Съемный вал № 3-6 - Температура ");
                pairsRus.Add("OPC_ABZ36_XV_K02", "Съемный вал № 3-6 - Скорость ");
                pairsRus.Add("OPC_ABZ710_XI_K02", "Съемный вал № 7-10 - Нагрузка ");
                pairsRus.Add("OPC_ABZ710_XT_K02", "Съемный вал № 7-10 - Температура ");
                pairsRus.Add("OPC_ABZ710_XV_K02", "Съемный вал № 7-10 - Скорость ");
                pairsRus.Add("OPC_TW12_XI_K02", "Темперирующий вал № 1-2 - Нагрузка ");
                pairsRus.Add("OPC_TW12_XV_K02", "Темперирующий вал № 1-2 - Скорость ");
                pairsRus.Add("OPC_TW13_XT_K02", "Темперирующий вал № 1-3 - Температура ");
                pairsRus.Add("OPC_TW3_XI_K02", "Темперирующий вал № 3 - Нагрузка ");
                pairsRus.Add("OPC_TW3_XV_K02", "Темперирующий вал № 3 - Скорость ");
                pairsRus.Add("OPC_TW47_XI_K02", "Темперирующий вал № 4-7 - Нагрузка ");
                pairsRus.Add("OPC_TW47_XT_K02", "Темперирующий вал № 4-7 - Температура ");
                pairsRus.Add("OPC_TW47_XV_K02", "Темперирующий вал № 4-7 - Скорость ");
                pairsRus.Add("OPC_TW811_XI_K02", "Темперирующий вал № 8-11 - Нагрузка ");
                pairsRus.Add("OPC_TW811_XT_K02", "Темперирующий вал № 8-11 - Температура ");
                pairsRus.Add("OPC_TW811_XV_K02", "Темперирующий вал № 8-11 - Скорость ");
                pairsRus.Add("OPC_TW1215_XI_K02", "Темперирующий вал № 12-15 - Нагрузка ");
                pairsRus.Add("OPC_TW1215_XT_K02", "Темперирующий вал № 12-15 - Температура ");
                pairsRus.Add("OPC_TW1215_XV_K02", "Темперирующий вал № 12-15 - Скорость ");
                pairsRus.Add("OPC_ASZ1_XI_K02", "Намотка № 1 - Нагрузка ");
                pairsRus.Add("OPC_ASZ1_XV_K02", "Намотка № 1 - Скорость ");
                pairsRus.Add("OPC_ASZ2_XI_K02", "Намотка № 2 - Нагрузка ");
                pairsRus.Add("OPC_ASZ2_XV_K02", "Намотка № 2 - Скорость ");
                pairsRus.Add("OPC_XI51_P1_K02", "OPC_XI51_P1_K02 ");
                pairsRus.Add("OPC_XI51_P2_K02", "OPC_XI51_P2_K02 ");
                pairsRus.Add("OPC_XT51_K02", "OPC_XT51_K02");
                pairsRus.Add("OPC_XI52_KALW_K02", "OPC_XI52_KALW_K02");
                pairsRus.Add("OPC_HB_XV_K02", "OPC_HB_XV_K02");
                pairsRus.Add("OPC_KLW_XV_K02", "OPC_KLW_XV_K02");
                pairsRus.Add("OPC_WP85_RBW4GS_R_K02", "Каландровый вал № 4 - Контр изгиб GS ");
                pairsRus.Add("OPC_WP85_RBW4HS_R_K02", "Каландровый вал № 4 - Контр изгиб HS  ");
                pairsRus.Add("OPC_I_voronka_K02", "Воронка - Нагрузка");
                pairsRus.Add("OPC_V_voronka_K02", "Воронка - Скорость");
                pairsRus.Add("OPC_IA_shnek_K02", "Шнек - Нагрузка");
                pairsRus.Add("OPC_V_shnek_K02", "Шнек - Скорость");
                pairsRus.Add("Process.RelTransparency", "Относительная прозрачность");
                pairsRus.Add("Process.AbsTransparency", "Абсолютная прозрачность");
                pairsRus.Add("Process.Noise", "Шум");
                pairsRus.Add("Process.WebSpeed", "Скорость измерения OCS");
            }
        }


        public static Dictionary<int, string> nameParameter = new Dictionary<int, string>();

        public static List<OneRow> Train
        {
            get { return _trainData; }
            set { _trainData = value; }
        }

        #endregion


        #region Methods
        /// <summary>
        /// Возвращает OurData по индексу массива
        /// </summary>
        public OneRow this[int index]
        {
            get { return _trainData[index]; }
            set { _trainData.Insert(index, value); }
        }


        /// <summary>
        /// Определяет число измерений
        /// </summary>
        public int Count
        { get { return _trainData.Count; } }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return _trainData.GetEnumerator();
        }

        /// <summary>
        /// Возвращает данные по указанному массиву
        /// </summary>
        public OneRow GetData(int pos)
        { return _trainData[pos]; }


        /// <summary>
        /// Добавляет данные в TrainDataProvider
        /// </summary>
        public void AddData(OneRow t)
        { _trainData.Add(t); }

        /// <summary>
        /// Формирует Запрос по времени
        /// </summary>
        /// <param name="dt">ттекущий момент времени по которому строится запрос</param>
        /// <returns>Возращает список значений</returns>
        public List<OneRow> GetSet(DateTime dt)
        {
            var data = new List<OneRow>();
            IEnumerable<OneRow> query = _trainData.AsParallel().OrderBy(train => train.Input.Keys).Where(train => train.Date == dt);
            foreach (OneRow train in query)
            {
                data.Add(train);
            }
            return data;
        }

        public void RemoveData()
        {
            int i = _trainData.Count;
            _trainData.RemoveRange(0, i);
        }
        #endregion
    }
}
