using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using BackendTest.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionResponse<GetProductByIdResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<CollectionResponse<GetProductByIdResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllProductsRequest());
        return Ok(new CollectionResponse<GetProductByIdResponse>(response.Products));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<GetProductByIdResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<SingleItemResponse<GetProductByIdResponse>>> GetById(int id)
    {
        var response = await _mediator.Send(new GetProductByIdRequest(id));
        return Ok(new SingleItemResponse<GetProductByIdResponse>(response));
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SingleItemResponse<AddProductResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<SingleItemResponse<AddProductResponse>>> Add([FromBody] AddProductRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, new SingleItemResponse<AddProductResponse>(response));
    }

    [HttpPut("{id}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(SingleItemResponse<UpdateProductResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<SingleItemResponse<UpdateProductResponse>>> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var command = request with { Id = id };
        var response = await _mediator.Send(command);
        return Accepted(new SingleItemResponse<UpdateProductResponse>(response));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductRequest(id));
        return NoContent();
    }
}
