using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeatManagement.Implementation
{
    public class CityService : ICityService
    {
        private readonly IRepositary<LookUpCity> _repositary;

        public CityService(IRepositary<LookUpCity> repositary)
        {
            _repositary=repositary;
        }
        public int Add(LookUpCityDTO cityDTO)
        {
            var City = new LookUpCity()
            {
                CityName = cityDTO.CityName,
                CityAbbrevation = cityDTO.CityAbbrevation
            };
            _repositary.Add(City);
            return City.CityId;
        }

        public List<LookUpCity> Get()
        {
            return _repositary.GetAll().ToList();
        }

        public LookUpCity GetById(int id)
        {
            var item = _repositary.GetById(id);
            return item;
        }
        public LookUpCity Update(LookUpCity City)
        {
            var item = _repositary.GetById(City.CityId);
            if (item == null)
            {
                return null;
            }
            item.CityName = City.CityName;
            item.CityAbbrevation = City.CityAbbrevation;
            _repositary.Update();
            return item;
        }
    }
}
