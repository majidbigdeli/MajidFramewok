using System;
using System.Globalization;
using System.Text;
using Majid.Configuration.Startup;
using Majid.Dependency;
using Majid.Extensions;
using Majid.MultiTenancy;

namespace Majid.Web.MultiTenancy
{
    public class MultiTenancyScriptManager : IMultiTenancyScriptManager, ITransientDependency
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public MultiTenancyScriptManager(IMultiTenancyConfig multiTenancyConfig)
        {
            _multiTenancyConfig = multiTenancyConfig;
        }

        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(majid){");
            script.AppendLine();

            script.AppendLine("    majid.multiTenancy = majid.multiTenancy || {};");
            script.AppendLine("    majid.multiTenancy.isEnabled = " + _multiTenancyConfig.IsEnabled.ToString().ToLowerInvariant() + ";");

            script.AppendLine();
            script.Append("})(majid);");

            return script.ToString();
        }
    }
}