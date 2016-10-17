using Quest.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.Impl
{

    [Export(typeof(IDbContextProvider))]
    public class MsSqlProvider : IDbContextProvider
    {
        AppDBContext m_AppDbContext = null;

        [Import("AppDBContext", typeof(DbContext))]
        private Lazy<AppDBContext> DBContext { get; set; }

        public DbContext Context
        {
            get
            {
                return DBContext.Value;
            }
        }
    }
}
