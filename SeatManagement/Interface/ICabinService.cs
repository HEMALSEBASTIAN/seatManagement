using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface ICabinService
    {
        public List<Cabin> Get();
        public void AddCabin(List<CabinDTO> cabinDTOList);
        public Cabin GetById(int id);
        public void AllocateCabin(int cabinId, int employeeId);
        public void DeallocateCabin(int cabinId);
        //public Cabin Allocate(AllocateDTO cabin);
        //public Cabin Deallocate(AllocateDTO cabin);
    }
}
