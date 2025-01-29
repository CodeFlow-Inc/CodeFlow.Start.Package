using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CodeFlow.Start.Package.Config;

/// <summary>
/// Class for configuring the database.
/// </summary>
public static class SqlServerConfig
{
    /// <summary>
    /// Configures the database with the specified SQLServer connection string.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="sqlConnection">The SQLServer connection string.</param>
    public static void ConfigureDatabaseSqlServer<TContext>(this IServiceCollection services, string sqlConnection) where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
                    options.UseSqlServer(sqlConnection));
    }

    /// <summary>
    /// Updates the database migration if there are pending migrations.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void UpdateMigrationDatabase<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        // Register database migration
        using var scope = services.BuildServiceProvider().CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
