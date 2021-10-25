﻿using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Notifications;
using ProductManagement.Business.Services;
using ProductManagement.Data.Context;
using ProductManagement.Data.Repository;

namespace ProductManagement.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}