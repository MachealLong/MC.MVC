using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.MVC.FrameWork.MyBatis
{
    /// <summary>
    /// 
    /// </summary>
    public class MybatisMapper
    {
        private static MybatisMapper instance = new MybatisMapper();
        private ISqlMapper _mapper;
        //var instance = new MybatisFactory();
        private MybatisMapper() { 
        
        }

        public  void  Init(string xmlPath){
            var domSqlMapBuilder = new DomSqlMapBuilder();
            this._mapper = domSqlMapBuilder.Configure(new Uri(xmlPath));
        }

        public static MybatisMapper Instance { get { return instance; } }

        public ISqlMapper GetMapper(){
        return this._mapper;
        }
    }
}
