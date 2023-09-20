using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.Models
{
    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }
        [ForeignKey("Facility")]
        public int FacilityId { get; set; }
        public string SeatNo { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }

        public virtual Facility Facility { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}