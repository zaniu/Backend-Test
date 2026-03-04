using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Requests.Purchase;

public record AddPurchaseRequest(int Id, int CustomerId, List<int> ProductsIds) : IRequest<AddPurchaseResponse>;
