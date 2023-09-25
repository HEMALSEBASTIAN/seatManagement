using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;
using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
using Microsoft.Extensions.Caching.Memory;

namespace SeatManagement.Implementation
{
    public class CityService : ICityService
    {
        private readonly IRepository<LookUpCity> _repository;
        private readonly IMemoryCache _memoryCache;
        
        public CityService(
            IRepository<LookUpCity> repository,
            IMemoryCache memoryCache)
        {
            _repository=repository;
            _memoryCache=memoryCache;
        }
        public int Add(LookUpCityDTO cityDTO)
        {
            var City = new LookUpCity()
            {
                CityName = cityDTO.CityName,
                CityAbbrevation = cityDTO.CityAbbrevation
            };
            _repository.Add(City);
            _memoryCache.Remove(FacilityService.Facilitykey);
            return City.CityId;
        }

        public IQueryable<LookUpCity> Get()
        {
            var cityList = _repository.GetAll();
            if ( cityList==null || !cityList.Any())
                throw new NoDataException("No cities found\nPlease add city first");
            return cityList;
        }
    }
}

//public LookUpCity GetById(int id)
//{
//    var item = _repositary.GetById(id);
//    return item;
//}
//public LookUpCity Update(int id, LookUpCityDTO City)
//{
//    var item = _repositary.GetById(id);
//    if (item == null)
//    {
//        return null;
//    }
//    item.CityName = City.CityName;
//    item.CityAbbrevation = City.CityAbbrevation;
//    _repositary.Update();
//    return item;
//}