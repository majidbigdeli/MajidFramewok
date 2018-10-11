using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Majid.Aspects;
using Majid.AspNetCore.Configuration;
using Majid.AspNetCore.Mvc.Extensions;
using Majid.Auditing;
using Majid.Dependency;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Auditing
{
    public class MajidAuditActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IMajidAspNetCoreConfiguration _configuration;
        private readonly IAuditingHelper _auditingHelper;

        public MajidAuditActionFilter(IMajidAspNetCoreConfiguration configuration, IAuditingHelper auditingHelper)
        {
            _configuration = configuration;
            _auditingHelper = auditingHelper;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!ShouldSaveAudit(context))
            {
                await next();
                return;
            }

            using (MajidCrossCuttingConcerns.Applying(context.Controller, MajidCrossCuttingConcerns.Auditing))
            {
                var auditInfo = _auditingHelper.CreateAuditInfo(
                    context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType(),
                    context.ActionDescriptor.AsControllerActionDescriptor().MethodInfo,
                    context.ActionArguments
                );

                var stopwatch = Stopwatch.StartNew();

                try
                {
                    var result = await next();
                    if (result.Exception != null && !result.ExceptionHandled)
                    {
                        auditInfo.Exception = result.Exception;
                    }
                }
                catch (Exception ex)
                {
                    auditInfo.Exception = ex;
                    throw;
                }
                finally
                {
                    stopwatch.Stop();
                    auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                    await _auditingHelper.SaveAsync(auditInfo);
                }
            }
        }

        private bool ShouldSaveAudit(ActionExecutingContext actionContext)
        {
            return _configuration.IsAuditingEnabled &&
                   actionContext.ActionDescriptor.IsControllerAction() &&
                   _auditingHelper.ShouldSaveAudit(actionContext.ActionDescriptor.GetMethodInfo(), true);
        }
    }
}
