using Microsoft.AspNetCore.Mvc;
using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface ICityService
    {
        public int Add(LookUpCityDTO cityDTO);
        public List<LookUpCity> Get();
        //public LookUpCity GetById(int id);
        //public LookUpCity Update(int id, LookUpCityDTO City);
    }
}
