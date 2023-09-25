using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface IDepartmentService
    {
        public int Add(DepartmentDTO departmentDTO);
        public IQueryable<Department> Get();
    }
}
