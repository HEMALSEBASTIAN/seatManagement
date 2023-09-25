using SeatManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.DTO
{
    public class CabinDTO
    {
        public int FacilityId { get; set; }
        public int Capacity { get; set; }
    }
}
