using System;
using Majid.Dependency;
using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;

namespace Majid.EntityFrameworkCore.Configuration
{
    public class MajidEfCoreConfiguration : IMajidEfCoreConfiguration
    {
        private readonly IIocManager _iocManager;

        public MajidEfCoreConfiguration(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public void AddDbContext<TDbContext>(Action<MajidDbContextConfiguration<TDbContext>> action) 
            where TDbContext : DbContext
        {
            _iocManager.IocContainer.Register(
                Component.For<IMajidDbContextConfigurer<TDbContext>>().Instance(
                    new MajidDbContextConfigurerAction<TDbContext>(action)
                ).IsDefault()
            );
        }
    }
}