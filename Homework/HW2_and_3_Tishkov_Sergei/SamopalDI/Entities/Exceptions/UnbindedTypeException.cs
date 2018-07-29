using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SamopalIndustries.Entities.Exceptions
{
    public class UnbindedTypeException : Exception
    {
        public UnbindedTypeException()
        {
        }

        public UnbindedTypeException(string message) : base(message)
        {
        }

        public UnbindedTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnbindedTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
