using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Windows.Media;

namespace Bataille_navale
{
    class SQLConnection
    {

        private string m_hostname;
        private string m_database;
        private string m_user;
        private string m_password;
        private bool m_state_connection;
        private MySqlConnection m_connection;

        public SQLConnection(string hostname, string database, string user, string password)
        {
            m_hostname = hostname;
            m_database = database;
            m_user = user;
            m_password = password;
            m_connection = new MySqlConnection(String.Format("SERVER={0}; DATABASE={1}; UID={2}; PASSWORD={3}", m_hostname, m_database, m_user, m_password));
            m_state_connection = false;
        }

        public bool connection()
        {
            try
            {
                m_connection.Open();
                m_state_connection = true;
                Console.WriteLine("Connection bdd ok!");
            }
            catch(Exception e)
            {

                m_state_connection = false;
                Console.WriteLine("Erreur connection bdd : " + e.Message);
            }
            return m_state_connection;
        }

        public bool deconnection()
        {
            try
            {
                if (m_state_connection)
                {
                    m_connection.Close();
                    m_state_connection = false;
                }
                else
                    m_state_connection = true;
            }
            catch
            {
                m_state_connection = false;
            }
            return m_state_connection;
        }

        public bool newUser(string name, string password)
        {
            bool state = false;

            try
            {
                MySqlCommand cmd = m_connection.CreateCommand();
                cmd.CommandText = "INSERT INTO users(username, password, grade) VALUES (@name, @password, @grade)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@grade", "Visiteur");
                cmd.BeginExecuteNonQuery();
                state = true;
            }
            catch
            {
                state = false;
            }

            return state;
        }

        public Dictionary<string, string> getDictPlayer(string name_player)
        {
            connection();
            Dictionary<string, string> dict_player = new Dictionary<string, string>();
            try
            {
                String sql = "SELECT * FROM users WHERE username = '" + name_player + "'";
                MySqlCommand cmd = new MySqlCommand(sql, m_connection);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    dict_player["id"] = (string) read.GetString("id");
                    dict_player["username"] = (string) read.GetString("username");
                    dict_player["password"] = (string) read.GetString("password");
                    dict_player["grade"] = (string) read.GetString("grade");
                    dict_player["profil"] = (string) read.GetString("profil");
                }
                read.Close();
                deconnection();
                return dict_player;
            }
            catch(Exception e)
            {
                Console.WriteLine("Message erreur get info player : " + e.Message);
                deconnection();
                return dict_player;
            }
        }
    }
}
