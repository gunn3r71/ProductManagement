using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.API.Data;

namespace ProductManagement.API.Extensions
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(x =>
                {
                    x.Password = new()
                    {
                        RequireDigit = true,
                        RequireLowercase = true,
                        RequireUppercase = true,
                        RequiredLength = 8,
                        RequireNonAlphanumeric = true
                    };
                    x.User = new()
                    {
                        RequireUniqueEmail = true,
                    };
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}