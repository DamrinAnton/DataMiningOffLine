using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DM.Helper;
using DM.Data;
using System.IO;
using DataMiningOffLine.Helpers;
using DataMiningOffLine.DBClasses;
using System.Diagnostics;

namespace DataMiningOffLine.Forms
{
    public partial class HelperForm : Form   
    {
        private List<RecomendationsStruct> _error_311_RU = ExeptionsData.getInstance().GetList_error_311_RU();
        private List<RecomendationsStruct> _error_715_RU = ExeptionsData.getInstance().GetList_error_715_RU();
        private List<RecomendationsStruct> _error_710_RU = ExeptionsData.getInstance().GetList_error_710_RU();
        private List<RecomendationsStruct> _error_705_RU = ExeptionsData.getInstance().GetList_error_705_RU();
        private List<RecomendationsStruct> _error_503_RU = ExeptionsData.getInstance().GetList_error_503_RU();
        private List<RecomendationsStruct> _error_319_RU = ExeptionsData.getInstance().GetList_error_319_RU();
        private List<RecomendationsStruct> _error_307_RU = ExeptionsData.getInstance().GetList_error_307_RU();

        private List<RecomendationsStruct> _error_311_EN = ExeptionsData.getInstance().GetList_error_311_EN();
        private List<RecomendationsStruct> _error_715_EN = ExeptionsData.getInstance().GetList_error_715_EN();
        private List<RecomendationsStruct> _error_710_EN = ExeptionsData.getInstance().GetList_error_710_EN();
        private List<RecomendationsStruct> _error_705_EN = ExeptionsData.getInstance().GetList_error_705_EN();
        private List<RecomendationsStruct> _error_503_EN = ExeptionsData.getInstance().GetList_error_503_EN();
        private List<RecomendationsStruct> _error_319_EN = ExeptionsData.getInstance().GetList_error_319_EN();
        private List<RecomendationsStruct> _error_307_EN = ExeptionsData.getInstance().GetList_error_307_EN();

        /*  Вызов методов для занесения стандартных параметров в
         *  -Перечень ошибок
         */
        public HelperForm()
        {
            InitializeComponent();
            DBClasses.Parameters.getParam().connection = Directory.GetCurrentDirectory()+ @"\DataBase\DataMining.db";
            Localisation.GetInstance().Language = "EN";
            StartViewParametrs();

            русскийRUToolStripMenuItem_Click(this, new EventArgs());

            try
            {
                Analyse_other();
            }
            catch
            { MessageBox.Show(Localisation.GetInstance().Language.Equals("RU") ? "База данных пуста, заполните базу данных" : "The Database is empty, fill the Database"); }
        }

        private void FindErrors() {
            analyseListBox.Items.Clear();

            if (Localisation.GetInstance().Language.Equals("RU"))
            {

                Model("311", "Черные точки", _error_311_RU);
                Model("715/716", "Неправильная усадка при нагревании", _error_715_RU);
                Model("710", "Отклонение по Глянцу/Мутность", _error_710_RU);
                Model("705", "Недостаточное сопротивление расслаиванию", _error_705_RU);
                Model("503", "Мягкие края пленки", _error_503_RU);
                Model("319", "Желто-Коричневые полосы (следы сгоревшего материала)", _error_319_RU);
                Model("307", "Воздух/Пузыри в пленке", _error_307_RU);
            }
            else if (Localisation.GetInstance().Language.Equals("EN")) {
                Model("311", "Black dots", _error_311_EN);
                Model("715/716", "Incorrect heat shrinkage", _error_715_EN);
                Model("710", "Deflection by Gloss/Turbidity", _error_710_EN);
                Model("705", "Insufficient resistance to delamination", _error_705_EN);
                Model("503", "Soft film edges", _error_503_EN);
                Model("319", "Yellow-Brown stripes (traces of burnt material)", _error_319_EN);
                Model("307", "Air/Bubbles in the film", _error_307_EN);
            }
        }

