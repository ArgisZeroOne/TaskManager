
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Something
{
    class UsersDatabase
    {
        string connectionString = "Server=90.189.194.247;User ID=root;Password=root;Database=task_manager;SslMode=None";

        private MySqlConnection _usersData;
        void Connect(string connectionString)
        {
            this._usersData = new MySqlConnection(connectionString);
        }
        void DeleteLocalUser()
        {
           Preferences.Default.Clear();
        }
        public static User ReadLocalUserSettings()
        {
            User user = new User(
                Preferences.Default.Get("username","default"),
                Preferences.Default.Get("password","default"),
                Preferences.Default.Get("firstname","default"),
                Preferences.Default.Get("lastname","default"),
                Preferences.Default.Get("rights","default")
                );
           
            return user;
        }
        public static void WriteLocalUserSettings(User user)
        {
            Preferences.Default.Set("username", user.Username());
            Preferences.Default.Set("password", user.Password());
            Preferences.Default.Set("firstname", user.FirstName());
            Preferences.Default.Set("lastname",user.LastName());
            Preferences.Default.Set("rights", user.Rights());
            
        }
        public User ChooseUser(string username, string password)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(password);

            byte[] hashValue = SHA256.HashData(messageBytes);

            string passwordHash = Convert.ToHexString(hashValue);
            
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
             
                string query = "select * from UsersData where username =@username";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader reader = cmd.ExecuteReader();
                string usernamefrombase = null;
                string passwordfrombase = null;
                string firstnamefrombase = null;
                string secondnamefrombase = null;
                string rightsfrombase = null;
                while (reader.Read())
                {
                    usernamefrombase = reader.GetString(0);
                    passwordfrombase = reader.GetString(1);
                    firstnamefrombase= reader.GetString(2);
                    secondnamefrombase= reader.GetString(3);
                    rightsfrombase= reader.GetString(4);

                }
                if (passwordfrombase == passwordHash)
                {
                    User user = new User(usernamefrombase, password, firstnamefrombase, secondnamefrombase,rightsfrombase);
                    
                    return user;
                }
                else
                {
                    User user = null;
                    return user;
                }

               

            }
        }
    }
}
