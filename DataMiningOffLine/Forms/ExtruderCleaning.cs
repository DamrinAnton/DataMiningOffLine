using DataMiningOffLine.DBClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMiningOffLine.Forms
{
    public partial class ExtruderCleaning : Form
    {
        public ExtruderCleaning()
        {
            InitializeComponent();
            monthCalendar1.Left = (this.ClientSize.Width - monthCalendar1.Width) / 2;
            monthCalendar1.Top = (this.ClientSize.Height - monthCalendar1.Height) / 2;

            Language();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox1.Text = monthCalendar1.SelectionRange.Start.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(GetDays(textBox1.Text) < 0)
                MessageBox.Show(Helpers.Localisation.GetInstance().Language.Equals("RU") ? "Нельзя выбрать дату из будущего." : "You can not select a date from the future.");
            else if (textBox1.Text.Length > 1)
            {
                Queries.SetExtruderCleaningData(textBox1.Text);
                MessageBox.Show(Helpers.Localisation.GetInstance().Language.Equals("RU") ? "Изменения сохранены." : "Changes saved.");
                Close();
            }
            else
                MessageBox.Show(Helpers.Localisation.GetInstance().Language.Equals("RU") ? "Не выбрана дата." : "No date selected.");
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

        private void Language()
        {
            if (Helpers.Localisation.GetInstance().Language.Equals("RU"))
            {
                Text = "ДАТА ЧИСТКИ";
                button1.Text = "СОХРАНИТЬ ИЗМЕНЕНИЯ";
            }
            else
            {
                Text = "CLEANING DATE";
                button1.Text = "SAVE CHANGES";
            }
        }
    }
}
