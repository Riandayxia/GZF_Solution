using Quest.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.Users
{
    public partial interface IAddressService
    {
        #region 公共属性

        #endregion

        #region 公共方法
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResult GetByUsersId(Guid userId);
        #endregion

        #region 私有方法

        #endregion
    }
}
