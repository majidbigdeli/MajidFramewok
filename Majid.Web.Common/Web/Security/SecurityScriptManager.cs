using System.Text;
using Majid.Dependency;
using Majid.Web.Security.AntiForgery;

namespace Majid.Web.Security
{
    internal class SecurityScriptManager : ISecurityScriptManager, ITransientDependency
    {
        private readonly IMajidAntiForgeryConfiguration _majidAntiForgeryConfiguration;

        public SecurityScriptManager(IMajidAntiForgeryConfiguration majidAntiForgeryConfiguration)
        {
            _majidAntiForgeryConfiguration = majidAntiForgeryConfiguration;
        }

        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine("    majid.security.antiForgery.tokenCookieName = '" + _majidAntiForgeryConfiguration.TokenCookieName + "';");
            script.AppendLine("    majid.security.antiForgery.tokenHeaderName = '" + _majidAntiForgeryConfiguration.TokenHeaderName + "';");
            script.Append("})();");

            return script.ToString();
        }
    }
}
