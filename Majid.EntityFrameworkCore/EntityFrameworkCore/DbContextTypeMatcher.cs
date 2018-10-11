using Majid.Domain.Uow;
using Majid.EntityFramework;

namespace Majid.EntityFrameworkCore
{
    public class DbContextTypeMatcher : DbContextTypeMatcher<MajidDbContext>
    {
        public DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
            : base(currentUnitOfWorkProvider)
        {
        }
    }
}