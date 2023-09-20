using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositary<Employee> _repositary;

        public EmployeeService(IRepositary<Employee> repositary)
        {
            _repositary = repositary;
        }
        public void Add(EmployeeDTO employeeDTO)
        {
            var employee = new Employee()
            {
                EmployeeName = employeeDTO.EmployeeName,
                DepartmentId = employeeDTO.DepartmentId,
                IsAllocated = false
            };
            _repositary.Add(employee);
        }

        public void Add(List<EmployeeDTO> emploeeDTOList)
        {
            List<Employee> employeeList=new List<Employee>();
            foreach(var employeeDTO in emploeeDTOList)
            {
                employeeList.Add(new Employee()
                {
                    EmployeeName = employeeDTO.EmployeeName,
                    DepartmentId = employeeDTO.DepartmentId,
                    IsAllocated = false
                });
            }
            _repositary.Add(employeeList);
        }

        public List<Employee> Get()
        {
            return _repositary.GetAll().ToList();
        }

        public Employee GetById(int id)
        {
            return _repositary.GetById(id);
        }

        public Employee Update(Employee employee)
        {
            var item = _repositary.GetById(employee.EmployeeId);
            if (item == null)
                return null;
            item.DepartmentId = employee.DepartmentId;
            item.EmployeeName = employee.EmployeeName;
            _repositary.Update();
            return item;
        }
    }
}
