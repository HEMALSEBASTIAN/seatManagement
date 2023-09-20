using Microsoft.AspNetCore.Mvc;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingRoomAssetController : Controller
    {
        private readonly IMeetingRoomAssetService _repositary;

        public MeetingRoomAssetController(IMeetingRoomAssetService repositary)
        {
            _repositary=repositary;
        }
        [HttpGet] //get all allocated meeting room asset
        public IActionResult Get() 
        {
            return Ok(_repositary.GetAll());
        }
        [HttpPost]  //allocate a asset to meeting room
        public IActionResult Post(MeetingRoomAssetDTO meetingRoomAssetDTO)
        {
            _repositary.Add(meetingRoomAssetDTO);
            return Ok();
        }
        
        [Route("id")] //Get meeting room asset details of a meeting room
        [HttpGet]
        public IActionResult Get(int id)
        {
            var item = _repositary.GetById(id);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        
        
        
        [HttpPatch] //update meeting room asset
        public IActionResult Update(MeetingRoomAsset meetingRoomAsset)
        {
            var item=_repositary.Update(meetingRoomAsset);
            if (item == null)
                return NotFound();
            return Ok();
        }


        [HttpGet("MeetingRoomId")] //get asset name from meetingRoomAsset bu linking asset, room and assetroom
        public IActionResult GetMeetingRoomAssetsName(int MeetingRoomId)
        {
            var item = _repositary.GetAll(MeetingRoomId);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
    }
}
