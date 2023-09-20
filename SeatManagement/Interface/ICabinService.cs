using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface ICabinService
    {
        public List<Cabin> Get();
        public void AddCabin(List<CabinDTO> cabinDTOList);
        public Cabin Allocate(AllocateDTO cabin);
        public Cabin Deallocate(AllocateDTO cabin);
        public Cabin GetById(int id);
    }
}
