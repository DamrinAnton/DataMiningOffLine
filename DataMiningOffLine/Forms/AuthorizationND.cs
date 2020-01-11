using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace DataMiningOffLine
{
    public partial class AuthorizationND : Form
    {

        private string dbFileName = "Resources\\BDUsersND.db";
        private SQLiteConnection m_dbConn = new SQLiteConnection();
        private SQLiteCommand m_sqlCmd = new SQLiteCommand();
        private Option option = new Option();
        private List<string> languageList = new List<string>();


        public AuthorizationND(Option options)
        {
            this.option = options;
            InitializeComponent();
            if (option.language)
                SelectedRussianLanguage();
            else
                SelectedEnglishLanguage();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State == ConnectionState.Open)
                m_dbConn.Close();
            this.Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            if (!File.Exists(dbFileName))
                MessageBox.Show(languageList[0]);
            else
            {
                try
                {
                    m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                    m_dbConn.Open();
                    if (FindUserStatus() != 0)
                        MessageBox.Show(languageList[1]);
                    else
                    {
                        string sqlQuery;
                        sqlQuery = String.Format("SELECT ID_user, Name_User FROM User WHERE Password_user =\"{0}\" AND Login_user =\"{1}\"", PasswordTextBox.Text, LoginTextBox.Text);
                        m_sqlCmd = new SQLiteCommand
                        {
                            Connection = m_dbConn,
                            CommandText = sqlQuery
                        };
                        SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
                        int idUser = 0;
                        string nameUser = "";
                        while (sqlReader.Read())
                        {
                            idUser = Convert.ToInt32(sqlReader["ID_user"]);
                            nameUser = sqlReader["Name_user"].ToString();
                        }
                        if (idUser != 0)
                        {
                            MessageBox.Show(languageList[5] + nameUser);
                            UpdateUserStatus(1, idUser);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(languageList[2]);
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            int idUser = FindUserStatus();
            if (idUser != 0)
            {
                UpdateUserStatus(0, idUser);
                if (m_dbConn.State == ConnectionState.Open)
                    m_dbConn.Close();
                this.Close();
                MessageBox.Show(languageList[3]);
            }
            else
                MessageBox.Show(languageList[4]);
        }

        public int FindUserStatus()
        {
            int id = 0;
            if (m_dbConn.State != ConnectionState.Open || m_dbConn == null)
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
            }
            string sqlQuery;
            sqlQuery = String.Format("SELECT ID_user, Name_User FROM User WHERE Status =\"1\"");
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            SQLiteDataReader sqlReader = m_sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                id = Convert.ToInt32(sqlReader["ID_user"]);
            }
            return id;
        }

        public void UpdateUserStatus(int status, int id)
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
            }
            string sqlQuery = String.Format("UPDATE User SET Status =\"{1}\" WHERE ID_user = \"{0}\"", id, status);
            m_sqlCmd = new SQLiteCommand
            {
                Connection = m_dbConn,
                CommandText = sqlQuery
            };
            m_sqlCmd.ExecuteNonQuery();
        }

        private void SelectedRussianLanguage()
        {
            label1.Text = "Логин:";
            label2.Text = "Пароль:";
            OkButton.Text = "Ок";
            CancelButton.Text = "Отмена";
            ExitButton.Text = "Выход";
            languageList.Add("База данных пользователей не найдена!!!");
            languageList.Add("Вы уже авторизированы!!!");
            languageList.Add("Неправильно введен логин или пароль!!!");
            languageList.Add("Прощайте(");
            languageList.Add("Пожалуйста, авторизируйтесь.");
            languageList.Add("Добро пожаловать, ");
        }

        private void SelectedEnglishLanguage()
        {
            label1.Text = "Login:";
            label2.Text = "Password:";
            OkButton.Text = "OK";
            CancelButton.Text = "Cancel";
            ExitButton.Text = "Exit";
            languageList.Add("Sorry, file is not!!!!");
            languageList.Add("You ara already logged in!!!");
            languageList.Add("Sorry, your login or password are not correct!!");
            languageList.Add("Goodbye");
            languageList.Add("Please, authorization.");
            languageList.Add("Welcom");
        }


    }
}
