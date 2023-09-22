using Microsoft.EntityFrameworkCore;
using SeatManagement.CustomException;
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

        //public Cabin Allocate(AllocateDTO cabin)
        //{
        //    var item = _repositaryCabin.GetById(cabin.SeatId);
        //    var employee = _repositaryEmployee.GetById((int)cabin.EmployeeId);

        //    if (item == null || employee == null)
        //        return null;

        //    item.EmployeeId = cabin.EmployeeId;
        //    employee.IsAllocated= true;
        //    _repositaryCabin.Update();
        //    _repositaryEmployee.Update();
        //    return item;
        //}

        public void AllocateCabin(int cabinId, int employeeId)
        {
            var cabin = _repositaryCabin.GetById(cabinId);
            var employee = _repositaryEmployee.GetById(employeeId);

            if (cabin == null)
                throw new NoDataException("Cabin does not exist");
            else if (employee == null)
                throw new NoDataException("Employee does not exist");
            if (employee.IsAllocated == true)
                throw new EmployeeAlreadyAllocatedException("Employee already allocated");
            else if (cabin.EmployeeId != null)
                throw new AllocationException("Cabin already allocated");

            cabin.EmployeeId = employeeId;
            employee.IsAllocated = true;
            _repositaryCabin.Update();
            _repositaryEmployee.Update();
        }

        //public Cabin Deallocate(AllocateDTO cabin)
        //{
        //    var item = _repositaryCabin.GetById(cabin.SeatId);
        //    var employee = _repositaryEmployee.GetById((int)cabin.EmployeeId);

        //    if (item == null || employee == null)
        //        return null;

        //    item.EmployeeId = null;
        //    employee.IsAllocated = false;
        //    _repositaryCabin.Update();
        //    _repositaryEmployee.Update();
        //    return item;
        //}

        public void DeallocateCabin(int cabinId)
        {
            var cabin = _repositaryCabin.GetById(cabinId);

            if (cabin == null)
                throw new NoDataException("Cabin does not exist");
            else if (cabin.EmployeeId == null)
                throw new AllocationException("Cabin is not yet allocated!");

            var employee = _repositaryEmployee.GetById((int)cabin.EmployeeId);
            cabin.EmployeeId = null;
            employee.IsAllocated = false;
            _repositaryCabin.Update();
            _repositaryEmployee.Update();
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


        public List<ViewAllocationDTO> GetCabinUnAllocatedView()
        {
            var item = _repositaryCabin.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == null)
                .Select(x => new ViewAllocationDTO
                {
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    SeatId = x.CabinId,
                    SeatNo = x.CabinNo,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No Unallocated cabins|");
            return item;
        }

        public List<ViewAllocationDTO> GetCabinUnAllocatedViewByFacility(int facilityId)
        {
            var item = _repositaryCabin.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == null && x.FacilityId == facilityId)
                .Select(x => new ViewAllocationDTO
                {
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    SeatId = x.CabinId,
                    SeatNo = x.CabinNo,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No Unallocated cabins!");
            return item;
        }

        public List<ViewAllocationDTO> GetCabinUnAllocatedViewByFloor(int floorNo)
        {
            var item = _repositaryCabin.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == null && x.Facility.FacilityFloor == floorNo)
                .Select(x => new ViewAllocationDTO
                {
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    SeatId = x.CabinId,
                    SeatNo = x.CabinNo,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No Unallocated cabins!");
            return item;
        }
    }
}
