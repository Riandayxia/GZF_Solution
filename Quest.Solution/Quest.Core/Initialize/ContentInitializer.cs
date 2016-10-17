using Quest.Core.Data;
using Quest.Core.Impl;
using Quest.Framework;
using Quest.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Quest.Core.Initialize
{
    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    public class ContentInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {

            bool isInitialize = ConfigurationManager.AppSettings["isInitialize"].CastTo(false);
            if (isInitialize)
            {
                // 初始化数据连接
                Quest.Core.Data.Initialize.DatabaseInitializer.Initialize();
                BasicDataInitializer dbi = new BasicDataInitializer();
                dbi.BasicData();
            }
        }
    }
}
