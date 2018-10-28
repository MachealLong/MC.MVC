using MC.MVC.DataEntity.SysManage;
using MC.MVC.FrameWork.MyBatis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.MVC.DataAccess.SysManage
{
    public class SysManageProvider
    {
        public static IList<SysUser> QueryUserList(SysUser sysUser)
        {
            return MybatisMapper.Instance.GetMapper().QueryForList<SysUser>("Fee.Test", sysUser);
        }

        public static string test()
        {
            return MybatisMapper.Instance.GetMapper().QueryForObject<string>("Fee.FindPageId", 1);
        }
    }
}
