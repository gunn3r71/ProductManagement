using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProductManagement.API.DTOs.Output;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.API.Middlewares
{
    public class InternalServerErrorInterceptorMiddleware
    {
        private readonly RequestDelegate _next;

        public InternalServerErrorInterceptorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Response.StatusCode is StatusCodes.Status500InternalServerError)
            {
                await context.Response.WriteAsJsonAsync(GenerateGenericError());

                await _next(context);
            }
        }

        private ErrorOutput GenerateGenericError()
        {
            var content = new ErrorOutput
            {
                Errors = new List<string>
                {
                    "Ocorreu um erro ao tentar processar sua solicitação."
                }
            };

            return content;
        }
    }
}
