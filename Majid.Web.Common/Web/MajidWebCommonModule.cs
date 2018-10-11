using Majid.Configuration.Startup;
using Majid.Localization.Dictionaries;
using Majid.Localization.Dictionaries.Xml;
using Majid.Modules;
using Majid.Web.Api.ProxyScripting.Configuration;
using Majid.Web.Api.ProxyScripting.Generators.JQuery;
using Majid.Web.Configuration;
using Majid.Web.MultiTenancy;
using Majid.Web.Security.AntiForgery;
using Majid.Reflection.Extensions;

namespace Majid.Web
{
    /// <summary>
    /// This module is used to use MAJID in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(MajidKernelModule))]    
    public class MajidWebCommonModule : MajidModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.Register<IWebMultiTenancyConfiguration, WebMultiTenancyConfiguration>();
            IocManager.Register<IApiProxyScriptingConfiguration, ApiProxyScriptingConfiguration>();
            IocManager.Register<IMajidAntiForgeryConfiguration, MajidAntiForgeryConfiguration>();
            IocManager.Register<IWebEmbeddedResourcesConfiguration, WebEmbeddedResourcesConfiguration>();
            IocManager.Register<IMajidWebCommonModuleConfiguration, MajidWebCommonModuleConfiguration>();

            Configuration.Modules.MajidWebCommon().ApiProxyScripting.Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    MajidWebConsts.LocalizaionSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(MajidWebCommonModule).GetAssembly(), "Majid.Web.Localization.MajidWebXmlSource"
                        )));
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MajidWebCommonModule).GetAssembly());            
        }
    }
}
