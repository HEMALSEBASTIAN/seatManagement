using SeatManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeatManagement.DTO
{
    public class FacilityDTO
    {
        public string? FacilityName { get; set; }
        public int CityId { get; set; }
        public int BuildingId { get; set; }
        public int FacilityFloor { get; set; }
    }
}
