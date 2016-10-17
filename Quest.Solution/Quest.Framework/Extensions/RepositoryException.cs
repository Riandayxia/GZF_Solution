using System;

namespace SuHui.Framework
{
    [Serializable]
    public class RepositoryException : SuHuiException
    {
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(string message, Exception inner)
            : base(message, inner) { }
    }
}
