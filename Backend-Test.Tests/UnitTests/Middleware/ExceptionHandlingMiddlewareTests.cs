using System.Text;
using System.Text.Json;
using BackendTest.Exceptions;
using BackendTest.Middleware;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace BackendTest.Test.UnitTests.Middleware;

public class ExceptionHandlingMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_WhenNoException_ShouldCallNext()
    {
        // Arrange
        var wasCalled = false;
        var middleware = new ExceptionHandlingMiddleware(_ =>
        {
            wasCalled = true;
            return Task.CompletedTask;
        });

        var context = new DefaultHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        wasCalled.Should().BeTrue();
    }

    [Fact]
    public async Task InvokeAsync_WhenNotFoundException_ShouldReturn404WithEnvelope()
    {
        // Arrange & Act
        var context = await InvokeAndCaptureResponseAsync(new NotFoundException());

        // Assert
        context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        context.Response.ContentType.Should().Be("application/json");

        var body = await ReadResponseBodyAsync(context);
        using var json = JsonDocument.Parse(body);

        json.RootElement.GetProperty("IsSuccess").GetBoolean().Should().BeFalse();
        json.RootElement.GetProperty("Message").GetString().Should().Be("Item does not exist");
        json.RootElement.GetProperty("Errors")[0].GetString().Should().Be("Item does not exist");
        json.RootElement.GetProperty("TraceId").GetString().Should().Be("trace-404");
    }

    [Fact]
    public async Task InvokeAsync_WhenValidationException_ShouldReturn400WithValidationErrors()
    {
        // Arrange
        var failures = new List<ValidationFailure>
        {
            new(nameof(TestRequest.YearOfBirth), "Customer can not be born after current year")
        };

        // Act
        var context = await InvokeAndCaptureResponseAsync(new ValidationException(failures), "trace-400");

        // Assert
        context.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        var body = await ReadResponseBodyAsync(context);
        using var json = JsonDocument.Parse(body);

        json.RootElement.GetProperty("Message").GetString().Should().StartWith("Validation failed:");
        json.RootElement.GetProperty("Errors")[0].GetString().Should().Be("YearOfBirth: Customer can not be born after current year");
        json.RootElement.GetProperty("TraceId").GetString().Should().Be("trace-400");
    }

    [Fact]
    public async Task InvokeAsync_WhenUnhandledException_ShouldReturn500()
    {
        // Arrange & Act
        var context = await InvokeAndCaptureResponseAsync(new Exception("boom"), "trace-500");

        // Assert
        context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        var body = await ReadResponseBodyAsync(context);
        using var json = JsonDocument.Parse(body);

        json.RootElement.GetProperty("Errors")[0].GetString().Should().Be("boom");
        json.RootElement.GetProperty("TraceId").GetString().Should().Be("trace-500");
    }

    private static async Task<HttpContext> InvokeAndCaptureResponseAsync(Exception exception, string traceId = "trace-404")
    {
        var middleware = new ExceptionHandlingMiddleware(_ => throw exception);

        var context = new DefaultHttpContext();
        context.TraceIdentifier = traceId;
        context.Response.Body = new MemoryStream();

        await middleware.InvokeAsync(context);

        return context;
    }

    private static async Task<string> ReadResponseBodyAsync(HttpContext context)
    {
        context.Response.Body.Position = 0;
        using var reader = new StreamReader(context.Response.Body, Encoding.UTF8, leaveOpen: true);
        return await reader.ReadToEndAsync();
    }

    private sealed record TestRequest(int YearOfBirth);
}
