using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DM.Data;

namespace DataMiningOffLine.Helpers
{
    public class RegressionModelsDatabaseHelper : IDisposable
    {
        private static readonly string _connectionString;
        private SQLiteConnection _connection;
        private Dictionary<string, int> _inputParametersId;
        private Dictionary<string, int> _outputParametersId;

        static RegressionModelsDatabaseHelper()
        {
            _connectionString = @"Data Source =.\Resources\RegressionModelsDatabase.db";
        }

        public SQLiteConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new SQLiteConnection(_connectionString);
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                return _connection;
            }
        }

        public RegressionModelsDatabaseHelper()
        {
            InitializeInputParametersTable();
            InitializeOutputParametersTable();
            GetModelParametersId();
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public void AddRegressionModel(RegressionModelEquation equation, string anotherName = null, DateTime? anotherDate = null)
        {
            string insertModelCommand =
                "Insert into RegressionModels (OutputParamId, CreationDate, Name, rmse, NormalizeValues) VALUES (@OutputParamId, @CreationDate, @Name, @rmse, @NormalizeValues)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"OutputParamId", _outputParametersId[XMLWork.FindScadaNameWithID(equation.OutputParameter.Id.Value)] },
                {"CreationDate", anotherDate.HasValue ? anotherDate.Value.ToOADate() : equation.CreationDate.ToOADate() },
                {"Name", anotherName ?? equation.Name },
                {"rmse", equation.RMSE },
                {"NormalizeValues", equation.NormalizeValues ? 1 : 0 }
            };
            int modelId = InsertCommand(insertModelCommand, parameters);
            StringBuilder insertParamsCommand = new StringBuilder(
                "Insert into RegressionModelParameters (ParamId, ModelId, Coefficient, UpperBound, LowerBound) VALUES ");
            Dictionary<string, object> insertParamsParameters = new Dictionary<string, object>();
            for (int i = 0; i < equation.InputParameters.Count; i++)
            {
                insertParamsCommand.Append(
                    $"(@ParamId{i}, @ModelId{i}, @Coefficient{i}, @UpperBound{i}, @LowerBound{i}), ");
                if (equation.InputParameters[i].Id.HasValue)
                {
                    insertParamsParameters.Add($"ParamId{i}",
                        _inputParametersId[XMLWork.FindScadaNameWithID(equation.InputParameters[i].Id.Value)]);
                }
                else
                {
                    insertParamsParameters.Add($"ParamId{i}", null);
                }
                insertParamsParameters.Add($"ModelId{i}", modelId);
                insertParamsParameters.Add($"Coefficient{i}", equation.InputParameters[i].Coefficient);
                insertParamsParameters.Add($"UpperBound{i}", equation.InputParameters[i].UpperBound);
                insertParamsParameters.Add($"LowerBound{i}", equation.InputParameters[i].LowerBound);
            }
            insertParamsCommand.Remove(insertParamsCommand.Length - 2, 2);
            InsertCommand(insertParamsCommand.ToString(), insertParamsParameters);
        }

        public List<RegressionModelEquation> GetAllRegressionModels()
        {
            string getSql = "Select Id from RegressionModels";
            var reader = GetCommand(getSql, null);
            List<RegressionModelEquation> models = new List<RegressionModelEquation>();
            List<int> id = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id.Add(Convert.ToInt32(reader["Id"].ToString()));
                }
                reader.Close();
                foreach (var item in id)
                {
                    models.Add(GetRegressionModelById(item));
                }
            }
            else
            {
                return null;
            }

            return models;
        }

        public RegressionModelEquation GetRegressionModelById(int id)
        {
            string getSql = "Select RegressionModels.Id, RegressionModels.OutputParamId, OutputParams.InternalName, RegressionModels.CreationDate, RegressionModels.Name, RegressionModels.rmse, RegressionModels.NormalizeValues from RegressionModels INNER JOIN OutputParams on OutputParams.ID = RegressionModels.OutputParamId where RegressionModels.Id = @Id";
            var reader = GetCommand(getSql, new Dictionary<string, object> {{"Id", id}});
            int modelId, outputParamId;
            DateTime creationDate;
            string name;
            double rmse;
            bool normalizeValues;
            if (reader.Read())
            {
                modelId = Convert.ToInt32(reader["Id"].ToString());
                outputParamId = XMLWork.FindID(reader["InternalName"].ToString());
                creationDate = DateTime.FromOADate(Convert.ToDouble(reader["CreationDate"].ToString()));
                name = reader["Name"].ToString();
                rmse = Convert.ToDouble(reader["rmse"].ToString());
                normalizeValues = Convert.ToInt32(reader["NormalizeValues"].ToString()) == 1;
                reader.Close();
            }
            else
            {
                return null;
            }

            string getParamsSql =
                "Select ParamId, InternalName, Coefficient, UpperBound, LowerBound from RegressionModelParameters LEFT OUTER JOIN InputParams on InputParams.ID = RegressionModelParameters.ParamId where ModelId = @ModelId";
            var paramsReader = GetCommand(getParamsSql, new Dictionary<string, object> {{"ModelId", modelId}});
            List<RegressionModelEquation.ModelParameter> inputParameters = new List<RegressionModelEquation.ModelParameter>();
            if (paramsReader.HasRows)
            {
                while (paramsReader.Read())
                {
                    int? paramId;
                    if (paramsReader["InternalName"] is DBNull)
                    {
                        paramId = null;
                    }
                    else
                    {
                        paramId = XMLWork.FindID(paramsReader["InternalName"].ToString());
                    }
                    double coefficient = Convert.ToDouble(paramsReader["Coefficient"].ToString());
                    double? lowerBound, upperBound;
                    if (paramsReader["LowerBound"] is DBNull)
                    {
                        lowerBound = null;
                    }
                    else
                    {
                        lowerBound = Convert.ToDouble(paramsReader["LowerBound"].ToString());
                    }
                    if (paramsReader["UpperBound"] is DBNull)
                    {
                        upperBound = null;
                    }
                    else
                    {
                        upperBound = Convert.ToDouble(paramsReader["UpperBound"].ToString());
                    }
                    inputParameters.Add(new RegressionModelEquation.ModelParameter
                    {
                        Id = paramId, Coefficient = coefficient, LowerBound = lowerBound, UpperBound = upperBound
                    });
                }
            }
            else
            {
                return null;
            }

            return new RegressionModelEquation(modelId, name, creationDate, rmse, outputParamId, inputParameters, normalizeValues);
        }

        public void DeleteRegressionModelById(int id)
        {
            string deleteParamsSql = "delete from RegressionModelParameters where ModelId = @Id";
            Dictionary<string, object> deleteParamsParameters = new Dictionary<string, object> {{"Id", id}};
            DeleteCommand(deleteParamsSql, deleteParamsParameters);
            string deleteModelSql = "delete from RegressionModels where ID = @Id";
            DeleteCommand(deleteModelSql, deleteParamsParameters);
        }

        private void InitializeOutputParametersTable()
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select ID from OutputParams";
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    return;
            }
            var qualityParams = TrainData.nameParameter.Values.Where(o => o.StartsWith("Def"));
            RemoveAllFromTable("OutputParams");
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                StringBuilder insertCommand = new StringBuilder("Insert into OutputParams (ID, InternalName, RussianName, EnglishName) VALUES ");
                int counter = 0;
                foreach (var qualityParam in qualityParams)
                {
                    insertCommand.Append(
                        $"(@ID{counter}, @InternalName{counter}, @RussianName{counter}, @EnglishName{counter}), ");
                    cmd.Parameters.AddWithValue($"ID{counter}", counter);
                    cmd.Parameters.AddWithValue($"InternalName{counter}", qualityParam);
                    cmd.Parameters.AddWithValue($"RussianName{counter}", XMLWork.FindNameWithScada(qualityParam, "ru-RU"));
                    cmd.Parameters.AddWithValue($"EnglishName{counter}", XMLWork.FindNameWithScada(qualityParam, "en-US"));
                    counter++;
                }
                insertCommand.Remove(insertCommand.Length - 2, 2);
                cmd.CommandText = insertCommand.ToString();
                var i = cmd.ExecuteNonQuery();
            }
        }

        private void InitializeInputParametersTable()
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select ID from InputParams";
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    return;
            }
            var inputParams = TrainData.nameParameter.Values.Where(o => o.StartsWith("OPC"));
            RemoveAllFromTable("InputParams");
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                StringBuilder insertCommand = new StringBuilder("Insert into InputParams (ID, InternalName, RussianName, EnglishName) VALUES ");
                int counter = 0;
                foreach (var inputParam in inputParams)
                {
                    insertCommand.Append(
                        $"(@ID{counter}, @InternalName{counter}, @RussianName{counter}, @EnglishName{counter}), ");
                    cmd.Parameters.AddWithValue($"ID{counter}", counter);
                    cmd.Parameters.AddWithValue($"InternalName{counter}", inputParam);
                    cmd.Parameters.AddWithValue($"RussianName{counter}", XMLWork.FindNameWithScada(inputParam, "ru-RU"));
                    cmd.Parameters.AddWithValue($"EnglishName{counter}", XMLWork.FindNameWithScada(inputParam, "en-US"));
                    counter++;
                }
                insertCommand.Remove(insertCommand.Length - 2, 2);
                cmd.CommandText = insertCommand.ToString();
                var i = cmd.ExecuteNonQuery();
            }
        }

        private void GetModelParametersId()
        {
            _inputParametersId = new Dictionary<string, int>();
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select ID, InternalName from InputParams";
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"].ToString());
                            string internalName = reader["InternalName"].ToString();
                            _inputParametersId.Add(internalName, id);
                        }
                    }
                }
            }

            _outputParametersId = new Dictionary<string, int>();
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select ID, InternalName from OutputParams";
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"].ToString());
                            string internalName = reader["InternalName"].ToString();
                            _outputParametersId.Add(internalName, id);
                        }
                    }
                }
            }
        }

        private void RemoveAllFromTable(string tableName)
        {
            try
            {
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"delete from {tableName}";
                    var i = cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        #region Common methods
        private int InsertCommand(string insertSql, Dictionary<string, object> parameters)
        {
            try
            {
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = insertSql;
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                    cmd.ExecuteNonQuery();
                    return (int)Connection.LastInsertRowId;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return -1;
            }
        }

        private SQLiteDataReader GetCommand(string getSql, Dictionary<string, object> parameters)
        {
            try
            {
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = getSql;
                    if (parameters != null)
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    return cmd.ExecuteReader();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private void DeleteCommand(string deleteSql, Dictionary<string, object> parameters)
        {
            try
            {
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = deleteSql;
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion
    }
}