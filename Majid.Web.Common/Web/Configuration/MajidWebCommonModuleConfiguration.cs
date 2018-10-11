using System.Collections.Generic;
using Majid.Web.Api.ProxyScripting.Configuration;
using Majid.Web.MultiTenancy;
using Majid.Web.Security.AntiForgery;

namespace Majid.Web.Configuration
{
    internal class MajidWebCommonModuleConfiguration : IMajidWebCommonModuleConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; }

        public IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        public IMajidAntiForgeryConfiguration AntiForgery { get; }

        public IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }

        public IWebMultiTenancyConfiguration MultiTenancy { get; }

        public MajidWebCommonModuleConfiguration(
            IApiProxyScriptingConfiguration apiProxyScripting, 
            IMajidAntiForgeryConfiguration majidAntiForgery,
            IWebEmbeddedResourcesConfiguration embeddedResources, 
            IWebMultiTenancyConfiguration multiTenancy)
        {
            ApiProxyScripting = apiProxyScripting;
            AntiForgery = majidAntiForgery;
            EmbeddedResources = embeddedResources;
            MultiTenancy = multiTenancy;
        }
    }
}