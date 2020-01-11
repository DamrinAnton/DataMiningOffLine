using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DM.DecisionTree
{
    public class SaveXML
    {
        #region Methods
        public static void SerializeObject(string fileName, TreeNode objToSerialize)
        {
            FileStream fstream = File.Open(fileName, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fstream, objToSerialize);
            fstream.Close();
        }

        public static void SerializeObjectRandomForest(string fileName, ListRandomForests objToSerialize)
        {
            FileStream fstream = File.Open(fileName, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fstream, objToSerialize);
            fstream.Close();
        }

        public static void SerializeToInt(string fileName, int objToSerialize, int id, double criterion)
        {
            TextWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + @"\" + "output1.txt");
            writer.WriteLine(objToSerialize.ToString());
            writer.WriteLine(id.ToString());
            writer.WriteLine(criterion.ToString());
            writer.Close();
        }

        public static TreeNode DeserializeObject(string fileName)
        {
            TreeNode objToSerialize = null;
            FileStream fstream = File.Open(fileName, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            objToSerialize = (TreeNode)binaryFormatter.Deserialize(fstream);
            fstream.Close();
            return objToSerialize;
        }

        public static ListRandomForests DeserializeObjectRandomForests(string fileName)
        {
            ListRandomForests objToSerialize = null;
            FileStream fstream = File.Open(fileName, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            objToSerialize = (ListRandomForests)binaryFormatter.Deserialize(fstream);
            fstream.Close();
            return objToSerialize;
        }



        public static List<double> DeserializeToInt(string fileName)
        {
            String line;
            List<double> numberOfMeasurement = new List<double>();
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\" + "output1.txt");

                //Read the first line of text
                line = sr.ReadLine();
                numberOfMeasurement.Add(Convert.ToDouble(line));
                line = sr.ReadLine();
                numberOfMeasurement.Add(Convert.ToDouble(line));
                line = sr.ReadLine();
                numberOfMeasurement.Add(Convert.ToDouble(line));
                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");

            }
            return numberOfMeasurement;
        }
        #endregion
    }
}
