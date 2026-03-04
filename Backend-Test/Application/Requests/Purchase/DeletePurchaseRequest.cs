using MediatR;

namespace BackendTest.Application.Requests.Purchase;

public record DeletePurchaseRequest(int Id) : IRequest<Unit>;
