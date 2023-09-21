using SeatManagement.DTO;
using SeatManagement.Models;
namespace SeatManagement.Interface
{
    public interface IMeetingRoomService
    {
        public void Add(List<MeetingRoomDTO> meetingRoomDTOList);
        public List<ViewAllocationDTO> GetAll();
        public MeetingRoom GetById(int id);
        public MeetingRoom Update(MeetingRoom meetingRoom);
        public List<MeetingRoom> GetMeetingRoomByFacility(int  facilityId);
    }
}
