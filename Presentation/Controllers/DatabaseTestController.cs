using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseTestController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public DatabaseTestController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Test database connectivity using Entity Framework
        /// </summary>
        /// <returns>Database connection status and details</returns>
        [HttpGet("ef-test")]
        public async Task<IActionResult> TestEntityFrameworkConnection()
        {
            try
            {
                var details = new List<string>();
                var connectionString = GetMaskedConnectionString();
                var status = "Testing...";

                // Test 1: Check if we can connect
                details.Add("Testing basic connection...");
                var canConnect = await _context.Database.CanConnectAsync();
                
                if (canConnect)
                {
                    status = "SUCCESS";
                    details.Add("✅ Database connection successful");
                }
                else
                {
                    status = "FAILED";
                    details.Add("❌ Database connection failed");
                    return BadRequest(new
                    {
                        Timestamp = DateTime.UtcNow,
                        TestType = "Entity Framework Connection",
                        Status = status,
                        ConnectionString = connectionString,
                        Details = details
                    });
                }

                // Test 2: Check database provider
                details.Add($"Database Provider: {_context.Database.ProviderName}");

                // Test 3: Check if database exists
                details.Add("Testing if database exists...");
                var dbExists = await _context.Database.EnsureCreatedAsync();
                details.Add($"Database exists: {!dbExists}");

                // Test 4: Test a simple query
                details.Add("Testing simple query...");
                var userCount = await _context.Users.CountAsync();
                details.Add($"Users in database: {userCount}");

                // Test 5: Check connection string details
                var actualConnectionString = _configuration.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrEmpty(actualConnectionString))
                {
                    details.Add("✅ Connection string found in configuration");
                    
                    // Check for required Azure SQL parameters
                    if (actualConnectionString.Contains("Encrypt=True"))
                        details.Add("✅ Encrypt=True parameter found");
                    else
                        details.Add("⚠️ Encrypt=True parameter missing (required for Azure SQL)");
                    
                    if (actualConnectionString.Contains("TrustServerCertificate=False"))
                        details.Add("✅ TrustServerCertificate=False parameter found");
                    else
                        details.Add("⚠️ TrustServerCertificate=False parameter missing");
                }
                else
                {
                    details.Add("❌ Connection string not found in configuration");
                }

                return Ok(new
                {
                    Timestamp = DateTime.UtcNow,
                    TestType = "Entity Framework Connection",
                    Status = status,
                    ConnectionString = connectionString,
                    Details = details
                });
            }
            catch (Exception ex)
            {
                var errorResult = new
                {
                    Timestamp = DateTime.UtcNow,
                    TestType = "Entity Framework Connection",
                    Status = "ERROR",
                    ConnectionString = GetMaskedConnectionString(),
                    Error = new
                    {
                        Message = ex.Message,
                        Type = ex.GetType().Name,
                        StackTrace = ex.StackTrace
                    },
                    Details = new List<string>
                    {
                        "❌ Database connection test failed",
                        $"Error Type: {ex.GetType().Name}",
                        $"Error Message: {ex.Message}"
                    }
                };

                return StatusCode(500, errorResult);
            }
        }

        /// <summary>
        /// Test database connectivity using raw SQL connection
        /// </summary>
        /// <returns>Raw SQL connection test results</returns>
        [HttpGet("sql-test")]
        public IActionResult TestSqlConnection()
        {
            try
            {
                var details = new List<string>();
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                var maskedConnectionString = GetMaskedConnectionString();
                var status = "Testing...";

                if (string.IsNullOrEmpty(connectionString))
                {
                    status = "FAILED";
                    details.Add("❌ Connection string not found in configuration");
                    return BadRequest(new
                    {
                        Timestamp = DateTime.UtcNow,
                        TestType = "Raw SQL Connection",
                        Status = status,
                        ConnectionString = maskedConnectionString,
                        Details = details
                    });
                }

                details.Add("Testing raw SQL connection...");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    details.Add("✅ SQL connection opened successfully");

                    // Test server info
                    details.Add($"Server Version: {connection.ServerVersion}");
                    details.Add($"Database: {connection.Database}");
                    details.Add($"Server: {connection.DataSource}");

                    // Test a simple query
                    using (var command = new SqlCommand("SELECT COUNT(*) FROM Users", connection))
                    {
                        var count = command.ExecuteScalar();
                        details.Add($"Users count via SQL: {count}");
                    }

                    connection.Close();
                    details.Add("✅ SQL connection closed successfully");
                }

                status = "SUCCESS";
                return Ok(new
                {
                    Timestamp = DateTime.UtcNow,
                    TestType = "Raw SQL Connection",
                    Status = status,
                    ConnectionString = maskedConnectionString,
                    Details = details
                });
            }
            catch (Exception ex)
            {
                var errorResult = new
                {
                    Timestamp = DateTime.UtcNow,
                    TestType = "Raw SQL Connection",
                    Status = "ERROR",
                    ConnectionString = GetMaskedConnectionString(),
                    Error = new
                    {
                        Message = ex.Message,
                        Type = ex.GetType().Name,
                        StackTrace = ex.StackTrace
                    },
                    Details = new List<string>
                    {
                        "❌ Raw SQL connection test failed",
                        $"Error Type: {ex.GetType().Name}",
                        $"Error Message: {ex.Message}"
                    }
                };

                return StatusCode(500, errorResult);
            }
        }

        /// <summary>
        /// Get database configuration details (without sensitive data)
        /// </summary>
        /// <returns>Configuration information</returns>
        [HttpGet("config")]
        public IActionResult GetDatabaseConfig()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            var result = new
            {
                Timestamp = DateTime.UtcNow,
                HasConnectionString = !string.IsNullOrEmpty(connectionString),
                ConnectionStringLength = connectionString?.Length ?? 0,
                MaskedConnectionString = GetMaskedConnectionString(),
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                ConfigurationSources = new List<string>
                {
                    "Azure App Service Settings",
                    "Environment Variables", 
                    "User Secrets (Development)",
                    "appsettings.Production.json",
                    "appsettings.json"
                },
                RequiredAzureSqlParameters = new List<string>
                {
                    "Encrypt=True",
                    "TrustServerCertificate=False", 
                    "Connection Timeout=30"
                }
            };

            return Ok(result);
        }

        /// <summary>
        /// Test specific database operations
        /// </summary>
        /// <returns>Database operation test results</returns>
        [HttpGet("operations")]
        public async Task<IActionResult> TestDatabaseOperations()
        {
            try
            {
                var details = new List<string>();

                // Test 1: Check if Users table exists
                details.Add("Testing Users table...");
                var userCount = await _context.Users.CountAsync();
                details.Add($"✅ Users table accessible, count: {userCount}");

                // Test 2: Check if Services table exists
                details.Add("Testing Services table...");
                var serviceCount = await _context.Services.CountAsync();
                details.Add($"✅ Services table accessible, count: {serviceCount}");

                // Test 3: Check if Bookings table exists
                details.Add("Testing Bookings table...");
                var bookingCount = await _context.Bookings.CountAsync();
                details.Add($"✅ Bookings table accessible, count: {bookingCount}");

                // Test 4: Check database schema
                details.Add("Testing database schema...");
                var tables = await _context.Database.SqlQueryRaw<string>(
                    "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
                ).ToListAsync();
                details.Add($"✅ Database schema accessible, tables found: {tables.Count}");

                return Ok(new
                {
                    Timestamp = DateTime.UtcNow,
                    TestType = "Database Operations",
                    Status = "SUCCESS",
                    Details = details
                });
            }
            catch (Exception ex)
            {
                var errorResult = new
                {
                    Timestamp = DateTime.UtcNow,
                    TestType = "Database Operations",
                    Status = "ERROR",
                    Error = new
                    {
                        Message = ex.Message,
                        Type = ex.GetType().Name
                    },
                    Details = new List<string>
                    {
                        "❌ Database operations test failed",
                        $"Error Type: {ex.GetType().Name}",
                        $"Error Message: {ex.Message}"
                    }
                };

                return StatusCode(500, errorResult);
            }
        }

        private string GetMaskedConnectionString()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                return "Connection string not found";

            // Mask sensitive parts of connection string
            var masked = connectionString
                .Replace("Password=", "Password=***")
                .Replace("User Id=", "User Id=***")
                .Replace("User ID=", "User ID=***");

            return masked;
        }
    }
}
