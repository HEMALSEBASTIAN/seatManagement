using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface IFacilityService
    {
        public int Add(FacilityDTO facilityDTO);
        public IQueryable<ViewFacilityDTO> Get();
        public Facility GetById(int id);
        public Facility Update(Facility facility);
        //public List<FacilityCityBuildingDTO> GetView();
    }
}
