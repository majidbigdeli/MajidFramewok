using Majid.Configuration;

namespace Majid.AutoMapper
{
    public class MultiLingualMapContext
    {
        public ISettingManager SettingManager { get; set; }

        public MultiLingualMapContext(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }
    }
}