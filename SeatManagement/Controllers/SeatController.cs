using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : Controller
    {
        private readonly ISeatService _repositary;

        public SeatController(ISeatService repositary)
        {
            _repositary=repositary;
        }
        [HttpGet] //Getting All Seats
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }

        [HttpGet("pagination")]
        public IActionResult GetByPages([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(_repositary.Get(pageNumber, pageSize));
        }

        [HttpPost] //Adding seat in bulk
        public IActionResult Post(List <SeatDTO> seatDTOList)
        {
            _repositary.AddSeat(seatDTOList);
            return Ok();
        }
        
        [HttpPatch] //ALocating and deallocating seats
        public IActionResult Allocation([FromQuery] string action, AllocateDTO seat)
        {
            try
            {
                Seat item;
                if (string.Equals(action, "allocate", StringComparison.OrdinalIgnoreCase))
                {
                    item = _repositary.AllocateSeat(seat);
                    return Ok("Seat Allocation successfull");
                }
                else if (string.Equals(action, "deallocate", StringComparison.OrdinalIgnoreCase))
                {
                    item = _repositary.DeAllocateSeat(seat);
                    return Ok("Seat UnAllocation successfull");
                }
                else
                    return BadRequest("Invalid action parameter");
            }
            catch(Exception ex)
            {
                if (ex is EmployeeAlreadyAllocatedException)
                    return Conflict(ex.Message);
                else if (ex is AllocationException)
                    return Conflict(ex.Message);
                else if (ex is NoDataException)
                    return NotFound(ex.Message);
                else 
                    return BadRequest(ex.Message);
            }
            
        }





        [Route("id")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var item = _repositary.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        //[Route("allocate")]
        //[HttpPatch]
        //public IActionResult Allocate(AllocateDTO seat)
        //{
        //    var item = _repositary.AllocateSeat(seat);
        //    if(item == null)
        //        return NotFound();
        //    return Ok("Success");
        //}

        //[Route("deallocate")]
        //[HttpPatch]
        //public IActionResult DeAllocate(AllocateDTO seat)
        //{
        //    var item = _repositary.DeAllocateSeat(seat);
        //    if (item == null)
        //        return NotFound();
        //    return Ok();
        //}

    }
}
