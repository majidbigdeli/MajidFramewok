namespace Majid.MultiTenancy
{
    public class NullTenantResolverCache : ITenantResolverCache
    {
        public TenantResolverCacheItem Value
        {
            get { return null; }
            set {  }
        }
    }
}