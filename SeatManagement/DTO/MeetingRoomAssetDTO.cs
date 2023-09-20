using SeatManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeatManagement.DTO
{
    public class MeetingRoomAssetDTO
    {
        public int MeetingRoomId { get; set; }
        public int AssetId { get; set; }
        public int AssetQuantity { get; set; }
    }
}
