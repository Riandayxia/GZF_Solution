using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SuHui.Core.Data.Common.Impl
{
    public class Global
    {
        ///// <summary>
        ///// 获取指定父级Id的数据集合
        ///// </summary>
        ///// <param name="parentId">父级编号</param>
        ///// <param name="tableName">项目名称</param>
        ///// <returns>返回操作结果</returns>
        //public IEnumerable<T> GetsByParentId<T>(Guid parentId, String tableName)
        //{
        //    //SqlParameter[] parms = new SqlParameter[]{
        //    //        new SqlParameter("@pId",parentId),
        //    //        new SqlParameter("@tableName",tableName)
        //    //    }; 
        //    //IEnumerable<T> items = SqlQuery("[dbo].[sp_GetsByParentId]  @pId ,@tableName", parms);
        //    //return items;
        //}
    }
}
