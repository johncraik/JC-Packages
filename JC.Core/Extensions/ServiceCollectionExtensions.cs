using JC.Core.Data;
using JC.Core.Models;
using JC.Core.Models.Auditing;
using JC.Core.Services;
using JC.Core.Services.DataRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JC.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore<TContext>(this IServiceCollection services)
        where TContext : DbContext, IDataDbContext
    {
        services.TryAddScoped<BugReportService>();
        services.TryAddScoped<AuditService>();
        services.TryAddScoped<IDataDbContext>(sp => sp.GetRequiredService<TContext>());
        services.TryAddScoped<DbContext>(sp => sp.GetRequiredService<TContext>());
        services.TryAddScoped<IRepositoryManager, RepositoryManager>();

        services.RegisterRepositoryContexts(
            typeof(ReportedIssue),
            typeof(AuditModel));

        return services;
    }

    public static IServiceCollection RegisterRepositoryContext<T>(this IServiceCollection services)
        where T : class
        => services.RegisterRepositoryContexts(typeof(T));

    public static IServiceCollection RegisterRepositoryContexts(this IServiceCollection services, params Type[] types)
    {
        foreach (var type in types)
        {
            if (!type.IsClass)
                throw new ArgumentException($"Type {type.Name} must be a class.", nameof(types));

            var interfaceType = typeof(IRepositoryContext<>).MakeGenericType(type);
            var implementationType = typeof(RepositoryContext<>).MakeGenericType(type);

            services.TryAddScoped(interfaceType, implementationType);
        }

        return services;
    }
}