using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JC.Core.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task MigrateDatabaseAsync<TContext>(this IServiceProvider services)
        where TContext : DbContext
    {
        await using var scope = services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        await context.Database.MigrateAsync();
    }
}