using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DataMiningOffLine.Helpers;

namespace DataMiningOffLine.Forms.RegressionAnalysis
{
    public partial class RegressionModelsDatabase : Form
    {
        const int EquationSignificantDigitsCount = 4;//Количество значащих цифр при выводе уравнения линейной регрессии

        private List<RegressionModelEquation> _regressionModels;
        private RegressionModelsDatabaseHelper _databaseHelper;
        private int _currentIndex;

        public RegressionModelEquation SelectedEquation => _regressionModels[_currentIndex];

        public RegressionModelsDatabase(RegressionModelsDatabaseHelper databaseHelper, List<RegressionModelEquation> regressionModels, bool isAdmin)
        {
            InitializeComponent();
            _regressionModels = regressionModels;
            _databaseHelper = databaseHelper;
            _currentIndex = 0;
            ShowRegressionModel();
            if (isAdmin)
                ShowAdminInterface();
        }

        private void ShowRegressionModel()
        {
            if (_currentIndex < 0 || _currentIndex >= _regressionModels.Count)
                return;
            var currentModel = _regressionModels[_currentIndex];
            labelRegressionModelId.Text = currentModel.Id.HasValue ? currentModel.Id.Value.ToString() : string.Empty;
            labelRegressionModelCreationDate.Text = currentModel.CreationDate.ToString("yy-MMM-dd ddd");
            labelRegressionModelName.Text = currentModel.Name;
            labelOutputParameterName.Text = XMLWork.FindNameWithID(currentModel.OutputParameter.Id.Value,
                Properties.Settings.Default.Languages);
            labelCalculatedRMSEValue.Text = currentModel.RMSEString;
            labelNormalizeValues.Text = currentModel.NormalizeValues ? "Yes" : "No";
            CreateInputParametersTable(currentModel);
            CheckButtonsEnabled(_currentIndex);
        }

        private void ShowAdminInterface()
        {
            buttonDeleteCurrentModel.Visible = buttonCreateBackup.Visible = buttonLoadBackup.Visible = true;
        }

        private void CreateInputParametersTable(RegressionModelEquation model)
        {
            DataTable table = new DataTable();
            table.Columns.Add("ParamId", typeof(string));
            table.Columns.Add("ParamName", typeof(string));
            table.Columns.Add("ParamCoefficient", typeof(double));
            table.Columns.Add("ParamUpperBound", typeof(string));
            table.Columns.Add("ParamLowerBound", typeof(string));

            foreach (var parameter in model.InputParameters)
            {
                table.Rows.Add(parameter.Id.HasValue ? parameter.Id.Value.ToString() : string.Empty,
                    parameter.Id.HasValue
                        ? XMLWork.FindNameWithID(parameter.Id.Value, Properties.Settings.Default.Languages)
                        : "free term", Math.Round(parameter.Coefficient, EquationSignificantDigitsCount),
                    parameter.UpperBound.HasValue ? Math.Round(parameter.UpperBound.Value, EquationSignificantDigitsCount).ToString() : string.Empty,
                    parameter.LowerBound.HasValue ? Math.Round(parameter.LowerBound.Value, EquationSignificantDigitsCount).ToString() : string.Empty);
            }

            dataGridViewInputParametersInfo.DataSource = table;
        }

        private void CheckButtonsEnabled(int index)
        {
            buttonSelectFirstModel.Enabled = buttonSelectPreviousModel.Enabled = index > 0;
            buttonSelectLastModel.Enabled = buttonSelectNextModel.Enabled = index < _regressionModels.Count - 1;
        }

        private void buttonSelectModel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonSelectFirstModel_Click(object sender, EventArgs e)
        {
            _currentIndex = 0;
            ShowRegressionModel();
        }

        private void buttonSelectPreviousModel_Click(object sender, EventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowRegressionModel();
            }
        }

        private void buttonSelectNextModel_Click(object sender, EventArgs e)
        {
            if (_currentIndex < _regressionModels.Count - 1)
            {
                _currentIndex++;
                ShowRegressionModel();
            }
        }

        private void buttonSelectLastModel_Click(object sender, EventArgs e)
        {
            _currentIndex = _regressionModels.Count - 1;
            ShowRegressionModel();
        }

        private void buttonDeleteCurrentModel_Click(object sender, EventArgs e)
        {
            _databaseHelper.DeleteRegressionModelById(SelectedEquation.Id.Value);
            _regressionModels = _databaseHelper.GetAllRegressionModels();
            _currentIndex = 0;
            ShowRegressionModel();
        }

        private void buttonCreateBackup_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@"Resources\RegressionModelsDatabase.db"))
            {
                MessageBox.Show("Can not create database backup file!");
                return;
            }

            var result = saveFileDialogMain.ShowDialog();
            if (result == DialogResult.OK)
                File.Copy(@"Resources\RegressionModelsDatabase.db", saveFileDialogMain.FileName, true);
            MessageBox.Show("Backup file created successfully!");
        }

        private void buttonLoadBackup_Click(object sender, EventArgs e)
        {
            var result = openFileDialogMain.ShowDialog();
            if (result != DialogResult.OK)
                return;
            try
            {
                File.Copy(openFileDialogMain.FileName, @"Resources\RegressionModelsDatabase.db", true);
                MessageBox.Show("Backup file loaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _regressionModels = _databaseHelper.GetAllRegressionModels();
            _currentIndex = 0;
            ShowRegressionModel();
        }
    }
}
