using BackendTest.Application.Requests.Person;
using BackendTest.Application.Responses.Person;
using BackendTest.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Controllers;

[ApiController]
[Route("persons")]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionResponse<GetPersonByIdResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<CollectionResponse<GetPersonByIdResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllPersonsRequest());
        return Ok(new CollectionResponse<GetPersonByIdResponse>(response.Persons));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<GetPersonByIdResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<GetPersonByIdResponse>>> GetById(int id)
    {
        var response = await _mediator.Send(new GetPersonByIdRequest(id));
        return Ok(new SingleItemResponse<GetPersonByIdResponse>(response));
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SingleItemResponse<AddPersonResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<AddPersonResponse>>> Add([FromBody] AddPersonRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, new SingleItemResponse<AddPersonResponse>(response));
    }

    [HttpPut("{id}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(SingleItemResponse<UpdatePersonResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<UpdatePersonResponse>>> Update(int id, [FromBody] UpdatePersonRequest request)
    {
        var command = request with { Id = id };
        var response = await _mediator.Send(command);
        return Accepted(new SingleItemResponse<UpdatePersonResponse>(response));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePersonRequest(id));
        return NoContent();
    }
}
