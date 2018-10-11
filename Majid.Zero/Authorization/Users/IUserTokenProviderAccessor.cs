using Microsoft.AspNet.Identity;

namespace Majid.Authorization.Users
{
    public interface IUserTokenProviderAccessor
    {
        IUserTokenProvider<TUser, long> GetUserTokenProviderOrNull<TUser>() 
            where TUser : MajidUser<TUser>;
    }
}