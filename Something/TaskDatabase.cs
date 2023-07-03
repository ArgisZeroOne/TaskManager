using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.App.DownloadManager;

namespace Something
{
    class TaskDatabase
    {

        string connectionString = "Server=90.189.194.247;User ID=root;Password=root;Database=task_manager;SslMode=None"; //Характеристики подключения к MySQL
        public void CreateTask(Task task)
        {
          
            using (MySqlConnection connection = new MySqlConnection(new TaskDatabase().connectionString))
            {
                connection.Open();

                string query = "INSERT Into TasksData (orderNumber, address, date, model, notes, time) VALUES (@ordernumber, @address,@date,@model,@notes,@time)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ordernumber", task.OrderNumber + "");
                cmd.Parameters.AddWithValue("@address", task.Address + "");
                cmd.Parameters.AddWithValue("@date", task.Date);
                cmd.Parameters.AddWithValue("@model", task.Model + "");
                cmd.Parameters.AddWithValue("@notes", task.Notes + "");
                cmd.Parameters.AddWithValue("@time", task.Time);
                cmd.ExecuteNonQuery();

            }
          

            
        } //Метод создания задания в базе данных MySQL
        List<Task> ReadTask(MySqlCommand cmd)
        {
            List<Task> taskList = new List<Task>();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                   
                    Task task = new Task(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).Date.ToString("dd/MM/yyyy"), reader.GetString(3), reader.GetString(4), reader.GetTimeSpan(5).ToString());
                    taskList.Add(task);
                } catch(Exception ex) { 
                   
                }
                

            }
            if (taskList != null) return taskList;
            else
            {
                taskList.Add(new Task());
                return taskList;
            }
        } // Метод считывания данных заданий из базы данных
        void ExecuteSet(string orderNumber, string newvalue, string param, MySqlConnection connection, string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue(param, newvalue);

            cmd.Parameters.AddWithValue("@ordernumber", orderNumber);

            cmd.ExecuteNonQuery();
        } //Универсальный метод для изменения конкретных данных
        public void SetTimeToTask(string orderNumber, string newTime)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                ExecuteSet(orderNumber, newTime, "@time", connection, "Update TasksData, (select * from TasksData where orderNumber = @ordernumber) " +
                    "as Selected set TasksData.time = @time" +
                    " where TasksData.orderNumber = Selected.orderNumber");


            };
        } // Метод изменения времени задания
        public  void SetAddressToTask(string orderNumber,  string newAddress)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                ExecuteSet(orderNumber,newAddress,"@address",connection,"Update TasksData, (select * from TasksData where orderNumber = @ordernumber)" +
                    " as Selected set TasksData.address = @address" +
                    " where TasksData.orderNumber = Selected.orderNumber");

               
            }
        } // Метод изменения адреса задания
        public  void SetDateToTask(string orderNumber, string newDate)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                ExecuteSet(orderNumber,newDate,"@date",connection,"Update TasksData,(select * from TasksData where orderNumber = @ordernumber)" +
                    " as Selected set TasksData.date = @date" +
                    "  where TasksData.orderNumber = Selected.orderNumber");
            }
        } //Метод изменения даты задания 
        public  void SetModelToTask(string orderNumber, string newModel)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                ExecuteSet(orderNumber,newModel,"@model",connection,"Update TasksData,(select * from TasksData where orderNumber = @ordernumber)" +
                    " as Selected set TasksData.model = @model" +
                    " where TasksData.orderNumber = Selected.orderNumber");

            
            }
        } // Метод изменения модели задания
        public  void SetNoteToTask(string orderNumber, string newNote)
        {
            List<Task> tasks = new List<Task>();
           
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                ExecuteSet(orderNumber,newNote,"@notes",connection,"Update TasksData,(select * from TasksData where orderNumber = @ordernumber)" +
                    " as Selected  set TasksData.notes = @notes" +
                    " where TasksData.orderNumber = Selected.orderNumber");

            }
        } // Метод изменения примечаний задания
        public  List<Task> GetTasksFromDBToDate(string date)
        {

            List<Task> tasks = new List<Task>();
           
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM TasksData where date = @date order by date";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@date", date);

                tasks = ReadTask(cmd);

            }
            return tasks;
        } // Метод получения заданий к определённой дате
    }
   
}
