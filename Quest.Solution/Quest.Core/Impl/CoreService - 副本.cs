using SuHui.Core.Data;
using SuHui.Core.Models.Test;
using SuHui.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuHui.Core.Impl
{
    [Export(typeof(ICoreService))]
    internal partial class CoreService : ICoreService
    {
        //    [Import("EFRepositoryBase", typeof(IRepository<DBTable, Guid>))]
        //    public IRepository<DBTable, Guid> EFRepositoryBase { get; set; }

        [Import]
        static IEnumerable<IDbContextProvider> m_Providers { get; set; }
        [Import]
        static IEnumerable<IEntity> Entitys { get; set; }

        [Import]
        private IDbContextProvider EFProvider { get; set; }

        private DbContext Context
        {
            get
            {
                return EFProvider.Context;
            }
        }

        public CompositionContainer Container
        {
            get
            {
                if (_catalog.IsNullOrEmpty())
                {
                    //使用目录方式查找MEF部件
                    _catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
                }

                //创建Container
                CompositionContainer container = new CompositionContainer(_catalog);
                return container;
            }
        }

        private DirectoryCatalog _catalog;

        public OperationResult Test(String strType, String strMethod, Object[] objs)
        {
            OperationResult or = new OperationResult(OperationResultType.Error);


            //获取Export项,这里直接加载不采用Lazy
            m_Providers = Container.GetExportedValues<IDbContextProvider>();
            Entitys = Container.GetExportedValues<IEntity>();

            var obj = Entitys.Where(c => c.GetType().Name == strType).FirstOrDefault();
            if (obj != null)
            {
                Type type = obj.GetType();
                //取得属性集合
                PropertyInfo[] pi = type.GetProperties();

                DbSet db = Context.Set(obj.GetType());
                switch (strMethod)
                {
                    case "Add":
                        db.Add(objs[0]);
                        Context.SaveChanges();
                        break;
                    case "Update":
                        break;
                    case "Del":
                        break;
                    case "GetAll":
                        var items = db;

                        or = new OperationResult(OperationResultType.Success, "", items);
                        break;
                }
            }

            //if (m_Providers != null)
            //{
            //    foreach (var provider in m_Providers)
            //    {
            //        DbContext dbContext = provider.Context;
            //        var dbUser = dbContext.Set(typeof(User));
            //        if (dbUser != null)
            //        {
            //            User user = new User()
            //            {
            //                Name = "admin",
            //                Password = "123"
            //            };
            //            dbUser.Add(user);
            //            dbContext.SaveChanges();
            //        }
            //    }
            //}

            //IDbContextProvider provider = new MsSqlProvider();
            //AppDBContext dbContext = provider.Get();


            return or;
        }
    }
}
