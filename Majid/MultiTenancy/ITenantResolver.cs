namespace Majid.MultiTenancy
{
    public interface ITenantResolver
    {
        int? ResolveTenantId();
    }
}