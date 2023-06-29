using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Something
{
    public class Task
    {
        public string OrderNumber { get; set; }
        public string Address { get; set; }
        public string Date { get; set; } 
        public string Model { get; set; }
        public string Notes { get; set; }

        public string Time { get; set; }

        public Task(string orderNumber, string address, string date, string model, string notes, string time)
        {
            OrderNumber = orderNumber;
            Address = address;
            Date = date;
            Model = model;
            Notes = notes;
            Time = time;
        }
        public Task(string orderNumber, string address, string date, string model, string notes)
        {
            OrderNumber = orderNumber;
            Address = address;
            Date = date;
            Model = model;
            Notes = notes;
           
        }
    }
}
