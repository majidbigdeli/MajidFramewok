using Majid.Dependency;
using Microsoft.AspNet.Identity;

namespace Majid.Authorization.Users
{
    public class NullUserTokenProviderAccessor : IUserTokenProviderAccessor, ISingletonDependency
    {
        public IUserTokenProvider<TUser, long> GetUserTokenProviderOrNull<TUser>() where TUser : MajidUser<TUser>
        {
            return null;
        }
    }
}