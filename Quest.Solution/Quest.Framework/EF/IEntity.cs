using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quest.Framework
{
    public interface IEntity : ICloneable
    {
        Guid Id { get; }
    }
}
