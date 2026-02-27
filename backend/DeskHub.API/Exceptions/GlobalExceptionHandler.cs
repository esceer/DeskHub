using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DeskHub.API.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case InvalidOperationException:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = exception.Message;

                _logger.LogWarning(
                    exception,
                    "Invalid operation occurred. Path: {Path}",
                    httpContext.Request.Path);
                break;

            default:
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Server error.";

                _logger.LogError(
                    exception,
                    "Unhandled exception occurred. Path: {Path}",
                    httpContext.Request.Path);
                break;
        }

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}