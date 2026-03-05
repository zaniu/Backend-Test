using MediatR;

namespace BackendTest.Application.Features.Persons.GetAllPersons;

public record GetAllPersonsRequest : IRequest<GetAllPersonsResponse>;
