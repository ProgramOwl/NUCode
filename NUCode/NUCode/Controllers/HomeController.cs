using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskService.Models;
using TaskService.Services;

namespace NUCode.Controllers
{
    public class HomeController : Controller
    {
        [Authorize()]
        public ActionResult Index()
        {
            ITaskService Service = new TaskService.Services.TaskService();
            return View(Service.GetTodayTasks(User.Identity.Name));
        }

        public ActionResult About()
        {
            ViewBag.Message = "NUCode Neumonteers";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "NUCode Neumonteers";

            return View();
        }
    }
}