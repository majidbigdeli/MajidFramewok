using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Majid.Modules;

namespace Majid.Reflection
{
    public class MajidAssemblyFinder : IAssemblyFinder
    {
        private readonly IMajidModuleManager _moduleManager;

        public MajidAssemblyFinder(IMajidModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public List<Assembly> GetAllAssemblies()
        {
            var assemblies = new List<Assembly>();

            foreach (var module in _moduleManager.Modules)
            {
                assemblies.Add(module.Assembly);
                assemblies.AddRange(module.Instance.GetAdditionalAssemblies());
            }

            return assemblies.Distinct().ToList();
        }
    }
}