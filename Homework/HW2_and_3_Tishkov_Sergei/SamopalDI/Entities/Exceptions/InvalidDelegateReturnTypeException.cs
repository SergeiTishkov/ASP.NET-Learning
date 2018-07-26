using System;
using System.Runtime.Serialization;

namespace SamopalIndustries.Entities.Exceptions
{
    /// <summary>
    /// The exception that is thrown when LateBindingOption is set to DefaultCtor, but binded class doesn't have default constructor.
    /// </summary>
    public class InvalidDelegateReturnTypeException : Exception
    {
        public InvalidDelegateReturnTypeException()
        {
        }

        public InvalidDelegateReturnTypeException(string message) : base(message)
        {
        }

        public InvalidDelegateReturnTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDelegateReturnTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
