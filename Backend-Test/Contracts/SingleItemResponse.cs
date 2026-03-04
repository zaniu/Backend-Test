#nullable enable

namespace BackendTest.Contracts;

public record SingleItemResponse<T>(bool IsSuccess, string? Message, T? Value, List<string>? Errors, string? TraceId)
    : ResponseBase(IsSuccess, Message, Errors, TraceId)
{
	public SingleItemResponse(T? value)
		: this(true, null, value, null, null)
	{
	}

	public SingleItemResponse(string message, List<string>? errors, string? traceId = null)
		: this(false, message, default, errors, traceId)
	{
	}
}