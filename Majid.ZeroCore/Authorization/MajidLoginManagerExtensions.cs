using Majid.Authorization.Roles;
using Majid.Authorization.Users;
using Majid.MultiTenancy;
using Majid.Threading;

namespace Majid.Authorization
{
    public static class MajidLogInManagerExtensions
    {
        public static MajidLoginResult<TTenant, TUser> Login<TTenant, TRole, TUser>(
            this MajidLogInManager<TTenant, TRole, TUser> logInManager, 
            string userNameOrEmailAddress, 
            string plainPassword, 
            string tenancyName = null, 
            bool shouldLockout = true)
                where TTenant : MajidTenant<TUser>
                where TRole : MajidRole<TUser>, new()
                where TUser : MajidUser<TUser>
        {
            return AsyncHelper.RunSync(
                () => logInManager.LoginAsync(
                    userNameOrEmailAddress,
                    plainPassword,
                    tenancyName,
                    shouldLockout
                )
            );
        }
    }
}
