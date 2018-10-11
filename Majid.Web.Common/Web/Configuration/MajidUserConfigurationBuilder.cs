using System.Collections.Generic;
using System.Threading.Tasks;
using Majid.Application.Features;
using Majid.Application.Navigation;
using Majid.Authorization;
using Majid.Configuration;
using Majid.Configuration.Startup;
using Majid.Localization;
using Majid.Runtime.Session;
using Majid.Timing;
using Majid.Timing.Timezone;
using Majid.Web.Models.MajidUserConfiguration;
using Majid.Web.Security.AntiForgery;
using System.Linq;
using Majid.Dependency;
using Majid.Extensions;
using System.Globalization;

namespace Majid.Web.Configuration
{
    public class MajidUserConfigurationBuilder : ITransientDependency
    {
        private readonly IMajidStartupConfiguration _startupConfiguration;

        protected IMultiTenancyConfig MultiTenancyConfig { get; }
        protected ILanguageManager LanguageManager { get; }
        protected ILocalizationManager LocalizationManager { get; }
        protected IFeatureManager FeatureManager { get; }
        protected IFeatureChecker FeatureChecker { get; }
        protected IPermissionManager PermissionManager { get; }
        protected IUserNavigationManager UserNavigationManager { get; }
        protected ISettingDefinitionManager SettingDefinitionManager { get; }
        protected ISettingManager SettingManager { get; }
        protected IMajidAntiForgeryConfiguration MajidAntiForgeryConfiguration { get; }
        protected IMajidSession MajidSession { get; }
        protected IPermissionChecker PermissionChecker { get; }
        protected Dictionary<string, object> CustomDataConfig { get; }

        private readonly IIocResolver _iocResolver;

        public MajidUserConfigurationBuilder(
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager,
            ILocalizationManager localizationManager,
            IFeatureManager featureManager,
            IFeatureChecker featureChecker,
            IPermissionManager permissionManager,
            IUserNavigationManager userNavigationManager,
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            IMajidAntiForgeryConfiguration majidAntiForgeryConfiguration,
            IMajidSession majidSession,
            IPermissionChecker permissionChecker,
            IIocResolver iocResolver,
            IMajidStartupConfiguration startupConfiguration)
        {
            MultiTenancyConfig = multiTenancyConfig;
            LanguageManager = languageManager;
            LocalizationManager = localizationManager;
            FeatureManager = featureManager;
            FeatureChecker = featureChecker;
            PermissionManager = permissionManager;
            UserNavigationManager = userNavigationManager;
            SettingDefinitionManager = settingDefinitionManager;
            SettingManager = settingManager;
            MajidAntiForgeryConfiguration = majidAntiForgeryConfiguration;
            MajidSession = majidSession;
            PermissionChecker = permissionChecker;
            _iocResolver = iocResolver;
            _startupConfiguration = startupConfiguration;

            CustomDataConfig = new Dictionary<string, object>();
        }

        public virtual async Task<MajidUserConfigurationDto> GetAll()
        {
            return new MajidUserConfigurationDto
            {
                MultiTenancy = GetUserMultiTenancyConfig(),
                Session = GetUserSessionConfig(),
                Localization = GetUserLocalizationConfig(),
                Features = await GetUserFeaturesConfig(),
                Auth = await GetUserAuthConfig(),
                Nav = await GetUserNavConfig(),
                Setting = await GetUserSettingConfig(),
                Clock = GetUserClockConfig(),
                Timing = await GetUserTimingConfig(),
                Security = GetUserSecurityConfig(),
                Custom = _startupConfiguration.GetCustomConfig()
            };
        }

        protected virtual MajidMultiTenancyConfigDto GetUserMultiTenancyConfig()
        {
            return new MajidMultiTenancyConfigDto
            {
                IsEnabled = MultiTenancyConfig.IsEnabled
            };
        }

        protected virtual MajidUserSessionConfigDto GetUserSessionConfig()
        {
            return new MajidUserSessionConfigDto
            {
                UserId = MajidSession.UserId,
                TenantId = MajidSession.TenantId,
                ImpersonatorUserId = MajidSession.ImpersonatorUserId,
                ImpersonatorTenantId = MajidSession.ImpersonatorTenantId,
                MultiTenancySide = MajidSession.MultiTenancySide
            };
        }

        protected virtual MajidUserLocalizationConfigDto GetUserLocalizationConfig()
        {
            var currentCulture = CultureInfo.CurrentUICulture;
            var languages = LanguageManager.GetLanguages();

            var config = new MajidUserLocalizationConfigDto
            {
                CurrentCulture = new MajidUserCurrentCultureConfigDto
                {
                    Name = currentCulture.Name,
                    DisplayName = currentCulture.DisplayName
                },
                Languages = languages.ToList()
            };

            if (languages.Count > 0)
            {
                config.CurrentLanguage = LanguageManager.CurrentLanguage;
            }

            var sources = LocalizationManager.GetAllSources().OrderBy(s => s.Name).ToArray();
            config.Sources = sources.Select(s => new MajidLocalizationSourceDto
            {
                Name = s.Name,
                Type = s.GetType().Name
            }).ToList();

            config.Values = new Dictionary<string, Dictionary<string, string>>();
            foreach (var source in sources)
            {
                var stringValues = source.GetAllStrings(currentCulture).OrderBy(s => s.Name).ToList();
                var stringDictionary = stringValues
                    .ToDictionary(_ => _.Name, _ => _.Value);
                config.Values.Add(source.Name, stringDictionary);
            }

            return config;
        }

