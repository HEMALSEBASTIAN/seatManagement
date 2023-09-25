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
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet] //get all department details
        public IActionResult Get()
        {
            return Ok(_departmentService.Get());
        }
        [HttpPost] //adding a department
        public IActionResult Post(DepartmentDTO departmentDTO)
        {
            return Ok(_departmentService.Add(departmentDTO));
        }
    }
}
