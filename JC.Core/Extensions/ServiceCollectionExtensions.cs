using JC.Core.Data;
using JC.Core.Service;
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
        services.TryAddScoped<IDataDbContext>(sp => sp.GetRequiredService<TContext>());
        
        return services;
    }
}