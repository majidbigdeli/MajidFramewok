using Microsoft.EntityFrameworkCore;

namespace Majid.EntityFrameworkCore.Configuration
{
    public interface IMajidDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        void Configure(MajidDbContextConfiguration<TDbContext> configuration);
    }
}