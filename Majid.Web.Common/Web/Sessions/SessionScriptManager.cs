using System.Text;
using Majid.Dependency;
using Majid.Runtime.Session;

namespace Majid.Web.Sessions
{
    public class SessionScriptManager : ISessionScriptManager, ITransientDependency
    {
        public IMajidSession MajidSession { get; set; }

        public SessionScriptManager()
        {
            MajidSession = NullMajidSession.Instance;
        }

        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();

            script.AppendLine("    majid.session = majid.session || {};");
            script.AppendLine("    majid.session.userId = " + (MajidSession.UserId.HasValue ? MajidSession.UserId.Value.ToString() : "null") + ";");
            script.AppendLine("    majid.session.tenantId = " + (MajidSession.TenantId.HasValue ? MajidSession.TenantId.Value.ToString() : "null") + ";");
            script.AppendLine("    majid.session.impersonatorUserId = " + (MajidSession.ImpersonatorUserId.HasValue ? MajidSession.ImpersonatorUserId.Value.ToString() : "null") + ";");
            script.AppendLine("    majid.session.impersonatorTenantId = " + (MajidSession.ImpersonatorTenantId.HasValue ? MajidSession.ImpersonatorTenantId.Value.ToString() : "null") + ";");
            script.AppendLine("    majid.session.multiTenancySide = " + ((int)MajidSession.MultiTenancySide) + ";");

            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}