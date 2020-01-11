using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace DataMiningOffLine
{
    public partial class InterfaceAdminBDND : Form
    {

        DataBaseND db = new DataBaseND { nameDB = "DBCriterionND.db" };
        DataBaseND dbUser = new DataBaseND { nameDB = "BDUsersND.db" };
        Option option = new Option();

        Dictionary<int, string> atributsDB = new Dictionary<int, string>
        {
            { 1, "Method"},
                { 2, "ID_method"},
                { 3, "RuName_method"},
                { 4, "EnName_method"},
               // { 5, "Description_method"},
            { 6, "Rule"},
                { 7, "ID_rule"},
                { 8, "EnName_rule"},
                { 9, "RuName_rule"},
            { 10, "Criterion"},
                { 11, "ID_criterion"},
                { 12, "RuName_criterion"},
                { 13, "EnName_criterion"},
                { 14, "Description_crit"},
            { 15, "Choose_Rule"},
                { 16, "ID_chooseRule"},
                { 17, "ID_method"},
                { 18, "ID_rule"},
                { 19, "ID_criterion"},
            { 20, "Value"},
                { 21, "ID_value"},
                { 22, "ID_criterion"},
                { 23, "Significance_level"},
                { 24, "Freedom_degree"},
                { 25, "Value"}
        };

        Dictionary<int, string> atributsDBUser = new Dictionary<int, string>
        {
            { 1, "User"},
            { 2, "ID_user"},
            { 3, "Login_user"},
            { 4, "Password_user"},
            { 5, "Name_user"},
            { 6, "Status"}
        };
        List<string> languageString = new List<string>();

        public InterfaceAdminBDND(Option options)
        {
            option = options;
            AddListLanguageString();
            InitializeComponent();
            db.ConnectionBD();
            dbUser.ConnectionBD();
            CreatTableComboBox(options);
            UpdateComboBoxIDChooseParameter();
            UpdateListComboBoxMethod();
            UpdateUserListComboBox();
            UpdateListComboBoxCriterion();
            UpdateListComboBoxRule();
            UpdateComboBoxIDValue();
        }

        private void CreatTableComboBox(Option options)
        {
            if (options.language)
            {
                SelectedRussianLanguage();
            }
            else
            {
                SelectedEnglishLanguage();
            }
            TableComboBox.Items.Add(languageString[0]);
            TableComboBox.Items.Add(languageString[1]);
            TableComboBox.Items.Add(languageString[2]);
            TableComboBox.Items.Add(languageString[3]);
            TableComboBox.Items.Add(languageString[4]);
            TableComboBox.SelectedIndex = 0;
        }

        private void UpdateListComboBoxMethod()
        {
            MethodListBox.Items.Clear();
            MethodChooseParameterComboBox.Items.Clear();
            string table = atributsDB[1];

            string field;
            if (option.language)
                field = atributsDB[3];
            else
                field = atributsDB[4];
            List<string> methods = db.SelectDB(table, field);
            foreach (var method in methods)
            {
                MethodListBox.Items.Add(method);
                MethodChooseParameterComboBox.Items.Add(method);
            }

            IDMethodComboBox.Items.Clear();
            field = atributsDB[2];
            methods = db.SelectDB(table, field);
            foreach (var method in methods)
            {
                IDMethodComboBox.Items.Add(method);
            }
            if (TableComboBox.Text == atributsDB[1])
                TableMainTable(atributsDB[1], new List<string> { atributsDB[2], atributsDB[3], atributsDB[4] });
        }

        private void UpdateUserListComboBox()
        {
            IDUserComboBox.Items.Clear();
            UserListBox.Items.Clear();
            string table = atributsDBUser[1];
            string field = atributsDBUser[5];
            List<string> userNames = dbUser.SelectDB(table, field);
            field = atributsDBUser[3];
            List<string> userLogins = dbUser.SelectDB(table, field);

            for (int index = 0; index < userNames.Count(); index++)
            {
                StringBuilder str = new StringBuilder();
                str.Append(userNames[index]);
                str.Append("/");
                str.Append(userLogins[index]);
                UserListBox.Items.Add(str.ToString());
            }

            field = atributsDBUser[2];
            userNames = dbUser.SelectDB(table, field);
            foreach (var id in userNames)
            {
                IDUserComboBox.Items.Add(id);
            }
            TableUser();
        }


        private void UpdateListComboBoxCriterion()
        {
            CriterionListBox.Items.Clear();
            CriterionChooseParameterComboBox.Items.Clear();
            CriterionValueComboBox.Items.Clear();
            string table = atributsDB[10];
            string field;
            if (option.language)
                field = atributsDB[12];
            else
                field = atributsDB[13];
            List<string> criterions = db.SelectDB(table, field);
            foreach (var criterion in criterions)
            {
                CriterionListBox.Items.Add(criterion);
                CriterionChooseParameterComboBox.Items.Add(criterion);
                CriterionValueComboBox.Items.Add(criterion);
            }
            CriterionChooseParameterComboBox.Items.Add(" ");

            IDCriterionComboBox.Items.Clear();
            field = atributsDB[11];
            criterions = db.SelectDB(table, field);
            foreach (var criterion in criterions)
            {
                IDCriterionComboBox.Items.Add(criterion);
            }
            if (TableComboBox.Text == atributsDB[10])
                TableMainTable(atributsDB[10], new List<string> { atributsDB[11], atributsDB[12], atributsDB[13], atributsDB[14] });
        }

        private void UpdateListComboBoxRule()
        {
            RuleListBox.Items.Clear();
            RuleChooseParameterComboBox.Items.Clear();
            string table = atributsDB[6];
            string field;
            if (option.language)
                field = atributsDB[9];
            else
                field = atributsDB[8];
            List<string> rules = db.SelectDB(table, field);
            foreach (var rule in rules)
            {
                RuleListBox.Items.Add(rule);
                RuleChooseParameterComboBox.Items.Add(rule);
            }

            IDRuleComboBox.Items.Clear();
            field = atributsDB[7];
            rules = db.SelectDB(table, field);
            foreach (var rule in rules)
            {
                IDRuleComboBox.Items.Add(rule);
            }
            if (TableComboBox.Text == atributsDB[6])
                TableMainTable(atributsDB[6], new List<string> { atributsDB[7], atributsDB[9], atributsDB[8] });
        }

        private void UpdateComboBoxIDChooseParameter()
        {
            IDChooseParameterComboBox.Items.Clear();
            string table = atributsDB[15];
            string field = atributsDB[16];
            List<string> chooseParameters = db.SelectDB(table, field);
            foreach (var chooseParameter in chooseParameters)
            {
                IDChooseParameterComboBox.Items.Add(chooseParameter);
            }
        }

        private void UpdateComboBoxIDValue()
        {
            IDValueComboBox.Items.Clear();
            string table = atributsDB[20];
            string field = atributsDB[21];
            List<string> values = db.SelectDB(table, field);
            foreach (var value in values)
            {
                IDValueComboBox.Items.Add(value);
            }
        }


        private void AddMethodButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(EnNameMethodTextBox.Text) || String.IsNullOrWhiteSpace(RuNameMethodTextBox.Text))
                    throw new Exception(languageString[5]);
                if (db.UseValueInDB(atributsDB[1], atributsDB[2], atributsDB[3], RuNameMethodTextBox.Text))
                    throw new Exception(languageString[6]);
                if (db.UseValueInDB(atributsDB[1], atributsDB[2], atributsDB[4], EnNameMethodTextBox.Text))
                    throw new Exception(languageString[7]);
                List<string> values = new List<string>() { RuNameMethodTextBox.Text, EnNameMethodTextBox.Text };
                List<string> fields = new List<string>() { atributsDB[3], atributsDB[4] };
                string table = atributsDB[1];
                db.AddDataIntoBD(table, fields, values);
                UpdateListComboBoxMethod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddChooseParameterButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(RuleChooseParameterComboBox.Text) ||
                String.IsNullOrWhiteSpace(MethodChooseParameterComboBox.Text))
                MessageBox.Show(languageString[5]);
            string nameRule = RuleChooseParameterComboBox.Text;
            string nameCriterion = CriterionChooseParameterComboBox.Text;
            string nameMethod = MethodChooseParameterComboBox.Text;
            List<string> id_method = db.SelectDB(atributsDB[1], new List<string>() { atributsDB[2] },
                                                 new List<string>() { option.language ? atributsDB[3] : atributsDB[4] }, new List<string>() { nameMethod });
            if (id_method.Count() != 1)
                throw new Exception(languageString[8]);
            List<string> id_criterion = new List<string>();
            if (!String.IsNullOrWhiteSpace(CriterionChooseParameterComboBox.Text))
            {
                id_criterion = db.SelectDB(atributsDB[10], new List<string>() { atributsDB[11] },
                                                    new List<string>() { option.language ? atributsDB[12] : atributsDB[13] }, new List<string>() { nameCriterion });
                if (id_criterion.Count() != 1)
                    throw new Exception(languageString[8]);
            }
            else
                id_criterion.Add("0");
            List<string> id_rule = db.SelectDB(atributsDB[6], new List<string>() { atributsDB[7] },
                                               new List<string>() { option.language ? atributsDB[9] : atributsDB[8] }, new List<string>() { nameRule });
            if (id_rule.Count() != 1)
                throw new Exception(languageString[8]);
            List<string> id_chooseParameter = db.SelectDB(atributsDB[15], new List<string>() { atributsDB[16] },
                                                          new List<string>() { atributsDB[17], atributsDB[18], atributsDB[19] },
                                                          new List<string>() { id_method.First(), id_rule.First(), id_criterion.First() });
            if (id_chooseParameter.Count() != 0)
                throw new Exception(languageString[9]);
            List<string> values = new List<string>() { id_method.First(), id_criterion.First(),
                                                       id_rule.First()};
            List<string> fields = new List<string>() { atributsDB[17], atributsDB[19], atributsDB[18] };
            string table = atributsDB[15];
            db.AddDataIntoBD(table, fields, values);
            if (TableComboBox.Text == atributsDB[15])
                TableChooseParameter();
            UpdateComboBoxIDChooseParameter();
        }

        private void AddRuleButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(EnNameRuleTextBox.Text) || String.IsNullOrWhiteSpace(RuNameRuleTextBox.Text))
                throw new Exception(languageString[5]);
            if (db.UseValueInDB(atributsDB[6], atributsDB[7], atributsDB[9], RuNameRuleTextBox.Text))
                throw new Exception(languageString[10]);
            if (db.UseValueInDB(atributsDB[6], atributsDB[7], atributsDB[8], EnNameRuleTextBox.Text))
                throw new Exception(languageString[11]);
            List<string> values = new List<string>() { RuNameRuleTextBox.Text, EnNameRuleTextBox.Text };
            List<string> fields = new List<string>() { atributsDB[9], atributsDB[8] };
            string table = atributsDB[6];
            db.AddDataIntoBD(table, fields, values);
            UpdateListComboBoxRule();
        }

        private void AddCriterionButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(EnNameCriterionTextBox.Text) || String.IsNullOrWhiteSpace(RuNameCriterionTextBox.Text))
                throw new Exception(languageString[5]);
            if (db.UseValueInDB(atributsDB[10], atributsDB[11], atributsDB[12], RuNameCriterionTextBox.Text))
                throw new Exception(languageString[12]);
            if (db.UseValueInDB(atributsDB[10], atributsDB[11], atributsDB[13], EnNameCriterionTextBox.Text))
                throw new Exception(languageString[13]);
            List<string> values = new List<string>() { RuNameCriterionTextBox.Text, EnNameCriterionTextBox.Text,
                                                       DescriptionCriterionTextBox.Text};
            List<string> fields = new List<string>() { atributsDB[12], atributsDB[13], atributsDB[14] };
            string table = atributsDB[10];
            db.AddDataIntoBD(table, fields, values);
            UpdateListComboBoxCriterion();
        }

        private void AddValueButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CriterionValueComboBox.Text) ||
                String.IsNullOrWhiteSpace(ValueCriterionValueTextBox.Text))
                throw new Exception(languageString[5]);
            ValueCriterionValueTextBox.Text = ValueCriterionValueTextBox.Text.Replace(",", ".");
            SigLevelValueTextBox.Text = SigLevelValueTextBox.Text.Replace(",", ".");
            FreedomDegreeValueTextBox.Text = Convert.ToInt32(FreedomDegreeValueTextBox.Text).ToString();
            string nameCriterion = CriterionValueComboBox.Text;
            List<string> id_criterion = db.SelectDB(atributsDB[10], new List<string>() { atributsDB[11] }, new List<string>() { option.language ? atributsDB[12] : atributsDB[13] }, new List<string>() { nameCriterion });
            if (id_criterion.Count() != 1)
                throw new Exception(languageString[8]);

            List<string> id_value = db.SelectDB(atributsDB[20], new List<string>() { atributsDB[21] },
                                                          new List<string>() { atributsDB[22], atributsDB[23], atributsDB[24], atributsDB[25] },
                                                          new List<string>() { id_criterion.First(), SigLevelValueTextBox.Text,
                                                                               FreedomDegreeValueTextBox.Text,
                                                                               ValueCriterionValueTextBox.Text });
            if (id_value.Count() != 0)
                throw new Exception(languageString[14]);

            List<string> values = new List<string>() { id_criterion.First(), SigLevelValueTextBox.Text,
                                                       FreedomDegreeValueTextBox.Text, ValueCriterionValueTextBox.Text};
            List<string> fields = new List<string>() { atributsDB[22], atributsDB[23], atributsDB[24], atributsDB[25] };
            string table = atributsDB[20];
            db.AddDataIntoBD(table, fields, values);
            if (TableComboBox.Text == atributsDB[20])
                TableValue();
        }

        private void UpdateMethodButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDMethodComboBox.Text) ||
                String.IsNullOrWhiteSpace(EnNameMethodTextBox.Text) || String.IsNullOrWhiteSpace(RuNameMethodTextBox.Text))
                throw new Exception(languageString[5]);
            List<string> values = new List<string>() { RuNameMethodTextBox.Text, EnNameMethodTextBox.Text };
            List<string> fields = new List<string>() { atributsDB[3], atributsDB[4] };
            db.UpdateDataInBD(atributsDB[1], fields, values, atributsDB[2], IDMethodComboBox.Text);
            UpdateListComboBoxMethod();
        }

        private void UpdateRuleButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDRuleComboBox.Text) || String.IsNullOrWhiteSpace(EnNameRuleTextBox.Text) ||
                String.IsNullOrWhiteSpace(RuNameRuleTextBox.Text))
                throw new Exception(languageString[5]);
            List<string> values = new List<string>() { RuNameRuleTextBox.Text, EnNameRuleTextBox.Text };
            List<string> fields = new List<string>() { atributsDB[9], atributsDB[8] };
            db.UpdateDataInBD(atributsDB[6], fields, values, atributsDB[7], IDRuleComboBox.Text);
            UpdateListComboBoxRule();
        }

        private void UpdateChooseParameterButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(RuleChooseParameterComboBox.Text) ||
                String.IsNullOrWhiteSpace(MethodChooseParameterComboBox.Text))
                throw new Exception(languageString[5]);
            string nameRule = RuleChooseParameterComboBox.Text;
            string nameCriterion = CriterionChooseParameterComboBox.Text;
            string nameMethod = MethodChooseParameterComboBox.Text;
            List<string> id_method = db.SelectDB(atributsDB[1], new List<string>() { atributsDB[2] },
                                                 new List<string>() { option.language ? atributsDB[3] : atributsDB[4] }, new List<string>() { nameMethod });
            if (id_method.Count() != 1)
                throw new Exception(languageString[8]);

            List<string> id_criterion = new List<string>();
            if (!String.IsNullOrWhiteSpace(CriterionChooseParameterComboBox.Text))
            {
                id_criterion = db.SelectDB(atributsDB[10], new List<string>() { atributsDB[11] },
                                                    new List<string>() { option.language ? atributsDB[12] : atributsDB[13] }, new List<string>() { nameCriterion });
                if (id_criterion.Count() != 1)
                    throw new Exception(languageString[8]);
            }
            else
                id_criterion.Add("0");
            List<string> id_rule = db.SelectDB(atributsDB[6], new List<string>() { atributsDB[7] },
                                               new List<string>() { option.language ? atributsDB[9] : atributsDB[8] }, new List<string>() { nameRule });
            if (id_rule.Count() != 1)
                throw new Exception(languageString[8]);

            List<string> values = new List<string>() { id_method.First(), id_criterion.First(),
                                                       id_rule.First()};
            List<string> fields = new List<string>() { atributsDB[17], atributsDB[19], atributsDB[18] };
            string table = atributsDB[1];
            db.UpdateDataInBD(atributsDB[15], fields, values, atributsDB[16], IDChooseParameterComboBox.Text);
            if (TableComboBox.Text == atributsDB[15])
                TableChooseParameter();
        }

        private void UpdateValueButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CriterionValueComboBox.Text) ||
                String.IsNullOrWhiteSpace(ValueCriterionValueTextBox.Text))
                throw new Exception(languageString[5]);
            ValueCriterionValueTextBox.Text = ValueCriterionValueTextBox.Text.Replace(".", ",");
            SigLevelValueTextBox.Text = SigLevelValueTextBox.Text.Replace(".", ",");
            string nameCriterion = CriterionValueComboBox.Text;
            List<string> id_criterion = db.SelectDB(atributsDB[10], new List<string>() { atributsDB[11] },
                                                    new List<string>() { option.language ? atributsDB[12] : atributsDB[13] }, new List<string>() { nameCriterion });
            if (id_criterion.Count() != 1)
                throw new Exception(languageString[8]);
            List<string> values = new List<string>() {id_criterion.First(), SigLevelValueTextBox.Text,
                                                      FreedomDegreeValueTextBox.Text, ValueCriterionValueTextBox.Text};
            List<string> fields = new List<string>() { atributsDB[22], atributsDB[23], atributsDB[24], atributsDB[25] };
            db.UpdateDataInBD(atributsDB[20], fields, values, atributsDB[21], IDValueComboBox.SelectedItem.ToString());
            if (TableComboBox.Text == atributsDB[20])
                TableValue();
        }


        private void DeleteMethodButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDMethodComboBox.Text))
                throw new Exception(languageString[5]);
            List<string> tables = new List<string> { atributsDB[15], atributsDB[1] };
            db.DeleteDataInDB(tables, atributsDB[2], Convert.ToInt32(IDMethodComboBox.Text));
            UpdateListComboBoxMethod();
            UpdateComboBoxIDChooseParameter();
        }

        private void DeleteChooseParameterButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDChooseParameterComboBox.Text))
                throw new Exception(languageString[5]);
            List<string> tables = new List<string> { atributsDB[15] };
            db.DeleteDataInDB(tables, atributsDB[16], Convert.ToInt32(IDChooseParameterComboBox.Text));
            UpdateComboBoxIDChooseParameter();
            if (TableComboBox.Text == atributsDB[15])
                TableChooseParameter();
        }

        private void DeleteRuleButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDRuleComboBox.Text))
                throw new Exception(languageString[5]);
            List<string> tables = new List<string> { atributsDB[15], atributsDB[6] };
            db.DeleteDataInDB(tables, atributsDB[7], Convert.ToInt32(IDRuleComboBox.Text));
            UpdateListComboBoxRule();
            UpdateComboBoxIDChooseParameter();
        }

        private void DeleteCriterionButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDCriterionComboBox.Text))
                throw new Exception(languageString[5]);
            List<string> tables = new List<string> { atributsDB[15], atributsDB[20], atributsDB[10] };
            db.DeleteDataInDB(tables, atributsDB[11], Convert.ToInt32(IDCriterionComboBox.Text));
            UpdateListComboBoxCriterion();
            UpdateComboBoxIDChooseParameter();
            UpdateComboBoxIDValue();
        }

        private void DeleteValueButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDValueComboBox.Text))
                throw new Exception(languageString[5]);
            List<string> tables = new List<string> { atributsDB[20] };
            db.DeleteDataInDB(tables, atributsDB[21], Convert.ToInt32(IDValueComboBox.Text));
            UpdateComboBoxIDValue();
            if (TableComboBox.Text == atributsDB[20])
                TableValue();
        }

        private void CriterionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nameCriterion = CriterionListBox.SelectedItem.ToString();
            List<string> id_criterion = db.SelectDB(atributsDB[10], new List<string>() { atributsDB[11] }, new List<string>() { option.language ? atributsDB[12] : atributsDB[13] }, new List<string>() { nameCriterion });
            if (id_criterion.Count() != 1)
                throw new Exception(languageString[8]);
            List<string> fields = new List<string> { atributsDB[11], atributsDB[12], atributsDB[13], atributsDB[14] };
            List<string> fieldsToFind = new List<string> { atributsDB[11] };
            List<string> valuesToFind = new List<string> { id_criterion.First() };
            List<string> values = db.SelectDB(atributsDB[10], fields, fieldsToFind, valuesToFind);
            IDCriterionComboBox.SelectedItem = values[0];
            RuNameCriterionTextBox.Text = values[1];
            EnNameCriterionTextBox.Text = values[2];
            DescriptionCriterionTextBox.Text = values[3];
        }

        private void MethodListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nameMethod = MethodListBox.SelectedItem.ToString();
            List<string> id_method = db.SelectDB(atributsDB[1], new List<string>() { atributsDB[2] }, new List<string>() { option.language ? atributsDB[3] : atributsDB[4] }, new List<string>() { nameMethod });
            if (id_method.Count() != 1)
                throw new Exception(languageString[8]);
            List<string> fields = new List<string> { atributsDB[2], atributsDB[3], atributsDB[4] };
            List<string> fieldsToFind = new List<string> { atributsDB[2] };
            List<string> valuesToFind = new List<string> { id_method.First() };
            List<string> values = db.SelectDB(atributsDB[1], fields, fieldsToFind, valuesToFind);
            IDMethodComboBox.SelectedItem = values[0];
            RuNameMethodTextBox.Text = values[1];
            EnNameMethodTextBox.Text = values[2];
        }

        private void RuleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nameRule = RuleListBox.SelectedItem.ToString();
            List<string> id_rule = db.SelectDB(atributsDB[6], new List<string>() { atributsDB[7] }, new List<string>() { option.language ? atributsDB[9] : atributsDB[8] }, new List<string>() { nameRule });
            if (id_rule.Count() != 1)
                throw new Exception(languageString[8]);
            List<string> fields = new List<string> { atributsDB[7], atributsDB[8], atributsDB[9] };
            List<string> fieldsToFind = new List<string> { atributsDB[7] };
            List<string> valuesToFind = new List<string> { id_rule.First() };
            List<string> values = db.SelectDB(atributsDB[6], fields, fieldsToFind, valuesToFind);
            IDRuleComboBox.SelectedItem = values[0];
            RuNameRuleTextBox.Text = values[1];
            EnNameRuleTextBox.Text = values[2];
        }

        private void IDMethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> fields = new List<string> { atributsDB[3], atributsDB[4] };
            List<string> fieldsToFind = new List<string> { atributsDB[2] };
            List<string> valuesToFind = new List<string> { IDMethodComboBox.Text };
            List<string> values = db.SelectDB(atributsDB[1], fields, fieldsToFind, valuesToFind);
            RuNameMethodTextBox.Text = values[0];
            EnNameMethodTextBox.Text = values[1];
        }

        private void IDRuleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> fields = new List<string> { atributsDB[8], atributsDB[9] };
            List<string> fieldsToFind = new List<string> { atributsDB[7] };
            List<string> valuesToFind = new List<string> { IDRuleComboBox.Text };
            List<string> values = db.SelectDB(atributsDB[6], fields, fieldsToFind, valuesToFind);
            RuNameRuleTextBox.Text = values[1];
            EnNameRuleTextBox.Text = values[0];
        }

        private void IDChooseParameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> fields = new List<string> { atributsDB[17], atributsDB[18], atributsDB[19] };
            List<string> fieldsToFind = new List<string> { atributsDB[16] };
            List<string> valuesToFind = new List<string> { IDChooseParameterComboBox.Text };
            List<string> values = db.SelectDB(atributsDB[15], fields, fieldsToFind, valuesToFind);
            string id_method = values[0];
            string id_rule = values[1];
            List<string> nameMethod = db.SelectDB(atributsDB[1], new List<string> { option.language ? atributsDB[3] : atributsDB[4] }, new List<string> { atributsDB[2] }, new List<string> { id_method });
            List<string> nameRule = db.SelectDB(atributsDB[6], new List<string> { option.language ? atributsDB[9] : atributsDB[8] }, new List<string> { atributsDB[7] }, new List<string> { id_rule });
            if (values[2] == "0")
                CriterionChooseParameterComboBox.SelectedItem = " ";
            else
            {
                string id_criterion = values[2];
                List<string> nameCriterion = db.SelectDB(atributsDB[10], new List<string> { option.language ? atributsDB[12] : atributsDB[13] }, new List<string> { atributsDB[11] }, new List<string> { id_criterion });
                CriterionChooseParameterComboBox.SelectedItem = nameCriterion.First();
            }
            MethodChooseParameterComboBox.SelectedItem = nameMethod.First();
            RuleChooseParameterComboBox.SelectedItem = nameRule.First();
        }

        private void IDCriterionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> fields = new List<string> { atributsDB[12], atributsDB[13], atributsDB[14] };
            List<string> fieldsToFind = new List<string> { atributsDB[11] };
            List<string> valuesToFind = new List<string> { IDCriterionComboBox.Text };
            List<string> values = db.SelectDB(atributsDB[10], fields, fieldsToFind, valuesToFind);
            RuNameCriterionTextBox.Text = values[0];
            EnNameCriterionTextBox.Text = values[1];
            DescriptionCriterionTextBox.Text = values[2];
        }

        private void IDValueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> fields = new List<string> { atributsDB[22], atributsDB[23], atributsDB[24], atributsDB[25] };
            List<string> fieldsToFind = new List<string> { atributsDB[21] };
            List<string> valuesToFind = new List<string> { IDValueComboBox.Text };
            List<string> values = db.SelectDB(atributsDB[20], fields, fieldsToFind, valuesToFind);
            string id_criterion = values[0];
            List<string> nameCriterion = db.SelectDB(atributsDB[10], new List<string> { option.language ? atributsDB[12] : atributsDB[13] }, new List<string> { atributsDB[11] }, new List<string> { id_criterion });
            CriterionValueComboBox.SelectedItem = nameCriterion.First();
            SigLevelValueTextBox.Text = values[1];
            FreedomDegreeValueTextBox.Text = values[2];
            ValueCriterionValueTextBox.Text = values[3];
        }

        private void IDUserComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> fields = new List<string> { atributsDBUser[3], atributsDBUser[4], atributsDBUser[5] };
            List<string> fieldsToFind = new List<string> { atributsDBUser[2] };
            List<string> valuesToFind = new List<string> { IDUserComboBox.Text };
            List<string> values = dbUser.SelectDB(atributsDBUser[1], fields, fieldsToFind, valuesToFind);
            LoginUserTextBox.Text = values[0];
            PasswordUserTextBox.Text = values[1];
            NameUserTextBox.Text = values[2];
        }

        private void UserListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = UserListBox.SelectedItem.ToString();
            List<string> users = str.Split('/').ToList();
            List<string> id_user = dbUser.SelectDB(atributsDBUser[1], new List<string>() { atributsDBUser[2] }, new List<string>() { atributsDBUser[3] }, new List<string>() { users.Last() });
            if (id_user.Count() != 1)
                throw new Exception(languageString[8]);
            List<string> fields = new List<string> { atributsDBUser[3], atributsDBUser[4], atributsDBUser[5] };
            List<string> fieldsToFind = new List<string> { atributsDBUser[2] };
            List<string> valuesToFind = new List<string> { id_user.First() };
            List<string> values = dbUser.SelectDB(atributsDBUser[1], fields, fieldsToFind, valuesToFind);
            IDUserComboBox.SelectedItem = id_user.First();
            LoginUserTextBox.Text = values[0];
            PasswordUserTextBox.Text = values[1];
            NameUserTextBox.Text = values[2];
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LoginUserTextBox.Text) || String.IsNullOrWhiteSpace(PasswordUserTextBox.Text))
                throw new Exception(languageString[5]);
            if (dbUser.UseValueInDB(atributsDBUser[1], atributsDBUser[2], atributsDBUser[3], LoginUserTextBox.Text))
                throw new Exception(languageString[15]);
            List<string> values = new List<string>() { LoginUserTextBox.Text, PasswordUserTextBox.Text,
                                                       NameUserTextBox.Text, "0"};
            List<string> fields = new List<string>() { atributsDBUser[3], atributsDBUser[4], atributsDBUser[5], atributsDBUser[6] };
            string table = atributsDBUser[1];
            dbUser.AddDataIntoBD(table, fields, values);
            UpdateUserListComboBox();
        }

        private void UpdateUserButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LoginUserTextBox.Text) || String.IsNullOrWhiteSpace(PasswordUserTextBox.Text) ||
                String.IsNullOrWhiteSpace(IDUserComboBox.Text))
                MessageBox.Show(languageString[5]);
            if (dbUser.UseValueInDB(atributsDBUser[1], atributsDBUser[2], atributsDBUser[3], LoginUserTextBox.Text))
                throw new Exception(languageString[15]);
            List<string> values = new List<string>() { LoginUserTextBox.Text, PasswordUserTextBox.Text,
                                                       NameUserTextBox.Text};
            List<string> fields = new List<string>() { atributsDBUser[3], atributsDBUser[4], atributsDBUser[5] };
            dbUser.UpdateDataInBD(atributsDBUser[1], fields, values, atributsDBUser[2], IDUserComboBox.Text);
            UpdateUserListComboBox();
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDUserComboBox.Text))
                throw new Exception(languageString[5]);
            List<string> tables = new List<string> { atributsDBUser[1] };
            dbUser.DeleteDataInDB(tables, atributsDBUser[2], Convert.ToInt32(IDUserComboBox.Text));
            UpdateUserListComboBox();
        }

        private void TableUser()
        {
            UserDataGridView.Columns.Clear();
            UserDataGridView.Rows.Clear();
            List<string> fields = new List<string> { atributsDBUser[2], atributsDBUser[3], atributsDBUser[4], atributsDBUser[5] };
            string table = atributsDBUser[1];
            List<List<string>> values = dbUser.SelectAllDataInDB(table, fields);
            for (int index = 0; index < fields.Count(); index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = fields[index];
                column.Name = fields[index];
                column.ReadOnly = true;
                UserDataGridView.Columns.Add(column);
            }
            for (int index_i = 0; index_i < values.Count(); index_i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                for (int index_j = 0; index_j < values.First().Count(); index_j++)
                {
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = values[index_i][index_j];
                    row.Cells.Add(cell);
                }
                UserDataGridView.Rows.Add(row);
            }
            UserDataGridView.AllowUserToAddRows = false;
        }

        private void TableMainTable(string table, List<string> fields)
        {
            MainDataGridView.Columns.Clear();
            MainDataGridView.Rows.Clear();
            List<List<string>> values = db.SelectAllDataInDB(table, fields);
            for (int index = 0; index < fields.Count(); index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = fields[index];
                column.Name = fields[index];
                column.ReadOnly = true;
                MainDataGridView.Columns.Add(column);
            }
            for (int index_i = 0; index_i < values.Count(); index_i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                for (int index_j = 0; index_j < values.First().Count(); index_j++)
                {
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = values[index_i][index_j];
                    row.Cells.Add(cell);
                }
                MainDataGridView.Rows.Add(row);
            }
        }

        private void TableChooseParameter()
        {
            MainDataGridView.Columns.Clear();
            MainDataGridView.Rows.Clear();
            List<string> fields = new List<string> { atributsDB[16], atributsDB[17], atributsDB[18], atributsDB[19] };
            string table = atributsDB[15];
            List<List<string>> values = db.SelectAllDataInDB(table, fields);


            Dictionary<int, List<string>> criterions = db.SelectAllDataInDBDictionary(atributsDB[10],
                                                                                      new List<string> { atributsDB[11],
                                                                                                         option.language?atributsDB[12]:atributsDB[13] });
            Dictionary<int, List<string>> methods = db.SelectAllDataInDBDictionary(atributsDB[1],
                                                                                   new List<string> { atributsDB[2],
                                                                                                      option.language?atributsDB[3]:atributsDB[4] });
            Dictionary<int, List<string>> rules = db.SelectAllDataInDBDictionary(atributsDB[6],
                                                                                 new List<string> { atributsDB[7],
                                                                                                    option.language?atributsDB[9]:atributsDB[8] });
            for (int index = 0; index < values.Count(); index++)
            {
                values[index][1] = methods[Convert.ToInt32(values[index][1])].First();
                values[index][2] = rules[Convert.ToInt32(values[index][2])].First();
                if (Convert.ToInt32(values[index][3]) == 0)
                    values[index][3] = "  ";
                else
                    values[index][3] = criterions[Convert.ToInt32(values[index][3])].First();
            }
            fields[1] = atributsDB[3];
            fields[2] = atributsDB[9];
            fields[3] = atributsDB[12];
            for (int index = 0; index < fields.Count(); index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = fields[index];
                column.Name = fields[index];
                column.ReadOnly = true;
                MainDataGridView.Columns.Add(column);
            }
            for (int index_i = 0; index_i < values.Count(); index_i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                for (int index_j = 0; index_j < values.First().Count(); index_j++)
                {
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = values[index_i][index_j];
                    row.Cells.Add(cell);
                }
                MainDataGridView.Rows.Add(row);
            }
        }

        private void TableValue()
        {
            MainDataGridView.Columns.Clear();
            MainDataGridView.Rows.Clear();
            List<string> fields = new List<string> { atributsDB[21], atributsDB[22], atributsDB[23], atributsDB[24], atributsDB[25] };
            string table = atributsDB[20];
            List<List<string>> values = db.SelectAllDataInDB(table, fields);
            Dictionary<int, List<string>> criterions = db.SelectAllDataInDBDictionary(atributsDB[10],
                                                                                      new List<string> { atributsDB[11],
                                                                                                         option.language?atributsDB[12]:atributsDB[13] });
            for (int index = 0; index < values.Count(); index++)
            {
                values[index][1] = criterions[Convert.ToInt32(values[index][1])].First();
            }
            fields[1] = atributsDB[12];
            for (int index = 0; index < fields.Count(); index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = fields[index];
                column.Name = fields[index];
                column.ReadOnly = true;
                MainDataGridView.Columns.Add(column);
            }
            for (int index_i = 0; index_i < values.Count(); index_i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                for (int index_j = 0; index_j < values.First().Count(); index_j++)
                {
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = values[index_i][index_j];
                    row.Cells.Add(cell);
                }
                MainDataGridView.Rows.Add(row);
            }
        }

        private void TableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TableComboBox.Text)
            {
                case "Method":
                    TableMainTable(atributsDB[1], new List<string> { atributsDB[2], atributsDB[3], atributsDB[4] });
                    break;
                case "Rule":
                    TableMainTable(atributsDB[6], new List<string> { atributsDB[7], atributsDB[9], atributsDB[8] });
                    break;
                case "Criterion":
                    TableMainTable(atributsDB[10], new List<string> { atributsDB[11], atributsDB[12], atributsDB[13], atributsDB[14] });
                    break;
                case "Value":
                    TableValue();
                    break;
                case "Choose_Rule":
                    TableChooseParameter();
                    break;
                case "Методы":
                    TableMainTable(atributsDB[1], new List<string> { atributsDB[2], atributsDB[3], atributsDB[4] });
                    break;
                case "Требования":
                    TableMainTable(atributsDB[6], new List<string> { atributsDB[7], atributsDB[9], atributsDB[8] });
                    break;
                case "Критерии":
                    TableMainTable(atributsDB[10], new List<string> { atributsDB[11], atributsDB[12], atributsDB[13], atributsDB[14] });
                    break;
                case "Значения критериев":
                    TableValue();
                    break;
                case "Соотношения":
                    TableChooseParameter();
                    break;
                default:
                    break;
            }
        }

        private void UpdateCriterionButton_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IDCriterionComboBox.Text) ||
                String.IsNullOrWhiteSpace(EnNameCriterionTextBox.Text) || String.IsNullOrWhiteSpace(RuNameCriterionTextBox.Text))
                throw new Exception(languageString[5]);
            List<string> values = new List<string>() { RuNameCriterionTextBox.Text, EnNameCriterionTextBox.Text,
                                                       DescriptionCriterionTextBox.Text};
            List<string> fields = new List<string>() { atributsDB[12], atributsDB[13], atributsDB[14] };
            db.UpdateDataInBD(atributsDB[10], fields, values, atributsDB[11], IDCriterionComboBox.Text);
            UpdateListComboBoxCriterion();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("No data loaded!", "Error!", MessageBoxButtons.OK);
                return;
            }
            var tmp = File.ReadAllLines(dialog.FileName);
            Dictionary<double, double> laplasValues = new Dictionary<double, double>();
            foreach (var item in tmp)
                laplasValues.Add(double.Parse(item.Split(' ')[0].Replace('.', ',')), double.Parse(item.Split(' ')[1].Replace('.', ',')));

            foreach (var lap in laplasValues)
            {
                List<string> values = new List<string>() { "2", "0",
                                                       lap.Key.ToString().Replace(',','.'), lap.Value.ToString().Replace(',','.')};
                List<string> fields = new List<string>() { atributsDB[22], atributsDB[23], atributsDB[24], atributsDB[25] };
                string table = atributsDB[20];
                db.AddDataIntoBD(table, fields, values);
            }

        }
        private void SelectedRussianLanguage()
        {
            fileToolStripMenuItem.Text = "Файл";
            editingTheUserDatabaseToolStripMenuItem.Text = "Выход";
            CriterionGroupBox.Text = "Критерии";
            ValueGroupBox.Text = "Значения";
            MethodGroupBox.Text = "Методы";
            RuleGroupBox.Text = "Требования к информации";
            ChooseParameterGroupBox.Text = "Соотношения";
            UserGroupBox.Text = "Пользователи";
            MethodDBTabPage.Text = "Методы";
            CriterionDBTabPage.Text = "Критерии";
            TablesTabPage.Text = "Таблицы";
            UserTabPage.Text = "Пользователи";
            AddChooseParameterButton.Text = "Добавить";
            AddCriterionButton.Text = "Добавить";
            AddMethodButton.Text = "Добавить";
            AddRuleButton.Text = "Добавить";
            AddUserButton.Text = "Добавить";
            AddValueButton.Text = "Добавить";
            DeleteChooseParameterButton.Text = "Удалить";
            DeleteCriterionButton.Text = "Удалить";
            DeleteMethodButton.Text = "Удалить";
            DeleteRuleButton.Text = "Удалить";
            DeleteValueButton.Text = "Удалить";
            DeleteUserButton.Text = "Удалить";
            UpdateChooseParameterButton.Text = "Редактировать";
            UpdateCriterionButton.Text = "Редактировать";
            UpdateMethodButton.Text = "Редактировать";
            UpdateUserButton.Text = "Редактировать";
            UpdateValueButton.Text = "Редактировать";
            UpdateRuleButton.Text = "Редактировать";
            label6.Text = "Идентификатор:";
            label19.Text = "Русское название:";
            label18.Text = "Английское название:";
            label2.Text = "Идентификатор:";
            label16.Text = "Русское название:";
            label16.Text = "Английское название:";
            label3.Text = "Идентификатор:";
            label13.Text = "Наименование метода:";
            label12.Text = "Наименование требования:";
            label14.Text = "Наименование критерия:";
            label11.Text = "Идентификатор:";
            label1.Text = "Русское название:";
            label5.Text = "Английское название:";
            label4.Text = "Примечание:";
            label25.Text = "Выбор таблицы:";
            label22.Text = "Идентификатор:";
            label23.Text = "Логин:";
            label21.Text = "Пароль:";
            label24.Text = "Имя:";
            label17.Text = "Идентификатор: ";
            label8.Text = "Наименование критерия:  ";
            label7.Text = "Уровень значимости: ";
            label9.Text = "Степень свободы:";
            label10.Text = "Значение критерия:";
        }
        private void SelectedEnglishLanguage()
        {
            fileToolStripMenuItem.Text = "File";
            editingTheUserDatabaseToolStripMenuItem.Text = "Exit";
            CriterionGroupBox.Text = "Criterions";
            ValueGroupBox.Text = "Values";
            MethodGroupBox.Text = "Methods";
            RuleGroupBox.Text = "Rules";
            ChooseParameterGroupBox.Text = "Choose parameters";
            UserGroupBox.Text = "Users";
            MethodDBTabPage.Text = "Methods";
            CriterionDBTabPage.Text = "Criterions";
            TablesTabPage.Text = "Tables";
            UserTabPage.Text = "Users";
            AddChooseParameterButton.Text = "Add";
            AddCriterionButton.Text = "Add";
            AddMethodButton.Text = "Add";
            AddRuleButton.Text = "Add";
            AddUserButton.Text = "Add";
            AddValueButton.Text = "Add";
            DeleteChooseParameterButton.Text = "Delete";
            DeleteCriterionButton.Text = "Delete";
            DeleteMethodButton.Text = "Delete";
            DeleteRuleButton.Text = "Delete";
            DeleteValueButton.Text = "Delete";
            DeleteUserButton.Text = "Delete";
            UpdateChooseParameterButton.Text = "Update";
            UpdateCriterionButton.Text = "Update";
            UpdateMethodButton.Text = "Update";
            UpdateUserButton.Text = "Update";
            UpdateValueButton.Text = "Update";
            UpdateRuleButton.Text = "Update";
            label6.Text = "Method ID:";
            label19.Text = "Russian name of method: ";
            label18.Text = "English name of method: ";
            label2.Text = "Rule ID:";
            label16.Text = "Russian name of rule:";
            label15.Text = "English name of rule:";
            label3.Text = "Choose parameter ID:";
            label13.Text = "Name of method: ";
            label12.Text = "Name of rule: ";
            label14.Text = "Name of criterion:";
            label11.Text = "Criterion ID: ";
            label1.Text = "Russian name of critarion: ";
            label5.Text = "English name of critarion: ";
            label4.Text = "Critarion's description: ";
            label17.Text = "Value ID: ";
            label8.Text = "Name of criterion:  ";
            label7.Text = "Significance level: ";
            label9.Text = "Freedom Degree:";
            label10.Text = "Value of criterion:";
            label25.Text = "Choose table :";
            label22.Text = "User ID: ";
            label23.Text = "Login: ";
            label21.Text = "Password : ";
            label24.Text = "Name : ";
        }

        private void AddListLanguageString()
        {
            if (option.language)
            {
                languageString.Add("Методы");
                languageString.Add("Требования");
                languageString.Add("Критерии");
                languageString.Add("Соотношения");
                languageString.Add("Значения критериев");
            }
            else
            {
                languageString.Add(atributsDB[1]);
                languageString.Add(atributsDB[6]);
                languageString.Add(atributsDB[10]);
                languageString.Add(atributsDB[15]);
                languageString.Add(atributsDB[20]);
                languageString.Add("Input data incorrect!!!");
                languageString.Add("The method with the ru name is being in database!!!");
                languageString.Add("The method with the en name is being in database!!!");
                languageString.Add("More than one id!!!");
                languageString.Add("The choose parameter is being in database!!!");
                languageString.Add("The rule with the ru name is being in database!!!");
                languageString.Add("The rule with the en name is being in database!!!");
                languageString.Add("The criterion with the ru name is being in database!!!");
                languageString.Add("The criterion with the en name is being in database!!!");
                languageString.Add("The value is being in database!!!");
                languageString.Add("The login is using!!!");
            }
        }
    }
}
