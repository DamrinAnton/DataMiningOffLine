using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMiningOffLine.Forms.RegressionAnalysis
{
    public partial class ModelNameDateSelection : Form
    {
        private static string _name = "DefaultName";
        private static DateTime _creationDate = DateTime.Now;
        public ModelNameDateSelection()
        {
            InitializeComponent();
        }

        private void buttonSaveModel_Click(object sender, EventArgs e)
        {
            if (textBoxModelName.Text.Length == 0)
            {
                MessageBox.Show("Enter model name!");
                return;
            }

            _name = textBoxModelName.Text;
            _creationDate = dateTimePickerModelCreationDate.Value;
            Close();
        }

        public static void GetRegressionModelSaveInfo(out string name, out DateTime creationDate)
        {
            ModelNameDateSelection form = new ModelNameDateSelection();
            form.ShowDialog();
            name = _name;
            creationDate = _creationDate;
        }
    }
}
