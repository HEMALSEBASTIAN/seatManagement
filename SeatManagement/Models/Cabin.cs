using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.Models
{
    public class Cabin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CabinId { get; set; }
        [ForeignKey("Facility")]
        public int FacilityId { get; set; }
        public string CabinNo { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Facility Facility { get; set; }
    }
}
