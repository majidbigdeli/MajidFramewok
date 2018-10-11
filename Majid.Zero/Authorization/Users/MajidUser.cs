using Majid.Domain.Entities.Auditing;
using Microsoft.AspNet.Identity;

namespace Majid.Authorization.Users
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public abstract class MajidUser<TUser> : MajidUserBase, IUser<long>, IFullAudited<TUser>
        where TUser : MajidUser<TUser>
    {
        public virtual TUser DeleterUser { get; set; }

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }
    }
}