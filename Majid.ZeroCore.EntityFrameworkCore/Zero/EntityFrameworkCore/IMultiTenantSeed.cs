using Majid.MultiTenancy;

namespace Majid.Zero.EntityFrameworkCore
{
    public interface IMultiTenantSeed
    {
        MajidTenantBase Tenant { get; set; }
    }
}