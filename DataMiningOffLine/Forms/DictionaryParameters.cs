using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace DataMiningOffLine
{
    public partial class DictionaryParameters : Form
    {
        public DictionaryParameters()
        {
            InitializeComponent();
            DataSet AuthorsDataSet = new DataSet();
            
            AuthorsDataSet.ReadXml(XMLWork.Path);

            gridViewParameters.DataSource  = AuthorsDataSet;
            gridViewParameters.DataMember = "parameter";
            gridViewParameters.Columns["index"].HeaderText = Localization.MyStrings.IndexName;
            gridViewParameters.Columns["scadaName"].HeaderText = Localization.MyStrings.ScadaName;
            gridViewParameters.Columns["enName"].HeaderText = Localization.MyStrings.EnName;
            gridViewParameters.Columns["ruName"].HeaderText = Localization.MyStrings.RuName;
            gridViewParameters.Columns["scadaName"].ReadOnly = true;
            EnableDoubleBuffering();
        }

        void EnableDoubleBuffering()
        {
            if (!SystemInformation.TerminalServerSession)
            {
                Type dgvType = gridViewParameters.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(gridViewParameters, true, null);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void DictionaryParameters_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(Localization.MyStrings.QuestionSave, Localization.MyStrings.FormClosing,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveData();
            }
        }

        public void SaveData()
        {
            if (File.Exists(XMLWork.Path))
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                for (int i = 0; i < gridViewParameters.RowCount; i++)
                {
                    DataRow row = (gridViewParameters.DataSource as DataSet).Tables["parameter"].Rows[i];
                    foreach (XElement xNode in xmlFile.Root.Nodes())
                    {
                        if (xNode.Attribute("scadaName").Value == row[1].ToString())
                        {
                            xNode.Attribute("index").SetValue(row["index"].ToString());
                            xNode.Attribute("ruName").SetValue(row["ruName"].ToString());
                            xNode.Attribute("enName").SetValue(row["enName"].ToString());
                            break;
                        }
                    }
                }
                xmlFile.Save(XMLWork.Path);
            }
            else MessageBox.Show(Localization.MyStrings.DataNotGiven);
        }
    }
    }
