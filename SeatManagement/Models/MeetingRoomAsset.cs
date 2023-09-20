using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.Models
{
    public class MeetingRoomAsset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingRoomAssetId { get; set; }
        [ForeignKey("MeetingRoom")]
        public int MeetingRoomId { get; set; }
        [ForeignKey("LookUpAsset")]
        public int AssetId { get; set; }
        public int AssetQuantity { get; set; }
        public virtual MeetingRoom? MeetingRoom { get; set; }
        public virtual LookUpAsset? LookUpAsset { get; set; }
    }
}
