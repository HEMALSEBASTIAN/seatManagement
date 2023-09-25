using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface ISeatService
    {
        public IQueryable<Seat> Get(int? pageNumber, int? pageSize);
        public void AddSeat(SeatDTO seatDTO);
        public void AllocateSeat(int seatId, int employeeId);
        public void DeAllocateSeat(int seatId);
        public Seat GetById(int id);
        public IQueryable<ViewAllocationDTO> GetSeatUnAllocatdView();
        public IQueryable<ViewAllocationDTO> GetSeatUnAllocatdViewByFacility(int facilityId);
        public IQueryable<ViewAllocationDTO> GetSeatUnAllocatedViewByFloor(int floorNo);
        public IQueryable<ViewAllocationDTO> GetSeatAllocatedView();
        public IQueryable<ViewAllocationDTO> ReportGenarator(string? type, int? facilityId, int? floorNo);


    }
}
