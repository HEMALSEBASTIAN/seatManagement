using SeatManagement.DTO;
using SeatManagement.Models;
using System.Diagnostics.Contracts;

namespace SeatManagement.Interface
{
    public interface IMeetingRoomAssetService
    {
        public void Add(MeetingRoomAssetDTO meetingRoomAssetDTO);
        public List<MeetingRoomAsset> GetAll();
        public List<MeetingRoomAssetNameDTO> GetAll(int MeetingRoomId);
        public MeetingRoomAsset GetById(int id);
        public MeetingRoomAsset Update(MeetingRoomAsset meetingRoomAsset);
    }
}
