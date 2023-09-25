using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;
using System.Security.Cryptography;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : Controller
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService)
        {
            _seatService=seatService;
        }
        [HttpGet] //Getting All Seats
        public IActionResult Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            return Ok(_seatService.Get(pageNumber, pageSize));
        }

        [HttpPost] //Adding seat in bulk
        public IActionResult Post(SeatDTO seatDTO)
        {
            _seatService.AddSeat(seatDTO);
            return Ok($"{seatDTO.Capacity} seats added succcessfully");
        }

        [HttpGet("report")]
        public IActionResult Report(string? type, int? facilityId, int? floorNo)
        {
            return Ok(_seatService.ReportGenarator(type, facilityId, floorNo));
        }
        
        [HttpPatch("{seatId}")] //For allocating or deallocating seat
        public IActionResult Allocate(int seatId, int? employeeId)
        {
            if(employeeId.HasValue)
            {
                _seatService.AllocateSeat(seatId, employeeId.Value);
            }
            else
            {
                _seatService.DeAllocateSeat(seatId);
            }
            return Ok();
        }

        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _seatService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
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

//[HttpGet("pagination")]
//public IActionResult GetByPages([FromQuery] int pageNumber, [FromQuery] int pageSize)
//{
//    return Ok(_repositary.Get(pageNumber, pageSize));
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

//[HttpPatch] //ALocating and deallocating seats
//public IActionResult Allocation([FromQuery] string action, AllocateDTO seat)
//{
//    try
//    {
//        Seat item;
//        if (string.Equals(action, "allocate", StringComparison.OrdinalIgnoreCase))
//        {
//            item = _repositary.AllocateSeat(seat);
//            return Ok("Seat Allocation successfull");
//        }
//        else if (string.Equals(action, "deallocate", StringComparison.OrdinalIgnoreCase))
//        {
//            item = _repositary.DeAllocateSeat(seat);
//            return Ok("Seat UnAllocation successfull");
//        }
//        else
//            return BadRequest("Invalid action parameter");
//    }
//    catch (Exception ex)
//    {
//        if (ex is EmployeeAlreadyAllocatedException)
//            return Conflict(ex.Message);
//        else if (ex is AllocationException)
//            return Conflict(ex.Message);
//        else if (ex is NoDataException)
//            return NotFound(ex.Message);
//        else
//            return BadRequest(ex.Message);
//    }
//}

//[HttpGet("{action}")]
//public IActionResult GetReport(string action ,int? facilityId)
//{
//    if(string.Equals(action, "allocated", StringComparison.OrdinalIgnoreCase)
//    {
//        if(facilityId.HasValue)
//        {
//            return Ok(_repositaryReport.GetSeatAllocatedView(facilityId.Value));
//        }
//        else
//        {
//            return Ok(_repositaryReport.GetSeatAllocatedView());
//        }
//    }
//    else if(string.Equals(action, "unallocated", StringComparison.OrdinalIgnoreCase))
//    {
//        if (facilityId.HasValue)
//        {
//            return Ok(_repositaryReport.GetSeatUnAllocatdView(facilityId.Value));
//        }
//        else
//        {
//            return Ok(_repositaryReport.GetSeatUnAllocatdView());
//        }
//    }
//}