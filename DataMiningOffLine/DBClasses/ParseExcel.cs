using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;


namespace DataMiningOffLine.DBClasses
{
    class ParseExcel
    {
        public static string excelPath { get; set; }

        private static ExcelQueryFactory excell;// = new ExcelQueryFactory(@"Z:\Dn\DataMiningOffLine\DataMiningOffLine\3Тестовая выборка.xlsx");

        public static void openExcell()
        {
            if (excelPath != null)
                excell = new ExcelQueryFactory(excelPath);
        }

        public static void addMeasurements()
        {
            var columnNames = excell.GetColumnNames(excell.GetWorksheetNames().ToArray()[0].ToString()).ToArray();

            var table = from x in excell.Worksheet(excell.GetWorksheetNames().ToArray()[0].ToString())
                        select x;

            string date = null;
            string time = null;
            int counter = 2;
            string paramname = null;
            double value = 0;
            string databaseName = Parameters.getParam().connection;

            using (SQLiteConnection connection =
                            new SQLiteConnection(string.Format("Data Source={0};", databaseName)))
            {
                connection.Open();

                foreach (var a in table)
                {
                    try
                    {
                        var t = TimeSpan.FromDays(Convert.ToDouble(a.ElementAt(1)));
                        time = t.Hours + ":" + t.Minutes + ":" + t.Seconds;
                        // date = a.First().ToString().Replace("0:00:00","");
                    }
                    catch { time = a.ElementAt(1).ToString().Remove(0, a.ElementAt(1).ToString().IndexOf(' ')); }

                    using (SQLiteTransaction tr = connection.BeginTransaction())
                    {
                        using (SQLiteCommand command1 = connection.CreateCommand())
                        {
                            command1.Transaction = tr;

                                foreach (var b in a.Skip(2))
                                {

                                    paramname = columnNames.ElementAt(counter).ToString().Replace('#', '.');

                                    
                                    string select = @"(SELECT P.id FROM Parameters P WHERE P.name_st = " + "'" + paramname + "'" + ")";
                                    string querry = "INSERT INTO Measurements (param_id,time_st, value) VALUES (" + select + ","
                                        + "'" + time + "'" + ","
                                        + "'" + b.Value.ToString().Replace(',','.') + "'" + ");";

                                    command1.CommandText = querry;

                                    command1.ExecuteNonQuery();
                                    counter++;
                                }                           
   
                        }
                        using (SQLiteCommand com = connection.CreateCommand()) {
                            com.Transaction = tr;
                            com.CommandText = "DELETE FROM Measurements WHERE value = '' or value ='               ' or value = '0.0'";

                            com.ExecuteNonQuery();
                        }

                        tr.Commit();
                    }

                    date = "";
                    time = "";
                    counter = 2;
                }
                connection.Close();
            }
        }
    }
}
