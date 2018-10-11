using Majid.Application.Editions;
using Majid.Application.Features;
using Majid.Auditing;
using Majid.Authorization.Roles;
using Majid.Authorization.Users;
using Majid.BackgroundJobs;
using Majid.MultiTenancy;
using Majid.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Majid.Zero.EntityFrameworkCore
{
    /// <summary>
    /// Base DbContext for MAJID zero.
    /// Derive your DbContext from this class to have base entities.
    /// </summary>
    public abstract class MajidZeroDbContext<TTenant, TRole, TUser, TSelf> : MajidZeroCommonDbContext<TRole, TUser, TSelf>
        where TTenant : MajidTenant<TUser>
        where TRole : MajidRole<TUser>
        where TUser : MajidUser<TUser>
        where TSelf : MajidZeroDbContext<TTenant, TRole, TUser, TSelf>
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
        /// Notifications.
        /// </summary>
        public virtual DbSet<NotificationInfo> Notifications { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected MajidZeroDbContext(DbContextOptions<TSelf> options)
            : base(options)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BackgroundJobInfo>(b =>
            {
                b.HasIndex(e => new { e.IsAbandoned, e.NextTryTime });
            });

            modelBuilder.Entity<TenantFeatureSetting>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.Name });
            });

            modelBuilder.Entity<EditionFeatureSetting>(b =>
            {
                b.HasIndex(e => new { e.EditionId, e.Name });
            });

            modelBuilder.Entity<TTenant>(b =>
            {
                b.HasOne(p => p.DeleterUser)
                    .WithMany()
                    .HasForeignKey(p => p.DeleterUserId);

                b.HasOne(p => p.CreatorUser)
                    .WithMany()
                    .HasForeignKey(p => p.CreatorUserId);

                b.HasOne(p => p.LastModifierUser)
                    .WithMany()
                    .HasForeignKey(p => p.LastModifierUserId);

                b.HasIndex(e => e.TenancyName);
            });

            modelBuilder.Entity<UserAccount>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.UserName });
                b.HasIndex(e => new { e.TenantId, e.EmailAddress });
                b.HasIndex(e => new { e.UserName });
                b.HasIndex(e => new { e.EmailAddress });
            });

            #region AuditLog.Set_MaxLengths

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.ServiceName)
                .HasMaxLength(AuditLog.MaxServiceNameLength);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.MethodName)
                .HasMaxLength(AuditLog.MaxMethodNameLength);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.Parameters)
                .HasMaxLength(AuditLog.MaxParametersLength);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.ClientIpAddress)
                .HasMaxLength(AuditLog.MaxClientIpAddressLength);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.ClientName)
                .HasMaxLength(AuditLog.MaxClientNameLength);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.BrowserInfo)
                .HasMaxLength(AuditLog.MaxBrowserInfoLength);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.Exception)
                .HasMaxLength(AuditLog.MaxExceptionLength);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.CustomData)
                .HasMaxLength(AuditLog.MaxCustomDataLength);

            #endregion

        }
    }
}
