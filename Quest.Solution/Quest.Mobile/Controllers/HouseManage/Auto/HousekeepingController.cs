using Quest.Core.HouseManage;
using Quest.Core.Models.HouseManage;
using Quest.Framework;
using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.Mobile.Controllers.HouseManage
{
    [Export]
    public partial class HousekeepingController : BaseController
    {
        #region 属性

        /// <summary>
        /// 获取或设置 用户数据访问对象
        /// </summary>
        [Import]
        public IHousekeepingService HousekeepingService { get; set; }

        #endregion

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加 用户数据
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        [ValidateInput(false)]
        [Feature("添加", "icon_add")]
        public virtual ActionResult Add(Housekeeping entity)
        {
            OperationResult or = HousekeepingService.Insert(entity);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 修改 用户数据
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        [ValidateInput(false)]
        [Feature("修改", "icon_edit")]
        public virtual ActionResult Update(Housekeeping entity)
        {
            entity.LastUpdatedTime = DateTime.Now;
            OperationResult or = HousekeepingService.Update(entity);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 删除 用户数据 
        /// 根据用户唯一标识Id集合,数据格式','隔开“1,2,3,4...”
        /// </summary>
        /// <returns>返回操作结果</returns>
        [Feature("删除", "icon_delete")]
        public virtual ActionResult Delete()
        {
            IList<Guid> ids = QuestRequest.GetGuids("ids");
            OperationResult or = HousekeepingService.Delete(c => ids.Contains(c.Id));
            return this.JsonFormat(or);
        }

        #endregion
    }
}