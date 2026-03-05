using MediatR;

namespace BackendTest.Application.Features.Purchases.DeletePurchase;

public record DeletePurchaseRequest(int Id) : IRequest<Unit>;
