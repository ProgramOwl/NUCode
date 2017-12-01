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
            ViewBag.Detail = false;

            if (ModelState.IsValid)
            {
                model.UserId = Service.GetUserIdByName(User.Identity.Name);
                Service.AddTask(model);
                return View("TaskList", Service.GetAllTasksById(model.UserId));
            }
            else
            {
                return View();
            }

        }
        
        public ActionResult TaskList()
        {
            ViewBag.Detail = false;
            if (User.IsInRole("Admin"))
            {
                return View("TaskList", Service.GetAllTasks());
            }
            else
            {
                return View("TaskList", Service.GetAllTasksById(Service.GetUserIdByName(User.Identity.Name)));
            }
        }
        
        public ActionResult ArchivedTask()
        {
            ViewBag.Detail = false;
            return View("ArchivedTask", Service.GetAllArchivedTasksById(Service.GetUserIdByName(User.Identity.Name)));
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
            return View("TaskList", Service.GetAllTasksById(Service.GetUserIdByName(User.Identity.Name)));
        }
        
        public ActionResult DeleteTask(int id)
        {
            TaskService.Services.TaskService.holderID = id;
            Service.DeleteTaskById(id);
            ViewBag.Title = "Task List";
            ViewBag.Detail = false;
            return View("TaskList", Service.GetAllTasksById(Service.GetUserIdByName(User.Identity.Name)));
        }

        public ActionResult ArchiveTask(int id)
        {
            TaskService.Services.TaskService.holderID = id;
            ViewBag.Detail = false;
            TaskModel model = Service.GetTaskById(id, Service.GetUserIdByName(User.Identity.Name));
            model.UserId = Service.GetUserIdByName(User.Identity.Name);
            Service.ArchiveTask(model);
            return View("TaskList", Service.GetAllTasksById(model.UserId));
        }
    }
}