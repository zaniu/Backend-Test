using MediatR;

namespace BackendTest.Application.Features.Purchases.GetAllPurchases;

public record GetAllPurchasesRequest : IRequest<GetAllPurchasesResponse>;
