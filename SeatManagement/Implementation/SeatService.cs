using Microsoft.EntityFrameworkCore;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class SeatService : ISeatService
    {
        private readonly IRepositary<Seat> _seatRepositary;
        private readonly IRepositary<Employee> _employeeRepositary;

        public SeatService(IRepositary<Seat> seatRepositary, IRepositary<Employee> employeeRepositary)
        {
            _seatRepositary = seatRepositary;
            _employeeRepositary = employeeRepositary;
        }
        public List<Seat> Get()
        {
            return _seatRepositary.GetAll().ToList();
        }
        

        public void AddSeat(List<SeatDTO> seatDTOList)
        {
            List<Seat> seatList = new List<Seat>();
            foreach (var seatDTO in seatDTOList)
            {
                seatList.Add(new Seat()
                {
                    SeatNo = seatDTO.SeatNo,
                    FacilityId = seatDTO.FacilityId,
                    EmployeeId = null
                });
            }
            _seatRepositary.Add(seatList);
        }

        

        public void AllocateSeat(int seatId, int employeeId) 
        {
            var seat = _seatRepositary.GetById(seatId);
            var employee = _employeeRepositary.GetById(employeeId);

            if (seat == null)
                throw new NoDataException("Seat does not exist");
            else if (employee == null)
                throw new NoDataException("Employee does not exist");
            if (employee.IsAllocated == true)
                throw new EmployeeAlreadyAllocatedException("Employee already allocated");
            else if (seat.EmployeeId != null)
                throw new AllocationException("Seat already allocated");

            seat.EmployeeId = employeeId;
            employee.IsAllocated = true;
            _seatRepositary.Update();
            _employeeRepositary.Update();
        }

        

        public void DeAllocateSeat(int seatId)
        {
            var seat = _seatRepositary.GetById(seatId);

            if (seat == null)
                throw new NoDataException("Seat does not exist");
            else if (seat.EmployeeId == null)
                throw new AllocationException("Seat is not yet allocated!");

            var employee = _employeeRepositary.GetById((int)seat.EmployeeId);
            seat.EmployeeId = null;
            employee.IsAllocated = false;
            _seatRepositary.Update();
            _employeeRepositary.Update();
        }

        public Seat GetById(int id)
        {
            return _seatRepositary.GetById(id);
        }

        public List<ViewAllocationDTO> GetSeatUnAllocatdViewByFacility(int facilityId)
        {
            var item = _seatRepositary.GetAll()
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
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No UnAllocated seats");
            return item;
        }

        public List<ViewAllocationDTO> GetSeatUnAllocatdView()
        {
            var item = _seatRepositary.GetAll()
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
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No UnAllocated seats!");
            return item;
        }


        public List<ViewAllocationDTO> GetSeatUnAllocatedViewByFloor(int floorNo)
        {
            var item = _seatRepositary.GetAll()
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
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No UnAllocated seats!");
            return item;
        }


        public List<Seat> Get(int pageNumber, int pageSize)
        {
            return _seatRepositary.GetAll()
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToList();
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