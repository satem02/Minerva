using Microsoft.AspNetCore.Mvc;
using Minerva.Shared.Contract;

namespace Minerva.API.Common
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        [NonAction]
        protected IActionResult Result(ResponseBase response)
        {
            return StatusCode(response.StatusCode, response);
        }
    }
}