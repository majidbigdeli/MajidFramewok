using Majid.Dependency;

namespace Majid.Web.Mvc.Alerts
{
    public class AlertManager : IAlertManager, IPerWebRequestDependency
    {
        public AlertList Alerts { get; }

        public AlertManager()
        {
            Alerts = new AlertList();
        }
    }
}