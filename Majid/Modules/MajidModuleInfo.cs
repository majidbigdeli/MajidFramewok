using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Majid.Modules
{
    /// <summary>
    /// Used to store all needed information for a module.
    /// </summary>
    public class MajidModuleInfo
    {
        /// <summary>
        /// The assembly which contains the module definition.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Type of the module.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Instance of the module.
        /// </summary>
        public MajidModule Instance { get; }

        /// <summary>
        /// Is this module loaded as a plugin.
        /// </summary>
        public bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// All dependent modules of this module.
        /// </summary>
        public List<MajidModuleInfo> Dependencies { get; }

        /// <summary>
        /// Creates a new MajidModuleInfo object.
        /// </summary>
        public MajidModuleInfo([NotNull] Type type, [NotNull] MajidModule instance, bool isLoadedAsPlugIn)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;
            Assembly = Type.GetTypeInfo().Assembly;

            Dependencies = new List<MajidModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}