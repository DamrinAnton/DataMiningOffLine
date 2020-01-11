namespace DM.Helper
{
    public class RecomendationsStruct
    {
        public string Reason { get; set; }
        public string Actions { get; set; }
        public string ControlParameter { get; set; }
        public string ControlParameterScadaName { get; set; }


        public RecomendationsStruct(string reason, string actions, string controlParameter, string controlParameterScadaName)
        {
            this.Reason = reason;
            this.Actions = actions;
            this.ControlParameter = controlParameter;
            this.ControlParameterScadaName = controlParameterScadaName;
        }
    }
}