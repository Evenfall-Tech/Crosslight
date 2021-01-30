using System;

namespace Crosslight.API.Exceptions
{
    public class VisitorException : CrosslightException
    {
        private const string ExceptionMessage = "Failed to apply visitor.";

        public VisitorException()
        {
        }

        public VisitorException(string message)
            : base(message)
        {
        }

        public VisitorException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public VisitorException(Exception inner)
            : base(ExceptionMessage, inner)
        {
        }
    }
}
