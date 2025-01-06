using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace PersonalScheduling.BuildingBlock.Application.Middleware;

public class GlobalException : IMiddleware
{
    private readonly ILogger<GlobalException> _logger;

    public GlobalException(ILogger<GlobalException> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {

            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError;
        var methodError = ex.TargetSite?.DeclaringType?.FullName;
        var errorResponse = new ErrorResponse()
        {
            StatusCode = statusCode,
            Message = ex.GetType().ToString(),
            //Location = (methodError != null ? "Class: " + methodError + ", " : "") + "Method: " + ex.TargetSite?.Name,
            Detail = ex.Message,
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        if (ex is FluentValidation.ValidationException validationException)
        {
            var validationErrors = validationException.Errors.Select(error => new
            {
                error.PropertyName,
                error.ErrorMessage
            }).ToList();

            var validationErrorResponse = new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Validation failed",
                Errors = validationErrors
            };

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
        }


        return context.Response.WriteAsync(errorResponse.ToString());
    }
}

