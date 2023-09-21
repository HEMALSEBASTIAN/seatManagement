using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _repositary;
        private readonly IMemoryCache _memoryCache;
        private const string key = "_reportCache";
        public ReportController(IReportService repositary, IMemoryCache memoryCache)
        {
            _repositary=repositary;
            _memoryCache = memoryCache;
        }
        //[HttpGet("ViewFacility")] //Return all facility details
        //public IActionResult FacilityView()
        //{
        //    return Ok(_repositary.GetFacilityList());
        //}

        //[HttpGet("ViewAllocatedSeat")]
        //public IActionResult ViewAllocatedSeat()
        //{
        //    if(Request.Query.ContainsKey("facilityId"))
        //    {
        //        int facilityId = int.Parse(Request.Query["facilityId"]);
        //        return Ok(_repositary.GetSeatAllocatedView(facilityId));
        //    }
        //    return Ok(_repositary.GetSeatAllocatedView());
        //}
        //[HttpGet("ViewUnAllocatedSeat")]
        //public IActionResult ViewUnAllocatedSeat()
        //{
        //    if(Request.Query.ContainsKey("facilityId"))
        //    {
        //        int facilityId = int.Parse(Request.Query["facilityId"]);
        //        return Ok(_repositary.GetSeatUnAllocatdView(facilityId));
        //    }
        //    return Ok(_repositary.GetSeatUnAllocatdView());
        //}


        [HttpGet()] //return allocated and unallocated seat and cabin report from all facilities or particular faciltiy
        public IActionResult AllocationReport([FromQuery] string type, [FromQuery] string action, [FromQuery] int?  facilityId)
        {
            try
            {
                
                if (string.Equals(type, "seat", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(action, "ViewUnAllocatedSeat", StringComparison.OrdinalIgnoreCase))
                    {
                        
                        if (facilityId.HasValue)
                        {
                            if (!_memoryCache.TryGetValue(key, out List<ViewAllocationDTO> list))
                            {
                                list = _repositary.GetSeatUnAllocatdView(facilityId.Value);
                                var options = new MemoryCacheEntryOptions
                                {
                                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                                };
                                Console.WriteLine("not cached");
                                _memoryCache.Set(key,list, options);
                            }
                            Console.WriteLine("Cached");
                            return Ok(list);
                        }
                            //return Ok(_repositary.GetSeatUnAllocatdView(facilityId.Value));
                        else
                            return Ok(_repositary.GetSeatUnAllocatdView());
                    }
                    else if (string.Equals(action, "ViewAllocatedSeat", StringComparison.OrdinalIgnoreCase))
                    {
                        if (facilityId.HasValue)
                            return Ok(_repositary.GetSeatAllocatedView(facilityId.Value));
                        else
                            return Ok(_repositary.GetSeatAllocatedView());
                    }
                    else
                        return BadRequest("Invalid Action Parameter");
                }
                else if (string.Equals(type, "cabin", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(action, "ViewUnAllocatedCabin", StringComparison.OrdinalIgnoreCase))
                    {
                        if (facilityId.HasValue)
                            return Ok(_repositary.GetCabinUnAllocatedView(facilityId.Value));
                        else
                            return Ok(_repositary.GetCabinUnAllocatedView());
                    }
                    else if (string.Equals(action, "ViewAllocatedCabin", StringComparison.OrdinalIgnoreCase))
                    {
                        if (facilityId.HasValue)
                            return Ok(_repositary.GetCabinAllocatedView(facilityId.Value));
                        else
                            return Ok(_repositary.GetCabinAllocatedView());
                    }
                    else
                        return BadRequest("Invaluid Action parameter");
                }
                else
                {
                    return BadRequest("Invalid Type Parameter");
                }

            }
            catch(NoDataException ex)
            {
                return NotFound(ex.Message);
            }
        }



        //[HttpGet("seat")]
        //public IActionResult ViewSeat([FromQuery] string action, [FromQuery] int? facilityId)
        //{
        //    if (string.Equals(action, "ViewAllocatedSeat"))
        //    {
        //        if (facilityId.HasValue)
        //        {
        //            return Ok(_repositary.GetSeatAllocatedView(facilityId.Value));
        //        }
        //        else
        //        {
        //            return Ok(_repositary.GetSeatAllocatedView());
        //        }
        //    }
        //    else if (string.Equals(action, "ViewUnAllocatedSeat"))
        //    {
        //        if (facilityId.HasValue)
        //        {
        //            List<ViewAllocationDTO> s = _repositary.GetSeatUnAllocatdView(facilityId.Value);
        //            return Ok(s);
        //        }
        //        else
        //        {
        //            return Ok(_repositary.GetSeatUnAllocatdView());
        //        }
        //    }
        //    else
        //        return BadRequest("Invalid action parameter.");
        //}



        //[HttpGet("ViewAllocatedSeat")]
        //public IActionResult ViewAllocatedSeatWithParameter()
        //{
        //    int facilityId = int.Parse(Request.Query["facilityId"]);
        //    return Ok(_repositary.GetSeatAllocatedView(facilityId));
        //}
        //[HttpGet("ViewUnAllocatedSeat/{facilityId:int}")]
        //public IActionResult ViewUnAllocatedSeatWithParameter(int facilityId)
        //{
        //    return Ok(_repositary.GetSeatUnAllocatdView(facilityId));
        //}





        //[HttpGet("cabin")]
        //public IActionResult ViewCabin([FromQuery] string action, [FromQuery] int? facilityId)
        //{
        //    try
        //    {
        //        if (string.Equals(action, "ViewUnAllocatedCabin"))
        //        {
        //            if (facilityId.HasValue)
        //            {
        //                return Ok(_repositary.GetCabinUnAllocatedView(facilityId.Value));
        //            }
        //            else
        //            {
        //                return Ok(_repositary.GetCabinUnAllocatedView());
        //            }
        //        }
        //        else if (string.Equals(action, "ViewAllocatedCabin"))
        //        {
        //            if (facilityId.HasValue)
        //            {
        //                return Ok(_repositary.GetCabinAllocatedView(facilityId.Value));
        //            }
        //            else
        //            {
        //                return Ok(_repositary.GetCabinAllocatedView());
        //            }
        //        }
        //        else
        //            return BadRequest("Invalid Action Parameter");
        //    }
        //    catch(NoDataException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
            
        //}

        //[HttpGet("ViewUnAllocatedCabin")]
        //public IActionResult ViewUnAllocatedCabin()
        //{
        //    return Ok(_repositary.GetCabinUnAllocatedView());
        //}
        //[HttpGet("ViewAllocatedCabin")]
        //public IActionResult ViewAllocatedCabin()
        //{
        //    return Ok(_repositary.GetCabinAllocatedView());
        //}
        //[HttpGet("ViewUnAllocatedCabin/Facility")]
        //public IActionResult ViewUnAllocatedCabinWithParameter(int facilityId)
        //{
        //    return Ok(_repositary.GetCabinUnAllocatedView(facilityId));
        //}
        //[HttpGet("ViewAllocatedCabin/Facility")]
        //public IActionResult ViewAllocatedCabinWithParameter(int facilityId)
        //{
        //    return Ok(_repositary.GetCabinAllocatedView(facilityId));
        //}



        [HttpGet("GetMeetingRoomView")]  //return meeting room details of all facilities
        public IActionResult GetMeetingRoomView()
        {
            return Ok(_repositary.GetMeetingRoomView());
        }
    }
}
