using System.Linq;
using Majid.AspNetCore.Configuration;
using Majid.AspNetCore.MultiTenancy;
using Majid.AspNetCore.Mvc.Auditing;
using Majid.AspNetCore.Runtime.Session;
using Majid.AspNetCore.Security.AntiForgery;
using Majid.Auditing;
using Majid.Configuration.Startup;
using Majid.Dependency;
using Majid.Modules;
using Majid.Reflection.Extensions;
using Majid.Runtime.Session;
using Majid.Web;
using Majid.Web.Security.AntiForgery;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Options;

namespace Majid.AspNetCore
{
    [DependsOn(typeof(MajidWebCommonModule))]
    public class MajidAspNetCoreModule : MajidModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new MajidAspNetCoreConventionalRegistrar());

            IocManager.Register<IMajidAspNetCoreConfiguration, MajidAspNetCoreConfiguration>();

            Configuration.ReplaceService<IPrincipalAccessor, AspNetCorePrincipalAccessor>(DependencyLifeStyle.Transient);
            Configuration.ReplaceService<IMajidAntiForgeryManager, MajidAspNetCoreAntiForgeryManager>(DependencyLifeStyle.Transient);
            Configuration.ReplaceService<IClientInfoProvider, HttpContextClientInfoProvider>(DependencyLifeStyle.Transient);

            Configuration.Modules.MajidAspNetCore().FormBodyBindingIgnoredTypes.Add(typeof(IFormFile));

            Configuration.MultiTenancy.Resolvers.Add<DomainTenantResolveContributor>();
            Configuration.MultiTenancy.Resolvers.Add<HttpHeaderTenantResolveContributor>();
            Configuration.MultiTenancy.Resolvers.Add<HttpCookieTenantResolveContributor>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MajidAspNetCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            AddApplicationParts();
            ConfigureAntiforgery();
        }

        private void AddApplicationParts()
        {
            var configuration = IocManager.Resolve<MajidAspNetCoreConfiguration>();
            var partManager = IocManager.Resolve<ApplicationPartManager>();
            var moduleManager = IocManager.Resolve<IMajidModuleManager>();

            var controllerAssemblies = configuration.ControllerAssemblySettings.Select(s => s.Assembly).Distinct();
            foreach (var controllerAssembly in controllerAssemblies)
            {
                partManager.ApplicationParts.Add(new AssemblyPart(controllerAssembly));
            }

            var plugInAssemblies = moduleManager.Modules.Where(m => m.IsLoadedAsPlugIn).Select(m => m.Assembly).Distinct();
            foreach (var plugInAssembly in plugInAssemblies)
            {
                partManager.ApplicationParts.Add(new AssemblyPart(plugInAssembly));
            }
        }

        private void ConfigureAntiforgery()
        {
            IocManager.Using<IOptions<AntiforgeryOptions>>(optionsAccessor =>
            {
                optionsAccessor.Value.HeaderName = Configuration.Modules.MajidWebCommon().AntiForgery.TokenHeaderName;
            });
        }
    }
}