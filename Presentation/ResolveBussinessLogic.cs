using BLL;
using BLL.Services.Implementations;
using BLL.Services.Interfaces;
using DAL.Context;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    public static class ResolveBussinessLogic
    {
        public static IServiceCollection ResolveServices(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IBookingService, BookingService>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.CommandTimeout(30);
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            }));

            //services.AddAuthentication(o =>
            //{
            //    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            //    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            //    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie()
            //.AddGoogle(options =>
            //{
            //    options.ClientId = configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            //    options.CallbackPath = "/auth/signin-google";
            //});

            return services;
        }
    }
}
