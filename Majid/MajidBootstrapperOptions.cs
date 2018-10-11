using Majid.Dependency;
using Majid.PlugIns;

namespace Majid
{
    public class MajidBootstrapperOptions
    {
        /// <summary>
        /// Used to disable all interceptors added by MAJID.
        /// </summary>
        public bool DisableAllInterceptors { get; set; }

        /// <summary>
        /// IIocManager that is used to bootstrap the MAJID system. If set to null, uses global <see cref="Majid.Dependency.IocManager.Instance"/>
        /// </summary>
        public IIocManager IocManager { get; set; }

        /// <summary>
        /// List of plugin sources.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        public MajidBootstrapperOptions()
        {
            IocManager = Majid.Dependency.IocManager.Instance;
            PlugInSources = new PlugInSourceList();
        }
    }
}
