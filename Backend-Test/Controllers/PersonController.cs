using BackendTest.Application.Requests.Person;
using BackendTest.Application.Responses.Person;
using BackendTest.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Controllers;

[ApiController]
[Route("persons")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetAllPersonsResponse>))]
    public async Task<ActionResult<Response<GetAllPersonsResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllPersonsRequest());
        return Ok(new Response<GetAllPersonsResponse>(response));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetPersonByIdResponse>))]
    public async Task<ActionResult<Response<GetPersonByIdResponse>>> GetById(int id)
    {
        var response = await _mediator.Send(new GetPersonByIdRequest(id));
        return Ok(new Response<GetPersonByIdResponse>(response));
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<AddPersonResponse>))]
    public async Task<ActionResult<Response<AddPersonResponse>>> Add([FromBody] AddPersonRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, new Response<AddPersonResponse>(response));
    }

    [HttpPut("{id}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Response<UpdatePersonResponse>))]
    public async Task<ActionResult<Response<UpdatePersonResponse>>> Update(int id, [FromBody] UpdatePersonRequest request)
    {
        request.Id = id;
        var response = await _mediator.Send(request);
        return Accepted(new Response<UpdatePersonResponse>(response));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePersonRequest(id));
        return NoContent();
    }
}
