using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Fixed
namespace SeatManagement.Models
{
    [Index(nameof(LookUpCity.CityAbbrevation),IsUnique=true)]
    public class LookUpCity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public string? CityAbbrevation { get; set; }
    }
}
