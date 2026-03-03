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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetAllProductsResponse>))]
    public async Task<ActionResult<Response<GetAllProductsResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllProductsRequest());
        return Ok(new Response<GetAllProductsResponse>(response));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetProductByIdResponse>))]
    public async Task<ActionResult<Response<GetProductByIdResponse>>> GetById(int id)
    {
        var response = await _mediator.Send(new GetProductByIdRequest(id));
        return Ok(new Response<GetProductByIdResponse>(response));
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<AddProductResponse>))]
    public async Task<ActionResult<Response<AddProductResponse>>> Add([FromBody] AddProductRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, new Response<AddProductResponse>(response));
    }

    [HttpPut("{id}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Response<UpdateProductResponse>))]
    public async Task<ActionResult<Response<UpdateProductResponse>>> Update(int id, [FromBody] UpdateProductRequest request)
    {
        request.Id = id;
        var response = await _mediator.Send(request);
        return Accepted(new Response<UpdateProductResponse>(response));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductRequest(id));
        return NoContent();
    }
}
