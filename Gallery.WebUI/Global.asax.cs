using Gallery.Domain.Entities;
using Gallery.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Gallery.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FileInfo f = new FileInfo(Server.MapPath("log4net.config"));
            log4net.Config.XmlConfigurator.Configure(f);

            ModelBinders.Binders.Add(typeof(User), new UserBinder());
        }
    }
}
