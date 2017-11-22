using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskService.Models;
using TaskService.Services;

namespace NUCode.Controllers
{
    [Authorize()]
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
            ViewBag.Detail = false;
            return View("TaskList", Service.GetAllTasks(Service.GetUserIdByName(User.Identity.Name)));
        }
        
        public ActionResult TaskList()
        {
            ViewBag.Detail = false;
            return View("TaskList", Service.GetAllTasks(Service.GetUserIdByName(User.Identity.Name)));
        }
        
        public ActionResult ArchiveTask()
        {
            ViewBag.Detail = false;
            return View("ArchiveTask", Service.GetAllArchivedTasks());
        }
        
        public ActionResult TaskDetail(int id)
        {
            ViewBag.Detail = true;
            TaskModel model = Service.GetTaskById(id, Service.GetUserIdByName(User.Identity.Name));
            return View(model);
        }

        [HttpGet]
        public ActionResult EditTask(int id)
        {
            TaskService.Services.TaskService.holderID = id;
            TaskModel model = Service.GetTaskById(id, Service.GetUserIdByName(User.Identity.Name));
            ViewBag.ID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditTask(TaskModel model)
        {
            Service.EditTaskById(TaskService.Services.TaskService.holderID, model);
            ViewBag.Title = "Task Catalog";
            ViewBag.Detail = false;
            return View("TaskList", Service.GetAllTasks());
        }
        
        public ActionResult DeleteTask(int id)
        {
            TaskService.Services.TaskService.holderID = id;
            Service.DeleteTaskById(id);
            ViewBag.Title = "Task List";
            ViewBag.Detail = false;
            return View("TaskList", Service.GetAllTasks());
        }
    }
}