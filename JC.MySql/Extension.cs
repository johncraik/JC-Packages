using JC.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JC.MySql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        string migrationsAssembly,
        string connectionStringName = "DefaultConnection",
        Action<MySqlDbContextOptionsBuilder>? mySqlOptions = null)
    {
        services.AddMySqlDatabase<DataDbContext>(configuration, migrationsAssembly, connectionStringName, mySqlOptions);
        return services;
    }

    public static IServiceCollection AddMySqlDatabase<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string migrationsAssembly,
        string connectionStringName = "DefaultConnection",
        Action<MySqlDbContextOptionsBuilder>? mySqlOptions = null)
        where TContext : DbContext, IDataDbContext
    {
        var connectionString = configuration.GetConnectionString(connectionStringName)
            ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");

        services.AddDbContext<TContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysql =>
            {
                mysql.MigrationsAssembly(migrationsAssembly);
                mySqlOptions?.Invoke(mysql);
            }));

        return services;
    }
}