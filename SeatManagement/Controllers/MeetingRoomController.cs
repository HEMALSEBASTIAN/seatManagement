using Microsoft.AspNetCore.Mvc;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingRoomController : Controller
    {
        private readonly IMeetingRoomService _repositary;

        public MeetingRoomController(IMeetingRoomService repositary)
        {
            _repositary = repositary;
        }
        [HttpGet]  //Get all meeting room details
        public IActionResult Get()
        {
            return Ok(_repositary.GetAll());
        }
        
        [HttpPost]  //Add meeting room on bulk
        public IActionResult Post(List<MeetingRoomDTO> meetingRoomDTOList)
        {
            _repositary.Add(meetingRoomDTOList);
            return Ok();
        }

        [Route("id")]
        [HttpGet]  //Get meeting room by details
        public IActionResult Get(int id)
        {
            var item = _repositary.GetById(id);
            if (item == null)
            {
                return NotFound("Meeting room not found");
            }
            return Ok(item);
        }

        [Route("id")]  //updating meeting room details
        [HttpPatch] 
        public IActionResult Update(MeetingRoom meetingRoom)
        {
            var item = _repositary.Update(meetingRoom);
            if(item == null)
            {
                return NotFound("Meeting room not found");
            }
            return Ok();
        }

        [HttpGet("FacilityId")]  //return all meeting room in a particular facility
        public IActionResult GetMeetingRoomByFacility(int facilityId)
        {
            var item = _repositary.GetMeetingRoomByFacility(facilityId);
            return Ok(item);
        }
    }
}
