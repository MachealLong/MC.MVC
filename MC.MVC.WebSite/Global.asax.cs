using log4net.Config;
using MC.MVC.Business.SysManage;
using MC.MVC.DataAccess.SysManage;
using MC.MVC.DataEntity.SysManage;
using MC.MVC.FrameWork.log;
using MC.MVC.FrameWork.MyBatis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MC.MVC.WebSite
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ////初始化log4net
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"bin/Config\log4net.config"));
            ////初始化MyBatis映射
            MybatisMapper.Instance.Init(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin/Config\SqlMap.config"));
            var manageUser = new SysUser();
            //var xx = SysManageOperator.QueryUserList(manageUser);
            AppLogger.Application.Info("sb");
          //  string str = SysManageProvider.test();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}