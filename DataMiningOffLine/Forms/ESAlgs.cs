using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMiningOffLine.Forms
{
    public partial class ESAlgs : Form
    {
        private static List<string> ESs;

        public ESAlgs()
        {
            InitializeComponent();
            StartParams();
        }

        private void StartParams()
        {
            ESs = new List<string>() {
                "307", "308-309" , "310" , "311" , "316", "319", 
                 "401", "503", "602", "705", "710-711", "715-716"};

            ESList.DataSource = ESs;
        }

        private void ESList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ESpb.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\ESResources\" + ESList.SelectedItem.ToString() + "-001.jpg");
        }
    }
}
