using System;
using System.Runtime.Serialization;

namespace Majid.Domain.Uow
{
    [Serializable]
    public class MajidDbConcurrencyException : MajidException
    {
        /// <summary>
        /// Creates a new <see cref="MajidDbConcurrencyException"/> object.
        /// </summary>
        public MajidDbConcurrencyException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="MajidException"/> object.
        /// </summary>
        public MajidDbConcurrencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="MajidDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public MajidDbConcurrencyException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="MajidDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public MajidDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}