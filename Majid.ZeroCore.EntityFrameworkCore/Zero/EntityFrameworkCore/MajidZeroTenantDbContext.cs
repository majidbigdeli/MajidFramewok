using Majid.Authorization.Roles;
using Majid.Authorization.Users;
using Majid.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Majid.Zero.EntityFrameworkCore
{
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class MajidZeroTenantDbContext<TRole, TUser,TSelf> : MajidZeroCommonDbContext<TRole, TUser,TSelf>
        where TRole : MajidRole<TUser>
        where TUser : MajidUser<TUser>
        where TSelf: MajidZeroTenantDbContext<TRole, TUser, TSelf>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected MajidZeroTenantDbContext(DbContextOptions<TSelf> options)
            :base(options)
        {

        }
    }
}