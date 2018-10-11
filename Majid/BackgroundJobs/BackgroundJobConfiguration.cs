using Majid.Configuration.Startup;

namespace Majid.BackgroundJobs
{
    internal class BackgroundJobConfiguration : IBackgroundJobConfiguration
    {
        public bool IsJobExecutionEnabled { get; set; }
        
        public IMajidStartupConfiguration MajidConfiguration { get; private set; }

        public BackgroundJobConfiguration(IMajidStartupConfiguration majidConfiguration)
        {
            MajidConfiguration = majidConfiguration;

            IsJobExecutionEnabled = true;
        }
    }
}