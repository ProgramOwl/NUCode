using ECommerceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModelDAL;
using TaskService.Models;

namespace TaskService.Services
{
    public class TaskService : ITaskService
    {
        public static int holderID = 1;
        public void AddTask(TaskModel model)
        {
            using (var db = new NUCodeTasksEntities())
            {
                var query = db.Tasks.Select(x => x);
                model.TaskId = query.Count();

                TaskModelDAL.Task taskToAdd = new TaskModelDAL.Task()
                {
                    Name = model.Name,
                    DateCompleted = model.DateCompleted,
                    DateStart = model.DateStart,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    EstimatedDuration = $"{model.EstimateDuration.Hours}-{model.EstimateDuration.Minutes}-{model.EstimateDuration.Seconds}",
                    id = model.TaskId,
                    IsCompleted = model.IsCompleted,
                    Tag1 = model.Tags[0],
                    Tag2 = model.Tags[1],
                    Tag3 = model.Tags[2]
                };

                db.Tasks.Add(taskToAdd);
                
                db.SaveChanges();
            }
        }

        public void DeleteTaskById(int id)
        {
            using (var db = new NUCodeTasksEntities())
            {
                var task = db.Tasks.Where(x => x.id == holderID).First();
                db.Tasks.Remove(task);

                db.SaveChanges();
            }
        }

        public void EditTaskById(int id, TaskModel model)
        {
            using (var db = new NUCodeTasksEntities())
            {
                TaskModelDAL.Task taskToEdit = new TaskModelDAL.Task()
                {
                    Name = model.Name,
                    DateCompleted = model.DateCompleted,
                    DateStart = model.DateStart,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    EstimatedDuration = $"{model.EstimateDuration.Hours}-{model.EstimateDuration.Minutes}-{model.EstimateDuration.Seconds}",
                    id = holderID,
                    IsCompleted = model.IsCompleted,
                    Tag1 = model.Tags[0],
                    Tag2 = model.Tags[1],
                    Tag3 = model.Tags[2],
                };

                DeleteTaskById(model.TaskId);

                db.Tasks.Add(taskToEdit);

                db.SaveChanges();
            }
        }

        public AllTasks GetAllArchivedTasks()
        {
            AllTasks allTasks = new AllTasks();
            using (var db = new NUCodeTasksEntities())
            {
                var query = db.Tasks.Where(x => x.IsCompleted == true);
                var tasks = query.OrderBy(x => x.DueDate).ToList();

                tasks.ForEach(x => allTasks.Tasks.Add(new TaskModel()
                {
                    Name = x.Name,
                    DateCompleted = x.DateCompleted,
                    DateStart = x.DateStart,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    EstimateDuration = new TimeSpan(int.Parse(x.EstimatedDuration.Split('-')[0]), int.Parse(x.EstimatedDuration.Split('-')[1]), int.Parse(x.EstimatedDuration.Split('-')[2])),
                    IsCompleted = x.IsCompleted,
                    Tags = new List<string>() { x.Tag1, x.Tag2, x.Tag3 },
                    TaskId = x.id
                }));
            }

            return allTasks;
        }

        public AllTasks GetAllTasks()
        {
            AllTasks allTasks = new AllTasks();
            using (var db = new NUCodeTasksEntities())
            {
                var query = db.Tasks.Select(x => x);
                var tasks = query.OrderBy(x => x.DueDate).ToList();
                
                tasks.ForEach(x => allTasks.Tasks.Add(new TaskModel()
                {
                    Name = x.Name,
                    DateCompleted = x.DateCompleted,
                    DateStart = x.DateStart,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    EstimateDuration = new TimeSpan(int.Parse(x.EstimatedDuration.Split('-')[0]), int.Parse(x.EstimatedDuration.Split('-')[1]), int.Parse(x.EstimatedDuration.Split('-')[2])),
                    IsCompleted = x.IsCompleted,
                    Tags = new List<string>() { x.Tag1, x.Tag2, x.Tag3 },
                    TaskId = x.id
                }));
            }

            return allTasks;
        }

        public TaskModel GetTaskById(int id)
        {
            return GetAllTasks().Tasks.Where(x => x.TaskId == id).First();
        }

        public UserList GetAllUsers()
        {
            UserList users = new UserList();

            using (var db = new NUCodeUsersEntities())
            {
                var query = db.AspNetUsers.Select(x => x).OrderBy(x => x.UserName);

                var userModels = query.ToList();

                string roleId = "";
                foreach (AspNetUser user in userModels)
                {
                    try
                    {
                        roleId = db.AspNetUserRoles.Where(x => x.UserId == user.Id).First().RoleId;
                    }
                    catch (Exception)
                    {
                        roleId = null;
                    }

                    users.Users.Add(new User(user.UserName, user.Id, roleId));
                }
            }

            return users;
        }

        public void DemoteAdminToUserById(string Id)
        {
            using (var db = new NUCodeUsersEntities())
            {
                db.AspNetUserRoles.Remove(db.AspNetUserRoles.Where(x => x.UserId == Id).First());
                db.SaveChanges();
            }
        }

        public void PromoteUserToAdminById(string Id)
        {
            using (var db = new NUCodeUsersEntities())
            {
                db.AspNetUserRoles.Add(new AspNetUserRole() { UserId = Id, RoleId = "c8299935-2c18-49a9-9765-22431174bece", AspNetUser = db.AspNetUsers.Where(x => x.Id == Id).First() });
                db.SaveChanges();
            }
        }
    }
}