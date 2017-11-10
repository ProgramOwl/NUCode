using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NUCode.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult AddTask()
        {
            return View();
        }

        public ActionResult TaskList()
        {
            return View();
        }

        public ActionResult ArchiveTask()
        {
            return View();
        }
    }
}