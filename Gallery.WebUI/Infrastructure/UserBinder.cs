using Gallery.Domain.Entities;
using Gallery.Domain.Utils;
using log4net;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gallery.WebUI.Infrastructure
{
    public class UserBinder : IModelBinder
    {
        const string SessionKey = "User";

        private ILog log = LogManager.GetLogger("UserBinder");

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            User user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = controllerContext.HttpContext.Session[SessionKey] as User;
            }

            if (user == null)
            {
                user = new User();
                try
                {
                    using (ISession session = HibernateUtil.OpenSession())
                    {
                        using (ITransaction t = session.BeginTransaction())
                        {
                            session.Save(user);
                            t.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }

                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[SessionKey] = user;
                }
            }
            return user;
        }
    }
}