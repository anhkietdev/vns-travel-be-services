using DAL.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Context
{
    public class DatabaseMigrationHelper
    {
        private readonly AppDbContext _context;
        private readonly DatabaseConfig _databaseConfig;
        private readonly ILogger<DatabaseMigrationHelper> _logger;

        public DatabaseMigrationHelper(AppDbContext context, DatabaseConfig databaseConfig, ILogger<DatabaseMigrationHelper> logger)
        {
            _context = context;
            _databaseConfig = databaseConfig;
            _logger = logger;
        }

        public async Task<bool> EnsureDatabaseCreatedAsync()
        {
            try
            {
                _logger.LogInformation($"Ensuring database exists for {_databaseConfig.DatabaseType}");
                
                var created = await _context.Database.EnsureCreatedAsync();
                
                if (created)
                {
                    _logger.LogInformation($"Database created successfully for {_databaseConfig.DatabaseType}");
                }
                else
                {
                    _logger.LogInformation($"Database already exists for {_databaseConfig.DatabaseType}");
                }
                
                return created;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating database for {_databaseConfig.DatabaseType}");
                throw;
            }
        }

        public async Task ApplyMigrationsAsync()
        {
            try
            {
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                
                if (pendingMigrations.Any())
                {
                    _logger.LogInformation($"Applying {pendingMigrations.Count()} pending migrations for {_databaseConfig.DatabaseType}");
                    await _context.Database.MigrateAsync();
                    _logger.LogInformation("Migrations applied successfully");
                }
                else
                {
                    _logger.LogInformation("No pending migrations");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error applying migrations for {_databaseConfig.DatabaseType}");
                throw;
            }
        }

        public async Task<string> GetDatabaseInfoAsync()
        {
            try
            {
                var providerName = _context.Database.ProviderName;
                var connectionString = _context.Database.GetConnectionString();
                
                return $"Provider: {providerName}, Connection: {connectionString}, Type: {_databaseConfig.DatabaseType}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting database info");
                return $"Error: {ex.Message}";
            }
        }

        public async Task<bool> CanConnectAsync()
        {
            try
            {
                return await _context.Database.CanConnectAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot connect to database: {_databaseConfig.DatabaseType}");
                return false;
            }
        }
    }
} 