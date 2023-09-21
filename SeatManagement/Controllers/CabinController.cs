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
        private readonly ICabinService _repositary;

        public CabinController(ICabinService repositary)
        {
            _repositary = repositary;
        }
        [HttpGet] //Get all cabin
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }
        [HttpPost] //Adding cabin in bulk
        public IActionResult Post(List<CabinDTO> cabinDTOList)
        {
            _repositary.AddCabin(cabinDTOList);
            return Ok();
        }
        [HttpPatch("{cabinId}")] //For Allocating and deallocating cabin
        public IActionResult Allocate(int cabinId, int? employeeId)
        {
            try
            {
                if (employeeId.HasValue)
                {
                    _repositary.AllocateCabin(cabinId, employeeId.Value);
                }
                else
                {
                    _repositary.DeallocateCabin(cabinId);
                }
                return Ok();
            }
            catch (NoDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EmployeeAlreadyAllocatedException ex)
            {
                return Conflict(ex.Message);
            }
            catch (AllocationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}")] //get cab by id
        public IActionResult Get(int id)
        {
            var item = _repositary.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
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
    }
}
