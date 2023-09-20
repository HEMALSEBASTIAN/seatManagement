using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.DTO
{
    public class MeetingRoomDTO
    {
        public int FacilityId { get; set; }
        public string MeetingRoomNo { get; set; }
        public int TotalSeat { get; set; }
    }
}
