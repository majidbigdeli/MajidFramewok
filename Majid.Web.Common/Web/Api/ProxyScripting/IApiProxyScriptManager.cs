namespace Majid.Web.Api.ProxyScripting
{
    public interface IApiProxyScriptManager
    {
        string GetScript(ApiProxyGenerationOptions options);
    }
}