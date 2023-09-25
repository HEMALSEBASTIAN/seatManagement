using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;

        public DepartmentService(IRepository<Department> repository)
        {
            _repository = repository;
        }
        public int Add(DepartmentDTO departmentDTO)
        {
            var department = new Department()
            {
                DepartmentName = departmentDTO.DepartmentName,
            };
            _repository.Add(department);
            return department.DepartmentId;
        }

        public IQueryable<Department> Get()
        {
            var departmentList= _repository.GetAll();
            if (departmentList ==null || !departmentList.Any())
                throw new NoDataException("No departments found\nPlease add departments first");
            return departmentList;
        }
    }
}
