using Microsoft.AspNetCore.Identity;
using MiniBook.Identity.Data;
using MiniBook.Identity.Models;

namespace MiniBook.Identity.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<IdentityOptions>(options => { 
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<MiniBookDbContext>()
                            .AddDefaultTokenProviders();

            return services;
        }
    }
}
