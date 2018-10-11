using Majid.Modules;
using Majid.Reflection.Extensions;

namespace Majid.EntityFramework
{
    [DependsOn(typeof(MajidKernelModule))]
    public class MajidEntityFrameworkCommonModule : MajidModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MajidEntityFrameworkCommonModule).GetAssembly());
        }
    }
}
