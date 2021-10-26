using Microsoft.AspNetCore.Builder;
using ProductManagement.API.Middlewares;

namespace ProductManagement.API.Extensions
{
    public static class CustomMiddlewareCOnfig
    {
        public static IApplicationBuilder UseInternalServerErrorInterceptor(this IApplicationBuilder app)
        {
            app.UseMiddleware<InternalServerErrorInterceptorMiddleware>();

            return app;
        }
    }
}
