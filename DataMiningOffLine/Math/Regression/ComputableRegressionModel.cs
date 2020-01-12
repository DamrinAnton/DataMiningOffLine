using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DM.Data;

namespace DataMiningOffLine
{
    public class ComputableRegressionModel
    {
        const double DataSensitivityThreshhold = 0.01;

        #region Internal classes
        public class ModelParameter
        {
            public int Id { get; set; }
            public double Coefficient { get; set; }
            public double LowerBound { get; set; }
            public double UpperBound { get; set; }
        }
        #endregion

        #region Ctor

        /// <summary>
        /// Main constructor for computable regression model
        /// </summary>
        /// <param name="outputParameterId">Id of the output parameter of the model</param>
        /// <param name="inputParametersIds">List of Ids of input parameters of the model</param>
        public ComputableRegressionModel(int outputParameterId, List<int> inputParametersIds)
        {
            _outputParameter = new ModelParameter {Id = outputParameterId};
            _inputParameters = new List<ModelParameter>();
            foreach (var parameterId in inputParametersIds)
            {
                _inputParameters.Add(new ModelParameter {Id = parameterId});
            }

            _modelTrained = false;
        }

        #endregion

        #region Private members

        private ModelParameter _outputParameter;
        private List<ModelParameter> _inputParameters;
        private double _freeParameterValue;
        private bool _modelTrained;

        #endregion

        #region Public members

        public ModelParameter OutputParameter => _outputParameter;
        public List<ModelParameter> InputParameters => _inputParameters;
        public double FreeParameterValue => _freeParameterValue;

        #endregion

        #region Public methods

        /// <summary>
        /// Method that trains the model,
        /// should be called before calling Fit method
        /// </summary>
        /// <param name="inputData">The data to train the model</param>
        /// <param name="normalizeValues">Use data normalization or not (better not to use)</param>
        public void Train(List<OneRow> inputData, bool normalizeValues = false)
        {
            double[,] processParametersData;
            double[] qualityParameterData;

            processParametersData = new double[inputData.Count, _inputParameters.Count + 1];
            qualityParameterData = new double[inputData.Count];

            //Setting first column of input values of regression matrix X with "1" values
            for (int i = 0; i < processParametersData.GetLength(0); i++)
                processParametersData[i, 0] = 1;

            List<double> parametersLowerBounds = new List<double>(), parametersUpperBounds = new List<double>();

            //Setting all input data
            for (int paramNumber = 0; paramNumber < _inputParameters.Count; paramNumber++)
            {
                double minValue = double.MaxValue, maxValue = double.MinValue;
                for (int rowNumber = 0; rowNumber < TrainData.Train.Count; rowNumber++)
                {
                    processParametersData[rowNumber, paramNumber + 1] =
                        (double)inputData[rowNumber].Input[_inputParameters[paramNumber].Id];

                    if (processParametersData[rowNumber, paramNumber + 1] < minValue)
                        minValue = processParametersData[rowNumber, paramNumber + 1];

                    if (processParametersData[rowNumber, paramNumber + 1] > maxValue)
                        maxValue = processParametersData[rowNumber, paramNumber + 1];
                }
                parametersLowerBounds.Add(minValue);
                parametersUpperBounds.Add(maxValue);
            }

            //Setting output data
            for (int i = 0; i < TrainData.Train.Count; i++)
                qualityParameterData[i] = (double)inputData[i].Output[_outputParameter.Id];

            //Checking if data is correct
            for (int j = 1; j < processParametersData.GetLength(1); j++)
            {
                bool throwData = true;
                for (int i = 0; i < processParametersData.GetLength(0); i++)
                    if (Math.Abs(processParametersData[i, j]) > DataSensitivityThreshhold)
                    {
                        throwData = false;
                        break;
                    }
                if (throwData)
                    throw new Exception("Incorrect input data");
            }

            if (!qualityParameterData.Any(item => Math.Abs(item) > DataSensitivityThreshhold))
                throw new Exception("Incorrect input data");

            //Call for multiple regression calculation method
            var coefficients = Regression.MultipleRegression(processParametersData, qualityParameterData, normalizeValues);

            _freeParameterValue = coefficients[0];
            for (int i = 1; i < coefficients.Length; i++)
            {
                _inputParameters[i - 1].Coefficient = coefficients[i];
                _inputParameters[i - 1].LowerBound = parametersLowerBounds[i - 1];
                _inputParameters[i - 1].UpperBound = parametersUpperBounds[i - 1];
            }

            _modelTrained = true;
            // double rmse = Regression.MultipleRMSE(processParametersData, qualityParameterData, coefficients);
        }

        /// <summary>
        /// Method that uses the model to predict values
        /// </summary>
        /// <param name="inputData">The data to predict</param>
        /// <param name="checkBounds">Should the model check the bounds of the input parameter values;
        /// If selected, writes debug errors when parameter value in <see cref="inputData"/> is out of bounds</param>
        /// <returns>The list of predicted values, the same length as the <see cref="inputData"/></returns>
        public List<double> Fit(List<OneRow> inputData, bool checkBounds = false)
        {
            if (!_modelTrained)
                throw new Exception("The model was not trained");
            List<double> resultValues = new List<double>(inputData.Count);

            if (checkBounds)
            {
                foreach (var inputParameter in _inputParameters)
                {
                    var dataMax = (double)inputData.Max(item => item.Input[inputParameter.Id]);
                    var dataMin = (double)inputData.Min(item => item.Input[inputParameter.Id]);

                    if (dataMin < inputParameter.LowerBound || dataMax > inputParameter.UpperBound)
                        Debug.WriteLine($"Parameter with id = {inputParameter.Id} is out of bounds");
                }
            }

            foreach (var timeSlice in inputData)
            {
                var calculatedValue = _freeParameterValue;
                foreach (var inputParameter in _inputParameters)
                {
                    calculatedValue += inputParameter.Coefficient * (double)timeSlice.Input[inputParameter.Id];
                }
                resultValues.Add(calculatedValue);
            }

            return resultValues;
        }

        #endregion

    }
}
