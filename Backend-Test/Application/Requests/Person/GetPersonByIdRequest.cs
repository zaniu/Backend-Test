using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Requests.Person;

public class GetPersonByIdRequest : IRequest<GetPersonByIdResponse>
{
    public int Id { get; set; }

    public GetPersonByIdRequest(int id)
    {
        Id = id;
    }
}
