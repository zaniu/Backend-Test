using MediatR;

namespace BackendTest.Application.Requests.Purchase;

public record DeletePurchaseByCustomerRequest(int CustomerId) : IRequest<Unit>;
