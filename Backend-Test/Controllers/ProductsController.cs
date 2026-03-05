using BackendTest.Application.Features.Products.AddProduct;
using BackendTest.Application.Features.Products.DeleteProduct;
using BackendTest.Application.Features.Products.GetAllProducts;
using BackendTest.Application.Features.Products.GetProductById;
using BackendTest.Application.Features.Products.UpdateProduct;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionResponse<GetAllProductsResponse.ProductItem>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<CollectionResponse<GetAllProductsResponse.ProductItem>>> GetAll()
    {
            var response = await _mediator.Send(new GetAllProductsRequest());
            return Ok(new CollectionResponse<GetAllProductsResponse.ProductItem>(response.Products));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<GetProductByIdResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<GetProductByIdResponse>>> GetById(int id)
    {
        var response = await _mediator.Send(new GetProductByIdRequest(id));
        return Ok(new SingleItemResponse<GetProductByIdResponse>(response));
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SingleItemResponse<AddProductResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<AddProductResponse>>> Add([FromBody] AddProductRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, new SingleItemResponse<AddProductResponse>(response));
    }

    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<UpdateProductResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<UpdateProductResponse>>> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var command = request with { Id = id };
        var response = await _mediator.Send(command);
        return Ok(new SingleItemResponse<UpdateProductResponse>(response));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductRequest(id));
        return NoContent();
    }
}
