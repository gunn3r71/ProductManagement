using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ProductManagement.API.Filters
{
    public class ApiLogFilter : IActionFilter
    {
        private readonly ILogger<ApiLogFilter> _logger;

        public ApiLogFilter(ILogger<ApiLogFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not null)
            {
                _logger.LogError($"StackTrace: {context.Exception.StackTrace} \n" +
                                 $"Error Message: {context.Exception.Message} \n" +
                                 $"Inner Exception: {context.Exception.InnerException}");
            }
        }
    }
}
