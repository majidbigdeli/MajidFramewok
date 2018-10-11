﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Majid.Collections.Extensions;
using Majid.Dependency;
using Majid.Domain.Entities;
using Majid.Domain.Uow;
using Majid.Reflection;
using Majid.Runtime.Session;
using Majid.Threading;

namespace Majid.Domain.Repositories
{
    public static class RepositoryExtensions
    {
        public static async Task EnsureCollectionLoadedAsync<TEntity, TPrimaryKey, TProperty>(
            this IRepository<TEntity, TPrimaryKey> repository,
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression,
            CancellationToken cancellationToken = default(CancellationToken)
        )
            where TEntity : class, IEntity<TPrimaryKey>
            where TProperty : class
        {
            var repo = ProxyHelper.UnProxy(repository) as ISupportsExplicitLoading<TEntity, TPrimaryKey>;
            if (repo != null)
            {
                await repo.EnsureCollectionLoadedAsync(entity, collectionExpression, cancellationToken);
            }
        }

        public static void EnsureCollectionLoaded<TEntity, TPrimaryKey, TProperty>(
            this IRepository<TEntity, TPrimaryKey> repository,
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression
        )
            where TEntity : class, IEntity<TPrimaryKey>
            where TProperty : class
        {
            AsyncHelper.RunSync(() => repository.EnsureCollectionLoadedAsync(entity, collectionExpression));
        }

        public static async Task EnsurePropertyLoadedAsync<TEntity, TPrimaryKey, TProperty>(
            this IRepository<TEntity, TPrimaryKey> repository,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken = default(CancellationToken)
        )
            where TEntity : class, IEntity<TPrimaryKey>
            where TProperty : class
        {
            var repo = ProxyHelper.UnProxy(repository) as ISupportsExplicitLoading<TEntity, TPrimaryKey>;
            if (repo != null)
            {
                await repo.EnsurePropertyLoadedAsync(entity, propertyExpression, cancellationToken);
            }
        }

        public static void EnsurePropertyLoaded<TEntity, TPrimaryKey, TProperty>(
            this IRepository<TEntity, TPrimaryKey> repository,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression
        )
            where TEntity : class, IEntity<TPrimaryKey>
            where TProperty : class
        {
            AsyncHelper.RunSync(() => repository.EnsurePropertyLoadedAsync(entity, propertyExpression));
        }

        public static IIocResolver GetIocResolver<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            var repo = ProxyHelper.UnProxy(repository) as MajidRepositoryBase<TEntity, TPrimaryKey>;
            if (repo != null)
            {
                return repo.IocResolver;
            }

            throw new ArgumentException($"Given {nameof(repository)} is not inherited from {typeof(MajidRepositoryBase<TEntity, TPrimaryKey>).AssemblyQualifiedName}");
        }

        public static async Task HardDelete<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>, ISoftDelete
        {
            var repo = ProxyHelper.UnProxy(repository) as IRepository<TEntity, TPrimaryKey>;
            if (repo == null)
            {
                throw new ArgumentException($"Given {nameof(repository)} is not inherited from {typeof(IRepository<TEntity, TPrimaryKey>).AssemblyQualifiedName}");
            }

            var items = ((IUnitOfWorkManagerAccessor)repo).UnitOfWorkManager.Current.Items;
            var hardDeleteEntities = items.GetOrAdd(UnitOfWorkExtensionDataTypes.HardDelete, () => new HashSet<string>()) as HashSet<string>;

            var tenantId = GetCurrentTenantIdOrNull(repo.GetIocResolver());
            var hardDeleteKey = EntityHelper.GetHardDeleteKey(entity, tenantId);

            hardDeleteEntities.Add(hardDeleteKey);

            await repo.DeleteAsync(entity);
        }

        private static int? GetCurrentTenantIdOrNull(IIocResolver iocResolver)
        {
            using (var scope = iocResolver.CreateScope())
            {
                var currentUnitOfWorkProvider = scope.Resolve<ICurrentUnitOfWorkProvider>();
                if (currentUnitOfWorkProvider?.Current != null)
                {
                    return currentUnitOfWorkProvider.Current.GetTenantId();
                }
            }

            return iocResolver.Resolve<IMajidSession>().TenantId;
        }
    }
}