using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ProductManagement.API.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Executing {context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode is StatusCodes.Status500InternalServerError)
                _logger.LogError($"{context.Exception.Message}");
            _logger.LogInformation($"Executed {context.ActionDescriptor.DisplayName}");
        }
    }
}
