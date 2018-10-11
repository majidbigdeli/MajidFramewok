using System;
using System.Data;
using System.Reflection;
using Majid.Data;
using Majid.Dependency;
using Majid.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Majid.EntityFrameworkCore
{
    public class EfCoreActiveTransactionProvider : IActiveTransactionProvider, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;

        public EfCoreActiveTransactionProvider(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args)
        {
            return GetDbContext(args).Database.CurrentTransaction?.GetDbTransaction();
        }

        public IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args)
        {
            return GetDbContext(args).Database.GetDbConnection();
        }

        private DbContext GetDbContext(ActiveTransactionProviderArgs args)
        {
            Type dbContextProviderType = typeof(IDbContextProvider<>).MakeGenericType((Type)args["ContextType"]);

            using (IDisposableDependencyObjectWrapper dbContextProviderWrapper = _iocResolver.ResolveAsDisposable(dbContextProviderType))
            {
                MethodInfo method = dbContextProviderWrapper.Object.GetType()
                                                            .GetMethod(
                                                                nameof(IDbContextProvider<MajidDbContext>.GetDbContext),
                                                                new[] { typeof(MultiTenancySides) }
                                                            );

                return (DbContext)method.Invoke(
                    dbContextProviderWrapper.Object,
                    new object[] { (MultiTenancySides?)args["MultiTenancySide"] }
                );
            }
        }
    }
}