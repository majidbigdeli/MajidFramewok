using System.Security.Claims;
using Majid.MultiTenancy;

namespace Majid.Authorization.Users
{
    public class MajidLoginResult<TTenant, TUser>
        where TTenant : MajidTenant<TUser>
        where TUser : MajidUserBase
    {
        public MajidLoginResultType Result { get; private set; }

        public TTenant Tenant { get; private set; }

        public TUser User { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public MajidLoginResult(MajidLoginResultType result, TTenant tenant = null, TUser user = null)
        {
            Result = result;
            Tenant = tenant;
            User = user;
        }

        public MajidLoginResult(TTenant tenant, TUser user, ClaimsIdentity identity)
            : this(MajidLoginResultType.Success, tenant)
        {
            User = user;
            Identity = identity;
        }
    }
}