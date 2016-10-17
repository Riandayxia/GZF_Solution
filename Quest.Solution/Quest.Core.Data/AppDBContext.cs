using Quest.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace Quest.Core.Data
{

    /// <summary>
    ///     EF数据访问上下文
    /// </summary>
    [Export("AppDBContext", typeof(DbContext))]
    public class AppDBContext : DbContext
    {
        public AppDBContext()
            : base("MsSql") { }

        public AppDBContext(String nameOrConnectionString)
            : base(nameOrConnectionString) { }

        public AppDBContext(DbConnection existingConnection)
            : base(existingConnection, true) { }

        [ImportMany(typeof(IEntityMapper))]
        public IEnumerable<IEntityMapper> EntityMappers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            if (EntityMappers != null)
            {
                foreach (var mapper in EntityMappers)
                {
                    mapper.RegistTo(modelBuilder.Configurations);
                }
            }
        }
    }
}
