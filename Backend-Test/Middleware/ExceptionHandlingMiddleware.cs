using System.Net;
using System.Text.Json;
using BackendTest.Contracts;
using BackendTest.Exceptions;
using FluentValidation;

namespace BackendTest.Middleware;

public class ExceptionHandlingMiddleware
{
    private const string GenericUnexpectedErrorMessage = "An unexpected error occurred.";
    private const string GenericClientErrorMessage = "The request is invalid.";

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            DuplicateException => HttpStatusCode.Conflict,
            DomainModelException => HttpStatusCode.BadRequest,
            ValidationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var isUnhandledError = statusCode == HttpStatusCode.InternalServerError;
        var responseMessage = CreateResponseMessage(exception, isUnhandledError);

        if (isUnhandledError)
        {
            _logger.LogError(exception, "Unhandled exception. TraceId: {TraceId}", context.TraceIdentifier);
        }

        var errors = CreateErrors(exception, responseMessage);

        var response = new SingleItemResponse<object>(responseMessage, errors, context.TraceIdentifier);

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }

    private static string CreateResponseMessage(Exception exception, bool isUnhandledError)
    {
        if (isUnhandledError)
        {
            return GenericUnexpectedErrorMessage;
        }

        var message = exception.Message?.Trim();
        return string.IsNullOrWhiteSpace(message) ? GenericClientErrorMessage : message;
    }

    private static List<string> CreateErrors(Exception exception, string responseMessage)
    {
        if (exception is not ValidationException validationException)
        {
            return [responseMessage];
        }

        return validationException.Errors
            .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
            .ToList();
    }

}
