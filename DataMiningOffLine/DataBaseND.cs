using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SQLite;

namespace DataMiningOffLine
{
    class DataBaseND
    {
        // Имя базы данных
        public string nameDB;

        // для соединения с базой данных
        private SQLiteConnection m_dbConn;

        // для выполнения запросов
        private SQLiteCommand m_sqlCmd;

        // Функция для соединения с базой данных 
        public void ConnectionBD()
        {
            nameDB = "Resources\\" + nameDB;
            if (string.IsNullOrWhiteSpace(nameDB))
                throw new Exception("Не введено имя базы данных.");
            if (!File.Exists(nameDB))
                throw new Exception("Sorry, no database!!!");
            m_dbConn = new SQLiteConnection("Data Source=" + nameDB + ";Version=3;");
            m_dbConn.Open();
        }

        public void CloseConnectionDB()
        {
            if (m_dbConn.State == ConnectionState.Open)
                m_dbConn.Close();
        }

        /*Добавление записей в таблицу:
        nameTable - название таблицы
        field - список полей
        value - список значений полей
        */
        public void AddDataIntoBD(string nameTable, List<string> fields, List<string> values)
        {
            if (m_dbConn.State != ConnectionState.Open)
                throw new Exception("No connection with DB!!!");
            if (fields.Count != values.Count || fields.Count == 0 || string.IsNullOrWhiteSpace(nameTable))
                throw new Exception("Incorrect input data!!!");
            // приведение к нужному формату для запроса
            StringBuilder fields_query = new StringBuilder();
            StringBuilder values_query = new StringBuilder();
            foreach (var field in fields)
            {
                fields_query.Append(field);
                if (field != fields.Last())
                    fields_query.Append(", ");
            }
            for (int index = 0; index < values.Count(); index++)
            {
                values_query.Append(String.Format("\"{0}\"", values[index]));
                if (index != values.Count() - 1)
                    values_query.Append(", ");
            }
            string sqlQuery = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", nameTable, fields_query.ToString(), values_query.ToString());
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            m_sqlCmd.ExecuteNonQuery();
        }

        /*Удаление записи из таблиц:
         tables - таблицы, которых может находится запись, последняя - где id является первичным ключем
         field - поле с id
         id - номер
         */
        public void DeleteDataInDB(List<string> tables, string field, int id)
        {
            if (m_dbConn.State != ConnectionState.Open)
                throw new Exception("No connection with DB!!!");
            if (tables.Count == 0 || string.IsNullOrWhiteSpace(field) || id <= 0)
                throw new Exception("Incorrect input data");
            foreach (var table in tables)
            {
                int count = FindDataInTable(table, field, id);
                while (count > 0)
                {
                    string sqlQuary = String.Format("DELETE FROM {0} WHERE {1} = \"{2}\"", table, field, id);
                    m_sqlCmd = new SQLiteCommand
                    {
                        Connection = m_dbConn,
                        CommandText = sqlQuary
                    };
                    m_sqlCmd.ExecuteNonQuery();
                    count--;
                }
            }
        }

        //Функция, которая проверяет есть ли запись в таблице, для удаления связанных записей
        public int FindDataInTable(string table, string field, int value)
        {
            int count = 0;
            string sqlQuery;
            sqlQuery = String.Format("SELECT {0} FROM {1} WHERE {0} =\"{2}\"", field, table, value);
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                count++;
            }
            return count;
        }

