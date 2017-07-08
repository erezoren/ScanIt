using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScanIt.Logic
{
    [Serializable]
    class QueryExecException : Exception
    {
     

        public QueryExecException()
        {
        }

   

        public QueryExecException(string message) : base(message)
        {
        }

        public QueryExecException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QueryExecException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
