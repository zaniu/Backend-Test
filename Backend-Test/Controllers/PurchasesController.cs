using BackendTest.Application.Features.Purchases.AddPurchase;
using BackendTest.Application.Features.Purchases.DeletePurchase;
using BackendTest.Application.Features.Purchases.DeletePurchaseByCustomer;
using BackendTest.Application.Features.Purchases.DownloadPurchaseReport;
using BackendTest.Application.Features.Purchases.GetAllPurchases;
using BackendTest.Application.Features.Purchases.GetPurchaseByCustomerId;
using BackendTest.Application.Features.Purchases.GetPurchaseReportById;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllPurchasesResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<GetAllPurchasesResponse>> GetAll()
    {
        var response = await _mediator.Send(new GetAllPurchasesRequest());
        return Ok(response);
    }

    [HttpGet("customer/{customerId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<GetPurchaseByCustomerIdResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<SingleItemResponse<GetPurchaseByCustomerIdResponse>>> GetByCustomerId(int customerId)
    {
        var response = await _mediator.Send(new GetPurchaseByCustomerIdRequest(customerId));
        return Ok(new SingleItemResponse<GetPurchaseByCustomerIdResponse>(response));
    }

    [HttpPost("{purchaseId:int}/report")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult> GetPurchaseReportById(int purchaseId)
    {
        var response = await _mediator.Send(new GetPurchaseReportByIdRequest(purchaseId));
        if (!string.IsNullOrWhiteSpace(response.DownloadUrl))
        {
            return Redirect(response.DownloadUrl);
        }

        return RedirectToAction(nameof(DownloadReportById), new { purchaseId = response.PurchaseId, reportId = response.ReportId });
    }

    [HttpGet("{purchaseId:int}/reports/{reportId:guid}")]
    [Produces("text/csv")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult<byte[]>> DownloadReportById(int purchaseId, Guid reportId)
    {
        var request = new DownloadPurchaseReportRequest(purchaseId, reportId);
        var report = await _mediator.Send(request);
        return File(report.Content, report.ContentType, report.FileName);
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

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePurchaseRequest(id));
        return NoContent();
    }

    [HttpDelete("customer/{customerId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SingleItemResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public async Task<ActionResult> DeleteFromCustomer(int customerId)
    {
        await _mediator.Send(new DeletePurchaseByCustomerRequest(customerId));
        return NoContent();
    }
}
