using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModelDAL;
using TaskService.Models;

namespace TaskService.Services
{
    //Task id's are NOT the identity in the database
    public class TaskService : ITaskService
    {
        public void AddTask(TaskModel model)
        {
            using (var db = new NUCodeTaskEntities())
            {
                var query = db.Tasks.Select(x => x);
                model.TaskId = query.Count();

                TaskModelDAL.Task taskToAdd = new TaskModelDAL.Task()
                {
                    TaskName = model.Name,
                    DateCompleted = model.DateCompleted,
                    DateStarted = model.DateStart,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    EstimatedDuration = $"{model.EstimateDuration.Hours}-{model.EstimateDuration.Minutes}-{model.EstimateDuration.Seconds}",
                    id = model.TaskId,
                    isCompleted = model.IsCompleted,
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
            using (var db = new NUCodeTaskEntities())
            {
                var task = db.Tasks.Where(x => x.id == id).First();
                db.Tasks.Remove(task);

                db.SaveChanges();
            }
        }

        public void EditTaskById(int id, TaskModel model)
        {
            throw new NotImplementedException();
        }

        public AllTasks GetAllArchivedTasks()
        {
            AllTasks allTasks = new AllTasks();
            using (var db = new NUCodeTaskEntities())
            {
                var query = db.Tasks.Where(x => x.isCompleted == true);
                var tasks = query.OrderBy(x => x.DueDate).ToList();

                tasks.ForEach(x => allTasks.Tasks.Add(new TaskModel()
                {
                    Name = x.TaskName,
                    DateCompleted = x.DateCompleted,
                    DateStart = x.DateStarted,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    EstimateDuration = new TimeSpan(int.Parse(x.EstimatedDuration.Split('-')[0]), int.Parse(x.EstimatedDuration.Split('-')[1]), int.Parse(x.EstimatedDuration.Split('-')[2])),
                    IsCompleted = x.isCompleted,
                    Tags = new List<string>() { x.Tag1, x.Tag2, x.Tag3 },
                    TaskId = x.id
                }));
            }

            return allTasks;
        }

        public AllTasks GetAllTasks()
        {
            AllTasks allTasks = new AllTasks();
            using (var db = new NUCodeTaskEntities())
            {
                var query = db.Tasks.Select(x => x);
                var tasks = query.OrderBy(x => x.DueDate).ToList();
                
                tasks.ForEach(x => allTasks.Tasks.Add(new TaskModel()
                {
                    Name = x.TaskName,
                    DateCompleted = x.DateCompleted,
                    DateStart = x.DateStarted,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    EstimateDuration = new TimeSpan(int.Parse(x.EstimatedDuration.Split('-')[0]), int.Parse(x.EstimatedDuration.Split('-')[1]), int.Parse(x.EstimatedDuration.Split('-')[2])),
                    IsCompleted = x.isCompleted,
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
    }
}
