using System;
using Majid.Application.Editions;
using Majid.Application.Features;
using Majid.Authorization;
using Majid.Authorization.Roles;
using Majid.Authorization.Users;
using Majid.MultiTenancy;
using Majid.Zero.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace - This is done to add extension methods to Microsoft.Extensions.DependencyInjection namespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class MajidZeroServiceCollectionExtensions
    {
        public static MajidIdentityBuilder AddMajidIdentity<TTenant, TUser, TRole>(this IServiceCollection services)
            where TTenant : MajidTenant<TUser>
            where TRole : MajidRole<TUser>, new()
            where TUser : MajidUser<TUser>
        {
            return services.AddMajidIdentity<TTenant, TUser, TRole>(setupAction: null);
        }

        public static MajidIdentityBuilder AddMajidIdentity<TTenant, TUser, TRole>(this IServiceCollection services, Action<IdentityOptions> setupAction)
            where TTenant : MajidTenant<TUser>
            where TRole : MajidRole<TUser>, new()
            where TUser : MajidUser<TUser>
        {
            services.AddSingleton<IMajidZeroEntityTypes>(new MajidZeroEntityTypes
            {
                Tenant = typeof(TTenant),
                Role = typeof(TRole),
                User = typeof(TUser)
            });

            //MajidTenantManager
            services.TryAddScoped<MajidTenantManager<TTenant, TUser>>();

            //MajidEditionManager
            services.TryAddScoped<MajidEditionManager>();

            //MajidRoleManager
            services.TryAddScoped<MajidRoleManager<TRole, TUser>>();
            services.TryAddScoped(typeof(RoleManager<TRole>), provider => provider.GetService(typeof(MajidRoleManager<TRole, TUser>)));

            //MajidUserManager
            services.TryAddScoped<MajidUserManager<TRole, TUser>>();
            services.TryAddScoped(typeof(UserManager<TUser>), provider => provider.GetService(typeof(MajidUserManager<TRole, TUser>)));

            //SignInManager
            services.TryAddScoped<MajidSignInManager<TTenant, TRole, TUser>>();
            services.TryAddScoped(typeof(SignInManager<TUser>), provider => provider.GetService(typeof(MajidSignInManager<TTenant, TRole, TUser>)));

            //MajidLogInManager
            services.TryAddScoped<MajidLogInManager<TTenant, TRole, TUser>>();

            //MajidUserClaimsPrincipalFactory
            services.TryAddScoped<MajidUserClaimsPrincipalFactory<TUser, TRole>>();
            services.TryAddScoped(typeof(UserClaimsPrincipalFactory<TUser, TRole>), provider => provider.GetService(typeof(MajidUserClaimsPrincipalFactory<TUser, TRole>)));
            services.TryAddScoped(typeof(IUserClaimsPrincipalFactory<TUser>), provider => provider.GetService(typeof(MajidUserClaimsPrincipalFactory<TUser, TRole>)));

            //MajidSecurityStampValidator
            services.TryAddScoped<MajidSecurityStampValidator<TTenant, TRole, TUser>>();
            services.TryAddScoped(typeof(SecurityStampValidator<TUser>), provider => provider.GetService(typeof(MajidSecurityStampValidator<TTenant, TRole, TUser>)));
            services.TryAddScoped(typeof(ISecurityStampValidator), provider => provider.GetService(typeof(MajidSecurityStampValidator<TTenant, TRole, TUser>)));

            //PermissionChecker
            services.TryAddScoped<PermissionChecker<TRole, TUser>>();
            services.TryAddScoped(typeof(IPermissionChecker), provider => provider.GetService(typeof(PermissionChecker<TRole, TUser>)));

            //MajidUserStore
            services.TryAddScoped<MajidUserStore<TRole, TUser>>();
            services.TryAddScoped(typeof(IUserStore<TUser>), provider => provider.GetService(typeof(MajidUserStore<TRole, TUser>)));

            //MajidRoleStore
            services.TryAddScoped<MajidRoleStore<TRole, TUser>>();
            services.TryAddScoped(typeof(IRoleStore<TRole>), provider => provider.GetService(typeof(MajidRoleStore<TRole, TUser>)));

            //MajidFeatureValueStore
            services.TryAddScoped<MajidFeatureValueStore<TTenant, TUser>>();
            services.TryAddScoped(typeof(IFeatureValueStore), provider => provider.GetService(typeof(MajidFeatureValueStore<TTenant, TUser>)));

            return new MajidIdentityBuilder(services.AddIdentity<TUser, TRole>(setupAction), typeof(TTenant));
        }
    }
}
