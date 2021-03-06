﻿using Majid.Domain.Uow;
using Majid.EntityFrameworkCore;
using Majid.Modules;
using Majid.MultiTenancy;
using Majid.Reflection.Extensions;
using Castle.MicroKernel.Registration;

namespace Majid.Zero.EntityFrameworkCore
{
    /// <summary>
    /// Entity framework integration module for ASP.NET Boilerplate Zero.
    /// </summary>
    [DependsOn(typeof(MajidZeroCoreModule), typeof(MajidEntityFrameworkCoreModule))]
    public class MajidZeroCoreEntityFrameworkCoreModule : MajidModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService(typeof(IConnectionStringResolver), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IConnectionStringResolver, IDbPerTenantConnectionStringResolver>()
                        .ImplementedBy<DbPerTenantConnectionStringResolver>()
                        .LifestyleTransient()
                    );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MajidZeroCoreEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
