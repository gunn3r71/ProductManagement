using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ProductManagement.API.Extensions
{
    public static class ApiConfig
    {
        public static IServiceCollection AddCustomApiConfiguration(this IServiceCollection services)
        {

            #region Api custom bad request response

            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.SuppressModelStateInvalidFilter = true;
            });
            
            #endregion
            
            #region Api Versioning

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;

            });

            #endregion

            #region Api Health Checks

            // BUG -> Domain exception
            // services.AddHealthChecks();
            // services.AddHealthChecksUI();
                
            #endregion

            return services;
        }

        public static IApplicationBuilder UseCustomApiConfiguration(this IApplicationBuilder app)
        {
            // BUG -> Domain exception
            // app.UseHealthChecks("/api/hc");
            // app.UseHealthChecksUI(o =>
            // {
            //     o.UIPath = "/api/hc-ui";
            // });
            //
            return app;
        }
    }
}