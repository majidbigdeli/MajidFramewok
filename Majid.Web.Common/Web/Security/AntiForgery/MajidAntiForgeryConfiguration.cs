namespace Majid.Web.Security.AntiForgery
{
    public class MajidAntiForgeryConfiguration : IMajidAntiForgeryConfiguration
    {
        public string TokenCookieName { get; set; }

        public string TokenHeaderName { get; set; }

        public string AuthorizationCookieName { get; set; }

        public MajidAntiForgeryConfiguration()
        {
            TokenCookieName = "XSRF-TOKEN";
            TokenHeaderName = "X-XSRF-TOKEN";
            AuthorizationCookieName = ".AspNet.ApplicationCookie";
        }
    }
}
