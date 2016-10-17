using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Data.Entity;
namespace Quest.Core.Data
{
    public interface IDbContextProvider
    {
        DbContext Context { get; }
    }
}
