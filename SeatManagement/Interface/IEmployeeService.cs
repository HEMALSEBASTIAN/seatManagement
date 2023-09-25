using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface IEmployeeService
    {
        public void Add(List<EmployeeDTO> emploeeDTOList);
        public IQueryable<Employee> Get();
        public Employee GetById(int id);
        public Employee Update(Employee employee);
    }
}
