using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.MVC.FrameWork.NHibernate
{
    public class NHSessionFactory
    {
        private static readonly string DefaultFactoryKey = "default";
        private static readonly ConcurrentDictionary<string, NHSessionFactory> dict = new ConcurrentDictionary<string, NHSessionFactory>();
        private ISessionFactory sf;
        private Configuration cfg;

        private NHSessionFactory()
        {

        }

        public static ISession OpenSession()
        {
            return OpenSession(DefaultFactoryKey);
        }

        public static ISession OpenSession(string name)
        {
            var sessionFactory = dict[name];
            return sessionFactory.sf.OpenSession();
        }

        public static void InitSessionFactory(string xmlPath)
        {
            InitSessionFactory(DefaultFactoryKey, xmlPath);
        }

        public static void InitSessionFactory(string name, string xmlPath)
        {
            if (!dict.ContainsKey(name))
            {
                var sessionFactory = new NHSessionFactory();
                sessionFactory.cfg = new Configuration();
                sessionFactory.cfg.Configure(xmlPath);
                sessionFactory.sf = sessionFactory.cfg.BuildSessionFactory();
                dict.TryAdd(name, sessionFactory);
            }
        }
    }
}
