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
            throw new NotImplementedException();
        }
    }
}
