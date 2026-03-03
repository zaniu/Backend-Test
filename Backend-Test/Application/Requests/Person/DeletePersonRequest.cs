using MediatR;

namespace BackendTest.Application.Requests.Person;

public class DeletePersonRequest : IRequest<Unit>
{
    public int Id { get; set; }

    public DeletePersonRequest(int id)
    {
        Id = id;
    }
}
