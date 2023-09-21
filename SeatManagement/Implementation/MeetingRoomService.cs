using Microsoft.EntityFrameworkCore;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class MeetingRoomService : IMeetingRoomService
    {
        private readonly IRepositary<MeetingRoom> _repositary;

        public MeetingRoomService(IRepositary<MeetingRoom> repositary)
        {
            _repositary=repositary;
        }
        public void Add(List<MeetingRoomDTO> meetingRoomDTOList)
        {
            List<MeetingRoom> meetingRoomList = new List<MeetingRoom>();
            foreach(var meetingRoomDTO in meetingRoomDTOList)
            {
                meetingRoomList.Add(new MeetingRoom()
                {
                    FacilityId= meetingRoomDTO.FacilityId,
                    TotalSeat=meetingRoomDTO.TotalSeat,
                    MeetingRoomNo=meetingRoomDTO.MeetingRoomNo,
                });
            }
            _repositary.Add(meetingRoomList);
        }

        public List<ViewAllocationDTO> GetAll()
        {
            return _repositary.GetAll()
                .Include(x => x.Facility)
                .Include(x => x.Facility.LookUpBuilding)
                .Include(x => x.Facility.LookUpCity)
                .Select(x => new ViewAllocationDTO
                {
                    SeatId = x.MeetingRoomId,
                    SeatNo = x.MeetingRoomNo,
                    TotalSeat = x.TotalSeat,
                    FacilityName = x.Facility.FacilityName,
                    FacilityFloor = x.Facility.FacilityFloor,
                    BuildingAbbrevation = x.Facility.LookUpBuilding.BuildingAbbrevation,
                    CityAbbrevation = x.Facility.LookUpCity.CityAbbrevation
                }).ToList();
        }
        public MeetingRoom GetById(int id)
        {
            return _repositary.GetById(id);
        }

        public List<MeetingRoom> GetMeetingRoomByFacility(int facilityId)
        {
            var item = _repositary.GetAll()
                .Where(x => x.FacilityId == facilityId)
                .ToList();
            return item;
        }

        public MeetingRoom Update(MeetingRoom meetingRoom)
        {
            var item = _repositary.GetById(meetingRoom.MeetingRoomId);
            if (item == null)
            {
                return null;
            }
            item.MeetingRoomNo = meetingRoom.MeetingRoomNo;
            item.TotalSeat = meetingRoom.TotalSeat;
            item.FacilityId = meetingRoom.FacilityId;
            _repositary.Update();
            return item;
        }
    }
}
