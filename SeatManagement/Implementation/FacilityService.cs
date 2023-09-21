using Microsoft.EntityFrameworkCore;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class FacilityService : IFacilityService
    {
        private readonly IRepositary<Facility> _repositary;

        public FacilityService(IRepositary<Facility> repositary)
        {
            _repositary=repositary;
        }
        public int Add(FacilityDTO facilityDTO)
        {
            var NewFacility = new Facility()
            {
                FacilityName = facilityDTO.FacilityName,
                FacilityFloor = facilityDTO.FacilityFloor,
                BuildingId = facilityDTO.BuildingId,
                CityId = facilityDTO.CityId,
            };
            _repositary.Add(NewFacility);
            return NewFacility.FacilityId;

        }
        
        public List<ViewFacilityDTO> Get()
        {
            return _repositary.GetAll()
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
            var item = _repositary.GetById(id);
            if (item == null)
            {
                return null;
            }
            return item;
        }
        public Facility Update(Facility facility)
        {
            var item = _repositary.GetById(facility.FacilityId);
            if (item == null)
                return null;
            item.FacilityFloor = facility.FacilityFloor;
            item.BuildingId = facility.BuildingId;
            item.CityId = facility.CityId;
            item.FacilityName = facility.FacilityName;
            _repositary.Update();
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
