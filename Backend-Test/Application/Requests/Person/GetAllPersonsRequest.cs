using MediatR;
using BackendTest.Application.Responses.Person;

namespace BackendTest.Application.Requests.Person;

public class GetAllPersonsRequest : IRequest<GetAllPersonsResponse>
{
}
