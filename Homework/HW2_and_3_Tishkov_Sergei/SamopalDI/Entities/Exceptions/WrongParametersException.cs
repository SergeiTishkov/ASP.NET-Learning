using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SamopalIndustries.Entities.Exceptions
{
    public class WrongParametersException : Exception
    {
        public WrongParametersException()
        {
        }

        public WrongParametersException(string message) : base(message)
        {
        }

        public WrongParametersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
