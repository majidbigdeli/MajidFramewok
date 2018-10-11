using System.Threading.Tasks;
using Majid.Authorization.Roles;
using Majid.Authorization.Users;
using Majid.Domain.Uow;
using Majid.MultiTenancy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Majid.Authorization
{
    public class MajidSecurityStampValidator<TTenant, TRole, TUser> : SecurityStampValidator<TUser>
        where TTenant : MajidTenant<TUser>
        where TRole : MajidRole<TUser>, new()
        where TUser : MajidUser<TUser>
    {
        public MajidSecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            MajidSignInManager<TTenant, TRole, TUser> signInManager,
            ISystemClock systemClock)
            : base(
                options, 
                signInManager,
                systemClock)
        {
        }

        [UnitOfWork]
        public override Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            return base.ValidateAsync(context);
        }
    }
}
