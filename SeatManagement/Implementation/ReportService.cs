using Microsoft.EntityFrameworkCore;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IRepositary<Facility> _repositaryViewFacility;
        private readonly IRepositary<Seat> _repositaryViewSeatAllocation;
        private readonly IRepositary<Cabin> _repositaryViewCabinAllocation;
        private readonly IRepositary<MeetingRoom> _repositaryViewMeetingRoom;

        public ReportService(
            IRepositary<Facility> repositaryViewFacility, 
            IRepositary<Seat> repositaryViewSeatAllocation,
            IRepositary<Cabin> repositaryViewCabinAllocation,
            IRepositary<MeetingRoom> repositaryViewMeetingRoom)
        {
            _repositaryViewFacility = repositaryViewFacility;
            _repositaryViewSeatAllocation = repositaryViewSeatAllocation;
            _repositaryViewCabinAllocation=repositaryViewCabinAllocation;
            _repositaryViewMeetingRoom = repositaryViewMeetingRoom;
        }
        //public List<ViewFacilityDTO> GetFacilityList()
        //{
        //    var item = _repositaryViewFacility.GetAll()
        //        .Include(x => x.LookUpCity)
        //        .Include(x => x.LookUpBuilding)
        //        .Select(x => new ViewFacilityDTO
        //        {
        //            FacilityFloor = x.FacilityFloor,
        //            FacilityId = x.FacilityId,
        //            CityName = x.LookUpCity.CityName,
        //            BuildingName = x.LookUpBuilding.BuildingName,
        //            FacilityName = x.FacilityName
        //        }).ToList();
        //    return item;
        //}
        public List<ViewAllocationDTO> GetSeatAllocatedView()
        {
            var item = _repositaryViewSeatAllocation.GetAll()
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
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No seats are allocated yet!");
            return item;
        }
        public List<ViewAllocationDTO> GetSeatUnAllocatdView()
        {
            var item = _repositaryViewSeatAllocation.GetAll()
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
        public List<ViewAllocationDTO> GetSeatAllocatedView(int facilityId)
        {
            var item = _repositaryViewSeatAllocation.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId != null && x.FacilityId==facilityId)
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
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No seats are allocated yet!");
            return item;
        }
        public List<ViewAllocationDTO> GetSeatUnAllocatdView(int facilityId)
        {
            var item = _repositaryViewSeatAllocation.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == null && x.FacilityId== facilityId)
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
        public List<ViewAllocationDTO> GetCabinAllocatedView()
        {
            var item = _repositaryViewCabinAllocation.GetAll()
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
                }).ToList();
            if (item.Count() == 0)
                throw new NoDataException("No cabins are allocated yet!");
            return item;
        }
        public List<ViewAllocationDTO> GetCabinUnAllocatedView()
        {
            var item = _repositaryViewCabinAllocation.GetAll()
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
        public List<ViewAllocationDTO> GetCabinUnAllocatedView(int facilityId)
        {
            var item = _repositaryViewCabinAllocation.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == null && x.FacilityId==facilityId)
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
        public List<ViewAllocationDTO> GetCabinAllocatedView(int facilityId)
        {
            var item = _repositaryViewCabinAllocation.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId != null && x.FacilityId==facilityId)
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
                }).ToList();
            if (item.Count()==0)
                throw new NoDataException("No cabins are allocated yet!");
            return item;
        }






        public List<ViewAllocationDTO> GetMeetingRoomView()
        {
            var item = _repositaryViewMeetingRoom.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Select(x => new ViewAllocationDTO
                {
                    SeatId = x.MeetingRoomId,
                    SeatNo = x.MeetingRoomNo,
                    TotalSeat = x.TotalSeat,
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor =x.Facility.FacilityFloor,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation=x.Facility.LookUpCity.CityAbbrevation
                }).ToList();

            return item;
        }
    }
}
