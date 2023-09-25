using Microsoft.Extensions.Caching.Memory;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class BuildingService : IBuildingService
    {
        private readonly IRepository<LookUpBuilding> _repository;
        private readonly IMemoryCache _memoryCache;
        public BuildingService(
            IRepository<LookUpBuilding> repository,
            IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }
        public int Add(LookUpBuildingDTO buildingDTO)
        {
            var Building = new LookUpBuilding()
            {
                BuildingName = buildingDTO.BuildingName,
                BuildingAbbrevation = buildingDTO.BuildingAbbrevation
            };
            _repository.Add(Building);
            _memoryCache.Remove(FacilityService.Facilitykey);
            return Building.BuildingId;
        }

        public IQueryable<LookUpBuilding> Get()
        {
            var buildingList=_repository.GetAll();
            if (buildingList==null || !buildingList.Any())
                throw new NoDataException("No builings found\nPlease add builing first");
            return buildingList;
        }
    }
}
