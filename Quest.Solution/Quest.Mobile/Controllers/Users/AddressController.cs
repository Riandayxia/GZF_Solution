using Quest.Core.Models.Users;
using Quest.Core.Users;
using Quest.Framework;
using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.Mobile.Controllers.Users
{
    public partial class AddressController
    {
        #region 属性
      
        #endregion

        #region 视图功能
        public ActionResult GetByUsersId()
        {
            Guid userId = QuestRequest.GetGuid("userId");
            return this.JsonFormat(AddressService.GetByUsersId(userId));
        }
        #endregion
    }
}