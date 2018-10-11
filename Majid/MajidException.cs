using System;
using System.Runtime.Serialization;

namespace Majid
{
    /// <summary>
    /// Base exception type for those are thrown by Majid system for Majid specific exceptions.
    /// </summary>
    [Serializable]
    public class MajidException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="MajidException"/> object.
        /// </summary>
        public MajidException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="MajidException"/> object.
        /// </summary>
        public MajidException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="MajidException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public MajidException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="MajidException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public MajidException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
