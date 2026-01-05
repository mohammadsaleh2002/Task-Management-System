using System.Net;
using System.Text.Json;
using FluentValidation;

namespace TaskManagement.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = exception.Message;

        // Change Exeption
        if (exception is ValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            var errors = validationException.Errors.Select(e => e.ErrorMessage);
            result = JsonSerializer.Serialize(new { Errors = errors });
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}