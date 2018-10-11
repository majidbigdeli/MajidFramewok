using Majid.Configuration.Startup;

namespace Majid.AutoMapper
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Majid.AutoMapper module.
    /// </summary>
    public static class MajidAutoMapperConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Majid.AutoMapper module.
        /// </summary>
        public static IMajidAutoMapperConfiguration MajidAutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.MajidConfiguration.Get<IMajidAutoMapperConfiguration>();
        }
    }
}