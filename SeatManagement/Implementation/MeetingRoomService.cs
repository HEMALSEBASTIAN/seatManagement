using Microsoft.EntityFrameworkCore;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SeatManagement.Implementation
{
    public class MeetingRoomService : IMeetingRoomService
    {
        private readonly IRepository<MeetingRoom> _repositoryMeetingRoom;
        private readonly IMeetingRoomAssetService _meetingRoomAssetService;
        private readonly IFacilityService _facilityService;
        private readonly IAssetService _assetService;

        public MeetingRoomService(
            IRepository<MeetingRoom> repositary,
            IMeetingRoomAssetService meetingRoomAssetService,
            IFacilityService facilityService,
            IAssetService assetService)
        {
            _repositoryMeetingRoom = repositary;
            _meetingRoomAssetService= meetingRoomAssetService;
            _facilityService = facilityService;
            _assetService = assetService;
        }
        public void Add(MeetingRoomDTO meetingRoomDTO) //adding meeting room in bulk
        {
            var facilityList = _facilityService.Get().Select(x => x.FacilityId);
            if (!facilityList.Contains(meetingRoomDTO.FacilityId))
                throw new ForeignKeyViolationException("Entered facility does not exist");

            int PreviousMeetingRoomCount = _repositoryMeetingRoom.GetAll().Where(x => x.FacilityId == meetingRoomDTO.FacilityId).Count();
            List<MeetingRoom> meetingRoomList = new List<MeetingRoom>();
            for(int start=0;start< meetingRoomDTO.MeetingRoomCount;start++)
            {
                meetingRoomList.Add(new MeetingRoom()
                {
                    FacilityId = meetingRoomDTO.FacilityId,
                    TotalSeat = meetingRoomDTO.TotalSeat[start],
                    MeetingRoomNo = string.Format("M{0:D3}", ++PreviousMeetingRoomCount)
                });
            }
            _repositoryMeetingRoom.Add(meetingRoomList);
        }

        public void AllocateAsset(int MeetingRoomId, MeetingRoomAssetDTO newAsset)
        {
            var meetingRoomList = this.GetAll().Select(x => x.SeatId);
            if (!meetingRoomList.Contains(MeetingRoomId))
                throw new ForeignKeyViolationException("Entered meeting room id not in the list");

            var assetList = _assetService.Get().Select(x=>x.AssetId);
            if (!assetList.Contains(newAsset.AssetId))
                throw new ForeignKeyViolationException("Entered asset id not in the list");

            var newMeetingRoomAsset = new MeetingRoomAssetDTO()
            {
                AssetQuantity = newAsset.AssetQuantity,
                AssetId = newAsset.AssetId,
                MeetingRoomId = MeetingRoomId
            };
            _meetingRoomAssetService.Add(newMeetingRoomAsset);
        }

        public IQueryable<ViewAllocationDTO> GetAll()
        {
            var meetingRoomList= _repositoryMeetingRoom.GetAll()
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
                });
            if (meetingRoomList == null || !meetingRoomList.Any())
                throw new NoDataException("No meeting room found\nPlease add meeting room first");
            return meetingRoomList;
        }
        public MeetingRoom GetById(int id)
        {
            return _repositoryMeetingRoom.GetById(id) ?? throw new NoDataException("Enter meeting room Id does not exist");
        }

        public IQueryable<MeetingRoom> GetMeetingRoomByFacility(int facilityId)
        {
            var meetingRoomList = _repositoryMeetingRoom.GetAll()
                .Where(x => x.FacilityId == facilityId);

            if (meetingRoomList == null || !meetingRoomList.Any())
                throw new NoDataException($"No meeting room in facility id: {facilityId}");
            return meetingRoomList;
        }

        public MeetingRoom Update(MeetingRoom meetingRoom)
        {
            var item = this.GetById(meetingRoom.MeetingRoomId);
                
            item.MeetingRoomNo = meetingRoom.MeetingRoomNo;
            item.TotalSeat = meetingRoom.TotalSeat;
            item.FacilityId = meetingRoom.FacilityId;
            _repositoryMeetingRoom.Update();
            return item;
        }
    }
}
