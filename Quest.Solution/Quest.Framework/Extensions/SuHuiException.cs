using System;

namespace SuHui.Framework
{
    public class SuHuiException : Exception
    {
        public SuHuiException(string message) : base(message) { }
        public SuHuiException(string message, Exception inner)
            : base(message, inner) { }
    }
}
