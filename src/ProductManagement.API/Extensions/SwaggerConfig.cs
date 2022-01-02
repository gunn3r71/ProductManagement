using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header utilizando o esquema bearer. \r\n" +
                                  "Insira 'Bearer' [espaço] e insira seu token depois.\r\n\r\n" +
                                  "Exemplo: \"Bearer 15asda1d5ad3as12dasd-f\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }                    
                });
            });

            return services;
        }
    }
}
