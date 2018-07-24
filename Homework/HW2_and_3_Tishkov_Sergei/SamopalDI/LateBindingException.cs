using System;
using System.Runtime.Serialization;

namespace SamopalIndustries
{
    /// <summary>
    /// The exception that is thrown when object returned by the delegate isn't convertible to binded type.
    /// </summary>
    public class LateBindingException : Exception
    {
        public LateBindingException()
        {
        }

        public LateBindingException(string message) : base(message)
        {
        }

        public LateBindingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LateBindingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
