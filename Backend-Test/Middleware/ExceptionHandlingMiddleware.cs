using System.Net;
using System.Text.Json;
using BackendTest.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            DuplicateException => HttpStatusCode.Conflict,
            DomainModelException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var errors = new Dictionary<string, string[]>
        {
            ["Error"] = [exception.Message]
        };

        var response = new ValidationProblemDetails(errors)
        {
            Type = GetTypeUri(statusCode),
            Title = "One or more validation errors occurred.",
            Status = (int)statusCode
        };
        response.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }

    private static string GetTypeUri(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            HttpStatusCode.NotFound => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            HttpStatusCode.Conflict => "https://tools.ietf.org/html/rfc9110#section-15.5.10",
            _ => "https://tools.ietf.org/html/rfc9110#section-15.6.1"
        };
    }
}
