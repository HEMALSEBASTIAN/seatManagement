using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface IBuildingService
    {
        public int Add(LookUpBuildingDTO buildingDTO);
        public List<LookUpBuilding> Get();
    }
}
