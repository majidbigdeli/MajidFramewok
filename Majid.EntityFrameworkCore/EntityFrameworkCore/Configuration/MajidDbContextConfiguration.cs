using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Majid.EntityFrameworkCore.Configuration
{
    public class MajidDbContextConfiguration<TDbContext>
        where TDbContext : DbContext
    {
        public string ConnectionString {get; internal set; }

        public DbConnection ExistingConnection { get; internal set; }

        public DbContextOptionsBuilder<TDbContext> DbContextOptions { get; }
        
        public MajidDbContextConfiguration(string connectionString, DbConnection existingConnection)
        {
            ConnectionString = connectionString;
            ExistingConnection = existingConnection;

            DbContextOptions = new DbContextOptionsBuilder<TDbContext>();
        }
    }
}