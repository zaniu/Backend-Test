using BackendTest.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendTest.Controllers;

[Route("environment")]
[ApiController]
public class EnvironmentController : ControllerBase
{
    private readonly Model.Environment _config;

    public EnvironmentController(IOptions<Model.Environment> options)
    {
        _config = options.Value;
    }

    [HttpGet("production")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<bool>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public ActionResult<SingleItemResponse<bool>> GetIsProduction()
    {
        return Ok(new SingleItemResponse<bool>(_config.IsProduction));
    }

    [HttpGet("apiversion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public ActionResult<SingleItemResponse<string>> GetApiVersion()
    {
        return Ok(new SingleItemResponse<string>(_config.ApiVersion));
    }

    [HttpGet("uiversion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleItemResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SingleItemResponse<object>))]
    public ActionResult<SingleItemResponse<string>> GetUIVersion()
    {
        return Ok(new SingleItemResponse<string>(_config.UiVersion));
    }
}
