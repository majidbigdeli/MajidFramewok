using System.Linq;
using System.Reflection;
using Majid.Application.Services;
using Majid.AspNetCore.Configuration;
using Majid.Dependency;
using Majid.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Majid.AspNetCore.Mvc.Providers
{
    /// <summary>
    /// Used to add application services as controller.
    /// </summary>
    public class MajidAppServiceControllerFeatureProvider : ControllerFeatureProvider
    {
        private readonly IIocResolver _iocResolver;

        public MajidAppServiceControllerFeatureProvider(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            var type = typeInfo.AsType();

            if (!typeof(IApplicationService).IsAssignableFrom(type) ||
                !typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
            {
                return false;
            }

            var remoteServiceAttr = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(typeInfo);

            if (remoteServiceAttr != null && !remoteServiceAttr.IsEnabledFor(type))
            {
                return false;
            }

            var configuration = _iocResolver.Resolve<MajidAspNetCoreConfiguration>().ControllerAssemblySettings.GetSettingOrNull(type);
            return configuration != null && configuration.TypePredicate(type);
        }
    }
}