using System.Collections.Generic;

namespace DataMiningOffLine.Helpers
{
    public class Localisation
    {
        private static Localisation instance;

        private Localisation() {}

        public static Localisation GetInstance() {
            if (instance == null)
            {
                instance = new Localisation();
            }
            return instance;
        }

        public string Language { get; set; }

    }

}
