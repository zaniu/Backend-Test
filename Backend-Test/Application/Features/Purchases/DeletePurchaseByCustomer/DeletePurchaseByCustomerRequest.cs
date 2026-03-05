using MediatR;

namespace BackendTest.Application.Features.Purchases.DeletePurchaseByCustomer;

public record DeletePurchaseByCustomerRequest(int CustomerId) : IRequest<Unit>;
