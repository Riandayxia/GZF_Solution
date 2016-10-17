using Quest.Core.Data;
using Quest.Core.Models.Base;
using Quest.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.Users.Impl
{
     [Export(typeof(IAddressService))]
    internal partial class AddressService : RepositoryBase<Address, Guid>, IAddressService
    {
        #region 公共属性

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion
    }
}
