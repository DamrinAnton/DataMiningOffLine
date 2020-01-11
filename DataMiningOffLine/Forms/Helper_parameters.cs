using DataMiningOffLine.Helpers;
using System;
using System.Windows.Forms;

namespace DataMiningOffLine.Forms
{
    public partial class Helper_parameters : Form
    {
        public Helper_parameters()
        {
            InitializeComponent();
        }



        private void Helper_parameters_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataMiningDataSet1.Parameters". При необходимости она может быть перемещена или удалена.
            this.parametersTableAdapter.Fill(this.dataMiningDataSet1.Parameters);
            ChangeLanguage();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // parametersTableAdapter.Adapter.Update(dataMiningDataSet1);
            this.parametersTableAdapter.Update(this.dataMiningDataSet1.Parameters);
        }

        private void ChangeLanguage() {
            if (Localisation.GetInstance().Language.Equals("RU"))
            {
                this.Text = "РЕДАКТИРОВАНИЕ ОГРАНИЧЕНИЙ ДЛЯ ПАРАМЕТРОВ";
                dataGridView1.Columns["namestDataGridViewTextBoxColumn"].HeaderText = "НАЗВАНИЕ ПАРАМЕТРА";
                dataGridView1.Columns["translaterustDataGridViewTextBoxColumn"].HeaderText = "RU";
                dataGridView1.Columns["translateenstDataGridViewTextBoxColumn"].HeaderText = "EN";
                dataGridView1.Columns["upperLimitPercentDataGridViewTextBoxColumn"].HeaderText = "ВЕРХНИЙ ПРЕДЕЛ";
                dataGridView1.Columns["lowerLimitPercentDataGridViewTextBoxColumn"].HeaderText = "НИЖНИЙ ПРЕДЕЛ";

                bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
                bindingNavigatorMovePreviousItem.Text = "Переместить назад";
                bindingNavigatorMoveNextItem.Text = "Переместить вперед";
                bindingNavigatorMoveLastItem.Text = "Переместить в конец";
                bindingNavigatorAddNewItem.Text = "Добавить";
                bindingNavigatorDeleteItem.Text = "Удалить";
                toolStripButton1.Text = "Сохранить изменения";
            }
            else
            {
                this.Text = "EDITING LIMITATIONS FOR PARAMETERS";
                dataGridView1.Columns["namestDataGridViewTextBoxColumn"].HeaderText = "NAME OF PARAMETER";
                dataGridView1.Columns["translaterustDataGridViewTextBoxColumn"].HeaderText = "RU";
                dataGridView1.Columns["translateenstDataGridViewTextBoxColumn"].HeaderText = "EN";
                dataGridView1.Columns["upperLimitPercentDataGridViewTextBoxColumn"].HeaderText = "UPPER LIMIT";
                dataGridView1.Columns["lowerLimitPercentDataGridViewTextBoxColumn"].HeaderText = "LOWER LIMIT";

                bindingNavigatorMoveFirstItem.Text = "Move to the beginning";
                bindingNavigatorMovePreviousItem.Text = "Move back";
                bindingNavigatorMoveNextItem.Text = "Move Forward";
                bindingNavigatorMoveLastItem.Text = "Move to end";
                bindingNavigatorAddNewItem.Text = "Add";
                bindingNavigatorDeleteItem.Text = "Delete";
                toolStripButton1.Text = "Save Changes";

            }
        }
    }
}
