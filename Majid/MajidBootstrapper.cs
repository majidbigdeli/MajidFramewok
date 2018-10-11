using System;
using System.Reflection;
using Majid.Auditing;
using Majid.Authorization;
using Majid.Configuration.Startup;
using Majid.Dependency;
using Majid.Dependency.Installers;
using Majid.Domain.Uow;
using Majid.EntityHistory;
using Majid.Modules;
using Majid.PlugIns;
using Majid.Runtime.Validation.Interception;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using JetBrains.Annotations;

namespace Majid
{
    /// <summary>
    /// This is the main class that is responsible to start entire MAJID system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class MajidBootstrapper : IDisposable
    {
        /// <summary>
        /// Get the startup module of the application which depends on other used modules.
        /// </summary>
        public Type StartupModule { get; }

        /// <summary>
        /// A list of plug in folders.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private MajidModuleManager _moduleManager;
        private ILogger _logger;

        /// <summary>
        /// Creates a new <see cref="MajidBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="MajidModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        private MajidBootstrapper([NotNull] Type startupModule, [CanBeNull] Action<MajidBootstrapperOptions> optionsAction = null)
        {
            Check.NotNull(startupModule, nameof(startupModule));

            var options = new MajidBootstrapperOptions();
            optionsAction?.Invoke(options);

            if (!typeof(MajidModule).GetTypeInfo().IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(MajidModule)}.");
            }

            StartupModule = startupModule;

            IocManager = options.IocManager;
            PlugInSources = options.PlugInSources;

            _logger = NullLogger.Instance;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }

        /// <summary>
        /// Creates a new <see cref="MajidBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="MajidModule"/>.</typeparam>
        /// <param name="optionsAction">An action to set options</param>
        public static MajidBootstrapper Create<TStartupModule>([CanBeNull] Action<MajidBootstrapperOptions> optionsAction = null)
            where TStartupModule : MajidModule
        {
            return new MajidBootstrapper(typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="MajidBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="MajidModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        public static MajidBootstrapper Create([NotNull] Type startupModule, [CanBeNull] Action<MajidBootstrapperOptions> optionsAction = null)
        {
            return new MajidBootstrapper(startupModule, optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="MajidBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="MajidModule"/>.</typeparam>
        /// <param name="iocManager">IIocManager that is used to bootstrap the MAJID system</param>
        [Obsolete("Use overload with parameter type: Action<MajidBootstrapperOptions> optionsAction")]
        public static MajidBootstrapper Create<TStartupModule>([NotNull] IIocManager iocManager)
            where TStartupModule : MajidModule
        {
            return new MajidBootstrapper(typeof(TStartupModule), options =>
            {
                options.IocManager = iocManager;
            });
        }

        /// <summary>
        /// Creates a new <see cref="MajidBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="MajidModule"/>.</param>
        /// <param name="iocManager">IIocManager that is used to bootstrap the MAJID system</param>
        [Obsolete("Use overload with parameter type: Action<MajidBootstrapperOptions> optionsAction")]
        public static MajidBootstrapper Create([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            return new MajidBootstrapper(startupModule, options =>
            {
                options.IocManager = iocManager;
            });
        }

        private void AddInterceptorRegistrars()
        {
            ValidationInterceptorRegistrar.Initialize(IocManager);
            AuditingInterceptorRegistrar.Initialize(IocManager);
            EntityHistoryInterceptorRegistrar.Initialize(IocManager);
            UnitOfWorkRegistrar.Initialize(IocManager);
            AuthorizationInterceptorRegistrar.Initialize(IocManager);
        }

        /// <summary>
        /// Initializes the MAJID system.
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new MajidCoreInstaller());

                IocManager.Resolve<MajidPlugInManager>().PlugInSources.AddRange(PlugInSources);
                IocManager.Resolve<MajidStartupConfiguration>().Initialize();

                _moduleManager = IocManager.Resolve<MajidModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(MajidBootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<MajidBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<MajidBootstrapper>().Instance(this)
                    );
            }
        }

        /// <summary>
        /// Disposes the MAJID system.
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            _moduleManager?.ShutdownModules();
        }
    }
}
