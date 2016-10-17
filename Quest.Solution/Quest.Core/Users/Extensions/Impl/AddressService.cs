using Quest.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.Users.Impl
{
    internal partial class AddressService
    {
        #region 公共属性

        #endregion

        #region 公共方法
        public OperationResult GetByUsersId(Guid userId)
        {
            var items = this.Entities.Where(c => c.UsersId == userId).ToList();
            return new OperationResult(OperationResultType.Success, "查询成功", items);
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
