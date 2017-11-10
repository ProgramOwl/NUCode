using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Models;

namespace TaskService.Services
{
    public interface ITaskService
    {
        AllTasks GetAllTasks();
        TaskModel GetTaskById(int id);
        void AddTask(TaskModel model);
        void EditTaskById(int id, TaskModel model);
        void DeleteTaskById(int id);
        AllTasks GetAllArchivedTasks();
    }
}
