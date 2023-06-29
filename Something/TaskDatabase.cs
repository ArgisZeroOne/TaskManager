using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static CoreFoundation.DispatchSource;

namespace Something
{
    class TaskDatabase
    {
        public void NewTask()
        {

        }
        public static void CreateTask(Task task)
        {
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT Into TasksData (orderNumber, address, date, model, notes, time) VALUES (@ordernumber, @address,@date,@model,@notes,@time)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ordernumber", task.OrderNumber + "");
                cmd.Parameters.AddWithValue("@address", task.Address + "");
                cmd.Parameters.AddWithValue("@date", task.Date);
                cmd.Parameters.AddWithValue("@model", task.Model + "");
                cmd.Parameters.AddWithValue("@notes", task.Notes + "");
                cmd.Parameters.AddWithValue("@time", task.Time);
                cmd.ExecuteNonQuery();

            }
          

            
        }
        public static List<Task> GetAllTasksFromDB()
        {
            List<Task> tasks = new List<Task>();
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM TasksData order by date";

                SqlCommand cmd = new SqlCommand(query, connection);

                tasks = ReadTask(cmd);

            }
            return tasks;
        }
        public static List<Task> GetTodayTasksFromDB()
        {
            List<Task> tasks = new List<Task>();
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM TasksData where date = @date order by date";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@date", DateTime.Today.ToString());

                tasks = ReadTask(cmd);

            }
            return tasks;
        }
       static List<Task> ReadTask(SqlCommand cmd)
        {
            List<Task> taskList = new List<Task>();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    Task task = new Task(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetString(3), reader.GetString(4), reader.GetTimeSpan(5).ToString());
                    taskList.Add(task);
                } catch(Exception ex) { 
                    Debug.WriteLine(ex.Message);
                }
                

            }
            return taskList;
        }
        public static void SetTimeToTask(string orderNumber,string newTime)
        {
            List<Task> tasks = new List<Task>();
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "Update TasksData set time = @time from (select * from TasksData where orderNumber = @ordernumber) as Selected where TasksData.orderNumber = Selected.orderNumber";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@time", newTime);

                cmd.Parameters.AddWithValue("@ordernumber", orderNumber);

                cmd.ExecuteNonQuery();
            };
        }
        public static void SetAddressToTask(string orderNumber,  string newAddress)
        {
            List<Task> tasks = new List<Task>();
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "Update TasksData set address = @address from (select * from TasksData where orderNumber = @ordernumber) as Selected where TasksData.orderNumber = Selected.orderNumber";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@address", newAddress);

                cmd.Parameters.AddWithValue("@ordernumber", orderNumber);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex) { }
            }
        }
        public static void SetDateToTask(string orderNumber, string newDate)
        {
            List<Task> tasks = new List<Task>();
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "Update TasksData set date = @date from (select * from TasksData where orderNumber = @ordernumber) as Selected where TasksData.orderNumber = Selected.orderNumber";

                SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    cmd.Parameters.AddWithValue("@date", newDate);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
           

                cmd.Parameters.AddWithValue("@ordernumber", orderNumber);

              
                
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) {
                Debug.WriteLine(ex);
                }
            }
        }
        public static void SetModelToTask(string orderNumber, string newModel)
        {
            List<Task> tasks = new List<Task>();
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "Update TasksData set model = @model from (select * from TasksData where orderNumber = @ordernumber) as Selected where TasksData.orderNumber = Selected.orderNumber";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@model", newModel);

                cmd.Parameters.AddWithValue("@ordernumber", orderNumber);



                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        public static void SetNoteToTask(string orderNumber, string newNote)
        {
            List<Task> tasks = new List<Task>();
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "Update TasksData set notes = @notes from (select * from TasksData where orderNumber = @ordernumber) as Selected where TasksData.orderNumber = Selected.orderNumber";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@notes", newNote);

                cmd.Parameters.AddWithValue("@ordernumber", orderNumber);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        public static List<Task> GetTasksFromDBToDate(string date)
        {
            List<Task> tasks = new List<Task>();
            string connectionString = @"Server=tcp:90.189.194.247, 1433;User Id = nikko;pwd = kimura2023;Database=task_manager;TrustServerCertificate=Yes";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM TasksData where date = @date order by date";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@date", date);

                tasks = ReadTask(cmd);

            }
            return tasks;
        }
    }
   
}
