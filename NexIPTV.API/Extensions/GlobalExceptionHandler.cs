using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace NexIPTV.API.Extensions
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = context.Exception.Message,
                Details = context.Exception.InnerException?.Message
            };

            context.Result = new JsonResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
