using DataMiningOffLine.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using ZedGraph;

namespace DataMiningOffLine.DBClasses
{
    class Queries
    {
        private void Starter()
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));


            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand("SELECT name_st FROM 'Parameters';", connection);
            SQLiteDataReader reader = command1.ExecuteReader();
            string outstr = "";
            foreach (DbDataRecord record in reader)
                outstr += record["name_st"].ToString() + "\n";

            connection.Close();
        }

        public static string GetAVG(string paramName)
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));


            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand("SELECT AVG(value) as AV FROM Measurements M, Parameters P WHERE P.id = M.param_id AND P.name_st = '" +
                paramName + "' AND M.value != '               '; ", connection);
            SQLiteDataReader reader = command1.ExecuteReader();
            string outstr = "";
            foreach (DbDataRecord record in reader)
                outstr = record["AV"].ToString();

            connection.Close();

            return outstr;
        }

        private static double Median(IEnumerable<int> source)
        {
            var data = source.OrderBy(n => n).ToArray();
            if (data.Length == 0)
                throw new InvalidOperationException();
            if (data.Length % 2 == 0)
                return (data[data.Length / 2 - 1] + data[data.Length / 2]) / 2.0;
            return data[data.Length / 2];
        }

        public static double GetLimit(string column, string paramName)
        {

            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand("SELECT P.lowerLimitPercent, P.upperLimitPercent FROM Parameters P"
                + " WHERE P.name_st = '" + paramName + "'"
                + " ;", connection);
            SQLiteDataReader reader = command1.ExecuteReader();
            double data = 0.0;
            foreach (DbDataRecord record in reader)
                data = Convert.ToDouble(record[column]);

            connection.Close();

            return data;
        }
        public static List<situationsES> GetESSituations()
        {
            List<situationsES> data = new List<situationsES>();
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                        new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            connection.Open();
            string querry = "SELECT DISTINCT D.ES_text FROM Dependences D;";
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                data.Add(new situationsES(reader.GetString(0)));
            }

            connection.Close();

            return data;
        }

        public static List<Dependences> GetResultAnalyse(string name_es)
        {
            List<Dependences> data = new List<Dependences>();

            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                        new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            connection.Open();

            string querry = "SELECT ES_text,firstMaxValue,secondMaxValue,TypeAnalyse FROM Dependences "
                         + " WHERE ES_text = '" + name_es + "';";

            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();


            while (reader.Read())
            {
                data.Add(new Dependences(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
            }

            return data;
        }

        public static void InsertDependences(List<Dependences> dependences)
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();

            string querry = "INSERT INTO 'Dependences' (typeAnalyse,ES_text,firstMaxValue,secondMaxValue)"
                          + " VALUES ('" + dependences[0].typeAnalyse + "','" + dependences[0].nameES + "','" + dependences[0].firstMaxValue + "','" + dependences[0].secondMaxValue + "'),"
                          + "('" + dependences[1].typeAnalyse + "','" + dependences[1].nameES + "','" + dependences[1].firstMaxValue + "','" + dependences[1].secondMaxValue + "'),"
                          + "('" + dependences[2].typeAnalyse + "','" + dependences[2].nameES + "','" + dependences[2].firstMaxValue + "','" + dependences[2].secondMaxValue + "'),"
                          + "('" + dependences[3].typeAnalyse + "','" + dependences[3].nameES + "','" + dependences[3].firstMaxValue + "','" + dependences[3].secondMaxValue + "')";



            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();
            connection.Close();
        }

        public static void ClearDependences()
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();

            string querry = "DELETE FROM Dependences";

            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();
            connection.Close();
        }

        public static List<Recomend> GetListOfRecomend()
        {

            List<Recomend> data = new List<Recomend>();

            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));


            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand("SELECT R.* FROM RECOMENDATIONS R"
                + " WHERE R.PARAM_ST != ''"
                + " ;", connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            foreach (DbDataRecord record in reader)
            {
                data.Add(new Recomend(Convert.ToInt64(record["reason_id"]),
                    Convert.ToInt16(record["number_in"]),
                    record["text_st"].ToString(),
                    record["param_st"].ToString(),
                    record["place_st"].ToString()));
            }

            connection.Close();

            return data;
        }

        public static string GetES(string reason_id)
        {
            string databaseName = Parameters.getParam().connection;
            string message = "";
            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));


            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand("SELECT E.NAME_ST FROM EMERGENCY_SITUATIONS E"
                + " JOIN REASONS R ON R.ES_ID = E.ID"
                + " JOIN RECOMENDATIONS RE ON RE.REASON_ID = '" + reason_id + "' AND R.ID = RE.REASON_ID"
                + " ; ", connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            foreach (DbDataRecord record in reader)
            {
                message = record["NAME_ST"].ToString();
            }

            connection.Close();

            return message;

        }

        public static string GetConstraintsErrors(string param_name)
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string querry = "SELECT MIN(M.time_st), M.value FROM Measurements M"
                            + " JOIN Parameters P ON P.id = M.param_id AND P.name_st = '" + param_name + "' AND M.value != '               '"
                            + " WHERE M.value > P.UpperLimitPercent OR M.value < P.LowerLimitPercent";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            // Change logic hear!
            foreach (DbDataRecord record in reader)
            {
                if (record["MIN(M.time_st)"].ToString() != "")
                    return record["MIN(M.time_st)"].ToString();
            }

            connection.Close();

            return null;

        }

        public static string Update(string querry)
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            command1.ExecuteReader();



            connection.Close();

            return null;

        }

        public static void DeleteMeasurements()
        {
            string databaseName = Parameters.getParam().connection;

            string querry = @"DELETE FROM Measurements";

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            command1.ExecuteReader();

            connection.Close();

        }

        public static HelpInfo GetInfo(string reason_id, string param_st)
        {

            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string langText = Localisation.GetInstance().Language.Equals("RU") ? "TEXT_ST" : "TEXTEN_ST";

            string querry = "SELECT E.NUMBER_ST, RE." + langText + " FROM RECOMENDATIONS R"
                + " JOIN REASONS RE ON RE.ID = R.REASON_ID"
                + " JOIN EMERGENCY_SITUATIONS E ON E.ID = RE.ES_ID"
                + " WHERE R.REASON_ID = '" + reason_id + "' AND R.PARAM_ST = '" + param_st + "'";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            foreach (DbDataRecord record in reader)
            {
                return new HelpInfo(record["NUMBER_ST"].ToString(), record[langText].ToString());
            }

            connection.Close();

            return null;
        }

        public static string GetLocalization(string reason_id, string text, string language)
        {

            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string querry = "SELECT " + (language.Equals("EN") ? "RE.TEXTEN_ST" : "RE.TEXT_ST") + " FROM REASONS RE"
                + " WHERE " + (language.Equals("EN") ? "RE.TEXT_ST" : "RE.TEXTEN_ST") + " = '" + text + "'";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            foreach (DbDataRecord record in reader)
            {
                return record[language.Equals("EN") ? "TEXTEN_ST" : "TEXT_ST"].ToString();
            }

            connection.Close();

            return null;
        }

        public static List<string> GetMeasurmentsTime()
        {

            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string querry = "SELECT M.TIME_ST FROM MEASUREMENTS M";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            List<string> time = new List<string>();

            foreach (DbDataRecord record in reader)
            {
                time.Add(record["TIME_ST"].ToString());
            }


            connection.Close();

            return time;
        }

        public static List<double> GetMeasurmentsByParam(string param)
        {

            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string querry = "SELECT M.VALUE FROM MEASUREMENTS M"
                + " JOIN PARAMETERS P ON P.NAME_ST = " + "'" + param + "'"
                + " WHERE P.ID = M.PARAM_ID";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            List<double> values = new List<double>();

            foreach (DbDataRecord record in reader)
            {
                values.Add(Convert.ToDouble(record["VALUE"]));
            }

            connection.Close();

            return values;
        }

        public static string GetParamName(string param)
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string querry = "SELECT " + (Localisation.GetInstance().Language.Equals("RU") ? "P.TRANSLATERU_ST" : "P.TRANSLATEEN_ST") + " AS V FROM PARAMETERS P"
                + " WHERE P.NAME_ST = " + "'" + param + "'";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            foreach (DbDataRecord record in reader)
            {
                return record["V"].ToString();
            }

            connection.Close();

            return null;
        }

        public static string GetExtruderCleaningData()
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string querry = "SELECT * FROM Cleaning";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            SQLiteDataReader reader = command1.ExecuteReader();

            foreach (DbDataRecord record in reader)
            {
                return record["Cleaning_Data"].ToString();
            }

            connection.Close();

            return null;
        }

        public static void SetExtruderCleaningData(string data)
        {
            string databaseName = Parameters.getParam().connection;

            SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            string querry = "DELETE FROM Cleaning; "
                + "INSERT INTO Cleaning(Cleaning_Data) VALUES('" + data + "'); ";

            connection.Open();
            SQLiteCommand command1 = new SQLiteCommand(querry, connection);
            command1.ExecuteReader();

            connection.Close();

        }
    }
}
