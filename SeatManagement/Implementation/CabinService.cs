using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class CabinService : ICabinService
    {
        private readonly IRepositary<Cabin> _repositaryCabin;
        private readonly IRepositary<Employee> _repositaryEmployee;

        public CabinService(IRepositary<Cabin> repositaryCabin, IRepositary<Employee> repositaryEmployee)
        {
            _repositaryCabin = repositaryCabin;
            _repositaryEmployee = repositaryEmployee;
        }
        public void AddCabin(List<CabinDTO> cabinDTOList)
        {
            List<Cabin> CabinList = new List<Cabin>();
            foreach(var cabinDTO in  cabinDTOList)
            {
                CabinList.Add(new Cabin()
                {
                    CabinNo = cabinDTO.CabinNo,
                    FacilityId = cabinDTO.FacilityId,
                    EmployeeId = null
                });
                
            }
            
            _repositaryCabin.Add(CabinList);
        }

        public Cabin Allocate(AllocateDTO cabin)
        {
            var item = _repositaryCabin.GetById(cabin.SeatId);
            var employee = _repositaryEmployee.GetById((int)cabin.EmployeeId);

            if (item == null || employee == null)
                return null;

            item.EmployeeId = cabin.EmployeeId;
            employee.IsAllocated= true;
            _repositaryCabin.Update();
            _repositaryEmployee.Update();
            return item;
        }

        public Cabin Deallocate(AllocateDTO cabin)
        {
            var item = _repositaryCabin.GetById(cabin.SeatId);
            var employee = _repositaryEmployee.GetById((int)cabin.EmployeeId);

            if (item == null || employee == null)
                return null;

            item.EmployeeId = null;
            employee.IsAllocated = false;
            _repositaryCabin.Update();
            _repositaryEmployee.Update();
            return item;
        }


        public List<Cabin> Get()
        {
            return _repositaryCabin.GetAll().ToList();
        }

        public Cabin GetById(int id)
        {
            var item = _repositaryCabin.GetById(id);
            if (item == null)
                return null;
            return item;
        }
    }
}
