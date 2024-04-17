using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;

namespace Route.Talabat.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error (int code)
        {
            return code switch
            {
                400 => BadRequest(new ApiResponse(code)),
                401 => Unauthorized(new ApiResponse(code)),
                404 => NotFound(new ApiResponse(code)),
                _ => StatusCode(code),
            };
        }
    }
}
