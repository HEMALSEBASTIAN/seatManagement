using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.Models
{
    public class Facility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacilityId { get; set; }
        public string? FacilityName { get; set; }
        [ForeignKey("LookUpCity")]
        public int CityId { get; set; }
        [ForeignKey("LookUpBuilding")]
        public int BuildingId { get; set; }
        public int FacilityFloor { get; set; }
        public virtual LookUpCity LookUpCity { get; set; }
        public virtual LookUpBuilding LookUpBuilding { get; set; }
    }
}
