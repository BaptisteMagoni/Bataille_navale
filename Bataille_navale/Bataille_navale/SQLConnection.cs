using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
            }
            catch
            {
                m_state_connection = false;
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

        public List<string> getDictPlayer(string username)
        {
            connection();
            List<string> list_player = new List<string>();
            try
            {
                String sql = String.Format("SELECT taux FROM users WHERE username = {0}", username);
                MySqlCommand cmd = new MySqlCommand(sql, m_connection);
                MySqlDataReader read = cmd.ExecuteReader();
                list_player.Add(read.GetString("id"));
                list_player.Add(read.GetString("username"));
                list_player.Add(read.GetString("password"));
                list_player.Add(read.GetString("grade"));
                read.Close();
                deconnection();
                return list_player;
            }
            catch
            {
                return list_player;
            }
        }
    }
}
