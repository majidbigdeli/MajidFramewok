namespace Majid.MultiTenancy
{
    public interface IMajidZeroDbMigrator
    {
        void CreateOrMigrateForHost();

        void CreateOrMigrateForTenant(MajidTenantBase tenant);
    }
}
