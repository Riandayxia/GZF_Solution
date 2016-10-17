using Quest.Core.Models.BPM;
using Quest.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core
{
    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    public static class CoreInitializer
    {
        public static void Initialize(List<CDTable> tables, List<CDColumn> columns)
        {
            SourceOperating.Initialize(tables, columns);
        }
    }
}
