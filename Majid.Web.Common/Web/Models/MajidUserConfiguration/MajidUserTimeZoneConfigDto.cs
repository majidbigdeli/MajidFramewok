namespace Majid.Web.Models.MajidUserConfiguration
{
    public class MajidUserTimeZoneConfigDto
    {
        public MajidUserWindowsTimeZoneConfigDto Windows { get; set; }

        public MajidUserIanaTimeZoneConfigDto Iana { get; set; }
    }
}