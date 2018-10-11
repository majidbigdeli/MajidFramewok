using System.Globalization;
using System.Linq;
using System.Text;
using Majid.Dependency;
using Majid.Json;
using Majid.Localization;

namespace Majid.Web.Localization
{
    internal class LocalizationScriptManager : ILocalizationScriptManager, ISingletonDependency
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly ILanguageManager _languageManager;

        public LocalizationScriptManager(
            ILocalizationManager localizationManager,
            ILanguageManager languageManager)
        {
            _localizationManager = localizationManager;
            _languageManager = languageManager;
        }

        /// <inheritdoc/>
        public string GetScript()
        {
            return GetScript(CultureInfo.CurrentUICulture);
        }

        /// <inheritdoc/>
        public string GetScript(CultureInfo cultureInfo)
        {
            //NOTE: Disabled caching since it's not true (localization script is changed per user, per tenant, per culture...)
            return BuildAll(cultureInfo);
            //return _cacheManager.GetCache(MajidCacheNames.LocalizationScripts).Get(cultureInfo.Name, () => BuildAll(cultureInfo));
        }

        private string BuildAll(CultureInfo cultureInfo)
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();
            script.AppendLine("    majid.localization = majid.localization || {};");
            script.AppendLine();
            script.AppendLine("    majid.localization.currentCulture = {");
            script.AppendLine("        name: '" + cultureInfo.Name + "',");
            script.AppendLine("        displayName: '" + cultureInfo.DisplayName + "'");
            script.AppendLine("    };");
            script.AppendLine();
            script.Append("    majid.localization.languages = [");

            var languages = _languageManager.GetLanguages();
            for (var i = 0; i < languages.Count; i++)
            {
                var language = languages[i];

                script.AppendLine("{");
                script.AppendLine("        name: '" + language.Name + "',");
                script.AppendLine("        displayName: '" + language.DisplayName + "',");
                script.AppendLine("        icon: '" + language.Icon + "',");
                script.AppendLine("        isDisabled: " + language.IsDisabled.ToString().ToLowerInvariant() + ",");
                script.AppendLine("        isDefault: " + language.IsDefault.ToString().ToLowerInvariant());
                script.Append("    }");

                if (i < languages.Count - 1)
                {
                    script.Append(" , ");
                }
            }

            script.AppendLine("];");
            script.AppendLine();

            if (languages.Count > 0)
            {
                var currentLanguage = _languageManager.CurrentLanguage;
                script.AppendLine("    majid.localization.currentLanguage = {");
                script.AppendLine("        name: '" + currentLanguage.Name + "',");
                script.AppendLine("        displayName: '" + currentLanguage.DisplayName + "',");
                script.AppendLine("        icon: '" + currentLanguage.Icon + "',");
                script.AppendLine("        isDisabled: " + currentLanguage.IsDisabled.ToString().ToLowerInvariant() + ",");
                script.AppendLine("        isDefault: " + currentLanguage.IsDefault.ToString().ToLowerInvariant());
                script.AppendLine("    };");
            }

            var sources = _localizationManager.GetAllSources().OrderBy(s => s.Name).ToArray();

            script.AppendLine();
            script.AppendLine("    majid.localization.sources = [");

            for (int i = 0; i < sources.Length; i++)
            {
                var source = sources[i];
                script.AppendLine("        {");
                script.AppendLine("            name: '" + source.Name + "',");
                script.AppendLine("            type: '" + source.GetType().Name + "'");
                script.AppendLine("        }" + (i < (sources.Length - 1) ? "," : ""));
            }

            script.AppendLine("    ];");

            script.AppendLine();
            script.AppendLine("    majid.localization.values = majid.localization.values || {};");
            script.AppendLine();

            foreach (var source in sources)
            {
                script.Append("    majid.localization.values['" + source.Name + "'] = ");

                var stringValues = source.GetAllStrings(cultureInfo).OrderBy(s => s.Name).ToList();
                var stringJson = stringValues
                    .ToDictionary(_ => _.Name, _ => _.Value)
                    .ToJsonString(indented: true);
                script.Append(stringJson);

                script.AppendLine(";");
                script.AppendLine();
            }

            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}
