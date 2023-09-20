using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositary<Department> _repositary;

        public DepartmentService(IRepositary<Department> repositary)
        {
            _repositary = repositary;
        }
        public int Add(DepartmentDTO departmentDTO)
        {
            var department = new Department()
            {
                DepartmentName = departmentDTO.DepartmentName,
            };
            _repositary.Add(department);
            return department.DepartmentId;
        }

        public List<Department> Get()
        {
            return _repositary.GetAll().ToList();
        }
    }
}
