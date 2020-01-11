using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMiningOffLine
{
    public partial class OptionND : Form
    {
        public Option option;
        public bool result;
        public OptionND(Option optionStart)
        {
            InitializeComponent();
            if (optionStart.log)
                OnLogRadioButton.Checked = true;
            else if (optionStart.unlog)
                InverselyLogRadioButton.Checked = true;
            else
                OffLogRadioButton.Checked = true;

            if (optionStart.minmax)
                MinMaxRadioButton.Checked = true;
            else if (optionStart.sigma)
                SigmaRadioButton.Checked = true;
            else
                NoTransformRadioButton.Checked = true;

            SignLevelTextBox.Text = optionStart.singLevel;
            if (optionStart.language)
            {
                CriterionsCheckedListBox.Items.Add("Критерий Пирсона", optionStart.critHi);
                CriterionsCheckedListBox.Items.Add("Критерий Колмогорова-Смирнова", optionStart.critKS);
                CriterionsCheckedListBox.Items.Add("Критерий Фроцини", optionStart.critFrocini);
                SelectedRussianLanguage();
            }
            else
            {
                CriterionsCheckedListBox.Items.Add("Pirson's criterion", optionStart.critHi);
                CriterionsCheckedListBox.Items.Add("Criterion of Kolmogorova-Smirnova", optionStart.critKS);
                CriterionsCheckedListBox.Items.Add("Frocini's criterion", optionStart.critFrocini);
                SelectedEnglishnLanguage();
            }
            option = optionStart;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            option.log = OnLogRadioButton.Checked;
            option.unlog = InverselyLogRadioButton.Checked;
            option.minmax = MinMaxRadioButton.Checked;
            option.sigma = SigmaRadioButton.Checked;
            option.singLevel = SignLevelTextBox.Text;
            var checkedItem = CriterionsCheckedListBox.CheckedIndices;
            if (checkedItem.Contains(0))
                option.critHi = true;
            else
                option.critHi = false;

            if (checkedItem.Contains(1))
                option.critKS = true;
            else
                option.critKS = false;

            if (checkedItem.Contains(2))
                option.critFrocini = true;
            else
                option.critFrocini = false;
            result = true;
            this.Close();
        }

        public static bool GetOptions(out Option newOptions, Option option)
        {
            OptionND optionWindow = new OptionND(option);
            optionWindow.ShowDialog();
            newOptions = optionWindow.option;
            return optionWindow.result;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            result = false;
            this.Close();
        }

        private void SelectedRussianLanguage()
        {
            groupBox1.Text = "Логарифмирование";
            groupBox2.Text = "Отбрасывание ошибок";
            groupBox3.Text = "Критерии нормальности";
            label1.Text = "Уровень значимости";
            OkButton.Text = "Ок";
            CancelButton.Text = "Отмена";
            OnLogRadioButton.Text = "Вкл";
            OffLogRadioButton.Text = "Выкл";
            InverselyLogRadioButton.Text = "Экспонирование";
            MinMaxRadioButton.Text = "Мин/Макс";
            SigmaRadioButton.Text = "3-сигма";
            NoTransformRadioButton.Text = "Нет";
        }

        private void SelectedEnglishnLanguage()
        {
            groupBox1.Text = "Logarithm";
            groupBox2.Text = "Data transformation";
            groupBox3.Text = "Criterions";
            label1.Text = "Significance level";
            OkButton.Text = "OK";
            CancelButton.Text = "Cancel";
            OnLogRadioButton.Text = "On";
            OffLogRadioButton.Text = "Off";
            InverselyLogRadioButton.Text = "Inversely";
            MinMaxRadioButton.Text = "Min/Max";
            SigmaRadioButton.Text = "3-sigma";
            NoTransformRadioButton.Text = "No";
        }
    }
}
