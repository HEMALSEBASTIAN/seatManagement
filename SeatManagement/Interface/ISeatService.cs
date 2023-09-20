using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface ISeatService
    {
        public List<Seat> Get();
        public List<Seat> Get(int pageNumber, int pageSize);
        public void AddSeat(SeatDTO seatDTO);
        public void AddSeat(List<SeatDTO> seatDTOList);
        public Seat AllocateSeat(AllocateDTO seat);
        public Seat DeAllocateSeat(AllocateDTO seat);
        public Seat GetById(int id);
    }
}
