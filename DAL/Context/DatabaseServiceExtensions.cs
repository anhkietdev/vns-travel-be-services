using DAL.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DAL.Context
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, DatabaseConfig databaseConfig)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                databaseConfig.ConfigureDbContext(options);
            }, ServiceLifetime.Scoped);

            services.AddSingleton(databaseConfig);
            
            return services;
        }

        public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
            var databaseConfig = scope.ServiceProvider.GetRequiredService<DatabaseConfig>();

            try
            {
                logger.LogInformation($"Initializing database: {databaseConfig.DatabaseType}");
                
                // Ensure database is created
                await context.Database.EnsureCreatedAsync();
                
                // Apply migrations if they exist
                if (context.Database.GetPendingMigrations().Any())
                {
                    await context.Database.MigrateAsync();
                    logger.LogInformation("Database migrations applied successfully");
                }
                else
                {
                    logger.LogInformation("Database is up to date");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error initializing database");
                throw;
            }
        }

        public static string GetDatabaseInfo(this DatabaseConfig databaseConfig)
        {
            return $"Database Type: {databaseConfig.DatabaseType}, Connection: {databaseConfig.ConnectionString}";
        }
    }
} 