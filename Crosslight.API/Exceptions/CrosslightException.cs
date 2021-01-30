using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Exceptions
{
    public class CrosslightException : Exception
    {
        private const string ExceptionMessage = "Crosslight Exception.";

        public CrosslightException()
        {
        }

        public CrosslightException(string message)
            : base(message)
        {
        }

        public CrosslightException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public CrosslightException(Exception inner)
            : base(ExceptionMessage, inner)
        {
        }
    }
}
