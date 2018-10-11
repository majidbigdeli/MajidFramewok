namespace Majid.Web.Models.MajidUserConfiguration
{
    public class MajidMultiTenancyConfigDto
    {
        public bool IsEnabled { get; set; }

        public MajidMultiTenancySidesConfigDto Sides { get; private set; }

        public MajidMultiTenancyConfigDto()
        {
            Sides = new MajidMultiTenancySidesConfigDto();
        }
    }
}