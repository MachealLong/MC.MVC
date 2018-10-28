using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.MVC.FrameWork.log
{
    public class AppLogger
    {
        public static ILog Application
        {
            get { return LogManager.GetLogger("App"); }
        }
    }
}
