using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface ISeatService
    {
        public List<Seat> Get();
        public List<Seat> Get(int pageNumber, int pageSize);
        public void AddSeat(List<SeatDTO> seatDTOList);
        public void AllocateSeat(int seatId, int employeeId);
        public void DeAllocateSeat(int seatId);
        public Seat GetById(int id);
        public List<ViewAllocationDTO> GetSeatUnAllocatdView();
        public List<ViewAllocationDTO> GetSeatUnAllocatdViewByFacility(int facilityId);
        public List<ViewAllocationDTO> GetSeatUnAllocatedViewByFloor(int floorNo);


    }
}
