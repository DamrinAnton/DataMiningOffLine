using DataMiningOffLine.DBClasses;
using DataMiningOffLine.Helpers;
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
    public partial class dependenceParameters : Form
    {
        public dependenceParameters()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dependenceParameters_Load(object sender, EventArgs e)
        {

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "Significant parameter";
            column2.HeaderText = Localisation.GetInstance().Language.Equals("RU") ? "Значимый параметр" : "Significant parameter";
            column2.DataPropertyName = "Significant parameter";
            dataGridView1.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "significant parameter";
            column3.HeaderText = Localisation.GetInstance().Language.Equals("RU") ? "Значимый параметр" : "Significant parameter";
            column3.DataPropertyName = "significant parameter";
            dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "Analysis method";
            column4.HeaderText = Localisation.GetInstance().Language.Equals("RU") ? "Метод анализа" : "Analysis method";
            column4.DataPropertyName = "Analysis method";
            dataGridView1.Columns.Add(column4);

            List<situationsES> situations = DBClasses.Queries.GetESSituations();

            foreach (var record in situations)
            {
                comboBox1.Items.Add(record.nameES);
            }
            if (comboBox1.Items.Count != 0)
                comboBox1.Text = comboBox1.Items[0].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            List<Dependences> data = DBClasses.Queries.GetResultAnalyse(comboBox1.Text);

            foreach (var record in data)
            {
                dataGridView1.Rows.Add(new String[] { record.firstMaxValue, record.secondMaxValue, record.typeAnalyse });
            }
        }
    }
}
