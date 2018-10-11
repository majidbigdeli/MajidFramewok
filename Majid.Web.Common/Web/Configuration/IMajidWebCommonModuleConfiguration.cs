﻿using System.Collections.Generic;
using Majid.Web.Api.ProxyScripting.Configuration;
using Majid.Web.MultiTenancy;
using Majid.Web.Security.AntiForgery;

namespace Majid.Web.Configuration
{
    /// <summary>
    /// Used to configure MAJID Web Common module.
    /// </summary>
    public interface IMajidWebCommonModuleConfiguration
    {
        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (MAJID hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }

        /// <summary>
        /// Used to configure Api proxy scripting.
        /// </summary>
        IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        /// <summary>
        /// Used to configure Anti Forgery security settings.
        /// </summary>
        IMajidAntiForgeryConfiguration AntiForgery { get; }

        /// <summary>
        /// Used to configure embedded resource system for web applications.
        /// </summary>
        IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }

        IWebMultiTenancyConfiguration MultiTenancy { get; }
    }
}