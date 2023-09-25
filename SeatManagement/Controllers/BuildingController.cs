using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : Controller
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService=buildingService;
        }
        [HttpGet] //getting the building details
        public IActionResult Get()
        {
            return Ok(_buildingService.Get());
        }
        [HttpPost] //Adding a building
        public IActionResult Post(LookUpBuildingDTO BuildingDTO) 
        {
            return Ok(_buildingService.Add(BuildingDTO));
        }
    }
}
