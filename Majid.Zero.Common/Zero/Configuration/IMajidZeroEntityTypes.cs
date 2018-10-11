using System;

namespace Majid.Zero.Configuration
{
    public interface IMajidZeroEntityTypes
    {
        /// <summary>
        /// User type of the application.
        /// </summary>
        Type User { get; set; }

        /// <summary>
        /// Role type of the application.
        /// </summary>
        Type Role { get; set; }

        /// <summary>
        /// Tenant type of the application.
        /// </summary>
        Type Tenant { get; set; }
    }
}