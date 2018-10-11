using System;
using Microsoft.EntityFrameworkCore;

namespace Majid.EntityFrameworkCore.Configuration
{
    public class MajidDbContextConfigurerAction<TDbContext> : IMajidDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        public Action<MajidDbContextConfiguration<TDbContext>> Action { get; set; }

        public MajidDbContextConfigurerAction(Action<MajidDbContextConfiguration<TDbContext>> action)
        {
            Action = action;
        }

        public void Configure(MajidDbContextConfiguration<TDbContext> configuration)
        {
            Action(configuration);
        }
    }
}