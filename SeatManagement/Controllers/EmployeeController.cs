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
        private readonly IEmployeeService _repositary;

        public EmployeeController(IEmployeeService repositary)
        {
            _repositary=repositary;
        }
        [HttpGet] //get all employee details
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }
        [HttpPost] //add employee in bulk
        public IActionResult AddEmployee(List<EmployeeDTO> employeeDTOList)
        {
            if (employeeDTOList.Count == 1)
            {
                _repositary.Add(employeeDTOList[0]);
            }
            else
                _repositary.Add(employeeDTOList);
            return Ok();
        }






        [Route("id")]
        [HttpGet] //get emplpoyee details
        public IActionResult Get(int id)
        {
            var item=_repositary.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [Route("id")]
        [HttpPatch] //update employee details
        public IActionResult Update(Employee employee)
        {
            var item = _repositary.Update(employee);
            if(item == null)
                return NotFound();
            return Ok();
        }


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
    }
}
