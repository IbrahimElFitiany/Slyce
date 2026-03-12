using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Exceptions;
using Shared.Domain.Exceptions;

namespace WebAPI.Infrastructure.ExceptionHandling
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Instance = httpContext.Request.Path;

            if (exception is NotFoundException nf)
            {
                httpContext.Response.StatusCode = 404;
                problemDetails.Title = nf.Message;
            }
            else if (exception is DuplicateException due)
            {
                httpContext.Response.StatusCode = 409;
                problemDetails.Title = due.Message;
            }
            else if (exception is DomainException de)
            {
                httpContext.Response.StatusCode = 400;
                problemDetails.Title = de.Message;
            }
            else if (exception is ValidationException ve)
            {
                httpContext.Response.StatusCode = 400;
                problemDetails.Title = "Validation Failed";
                problemDetails.Extensions["errors"] = ve.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
            }
            else
            {
                httpContext.Response.StatusCode = 500;
                problemDetails.Title = "Internal Server Error";
            }
            logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

            problemDetails.Status = httpContext.Response.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
