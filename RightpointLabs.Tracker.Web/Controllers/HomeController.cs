using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RightpointLabs.Tracker.Domain;
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
                IsRunning = _tracker.IsRunning
            });
        }

        public async Task<JsonResult> LatestData()
        {
            return Json((await _tracker.GetNow()).ChainIfNotNull(s => new
            {
                Timestamp = s.Timestamp.ToString("u"),
                Devices = s.Devices.Select(i => new
                {
                    i.IpAddress,
                    i.MacAddress,
                    i.Name,
                    i.X,
                    i.Y,
                }).ToArray()
            }), JsonRequestBehavior.AllowGet);
        }
    }
}