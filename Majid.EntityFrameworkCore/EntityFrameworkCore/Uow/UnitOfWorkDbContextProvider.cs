using Majid.Domain.Uow;
using Majid.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Majid.EntityFrameworkCore.Uow
{
    /// <summary>
    /// Implements <see cref="IDbContextProvider{TDbContext}"/> that gets DbContext from
    /// active unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext> 
        where TDbContext : DbContext
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkDbContextProvider{TDbContext}"/>.
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public TDbContext GetDbContext()
        {
            return GetDbContext(null);
        }

        public TDbContext GetDbContext(MultiTenancySides? multiTenancySide)
        {
            return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>(multiTenancySide);
        }
    }
}