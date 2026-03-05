namespace BackendTest.Contracts;

public record CollectionResponse<T>(bool IsSuccess, string Message, IReadOnlyCollection<T> Value, List<string> Errors, string TraceId)
    : ResponseBase(IsSuccess, Message, Errors, TraceId)
{
    public CollectionResponse(IReadOnlyCollection<T> value)
        : this(true, null, value, null, null)
    {
    }
}
