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

        [HttpPost] //Adding seat in bulk
        public IActionResult Post(List <SeatDTO> seatDTOList)
        {
            _repositary.AddSeat(seatDTOList);
            return Ok();
        }
        
        [HttpPatch("{seatId}")] //For allocating or deallocating seat
        public IActionResult Allocate(int seatId, int? employeeId)
        {
            try
            {
                if(employeeId.HasValue)
                {
                    _repositary.AllocateSeat(seatId, employeeId.Value);
                }
                else
                {
                    _repositary.DeAllocateSeat(seatId);
                }
                return Ok();
            }
            catch(NoDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch(EmployeeAlreadyAllocatedException ex)
            {
                return Conflict(ex.Message);
            }
            catch(AllocationException ex)
            {
                return Conflict(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
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

    }
}
