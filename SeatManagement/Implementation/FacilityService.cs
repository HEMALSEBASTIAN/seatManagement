using Microsoft.EntityFrameworkCore;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class FacilityService : IFacilityService
    {
        private readonly IRepositary<Facility> _repositaryFacility;
        private readonly IRepositary<LookUpCity> _repositaryCity;
        private readonly IRepositary<LookUpBuilding> _repositaryBuilding;

        public FacilityService(IRepositary<Facility> repositaryFacility, 
            IRepositary<LookUpCity> repositaryCity, 
            IRepositary<LookUpBuilding> repositaryBuilding)
        {
            _repositaryFacility=repositaryFacility;
            _repositaryCity=repositaryCity;
            _repositaryBuilding = repositaryBuilding;
        }
        public int Add(FacilityDTO facilityDTO)
        {
            var cityList = _repositaryCity.GetAll().Select(x => x.CityId);
            if (!cityList.Contains(facilityDTO.CityId))
                throw new ForeignKeyViolationException("Entered city does not exist");
            
            var buildingList=_repositaryBuilding.GetAll().Select(x => x.BuildingId);
            if (!buildingList.Contains(facilityDTO.BuildingId))
                throw new ForeignKeyViolationException("Entered builging does not exist");

            var NewFacility = new Facility()
            {
                FacilityName = facilityDTO.FacilityName,
                FacilityFloor = facilityDTO.FacilityFloor,
                BuildingId = facilityDTO.BuildingId,
                CityId = facilityDTO.CityId,
            };
            _repositaryFacility.Add(NewFacility);
            return NewFacility.FacilityId;

        }
        
        public List<ViewFacilityDTO> Get()
        {
            return _repositaryFacility.GetAll()
                .Include(x => x.LookUpCity)
                .Include(x => x.LookUpBuilding)
                .Select(x => new ViewFacilityDTO
                {
                    FacilityId = x.FacilityId,
                    FacilityName = x.FacilityName,
                    FacilityFloor = x.FacilityFloor,
                    CityName = x.LookUpCity.CityName,
                    BuildingName = x.LookUpBuilding.BuildingName
                }).ToList();
        }

        public Facility GetById(int id)
        {
            var item = _repositaryFacility.GetById(id);
            if (item == null)
            {
                return null;
            }
            return item;
        }
        public Facility Update(Facility facility)
        {
            var item = _repositaryFacility.GetById(facility.FacilityId);
            if (item == null)
                return null;
            item.FacilityFloor = facility.FacilityFloor;
            item.BuildingId = facility.BuildingId;
            item.CityId = facility.CityId;
            item.FacilityName = facility.FacilityName;
            _repositaryFacility.Update();
            return item;
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
    }
}
