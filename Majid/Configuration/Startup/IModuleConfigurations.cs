namespace Majid.Configuration.Startup
{
    /// <summary>
    /// Used to provide a way to configure modules.
    /// Create entension methods to this class to be used over <see cref="IMajidStartupConfiguration.Modules"/> object.
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the MAJID configuration object.
        /// </summary>
        IMajidStartupConfiguration MajidConfiguration { get; }
    }
}