using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface ICabinService
    {
        public IQueryable<Cabin> Get();
        public void AddCabin(CabinDTO cabinDTOList);
        public Cabin GetById(int id);
        public void AllocateCabin(int cabinId, int employeeId);
        public void DeallocateCabin(int cabinId);
        public IQueryable<ViewAllocationDTO> GetCabinUnAllocatedView();
        public IQueryable<ViewAllocationDTO> GetCabinUnAllocatedViewByFacility(int facilityId);
        public IQueryable<ViewAllocationDTO> GetCabinUnAllocatedViewByFloor(int floorNo);
        public IQueryable<ViewAllocationDTO> GetCabinAllocatedView();
        public IQueryable<ViewAllocationDTO> ReportGenarator(string? type, int? facilityId, int? floorNo);
    }
}

//object? GetCabinUnAllocatedViewByFloor(int value);
//public Cabin Allocate(AllocateDTO cabin);
//public Cabin Deallocate(AllocateDTO cabin);