        public void UploadDB()
        {
            DBClasses.Queries.DeleteMeasurements();
            DBClasses.ParseExcel.openExcell();
            DBClasses.ParseExcel.addMeasurements();
        }
        // Начальные параметры для компонентов View
        private void StartViewParametrs() {

            //exeptionNumberLabel.Font = new Font(exeptionNumberLabel.Font, FontStyle.Underline);

            //exeptionTypeLabel.Font = new Font(exeptionTypeLabel.Font, FontStyle.Underline);

            findingErrorsDataGridView.Columns.Add("Code", "Код ошибки");
            findingErrorsDataGridView.Columns.Add("Name", "Причина");
            findingErrorsDataGridView.Columns.Add("Param", "Название параметра");
            findingErrorsDataGridView.Columns.Add("ParamValue", "Время возникновения или значение");
            findingErrorsDataGridView.AllowUserToAddRows = false;
        }

        // Перечень ошибок и их названия
        private void Errors() {
            errorsNamesDataGridView.Rows.Clear();

            if (Localisation.GetInstance().Language.Equals("RU"))
            {
                errorsNamesDataGridView.Rows.Add(new String[] { "311", "ЧЕРНЫЕ ТОЧКИ" });
                errorsNamesDataGridView.Rows.Add(new String[] { "715/716", "НЕПРАВИЛЬНАЯ УСАДКА ПРИ НАГРЕВАНИИ" });
                errorsNamesDataGridView.Rows.Add(new String[] { "710", "ОТКЛОНЕНИЕ ПО ГЛЯНЦУЮ/МУТНОСТЬ" });
                errorsNamesDataGridView.Rows.Add(new String[] { "705", "НЕДОСТАТОЧНОЕ СОПРОТИВЛЕНИЕ РАССЛАИВАНИЮ" });
                errorsNamesDataGridView.Rows.Add(new String[] { "503", "МЯГКИЕ КРАЯ ПЛЕНКИ" });
                errorsNamesDataGridView.Rows.Add(new String[] { "319", "ЖЕЛТО-КОРИЧНЕВЫЕ ПОЛОСЫ (СЛЕДЫ СГОРЕВШЕГО МАТЕРИАЛА)" });
                errorsNamesDataGridView.Rows.Add(new String[] { "307", "ВОЗДУХ/ПУЗЫРИ В ПЛЕНКЕ" });
            }
            else if (Localisation.GetInstance().Language.Equals("EN")) {
                errorsNamesDataGridView.Rows.Add(new String[] { "311", "BLACK DOTS" });
                errorsNamesDataGridView.Rows.Add(new String[] { "715/716", "INCORRECT HEAT SHRINKAGE" });
                errorsNamesDataGridView.Rows.Add(new String[] { "710", "DEFLECTION BY GLOSS/TURBIDITY" });
                errorsNamesDataGridView.Rows.Add(new String[] { "705", "INSUFFICIENT RESISTANCE TO DELAMINATION" });
                errorsNamesDataGridView.Rows.Add(new String[] { "503", "SOFT FILM EDGES" });
                errorsNamesDataGridView.Rows.Add(new String[] { "319", "YELLOW-BROWN STRIPES (TRACES OF BURNT MATERIAL)" });
                errorsNamesDataGridView.Rows.Add(new String[] { "307", "AIR/BUBBLES IN THE FILM" });
            }
        }

        // Создание колонок и привязка списка для ошибок ### к View DataGrid
        private void CreatingGrid(List<RecomendationsStruct> errorList)
        {
            RecomendsDataGridView.AutoGenerateColumns = false;
            RecomendsDataGridView.AllowUserToAddRows = false;
            RecomendsDataGridView.DataSource = errorList;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "Reason";
            column1.HeaderText = Localisation.GetInstance().Language.Equals("RU") ? "ПРИЧИНА" : "REASON";
            column1.DataPropertyName = "Reason";
            RecomendsDataGridView.Columns.Add(column1);

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "Actions";
            column2.HeaderText = Localisation.GetInstance().Language.Equals("RU") ? "ДЕЙСТВИЕ НА ПРИЧИНУ" : "ACTION";
            column2.DataPropertyName = "Actions";
            RecomendsDataGridView.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "ControlParameter";
            column3.HeaderText = Localisation.GetInstance().Language.Equals("RU") ? "КОНТРОЛИРУЕМЫЕ ПАРАМЕТРЫ" : "CONTROLLED PARAMETERS";
            column3.DataPropertyName = "ControlParameter";
            RecomendsDataGridView.Columns.Add(column3);
        }

