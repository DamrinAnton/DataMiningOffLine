
namespace DataMiningOffLine.DBClasses
{
    public class Parameters
    {
        private static Parameters p;

        public string connection { get; set; }

        private Parameters() { }

        public static Parameters getParam() {
            if (p == null)
                p = new Parameters();
            return p;

        }
    }
}
