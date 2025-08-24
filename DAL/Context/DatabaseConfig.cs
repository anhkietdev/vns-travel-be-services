using DAL.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Context
{
    public class DatabaseConfig
    {
        public DatabaseType DatabaseType { get; set; }
        public string ConnectionString { get; set; } = string.Empty;

        public void ConfigureDbContext(DbContextOptionsBuilder options)
        {
            switch (DatabaseType)
            {
                case DatabaseType.SqlServer:
                    options.UseSqlServer(ConnectionString);
                    break;
                case DatabaseType.Sqlite:
                    options.UseSqlite(ConnectionString);
                    break;
                default:
                    throw new ArgumentException($"Unsupported database type: {DatabaseType}");
            }
        }

        public static DatabaseConfig FromConfiguration(IConfiguration configuration)
        {            
            var databaseType = configuration.GetValue<string>("Database:Type")?.ToLower();
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                  configuration.GetConnectionString("SqliteConnection") ??
                                  configuration.GetConnectionString("SqlServerConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("No connection string found in configuration");
            }

            return new DatabaseConfig
            {
                DatabaseType = databaseType switch
                {
                    "sqlite" => DatabaseType.Sqlite,
                    "sqlserver" => DatabaseType.SqlServer,
                    _ => throw new ArgumentException($"Unsupported database type: {databaseType}")
                },
                ConnectionString = connectionString
            };
        }
    }
} 