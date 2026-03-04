namespace BackendTest.Contracts;

public abstract record ResponseBase(bool IsSuccess, string Message, List<string> Errors, string TraceId);