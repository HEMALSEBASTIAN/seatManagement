using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.Models
{
    public class MeetingRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingRoomId { get; set; }
        [ForeignKey("Facility")]
        public int FacilityId { get; set; }
        public string MeetingRoomNo { get; set; }
        public int TotalSeat { get; set; }
        public virtual Facility Facility { get; set; }
    }
}
