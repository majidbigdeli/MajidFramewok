using Majid.Configuration.Startup;

namespace Majid.Zero.Configuration
{
    /// <summary>
    /// Extension methods for module zero configurations.
    /// </summary>
    public static class ModuleZeroConfigurationExtensions
    {
        /// <summary>
        /// Used to configure module zero.
        /// </summary>
        /// <param name="moduleConfigurations"></param>
        /// <returns></returns>
        public static IMajidZeroConfig Zero(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.MajidConfiguration.Get<IMajidZeroConfig>();
        }
    }
}