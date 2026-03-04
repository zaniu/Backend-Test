using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Responses.Purchase;
using BackendTest.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Controllers;

[Route("purchases")]
[ApiController]
public class PurchasesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PurchasesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionResponse<GetPurchaseByCustomerIdResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<CollectionResponse<GetPurchaseByCustomerIdResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllPurchasesRequest());
        return Ok(new CollectionResponse<GetPurchaseByCustomerIdResponse>(response.Purchases));
    }

    [HttpGet("customer/{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<GetPurchaseByCustomerIdResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<GetPurchaseByCustomerIdResponse>>> GetByCustomerId(int customerId)
    {
        var response = await _mediator.Send(new GetPurchaseByCustomerIdRequest(customerId));
        return Ok(new SingleItemResponse<GetPurchaseByCustomerIdResponse>(response));
    }

    /// <summary>
    /// Generates a CSV report of a purchase, including a list of purchased items, their prices, the total expenditure, and customer information.
    /// </summary>
    /// <param name="id">The id of the Purchase order</param>
    [HttpGet("{id}/report")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<byte[]>> GetPurchaseReportById(int id)
    {
        throw new NotImplementedException("Please implement me!");
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SingleItemResponse<AddPurchaseResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<AddPurchaseResponse>>> Add([FromBody] AddPurchaseRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetByCustomerId), new { customerId = response.CustomerId }, new SingleItemResponse<AddPurchaseResponse>(response));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePurchaseRequest(id));
        return NoContent();
    }

    [HttpDelete("customer/{customerId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult> DeleteFromCustomer(int customerId)
    {
        await _mediator.Send(new DeletePurchaseByCustomerRequest(customerId));
        return NoContent();
    }
}
