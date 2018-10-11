﻿using System.Threading.Tasks;

namespace Majid.Notifications
{
    /// <summary>
    /// Null pattern implementation of <see cref="IRealTimeNotifier"/>.
    /// </summary>
    public class NullRealTimeNotifier : IRealTimeNotifier
    {
        /// <summary>
        /// Gets single instance of <see cref="NullRealTimeNotifier"/> class.
        /// </summary>
        public static NullRealTimeNotifier Instance { get; } = new NullRealTimeNotifier();

        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            return Task.FromResult(0);
        }
    }
}
