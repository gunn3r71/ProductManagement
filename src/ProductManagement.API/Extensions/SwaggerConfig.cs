using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace ProductManagement.API.Extensions
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ProductManagement.API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Email = "lucas.p.oliveira@outlook.pt",
                        Name = "Lucas Pereira",
                        Url = new Uri("https://github.com/gunn3r71")
                    },
                    Description = "API criada para gerenciamento de produtos e fornecedores com base no projeto" +
                    $"do curso: REST com ASP.NET Core WebAPI no {new Uri("https://desenvolvedor.io/")}"
                });
            });

            return services;
        }
    }
}
