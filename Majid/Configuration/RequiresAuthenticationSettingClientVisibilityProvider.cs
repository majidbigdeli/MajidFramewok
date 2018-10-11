using System.Threading.Tasks;
using Majid.Dependency;
using Majid.Runtime.Session;

namespace Majid.Configuration
{
    public class RequiresAuthenticationSettingClientVisibilityProvider : ISettingClientVisibilityProvider
    {
        public async Task<bool> CheckVisible(IScopedIocResolver scope)
        {
            return await Task.FromResult(
                scope.Resolve<IMajidSession>().UserId.HasValue
            );
        }
    }
}