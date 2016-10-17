using Quest.Core.Data;
using Quest.Core.Models.HouseManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.HouseManage.Impl
{
     [Export(typeof(IHousekeepingService))]
    internal partial class HousekeepingService : RepositoryBase<Housekeeping, Guid>, IHousekeepingService
    {
        #region 公共属性

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion
    }
}
