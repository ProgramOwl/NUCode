using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskService.Models;
using TaskService.Services;

namespace NUCode.Controllers
{
    public class TaskController : Controller
    {
        public static ITaskService Service = new TaskService.Services.TaskService();

        [HttpGet]
        public ActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(TaskModel model)
        {
            Service.AddTask(model);
            return View("TaskList", Service.GetAllTasks());
        }

        public ActionResult TaskList()
        {
            return View("TaskList", Service.GetAllTasks());
        }

        public ActionResult ArchiveTask()
        {
            return View("ArchiveTask", Service.GetAllArchivedTasks());
        }

        public ActionResult TaskDetail(int id)
        {
            TaskModel model = Service.GetTaskById(id);

            return View(model);
        }
    }
}