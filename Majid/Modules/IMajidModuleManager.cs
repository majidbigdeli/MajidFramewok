using System;
using System.Collections.Generic;

namespace Majid.Modules
{
    public interface IMajidModuleManager
    {
        MajidModuleInfo StartupModule { get; }

        IReadOnlyList<MajidModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}