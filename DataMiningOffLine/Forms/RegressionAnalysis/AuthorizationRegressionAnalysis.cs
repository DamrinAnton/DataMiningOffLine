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
    public partial class AuthorizationRegressionAnalysis : Form
    {
        public AuthorizationRegressionAnalysis()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Length == 0 || textBoxUsername.Text.Length == 0)
            {
                MessageBox.Show("Wrong user data!");
                return;
            }

            if (textBoxUsername.Text.Equals("admin") && textBoxPassword.Text.Equals("qwerty"))
                DialogResult = DialogResult.Yes;
            else
                DialogResult = DialogResult.No;
            Close();
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonLogin_Click(null, null);
        }
    }
}
