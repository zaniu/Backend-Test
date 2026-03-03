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
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<Response<GetAllPersonsResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllPersonsRequest());
        return Ok(new Response<GetAllPersonsResponse>(response));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetPersonByIdResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<Response<GetPersonByIdResponse>>> GetById(int id)
    {
        var response = await _mediator.Send(new GetPersonByIdRequest(id));
        return Ok(new Response<GetPersonByIdResponse>(response));
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<AddPersonResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<Response<AddPersonResponse>>> Add([FromBody] AddPersonRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, new Response<AddPersonResponse>(response));
    }

    [HttpPut("{id}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Response<UpdatePersonResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<Response<UpdatePersonResponse>>> Update(int id, [FromBody] UpdatePersonRequest request)
    {
        request.Id = id;
        var response = await _mediator.Send(request);
        return Accepted(new Response<UpdatePersonResponse>(response));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePersonRequest(id));
        return NoContent();
    }
}
