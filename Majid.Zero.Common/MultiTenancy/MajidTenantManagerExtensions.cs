using System.Collections.Generic;
using Majid.Authorization.Users;
using Majid.Threading;

namespace Majid.MultiTenancy
{
    public static class MajidTenantManagerExtensions
    {
        public static void Create<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, TTenant tenant)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.CreateAsync(tenant));
        }

        public static void Update<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, TTenant tenant)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.UpdateAsync(tenant));
        }

        public static TTenant FindById<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, int id)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.FindByIdAsync(id));
        }

        public static TTenant GetById<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, int id)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.GetByIdAsync(id));
        }

        public static TTenant FindByTenancyName<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, string tenancyName)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.FindByTenancyNameAsync(tenancyName));
        }

        public static void Delete<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, TTenant tenant)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.DeleteAsync(tenant));
        }

        public static string GetFeatureValueOrNull<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, int tenantId, string featureName)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.GetFeatureValueOrNullAsync(tenantId, featureName));
        }

        public static IReadOnlyList<NameValue> GetFeatureValues<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, int tenantId)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.GetFeatureValuesAsync(tenantId));
        }

        public static void SetFeatureValues<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, int tenantId, params NameValue[] values)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.SetFeatureValuesAsync(tenantId, values));
        }

        public static void SetFeatureValue<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, int tenantId, string featureName, string value)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.SetFeatureValueAsync(tenantId, featureName, value));
        }

        public static void SetFeatureValue<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, TTenant tenant, string featureName, string value)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.SetFeatureValueAsync(tenant, featureName, value));
        }

        public static void ResetAllFeatures<TTenant, TUser>(this MajidTenantManager<TTenant, TUser> tenantManager, int tenantId)
            where TTenant : MajidTenant<TUser>
            where TUser : MajidUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.ResetAllFeaturesAsync(tenantId));
        }

    }
}