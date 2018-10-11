using System;

namespace Majid.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by MAJID infrastructure.
    /// </summary>
    public class MajidHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
        public MajidHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}