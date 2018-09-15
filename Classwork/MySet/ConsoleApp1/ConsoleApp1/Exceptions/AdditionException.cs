using System;
using System.Runtime.Serialization;

namespace MySet.Exceptions
{
    public class AdditionException : Exception
    {
        public AdditionException()
        {
        }

        public AdditionException(string message) : base(message)
        {
        }

        public AdditionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdditionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
