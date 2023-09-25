using Microsoft.EntityFrameworkCore;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class CabinService : ICabinService
    {
        private readonly IRepository<Cabin> _repositaryCabin;
        private readonly IEmployeeService _employeeService;
        private readonly IFacilityService _facilityService;

        public CabinService(IRepository<Cabin> repositaryCabin,
            IEmployeeService employeeService,
            IFacilityService facilityService)
        {
            _repositaryCabin = repositaryCabin;
            _employeeService = employeeService;
            _facilityService = facilityService;
        }
        public void AddCabin(CabinDTO cabinDTO)
        {
            var facilityList = _facilityService.Get().Select(x => x.FacilityId);
            if (!facilityList.Contains(cabinDTO.FacilityId))
                throw new ForeignKeyViolationException("Entered facility does not exist");

            int PreviousCabinCount = this.Get().Where(x => x.FacilityId == cabinDTO.FacilityId).Count();
            List<Cabin> CabinList = new List<Cabin>();
            for(int start = 0; start<cabinDTO.Capacity; start++) 
            {
                CabinList.Add(new Cabin()
                {
                    CabinNo = string.Format("C{0:D3}", ++PreviousCabinCount),
                    FacilityId = cabinDTO.FacilityId,
                    EmployeeId = null
                });
            }
            _repositaryCabin.Add(CabinList);
        }



        public void AllocateCabin(int cabinId, int employeeId)
        {
            var cabin = this.GetById(cabinId);
            var employee = _employeeService.GetById(employeeId);

            if (employee.IsAllocated == true)
                throw new EmployeeAlreadyAllocatedException("Employee already allocated");
            else if (cabin.EmployeeId != null)
                throw new AllocationException("Cabin already allocated");

            cabin.EmployeeId = employeeId;
            employee.IsAllocated = true;
            _repositaryCabin.Update();
            _employeeService.Update(employee);
        }



        public void DeallocateCabin(int cabinId)
        {
            var cabin = this.GetById(cabinId);

            if (cabin.EmployeeId == null)
                throw new AllocationException("Cabin is not yet allocated!");

            var employee = _employeeService.GetById((int)cabin.EmployeeId);
            cabin.EmployeeId = null;
            employee.IsAllocated = false;
            _repositaryCabin.Update();
            _employeeService.Update(employee);
        }

        public IQueryable<Cabin> Get()
        {
            var cabinList= _repositaryCabin.GetAll();
            if (cabinList == null || !cabinList.Any())
                throw new NoDataException("No cabins found\nPlease add cabin first");
            return cabinList;
        }

        public Cabin GetById(int id)
        {
            return _repositaryCabin.GetById(id) ?? throw new NoDataException("Entered cabin id is not found"); ;
        }


        public IQueryable<ViewAllocationDTO> GetCabinUnAllocatedView()
        {
            var item = this.Get()
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
                });
            if (item.Count() == 0)
                throw new NoDataException("No Unallocated cabins!");
            return item;
        }

        public IQueryable<ViewAllocationDTO> GetCabinUnAllocatedViewByFacility(int facilityId)
        {
            var item = this.Get()
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
                });
            if (item.Count() == 0)
                throw new NoDataException("No Unallocated cabins!");
            return item;
        }

        public IQueryable<ViewAllocationDTO> GetCabinUnAllocatedViewByFloor(int floorNo)
        {
            var item = this.Get()
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
                });
            if (item.Count() == 0)
                throw new NoDataException("No Unallocated cabins!");
            return item;
        }

        public IQueryable<ViewAllocationDTO> GetCabinAllocatedView()
        {
            var item = this.Get()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId != null)
                .Select(x => new ViewAllocationDTO
                {
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    SeatId = x.CabinId,
                    SeatNo = x.CabinNo,
                    EmployeeName = x.Employee.EmployeeName,
                    EmployeeId = x.Employee.EmployeeId,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                });
            if (item.Count() == 0)
                throw new NoDataException("No cabins are allocated yet!");
            return item;
        }

        public IQueryable<ViewAllocationDTO> ReportGenarator(string? type, int? facilityId, int? floorNo)
        {
            if (type != null && type.Equals("allocated"))
                return(this.GetCabinAllocatedView());
            else if (facilityId.HasValue)
                return(this.GetCabinUnAllocatedViewByFacility(facilityId.Value));
            else if (floorNo.HasValue)
                return(this.GetCabinUnAllocatedViewByFloor(floorNo.Value));
            else
                return(this.GetCabinUnAllocatedView());
        }
    }
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