using Microsoft.AspNetCore.Mvc;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _repositary;

        public DepartmentController(IDepartmentService respositary)
        {
            _repositary = respositary;
        }
        [HttpGet] //get all department details
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }
        [HttpPost] //adding a department
        public IActionResult Post(DepartmentDTO departmentDTO)
        {
            int departmentId=_repositary.Add(departmentDTO);
            return Ok("Added successfully");
        }
    }
}
