using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Utils
{
    public class HibernateUtil
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if(_sessionFactory == null)
                {
                    var cfg = new Configuration();
                    cfg.Configure();

                    Assembly assembly = Assembly.GetAssembly(typeof(HibernateUtil));
                    var names = assembly.GetManifestResourceNames();
                    foreach(var name in names)
                    {
                        var stream = assembly.GetManifestResourceStream(name);
                        cfg.AddInputStream(stream);
                    }

                    _sessionFactory = cfg.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static ISession OpenSession(IInterceptor interceptor)
        {
            return SessionFactory.OpenSession(interceptor);
        }
    }
}
