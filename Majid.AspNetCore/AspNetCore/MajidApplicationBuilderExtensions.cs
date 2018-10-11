﻿using System;
using System.Linq;
using Majid.AspNetCore.EmbeddedResources;
using Majid.AspNetCore.Localization;
using Majid.Dependency;
using Majid.Localization;
using Castle.LoggingFacility.MsLogging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Majid.AspNetCore.Security;
using Microsoft.AspNetCore.Hosting;

namespace Majid.AspNetCore
{
    public static class MajidApplicationBuilderExtensions
    {
        public static void UseMajid(this IApplicationBuilder app)
        {
            app.UseMajid(null);
        }

        public static void UseMajid([NotNull] this IApplicationBuilder app, Action<MajidApplicationBuilderOptions> optionsAction)
        {
            Check.NotNull(app, nameof(app));

            var options = new MajidApplicationBuilderOptions();
            optionsAction?.Invoke(options);

            if (options.UseCastleLoggerFactory)
            {
                app.UseCastleLoggerFactory();
            }

            InitializeMajid(app);

            if (options.UseMajidRequestLocalization)
            {
                //TODO: This should be added later than authorization middleware!
                app.UseMajidRequestLocalization();
            }

            if (options.UseSecurityHeaders)
            {
                app.UseMajidSecurityHeaders();
            }
        }

        public static void UseEmbeddedFiles(this IApplicationBuilder app)
        {
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new EmbeddedResourceFileProvider(
                        app.ApplicationServices.GetRequiredService<IIocResolver>()
                    )
                }
            );
        }

        private static void InitializeMajid(IApplicationBuilder app)
        {
            var majidBootstrapper = app.ApplicationServices.GetRequiredService<MajidBootstrapper>();
            majidBootstrapper.Initialize();

            var applicationLifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() => majidBootstrapper.Dispose());
        }

        public static void UseCastleLoggerFactory(this IApplicationBuilder app)
        {
            var castleLoggerFactory = app.ApplicationServices.GetService<Castle.Core.Logging.ILoggerFactory>();
            if (castleLoggerFactory == null)
            {
                return;
            }

            app.ApplicationServices
                .GetRequiredService<ILoggerFactory>()
                .AddCastleLogger(castleLoggerFactory);
        }

        public static void UseMajidRequestLocalization(this IApplicationBuilder app, Action<RequestLocalizationOptions> optionsAction = null)
        {
            var iocResolver = app.ApplicationServices.GetRequiredService<IIocResolver>();
            using (var languageManager = iocResolver.ResolveAsDisposable<ILanguageManager>())
            {
                var supportedCultures = languageManager.Object
                    .GetLanguages()
                    .Select(l => CultureInfo.GetCultureInfo(l.Name))
                    .ToArray();

                var options = new RequestLocalizationOptions
                {
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                };

                var userProvider = new MajidUserRequestCultureProvider();

                //0: QueryStringRequestCultureProvider
                options.RequestCultureProviders.Insert(1, userProvider);
                options.RequestCultureProviders.Insert(2, new MajidLocalizationHeaderRequestCultureProvider());
                //3: CookieRequestCultureProvider
                options.RequestCultureProviders.Insert(4, new MajidDefaultRequestCultureProvider());
                //5: AcceptLanguageHeaderRequestCultureProvider

                optionsAction?.Invoke(options);

                userProvider.CookieProvider = options.RequestCultureProviders.OfType<CookieRequestCultureProvider>().FirstOrDefault();
                userProvider.HeaderProvider = options.RequestCultureProviders.OfType<MajidLocalizationHeaderRequestCultureProvider>().FirstOrDefault();

                app.UseRequestLocalization(options);
            }
        }

        public static void UseMajidSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseMiddleware<MajidSecurityHeadersMiddleware>();
        }
    }
}
