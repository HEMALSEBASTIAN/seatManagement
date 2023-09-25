using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;

namespace SeatManagement.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("exception")]
    public class ErrorController : ControllerBase
    {
        public IActionResult exception()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            if(exception is NoDataException)
            {
                return NotFound(exception.Message);
            }
            else if(exception is ForeignKeyViolationException)
            {
                return BadRequest(exception.Message);
            }
            else if(exception is AllocationException)
            {
                return Conflict(exception.Message);
            }
            else if(exception is EmployeeAlreadyAllocatedException)
            {
                return Conflict(exception.Message);
            }
            return StatusCode(500, exception.Message);
        }
    }
}
