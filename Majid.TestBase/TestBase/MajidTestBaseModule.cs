using Majid.Modules;
using Majid.Reflection.Extensions;

namespace Majid.TestBase
{
    [DependsOn(typeof(MajidKernelModule))]
    public class MajidTestBaseModule : MajidModule
    {
        public override void PreInitialize()
        {
            Configuration.EventBus.UseDefaultEventBus = false;
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MajidTestBaseModule).GetAssembly());
        }
    }
}