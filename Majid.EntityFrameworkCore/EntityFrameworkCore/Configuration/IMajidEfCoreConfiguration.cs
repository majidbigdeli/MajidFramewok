using System;
using Microsoft.EntityFrameworkCore;

namespace Majid.EntityFrameworkCore.Configuration
{
    public interface IMajidEfCoreConfiguration
    {
        void AddDbContext<TDbContext>(Action<MajidDbContextConfiguration<TDbContext>> action)
            where TDbContext : DbContext;
    }
}
