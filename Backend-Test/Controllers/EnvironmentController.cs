using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Backend_Test.Controllers
{
    [Route("environment")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        [HttpGet("isproduction")]
        public ActionResult<bool> GetIsProduction()
        {
            if (Debugger.IsAttached)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [HttpGet("apiversion")]
        public ActionResult<string> GetApiVersion()
        {
            // TODO: Change version when adding or updating core functionality
            return "Api Version is 2.3";
        }

        [HttpGet("uiversion")]
        public ActionResult<string> GetUIVersion()
        {
            // TODO: Change version when adding or updating core functionality in the web interface
            return "UI Version is 4.7";
        }
    }
}
