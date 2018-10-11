using System.Collections.Generic;
using Majid.Localization;

namespace Majid.Web.Models.MajidUserConfiguration
{
    public class MajidUserLocalizationConfigDto
    {
        public MajidUserCurrentCultureConfigDto CurrentCulture { get; set; }

        public List<LanguageInfo> Languages { get; set; }

        public LanguageInfo CurrentLanguage { get; set; }

        public List<MajidLocalizationSourceDto> Sources { get; set; }

        public Dictionary<string, Dictionary<string, string>> Values { get; set; }
    }
}