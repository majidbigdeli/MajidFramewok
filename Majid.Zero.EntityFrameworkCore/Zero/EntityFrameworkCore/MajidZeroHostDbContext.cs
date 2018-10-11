using Majid.Application.Editions;
using Majid.Application.Features;
using Majid.Authorization.Roles;
using Majid.Authorization.Users;
using Majid.BackgroundJobs;
using Majid.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Majid.Zero.EntityFrameworkCore
{
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class MajidZeroHostDbContext<TTenant, TRole, TUser, TSelf> : MajidZeroCommonDbContext<TRole, TUser, TSelf>
        where TTenant : MajidTenant<TUser>
        where TRole : MajidRole<TUser>
        where TUser : MajidUser<TUser>
        where TSelf : MajidZeroHostDbContext<TTenant, TRole, TUser, TSelf>
    {
        /// <summary>
        /// Tenants
        /// </summary>
        public virtual DbSet<TTenant> Tenants { get; set; }

        /// <summary>
        /// Editions.
        /// </summary>
        public virtual DbSet<Edition> Editions { get; set; }

        /// <summary>
        /// FeatureSettings.
        /// </summary>
        public virtual DbSet<FeatureSetting> FeatureSettings { get; set; }

        /// <summary>
        /// TenantFeatureSetting.
        /// </summary>
        public virtual DbSet<TenantFeatureSetting> TenantFeatureSettings { get; set; }

        /// <summary>
        /// EditionFeatureSettings.
        /// </summary>
        public virtual DbSet<EditionFeatureSetting> EditionFeatureSettings { get; set; }

        /// <summary>
        /// Background jobs.
        /// </summary>
        public virtual DbSet<BackgroundJobInfo> BackgroundJobs { get; set; }

        /// <summary>
        /// User accounts
        /// </summary>
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected MajidZeroHostDbContext(DbContextOptions<TSelf> options)
            :base(options)
        {

        }
    }
}