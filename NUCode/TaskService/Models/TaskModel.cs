using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Models
{
    public class TaskModel
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public TimeSpan EstimateDuration { get; set; }
        public List<string> Tags { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateStart { get; set; }
        public string Description { get; set; }
        public int TaskId { get; set; }
        public static int Id = 1;

        public TaskModel(int id, string name = "Task", DateTime dueDate = new DateTime(), TimeSpan estDuration = new TimeSpan(), List<string> tags = null, bool isCompleted = false, DateTime dateCompleted = new DateTime(), DateTime dateStart = new DateTime(), string desc = "Task Due")
        {
            TaskId = id;
            Name = name;
            DueDate = dueDate;
            EstimateDuration = estDuration;
            Tags = new List<string>()
            {
                "Task"
            };
            IsCompleted = isCompleted;
            DateCompleted = dateCompleted;
            DateStart = dateStart;
            Description = desc;
        }

        public TaskModel(string name)
        {
            TaskId = Id++;
            Name = name;
            DueDate = new DateTime(2017, 11, 12);
            EstimateDuration = new TimeSpan(1, 30, 0);
            Tags = new List<string>()
            {
                "Task",
                "Main",
                "Urgent",
                name
            };
            IsCompleted = false;
            DateCompleted = new DateTime();
            DateStart = new DateTime(2017, 11, 8);
            Description = $"Complete {name}";
        }

        public TaskModel()
        {

        }
    }
}