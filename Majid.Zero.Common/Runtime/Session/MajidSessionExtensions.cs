using System;
using Majid.Authorization.Users;

namespace Majid.Runtime.Session
{
    public static class MajidSessionExtensions
    {
        public static bool IsUser(this IMajidSession session, MajidUserBase user)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return session.TenantId == user.TenantId && 
                session.UserId.HasValue && 
                session.UserId.Value == user.Id;
        }
    }
}
