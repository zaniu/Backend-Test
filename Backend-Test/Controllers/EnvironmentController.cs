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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
    public ActionResult<Response<bool>> GetIsProduction() 
    {
        return Ok(new Response<bool>(_config.IsProduction));
    }

    [HttpGet("apiversion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    public ActionResult<Response<string>> GetApiVersion()
    {
        return Ok(new Response<string>(_config.ApiVersion));
    }

    [HttpGet("uiversion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    public ActionResult<Response<string>> GetUIVersion()
    {
        return Ok(new Response<string>(_config.UiVersion));
    }
}
