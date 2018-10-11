using System.Threading.Tasks;
using Majid.Dependency;

namespace Majid.Configuration
{
    public interface ISettingClientVisibilityProvider
    {
        Task<bool> CheckVisible(IScopedIocResolver scope);
    }
}