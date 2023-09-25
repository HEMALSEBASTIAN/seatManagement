using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
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

        [HttpGet]  //Get all meeting room details with city and building name
        public IActionResult Get()
        {
            return Ok(_repositary.GetAll());
        }
        
        [HttpPost]  //Add meeting room on bulk
        public IActionResult Post(MeetingRoomDTO meetingRoomDTO)
        {
            _repositary.Add(meetingRoomDTO);
            return Ok($"{meetingRoomDTO.MeetingRoomCount} meeting rooms added successfully");
        }

        [HttpPost("{MeetingRoomId}")] //adding asset to meeting room
        public IActionResult AllocateAsset(int MeetingRoomId, MeetingRoomAssetDTO newAsset)
        {
            try
            {
                _repositary.AllocateAsset(MeetingRoomId, newAsset);
                return Ok("Asset allocated to meeting room ");
            }
            catch(ForeignKeyViolationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("{id}")]  //updating a meeting room detail
        public IActionResult Update(MeetingRoom meetingRoom)
        {
            var item = _repositary.Update(meetingRoom);
            return Ok("Meeting room updated successfully");
        }


        [HttpGet("{facilityId}")]  //return all meeting room in a particular facility
        public IActionResult GetMeetingRoomByFacility(int facilityId)
        {
            var item = _repositary.GetMeetingRoomByFacility(facilityId);
            return Ok(item);
        }
    }
}



//[HttpGet("{id}")]  //Get meeting room by details
//public IActionResult Get(int id)
//{
//    return Ok(_repositary.GetById(id));
//}