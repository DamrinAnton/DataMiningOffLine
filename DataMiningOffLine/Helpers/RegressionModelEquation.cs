using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;

namespace DataMiningOffLine.Helpers
{
    public class RegressionModelEquation
    {
        #region Constants
        const int EquationSignificantDigitsCount = 4;//Количество значащих цифр при выводе уравнения линейной регрессии
        const int RMSESignificantDigitsCount = 5;   //Количество значащих цифр при выводе RMSE
        const int MultipleRegressionEquasionSignificantDigitsCount = 4;//Количество значащих цифр при выводе RMSE для множественной регрессии
        const int ErrorIntervalsCount = 10;//Количество интервалов на графике ошибок линейной регрессии
        const double SubscriptFontScaleFactor = 1.5;//Делитель размера шрифта для вывода надстрочных/подстрочных знаков
        const double DataSensitivityThreshhold = 0.01;
        #endregion

        #region Private fields
        private readonly ModelParameter _outputParameter;
        private readonly List<ModelParameter> _inputParameters;
        private readonly double _RMSE;
        #endregion

        public class ModelParameter
        {
            public int? Id { get; set; }
            public double Coefficient { get; set; }
            public double? LowerBound { get; set; }
            public double? UpperBound { get; set; }
        }

        #region Public fields
        public int? Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string RMSEString => $"RMSE = {Math.Round(_RMSE, RMSESignificantDigitsCount)}";
        public double RMSE => _RMSE;
        public ModelParameter OutputParameter => _outputParameter;
        public List<ModelParameter> InputParameters => _inputParameters;
        public bool IsLinearRegression { get; private set; }
        public bool NormalizeValues { get; private set; }
        #endregion

        #region Ctors

        /// <summary>
        /// Ctor for loading from database
        /// </summary>
        public RegressionModelEquation(int id, string name, DateTime creationDate, double rmse, int outputParamId, List<ModelParameter> inputParameters, bool normalizeValues)
        {
            IsLinearRegression = inputParameters.Count == 2;
            Id = id;
            Name = name;
            CreationDate = creationDate;
            NormalizeValues = normalizeValues;
            _RMSE = rmse;
            _outputParameter = new ModelParameter
            {
                Id = outputParamId,
                Coefficient = 1,
                LowerBound = null,
                UpperBound = null
            };
            _inputParameters = inputParameters;
        }

        /// <summary>
        /// Ctor for linear regression model
        /// </summary>
        public RegressionModelEquation(string modelName, double[] coefficients, double rmse, double minX, double maxX, double minY, double maxY, string nameX, string nameY, bool normalizeValues)
        {
            IsLinearRegression = true;
            Name = modelName;
            CreationDate = DateTime.Now;
            NormalizeValues = normalizeValues;
            _outputParameter = new ModelParameter
            {
                Id = XMLWork.FindIDWithName(nameY, Properties.Settings.Default.Languages),
                Coefficient = 1,
                LowerBound = minY,
                UpperBound = maxY
            };
            _RMSE = rmse;
            _inputParameters = new List<ModelParameter>
            {
                new ModelParameter
                {
                    Id = null,
                    Coefficient = coefficients[0],
                    LowerBound = null,
                    UpperBound = null
                },

                new ModelParameter
                {
                    Id = XMLWork.FindIDWithName(nameX, Properties.Settings.Default.Languages),
                    Coefficient = coefficients[1],
                    LowerBound = minX,
                    UpperBound = maxX
                }
            };
        }

        /// <summary>
        /// Ctor for multiple regression model
        /// </summary>
        public RegressionModelEquation(string modelName, string outputParamName, List<string> inputParamNames, double rmse, double[] equationCoefficients,
            string[] equationParametersNames, double[] equationParametersLowerBounds, double[] equationParametersUpperBounds, bool normalizeValues)
        {
            IsLinearRegression = false;
            Name = modelName;
            CreationDate = DateTime.Now;
            NormalizeValues = normalizeValues;
            _outputParameter = new ModelParameter
            {
                Id = XMLWork.FindIDWithName(outputParamName, Properties.Settings.Default.Languages),
                Coefficient = 1,
                LowerBound = null,
                UpperBound = null
            };

            _RMSE = rmse;
            _inputParameters = new List<ModelParameter>
            {
                new ModelParameter
                {
                    Id = null,
                    Coefficient = equationCoefficients[0],
                    LowerBound = null,
                    UpperBound = null
                }
            };
            for (int i = 1; i < equationCoefficients.Length; i++)
            {
                _inputParameters.Add(new ModelParameter
                {
                    Id = XMLWork.FindIDWithName(equationParametersNames[i - 1], Properties.Settings.Default.Languages),
                    Coefficient = equationCoefficients[i],
                    LowerBound = equationParametersLowerBounds[i - 1],
                    UpperBound = equationParametersUpperBounds[i - 1]
                });
            }
        }
        #endregion

        #region Public methods
        public void GetLinearRegressionModelInfo(out string modelText, out string xAxisTitle, out string yAxisTitle, out double[] coefficients, out double minX,  out double maxX, out string rmse, out bool normalizeValues)
        {
            modelText =
                $"{XMLWork.FindNameWithID(_outputParameter.Id.Value, Properties.Settings.Default.Languages)} = {Math.Round(_inputParameters[0].Coefficient, EquationSignificantDigitsCount)} {(_inputParameters[1].Coefficient < 0 ? "-" : "+")} {Math.Abs(Math.Round(_inputParameters[1].Coefficient, EquationSignificantDigitsCount))} · {XMLWork.FindNameWithID(_inputParameters[1].Id.Value, Properties.Settings.Default.Languages)}";
            coefficients = new[] { _inputParameters[0].Coefficient, _inputParameters[1].Coefficient };
            xAxisTitle = XMLWork.FindNameWithID(_inputParameters[1].Id.Value, Properties.Settings.Default.Languages);
            yAxisTitle = XMLWork.FindNameWithID(_outputParameter.Id.Value, Properties.Settings.Default.Languages);
            minX = _inputParameters[1].LowerBound.Value;
            maxX = _inputParameters[1].UpperBound.Value;
            rmse = RMSEString;
            normalizeValues = NormalizeValues;
        }

        public void GetMultipleRegressionModelInfo(out double[] coefficients, out string qualityParameterName, out string[] inputParameterNames, out string rmse, out bool normalizeValues)
        {
            coefficients = _inputParameters.Select(item => item.Coefficient).ToArray();
            qualityParameterName =
                XMLWork.FindNameWithID(_outputParameter.Id.Value, Properties.Settings.Default.Languages);
            inputParameterNames = _inputParameters
                .Select(item => XMLWork.FindNameWithID(item.Id.Value, Properties.Settings.Default.Languages)).ToArray();
            rmse = RMSEString;
            normalizeValues = NormalizeValues;
        }
        #endregion
    }
}
