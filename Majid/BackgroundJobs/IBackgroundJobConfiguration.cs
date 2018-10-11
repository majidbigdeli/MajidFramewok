﻿using Majid.Configuration.Startup;

namespace Majid.BackgroundJobs
{
    /// <summary>
    /// Used to configure background job system.
    /// </summary>
    public interface IBackgroundJobConfiguration
    {
        /// <summary>
        /// Used to enable/disable background job execution.
        /// </summary>
        bool IsJobExecutionEnabled { get; set; }

        /// <summary>
        /// Gets the MAJID configuration object.
        /// </summary>
        IMajidStartupConfiguration MajidConfiguration { get; }
    }
}