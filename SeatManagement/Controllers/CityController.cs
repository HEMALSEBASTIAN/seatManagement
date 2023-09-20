using Microsoft.AspNetCore.Mvc;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {

        private readonly ICityService _repositary;
        public CityController(ICityService repositary)
        {
            this._repositary = repositary;
        }
        [HttpGet] //Get all city details
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }
        [HttpPost] //Adding a city
        public IActionResult Post(LookUpCityDTO CityDTO)
        {
            int CityId=_repositary.Add(CityDTO);   
            return Ok(CityId);
        }
        
        

        
        [HttpGet] //get city details by id
        [Route("id")]
        public IActionResult Get(int id) 
        {
            var item=_repositary.GetById(id);
            if(item == null) 
                return NotFound();
            return Ok(item);
        }
        [HttpPatch] //Update city by id
        [Route("id")]
        public IActionResult Update(LookUpCity City)
        {
            var item = _repositary.Update(City);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(); 
        }
    }
}