        protected virtual async Task<MajidUserFeatureConfigDto> GetUserFeaturesConfig()
        {
            var config = new MajidUserFeatureConfigDto()
            {
                AllFeatures = new Dictionary<string, MajidStringValueDto>()
            };

            var allFeatures = FeatureManager.GetAll().ToList();

            if (MajidSession.TenantId.HasValue)
            {
                var currentTenantId = MajidSession.GetTenantId();
                foreach (var feature in allFeatures)
                {
                    var value = await FeatureChecker.GetValueAsync(currentTenantId, feature.Name);
                    config.AllFeatures.Add(feature.Name, new MajidStringValueDto
                    {
                        Value = value
                    });
                }
            }
            else
            {
                foreach (var feature in allFeatures)
                {
                    config.AllFeatures.Add(feature.Name, new MajidStringValueDto
                    {
                        Value = feature.DefaultValue
                    });
                }
            }

            return config;
        }

        protected virtual async Task<MajidUserAuthConfigDto> GetUserAuthConfig()
        {
            var config = new MajidUserAuthConfigDto();

            var allPermissionNames = PermissionManager.GetAllPermissions(false).Select(p => p.Name).ToList();
            var grantedPermissionNames = new List<string>();

            if (MajidSession.UserId.HasValue)
            {
                foreach (var permissionName in allPermissionNames)
                {
                    if (await PermissionChecker.IsGrantedAsync(permissionName))
                    {
                        grantedPermissionNames.Add(permissionName);
                    }
                }
            }

            config.AllPermissions = allPermissionNames.ToDictionary(permissionName => permissionName, permissionName => "true");
            config.GrantedPermissions = grantedPermissionNames.ToDictionary(permissionName => permissionName, permissionName => "true");

            return config;
        }

        protected virtual async Task<MajidUserNavConfigDto> GetUserNavConfig()
        {
            var userMenus = await UserNavigationManager.GetMenusAsync(MajidSession.ToUserIdentifier());
            return new MajidUserNavConfigDto
            {
                Menus = userMenus.ToDictionary(userMenu => userMenu.Name, userMenu => userMenu)
            };
        }

        protected virtual async Task<MajidUserSettingConfigDto> GetUserSettingConfig()
        {
            var config = new MajidUserSettingConfigDto
            {
                Values = new Dictionary<string, string>()
            };

            var settingDefinitions = SettingDefinitionManager
                .GetAllSettingDefinitions();

            using (var scope = _iocResolver.CreateScope())
            {
                foreach (var settingDefinition in settingDefinitions)
                {
                    if (!await settingDefinition.ClientVisibilityProvider.CheckVisible(scope))
                    {
                        continue;
                    }

                    var settingValue = await SettingManager.GetSettingValueAsync(settingDefinition.Name);
                    config.Values.Add(settingDefinition.Name, settingValue);
                }
            }

            return config;
        }

        protected virtual MajidUserClockConfigDto GetUserClockConfig()
        {
            return new MajidUserClockConfigDto
            {
                Provider = Clock.Provider.GetType().Name.ToCamelCase()
            };
        }

        protected virtual async Task<MajidUserTimingConfigDto> GetUserTimingConfig()
        {
            var timezoneId = await SettingManager.GetSettingValueAsync(TimingSettingNames.TimeZone);
            var timezone = TimezoneHelper.FindTimeZoneInfo(timezoneId);

            return new MajidUserTimingConfigDto
            {
                TimeZoneInfo = new MajidUserTimeZoneConfigDto
                {
                    Windows = new MajidUserWindowsTimeZoneConfigDto
                    {
                        TimeZoneId = timezoneId,
                        BaseUtcOffsetInMilliseconds = timezone.BaseUtcOffset.TotalMilliseconds,
                        CurrentUtcOffsetInMilliseconds = timezone.GetUtcOffset(Clock.Now).TotalMilliseconds,
                        IsDaylightSavingTimeNow = timezone.IsDaylightSavingTime(Clock.Now)
                    },
                    Iana = new MajidUserIanaTimeZoneConfigDto
                    {
                        TimeZoneId = TimezoneHelper.WindowsToIana(timezoneId)
                    }
                }
            };
        }

        protected virtual MajidUserSecurityConfigDto GetUserSecurityConfig()
        {
            return new MajidUserSecurityConfigDto
            {
                AntiForgery = new MajidUserAntiForgeryConfigDto
                {
                    TokenCookieName = MajidAntiForgeryConfiguration.TokenCookieName,
                    TokenHeaderName = MajidAntiForgeryConfiguration.TokenHeaderName
                }
            };
        }
    }
}
