using Majid.Web.Configuration;

namespace Majid.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure MAJID Web module.
    /// </summary>
    public static class MajidWebConfigurationExtensions
    {
        /// <summary>
        /// Used to configure MAJID Web Common module.
        /// </summary>
        public static IMajidWebCommonModuleConfiguration MajidWebCommon(this IModuleConfigurations configurations)
        {
            return configurations.MajidConfiguration.Get<IMajidWebCommonModuleConfiguration>();
        }
    }
}