using JC.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JC.SqlServer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServerDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        string migrationsAssembly,
        string connectionStringName = "DefaultConnection",
        Action<SqlServerDbContextOptionsBuilder>? sqlServerOptions = null)
    {
        services.AddSqlServerDatabase<DataDbContext>(configuration, migrationsAssembly, connectionStringName, sqlServerOptions);
        return services;
    }

    public static IServiceCollection AddSqlServerDatabase<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string migrationsAssembly,
        string connectionStringName = "DefaultConnection",
        Action<SqlServerDbContextOptionsBuilder>? sqlServerOptions = null)
        where TContext : DbContext, IDataDbContext
    {
        var connectionString = configuration.GetConnectionString(connectionStringName)
            ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");

        services.AddDbContext<TContext>(options =>
            options.UseSqlServer(connectionString, sql =>
            {
                sql.MigrationsAssembly(migrationsAssembly);
                sqlServerOptions?.Invoke(sql);
            }));

        return services;
    }
}