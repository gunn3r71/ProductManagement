using KissLog;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.API.Filters;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Notifications;
using ProductManagement.Business.Services;
using ProductManagement.Data.Context;
using ProductManagement.Data.Repository;

namespace ProductManagement.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AppDbContext>();

            services.AddDatabaseConfiguration(configuration);

            services.AddScoped<ILogger>((context) => Logger.Factory.Get());

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

            services.AddScoped<ApiLogFilter>();

            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddIdentityConfiguration(configuration);

            services.AddAutoMapper(typeof(Startup));


            return services;
        }
    }
}
