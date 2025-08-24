using BLL.Services;
using DAL.Context;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// CONFIGURATION SETUP
// ============================================================================
builder.Configuration.AddUserSecrets<Program>();

// ============================================================================
// DATABASE CONFIGURATION
// ============================================================================
var databaseConfig = DatabaseConfig.FromConfiguration(builder.Configuration);
builder.Services.ResolveServices(builder.Configuration, databaseConfig.ConnectionString);
builder.Services.AddDatabaseServices(databaseConfig);

// ============================================================================
// AUTHENTICATION & AUTHORIZATION
// ============================================================================
ConfigureAuthentication(builder);
ConfigureGoogleAuthentication(builder);

// ============================================================================
// CORS POLICY
// ============================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ============================================================================
// SERVICE REGISTRATION
// ============================================================================
RegisterServices(builder.Services);

// ============================================================================
// CONTROLLERS & API EXPLORER
// ============================================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ============================================================================
// SWAGGER CONFIGURATION
// ============================================================================
ConfigureSwagger(builder.Services);

// ============================================================================
// APPLICATION BUILD & MIDDLEWARE PIPELINE
// ============================================================================
var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

// ============================================================================
// CONFIGURATION METHODS
// ============================================================================

static void ConfigureAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
        };
    });
}

static void ConfigureGoogleAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        options.CallbackPath = "/signin-google";
        options.Events.OnCreatingTicket = async context =>
        {
            var email = context.Principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = context.Principal.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var phoneNumber = context.Principal.FindFirst(System.Security.Claims.ClaimTypes.MobilePhone)?.Value;

            var db = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
            var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();

            var user = await db.User.GetAsync(u => u.Email == email);
            if (user == null)
            {
                user = new User
                {
                    UserId = Guid.NewGuid(),
                    Email = email,
                    FullName = name,
                    PhoneNumber = phoneNumber,
                    Role = (int)DAL.Models.Enum.Role.Customer
                };
                await db.User.AddAsync(user);
                await db.SaveChangesAsync();
            }

            var token = jwtService.GenerateJwtToken(user);
            context.Properties.RedirectUri = $"/auth/google-callback?token={token}";
        };
    });
}

static void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IJwtService, JwtService>();
    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
}

static void ConfigureSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "VNS Travel API",
            Version = "v1",
            Description = "A comprehensive API for VNS Travel platform including authentication, bookings, services, and more.",
            Contact = new OpenApiContact
            {
                Name = "VNS Travel Team",
                Email = "truonganhkietdev@gmail.com"
            }
        });

        // JWT Authentication configuration
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        // Include XML comments if available
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }
    });
}

static void ConfigureMiddleware(WebApplication app)
{
    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        // Enable Swagger in production for Azure deployment
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "VNS Travel API v1");
            c.RoutePrefix = "swagger";
        });
    }

    app.UseCors("AllowAll");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    // Default route redirect to Swagger
    app.MapGet("/", () => Results.Redirect("/swagger"));
}
