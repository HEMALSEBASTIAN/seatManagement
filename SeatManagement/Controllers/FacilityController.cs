using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy="User")]
    public class FacilityController : Controller
    {
        private readonly IFacilityService _repositary;

        public FacilityController(IFacilityService repositary)
        {
            _repositary = repositary;
        }

        [HttpGet]  //get all faciltiy details with city and building name
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }

        [HttpPost] //Add facility
        public IActionResult Post(FacilityDTO facilityDTO)
        {
            int FacilityId = _repositary.Add(facilityDTO);
            return Ok(FacilityId);
        }

        [HttpGet("{id}")] //get a faciltiy detail by id
        public IActionResult Get(int id)
        {
            return Ok(_repositary.GetById(id));
        }
    }
}


//[Route("id")]
//[HttpPatch] //update a facility
//public IActionResult Update(Facility facility)
//{
//    var item=_repositary.Update(facility);
//    if(item==null)
//        return NotFound();
//    return Ok();
//}

//[HttpGet("view")]
//public IActionResult View()
//{
//    var item = _repositary.GetView();
//    return Ok(item);
//}