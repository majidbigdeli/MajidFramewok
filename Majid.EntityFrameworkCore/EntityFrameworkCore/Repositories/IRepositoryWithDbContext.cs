using Microsoft.EntityFrameworkCore;

namespace Majid.EntityFrameworkCore.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}