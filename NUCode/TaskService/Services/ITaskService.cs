using ECommerceService.Model;
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
        AllTasks GetAllTasks(string currentUserId);
        TaskModel GetTaskById(int id, string currentUserId);
        void AddTask(TaskModel model);
        void EditTaskById(int id, TaskModel model);
        void DeleteTaskById(int id);
        AllTasks GetAllArchivedTasks();
        UserList GetAllUsers();
        void DemoteAdminToUserById(string Id);
        void PromoteUserToAdminById(string Id);
        string GetUserIdByName(string name);
    }
}
