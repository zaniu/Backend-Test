using System.Diagnostics;
using MediatR;

namespace BackendTest.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private const long SlowRequestThresholdMs = 500;

    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Starting handler {HandlerName}", requestName);

        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await next();
        }
        finally
        {
            stopwatch.Stop();

            var executionTime = stopwatch.ElapsedMilliseconds;
            _logger.LogInformation("Handler {HandlerName} executed in {ExecutionTime}ms", requestName, executionTime);

            if (executionTime > SlowRequestThresholdMs)
            {
                _logger.LogWarning("Handler {HandlerName} execution exceeded {Threshold}ms: {ExecutionTime}ms", requestName, SlowRequestThresholdMs, executionTime);
            }
        }
    }
}