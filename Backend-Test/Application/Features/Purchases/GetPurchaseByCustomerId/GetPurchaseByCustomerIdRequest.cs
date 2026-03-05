using MediatR;

namespace BackendTest.Application.Features.Purchases.GetPurchaseByCustomerId;

public record GetPurchaseByCustomerIdRequest(int CustomerId) : IRequest<GetPurchaseByCustomerIdResponse>;
