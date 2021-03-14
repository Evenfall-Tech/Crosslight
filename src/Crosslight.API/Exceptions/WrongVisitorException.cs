using System;

namespace Crosslight.API.Exceptions
{
    public class WrongVisitorException : VisitorException
    {
        public WrongVisitorException()
        {
        }

        public WrongVisitorException(string message)
            : base(message)
        {
        }

        public WrongVisitorException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public WrongVisitorException(Type visitor, Type visitee)
            : this($"Wrong visitor {visitor.Name} for node {visitee.Name}.")
        {

        }

        public WrongVisitorException(Type visitor, Type visitee, Exception inner)
            : this($"Wrong visitor {visitor.Name} for node {visitee.Name}.", inner)
        {

        }

        public WrongVisitorException(string visitor, string visitee)
            : this($"Wrong visitor {visitor} for node {visitee}.")
        {

        }

        public WrongVisitorException(string visitor, string visitee, Exception inner)
            : this($"Wrong visitor {visitor} for node {visitee}.", inner)
        {

        }
    }
}
