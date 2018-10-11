using System;
using Majid.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Majid.EntityFrameworkCore
{
    public static class MajidEfCoreServiceCollectionExtensions
    {
        public static void AddMajidDbContext<TDbContext>(
            this IServiceCollection services,
            Action<MajidDbContextConfiguration<TDbContext>> action)
            where TDbContext : DbContext
        {
            services.AddSingleton(
                typeof(IMajidDbContextConfigurer<TDbContext>),
                new MajidDbContextConfigurerAction<TDbContext>(action)
            );
        }
    }
}
