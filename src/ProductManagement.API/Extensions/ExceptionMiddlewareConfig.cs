using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProductManagement.API.DTOs.Output;

namespace ProductManagement.API.Extensions
{
    public static class ExceptionMiddlewareConfig
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionObject is not null)
                    {
                        var message = JsonConvert.SerializeObject(new CustomResponseOutput()
                        {
                            Success = false,
                            Message = "Algo deu errado ao processar sua solicitação =(",
                            Data = null
                        });

                        await context.Response.WriteAsync(message);
                    }
                });
            });

            return app;
        }
    }
}
