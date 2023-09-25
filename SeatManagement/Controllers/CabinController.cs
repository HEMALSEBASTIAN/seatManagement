using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinController : Controller
    {
        private readonly ICabinService _cabinService;

        public CabinController(ICabinService cabinService)
        {
            _cabinService = cabinService;
        }
        [HttpGet] //Get all cabin
        public IActionResult Get()
        {
            return Ok(_cabinService.Get());
        }
        [HttpPost] //Adding cabin in bulk
        public IActionResult Post(CabinDTO cabinDTO)
        {
            _cabinService.AddCabin(cabinDTO);
            return Ok($"{cabinDTO.Capacity} cabins added successfully");   
        }
        [HttpPatch("{cabinId}")] //For Allocating and deallocating cabin
        public IActionResult Allocate(int cabinId, int? employeeId)
        {
            if (employeeId.HasValue)
                _cabinService.AllocateCabin(cabinId, employeeId.Value);
            else
                _cabinService.DeallocateCabin(cabinId);
            return Ok();
        }
        [HttpGet("{id}")] //get cab by id
        public IActionResult Get(int id)
        {
            return Ok(_cabinService.GetById(id));
        }
        [HttpGet("report")]
        public IActionResult Report(string? type, int? facilityId, int? floorNo)
        {
            return Ok(_cabinService.ReportGenarator(type, facilityId, floorNo));
        }
    }
}


//[Route("allocate")]
//[HttpPatch]
//public IActionResult Allocate(AllocateDTO cabin)
//{
//    var item=_repositary.Allocate(cabin);
//    if(item==null)
//        return NotFound();
//    return Ok();
//}
//[Route("deallocate")]
//[HttpPatch]
//public IActionResult DeAllocate(AllocateDTO cabin)
//{
//    var item = _repositary.Deallocate(cabin);
//    if (item == null)
//        return NotFound();
//    return Ok();
//}
//[HttpPatch] //Alocating and deallocating cabin
//public IActionResult Allocation([FromQuery] string action, AllocateDTO cabin)
//{
//    Cabin item;
//    if (String.Equals(action, "allocate", StringComparison.OrdinalIgnoreCase))
//    {
//        item = _repositary.Allocate(cabin);
//        if (item == null)
//            return NotFound("Content not found");
//        return Ok("Cabin Allocation successfull");
//    }
//    else if (String.Equals(action, "deallocate", StringComparison.OrdinalIgnoreCase))
//    {
//        item = _repositary.Deallocate(cabin);
//        if (item == null)
//            return NotFound("Content not found");
//        return Ok("Cabin Unallocation successfull");
//    }
//    else
//        return BadRequest("Invalid action parameter");
//}