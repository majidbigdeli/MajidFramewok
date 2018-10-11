using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Majid.Authorization.Roles;
using Majid.Authorization.Users;
using Majid.Dependency;
using Majid.Domain.Uow;
using Majid.Runtime.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Majid.Authorization
{
    public class MajidUserClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>, ITransientDependency
        where TRole : MajidRole<TUser>, new()
        where TUser : MajidUser<TUser>
    {
        public MajidUserClaimsPrincipalFactory(
            MajidUserManager<TRole, TUser> userManager,
            MajidRoleManager<TRole, TUser> roleManager,
            IOptions<IdentityOptions> optionsAccessor
            ) : base(userManager, roleManager, optionsAccessor)
        {

        }

        [UnitOfWork]
        public override async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var principal = await base.CreateAsync(user);

            if (user.TenantId.HasValue)
            {
                principal.Identities.First().AddClaim(new Claim(MajidClaimTypes.TenantId,user.TenantId.ToString()));
            }

            return principal;
        }
    }
}