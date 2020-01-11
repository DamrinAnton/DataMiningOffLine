using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace DataMiningOffLine
{
    static class XMLWork
    {
        //Путь к базе данных проектов
        private static string pathToXML = System.AppDomain.CurrentDomain.BaseDirectory + "\\XMLDataBase.xml";
        private static string pathToShrinkage = System.AppDomain.CurrentDomain.BaseDirectory + "\\Shrinkage.xml";
        //Провека на наличие параметра в базе данных
        private static bool isExist;

        public static string Path
        {
            get { return pathToXML; }
        }

        public static string PathShrinkage
        {
            get { return pathToShrinkage; }
        }


        public static void addShrinkageFile(DateTime date, decimal value)
        {
            //Путь к XMLDataBase
            
            //Файл не существует создаем его
            
            if (!File.Exists(pathToShrinkage))
            {
                //Файл не существует создаем его
                XDocument doc = new XDocument();
                doc.Declaration = new XDeclaration("1.0", "utf-8", null);
                doc.Add(new XElement("Shrinkage"));
                //Корень XML документа
                XElement root = doc.Element("Shrinkage");
                //Создаем пустой эксперимент и считываем данные в него данные
                //Добовляем новый элемент/эксперимент
                XElement shrinkageValue = new XElement("Shrinkage");
                //Устанавливаем свойства эксперимента
                shrinkageValue.Add(new XAttribute("DateTime", date));
                shrinkageValue.Add(new XAttribute("Value", value));

                //Добавляем эксперимент
                root.Add(shrinkageValue);

                doc.Save(pathToShrinkage);
            }
            else
            {
                isExist = false;
                //Загружаем файл если он существует
                XDocument xDoc = XDocument.Load(pathToShrinkage);
                
                // Если в базе данных существует параметр, то присваиваем переменной isExist true
                foreach (XElement xNode in xDoc.Root.Nodes())
                {
                    if (xNode.Attribute("DateTime").Value == date.ToLongDateString())
                    {
                        isExist = true;
                        break;
                    }
                }
                // Сохраняем параметр в бд если его нету там
                if (!isExist)
                {
                    XDocument doc = XDocument.Load(pathToShrinkage);

                    //Корень XML документа
                    XElement root = doc.Element("Shrinkage");
                    //Создаем пустой эксперимент и считываем данные в него данные
                    //Добовляем новый элемент/эксперимент
                    XElement shrinkageValue = new XElement("Shrinkage");
                    //Устанавливаем свойства эксперимента
                    shrinkageValue.Add(new XAttribute("DateTime", date));
                    shrinkageValue.Add(new XAttribute("Value", value));

                    //Добавляем эксперимент
                    root.Add(shrinkageValue);

                    doc.Save(pathToShrinkage);
                }
            }
        }

        public static void AddRow(int id, string name)
        {
            //Путь к XMLDataBase
            if (!File.Exists(pathToXML))
            {
                //Файл не существует создаем его
                XDocument doc = new XDocument();
                doc.Declaration = new XDeclaration("1.0", "utf-8", null);
                doc.Add(new XElement("parameters"));
                //Корень XML документа
                XElement root = doc.Element("parameters");
                //Создаем пустой эксперимент и считываем данные в него данные
                //Добовляем новый элемент/эксперимент
                XElement parameter = new XElement("parameter");
                //Устанавливаем свойства эксперимента
                parameter.Add(new XAttribute("index", id));
                parameter.Add(new XAttribute("scadaName", name));
                parameter.Add(new XAttribute("ruName", name));
                parameter.Add(new XAttribute("enName", name));

                //Добавляем эксперимент
                root.Add(parameter);

                doc.Save(pathToXML);
            }
            else
            {
                isExist = false;
                //Загружаем файл если он существует
                XDocument xDoc = XDocument.Load(pathToXML);
                // Если в базе данных существует параметр, то присваиваем переменной isExist true
                foreach (XElement xNode in xDoc.Root.Nodes())
                {
                    if (xNode.Attribute("scadaName").Value == name)
                    {
                        isExist = true;
                        break;
                    }
                }
                // Сохраняем параметр в бд если его нету там
                if (!isExist)
                {
                    XDocument doc = XDocument.Load(pathToXML);

                    //Корень XML документа
                    XElement root = doc.Element("parameters");
                    //Создаем пустой эксперимент и считываем данные в него данные
                    //Добовляем новый элемент/эксперимент
                    XElement parameter = new XElement("parameter");
                    //Устанавливаем свойства эксперимента
                    parameter.Add(new XAttribute("index", id));
                    parameter.Add(new XAttribute("scadaName", name));
                    parameter.Add(new XAttribute("ruName", name));
                    parameter.Add(new XAttribute("enName", name));

                    //Добавляем эксперимент
                    root.Add(parameter);

                    doc.Save(pathToXML);
                }
            }
        }

        public static string FindShrinkageWithTimestamp(DateTime date)
        {
            if (!File.Exists(pathToShrinkage))
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.pathToShrinkage);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (Convert.ToDateTime(xNode.Attribute("DateTime").Value) == date)
                    {
                        return xNode.Attribute("Value").Value;
                    }
                }
            }
            throw new Exception("Данные не найдены в файле");
        }


        private static string FindRuNameWithID(int id)
        {
            if (!File.Exists(pathToXML))
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("index").Value == id.ToString())
                    {
                        return xNode.Attribute("ruName").Value;
                    }
                }
            }
            return "Параметр не найден в БД";
        }

        private static string FindEnNameWithID(int id)
        {
            if (!File.Exists(pathToXML))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("index").Value == id.ToString())
                    {
                        return xNode.Attribute("enName").Value;
                    }
                }

                
            }
            return "Parameter Not Found In DB";
        }
        public static string FindScadaNameWithID(int id)
        {
            if (!File.Exists(pathToXML))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("index").Value == id.ToString())
                    {
                        return xNode.Attribute("scadaName").Value;
                    }
                }


            }
            return "Parameter Not Found In DB";
        }

        public static string FindNameWithID(int id, string language)
        {
            if (language == "en-US")
                return FindEnNameWithID(id);
            return FindRuNameWithID(id);
        }

        public static int FindID(string scadaName)
        {
            if (!File.Exists(pathToXML))
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("scadaName").Value == scadaName.ToString())
                    {
                        return Convert.ToInt32(xNode.Attribute("index").Value);
                    }
                }
            }
            return 0;
        }

        public static int FindIDWithName(string name, string language)
        {
            if (language == "en-US")
                return FindIDWithEnName(name);
            return FindIDWithRuName(name);
        }

        private static int FindIDWithEnName(string name)
        {
            if (!File.Exists(pathToXML))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("enName").Value == name)
                    {
                        return Convert.ToInt32( xNode.Attribute("index").Value);
                    }
                }


            }
            return 0;
        }

        private static int FindIDWithRuName(string name)
        {
            if (!File.Exists(pathToXML))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("ruName").Value == name)
                    {
                        return Convert.ToInt32(xNode.Attribute("index").Value);
                    }
                }


            }
            return 0;
        }

        public static string FindScadaNameWithAnotherName(string name, string language)
        {
            if (language == "en-US")
                return FindScadaNameWithAEnName(name);
            return FindScadaNameWithARuName(name);
        }

        private static string FindScadaNameWithAEnName(string name)
        {
            if (!File.Exists(pathToXML))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("enName").Value == name)
                    {
                        return xNode.Attribute("scadaName").Value;
                    }
                }


            }
            return "Parameter Not Found In DB";
        }

        private static string FindScadaNameWithARuName(string name)
        {
            if (!File.Exists(pathToXML))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("ruName").Value == name)
                    {
                        return xNode.Attribute("scadaName").Value;
                    }
                }


            }
            return "Файл не найден в БД";
        }


        public static string FindNameWithScada(string name, string language)
        {
            if (language == "en-US")
                return FindEnNameWithScada(name);
            return FindRuNameWithScada(name);
        }

        private static string FindEnNameWithScada(string name)
        {
            if (!File.Exists(pathToXML))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("scadaName").Value == name)
                    {
                        return xNode.Attribute("enName").Value;
                    }
                }


            }
            return "File not found in DB";
        }

        private static string FindRuNameWithScada(string name)
        {
            if (!File.Exists(pathToXML))
            {
                MessageBox.Show(Localization.MyStrings.FileNotFound);
            }
            else
            {
                XDocument xmlFile = XDocument.Load(XMLWork.Path);
                foreach (XElement xNode in xmlFile.Root.Nodes())
                {
                    if (xNode.Attribute("scadaName").Value == name)
                    {
                        return xNode.Attribute("ruName").Value;
                    }
                }


            }
            return "Файл не найден в БД";
        }
    }
}
