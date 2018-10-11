namespace Majid.Configuration.Startup
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public IMajidStartupConfiguration MajidConfiguration { get; private set; }

        public ModuleConfigurations(IMajidStartupConfiguration majidConfiguration)
        {
            MajidConfiguration = majidConfiguration;
        }
    }
}