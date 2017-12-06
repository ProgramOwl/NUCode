using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            string userName = User.Identity.Name;
            ViewBag.Velocity = Service.GetVelocity(User.Identity.Name);
            var myChart = new Chart(600, 200, ChartTheme.Green)
           .AddTitle($"{DateTime.Now.Year} Velocity")
           .AddSeries(
                chartType: "Range",
                markerStep: 1,
               xValue: new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
               yValues: new[] 
               {
                   Service.GetVelocityByMonth(userName, 1).ToString(),
                   Service.GetVelocityByMonth(userName, 2).ToString(),
                   Service.GetVelocityByMonth(userName, 3).ToString(),
                   Service.GetVelocityByMonth(userName, 4).ToString(),
                   Service.GetVelocityByMonth(userName, 5).ToString(),
                   Service.GetVelocityByMonth(userName, 6).ToString(),
                   Service.GetVelocityByMonth(userName, 7).ToString(),
                   Service.GetVelocityByMonth(userName, 8).ToString(),
                   Service.GetVelocityByMonth(userName, 9).ToString(),
                   Service.GetVelocityByMonth(userName, 10).ToString(),
                   Service.GetVelocityByMonth(userName, 11).ToString(),
                   Service.GetVelocityByMonth(userName, 12).ToString()
               })
               .Save("C:/Users/Dylan Shoupe/Documents/GitHub/LMSWebsite/NUCode/NUCode/Content/TempImages/Chart.jpg");
            
            
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