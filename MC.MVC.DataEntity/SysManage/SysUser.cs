using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.MVC.DataEntity.SysManage
{
    /// <summary>
    /// 用户
    /// </summary>
    public class SysUser
    {
        public long UserId { get; set; }

        public long DeptId { get; set; }

        public string UserCode { get; set; }

        public string LOGINNAME { get; set; }

        public string LoginName { get; set; }
    }
}
