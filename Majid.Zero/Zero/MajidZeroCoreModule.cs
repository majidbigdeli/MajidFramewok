using System.Reflection;
using Majid.Localization.Dictionaries.Xml;
using Majid.Localization.Sources;
using Majid.Modules;

namespace Majid.Zero
{
    [DependsOn(typeof(MajidZeroCommonModule))]
    public class MajidZeroCoreModule : MajidModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo(
                    MajidZeroConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Majid.Zero.Localization.SourceExt"
                    )
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
