using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Data.Context;

namespace ProductManagement.API.Extensions
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            // BUG -> DOMAIN EXCEPTION
            // services.AddHealthChecks()
            //         .AddMySql(connectionString, "MySql database health");
            
            return services;
        }
    }
}