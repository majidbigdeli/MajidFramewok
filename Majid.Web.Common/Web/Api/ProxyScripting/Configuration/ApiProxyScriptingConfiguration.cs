using System;
using System.Collections.Generic;

namespace Majid.Web.Api.ProxyScripting.Configuration
{
    public class ApiProxyScriptingConfiguration : IApiProxyScriptingConfiguration
    {
        public IDictionary<string, Type> Generators { get; }

        public bool RemoveAsyncPostfixOnProxyGeneration { get; set; }

        public ApiProxyScriptingConfiguration()
        {
            Generators = new Dictionary<string, Type>();
            RemoveAsyncPostfixOnProxyGeneration = true;
        }
    }
}