        /*Функция для обновления данных в базе данных:
         table - название таблицы
         fields - список полей для обновления
         values - список значений для обновления
         id_field - названия поля с id
         fieldToFind - название поля по которому ищется id
         valueToFind - значение поля fieldToFind*/
        public void UpdateDataInBD(string table, List<string> fields, List<string> values, string fieldToFind, string valueToFind)
        {
            if (m_dbConn.State != ConnectionState.Open)
                throw new Exception("No connection with DB!!!");
            if (fields.Count != values.Count || fields.Count == 0 || string.IsNullOrWhiteSpace(table) || string.IsNullOrWhiteSpace(fieldToFind)
                                             || string.IsNullOrWhiteSpace(fieldToFind) || string.IsNullOrWhiteSpace(valueToFind))
                throw new Exception("Incorrect input data!!!");
            /*List<int> id = FindIdInDB(table, id_field, fieldToFind, valueToFind);
            if (id.Count != 1)
                throw new Exception("More than one id return!!!");*/
            StringBuilder update_fields_and_values = new StringBuilder();
            for (int index = 0; index < fields.Count; index++)
            {
                update_fields_and_values.Append(String.Format("{0} = \"{1}\"", fields[index], values[index]));
                if (index != fields.Count - 1)
                    update_fields_and_values.Append(", ");
            }
            string sqlQuary = String.Format("UPDATE {0} SET {1} WHERE {2} = \"{3}\"", table, update_fields_and_values.ToString(), fieldToFind, valueToFind);
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuary
            };
            m_sqlCmd.ExecuteNonQuery();
        }

        /*Функция для нахождения id
         table - таблица где искать
         field - название поля, которое содержит id
         fieldToFind - название поля по которому ищем
         value - значение поля fieldToFind*/
        /* public List<int> FindIdInDB(string table, string field, string fieldToFind, string value)
         {
             List<int> id = new List<int>();
             if (m_dbConn.State != ConnectionState.Open)
                 throw new Exception("No connection with DB!!!");

             string sqlQuery = String.Format("SELECT {0} FROM {1} WHERE {2} =\"{3}\"", table, field, fieldToFind, value);
             m_sqlCmd = new SQLiteCommand
             {
                 Connection = m_dbConn,
                 CommandText = sqlQuery
             };
             SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
             while (sqlReader.Read())
             {
                 id.Add(Convert.ToInt32(sqlReader[String.Format("{0}", field)]));
             }
             return id;
         }*/

        // Возвращает из таблицы table список значений поля field
        public List<string> SelectDB(string table, string field)
        {
            if (m_dbConn.State != ConnectionState.Open)
                throw new Exception("No connection with DB!!!");
            if (string.IsNullOrWhiteSpace(table) || string.IsNullOrWhiteSpace(field))
                throw new Exception("Incorrect input data!!!");
            List<string> values = new List<string>();
            string sqlQuery = String.Format("SELECT {1} FROM {0}", table, field);
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                values.Add(Convert.ToString(sqlReader[String.Format("{0}", field)]));
            }
            return values;
        }

        // Возвращает список values - значения полей fieldsd из таблицы table, где значение поля fieldToFind равно valueToFind
        public List<string> SelectDB(string table, List<string> fields, List<string> fieldToFind, List<string> valueToFind)
        {
            if (m_dbConn.State != ConnectionState.Open)
                throw new Exception("No connection with DB!!!");
            if (string.IsNullOrWhiteSpace(table) || fields.Count == 0)
                throw new Exception("Incorrect input data!!!");
            List<string> values = new List<string>();
            // Приведение списка полей к формату запроса
            StringBuilder fields_query = new StringBuilder();
            foreach (var field in fields)
            {
                fields_query.Append(field);
                if (field != fields.Last())
                    fields_query.Append(", ");
            }
            StringBuilder fieldToFind_query = new StringBuilder();

            for (int index = 0; index < fieldToFind.Count(); index++)
            {
                fieldToFind_query.Append(String.Format("{0} = \"{1}\"", fieldToFind[index], valueToFind[index]));
                if (index != fieldToFind.Count() - 1)
                    fieldToFind_query.Append(" AND ");
            }

            string sqlQuery = String.Format("SELECT {1} FROM {0} WHERE {2}", table, fields_query.ToString(), fieldToFind_query.ToString());
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                foreach (var field in fields)
                {
                    values.Add(sqlReader[string.Format("{0}", field)].ToString());
                }
            }
            return values;
        }

        public bool UseValueInDB(string table, string field_id, string field, string value)
        {
            List<string> id = SelectDB(table, new List<string>() { field_id }, new List<string>() { field }, new List<string>() { value });
            if (id.Count() > 0)
                return true;
            return false;
        }

        public List<List<string>> SelectAllDataInDB(string table, List<string> fields)
        {
            if (m_dbConn.State != ConnectionState.Open)
                throw new Exception("No connection with DB!!!");
            if (string.IsNullOrWhiteSpace(table) || fields.Count == 0)
                throw new Exception("Incorrect input data!!!");
            List<List<string>> values = new List<List<string>>();
            // Приведение списка полей к формату запроса
            StringBuilder fields_query = new StringBuilder();
            foreach (var field in fields)
            {
                fields_query.Append(field);
                if (field != fields.Last())
                    fields_query.Append(", ");
            }
            string sqlQuery = String.Format("SELECT {1} FROM {0} ", table, fields_query.ToString());
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                List<string> data = new List<string>();
                foreach (var field in fields)
                    data.Add(Convert.ToString(sqlReader[string.Format("{0}", field)]));
                values.Add(data);
            }
            return values;
        }

        public Dictionary<double, double> CriterionValue(string signLevel, int id)
        {
            if (m_dbConn.State != ConnectionState.Open)
                throw new Exception("No connection with DB!!!");
            if (id == 0)
                throw new Exception("Incorrect input data!!!");
            Dictionary<double, double> values = new Dictionary<double, double>();
            // Приведение списка полей к формату запроса

            string sqlQuery = String.Format("SELECT Freedom_degree, Value FROM Value WHERE ID_criterion = \"{0}\" AND Significance_level = \"{1}\"", id, signLevel);
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {

                double degree = Convert.ToDouble(sqlReader[string.Format("{0}", "Freedom_degree")].ToString());
                //double.Parse(sqlReader[string.Format("{0}", "Freedom_degree")].ToString(), CultureInfo.GetCultureInfo("ru-RU"));
                double value = Convert.ToDouble(sqlReader[string.Format("{0}", "Value")].ToString());
                //double.Parse(sqlReader[string.Format("{0}", "Value")].ToString(), CultureInfo.GetCultureInfo("ru-RU"));
                values.Add(degree, value);
            }
            return values;
        }

        public int FindUserStatus()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            int id = 0;
            m_dbConn = new SQLiteConnection("Data Source=" + nameDB + ";Version=3;");
            m_dbConn.Open();
            string sqlQuery;
            sqlQuery = String.Format("SELECT ID_user, Name_User FROM User WHERE Status =\"1\"");
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                id = Convert.ToInt32(sqlReader["ID_user"]);
            }
            m_dbConn.Close();
            return id;
        }

        public void UpdateUserStatus()
        {
            int id = FindUserStatus();
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            m_dbConn = new SQLiteConnection("Data Source=" + nameDB + ";Version=3;");
            m_dbConn.Open();
            while (id != 0)
            {
                string sqlQuery = String.Format("UPDATE User SET Status =\"{1}\" WHERE ID_user = \"{0}\"", id, "0");
                m_sqlCmd = new SQLiteCommand
                {
                    Connection = m_dbConn,
                    CommandText = sqlQuery
                };
                m_sqlCmd.ExecuteNonQuery();
                id = FindUserStatus();
            }
            m_dbConn.Close();
        }

        public Dictionary<int, List<string>> SelectAllDataInDBDictionary(string table, List<string> fields)
        {
            if (m_dbConn.State != ConnectionState.Open)
                throw new Exception("No connection with DB!!!");
            if (string.IsNullOrWhiteSpace(table) || fields.Count < 2)
                throw new Exception("Incorrect input data!!!");
            Dictionary<int, List<string>> values = new Dictionary<int, List<string>>();
            // Приведение списка полей к формату запроса
            StringBuilder fields_query = new StringBuilder();
            foreach (var field in fields)
            {
                fields_query.Append(field);
                if (field != fields.Last())
                    fields_query.Append(", ");
            }
            string sqlQuery = String.Format("SELECT {1} FROM {0} ", table, fields_query.ToString());
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                List<string> data = new List<string>();
                int id = 0;
                for (int index = 0; index < fields.Count(); index++)
                {
                    if (index == 0)
                        id = Convert.ToInt32(sqlReader[string.Format("{0}", fields[index])]);
                    else
                        data.Add(Convert.ToString(sqlReader[string.Format("{0}", fields[index])]));
                }
                values.Add(id, data);
            }
            return values;
        }
    }
}
