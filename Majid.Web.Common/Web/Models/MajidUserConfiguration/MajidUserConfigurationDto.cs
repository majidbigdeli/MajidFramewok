using System.Collections.Generic;

namespace Majid.Web.Models.MajidUserConfiguration
{
    public class MajidUserConfigurationDto
    {
        public MajidMultiTenancyConfigDto MultiTenancy { get; set; }

        public MajidUserSessionConfigDto Session { get; set; }

        public MajidUserLocalizationConfigDto Localization { get; set; }

        public MajidUserFeatureConfigDto Features { get; set; }

        public MajidUserAuthConfigDto Auth { get; set; }

        public MajidUserNavConfigDto Nav { get; set; }

        public MajidUserSettingConfigDto Setting { get; set; }

        public MajidUserClockConfigDto Clock { get; set; }

        public MajidUserTimingConfigDto Timing { get; set; }

        public MajidUserSecurityConfigDto Security { get; set; }

        public Dictionary<string, object> Custom { get; set; }
    }
}