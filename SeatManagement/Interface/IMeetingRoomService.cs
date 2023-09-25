using SeatManagement.DTO;
using SeatManagement.Models;
namespace SeatManagement.Interface
{
    public interface IMeetingRoomService
    {
        public void Add(MeetingRoomDTO meetingRoomDTO);
        public IQueryable<ViewAllocationDTO> GetAll();
        public MeetingRoom GetById(int id);
        public MeetingRoom Update(MeetingRoom meetingRoom);
        public IQueryable<MeetingRoom> GetMeetingRoomByFacility(int  facilityId);
        public void AllocateAsset(int MeetingRoomId, MeetingRoomAssetDTO newAsset);
    }
}
