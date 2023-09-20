using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Fixed
namespace SeatManagement.Models
{
    [Index(nameof(LookUpBuilding.BuildingAbbrevation), IsUnique = true)]
    public class LookUpBuilding
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public string? BuildingAbbrevation { get; set; }
    }
}
