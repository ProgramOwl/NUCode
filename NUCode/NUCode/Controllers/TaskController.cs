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
        [Authorize(Roles = "Admin")]
        public ActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddTask(TaskModel model)
        {
            Service.AddTask(model);
            return View("TaskList", Service.GetAllTasks());
        }
        
        public ActionResult TaskList()
        {
            ViewBag.Detail = false;
            return View("TaskList", Service.GetAllTasks());
        }
        
        public ActionResult ArchiveTask()
        {
            ViewBag.Detail = false;
            return View("ArchiveTask", Service.GetAllArchivedTasks());
        }

        public ActionResult TaskDetail(int id)
        {
            ViewBag.Detail = true;
            TaskModel model = Service.GetTaskById(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditTask(int id)
        {
            TaskModel model = Service.GetTaskById(id);
            ViewBag.ID = id;
            TaskService.Services.TaskService.holderID = id;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditTask(TaskModel model)
        {
            Service.EditTaskById(TaskService.Services.TaskService.holderID, model);
            ViewBag.Title = "Task Catalog";
            return View("TaskList", Service.GetAllTasks());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTask(int id)
        {
            Service.DeleteTaskById(id);
            ViewBag.Title = "Product Catalog";
            ViewBag.ProDetail = false;
            return View("ProductCatalog", Service.GetAllTasks());
        }
    }
}