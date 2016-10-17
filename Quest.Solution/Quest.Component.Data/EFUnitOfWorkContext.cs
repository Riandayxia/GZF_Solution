using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

using SuHui.Framework;

namespace SuHui.Component.Data
{
    /// <summary>
    ///     数据单元操作类
    /// </summary>
    [Export(typeof(IUnitOfWork))]
    internal class EFUnitOfWorkContext : UnitOfWorkContextBase
    {
        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get
            {
                return EFDbContext.Value;
            }
        }

        [Import("EFDbContext", typeof(DbContext))]
        private Lazy<EFDbContext> EFDbContext { get; set; }
    }
}
