using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using DynamicCodeGenerate.Core;
using DynamicCodeGenerate.Model;
using SuHui.Framework;
using SuHui.Core.Models.Base;
using SuHui.Core.BPM;
namespace SuHui.WebSite.Controllers
{
    [Export]
    public class Test3Controller : BaseController
    {
        [Import]
        public ITest3Service Test3Service { get; set; }
        [Import]
        public IWFRunInstanceService WFRunInstanceService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult Add(Test3 entity)
        {
            OperationResult or = new OperationResult(OperationResultType.Error);
            Guid flowId = SuHuiRequest.GetGuid("flowId");
            if (flowId.IsNullOrEmpty())
            {
                or = Test3Service.Insert(entity);
            }
            else
            {
                User user = new User();
                user.Id = "00000000-0000-0000-0001-000000000001".GetGuid();
                or = Test3Service.Insert(entity, false);
                or = WFRunInstanceService.Execute(entity.Id.ToString(), flowId, user);
            }
            return this.JsonFormat(or);
        }
        public virtual ActionResult Update(Test3 entity)
        {
            OperationResult or = Test3Service.Update(entity);
            return this.JsonFormat(or);
        }
        public virtual ActionResult Delete()
        {
            IList<Guid> ids = SuHuiRequest.GetGuids("ids");
            OperationResult or = Test3Service.Delete(c => ids.Contains(c.Id));
            return this.JsonFormat(or);
        }
        public ActionResult GetAll()
        {
            IQueryable<Test3> items = Test3Service.Entities;
            OperationResult or = new OperationResult(OperationResultType.Success, String.Empty, items);
            return this.JsonFormat(or);
        }
    }
    [Export]
    public class Test2Controller : BaseController
    {
        [Import]
        public ITest2Service Test2Service { get; set; }
        [Import]
        public IWFRunInstanceService WFRunInstanceService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult Add(Test2 entity)
        {
            OperationResult or = new OperationResult(OperationResultType.Error);
            Guid flowId = SuHuiRequest.GetGuid("flowId");
            if (flowId.IsNullOrEmpty())
            {
                or = Test2Service.Insert(entity);
            }
            else
            {
                User user = new User();
                user.Id = "00000000-0000-0000-0001-000000000001".GetGuid();
                or = Test2Service.Insert(entity, false);
                or = WFRunInstanceService.Execute(entity.Id.ToString(), flowId, user);
            }
            return this.JsonFormat(or);
        }
        public virtual ActionResult Update(Test2 entity)
        {
            OperationResult or = Test2Service.Update(entity);
            return this.JsonFormat(or);
        }
        public virtual ActionResult Delete()
        {
            IList<Guid> ids = SuHuiRequest.GetGuids("ids");
            OperationResult or = Test2Service.Delete(c => ids.Contains(c.Id));
            return this.JsonFormat(or);
        }
        public ActionResult GetAll()
        {
            IQueryable<Test2> items = Test2Service.Entities;
            OperationResult or = new OperationResult(OperationResultType.Success, String.Empty, items);
            return this.JsonFormat(or);
        }
    }
    [Export]
    public class TestController : BaseController
    {
        [Import]
        public ITestService TestService { get; set; }
        [Import]
        public IWFRunInstanceService WFRunInstanceService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult Add(Test entity)
        {
            OperationResult or = new OperationResult(OperationResultType.Error);
            Guid flowId = SuHuiRequest.GetGuid("flowId");
            if (flowId.IsNullOrEmpty())
            {
                or = TestService.Insert(entity);
            }
            else
            {
                User user = new User();
                user.Id = "00000000-0000-0000-0001-000000000001".GetGuid();
                or = TestService.Insert(entity, false);
                or = WFRunInstanceService.Execute(entity.Id.ToString(), flowId, user);
            }
            return this.JsonFormat(or);
        }
        public virtual ActionResult Update(Test entity)
        {
            OperationResult or = TestService.Update(entity);
            return this.JsonFormat(or);
        }
        public virtual ActionResult Delete()
        {
            IList<Guid> ids = SuHuiRequest.GetGuids("ids");
            OperationResult or = TestService.Delete(c => ids.Contains(c.Id));
            return this.JsonFormat(or);
        }
        public ActionResult GetAll()
        {
            IQueryable<Test> items = TestService.Entities;
            OperationResult or = new OperationResult(OperationResultType.Success, String.Empty, items);
            return this.JsonFormat(or);
        }
    }
}
