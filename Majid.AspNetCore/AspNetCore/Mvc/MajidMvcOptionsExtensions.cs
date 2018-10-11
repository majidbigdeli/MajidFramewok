using Majid.AspNetCore.Mvc.Auditing;
using Majid.AspNetCore.Mvc.Authorization;
using Majid.AspNetCore.Mvc.Conventions;
using Majid.AspNetCore.Mvc.ExceptionHandling;
using Majid.AspNetCore.Mvc.ModelBinding;
using Majid.AspNetCore.Mvc.Results;
using Majid.AspNetCore.Mvc.Uow;
using Majid.AspNetCore.Mvc.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Majid.AspNetCore.Mvc
{
    internal static class MajidMvcOptionsExtensions
    {
        public static void AddMajid(this MvcOptions options, IServiceCollection services)
        {
            AddConventions(options, services);
            AddFilters(options);
            AddModelBinders(options);
        }

        private static void AddConventions(MvcOptions options, IServiceCollection services)
        {
            options.Conventions.Add(new MajidAppServiceConvention(services));
        }

        private static void AddFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(MajidAuthorizationFilter));
            options.Filters.AddService(typeof(MajidAuditActionFilter));
            options.Filters.AddService(typeof(MajidValidationActionFilter));
            options.Filters.AddService(typeof(MajidUowActionFilter));
            options.Filters.AddService(typeof(MajidExceptionFilter));
            options.Filters.AddService(typeof(MajidResultFilter));
        }

        private static void AddModelBinders(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new MajidDateTimeModelBinderProvider());
        }
    }
}