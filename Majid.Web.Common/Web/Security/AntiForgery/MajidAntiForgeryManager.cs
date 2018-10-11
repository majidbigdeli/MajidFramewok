using System;
using Majid.Dependency;
using Castle.Core.Logging;

namespace Majid.Web.Security.AntiForgery
{
    public class MajidAntiForgeryManager : IMajidAntiForgeryManager, IMajidAntiForgeryValidator, ITransientDependency
    {
        public ILogger Logger { protected get; set; }

        public IMajidAntiForgeryConfiguration Configuration { get; }

        public MajidAntiForgeryManager(IMajidAntiForgeryConfiguration configuration)
        {
            Configuration = configuration;
            Logger = NullLogger.Instance;
        }

        public virtual string GenerateToken()
        {
            return Guid.NewGuid().ToString("D");
        }

        public virtual bool IsValid(string cookieValue, string tokenValue)
        {
            return cookieValue == tokenValue;
        }
    }
}