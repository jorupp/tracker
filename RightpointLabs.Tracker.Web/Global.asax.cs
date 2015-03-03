using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RightpointLabs.Tracker.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitLogging();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void InitLogging()
        {
            // initialize log4net
            var file = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            if (System.IO.File.Exists(file))
            {
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(file));
            }
        }
    }
}
