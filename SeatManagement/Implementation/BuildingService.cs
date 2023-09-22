using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class BuildingService : IBuildingService
    {
        private readonly IRepositary<LookUpBuilding> _repositary;

        public BuildingService(IRepositary<LookUpBuilding> repositary)
        {
            _repositary = repositary;
        }
        public int Add(LookUpBuildingDTO buildingDTO)
        {
            var Building = new LookUpBuilding()
            {
                BuildingName = buildingDTO.BuildingName,
                BuildingAbbrevation = buildingDTO.BuildingAbbrevation
            };
            _repositary.Add(Building);
            return Building.BuildingId;
        }

        public List<LookUpBuilding> Get()
        {
            return _repositary.GetAll().ToList();
        }
    }
}
