using Microsoft.AspNetCore.Mvc;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService=employeeService;
        }

        [HttpGet] //get all employee details
        public IActionResult Get()
        {
            return Ok(_employeeService.Get());
        }

        [HttpPost] //add employee in bulk
        public IActionResult AddEmployee(List<EmployeeDTO> employeeDTOList)
        {
            _employeeService.Add(employeeDTOList);
            return Ok();
        }

        [HttpGet("{id}")] //get emplpoyee details
        public IActionResult Get(int id)
        {
            return Ok(_employeeService.GetById(id));
        }
    }
}

//[Route("id")]
//[HttpPatch] //update employee details
//public IActionResult Update(Employee employee)
//{
//    var item = _repositary.Update(employee);
//    if(item == null)
//        return NotFound();
//    return Ok();
//}


//[HttpPost]
//public IActionResult Post(EmployeeDTO employeeDTO)
//{
//    _repositary.Add(employeeDTO);
//    return Ok();
//}


//[HttpPost("BulkAdd")]
//public IActionResult Post(List<EmployeeDTO> employeeDTOList) 
//{
//    _repositary.Add(employeeDTOList);
//    return Ok();
//}