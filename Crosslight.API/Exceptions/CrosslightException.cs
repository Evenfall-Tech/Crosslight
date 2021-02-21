using System;

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
