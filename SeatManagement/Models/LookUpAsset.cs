using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Fixed
namespace SeatManagement.Models
{
    public class LookUpAsset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssetId { get; set; }
        public string? AssetName { get; set; }
    }
}
