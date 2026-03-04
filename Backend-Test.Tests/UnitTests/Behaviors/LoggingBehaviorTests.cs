using BackendTest.Application.Behaviors;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendTest.Test.UnitTests.Behaviors;

public class LoggingBehaviorTests
{
    [Fact]
    public async Task Handle_ShouldLogStartAndExecutionInfo()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<LoggingBehavior<TestRequest, string>>>();
        var behavior = new LoggingBehavior<TestRequest, string>(loggerMock.Object);

        // Act
        var response = await behavior.Handle(
            new TestRequest("sample"),
            () => Task.FromResult("done"),
            CancellationToken.None);

        // Assert
        response.Should().Be("done");

        VerifyLog(loggerMock, LogLevel.Information, "Starting handler TestRequest", Times.Once());
        VerifyLog(loggerMock, LogLevel.Information, "Handler TestRequest executed in", Times.Once());
    }

    [Fact]
    public async Task Handle_WithSlowExecution_ShouldLogWarning()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<LoggingBehavior<TestRequest, string>>>();
        var behavior = new LoggingBehavior<TestRequest, string>(loggerMock.Object);

        // Act
        await behavior.Handle(
            new TestRequest("slow"),
            async () =>
            {
                await Task.Delay(550);
                return "done";
            },
            CancellationToken.None);

        // Assert
        VerifyLog(loggerMock, LogLevel.Warning, "execution exceeded 500ms", Times.Once());
    }

    public sealed record TestRequest(string Name) : IRequest<string>;

    private static void VerifyLog<T>(
        Mock<ILogger<T>> loggerMock,
        LogLevel level,
        string expectedMessagePart,
        Times times)
    {
        loggerMock.Verify(
            logger => logger.Log(
                level,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, _) => state.ToString()!.Contains(expectedMessagePart)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            times);
    }
}
