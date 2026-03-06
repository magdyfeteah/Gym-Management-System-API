using System.ComponentModel.DataAnnotations;
using GymManagementSystem.Enums;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Exceptions
{
    public class GlobalExceptionHandler(IProblemDetailsService problemDetails) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var statusCode = exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                InvalidOperationException => StatusCodes.Status400BadRequest,
                NotImplementedException => StatusCodes.Status406NotAcceptable,
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.StatusCode = statusCode;
            return await problemDetails.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext ,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "Error has occurred!!",
                    Detail = exception.Message,
                    Status = statusCode,
                    Instance = httpContext.Request.Path
                }
            });
        }
    }
}