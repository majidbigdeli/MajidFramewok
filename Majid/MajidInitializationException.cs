using System;
using System.Runtime.Serialization;

namespace Majid
{
    /// <summary>
    /// This exception is thrown if a problem on MAJID initialization progress.
    /// </summary>
    [Serializable]
    public class MajidInitializationException : MajidException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MajidInitializationException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public MajidInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public MajidInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public MajidInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
