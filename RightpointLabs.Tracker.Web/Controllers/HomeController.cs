using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RightpointLabs.Tracker.Web.Models;

namespace RightpointLabs.Tracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Application.Tracker _tracker;

        public HomeController(Application.Tracker tracker)
        {
            _tracker = tracker;
        }

        public ActionResult Index()
        {
            return View(new HomeIndexModel
            {
                IsRunning = _tracker.IsRunning,
                LastSnapshot = _tracker.LastSnapshot
            });
        }
    }
}