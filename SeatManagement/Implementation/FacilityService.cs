using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class FacilityService : IFacilityService
    {
        private readonly IRepository<Facility> _repositaryFacility;
        private readonly ICityService _cityService;
        private readonly IBuildingService _buildingService;
        private readonly IMemoryCache _memoryCache;
        public readonly static string Facilitykey = "FacilityView";

        public FacilityService(IRepository<Facility> repositaryFacility,
            ICityService cityService,
            IBuildingService buildingService,
            IMemoryCache memoryCache)
        {
            _repositaryFacility=repositaryFacility;
            _cityService=cityService;
            _buildingService=buildingService;
            _memoryCache=memoryCache;
        }

        public IQueryable<ViewFacilityDTO> Get()
        {
            if (!_memoryCache.TryGetValue(Facilitykey, out IQueryable<ViewFacilityDTO> list))
            {
                list = _repositaryFacility.GetAll()
               .Include(x => x.LookUpCity)
               .Include(x => x.LookUpBuilding)
               .Select(x => new ViewFacilityDTO
               {
                   FacilityId = x.FacilityId,
                   FacilityName = x.FacilityName,
                   FacilityFloor = x.FacilityFloor,
                   CityName = x.LookUpCity.CityName,
                   BuildingName = x.LookUpBuilding.BuildingName
               });

                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                };
                _memoryCache.Set(Facilitykey, list, options);
            }
            if (list == null || !list.Any())
                throw new NoDataException("No facility\nPlease add facility first");
            return list;
        }

        public int Add(FacilityDTO facilityDTO)
        {
            var cityList = _cityService.Get().Select(x => x.CityId);
            if (!cityList.Contains(facilityDTO.CityId))
                throw new ForeignKeyViolationException("Entered city does not exist");
            
            var buildingList=_buildingService.Get().Select(x => x.BuildingId);
            if (!buildingList.Contains(facilityDTO.BuildingId))
                throw new ForeignKeyViolationException("Entered building does not exist");

            var NewFacility = new Facility()
            {
                FacilityName = facilityDTO.FacilityName,
                FacilityFloor = facilityDTO.FacilityFloor,
                BuildingId = facilityDTO.BuildingId,
                CityId = facilityDTO.CityId,
            };
            _repositaryFacility.Add(NewFacility);
            _memoryCache.Remove(Facilitykey);
            return NewFacility.FacilityId;

        }

        public Facility GetById(int id)
        {
            return _repositaryFacility.GetById(id) ?? throw new NoDataException("Entered facility Id does not exist");
        }

        public Facility Update(Facility facility)
        {
            var item = this.GetById(facility.FacilityId);

            item.FacilityFloor = facility.FacilityFloor;
            item.BuildingId = facility.BuildingId;
            item.CityId = facility.CityId;
            item.FacilityName = facility.FacilityName;
            _repositaryFacility.Update();
            _memoryCache.Remove(Facilitykey);
            return item;
        }
    }
}

//public List<FacilityCityBuildingDTO> GetView()
//{
//    var FacilityList = _repositary.GetAll()
//        .Include(x => x.LookUpBuilding)
//        .Include(y=>y.LookUpCity)
//        .Select(z=> new FacilityCityBuildingDTO
//        {
//            FacilityName=z.FacilityName,
//            CityAbbrevation=z.LookUpCity.CityAbbrevation,
//            BuildingAbbrevation=z.LookUpBuilding.BuildingAbbrevation
//        }).ToList();
//    return FacilityList;

//}