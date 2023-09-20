using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.DTO
{
    public class SeatDTO
    {
        public int FacilityId { get; set; }
        public string SeatNo { get; set; }
    }
}
