namespace Majid.Web.Models.MajidUserConfiguration
{
    public class MajidUserAntiForgeryConfigDto
    {
        public string TokenCookieName { get; set; }

        public string TokenHeaderName { get; set; }
    }
}