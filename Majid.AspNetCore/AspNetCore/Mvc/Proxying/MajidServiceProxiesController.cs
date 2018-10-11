using Majid.AspNetCore.Mvc.Controllers;
using Majid.Auditing;
using Majid.Web.Api.ProxyScripting;
using Majid.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Majid.AspNetCore.Mvc.Proxying
{
    [DontWrapResult]
    [DisableAuditing]
    public class MajidServiceProxiesController : MajidController
    {
        private readonly IApiProxyScriptManager _proxyScriptManager;

        public MajidServiceProxiesController(IApiProxyScriptManager proxyScriptManager)
        {
            _proxyScriptManager = proxyScriptManager;
        }

        [Produces("application/x-javascript")]
        public ContentResult GetAll(ApiProxyGenerationModel model)
        {
            var script = _proxyScriptManager.GetScript(model.CreateOptions());
            return Content(script, "application/x-javascript");
        }
    }
}
