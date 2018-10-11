using Majid.Configuration.Startup;

namespace Majid.AspNetCore.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure MAJID ASP.NET Core module.
    /// </summary>
    public static class MajidAspNetCoreConfigurationExtensions
    {
        /// <summary>
        /// Used to configure MAJID ASP.NET Core module.
        /// </summary>
        public static IMajidAspNetCoreConfiguration MajidAspNetCore(this IModuleConfigurations configurations)
        {
            return configurations.MajidConfiguration.Get<IMajidAspNetCoreConfiguration>();
        }
    }
}