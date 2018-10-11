using System.Threading.Tasks;
using Majid.Application.Services;
using Majid.Aspects;
using Majid.AspNetCore.Configuration;
using Majid.AspNetCore.Mvc.Extensions;
using Majid.Dependency;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Validation
{
    public class MajidValidationActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly IMajidAspNetCoreConfiguration _configuration;

        public MajidValidationActionFilter(IIocResolver iocResolver, IMajidAspNetCoreConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_configuration.IsValidationEnabledForControllers || !context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            using (MajidCrossCuttingConcerns.Applying(context.Controller, MajidCrossCuttingConcerns.Validation))
            {
                using (var validator = _iocResolver.ResolveAsDisposable<MvcActionInvocationValidator>())
                {
                    validator.Object.Initialize(context);
                    validator.Object.Validate();
                }

                await next();
            }
        }
    }
}
