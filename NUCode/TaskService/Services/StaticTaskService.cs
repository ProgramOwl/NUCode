using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Models;

namespace TaskService.Services
{
    public class StaticTaskService : ITaskService
    {
        public void AddTask(TaskModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public void EditTaskById(int id, TaskModel model)
        {
            throw new NotImplementedException();
        }

        public AllTasks GetAllArchivedTasks()
        {
            throw new NotImplementedException();
        }

        public AllTasks GetAllTasks()
        {
            AllTasks allTasks = new AllTasks();

            for (int i = 0; i < 5; i++)
            {
                allTasks.Tasks.Add(new TaskModel("Test Task"));
            }

            return allTasks;
        }

        public TaskModel GetTaskById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
