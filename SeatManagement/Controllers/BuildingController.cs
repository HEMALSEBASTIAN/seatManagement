using Microsoft.AspNetCore.Mvc;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : Controller
    {
        private readonly IBuildingService _repositary;

        public BuildingController(IBuildingService repositary)
        {
            _repositary=repositary;
        }
        [HttpGet] //getting the building details
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }
        [HttpPost] //Adding a building
        public IActionResult Post(LookUpBuildingDTO BuildingDTO) 
        {
            int BuildingId= _repositary.Add(BuildingDTO);
            return Ok(BuildingId);
        }
    }
}
