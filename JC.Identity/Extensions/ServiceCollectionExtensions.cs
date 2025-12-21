using JC.Core.Models;
using JC.Identity.Authentication;
using JC.Identity.Extensions.Options;
using JC.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JC.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity<TUser, TRole, TUserInfo>(
        this IServiceCollection services,
        Action<IdentityMiddlewareOptions>? configureMiddleware = null)
        where TUser : BaseUser
        where TRole : BaseRole
        where TUserInfo : class, IUserInfo
    {
        services.AddAuthorization();
        services.AddAuthentication();
        
        // Register IUserInfo as scoped (per-request)
        services.TryAddScoped<IUserInfo, TUserInfo>();

        // Configure middleware options
        if (configureMiddleware != null)
        {
            services.Configure(configureMiddleware);
        }
        else
        {
            services.Configure<IdentityMiddlewareOptions>(_ => { });
        }

        // Replace default claims principal factory with our custom one
        services.AddScoped<IUserClaimsPrincipalFactory<TUser>, DefaultClaimsPrincipalFactory<TUser, TRole>>();
        
        return services;
    }

    public static IServiceCollection AddIdentity<TUser, TRole>(
        this IServiceCollection services,
        Action<IdentityMiddlewareOptions>? configureMiddleware = null)
        where TUser : BaseUser
        where TRole : BaseRole
    {
        services.AddIdentity<TUser, TRole, UserInfo>();
        return services;
    }
    

    public static IdentityBuilder AddIdentity<TUser, TRole>(
        this IdentityBuilder builder,
        Action<IdentityMiddlewareOptions>? configureMiddleware = null)
        where TUser : BaseUser
        where TRole : BaseRole
    {
        builder.Services.AddIdentity<TUser, TRole>(configureMiddleware);
        return builder;
    }
}
