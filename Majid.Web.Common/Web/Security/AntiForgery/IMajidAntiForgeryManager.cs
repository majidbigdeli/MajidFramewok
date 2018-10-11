namespace Majid.Web.Security.AntiForgery
{
    public interface IMajidAntiForgeryManager
    {
        IMajidAntiForgeryConfiguration Configuration { get; }

        string GenerateToken();
    }
}
