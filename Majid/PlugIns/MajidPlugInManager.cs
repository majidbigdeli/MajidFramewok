using System;
using System.Linq;

namespace Majid.PlugIns
{
    public class MajidPlugInManager : IMajidPlugInManager
    {
        public PlugInSourceList PlugInSources { get; }

        private static readonly object SyncObj = new object();
        private static bool _isRegisteredToAssemblyResolve;

        public MajidPlugInManager()
        {
            PlugInSources = new PlugInSourceList();

            //TODO: Try to use AssemblyLoadContext.Default..?
            RegisterToAssemblyResolve(PlugInSources);
        }

        private static void RegisterToAssemblyResolve(PlugInSourceList plugInSources)
        {
            if (_isRegisteredToAssemblyResolve)
            {
                return;
            }

            lock (SyncObj)
            {
                if (_isRegisteredToAssemblyResolve)
                {
                    return;
                }

                _isRegisteredToAssemblyResolve = true;

                AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
                {
                    return plugInSources.GetAllAssemblies().FirstOrDefault(a => a.FullName == args.Name);
                };
            }
        }
    }
}