using Majid.Application.Editions;
using Majid.Application.Features;
using Majid.Authorization;
using Microsoft.AspNetCore.Identity;
using Majid.Authorization.Users;
using Majid.Authorization.Roles;
using Majid.MultiTenancy;

// ReSharper disable once CheckNamespace - This is done to add extension methods to Microsoft.Extensions.DependencyInjection namespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class MajidZeroIdentityBuilderExtensions
    {
        public static MajidIdentityBuilder AddMajidTenantManager<TTenantManager>(this MajidIdentityBuilder builder)
            where TTenantManager : class
        {
            var type = typeof(TTenantManager);
            var majidManagerType = typeof(MajidTenantManager<,>).MakeGenericType(builder.TenantType, builder.UserType);
            builder.Services.AddScoped(type, provider => provider.GetRequiredService(majidManagerType));
            builder.Services.AddScoped(majidManagerType, type);
            return builder;
        }

        public static MajidIdentityBuilder AddMajidEditionManager<TEditionManager>(this MajidIdentityBuilder builder)
            where TEditionManager : class
        {
            var type = typeof(TEditionManager);
            var majidManagerType = typeof(MajidEditionManager);
            builder.Services.AddScoped(type, provider => provider.GetRequiredService(majidManagerType));
            builder.Services.AddScoped(majidManagerType, type);
            return builder;
        }

        public static MajidIdentityBuilder AddMajidRoleManager<TRoleManager>(this MajidIdentityBuilder builder)
            where TRoleManager : class
        {
            var majidManagerType = typeof(MajidRoleManager<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var managerType = typeof(RoleManager<>).MakeGenericType(builder.RoleType);
            builder.Services.AddScoped(majidManagerType, services => services.GetRequiredService(managerType));
            builder.AddRoleManager<TRoleManager>();
            return builder;
        }

        public static MajidIdentityBuilder AddMajidUserManager<TUserManager>(this MajidIdentityBuilder builder)
            where TUserManager : class
        {
            var majidManagerType = typeof(MajidUserManager<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var managerType = typeof(UserManager<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(majidManagerType, services => services.GetRequiredService(managerType));
            builder.AddUserManager<TUserManager>();
            return builder;
        }

        public static MajidIdentityBuilder AddMajidSignInManager<TSignInManager>(this MajidIdentityBuilder builder)
            where TSignInManager : class
        {
            var majidManagerType = typeof(MajidSignInManager<,,>).MakeGenericType(builder.TenantType, builder.RoleType, builder.UserType);
            var managerType = typeof(SignInManager<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(majidManagerType, services => services.GetRequiredService(managerType));
            builder.AddSignInManager<TSignInManager>();
            return builder;
        }

        public static MajidIdentityBuilder AddMajidLogInManager<TLogInManager>(this MajidIdentityBuilder builder)
            where TLogInManager : class
        {
            var type = typeof(TLogInManager);
            var majidManagerType = typeof(MajidLogInManager<,,>).MakeGenericType(builder.TenantType, builder.RoleType, builder.UserType);
            builder.Services.AddScoped(type, provider => provider.GetService(majidManagerType));
            builder.Services.AddScoped(majidManagerType, type);
            return builder;
        }

        public static MajidIdentityBuilder AddMajidUserClaimsPrincipalFactory<TUserClaimsPrincipalFactory>(this MajidIdentityBuilder builder)
            where TUserClaimsPrincipalFactory : class
        {
            var type = typeof(TUserClaimsPrincipalFactory);
            builder.Services.AddScoped(typeof(UserClaimsPrincipalFactory<,>).MakeGenericType(builder.UserType, builder.RoleType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(MajidUserClaimsPrincipalFactory<,>).MakeGenericType(builder.UserType, builder.RoleType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(IUserClaimsPrincipalFactory<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(type);
            return builder;
        }

        public static MajidIdentityBuilder AddMajidSecurityStampValidator<TSecurityStampValidator>(this MajidIdentityBuilder builder)
            where TSecurityStampValidator : class, ISecurityStampValidator
        {
            var type = typeof(TSecurityStampValidator);
            builder.Services.AddScoped(typeof(SecurityStampValidator<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(MajidSecurityStampValidator<,,>).MakeGenericType(builder.TenantType, builder.RoleType, builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(ISecurityStampValidator), services => services.GetRequiredService(type));
            builder.Services.AddScoped(type);
            return builder;
        }

        public static MajidIdentityBuilder AddPermissionChecker<TPermissionChecker>(this MajidIdentityBuilder builder)
            where TPermissionChecker : class
        {
            var type = typeof(TPermissionChecker);
            var checkerType = typeof(PermissionChecker<,>).MakeGenericType(builder.RoleType, builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(checkerType, provider => provider.GetService(type));
            builder.Services.AddScoped(typeof(IPermissionChecker), provider => provider.GetService(type));
            return builder;
        }

        public static MajidIdentityBuilder AddMajidUserStore<TUserStore>(this MajidIdentityBuilder builder)
            where TUserStore : class
        {
            var type = typeof(TUserStore);
            var majidStoreType = typeof(MajidUserStore<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var storeType = typeof(IUserStore<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(majidStoreType, services => services.GetRequiredService(type));
            builder.Services.AddScoped(storeType, services => services.GetRequiredService(type));
            return builder;
        }

        public static MajidIdentityBuilder AddMajidRoleStore<TRoleStore>(this MajidIdentityBuilder builder)
            where TRoleStore : class
        {
            var type = typeof(TRoleStore);
            var majidStoreType = typeof(MajidRoleStore<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var storeType = typeof(IRoleStore<>).MakeGenericType(builder.RoleType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(majidStoreType, services => services.GetRequiredService(type));
            builder.Services.AddScoped(storeType, services => services.GetRequiredService(type));
            return builder;
        }

        public static MajidIdentityBuilder AddFeatureValueStore<TFeatureValueStore>(this MajidIdentityBuilder builder)
            where TFeatureValueStore : class
        {
            var type = typeof(TFeatureValueStore);
            var storeType = typeof(MajidFeatureValueStore<,>).MakeGenericType(builder.TenantType, builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(storeType, provider => provider.GetService(type));
            builder.Services.AddScoped(typeof(IFeatureValueStore), provider => provider.GetService(type));
            return builder;
        }
    }
}
