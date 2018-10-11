using Majid.Localization.Dictionaries.Xml;
using Majid.Localization.Sources;
using Majid.Modules;
using Majid.Reflection.Extensions;

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
                        typeof(MajidZeroCoreModule).GetAssembly(), "Majid.Zero.Localization.SourceExt"
                    )
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MajidZeroCoreModule).GetAssembly());
        }
    }
}