        // Обработка выбранной позиции из списка ошибок (Errors)
        private void ErrorsNamesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            RecomendsDataGridView.Columns.Clear();

            if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("311"))
            {
                CreatingGrid(Localisation.GetInstance().Language.Equals("RU") ? _error_311_RU : _error_311_EN);
            }
            else if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("715/716"))
            {
                CreatingGrid(Localisation.GetInstance().Language.Equals("RU") ? _error_715_RU : _error_715_EN);
            }
            else if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("710"))
            {
                CreatingGrid(Localisation.GetInstance().Language.Equals("RU") ? _error_710_RU : _error_710_EN);
            }
            else if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("705"))
            {
                CreatingGrid(Localisation.GetInstance().Language.Equals("RU") ? _error_705_RU : _error_705_EN);
            }
            else if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("503"))
            {
                CreatingGrid(Localisation.GetInstance().Language.Equals("RU") ? _error_503_RU : _error_503_EN);
            }
            else if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("319"))
            {
                CreatingGrid(Localisation.GetInstance().Language.Equals("RU") ? _error_319_RU : _error_319_EN);
            }
            else if (errorsNamesDataGridView[0, errorsNamesDataGridView.CurrentRow.Index].Value.Equals("307"))
            {
                CreatingGrid(Localisation.GetInstance().Language.Equals("RU") ? _error_307_RU : _error_307_EN);
            }
        }

        
        private void ShowAnalysingError(String errorNumber, String name) {
            //exeptionNumberLabel.Text = errorNumber;
            //exeptionTypeLabel.Text = name;

            analyseListBox.Items.Add(((Localisation.GetInstance().Language.Equals("RU") ? "Проверка ошибки №" : "Error check №") + errorNumber + " \"" + name + "\""));
        }


        /* TODO: Modify class ExeptionsData
         * -to have more exeptions 
         * -make it like collection
         */
        private void Model(String errorNumber, String name, List<RecomendationsStruct> error)
        {
            ShowAnalysingError(errorNumber, name);

            foreach (RecomendationsStruct r in error)
            {
                analyseListBox.Items.Add((Localisation.GetInstance().Language.Equals("RU") ? "Идет проверка: " : "Checking: ") + r.Reason);
                Analyse(errorNumber, r.Reason, r.ControlParameterScadaName);
            }
        }

        private void Analyse(string errorNumber, string reason, string parameter) {
            int parameterID = XMLWork.FindID(parameter);

            if (parameterID == 0)
            {
                analyseListBox.Items.Add(Localisation.GetInstance().Language.Equals("RU") ? "По данной ошибке нет параметра." : "There is no parameter for this error.");
            }
            else
            {
                foreach (OneRow row in TrainData.Train)
                {
                    // TODO: Propose Misha to do public static collection with main optimal parametrs of line
                    // Use normal params, and non magic
                    if (parameter.Equals("OPC_ABZ12_XT_K02"))
                    {
                        if (!(row.Input[66] > 60m && row.Input[66] < 80m))
                        {
                            analyseListBox.Items.Add(Localisation.GetInstance().Language.Equals("RU") ? "====> Ошибка!" : "====> Error!");
                            //findingErrorsDataGridView.Rows.Add(new String[] { errorNumber, reason, parameter, row.Date.TimeOfDay.ToString() });
                            return;
                        }
                    }
                    else if (parameter.Equals("OPC_ABZ36_XT_K02"))
                    {
                        if (!(row.Input[69] > 70m && row.Input[69] < 90m))
                        {
                            analyseListBox.Items.Add(Localisation.GetInstance().Language.Equals("RU") ? "====> Ошибка!" : "====> Error!");
                            //findingErrorsDataGridView.Rows.Add(new String[] { errorNumber, reason, parameter, row.Date.TimeOfDay.ToString() });
                            return;
                        }
                    }
                    else
                    {
                        if (!(row.Input[parameterID] > 10.0m && row.Input[parameterID] < 40.0m))
                        {
                            analyseListBox.Items.Add(Localisation.GetInstance().Language.Equals("RU") ? "====> Ошибка!" : "====> Error!");
                            //findingErrorsDataGridView.Rows.Add(new String[] { errorNumber, reason, parameter, row.Date.TimeOfDay.ToString()});
                            return;
                        }
                    }
                }
            }
            analyseListBox.Items.Add(Localisation.GetInstance().Language.Equals("RU") ? "Ошибки не обнаружено" : "No errors found");
            analyseListBox.Items.Add("");
        }

        private void RecomendsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex = RecomendsDataGridView.CurrentCell.RowIndex;
            try
            {
                label7.Text = RecomendsDataGridView.Rows[rowindex].Cells[0].Value.ToString();
                label8.Text = RecomendsDataGridView.Rows[rowindex].Cells[1].Value.ToString();
                label9.Text = RecomendsDataGridView.Rows[rowindex].Cells[2].Value.ToString();
            }
            catch { }
        }

        private void findingErrorsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex = findingErrorsDataGridView.CurrentCell.RowIndex;
            label7.Text = findingErrorsDataGridView.Rows[rowindex].Cells[1].Value.ToString();

            foreach (RecomendationsStruct ex in _error_715_RU) { 
                if(ex.Reason.Equals(label7.Text)){
                    label8.Text = ex.Actions;
                    label9.Text = ex.ControlParameter;
                }
            }
            
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            
        }

        // 31.05.2018 здесь исправляем!!!!!!!!!!!!!!!!
        private void Analyse_other() {

            //DBClasses.Queries.Update(@"INSERT INTO Measurements (value) VALUES ('1');");

            List<Recomend> rec = Queries.GetListOfRecomend();
            string result = null;

            foreach (Recomend r in rec)
            {
                result = Queries.GetConstraintsErrors(r.param_st);


                if (result != null)
                {
                    var info = (HelpInfo)null;

                    info = Queries.GetInfo(r.reason_id.ToString(), r.param_st.ToString());

                    findingErrorsDataGridView.Rows.Add(new String[] { info.Esnumber, info.Reason_text, r.param_st, result });
                }
            }

        }

        private void спрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EC_INformation().Show();
        }
        
        private void создатьОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string path = Application.StartupPath + "\\" + DateTime.Today.ToString("D") + " " + string.Format("{0:HH:mm:ss tt}", DateTime.Now).Replace(':', '_') + ".txt";

            try
            {

                // Delete the file if it exists. In fact, there is no situatuon when we need this.
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                string data = "";
                foreach (DataGridViewRow row in findingErrorsDataGridView.Rows) {
                    data += row.Cells["Code"].Value + " : " + row.Cells["Name"].Value + " : " + row.Cells["ParamValue"].Value + Environment.NewLine;
                }

                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    byte[] info;

                    if (Localisation.GetInstance().Language.Equals("RU")) {
                        info = new UTF8Encoding(true).GetBytes("ОТЧЕТ ПО ВОЗНИКНОВЕНИЮ НЕШТАТНЫХ СИТУАЦИЙ ЗА " + DateTime.Today.ToString("D") + string.Format("{0:HH:mm:ss tt}", DateTime.Now)/*.Replace(':', ' ')*/
                            + Environment.NewLine + Environment.NewLine
                            + Environment.NewLine + "По технологическим данным, обнаружены следующие нештатные ситуации: "
                            + Environment.NewLine + Environment.NewLine + "КОД ОШИБКИ : ПРИЧИНА : ВРЕМЯ ВОЗНИКНОВЕНИЯ"
                            + Environment.NewLine + Environment.NewLine + data
                            + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Чистка экструдера проводилась: " + extruderCleaningDataTextBox.Text);
                        // Add some information to the file.
                    }
                    else {
                        info = new UTF8Encoding(true).GetBytes("STATISTICAL ACCOUNT REPORT FOR" + DateTime.Today.ToString("D") + string.Format("{0: HH: mm: ss tt}", DateTime.Now) /*.Replace(':', '') */
                             + Environment.NewLine + Environment.NewLine
                             + Environment.NewLine + "According to technological data, the following abnormal situations were detected:"
                             + Environment.NewLine + Environment.NewLine + "ERROR CODE: CAUSE: TIME OF OCCURRENCE"
                             + Environment.NewLine + Environment.NewLine + data
                             + Environment.NewLine + Environment.NewLine + Environment.NewLine + "The extruder was cleaned:" + extruderCleaningDataTextBox.Text);
                    }
                        fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // Open the file in standart programm.
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch { }
        }

        #region Language
        private void русскийRUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Localisation.GetInstance().Language.Equals("RU")) return;

            Localisation.GetInstance().Language = "RU";

            this.Text = "РЕКОМЕНДАЦИИ ПО УСТРАНЕНИЮ ОШИБОК";
            граничащиеПараметрыToolStripMenuItem.Text = "НАСТРОЙКИ ПАРАМЕТРОВ";
            спрToolStripMenuItem.Text = "СПРАВКА НЕШТАТНЫХ СИТУАЦИЙ";
            трендыToolStripMenuItem.Text = "ТРЕНДЫ";
            сменитьЯзыкToolStripMenuItem.Text = "СМЕНИТЬ ЯЗЫК";
            создатьОтчетToolStripMenuItem.Text = "СОЗДАТЬ ОТЧЕТ";
            
            recountToolStripMenuItem.Text = "ПЕРЕСЧИТАТЬ";
            граничащиеПараметрыToolStripMenuItem1.Text = "ГРАНИЧАЩИЕ ПАРАМЕТРЫ";
            чисткаЭкструдераToolStripMenuItem.Text = "ЧИСТКА ЭКСТРУДЕРА";
            алгоритмыНСToolStripMenuItem.Text = "АЛГОРИТМЫ НС";

            groupBox1.Text = "ПРОВЕРКА НА ОШИБКИ";
            groupBox6.Text = "НАЙДЕНЫЕ ОШИБКИ";

            groupBox7.Text = "КОД/ТИП НЕШТАТНОЙ СИТУАЦИИ";
            groupBox8.Text = "НЕШТАТНЫЕ СИТУАЦИИ";

            groupBox3.Text = "ПРИЧИНА";
            groupBox4.Text = "ДЕЙСТВИЕ НА ПРИЧИНУ";
            groupBox5.Text = "КОНТРОЛИРУЕМЫЕ ПАРАМЕТРЫ";
            groupBox2.Text = "ПОСЛЕДНЯЯ ЧИСТКА ЭКСТРУДЕРА";

            label3.Text = "ДАТА:";
            label4.Text = "ПРОЧИСТКА:";

            errorsNamesDataGridView.Columns["Column1"].HeaderText = "КОД";
            errorsNamesDataGridView.Columns["Column2"].HeaderText = "ТИП";

            findingErrorsDataGridView.Columns["Code"].HeaderText = "КОД ОШИБКИ";
            findingErrorsDataGridView.Columns["Name"].HeaderText = "ПРИЧИНА";
            findingErrorsDataGridView.Columns["Param"].HeaderText = "НАЗВАНИЕ ПАРАМЕТРА";
            findingErrorsDataGridView.Columns["ParamValue"].HeaderText = "ВРЕМЯ ВОЗНИКНОВЕНИЯ";

            Errors();
            FindErrors();

            ChangeFindingErrorsLanguage();
            ExtruderCleaning();

        }

        private void englishENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Localisation.GetInstance().Language.Equals("EN")) return;

            Localisation.GetInstance().Language = "EN";

            this.Text = "EMERGENCY SITUATION RECOMMENDATIONS";
            граничащиеПараметрыToolStripMenuItem.Text = "PARAMETERS SETTINGS";
            спрToolStripMenuItem.Text = "E.S. HELP";
            трендыToolStripMenuItem.Text = "TRENDS";
            сменитьЯзыкToolStripMenuItem.Text = "LANGUAGE";
            создатьОтчетToolStripMenuItem.Text = "CREATE REPORT";
            
            recountToolStripMenuItem.Text = "RECOUNT";
            граничащиеПараметрыToolStripMenuItem1.Text = "BORDER PARAMETERS";
            чисткаЭкструдераToolStripMenuItem.Text = "CLEANING THE EXTRUDER";
            алгоритмыНСToolStripMenuItem.Text = "ES ALGORITHMS";

            groupBox1.Text = "CHECK FOR ERRORS";
            groupBox6.Text = "FOUND ERRORS";

            groupBox7.Text = "CODE/TYPE OF EMERGENCY SITUATION";
            groupBox8.Text = "EMERGENCY SITUATIONS";

            groupBox3.Text = "REASON";
            groupBox4.Text = "ACTION FOR THE REASON";
            groupBox5.Text = "CONTROLLED PARAMETERS";
            groupBox2.Text = "LAST CLEANING OF THE EXTRUDER";

            label3.Text = "DATE:";
            label4.Text = "CLEAR:";

            errorsNamesDataGridView.Columns["Column1"].HeaderText = "ERROR CODE";
            errorsNamesDataGridView.Columns["Column2"].HeaderText = "TYPE";

            findingErrorsDataGridView.Columns["Code"].HeaderText = "ERROR CODE";
            findingErrorsDataGridView.Columns["Name"].HeaderText = "REASON";
            findingErrorsDataGridView.Columns["Param"].HeaderText = "PARAMETER NAME";
            findingErrorsDataGridView.Columns["ParamValue"].HeaderText = "TIME OF OCCURANCE";

            Errors();
            FindErrors();

            ChangeFindingErrorsLanguage();
            ExtruderCleaning();
        }

        #endregion

        private void ChangeFindingErrorsLanguage() {
            // foreach элемент в таблице, замена + написать запрос
            foreach (DataGridViewRow row in findingErrorsDataGridView.Rows)
            {
                string name = row.Cells["Name"].Value.ToString();
                row.Cells["Name"].Value = Queries.GetLocalization(row.Cells["Code"].Value.ToString(), row.Cells["Name"].Value.ToString(), Localisation.GetInstance().Language.Equals("RU") ? "RU" : "EN");
            }
        }

        private void findingErrorsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string param = findingErrorsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            string number = findingErrorsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            string reason = findingErrorsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            new Trends(param, number, reason).Show();

        }

       

        private void recountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                findingErrorsDataGridView.Rows.Clear();
            }
            catch { }

            Analyse_other();
        }

        private void ExtruderCleaning()
        {
            string dateCleaning = Queries.GetExtruderCleaningData();
            int days = GetDays(dateCleaning);

            if (days < 14)
            {
                extruderCleaningRecomendationsTextBox.Text = Localisation.GetInstance().Language.Equals("RU") ? "НЕ НУЖНА" : "NOT NECESSARY";
                extruderCleaningRecomendationsTextBox.BackColor = Color.FromKnownColor(KnownColor.Control);
                extruderCleaningRecomendationsTextBox.ForeColor = Color.Green;
            }
            else if (days > 14 && days < 30)
            {
                extruderCleaningRecomendationsTextBox.Text = Localisation.GetInstance().Language.Equals("RU") ? "РЕКОМЕНДУЕТСЯ" : "RECOMENDED";
                extruderCleaningRecomendationsTextBox.BackColor = Color.FromKnownColor(KnownColor.Control);
                extruderCleaningRecomendationsTextBox.ForeColor = Color.Yellow;
            }
            else
            {
                extruderCleaningRecomendationsTextBox.Text = Localisation.GetInstance().Language.Equals("RU") ? "НЕОБХОДИМА" : "NECESSARY";
                extruderCleaningRecomendationsTextBox.BackColor = Color.FromKnownColor(KnownColor.Control);
                extruderCleaningRecomendationsTextBox.ForeColor = Color.Red;
            }


            extruderCleaningDataTextBox.Text = dateCleaning;
            extruderCleaningDataTextBox.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private int GetDays(string cleaning)
        {
            DateTime dt1 = ConvertToDateTime(cleaning);
            DateTime dt2 = DateTime.Today;
            
            return (dt2 - dt1).Days;
        }

        private static DateTime ConvertToDateTime(string value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch (FormatException)
            {
                MessageBox.Show("'{0}' is not in the proper format.", value);
            }

            return new DateTime();
        }

        private void граничащиеПараметрыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Helper_parameters().Show();
        }

        private void чисткаЭкструдераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ExtruderCleaning().Show();
        }

        private void алгоритмыНСToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ESAlgs().Show();
        }
    }
}
