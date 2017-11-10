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
        public DateTime TimeDue { get; set; }
    }
}