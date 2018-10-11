using System.Threading.Tasks;

namespace Majid.Application.Features
{
    public interface IMajidZeroFeatureValueStore : IFeatureValueStore
    {
        Task<string> GetValueOrNullAsync(int tenantId, string featureName);
        Task<string> GetEditionValueOrNullAsync(int editionId, string featureName);
        Task SetEditionFeatureValueAsync(int editionId, string featureName, string value);
    }
}