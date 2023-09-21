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
        //public Seat DeAllocateSeat(AllocateDTO seat);
        //public Seat AllocateSeat(AllocateDTO seat);
        //public void AddSeat(SeatDTO seatDTO);
    }
}
