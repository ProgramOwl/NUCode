using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Models
{
    public class AllTasks
    {
        public List<TaskModel> Tasks { get; set; }

        public AllTasks()
        {
            Tasks = new List<TaskModel>();
        }
    }
}