using System;
using System.Runtime.Serialization;

namespace MySet.Exceptions
{
    public class RemovalException : Exception
    {
        public RemovalException()
        {
        }

        public RemovalException(string message) : base(message)
        {
        }

        public RemovalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RemovalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
