using System;
using Majid.Authorization.Roles;
using Majid.Threading;

namespace Majid.Authorization.Users
{
    /// <summary>
    /// Extension methods for <see cref="MajidUserManager{TRole,TUser}"/>.
    /// </summary>
    public static class MajidUserManagerExtensions
    {
        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="manager">User manager</param>
        /// <param name="userId">User id</param>
        /// <param name="permissionName">Permission name</param>
        public static bool IsGranted<TRole, TUser>(MajidUserManager<TRole, TUser> manager, long userId, string permissionName)
            where TRole : MajidRole<TUser>, new()
            where TUser : MajidUser<TUser>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            return AsyncHelper.RunSync(() => manager.IsGrantedAsync(userId, permissionName));
        }
    }
}