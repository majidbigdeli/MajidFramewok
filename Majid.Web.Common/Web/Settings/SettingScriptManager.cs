using System.Text;
using System.Threading.Tasks;
using Majid.Configuration;
using Majid.Dependency;
using Majid.Runtime.Session;
using Majid.Web.Http;

namespace Majid.Web.Settings
{
    /// <summary>
    /// This class is used to build setting script.
    /// </summary>
    public class SettingScriptManager : ISettingScriptManager, ISingletonDependency
    {
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly ISettingManager _settingManager;
        private readonly IMajidSession _majidSession;
        private readonly IIocResolver _iocResolver;

        public SettingScriptManager(
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            IMajidSession majidSession,
            IIocResolver iocResolver)
        {
            _settingDefinitionManager = settingDefinitionManager;
            _settingManager = settingManager;
            _majidSession = majidSession;
            _iocResolver = iocResolver;
        }

        public async Task<string> GetScriptAsync()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine("    majid.setting = majid.setting || {};");
            script.AppendLine("    majid.setting.values = {");

            var settingDefinitions = _settingDefinitionManager
                .GetAllSettingDefinitions();

            var added = 0;

            using (var scope = _iocResolver.CreateScope())
            {
                foreach (var settingDefinition in settingDefinitions)
                {
                    if (!await settingDefinition.ClientVisibilityProvider.CheckVisible(scope))
                    {
                        continue;
                    }

                    if (added > 0)
                    {
                        script.AppendLine(",");
                    }
                    else
                    {
                        script.AppendLine();
                    }

                    var settingValue = await _settingManager.GetSettingValueAsync(settingDefinition.Name);

                    script.Append("        '" +
                                  settingDefinition.Name.Replace("'", @"\'") + "': " +
                                  (settingValue == null ? "null" : "'" + HttpEncode.JavaScriptStringEncode(settingValue) + "'"));

                    ++added;
                }
            }

            script.AppendLine();
            script.AppendLine("    };");

            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}