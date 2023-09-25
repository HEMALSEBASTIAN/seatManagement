using Microsoft.EntityFrameworkCore;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;
using System.Linq;

namespace SeatManagement.Implementation
{
    public class SeatService : ISeatService
    {
        private readonly IRepository<Seat> _seatRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IFacilityService _facilityService;

        public SeatService(IRepository<Seat> seatRepositary, 
            IEmployeeService employeeService,
            IFacilityService facilityService)
        {
            _seatRepository = seatRepositary;
            _employeeService = employeeService;
            _facilityService = facilityService;
        }


        public IQueryable<Seat> Get(int? pageNumber, int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                return _seatRepository.GetAll()
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
            }
            return _seatRepository.GetAll();

        }

        public void AddSeat(SeatDTO seatDTO)
        {
            var facilityList = _facilityService.Get().Select(x => x.FacilityId);

            if (!facilityList.Contains(seatDTO.FacilityId))
                throw new ForeignKeyViolationException("Entered facility does not exist");

            int PreviousSeatCount = _seatRepository.GetAll().Where(x => x.FacilityId == seatDTO.FacilityId).Count();
            List<Seat> seatList = new List<Seat>();
            for(int start=0; start<seatDTO.Capacity; start++)
            {
                seatList.Add(new Seat()
                {
                    SeatNo = string.Format("S{0:D3}", ++PreviousSeatCount),
                    FacilityId = seatDTO.FacilityId,
                    EmployeeId = null
                });
            }
            _seatRepository.Add(seatList);
        }

        

        public void AllocateSeat(int seatId, int employeeId) 
        {
            var seat = this.GetById(seatId);
            var employee = _employeeService.GetById(employeeId);

            if (employee.IsAllocated == true)
                throw new EmployeeAlreadyAllocatedException("Employee already allocated");
            else if (seat.EmployeeId != null)
                throw new AllocationException("Seat already allocated");

            seat.EmployeeId = employeeId;
            employee.IsAllocated = true;
            _seatRepository.Update();
            _employeeService.Update(employee);
        }

        

        public void DeAllocateSeat(int seatId)
        {
            var seat = this.GetById(seatId);

            if (seat.EmployeeId == null)
                throw new AllocationException("Seat is not yet allocated!");

            var employee = _employeeService.GetById((int)seat.EmployeeId);
            seat.EmployeeId = null;
            employee.IsAllocated = false;
            _seatRepository.Update();
            _employeeService.Update(employee);
        }

        public Seat GetById(int id)
        {
            var seat= _seatRepository.GetById(id);
            return seat ?? throw new NoDataException("Entered seat Id does not exist");
        }

        public IQueryable<ViewAllocationDTO> ReportGenarator(string? type, int? facilityId, int? floorNo)
        {
            if (type != null && type.Equals("allocated"))
                return this.GetSeatAllocatedView();
            else if (facilityId.HasValue)
                return this.GetSeatUnAllocatdViewByFacility(facilityId.Value);
            else if (floorNo.HasValue)
                return this.GetSeatUnAllocatedViewByFloor(floorNo.Value);
            else
                return this.GetSeatUnAllocatdView();
        }
        public IQueryable<ViewAllocationDTO> GetSeatUnAllocatdViewByFacility(int facilityId)
        {
            var item = this.Get(null, null)
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == null && x.FacilityId == facilityId)
                .Select(x => new ViewAllocationDTO
                {
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    SeatId = x.SeatId,
                    SeatNo = x.SeatNo,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                });
            if (!item.Any())
                throw new NoDataException("No UnAllocated seats");
            return item;
        }

        public IQueryable<ViewAllocationDTO> GetSeatUnAllocatdView()
        {
            var item = this.Get(null, null)
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == null)
                .Select(x => new ViewAllocationDTO
                {
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    SeatId = x.SeatId,
                    SeatNo = x.SeatNo,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                });
            if (!item.Any())
                throw new NoDataException("No UnAllocated seats!");
            return item;
        }


        public IQueryable<ViewAllocationDTO> GetSeatUnAllocatedViewByFloor(int floorNo)
        {
            var item = this.Get(null, null)
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == null && x.Facility.FacilityFloor == floorNo)
                .Select(x => new ViewAllocationDTO
                {
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    SeatId = x.SeatId,
                    SeatNo = x.SeatNo,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                });
            if (!item.Any())
                throw new NoDataException("No UnAllocated seats!");
            return item;
        }

        public IQueryable<ViewAllocationDTO> GetSeatAllocatedView()
        {
            var item = this.Get(null, null)
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId != null)
                .Select(x => new ViewAllocationDTO
                {
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    SeatId = x.SeatId,
                    SeatNo = x.SeatNo,
                    EmployeeName = x.Employee.EmployeeName,
                    EmployeeId = x.Employee.EmployeeId,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                });
            if (!item.Any())
                throw new NoDataException("No seats are allocated yet!");
            return item;
        }

        
    }
}


//public Seat AllocateSeat(AllocateDTO seat)
//{
//    var item = _seatRepositary.GetById(seat.SeatId);
//    var employee = _employeeRepositary.GetById((int)seat.EmployeeId);
//    if (item == null)  
//        throw new NoDataException("Seat does not exist");
//    else if(employee == null)
//            throw new NoDataException("Employee does not exist");
//    else if (employee.IsAllocated == true)
//        throw new EmployeeAlreadyAllocatedException("Employee already allocated");
//    else if(item.EmployeeId!=null)
//        throw new AllocationException("Seat already allocated");
//    item.EmployeeId = seat.EmployeeId;
//    employee.IsAllocated = true;
//    _employeeRepositary.Update();
//    _seatRepositary.Update();
//    return item;
//}


//public Seat DeAllocateSeat(AllocateDTO seat)
//{
//    var item = _seatRepositary.GetById(seat.SeatId);
//    var employee = _employeeRepositary.GetById((int)seat.EmployeeId);
//    if (item == null)
//        throw new NoDataException("Seat does not exist");
//    else if (employee == null)
//        throw new NoDataException("Employee does not exist");
//    else if (employee.IsAllocated == false)
//        throw new EmployeeAlreadyAllocatedException("Employee is not yet allocated!");
//    else if (item.EmployeeId == null)
//        throw new AllocationException("Seat is not yet allocated!"); 
//    item.EmployeeId = null;
//    employee.IsAllocated = false;
//    _employeeRepositary.Update();
//    _seatRepositary.Update();
//    return item;
//}


//public void AddSeat(SeatDTO seatDTO)
//{
//    var seat = new Seat()
//    {
//        SeatNo = seatDTO.SeatNo,
//        FacilityId = seatDTO.FacilityId,
//        EmployeeId=null
//    };
//    _seatRepositary.Add(seat);
//}