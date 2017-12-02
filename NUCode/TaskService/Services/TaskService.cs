using ECommerceService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            using (var db = new TaskModelEntities())
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
                    EstimatedDuration = model.EstimateDuration.ToString(),
                    id = model.TaskId,
                    IsCompleted = false,
                    Tag1 = model.Tags[0],
                    Tag2 = "",
                    Tag3 = "",
                    TaskValue = model.TaskValue,
                    UserId = model.UserId
                };

                db.Tasks.Add(taskToAdd);

                db.SaveChanges();
            }
        }

        public void DeleteTaskById(int id)
        {
            using (var db = new TaskModelEntities())
            {
                var task = db.Tasks.Where(x => x.id == holderID).First();
                db.Tasks.Remove(task);

                db.SaveChanges();
            }
        }

        public void EditTaskById(int id, TaskModel model)
        {
            using (var db = new TaskModelEntities())
            {
                TaskModelDAL.Task taskToEdit = new TaskModelDAL.Task()
                {
                    Name = model.Name,
                    DateCompleted = model.DateCompleted,
                    DateStart = model.DateStart,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    EstimatedDuration = model.EstimateDuration.ToString(),
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

        public AllTasks GetAllArchivedTasksById(string currentUserId)
        {
            AllTasks allTasks = new AllTasks();
            using (var db = new TaskModelEntities())
            {
                var query = db.Tasks.Where(x => x.IsCompleted == true);
                var tasks = query.OrderBy(x => x.DueDate).ToList();

                foreach (var task in tasks)
                {
                    if (task.IsCompleted && task.UserId == currentUserId)
                    {
                        allTasks.Tasks.Add(new TaskModel()
                        {
                            Name = task.Name,
                            DateCompleted = task.DateCompleted,
                            DateStart = task.DateStart,
                            Description = task.Description,
                            DueDate = task.DueDate,
                            EstimateDuration = int.Parse(task.EstimatedDuration),
                            IsCompleted = task.IsCompleted,
                            Tags = new List<string>() { task.Tag1, task.Tag2, task.Tag3 },
                            TaskId = task.id,
                            TaskValue = task.TaskValue
                        });
                    }
                }

            }

            return allTasks;
        }

        public AllTasks GetAllTasks()
        {
            AllTasks allTasks = new AllTasks();
            using (var db = new TaskModelEntities())
            {
                var query = db.Tasks.Select(x => x);
                var tasks = query.OrderBy(x => x.DueDate).ToList();
                
                foreach(var task in tasks)
                {
                    if (!task.IsCompleted)
                    {
                        allTasks.Tasks.Add(new TaskModel()
                        {
                            Name = task.Name,
                            DateCompleted = task.DateCompleted,
                            DateStart = task.DateStart,
                            Description = task.Description,
                            DueDate = task.DueDate,
                            EstimateDuration = int.Parse(task.EstimatedDuration),
                            IsCompleted = task.IsCompleted,
                            Tags = new List<string>() { task.Tag1, task.Tag2, task.Tag3 },
                            TaskId = task.id
                        });
                    }
                }
            }
            return allTasks;
        }

        public AllTasks GetAllTasksById(string currentUserId)
        {
            AllTasks allTasks = new AllTasks();
            using (var db = new TaskModelEntities())
            {
                var query = db.Tasks.Select(x => x);
                var tasks = query.OrderBy(x => x.DueDate).ToList();

                foreach (var task in tasks)
                {
                    if (!task.IsCompleted && task.UserId == currentUserId)
                    {
                        allTasks.Tasks.Add(new TaskModel()
                        {
                            Name = task.Name,
                            DateCompleted = task.DateCompleted,
                            DateStart = task.DateStart,
                            Description = task.Description,
                            DueDate = task.DueDate,
                            EstimateDuration = int.Parse(task.EstimatedDuration),
                            IsCompleted = task.IsCompleted,
                            Tags = new List<string>() { task.Tag1, task.Tag2, task.Tag3 },
                            TaskId = task.id,
                            TaskValue = task.TaskValue
                        });
                    }
                }
            }
            return allTasks;
        }

        public TaskModel GetTaskById(int id, string currentUserId)
        {
            try
            {
                return GetAllTasksById(currentUserId).Tasks.Where(x => x.TaskId == holderID).First();
            }
            catch (Exception)
            {
                return GetAllArchivedTasksById(currentUserId).Tasks.Where(x => x.TaskId == holderID).First();
            }
        }

        public string GetUserIdByName(string name)
        {
            string retVal = "";
            using (var db = new UserModelEntities())
            {
                retVal = db.AspNetUsers.Where(x => x.UserName == name).First().Id;
            }
            return retVal;
        }

        public UserList GetAllUsers()
        {
            UserList users = new UserList();

            using (var db = new UserModelEntities())
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
            using (var db = new UserModelEntities())
            {
                db.AspNetUserRoles.Remove(db.AspNetUserRoles.Where(x => x.UserId == Id).First());
                db.SaveChanges();
            }
        }

        public void PromoteUserToAdminById(string Id)
        {
            using (var db = new UserModelEntities())
            {
                db.AspNetUserRoles.Add(new AspNetUserRole() { UserId = Id, RoleId = "c8299935-2c18-49a9-9765-22431174bece" });
                db.SaveChanges();
            }
        }

        public AllTasks GetTodayTasks(string name)
        {
            AllTasks at = new AllTasks();
                at.Tasks = GetAllTasksById(GetUserIdByName(name)).Tasks.Where(x => x.DueDate.Day == DateTime.Now.Day && x.DueDate.Month == DateTime.Now.Month && x.DueDate.Year == DateTime.Now.Year).ToList();
            return at;
        }

        public void ArchiveTask(TaskModel model)
        {
            using (var db = new TaskModelEntities())
            {
                TaskModelDAL.Task taskToEdit = new TaskModelDAL.Task()
                {
                    Name = model.Name,
                    DateCompleted = model.DateCompleted,
                    DateStart = model.DateStart,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    EstimatedDuration = model.EstimateDuration.ToString(),
                    id = holderID,
                    IsCompleted = !model.IsCompleted,
                    TaskValue = model.TaskValue,
                    Tag1 = model.Tags[0],
                    Tag2 = model.Tags[1],
                    Tag3 = model.Tags[2],
                    UserId = model.UserId

                };

                DeleteTaskById(model.TaskId);

                db.Tasks.Add(taskToEdit);
                try
                {
                    db.SaveChanges();

                }
                catch (Exception e)
                {

                    string s = e.Message;
                }
            }
        }
    }
}