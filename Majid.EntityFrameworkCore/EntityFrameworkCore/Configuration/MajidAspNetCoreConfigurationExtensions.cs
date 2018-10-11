using Majid.Configuration.Startup;

namespace Majid.EntityFrameworkCore.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure MAJID EntityFramework Core module.
    /// </summary>
    public static class MajidEfCoreConfigurationExtensions
    {
        /// <summary>
        /// Used to configure MAJID EntityFramework Core module.
        /// </summary>
        public static IMajidEfCoreConfiguration MajidEfCore(this IModuleConfigurations configurations)
        {
            return configurations.MajidConfiguration.Get<IMajidEfCoreConfiguration>();
        }
    }
}