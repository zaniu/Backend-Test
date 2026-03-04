using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Requests.Purchase;

public record GetPurchaseByCustomerIdRequest(int CustomerId) : IRequest<GetPurchaseByCustomerIdResponse>;
