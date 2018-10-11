using Majid.AspNetCore.Configuration;
using Majid.AspNetCore.Mvc.Extensions;
using Majid.AspNetCore.Mvc.Results.Wrapping;
using Majid.Dependency;
using Majid.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Results
{
    public class MajidResultFilter : IResultFilter, ITransientDependency
    {
        private readonly IMajidAspNetCoreConfiguration _configuration;
        private readonly IMajidActionResultWrapperFactory _actionResultWrapperFactory;

        public MajidResultFilter(IMajidAspNetCoreConfiguration configuration, 
            IMajidActionResultWrapperFactory actionResultWrapper)
        {
            _configuration = configuration;
            _actionResultWrapperFactory = actionResultWrapper;
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();

            //var clientCacheAttribute = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
            //    methodInfo,
            //    _configuration.DefaultClientCacheAttribute
            //);

            //clientCacheAttribute?.Apply(context);
            
            var wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    methodInfo,
                    _configuration.DefaultWrapResultAttribute
                );

            if (!wrapResultAttribute.WrapOnSuccess)
            {
                return;
            }

            _actionResultWrapperFactory.CreateFor(context).Wrap(context);
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }
    }
}
