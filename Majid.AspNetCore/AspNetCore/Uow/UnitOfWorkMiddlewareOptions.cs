using System;
using Majid.Domain.Uow;
using Microsoft.AspNetCore.Http;

namespace Majid.AspNetCore.Uow
{
    public class UnitOfWorkMiddlewareOptions
    {
        public Func<HttpContext, bool> Filter { get; set; } = context => true;

        public Func<HttpContext, UnitOfWorkOptions> OptionsFactory { get; set; } = context => new UnitOfWorkOptions();
    }
}
