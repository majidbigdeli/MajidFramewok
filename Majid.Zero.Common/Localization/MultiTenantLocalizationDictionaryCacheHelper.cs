using System.Collections.Generic;
using Majid.Runtime.Caching;

namespace Majid.Localization
{
    /// <summary>
    /// A helper to implement localization cache.
    /// </summary>
    public static class MultiTenantLocalizationDictionaryCacheHelper
    {
        /// <summary>
        /// The cache name.
        /// </summary>
        public const string CacheName = "MajidZeroMultiTenantLocalizationDictionaryCache";

        public static ITypedCache<string, Dictionary<string, string>> GetMultiTenantLocalizationDictionaryCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache(CacheName).AsTyped<string, Dictionary<string, string>>();
        }

        public static string CalculateCacheKey(int? tenantId, string sourceName, string languageName)
        {
            return sourceName + "#" + languageName + "#" + (tenantId ?? 0);
        }
    }
}