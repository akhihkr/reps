using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Common
{
    public class CWCFException : System.Exception

    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public CWCFException() : base()
        {

        }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        public CWCFException(String message) : base(message)
        {

        }

        /// <summary>
        /// Argument constructor with inner exception
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        /// <param name="innerException">Inner exception</param>
        public CWCFException(String message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Argument constructor with serialization support
        /// </summary>
        /// <param name="info">Instance of SerializationInfo</param>
        /// <param name="context">Instance of StreamingContext</param>
        protected CWCFException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }




    }
}
