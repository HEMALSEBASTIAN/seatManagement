using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Implementation;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repositoryEmployee;
        private readonly IDepartmentService _departmentService;

        public EmployeeService(
            IRepository<Employee> repositoryEmployee,
            IDepartmentService departmentService
            )
        {
            _repositoryEmployee = repositoryEmployee;
            _departmentService = departmentService;
        }

        public IQueryable<Employee> Get()
        {
            var employeeList=_repositoryEmployee.GetAll();
            if (employeeList == null || !employeeList.Any())
                throw new NoDataException("No employee found\nPlease add employee first");
            return employeeList;
        }

        public Employee GetById(int id)
        {
            var employee = _repositoryEmployee.GetById(id);
            return employee ?? throw new NoDataException("Entered employee Id does not exist");
        }

        public Employee Update(Employee employee)
        {
            var item = this.GetById(employee.EmployeeId);
            item.DepartmentId = employee.DepartmentId;
            item.EmployeeName = employee.EmployeeName;
            _repositoryEmployee.Update();
            return item;
        }

        public void Add(List<EmployeeDTO> emploeeDTOList)
        {
            var departmentList = _departmentService.Get().Select(x=>x.DepartmentId);

            int errorCount = 0;
            List<Employee> employeeList=new List<Employee>();

            foreach(var employeeDTO in emploeeDTOList)
            {
                if(!departmentList.Contains(employeeDTO.DepartmentId))
                {
                    errorCount++;
                    continue;
                }
                employeeList.Add(new Employee()
                {
                    EmployeeName = employeeDTO.EmployeeName,
                    DepartmentId = employeeDTO.DepartmentId,
                    IsAllocated = false
                });
            }
            _repositoryEmployee.Add(employeeList);
            if(errorCount > 0)
            {
                throw new ForeignKeyViolationException($"{errorCount} employees was not added due to invalid department entered.");
            }
        }  
    }
}


//public void Add(EmployeeDTO employeeDTO)
//{
//    var employee = new Employee()
//    {
//        EmployeeName = employeeDTO.EmployeeName,
//        DepartmentId = employeeDTO.DepartmentId,
//        IsAllocated = false
//    };
//    _repositary.Add(employee);
//}