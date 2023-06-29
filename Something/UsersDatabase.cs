using Microsoft.Data.SqlClient;
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
        
        private SqlConnection _usersData;
        void Connect(string connectionString)
        {
            // for example default connection string:
            // @"Data Source=.\kitsune;Initial Catalog=UsersData;"

            this._usersData = new SqlConnection(connectionString);
        }
        void CreateUser(string username, string password)
        {

        }
        void DeleteUser()
        {
           // Preferences.Default.Clear();
           
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
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
             
                string query = "select * from UsersData where username =@username";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();
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
