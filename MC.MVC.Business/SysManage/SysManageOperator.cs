using MC.MVC.DataAccess.SysManage;
using MC.MVC.DataEntity.SysManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.MVC.Business.SysManage
{
    public class SysManageOperator
    {
        public static IList<SysUser> QueryUserList(SysUser sysUser)
        {
            return SysManageProvider.QueryUserList(sysUser);
        }
    }
}
