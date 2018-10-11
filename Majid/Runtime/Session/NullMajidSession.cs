using Majid.Configuration.Startup;
using Majid.MultiTenancy;
using Majid.Runtime.Remoting;

namespace Majid.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="IMajidSession"/>.
    /// </summary>
    public class NullMajidSession : MajidSessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullMajidSession Instance { get; } = new NullMajidSession();

        /// <inheritdoc/>
        public override long? UserId => null;

        /// <inheritdoc/>
        public override int? TenantId => null;

        public override MultiTenancySides MultiTenancySide => MultiTenancySides.Tenant;

        public override long? ImpersonatorUserId => null;

        public override int? ImpersonatorTenantId => null;

        private NullMajidSession() 
            : base(
                  new MultiTenancyConfig(), 
                  new DataContextAmbientScopeProvider<SessionOverride>(new AsyncLocalAmbientDataContext())
            )
        {

        }
    }
}